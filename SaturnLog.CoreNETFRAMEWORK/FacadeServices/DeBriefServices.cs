using SaturnLog.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public class DeBriefServices
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

        #region Constructors
        public DeBriefServices(App app)
        {
            this._app = app;
        }
        #endregion

        #region Methods
        public async Task ConfirmInDepotSaturn5BySerialNumberAsync(string serialNumber, bool throwOnLastSeenWithStarUser)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameter validation
                if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

                // Assure that all the characters used in provided serial number string are upper case, and 'empty space' doesn't perpend, or append the string.
                serialNumber = serialNumber.Trim().ToUpper();

                if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber))
                    throw new ArgumentException($"Unable to find Saturn5 associated with provided serial number: {serialNumber}", nameof(serialNumber));

                // Gather ..
                this.GatherConfirmedInDepotSaturn5BySerialNumberData(serialNumber, out Saturn5 saturn5, out User lastSeenUser, out string confirmInDepotLog);

                // If 'throwOnLastSeenWithStarUser' indicate that confirming saturns 5 allocated to StarUsers is not allowed..
                if (throwOnLastSeenWithStarUser && saturn5.Status.IsWithUser() && lastSeenUser.Type == UserType.StarUser)
                    // .. throw appropriate exception.
                    throw new AttemptToConfirmStarUserSaturn5Exception(lastSeenUser, saturn5);

                // Adjust ..
                this.AdjustConfirmBackInSaturn5Data(saturn5);

                // Commit ..
                await Task.Run(() => { this.CommitConfirmedInDepotSaturn5Data(saturn5, confirmInDepotLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }

        public async Task ConfirmInDepotSaturn5ByShortIdAsync(string shortId, bool throwOnLastSeenWithStarUser)
        {
            // Await database commit semaphore to indicate current method turn
            await this.DataRepository.LockDBContentAsync();

            try
            {
                // Parameter validation
                if (shortId is null) throw new ArgumentNullException(nameof(shortId));

                // Assure that all the characters used in provided short id string are upper case, and 'empty space' doesn't perpend, or append the string.
                shortId = shortId.Trim().ToUpper();

                if (!this.Saturn5Repository.HasSaturn5ShortId(shortId))
                    throw new ArgumentException($"Unable to find Saturn5 associated with provided shortId: {shortId}", nameof(shortId));
                
                // Gather ..
                this.GatherConfirmedInDepotSaturn5ByShortIdData(shortId, out Saturn5 saturn5, out User lastSeenUser, out string confirmInDepotLog);

                // If 'throwOnLastSeenWithStarUser' indicate that confirming saturns 5 allocated to StarUsers is not allowed..
                if (throwOnLastSeenWithStarUser && saturn5.Status.IsWithUser() && lastSeenUser.Type == UserType.StarUser)
                    // .. throw appropriate exception.
                    throw new AttemptToConfirmStarUserSaturn5Exception(lastSeenUser, saturn5);

                // Adjust ..
                this.AdjustConfirmBackInSaturn5Data(saturn5);

                // Commit ..
                await Task.Run(() => { this.CommitConfirmedInDepotSaturn5Data(saturn5, confirmInDepotLog); });
            }
            // Finally release the database semaphore
            finally { this.DataRepository.ReleaseDBContent(); }
        }
        #endregion

        #region Private Helpers
        #region Gather Data
        private void GatherConfirmedInDepotSaturn5BySerialNumberData(string serialNumber, out Saturn5 saturn5, out User lastSeenUser, out string confirmInDepotLog)
        {
            // Retrieve saturn5 from serial number parameter
            saturn5 = this.Saturn5Repository.Read(serialNumber);

            // If saturn 5 last seen username is not recognized throw appropriate exception.
            if (!this.UserRepository.HasUsername(saturn5.LastSeenUsername))
                throw new Saturn5LastSeenUsernameIsInvalidException(saturn5);
            
            // Retrieve user from saturn 5 last seen username parameter if its represent a valid user,
            // otherwise retrieve a logged user.
            lastSeenUser = this.UserRepository.Read(saturn5.LastSeenUsername);

            // Get unresolved saturn 5 damages and faults.
            IList<Saturn5Issue> saturn5UnresolvedDamages = this.Saturn5IssuesRepository.GetUnresolvedDamages(serialNumber);
            IList<Saturn5Issue> saturn5UnresolvedFaults = this.Saturn5IssuesRepository.GetUnresolvedFaults(serialNumber);

            // Get last unresolved saturn 5 damage/fault.
            Saturn5Issue saturn5LastUnresolvedDamage = saturn5UnresolvedDamages.LastOrDefault();
            Saturn5Issue saturn5LastUnresolvedFault = saturn5UnresolvedFaults.LastOrDefault();
            
            // Get appropriate confirmed in depot log.
            if (this.Saturn5IssuesRepository.HasUnresolvedDamages(serialNumber))
                confirmInDepotLog = this.LogsContentConstructor.GetConfirmInDepotDamageSaturn5Log(lastSeenUser, saturn5, saturn5LastUnresolvedDamage.Description);
            else if (this.Saturn5IssuesRepository.HasUnresolvedFaults(serialNumber))
                confirmInDepotLog = this.LogsContentConstructor.GetConfirmInDepotFaultySaturn5Log(lastSeenUser, saturn5, saturn5LastUnresolvedFault.Description);
            else
                confirmInDepotLog = this.LogsContentConstructor.GetConfirmInDepotSaturn5Log(lastSeenUser, saturn5);
        }

        private void GatherConfirmedInDepotSaturn5ByShortIdData(string shortId, out Saturn5 saturn5, out User lastSeenUser, out string confirmInDepotLog)
        {
            // Get Saturn 5 serial number based on the provided short id ..
            string serialNumber = this.Saturn5Repository.GetSerialNumberFromShortId(shortId);
            
            // .. and use it to call twin method version of itself using serial number rather then short it.
            this.GatherConfirmedInDepotSaturn5BySerialNumberData(serialNumber, out saturn5, out lastSeenUser, out confirmInDepotLog);
        }
        #endregion

        #region Adjust Data
        private void AdjustConfirmBackInSaturn5Data(Saturn5 saturn5)
        {
            // Get saturn 5 confirmation timestamp parts...
            DateTimeExtensions.GetTimestampParams(out DateTime now, out string lastSeenDate, out string lastSeenTime);

            // .. and set provided saturn 5 last seen date/time accordingly. 
            saturn5.LastSeenDate = lastSeenDate;
            saturn5.LastSeenTime = lastSeenTime;

            // Set appropriate status depending on whether the unit has unresolved damages or faults
            if (this.Saturn5IssuesRepository.HasUnresolvedDamages(saturn5.SerialNumber))
                saturn5.Status = Saturn5Status.InDepotDamaged;
            else if (this.Saturn5IssuesRepository.HasUnresolvedFaults(saturn5.SerialNumber))
                saturn5.Status = Saturn5Status.InDepotFaulty;
            else
                saturn5.Status = Saturn5Status.InDepot;
        }
        #endregion

        #region Commit Data
        // Commits all the necessary data associated with confirmation of not damaged, nor faulty saturn 5 unit back in depot.
        private void CommitConfirmedInDepotSaturn5Data(Saturn5 saturn5, string confirmInDepotLog)
        {
            // Commit changes into the Saturns5DB
            this.Saturn5Repository.Update(saturn5.SerialNumber, null, saturn5.Status, null, saturn5.LastSeenDate, saturn5.LastSeenTime, saturn5.LastSeenUsername);

            // Commits changes into the dashboard repository
            this.Saturns5DashboardRepository.Update(saturn5);

            // Log into the saturn 5 individual log.
            this.Saturn5Repository.AddSaturn5Log(saturn5.SerialNumber, confirmInDepotLog);

            // Log into the user individual log of the user to whom the saturn 5 unit is getting allocated to.
            this.UserRepository.AddUserLog(saturn5.LastSeenUsername, confirmInDepotLog);

            // Assure that logged on user doesn't confirming a unit which has him as last user to avoid double user log.
            if (saturn5.LastSeenUsername != this._app.LoggedUsername)
                // Log into the currently logged in user individual log
                this.UserRepository.AddUserLog(this._app.LoggedUsername, confirmInDepotLog);
        }
        #endregion
        #endregion
    }
}
