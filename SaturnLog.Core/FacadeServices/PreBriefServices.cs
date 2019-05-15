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
    public class PreBriefServices
    {
        #region Private Fields
        private App _app;
        #endregion

        #region Private Properties
        private IDataRepository DataRepository { get { return this._app.DataRepository; } }
        private IUserRepository UserRepository { get { return this._app.UserRepository; } }
        private ISaturn5Repository Saturn5Repository { get { return this._app.Saturn5Repository; } }
        private ISaturn5IssuesRepository Saturn5IssuesRepository { get { return this._app.Saturn5IssuesRepository; } }
        private ISaturns5DashboardRepository Saturns5DashboardRepository { get { return this._app.Saturns5DashboardRepository; } }

        private LogsContentConstructor LogsContentConstructor { get { return this._app.LogsContentConstructor; } }
        #endregion

        #region Constructor
        public PreBriefServices(App app)
        {
            this._app = app;
        }
        #endregion


        #region Methods
        public async Task AllocateSaturn5BySerialNumberAsync(string serialNumber, string username)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameters
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
                else if (username is null) throw new ArgumentNullException(nameof(username));

                // Assure that all the characters used in provided serial number and username strings are upper case, and 'empty space' doesn't perpend, or append the strings.
                serialNumber = serialNumber.Trim().ToUpper();
                username = username.Trim().ToUpper();

                if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    throw new ArgumentException($"Unable to find Saturn5 associated with provided serial number: {serialNumber}", nameof(serialNumber));
                else if (!this.UserRepository.HasUsername(username))
                    throw new ArgumentException($"Unable to find User associated with provided username {username}", nameof(username));

                // Gather necessary data associated with allocating to the user into the database.
                this.GatherAllocateSaturn5BySerialNumberData(serialNumber, username, out Saturn5 saturn5, out User user, out string allocateLog);

                // Parameters validation

                // If Saturn 5 unit is damage, throw exception.
                if (this.Saturn5IssuesRepository.HasUnresolvedDamages(saturn5.SerialNumber))
                    throw new AttemptToAllocateDamagedSaturn5Exception(user, saturn5);

                // Else if Saturn 5 unit is faulty, throw exception.
                else if (this.Saturn5IssuesRepository.HasUnresolvedFaults(saturn5.SerialNumber))
                    throw new AttemptToAllocateFaultySaturn5Exception(user, saturn5);

                // TODO Implement stopping user from having Saturn5 unit allocate to, if stopping this specific user has been requested

                // Adjust the collected data accordingly.
                this.AdjustAllocateSaturn5Data(saturn5, user);

                // Commit changes related with allocating to the user into the database.
                await Task.Run(() => { this.CommitAllocateSaturn5Data(saturn5, allocateLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }

        public async Task AllocateSaturn5ByShortIdAsync(string shortId, string username)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameters validation
                if (shortId is null) throw new ArgumentNullException(nameof(shortId));
                if (username is null) throw new ArgumentNullException(nameof(username));

                // Assure that all the characters used in provided shortId string are upper case, and 'empty space' doesn't perpend, or append the shortId string.
                shortId = shortId.Trim().ToUpper();
                username = username.Trim().ToUpper();

                if (!this.Saturn5Repository.HasSaturn5ShortId(shortId))
                    throw new ArgumentException($"Unable to find Saturn5 associated with provided barcode shortId: {shortId}", nameof(shortId));
                else if (!this.UserRepository.HasUsername(username))
                    throw new ArgumentException($"Unable to find User associated with provided username {username}", nameof(username));

                // Gather necessary data associated with allocating to the user into the database.
                this.GatherAllocateSaturn5ByShortIdData(shortId, username, out Saturn5 saturn5, out User user, out string allocateLog);

                // Parameters validation

                // If Saturn 5 unit is damage, throw exception.
                if (this.Saturn5IssuesRepository.HasUnresolvedDamages(saturn5.SerialNumber))
                    throw new AttemptToAllocateDamagedSaturn5Exception(user, saturn5);

                // Else if Saturn 5 unit is faulty, throw exception.
                else if (this.Saturn5IssuesRepository.HasUnresolvedFaults(saturn5.SerialNumber))
                    throw new AttemptToAllocateFaultySaturn5Exception(user, saturn5);

                // TODO Implement stopping user from having Saturn5 unit allocate to, if stopping this specific user has been requested

                // Adjust the collected data accordingly.
                this.AdjustAllocateSaturn5Data(saturn5, user);

                // Commit changes related with allocating to the user into the database.
                await Task.Run(() => { this.CommitAllocateSaturn5Data(saturn5, allocateLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }

        public async Task EmergencyAllocateSaturn5BySerialNumberAsync(string serialNumber, string username)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameters
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
                else if (username is null) throw new ArgumentNullException(nameof(username));

                // Assure that all the characters used in provided serial number and username strings are upper case, and 'empty space' doesn't perpend, or append the strings.
                serialNumber = serialNumber.Trim().ToUpper();
                username = username.Trim().ToUpper();

                if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    throw new ArgumentException($"Unable to find Saturn5 associated with provided serial number: {serialNumber}", nameof(serialNumber));
                else if (!this.UserRepository.HasUsername(username))
                    throw new ArgumentException($"Unable to find User associated with provided username {username}", nameof(username));

                // Gather necessary data associated with allocating to the user into the database.
                this.GatherAllocateSaturn5BySerialNumberData(serialNumber, username, out Saturn5 saturn5, out User user, out string allocateLog);

                // TODO Implement stopping user from having Saturn5 unit allocate to, if stopping this specific user has been requested

                // Adjust the collected data accordingly.
                this.AdjustAllocateSaturn5Data(saturn5, user);

                // Commit changes related with allocating to the user into the database.
                await Task.Run(() => { this.CommitAllocateSaturn5Data(saturn5, allocateLog); });

            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }
        #endregion

        #region Private Helpers
        #region Gather Data
        private void GatherAllocateSaturn5BySerialNumberData(string serialNumber, string username, out Saturn5 saturn5, out User user, out string allocateLog)
        {
            // Retrieve user from username argument
            user = this.UserRepository.Read(username);

            // Retrieve saturn5 from serial number argument
            saturn5 = this.Saturn5Repository.Read(serialNumber);

            // Assign saturn 5 unit to user allocation log.
            allocateLog = this.LogsContentConstructor.GetAllocateSaturn5ToUserLog(user, saturn5);
        }

        private void GatherAllocateSaturn5ByShortIdData(string shortId, string username, out Saturn5 saturn5, out User user, out string allocateLog)
        {
            // Get Saturn 5 serial number based on the provided short id ..
            string serialNumber = this.Saturn5Repository.GetSerialNumberFromShortId(shortId);
            
            // .. and use it to call twin method version of itself using serial number rather then short it.
            this.GatherAllocateSaturn5BySerialNumberData(serialNumber, username, out saturn5, out user, out allocateLog);
        }
        #endregion

        #region Adjust Data
        // Adjust all the data in necessery way to  prior to committing it.
        private void AdjustAllocateSaturn5Data(Saturn5 saturn5, User user)
        {
            // Get saturn 5 allocation timestamp parts...
            DateTimeExtensions.GetTimestampParams(out DateTime now, out string lastSeenDate, out string lastSeenTime);

            // .. and set provided saturn 5 last seen date/time accordingly. 
            saturn5.LastSeenDate = lastSeenDate;
            saturn5.LastSeenTime = lastSeenTime;

            // Set last user username of the saturn 5 unit using the username of the
            saturn5.LastSeenUsername = user.Username;

            // Set saturn5 status according the type of the user the saturn5 is getting allocated to.
            // saturn5.Status = Saturn5Status.WithStarUser; or Saturn5Status.WithManager; default Saturn5Status.WithUser;
            switch (user.Type)
            {
                // Star User
                case UserType.StarUser:
                    saturn5.Status = Saturn5Status.WithStarUser;
                    break;
                // Manager
                case UserType.Manager:
                    saturn5.Status = Saturn5Status.WithManager;
                    break;
                // Otherwise (User, Pre-Brief, De-Brief, Admin)
                default:
                    saturn5.Status = Saturn5Status.WithUser;
                    break;
            }
        }
        #endregion

        #region Commit Data
        // Commits all the necessary data associated with allocating saturn 5 to the user.
        private void CommitAllocateSaturn5Data(Saturn5 saturn5, string allocateLog)
        {
            // Commit changes into the Saturns5DB
            this.Saturn5Repository.Update(saturn5.SerialNumber, null, saturn5.Status, null, saturn5.LastSeenDate, saturn5.LastSeenTime, saturn5.LastSeenUsername);

            // Commits changes into the dashboard repository
            this.Saturns5DashboardRepository.Update(saturn5);

            // Log into the saturn 5 individual log.
            this.Saturn5Repository.AddSaturn5Log(saturn5.SerialNumber, allocateLog);

            // Log into the user individual log of the user to whom the saturn 5 unit is getting allocated to.
            this.UserRepository.AddUserLog(saturn5.LastSeenUsername, allocateLog);

            // Assure that logged on user doesn't allocating unit to him self to avoid double user log.
            if (saturn5.LastSeenUsername != this._app.LoggedUsername)
                // Log into the currently logged in user individual log 
                this.UserRepository.AddUserLog(this._app.LoggedUsername, allocateLog);
        }
        #endregion
        #endregion
    }
}
