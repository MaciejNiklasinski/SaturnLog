using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SaturnLog.Core.EventArgs;
using SaturnLog.Core.Exceptions;

namespace SaturnLog.Core
{
    public class Saturn5Services
    {
        #region Const
        public const Saturn5Status InitialSaturn5Status = Saturn5Status.InDepot;
        #endregion

        #region Private Fields
        private App _app;
        #endregion

        #region Private Properties
        private LogsContentConstructor LogsContentConstructor { get { return this._app.LogsContentConstructor; } }

        private IDataRepository DataRepository { get { return this._app.DataRepository; } }
        private IUserRepository UserRepository { get { return this._app.UserRepository; } }
        private ISaturn5Repository Saturn5Repository { get { return this._app.Saturn5Repository; } }
        private ISaturn5IssuesRepository Saturn5IssuesRepository { get { return this._app.Saturn5IssuesRepository; } }
        private ISaturns5MovementRepository Saturns5MovementRepository { get { return this._app.Saturns5MovementRepository; } }
        private ISaturns5DashboardRepository Saturns5DashboardRepository { get { return this._app.Saturns5DashboardRepository; } }
        #endregion

        #region Constructor
        public Saturn5Services(App app)
        {
            this._app = app;
        }
        #endregion

        #region Methods
        #region Archive
        // Archive
        // Send damage, faulty or surplus unit to IT.
        public async Task SendToITAsync(string serialNumber, string consignmentNumber, string incidentNumber, string movementNote, bool surplus)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameters validation
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

                // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
                serialNumber = serialNumber.Trim().ToUpper();
                consignmentNumber = consignmentNumber?.Trim()?.ToUpper();
                incidentNumber = incidentNumber?.Trim()?.ToUpper();
                movementNote = movementNote?.Trim();

