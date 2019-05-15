using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public class LogsContentConstructor
    {
        #region Private Fields

        private IUserRepository UserRepository { get { return this._app.UserRepository; } }
        private ISaturn5Repository Saturn5Repository { get { return this._app.Saturn5Repository; } }
        private ISaturn5IssuesRepository Saturn5IssuesRepository { get { return this._app.Saturn5IssuesRepository; } }
        private ISaturns5DashboardRepository Saturns5DashboardRepository { get { return this._app.Saturns5DashboardRepository; } }
        #endregion


        #region Private Fields
        private App _app;
        #endregion

        #region Private Properties
        private User LoggedUser { get { return this._app.LoggedUser; } }
        private string LoggedBy { get { return $"[Logged By: {this._app.LoggedUser.Username}-{this._app.LoggedUser.FirstName} {this._app.LoggedUser.Surname}]"; } }
        #endregion

        #region Constructor
        public LogsContentConstructor(App app)
        {
            this._app = app;
        }
        #endregion

        public string GetSaturn5CreatedLog(string serialNumber, string shortId)
        {
            return $"Saturn 5 serial number: {serialNumber}, short id: {shortId} has been added into the database. {this.LoggedBy}";
        }

        public string GetSaturn5RemovedLog(Saturn5 toBeRemovedSaturn5, User lastUser)
        {
            if (!(lastUser is null))
                return $"Saturn 5 serial number: {toBeRemovedSaturn5.SerialNumber}, short id: {toBeRemovedSaturn5.ShortId}, last seen user: {lastUser.Username} - {lastUser.FirstName} {lastUser.Surname} last seen date: {toBeRemovedSaturn5.LastSeenDate}, last seen time: {toBeRemovedSaturn5.LastSeenTime} has been removed from the database. {this.LoggedBy}";
            else
                return $"Saturn 5 serial number: {toBeRemovedSaturn5.SerialNumber}, short id: {toBeRemovedSaturn5.ShortId}, last seen username: {toBeRemovedSaturn5.LastSeenUsername}, last seen date: {toBeRemovedSaturn5.LastSeenDate}, last seen time: {toBeRemovedSaturn5.LastSeenTime} has been removed from the database. {this.LoggedBy}";
        }

        public string GetSaturn5EditShortIdAndPhoneNumberLog(Saturn5 editedSaturn5, string newShortId, string newPhoneNumber)
        {
            return $"Saturn 5 short id has been change from {editedSaturn5.ShortId} to {newShortId} and its phone number changed from {editedSaturn5.PhoneNumber} {newPhoneNumber}. {this.LoggedBy}";
        }

        public string GetSaturn5EditShortIdLog(Saturn5 editedSaturn5, string newShortId)
        {
            return $"Saturn 5 short id has been change from {editedSaturn5.ShortId} to {newShortId}. {this.LoggedBy}";
        }

        public string GetSaturn5EditPhoneNumberLog(Saturn5 editedSaturn5, string newPhoneNumber)
        {
            return $"Saturn 5 phone number has been change from {editedSaturn5.PhoneNumber ?? "NO NUMBER"} to {newPhoneNumber}. {this.LoggedBy}";
        }

        public string GetUserCreatedLog(string username, string firstName, string surname, UserType type)
        {
            return $"User username: {username} - {firstName} {surname} -{type.GetDisplayableString()} has been added into the database. {this.LoggedBy}";
        }

        public string GetUserRemovedLog(User toBeRemovedUser)
        {
            return $"User username: {toBeRemovedUser.Username} - {toBeRemovedUser.FirstName} {toBeRemovedUser.Surname} has been removed from the database. {this.LoggedBy}";
        }

        public string GetAllocateSaturn5ToUserLog(User recipientUser, Saturn5 saturn5)
        {
            return $"Saturn 5 serial number: {saturn5.SerialNumber} has been given to: {recipientUser.Username} - {recipientUser.FirstName} {recipientUser.Surname}. {this.LoggedBy}";
        }

        public string GetConfirmInDepotSaturn5Log(User returningUser, Saturn5 saturn5)
        {
            return $"Saturn 5 serial number: {saturn5.SerialNumber} has been confirmed in depot, returned by: {returningUser?.Username} - {returningUser?.FirstName} {returningUser?.Surname}. {this.LoggedBy}";
        }

        public string GetConfirmInDepotDamageSaturn5Log(User returningUser, Saturn5 saturn5, string damageReport)
        {
            return $"Saturn 5 serial number: {saturn5.SerialNumber} has been confirmed in depot damaged({damageReport}), returned by: {returningUser?.Username} - {returningUser?.FirstName} {returningUser?.Surname}. {this.LoggedBy}";
        }

        public string GetConfirmInDepotFaultySaturn5Log(User returningUser, Saturn5 saturn5, string issueReport)
        {
            return $"Saturn 5 serial number: {saturn5.SerialNumber} has been confirmed in depot, returned by: {returningUser?.Username} - {returningUser?.FirstName} {returningUser?.Surname}. Following issue has been reported: {issueReport} {this.LoggedBy}";
        }

        public string GetUserEditFirstNameSurnameAndUserTypeLog(User editedUser, string firstName, string surname, UserType type)
        {
            return $"User username: {editedUser.Username} first name has been change from {editedUser.FirstName} to {firstName}, surname has been change from {editedUser.Surname} to {surname} and type has been change from {editedUser.Type} to {type}. {this.LoggedBy}";
        }

        public string GetUserEditFirstNameAndSurnameLog(User editedUser, string firstName, string surname)
        {
            return $"User username: {editedUser.Username} first name has been change from {editedUser.FirstName} to {firstName} and surname has been change from {editedUser.Surname} to {surname}. {this.LoggedBy}";
        }

        public string GetUserEditFirstNameAndUserTypeLog(User editedUser, string firstName, UserType type)
        {
            return $"User username: {editedUser.Username} first name has been change from {editedUser.FirstName} to {firstName} and type has been change from {editedUser.Type} to {type}. {this.LoggedBy}";
        }

        public string GetUserEditSurnameAndUserTypeLog(User editedUser, string surname, UserType type)
        {
            return $"User username: {editedUser.Username} surname has been change from {editedUser.Surname} to {surname} and type has been change from {editedUser.Type} to {type}. {this.LoggedBy}";
        }

        public string GetUserEditFirstNameLog(User editedUser, string firstName)
        {
            return $"User username: {editedUser.Username} first name has been change from {editedUser.FirstName} to {firstName}. {this.LoggedBy}";
        }

        public string GetUserEditSurnameLog(User editedUser, string surname)
        {
            return $"User username: {editedUser.Username} surname has been change from {editedUser.Surname} to {surname}. {this.LoggedBy}";
        }

        public string GetUserEditUserTypeLog(User editedUser, UserType type)
        {
            return $"User username: {editedUser.Username} type has been change from {editedUser.Type} to {type}. {this.LoggedBy}";
        }

        public string GetSaturn5ReceivedFromITLog(string serialNumber)
        {
            return $"Saturn 5 serial number:{serialNumber} has been received from IT. {this.LoggedBy}";
        }

        public string GetCreateSaturn5Log(string serialNumber)
        {
            return $"Saturn 5 serial number:{serialNumber} has been added into the depot stock. {this.LoggedBy}";
        }

        public string GetDamagedSaturn5SentToITLog(Saturn5 toBeSentToITSaturn5, IList<Saturn5Issue> saturn5damaged, IList<Saturn5Issue> saturn5Faults, bool surplus)
        {
            string log = "";

            if (!surplus)
                log = $"Saturn 5 serial number {toBeSentToITSaturn5.SerialNumber} unit has been sent to IT for REPLACEMENT with following damages: ";
            if (surplus)
                log = $"Saturn 5 serial number {toBeSentToITSaturn5.SerialNumber} unit has been sent to IT as SURPLUS with following damages: ";
            
            for (int i = 0; i < saturn5damaged.Count - 1; i++)
                log += $"Damaged by: {saturn5damaged[i].ReportedByUsername} Description: {saturn5damaged[i].Description}, ";

            if (saturn5Faults.Count == 0)
                log += $"Damaged by: {saturn5damaged.Last().ReportedByUsername} Description: {saturn5damaged.Last().Description}. ";
            else
            {
                log += $"Damaged by: {saturn5damaged.Last().ReportedByUsername} Description: {saturn5damaged.Last().Description}. Faults: ";
                
                for (int i = 0; i < saturn5Faults.Count - 1; i++)
                    log += $"Reported by: {saturn5Faults[i].ReportedByUsername} Description: {saturn5Faults[i].Description}, ";

                log += $"Reported by: {saturn5Faults.Last().ReportedByUsername} Description: {saturn5Faults.Last().Description}. ";
            }

            return $"{log} {this.LoggedBy}";
        }

        public string GetFaultedSaturn5SentToITLog(Saturn5 toBeSentToITSaturn5, IList<Saturn5Issue> saturn5Faults, bool surplus)
        {
            string log = "";

            if (!surplus)
                log = $"Saturn 5 serial number {toBeSentToITSaturn5.SerialNumber} unit has been sent to IT for REPLACEMENT with following issues: ";
            if (surplus)
                log = $"Saturn 5 serial number {toBeSentToITSaturn5.SerialNumber} unit has been sent to IT as SURPLUS with following issues: ";

            for (int i = 0; i < saturn5Faults.Count - 1; i++)
                log += $"Reported by: {saturn5Faults[i].ReportedByUsername} Description: {saturn5Faults[i].Description}, ";

            log += $"Reported by: {saturn5Faults.Last().ReportedByUsername} Description: {saturn5Faults.Last().Description}. ";

            return $"{log} {this.LoggedBy}";
        }

        public string GetFullyFunctionalSurplusSaturn5SentToITLog(Saturn5 toBeSentToITSaturn5)
        {
            return $"Saturn 5 serial number {toBeSentToITSaturn5.SerialNumber} Fully Functional Surplus unit has been sent to IT. {this.LoggedBy}";
        }
        
        public string GetReportSaturn5FaultLog(User reportingUser, Saturn5 faultySaturn5, string issueReport)
        {
            return $"Fault Saturn 5 serial number: {faultySaturn5.SerialNumber} has been reported by: {reportingUser?.Username} - {reportingUser?.FirstName} {reportingUser?.Surname}. See description: {issueReport} {this.LoggedBy}";
        }

        public string GetReportSaturn5DamageLog(User responsiableUser, Saturn5 damageSaturn5, string issueReport)
        {
            return $"Damage to Saturn 5 serial number: {damageSaturn5.SerialNumber} has been reported, responsible user: {responsiableUser?.Username} - {responsiableUser?.FirstName} {responsiableUser?.Surname}. See description: {issueReport} {this.LoggedBy}";
        }

        public string GetSaturn5IssueResolvedInDepotLog(Saturn5Issue saturn5Issue)
        {
            return $"Following issue: '{saturn5Issue.Description}' of the Saturn 5 unit serial number: {saturn5Issue.SerialNumber} has been resolved in the following way: '{saturn5Issue.ResolvedHowDescription}' {this.LoggedBy}";
        }

        public string GetSaturn5IssueMarkedAsKnownIssueLog(Saturn5Issue saturn5Issue)
        {
            return $"Following issue: '{saturn5Issue.Description}' of the Saturn 5 unit serial number: {saturn5Issue.SerialNumber} has been marked as 'Known Issue' {this.LoggedBy}";
        }

        public string GetSaturn5IssueCannotBeReplicatedLog(Saturn5Issue saturn5Issue)
        {
            return $"Following issue: '{saturn5Issue.Description}' of the Saturn 5 unit serial number: {saturn5Issue.SerialNumber} cannot be replicated and as such unit has been marked as fully usable {this.LoggedBy}";
        }

        public string GetSaturn5IssueMarkedAsNotAnIssueLog(Saturn5Issue saturn5Issue)
        {
            return $"Following issue: '{saturn5Issue.Description}'of the Saturn 5 unit serial number: {saturn5Issue.SerialNumber} has been marked as 'Not An Issue' {this.LoggedBy}";
        }
    }
}