                // If saturn doest exists return
                if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));
                // If provided serial number is associated with the last unit in the depot stock, throw exception
                else if (this.Saturn5Repository.Count() == 1)
                    throw new AttemptToRemoveLastSaturn5Exception(serialNumber);

                // Gather data...
                this.GatherSendToITData(serialNumber, surplus, out string sentBy, out bool damaged, out bool faulted, out string damagedByUsername, out string faultReportedByUsername, out string sentToITLog);

                // Adjust data ..
                // .. nothing.
                
                // Commit data in appropriate way, depending on whether the unit ..

                // .. has been send because it has physically damages and faults ..
                if (damaged && faulted)
                    await Task.Run(() => { this.CommitSendToITFaultedAndDamagedData(serialNumber, consignmentNumber, incidentNumber, sentBy, damagedByUsername, faultReportedByUsername, sentToITLog, movementNote); });

                // .. has been send because it has physically damages ..
                else if (damaged)
                    await Task.Run(() => { this.CommitSendToITOnlyDamagedData(serialNumber, consignmentNumber, incidentNumber, sentBy, damagedByUsername, sentToITLog, movementNote); });

                // .. has been send because it has physically faults ..
                else if (faulted)
                    await Task.Run(() => { this.CommitSendToITOnlyFaultedData(serialNumber, consignmentNumber, incidentNumber, sentBy, faultReportedByUsername, sentToITLog, movementNote); });

                // .. has been send as surplus ..
                else if (surplus)
                    await Task.Run(() => { this.CommitSendToITFullyFunctionalSurplusData(serialNumber, consignmentNumber, incidentNumber, sentBy, sentToITLog, movementNote); });
                // .. or throw exception.
                else
                    throw new ArgumentException("Saturn 5 unit without any reported faults, and/or damages cannot be returned, unless marked as Surplus.", nameof(surplus));
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }
        #endregion

        // Crud - Create or Unarchive
        #region Create or Unarchive
        // Receive new unit from IT.
        public async Task ReceiveFromITAsync(string serialNumber, string shortId, string phoneNumber, string consignmentNumber, string movementNote)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameters validation & format correction
                // If saturn doest exists return
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
                else if (shortId is null) throw new ArgumentNullException(nameof(shortId));
                else if (serialNumber.Trim() == "") throw new ArgumentException("Serial number cannot be empty.", nameof(serialNumber));
                else if (shortId.Trim() == "") throw new ArgumentException("Short id cannot be empty.", nameof(shortId));

                // Assure that all the characters used in provided serial number and short id strings are upper case, and 'empty space' doesn't perpend, or append the strings.
                serialNumber = serialNumber.Trim().ToUpper();
                shortId = shortId.Trim().ToUpper();
                phoneNumber = phoneNumber?.Trim()?.ToUpper();
                consignmentNumber = consignmentNumber?.Trim()?.ToUpper();
                movementNote = movementNote?.Trim();

                // Parameters validation
                if (this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is associated with Saturn5 already existing in the stock.", nameof(serialNumber));
                else if (this.Saturn5Repository.HasSaturn5ShortId(shortId))
                    throw new ArgumentException($"Provided Saturn5 short id: {shortId} is not associated with Saturn5 already existing in the stock.", nameof(shortId));

                // Gather data
                this.GatherReceiveFromITData(serialNumber, out string receivedBy, out string receivedDate, out string receivedTime, out string receiveFromITLog);

                // Adjust data ..
                // .. nothing.

                // Commit saturn 5 receive from IT data
                await Task.Run(() => { this.CommitReceiveFromITData(serialNumber, shortId, phoneNumber, consignmentNumber, receivedBy, receivedDate, receivedTime, receiveFromITLog, movementNote); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        } 
        
        // Add unit into the depot stock - without logging it into the saturn 5 movement repository
        public async Task CreateAsync(string serialNumber, string shortId, string phoneNumber)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameters validation & format correction
                // If saturn doest exists return
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
                else if (shortId is null) throw new ArgumentNullException(nameof(shortId));
                else if (serialNumber.Trim() == "") throw new ArgumentException("Serial number cannot be empty.", nameof(serialNumber));
                else if (shortId.Trim() == "") throw new ArgumentException("Short id cannot be empty.", nameof(shortId));

                // Assure that all the characters used in provided serial number and short id strings are upper case, and 'empty space' doesn't perpend, or append the strings.
                serialNumber = serialNumber.Trim().ToUpper();
                shortId = shortId.Trim().ToUpper();
                phoneNumber = phoneNumber?.Trim()?.ToUpper();

                // Parameters validation
                if (this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is associated with Saturn5 already existing in the stock.", nameof(serialNumber));
                else if (this.Saturn5Repository.HasSaturn5ShortId(shortId))
                    throw new ArgumentException($"Provided Saturn5 short id: {shortId} is not associated with Saturn5 already existing in the stock.", nameof(shortId));

                // Gather data
                this.GatherCreateData(serialNumber, out string receivedBy, out string receivedDate, out string receivedTime, out string receiveFromITLog);

                // Adjust data ..
                // .. nothing.

                // Commit saturn 5 receive from IT data
                await Task.Run(() => { this.CommitCreateData(serialNumber, shortId, phoneNumber, receivedBy, receivedDate, receivedTime, receiveFromITLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }

        // Reports new unit fault.
        public async Task ReportFaultAsync(string serialNumber, string byUsername, string description)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Validate parameters.
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
                if (byUsername is null) throw new ArgumentNullException(nameof(byUsername));
                else if (description is null) throw new ArgumentNullException(nameof(description));
                else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));
                else if (!this.UserRepository.HasUsername(byUsername)) throw new ArgumentException($"Provided User username {byUsername} is not recognized.", nameof(byUsername));
                else if (description.Trim() == "") throw new ArgumentException("Fault description cannot be empty.", nameof(description));

                // Gather ..
                this.GatherReportFaultData(serialNumber, byUsername, description, out Saturn5 faultySaturn5, out bool hasUnresolvedDamages, out string reportFaultLog);

                // Adjust ..
                this.AdjustReportFaultData(faultySaturn5, hasUnresolvedDamages);

                // Commit ..
                await Task.Run(() => { this.CommitReportFaultData(faultySaturn5, byUsername, description.Trim(), reportFaultLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }

        // Reports new unit damage.
        public async Task ReportDamageAsync(string serialNumber, string byUsername, string description)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Validate parameters.
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
                if (byUsername is null) throw new ArgumentNullException(nameof(byUsername));
                else if (description is null) throw new ArgumentNullException(nameof(description));
                else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));
                else if (!this.UserRepository.HasUsername(byUsername)) throw new ArgumentException($"Provided User username {byUsername} is not recognized.", nameof(byUsername));
                else if (description.Trim() == "") throw new ArgumentException("Fault description cannot be empty.", nameof(description));

                // Gather ..
                this.GatherReportDamageData(serialNumber, byUsername, description, out Saturn5 damageSaturn5, out string reportDamageLog);

                // Adjust ..
                this.AdjustReportDamageData(damageSaturn5);

                // Commit ..
                await Task.Run(() => { this.CommitReportDamageData(damageSaturn5, byUsername, description.Trim(), reportDamageLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }
        #endregion

        // Crud - Read
        #region Read
        // Answers the question whether provided string is recognized as valid saturn 5 serial number.
        public bool HasSerialNumber(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(serialNumber);

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            return this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber);
        }

        // Answers the question whether provided string is recognized as valid saturn 5 short id.
        public bool HasShortId(string shortId)
        {
            // Parameters validation
            if (shortId is null) throw new ArgumentNullException(shortId);

            // Assure that all the characters used in provided short id string are upper case, and 'empty space' doesn't perpend, or append the username.
            shortId = shortId.Trim().ToUpper();

            return this.Saturn5Repository.HasSaturn5ShortId(shortId);
        }

        // Answers the question whether provided string is a valid saturn 5 serial number associated with 
        // saturn 5 unit which currently has at least one unresolved issue, or damage.
        public bool HasUnresolvedIssues(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.HasUnresolvedIssues(serialNumber);
        }

        // Answers the question whether provided string is a valid saturn 5 serial number associated with 
        // saturn 5 unit which currently has at least one unresolved fault - which is not physical damage.
        public bool HasUnresolvedFaults(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.HasUnresolvedFaults(serialNumber);
        }

        // Answers the question whether provided string is a valid saturn 5 serial number associated with 
        // saturn 5 unit which currently has at least one unresolved damage.
        public bool HasUnresolvedDamages(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.HasUnresolvedDamages(serialNumber);
        }

        // Returns saturn 5 associated with provided serial number.
        public Saturn5 GetBySerialNumber(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(serialNumber);

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5Repository.Read(serialNumber);
        }

        // Returns saturn associated with provided serial number asynchronously.
        public async Task<Saturn5> GetBySerialNumberAsync(string serialNumber)
        {
            return await Task.Run(() => { return this.GetBySerialNumber(serialNumber); });
        }

        // Returns saturn associated with provided short id.
        public Saturn5 GetByShortId(string shortId)
        {
            // Parameters validation
            if (shortId is null) throw new ArgumentNullException(shortId);

            // Assure that all the characters used in provided short id string are upper case, and 'empty space' doesn't perpend, or append the username.
            shortId = shortId.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5ShortId(shortId))
                throw new ArgumentException($"Provided Saturn5 short id: {shortId} is not associated with any of the existing saturn 5 units.", nameof(shortId));

            // Obtain serial number based on the provided newShortId
            string serialNumber = this.Saturn5Repository.GetSerialNumberFromShortId(shortId);

            return this.Saturn5Repository.Read(serialNumber);
        }

        // Returns saturn associated with provided short id asynchronously.
        public async Task<Saturn5> GetByShortIdAsync(string shortId)
        {
            return await Task<Saturn5>.Run(() => { return this.GetByShortId(shortId); });
        }

        // Returns last unresolved saturn 5 fault or damage associated with the provided serial number, or throw appropriate exception if not available.
        public Saturn5Issue GetLastUnresolvedIssue(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.GetLastUnresolvedIssue(serialNumber);
        }

        // Returns last unresolved saturn 5 fault or damage associated with the provided serial number, or throw appropriate exception if not available.
        public async Task<Saturn5Issue> GetLastUnresolvedIssueAsync(string serialNumber)
        {
            return await Task<Saturn5Issue>.Run(() => { return this.GetLastUnresolvedIssue(serialNumber); });
        }

        // Returns last unresolved saturn 5 fault (but not damage) associated with the provided serial number, or throw appropriate exception if not available.
        public Saturn5Issue GetLastUnresolvedFault(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.GetLastUnresolvedFault(serialNumber);
        }

        // Returns last unresolved saturn 5 fault (but not damage) associated with the provided serial number, or throw appropriate exception if not available.
        public async Task<Saturn5Issue> GetLastUnresolvedFaultAsync(string serialNumber)
        {
            return await Task<Saturn5Issue>.Run(() => { return this.GetLastUnresolvedFault(serialNumber); });
        }

        // Returns last unresolved saturn 5 damage (but not fault) associated with the provided serial number, or throw appropriate exception if not available.
        public Saturn5Issue GetLastUnresolvedDamage(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.GetLastUnresolvedDamage(serialNumber);
        }

        // Returns last unresolved saturn 5 damage (but not fault) associated with the provided serial number, or throw appropriate exception if not available.
        public async Task<Saturn5Issue> GetLastUnresolvedDamageAsync(string serialNumber)
        {
            return await Task<Saturn5Issue>.Run(() => { return this.GetLastUnresolvedDamage(serialNumber); });
        }

        // Return all (resolved and unresolved) saturn 5 faults and damages.
        public IList<Saturn5Issue> GetIssues(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.GetIssues(serialNumber);
        }

        // Return all (resolved and unresolved) saturn 5 faults and damages.
        public async Task<IList<Saturn5Issue>> GetIssuesAsync(string serialNumber)
        {
            return await Task<IList<Saturn5Issue>>.Run(() => { return this.GetIssues(serialNumber); });
        }

        // Get unresolved faults and damages related with the Saturn 5 unit associated with the provided serial number.
        public IList<Saturn5Issue> GetUnresolvedIssues(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.GetUnresolvedIssues(serialNumber);
        }

        // Get unresolved faults and damages related with the Saturn 5 unit associated with the provided serial number.
        public async Task<IList<Saturn5Issue>> GetUnresolvedIssuesAsync(string serialNumber)
        {
            return await Task<IList<Saturn5Issue>>.Run(() => { return this.GetUnresolvedIssues(serialNumber); });
        }

        // Get unresolved faults related with the Saturn 5 unit associated with the provided serial number.
        public IList<Saturn5Issue> GetUnresolvedFaults(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.GetUnresolvedFaults(serialNumber);
        }

        // Get unresolved faults related with the Saturn 5 unit associated with the provided serial number.
        public async Task<IList<Saturn5Issue>> GetUnresolvedFaultsAsync(string serialNumber)
        {
            return await Task<IList<Saturn5Issue>>.Run(() => { return this.GetUnresolvedFaultsAsync(serialNumber); });
        }

        // Get unresolved damages related with the Saturn 5 unit associated with the provided serial number.
        public IList<Saturn5Issue> GetUnresolvedDamages(string serialNumber)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
            serialNumber = serialNumber.Trim().ToUpper();

            // If saturn doest exists return
            if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

            return this.Saturn5IssuesRepository.GetUnresolvedDamages(serialNumber);
        }

        // Get unresolved damages related with the Saturn 5 unit associated with the provided serial number.
        public async Task<IList<Saturn5Issue>> GetUnresolvedDamagesAsync(string serialNumber)
        {
            return await Task<IList<Saturn5Issue>>.Run(() => { return this.GetUnresolvedDamages(serialNumber); });
        }
        #endregion

        // Crud - Update
        #region Update
        // Resolve currently unresolved saturn 5 fault or damage.
        public async Task ResolveIssueAsync(string serialNumber, string timestamp, Saturn5IssueStatus resolvedHow, string resolvedHowDescription)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameters validation
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
                else if (timestamp is null) throw new ArgumentNullException(nameof(timestamp));
                else if ((resolvedHow != Saturn5IssueStatus.Resolved) 
                    && (resolvedHow != Saturn5IssueStatus.KnownIssue) 
                    && (resolvedHow != Saturn5IssueStatus.CannotReplicate) 
                    && (resolvedHow != Saturn5IssueStatus.NotAnIssue))
                    throw new ArgumentException("Provided resolvedHow enum has invalid value for this method. Only following enum Saturn5IssueStatus values are allowed: " +
                        "Saturn5IssueStatus.Resolved, Saturn5IssueStatus.KnownIssue, Saturn5IssueStatus.CannotReplicate, Saturn5IssueStatus.NotAnIssue.", nameof(resolvedHow));

                // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
                serialNumber = serialNumber.Trim().ToUpper();

                // If saturn doest exists return
                if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));

                // Get data necessary for the operation based on the provided serial number, saturn 5 issue timestamp, 
                // and resolvedHow Saturn5IssueStatus enum indicating how the issue has been resolved.
                this.GatherResolveIssueData(serialNumber, timestamp, resolvedHow,
                    // Saturn 5 unit associated to provided serial number.
                    out Saturn5 saturn5, 
                    // Saturn 5 Issue to be resolved.
                    out Saturn5Issue toBeResolve, 
                    // Saturn 5 Issue resolved log.
                    out string issueResolvedLog, 
                    // Saturn 5 has other (then currently being resolved) unresolved damages.
                    out bool hasOtherUnresolvedDamages, 
                    // Saturn 5 has other (then currently being resolved) unresolved faults.
                    out bool hasOtherUnresolvedFaults);
                
                // Adjust data...
                this.AdjustResolveUnitIssueData(saturn5, toBeResolve, resolvedHow, issueResolvedLog, hasOtherUnresolvedDamages, hasOtherUnresolvedFaults);

                // Commit data...
                await Task.Run(() => { this.CommitResolveIssueData(saturn5, toBeResolve, issueResolvedLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }

        // Edit saturn 5 short id and/or phone number.
        public async Task EditAsync(string serialNumber, string newShortId, string newPhoneNumber)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameters validation
                if (serialNumber is null) throw new ArgumentNullException(serialNumber);
                else if (newShortId is null && newPhoneNumber is null) throw new ArgumentException("Either newShortId or newPhoneNumber must not be null, and different than current value.");
                
                // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the the string.
                serialNumber = serialNumber.Trim().ToUpper();
                newShortId = newShortId?.Trim()?.ToUpper();
                newPhoneNumber = newPhoneNumber?.Trim()?.ToUpper();

                // If saturn 5 associated with provided serial number doest exist ..
                if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    // .. throw appropriate exception.
                    throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));
                // .. otherwise if new short id has been provided...
                else if (!(newShortId is null)
                    // .. and its consist of either empty string, or string already taken as newShortId.
                    && (newShortId == "" || this.Saturn5Repository.HasSaturn5ShortId(newShortId)))
                    // .. throw appropriate exception.
                    throw new ArgumentException($"Provided Saturn5 new short id: {newShortId} must be not empty unique string.", nameof(newShortId));

                // Gather..
                this.GatherEditData(serialNumber, ref newShortId, ref newPhoneNumber, out string editLog);
                
                // Adjust ..
                // .. nothing.

                // Commit ..
                await Task.Run(() => {this.CommitEditData(serialNumber, newShortId, newPhoneNumber, editLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }
        #endregion

        // Crud - Delete
        #region Delete
        // Remove unit associated with specified serial number.
        public async Task RemoveAsync(string serialNumber)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {   
                // Parameters validation
                if (serialNumber is null) throw new ArgumentNullException(serialNumber);

                // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the the string.
                serialNumber = serialNumber.Trim().ToUpper();

                // If saturn 5 associated with provided serial number doesn't exists throw exception
                if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    throw new ArgumentException($"Provided Saturn5 serial number: {serialNumber} is not associated with any of the existing saturn 5 units.", nameof(serialNumber));
                // If provided serial number is associated with the last unit in the depot stock, throw exception
                else if (this.Saturn5Repository.Count() == 1)
                    throw new AttemptToRemoveLastSaturn5Exception(serialNumber);

                // Gather ..
                this.GatherRemoveData(serialNumber, out Saturn5 toBeRemoveSaturn5, out string saturn5RemoveLog);

                // Adjust ..
                // .. nothing.

                // Commit ..
                await Task.Run(() => { this.CommitRemoveData(serialNumber, saturn5RemoveLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }
        #endregion
        #endregion

        #region Private helpers
        #region Gather Data
        // SendToIT - Obtain necessary raw data. 
        private void GatherSendToITData(string serialNumber, bool surplus, out string sentBy, out bool damaged, out bool faulted, out string damagedBy, out string faultReportedBy, out string sentToITLog)
        {
            // Assure that currently logged user will be associated with obtaining this unit from IT.\
            sentBy = this._app.LoggedUsername;

            // Set info.Damaged flag base on whether Saturn 5 has any unresolved damages or not.
            damaged = this.Saturn5IssuesRepository.HasUnresolvedDamages(serialNumber);

            // Set info.Faulted flag base on whether Saturn 5 has any unresolved faults or not.
            faulted = this.Saturn5IssuesRepository.HasUnresolvedFaults(serialNumber);

            // Get unit designated to be sent to IT
            Saturn5 toBeSentToIT = this.Saturn5Repository.Read(serialNumber);

            // Get unresolved saturn 5 damages and faults.
            IList<Saturn5Issue> saturn5UnresolvedDamages = this.Saturn5IssuesRepository.GetUnresolvedDamages(serialNumber);
            IList<Saturn5Issue> saturn5UnresolvedFaults = this.Saturn5IssuesRepository.GetUnresolvedFaults(serialNumber);

            // Get last unresolved saturn 5 damage/fault.
            Saturn5Issue saturn5LastUnresolvedDamage = saturn5UnresolvedDamages.LastOrDefault();
            Saturn5Issue saturn5LastUnresolvedFault = saturn5UnresolvedFaults.LastOrDefault();

            // If able to obtain last unresolved fault/damage, use it to obtain damagedByUsername and faultReportedByUsername
            damagedBy = saturn5LastUnresolvedDamage?.ReportedByUsername;
            faultReportedBy = saturn5LastUnresolvedFault?.ReportedByUsername;

            // Declare issue description variable
            if (damaged)
                sentToITLog = this.LogsContentConstructor.GetDamagedSaturn5SentToITLog(toBeSentToIT, saturn5UnresolvedDamages, saturn5UnresolvedFaults, surplus);
            else if (faulted)
                sentToITLog = this.LogsContentConstructor.GetFaultedSaturn5SentToITLog(toBeSentToIT, saturn5UnresolvedFaults, surplus);
            else if (surplus)
                sentToITLog = this.LogsContentConstructor.GetFullyFunctionalSurplusSaturn5SentToITLog(toBeSentToIT);
            else
                throw new ArgumentException("Unable to send to IT fully functional Saturn 5 unit, unless it has been marked as 'surplus'.", nameof(surplus));
        }

        // ReceiveFromIT - Obtain necessary raw data. 
        private void GatherReceiveFromITData(string serialNumber, out string receivedBy, out string receivedDate, out string receivedTime, out string receiveFromITLog)
        {
            // Get saturn 5 creation timestamp parts.
            DateTimeExtensions.GetTimestampParams(out DateTime now, out receivedDate, out receivedTime);

            // Assure that currently logged user will be associated with obtaining this unit from IT.\
            receivedBy = this._app.LoggedUsername;

            // Obtain received form IT log.
            receiveFromITLog = this.LogsContentConstructor.GetSaturn5ReceivedFromITLog(serialNumber);
        }

        // Create - Obtain necessary raw data. 
        private void GatherCreateData(string serialNumber, out string receivedBy, out string receivedDate, out string receivedTime, out string receiveFromITLog)
        {
            // Get saturn 5 creation timestamp parts.
            DateTimeExtensions.GetTimestampParams(out DateTime now, out receivedDate, out receivedTime);

            // Assure that currently logged user will be associated adding unit into the depot stock
            receivedBy = this._app.LoggedUsername;

            // Obtain received form IT log.
            receiveFromITLog = this.LogsContentConstructor.GetCreateSaturn5Log(serialNumber);
        }

        // ReportFaultData - Obtain necessary raw data. 
        private void GatherReportFaultData(string serialNumber, string byUsername, string faultDescription, out Saturn5 saturn5, out bool hasUnresolvedDamages, out string reportFaultLog)
        {
            // Get saturn 5 associated with the provided serial number.
            saturn5 = this.Saturn5Repository.Read(serialNumber);

            // Get unresolved saturn 5 damages;
            hasUnresolvedDamages = this.Saturn5IssuesRepository.GetUnresolvedDamages(serialNumber).Count > 0;

            // Get user reporting for faults.
            User reportingUser = this.UserRepository.Read(byUsername);

            // Get report fault log
            reportFaultLog = this.LogsContentConstructor.GetReportSaturn5FaultLog(reportingUser, saturn5, faultDescription);
        }

        // ReportFaultData - Obtain necessary raw data. 
        private void GatherReportDamageData(string serialNumber, string byUsername, string damageDescription, out Saturn5 saturn5, out string reportDamageLog)
        {
            // Get saturn 5 associated with the provided serial number.
            saturn5 = this.Saturn5Repository.Read(serialNumber);

            // Get user responsible for damages.
            User responsibleUser = this.UserRepository.Read(byUsername); 

            // Get report damage log
            reportDamageLog = this.LogsContentConstructor.GetReportSaturn5DamageLog(responsibleUser, saturn5, damageDescription);
        }

        // ResolveIssue - Obtain necessary raw data. 
        private void GatherResolveIssueData(string serialNumber, string timestamp, Saturn5IssueStatus resolvedHow, out Saturn5 saturn5, out Saturn5Issue toBeResolve, out string issueResolvedLog, out bool hasOtherUnresolvedDamages, out bool hasOtherUnresolvedFaults)
        {
            // Get unresolved issue related with provided timestamp....
            toBeResolve = this.Saturn5IssuesRepository.GetUnresolvedIssues(serialNumber)
                // or default
                .FirstOrDefault((issue) => { return issue.Timestamp == timestamp; });

            // If no saturn 5 issue associated with the provided timestamp and serial number can be found throw appropriate exception.
            if (toBeResolve is null)
                throw new ArgumentException($"Provided timestamp: {timestamp} is not related with any the outstanding issues of the Saturn 5 serial number: {serialNumber}", nameof(timestamp));

            // Get saturn 5 associated with the provided serial number.
            saturn5 = this.Saturn5Repository.Read(serialNumber);
            
            // Get unresolved saturn 5 damages and faults, other then the one designated to be resolved.
            IEnumerable<Saturn5Issue> saturn5UnresolvedDamages = this.Saturn5IssuesRepository.GetUnresolvedDamages(serialNumber).Where((issue) => { return issue.Timestamp != timestamp; });
            IEnumerable<Saturn5Issue> saturn5UnresolvedFaults = this.Saturn5IssuesRepository.GetUnresolvedFaults(serialNumber).Where((issue) => { return issue.Timestamp != timestamp; });

            // Get flags indicating whether saturn 5 unit specified with the provided serial number,
            // has any other unresolved faults and/or any other unresolved damages, other then the one
            // specified with the provided timestamp.
            hasOtherUnresolvedDamages = (saturn5UnresolvedDamages.Count() > 0);
            hasOtherUnresolvedFaults = (saturn5UnresolvedFaults.Count() > 0);

            switch (resolvedHow)
            {
                case Saturn5IssueStatus.Resolved:
                    issueResolvedLog = this.LogsContentConstructor.GetSaturn5IssueResolvedInDepotLog(toBeResolve);
                    break;
                case Saturn5IssueStatus.KnownIssue:
                    issueResolvedLog = this.LogsContentConstructor.GetSaturn5IssueMarkedAsKnownIssueLog(toBeResolve);
                    break;
                case Saturn5IssueStatus.CannotReplicate:
                    issueResolvedLog = this.LogsContentConstructor.GetSaturn5IssueCannotBeReplicatedLog(toBeResolve);
                    break;
                case Saturn5IssueStatus.NotAnIssue:
                    issueResolvedLog = this.LogsContentConstructor.GetSaturn5IssueMarkedAsNotAnIssueLog(toBeResolve);
                    break;
                default:
                    throw new ArgumentException($"Provided enum Saturn5Issue has invalid value to be used with this method. Please provide one of following enum values: Saturn5IssueStatus.Resolved, Saturn5IssueStatus.KnownIssue, Saturn5IssueStatus.CannotReplicate, Saturn5IssueStatus.NotAnIssue", nameof(resolvedHow));
            }
        }

        // Edit - Obtain necessary raw data. 
        private void GatherEditData(string serialNumber, ref string newShortId, ref string newPhoneNumber, out string editLog)
        {
            // Get instance of the saturn 5 to be edited, and build the saturn 5 edit log based on it.
            Saturn5 toBeEdited = this.Saturn5Repository.Read(serialNumber);

            // If string provided as new short id and current unit short id are equal set it to null.
            if (toBeEdited.ShortId == newShortId)
                newShortId = null;

            // If string provided as new phone number and current phone unit are equal set it to null.
            if (toBeEdited.PhoneNumber == newPhoneNumber)
                newPhoneNumber = null;

            // Get appropriate saturn 5 edit log depending on whether 
            // both short id and phone number of the unit getting edited.
            if (!(newShortId is null) && !(newPhoneNumber is null))
                editLog = this.LogsContentConstructor.GetSaturn5EditShortIdAndPhoneNumberLog(toBeEdited, newShortId, newPhoneNumber);
            // .. only short id of the unit getting edited.
            else if (!(newShortId is null))
                editLog = this.LogsContentConstructor.GetSaturn5EditShortIdLog(toBeEdited, newShortId);
            // .. only phone number of the unit getting edited.
            else if (!(newPhoneNumber is null))
                editLog = this.LogsContentConstructor.GetSaturn5EditPhoneNumberLog(toBeEdited, newShortId);
            // otherwise throw exception.
            else
                throw new ArgumentException("Either newShortId or newPhoneNumber must not be null, and different than current value.");
        }

        // Remove - Obtain necessary raw data. 
        private void GatherRemoveData(string serialNumber, out Saturn5 toBeRemoveSaturn5, out string saturn5RemoveLog)
        {
            // Get instance of the saturn to be removed, and build the saturn 5 removal log based on it.
            toBeRemoveSaturn5 = this.Saturn5Repository.Read(serialNumber);

            // Declare null last user.
            User lastUser = null;

            // If user repository recognizes toBeRemoveSaturn5.LastSeenUsername as a valid username...
            if (this.UserRepository.HasUsername(toBeRemoveSaturn5.LastSeenUsername))
                // .. obtain user associated with it.
                lastUser = this.UserRepository.Read(toBeRemoveSaturn5.LastSeenUsername);

            // Obtain saturn remove log.
            saturn5RemoveLog = this.LogsContentConstructor.GetSaturn5RemovedLog(toBeRemoveSaturn5, lastUser);
        }
        #endregion

        #region Adjust Data
        private void AdjustReportFaultData(Saturn5 faultySaturn5, bool hasUnresolvedDamages)
        {
            // If Saturn 5 status indicating that the unit is currently in depot, but not already faulty or damage...
            if (faultySaturn5.Status == Saturn5Status.InDepot && !hasUnresolvedDamages)
                faultySaturn5.Status = Saturn5Status.InDepotFaulty; 
            else if (faultySaturn5.Status == Saturn5Status.InDepot)
                faultySaturn5.Status = Saturn5Status.InDepotDamaged;
        }

        private void AdjustReportDamageData(Saturn5 damageSaturn5)
        {
            // If Saturn 5 status indicating that the unit is currently in depot, but not already damage...
            if (damageSaturn5.Status == Saturn5Status.InDepot || damageSaturn5.Status == Saturn5Status.InDepotFaulty)
                damageSaturn5.Status = Saturn5Status.InDepotDamaged;
        }

        // ResolveUnitIssue - Adjust the value of provided commit data, prior to it being committed, to modify it in way needed for it to be ready for commit.
        private void AdjustResolveUnitIssueData(Saturn5 saturn5, Saturn5Issue toBeResolve, Saturn5IssueStatus resolvedHow, string issueResolvedLog, bool hasOtherUnresolvedDamages, bool hasOtherUnresolvedFaults)
        {
            // If Saturn 5 status indicating that the unit is currently in depot, adjust its status
            // depending on the hasOtherUnresolvedDamages and hasOtherUnresolvedFaults flags values...
            if (saturn5.Status == Saturn5Status.InDepot || saturn5.Status == Saturn5Status.InDepotDamaged || saturn5.Status == Saturn5Status.InDepotFaulty)
                // ... if saturn5 unit associated with resolved issue has other unresolved damages ..
                if (hasOtherUnresolvedDamages)
                    // .. set saturn5 status accordingly.
                    saturn5.Status = Saturn5Status.InDepotDamaged;
                // ... if saturn5 unit associated with resolved issue has other unresolved faults
                // and doesn't have any other unresolved damages..
                else if (hasOtherUnresolvedFaults)
                    // .. set saturn5 status accordingly.
                    saturn5.Status = Saturn5Status.InDepotFaulty;
                // ... if saturn5 unit associated with resolved issue doesn't have any other unresolved faults or damages..
                else
                    // .. set saturn5 status accordingly.
                    saturn5.Status = Saturn5Status.InDepot;
            
            // Adjust saturn 5 issue status and resolved how description.
            toBeResolve.Status = resolvedHow;
            toBeResolve.ResolvedHowDescription = issueResolvedLog;
            toBeResolve.ResolvedByUsername = this._app.LoggedUsername;
        }
        #endregion

        #region Commit Data
        // ReceiveFromIT - Commits all the necessary data associated with persisting retrieval of the saturn 5 unit from IT.
        private void CommitReceiveFromITData(string serialNumber, string shortId, string phoneNumber, string consignmentNumber, string receivedBy, string receivedDate, string receivedTime, string receiveFromITLog, string movementNote)
        {
            // Persists created Saturn5 in the repository
            this.Saturn5Repository.Create(serialNumber, shortId, Saturn5Services.InitialSaturn5Status, phoneNumber, receivedDate, receivedTime, receivedBy);

            // Obtain instance of newly created saturn 5.
            Saturn5 createdSaturn5 = this.Saturn5Repository.Read(serialNumber);

            // Add new log into the saturn dashboard movement repository.
            this.Saturns5MovementRepository.AddReceiveFromITLog(createdSaturn5.SerialNumber, consignmentNumber, receivedBy, receiveFromITLog, movementNote);

            // Log Saturn creation in currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, this.LogsContentConstructor.GetSaturn5CreatedLog(createdSaturn5.SerialNumber, createdSaturn5.ShortId));

            // Create new Saturns5DashboardEntry in the appropriate repository.
            this.Saturns5DashboardRepository.Create(createdSaturn5);
        }

        // Create - Commits all the necessary data associated with persisting creating saturn 5 in a depot stock.
        private void CommitCreateData(string serialNumber, string shortId, string phoneNumber, string receivedBy, string receivedDate, string receivedTime, string receiveFromITLog)
        {
            // Persists created Saturn5 in the repository
            this.Saturn5Repository.Create(serialNumber, shortId, Saturn5Services.InitialSaturn5Status, phoneNumber, receivedDate, receivedTime, receivedBy);

            // Obtain instance of newly created saturn 5.
            Saturn5 createdSaturn5 = this.Saturn5Repository.Read(serialNumber);
            
            // Log Saturn creation in currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, this.LogsContentConstructor.GetSaturn5CreatedLog(createdSaturn5.SerialNumber, createdSaturn5.ShortId));

            // Create new Saturns5DashboardEntry in the appropriate repository.
            this.Saturns5DashboardRepository.Create(createdSaturn5);
        }

        // SendToIT - Commits all the necessary data associated with persisting sending damaged saturn 5 unit to IT.
        private void CommitSendToITFaultedAndDamagedData(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string damagedBy, string faultReportedBy, string damageSendToITLog, string movementNote)
        {
            // Marked all outstanding issues and damages as resolved as IT. 
            this.CommitResolveAllIssuesForSentToITData(serialNumber);

            // Add new log into the saturn dashboard movement repository.
            this.Saturns5MovementRepository.AddSendToITFaultedAndDamagedLog(serialNumber, consignmentNumber, incidentNumber, sentBy, damagedBy, faultReportedBy, damageSendToITLog, movementNote);

            // Log Saturn archivisation into currently logged in user personal log.
            this.Saturn5Repository.AddSaturn5Log(serialNumber, damageSendToITLog);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, damageSendToITLog);

            // Remove saturn5 from the repository
            this.Saturn5Repository.Archive(serialNumber, damageSendToITLog, sentBy);

            // Removes existing Saturns5DashboardEntry from appropriate repository.
            this.Saturns5DashboardRepository.Delete(serialNumber);
        }

        // SendToIT - Commits all the necessary data associated with persisting sending damaged saturn 5 unit to IT.
        private void CommitSendToITOnlyDamagedData(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string damagedBy, string damageSendToITLog, string movementNote)
        {
            // Marked all outstanding issues and damages as resolved as IT. 
            this.CommitResolveAllIssuesForSentToITData(serialNumber);

            // Add new log into the saturn dashboard movement repository.
            this.Saturns5MovementRepository.AddSendToITOnlyDamagedLog(serialNumber, consignmentNumber, incidentNumber, sentBy, damagedBy, damageSendToITLog, movementNote);

            // Log Saturn archivisation into currently logged in user personal log.
            this.Saturn5Repository.AddSaturn5Log(serialNumber, damageSendToITLog);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, damageSendToITLog);

            // Remove saturn5 from the repository
            this.Saturn5Repository.Archive(serialNumber, damageSendToITLog, sentBy);

            // Removes existing Saturns5DashboardEntry from appropriate repository.
            this.Saturns5DashboardRepository.Delete(serialNumber);
        }

        // SendToIT - Commits all the necessary data associated with persisting sending faulty saturn 5 unit to IT.
        private void CommitSendToITOnlyFaultedData(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string faultReportedBy, string faultySendToITLog, string movementNote)
        {
            // Marked all outstanding issues and damages as resolved as IT. 
            this.CommitResolveAllIssuesForSentToITData(serialNumber);

            // Add new log into the saturn dashboard movement repository.
            this.Saturns5MovementRepository.AddSendToITOnlyFaultedLog(serialNumber, consignmentNumber, incidentNumber, sentBy, faultReportedBy, faultySendToITLog, movementNote);

            // Log Saturn archivisation into currently logged in user personal log.
            this.Saturn5Repository.AddSaturn5Log(serialNumber, faultySendToITLog);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, faultySendToITLog);

            // Remove saturn5 from the repository
            this.Saturn5Repository.Archive(serialNumber, faultySendToITLog, sentBy);

            // Removes existing Saturns5DashboardEntry from appropriate repository.
            this.Saturns5DashboardRepository.Delete(serialNumber);
        }

        // SendToIT - Commits all the necessary data associated with persisting sending surplus saturn 5 unit to IT.
        private void CommitSendToITFullyFunctionalSurplusData(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string surplusSendToITLog, string movementNote)
        {
            // Marked all outstanding issues and damages as resolved as IT. 
            this.CommitResolveAllIssuesForSentToITData(serialNumber);

            // Add new log into the saturn dashboard movement repository.
            this.Saturns5MovementRepository.AddSendToITFullyFunctionalSurplusLog(serialNumber, consignmentNumber, incidentNumber, sentBy, surplusSendToITLog, movementNote);

            // Log Saturn archivisation into currently logged in user personal log.
            this.Saturn5Repository.AddSaturn5Log(serialNumber, surplusSendToITLog);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, surplusSendToITLog);

            // Remove saturn5 from the repository
            this.Saturn5Repository.Archive(serialNumber, surplusSendToITLog, sentBy);

            // Removes existing Saturns5DashboardEntry from appropriate repository.
            this.Saturns5DashboardRepository.Delete(serialNumber);
        }

        // Common - Commits data necessary to persist all the unresolved issues as resolved by IT.
        private void CommitResolveAllIssuesForSentToITData(string serialNumber)
        {
            foreach (Saturn5Issue saturn5Issue in this.Saturn5IssuesRepository.GetUnresolvedIssues(serialNumber))
            {
                saturn5Issue.Status = Saturn5IssueStatus.ResolvedByIT;
                saturn5Issue.ResolvedHowDescription = "Sent to IT.";
                this.Saturn5IssuesRepository.Update(saturn5Issue);
            }
        }

        // ReportFault - Commits necessary data to persist reported saturn 5 fault
        private void CommitReportFaultData(Saturn5 faultySaturn5, string byUsername, string faultDescription, string reportFaultLog)
        {
            // Add new saturn 5 fault.
            this.Saturn5IssuesRepository.AddNewFault(faultySaturn5.SerialNumber, byUsername, faultDescription.Trim());
            
            // Update saturn5 Status
            this.Saturn5Repository.Update(faultySaturn5.SerialNumber, null, faultySaturn5.Status, null, null, null, null);

            // Add saturn 5 log
            this.Saturn5Repository.AddSaturn5Log(faultySaturn5.SerialNumber, reportFaultLog);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, reportFaultLog);

            // Update saturn 5 entry in the dashboard.
            this.Saturns5DashboardRepository.Update(faultySaturn5);
        }

        // ReportDamage - Commits necessary data to persist reported saturn 5 damage
        private void CommitReportDamageData(Saturn5 damageSaturn5, string byUsername, string damageDescription, string reportDamageLog)
        {
            // Add new saturn 5 damage
            this.Saturn5IssuesRepository.AddNewDamage(damageSaturn5.SerialNumber, byUsername, damageDescription.Trim());

            // Update saturn5 Status
            this.Saturn5Repository.Update(damageSaturn5.SerialNumber, null, damageSaturn5.Status, null, null, null, null);

            // Add saturn 5 log
            this.Saturn5Repository.AddSaturn5Log(damageSaturn5.SerialNumber, reportDamageLog);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, reportDamageLog);

            // Update saturn 5 entry in the dashboard.
            this.Saturns5DashboardRepository.Update(damageSaturn5);
        }

        // ResolveIssue - Commits data necessary to persist resolving saturn 5 unit.
        private void CommitResolveIssueData(Saturn5 saturn5, Saturn5Issue toBeResolve, string issueResolvedLog)
        {
            // Update Saturn 5 Issue
            this.Saturn5IssuesRepository.Update(toBeResolve);

            // Update saturn5 Status
            this.Saturn5Repository.Update(saturn5.SerialNumber, null, saturn5.Status, null, null, null, null);

            // Add saturn 5 log
            this.Saturn5Repository.AddSaturn5Log(saturn5.SerialNumber, issueResolvedLog);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, issueResolvedLog);

            // Update saturn 5 entry in the dashboard.
            this.Saturns5DashboardRepository.Update(saturn5);
        }

        // Edit - Commits data necessary to persist edit of saturn 5 short id and/or phone number.
        private void CommitEditData(string serialNumber, string newShortId, string newPhoneNumber, string editLog)
        {
            // Update Saturn5 unit phone number
            this.Saturn5Repository.Update(serialNumber, newShortId, null, newPhoneNumber, null, null, null);

            // Log Saturn edit into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, editLog);

            // Log Saturn edit into currently logged in user personal log.
            this.Saturn5Repository.AddSaturn5Log(serialNumber, editLog);

            // Get edited instance of the saturn5 from the saturns repository.
            Saturn5 editedSaturn5 = this.Saturn5Repository.Read(serialNumber);

            // Commit changes into the Saturns5Dashboard
            this.Saturns5DashboardRepository.Update(editedSaturn5);
        }

        // Remove - Commits data necessary to persist removal of saturn 5 unit.
        private void CommitRemoveData(string serialNumber, string saturn5RemoveLog)
        {
            // Remove saturn5 from the repository
            this.Saturn5Repository.Delete(serialNumber);

            // Removes existing Saturns5DashboardEntry from appropriate repository.
            this.Saturns5DashboardRepository.Delete(serialNumber);

            // Log Saturn removal into currently logged in user personal log.
            this.UserRepository.AddUserLog(this._app.LoggedUsername, saturn5RemoveLog);
        }
        #endregion
        #endregion
    }
}
