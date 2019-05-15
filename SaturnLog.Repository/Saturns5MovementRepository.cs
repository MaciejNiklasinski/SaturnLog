using LiveGoogle.Sheets;
using SaturnLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Repository
{
    public class Saturns5MovementRepository : ISaturns5MovementRepository, IDisposable
    {
        #region Const
        internal readonly string Saturns5Movement_SpreadsheetId;
        
        public const string Saturns5Movement_SheetId = "Saturns5 Movement";
        public const int Saturns5Movement_HeaderRowIndex = 0;
        public const int Saturns5Movement_InitialRowsCount = 1;
        public const int Saturns5Movement_EntryRowsCount = 1;
        public const int Saturns5Movement_ColumnsCount = 13;

        public const int Saturns5Movement_Timestamp_ColumnsIndex = 0;
        public const int Saturns5Movement_SerialNumber_ColumnsIndex = 1;
        public const int Saturns5Movement_ConsignmentNumber_ColumnsIndex = 2;
        public const int Saturns5Movement_IncidentNumber_ColumnsIndex = 3;
        public const int Saturns5Movement_Saturn5SpreadsheetURL_ColumnsIndex = 4;
        public const int Saturns5Movement_ReceivedOrSent_ColumnsIndex = 5;
        public const int Saturns5Movement_ReceivedOrSentByUsername_ColumnsIndex = 6;
        public const int Saturns5Movement_PhysicalDamage_ColumnsIndex = 7;
        public const int Saturns5Movement_DamagedByUser_ColumnsIndex = 8;
        public const int Saturns5Movement_NonPhysicalIssue_ColumnsIndex = 9;
        public const int Saturns5Movement_NonPhysicalIssueReportedByUsername_ColumnsIndex = 10;
        public const int Saturns5Movement_DamageOrIssueDescription_ColumnsIndex = 11;
        public const int Saturns5Movement_ReceivedOrSentByNote_ColumnsIndex = 12;

        public readonly static string[] Saturn5Movement_HeaderRowContent = new string[Saturns5MovementRepository.Saturns5Movement_ColumnsCount]
        { "Timestamp:", "Serial Number:", "Consignment Number:", "Incident Number:","Saturn Spreadsheet URL:", "Received / Sent:", "Received by/ Sent by:", "Physical damage:", "Damaged by username:", "Non-physical issue:", "Non-physical issue reported by:", "Damage/Issue description:", "Received by note/ Sent by note:" };
        #endregion

        #region Private Fields/Properties
        private DataRepository _dataRepository;

        private LiveSpreadsheet _dashboardSpreadsheet;

        private LiveSheet _movementSheet;

        private IUserRepository UserRepository { get { return this._dataRepository.UserRepository; } }
        private ISaturn5Repository Saturn5Repository { get { return this._dataRepository.Saturn5Repository; } }
        private ISaturns5DashboardRepository Saturns5DashboardRepository { get { return this._dataRepository.Saturns5DashboardRepository; } }
        private ISaturn5IssuesRepository Saturn5IssuesRepository { get { return this._dataRepository.Saturn5IssuesRepository; } }
        #endregion

        #region Properties

        #endregion

        #region Constructors
        // Must be constructed after Saturn5Dashboard repository has been constructed withing dataRepository
        private Saturns5MovementRepository(string spreadsheetId) { this.Saturns5Movement_SpreadsheetId = spreadsheetId; }
        internal static async Task<Saturns5MovementRepository> GetAsync(DataRepository dataRepository, string spreadsheetId)
        {
            //
            Saturns5MovementRepository saturns5MovementRepository = new Saturns5MovementRepository(spreadsheetId);

            // Assign provided data repository.
            saturns5MovementRepository._dataRepository = dataRepository;

            // Get live spreadsheets database reference.
            LiveSpreadsheetsDb db = saturns5MovementRepository._dataRepository.GoogleService.SpreadsheetsDb;

            // Load spreadsheet containing saturns dashboard.
            await db.LoadSpreadsheetAsync(saturns5MovementRepository.Saturns5Movement_SpreadsheetId);

            // Get and assign reference to Movement spreadsheet and sheet
            saturns5MovementRepository._dashboardSpreadsheet = db[saturns5MovementRepository.Saturns5Movement_SpreadsheetId];
            saturns5MovementRepository._movementSheet = saturns5MovementRepository._dashboardSpreadsheet[Saturns5MovementRepository.Saturns5Movement_SheetId];

            return saturns5MovementRepository;
        }
        #endregion

        #region ISaturns5MovementRepository - support
        // Create
        public void AddSendToITFaultedAndDamagedLog(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string damagedBy, string faultReportedBy, string damageDescription, string movementNote)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (sentBy is null) throw new ArgumentNullException(nameof(sentBy));
            else if (damagedBy is null) throw new ArgumentNullException(nameof(damagedBy));
            else if (faultReportedBy is null) throw new ArgumentNullException(nameof(faultReportedBy));
            else if (damageDescription is null) throw new ArgumentNullException(nameof(damageDescription));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided {serialNumber} is not recognized.", nameof(serialNumber));
            else if (!this.UserRepository.HasUsername(sentBy)) throw new ArgumentException($"Provided {sentBy} is not recognized.", nameof(sentBy));

            // Get necessary data

            // Get database reference.
            LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

            // Get timestamp
            string timestamp = DateTime.Now.ToTimestamp();

            // Obtain saturn 5 spreadsheet id...
            string saturn5SpreadsheetId = this.Saturn5Repository.GetSaturn5LogSpreadsheetId(serialNumber);

            // ... and URL based on it.
            string saturn5SpreadsheetURL = this.GetUrlFromSpreadsheetId(saturn5SpreadsheetId);
        
            // Prepare data to commit

            // Create new row blueprint...
            IList<IList<string>> saturn5MovementLogRowData = new IList<string>[Saturns5MovementRepository.Saturns5Movement_EntryRowsCount]
            { new string[Saturns5MovementRepository.Saturns5Movement_ColumnsCount] };

            // .. and fill it with data.. (0 as first (and only) row of the data in the one-row-grid provided to 'AppendRows' method).

            // 'Timestamp:' - when send to IT has been logged.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Timestamp_ColumnsIndex] = timestamp;

            // 'Serial Number:' of the saturn 5 unit send to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_SerialNumber_ColumnsIndex] = serialNumber;

            // 'Consignment Number:' used to send unit to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ConsignmentNumber_ColumnsIndex] = consignmentNumber;

            // 'Incident Number:' associated with
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_IncidentNumber_ColumnsIndex] = incidentNumber;

            // 'Saturn 5 Spreadsheet URL:' last known saturn5 unit spreadsheetURL
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Saturn5SpreadsheetURL_ColumnsIndex] = saturn5SpreadsheetURL;

            // 'Received / Sent:' - Sent
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSent_ColumnsIndex] = Saturn5MovementType.Sent.ToString();

            // 'Received by/ Sent by:' Send by - Username.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByUsername_ColumnsIndex] = sentBy;

            // 'Physical damage:' - boolean flag indicating whether unit is damage or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_PhysicalDamage_ColumnsIndex] = true.ToString();

            // 'Damaged by username:' - Username of the user responsible for damage of the unit.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamagedByUser_ColumnsIndex] = damagedBy;

            // 'Non-physical issue::' - boolean flag indicating whether unit is faulty or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssue_ColumnsIndex] = true.ToString();

            // 'Non-physical issue reported by:' - Username of the user responsible for reporting unit fault.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssueReportedByUsername_ColumnsIndex] = faultReportedBy;

            // 'Damage/Issue description:' - damage description
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamageOrIssueDescription_ColumnsIndex] = damageDescription;

            // 'Received by note/ Sent by note:' - Movement note
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByNote_ColumnsIndex] = movementNote;

            
            // Commit data.
            this._movementSheet.AppendRows(db, Saturns5MovementRepository.Saturns5Movement_EntryRowsCount, saturn5MovementLogRowData);
        }

        public void AddSendToITOnlyDamagedLog(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string damagedBy, string damageDescription, string movementNote)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (sentBy is null) throw new ArgumentNullException(nameof(sentBy));
            else if (damagedBy is null) throw new ArgumentNullException(nameof(damagedBy));
            else if (damageDescription is null) throw new ArgumentNullException(nameof(damageDescription));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided {serialNumber} is not recognized.", nameof(serialNumber));
            else if (!this.UserRepository.HasUsername(sentBy)) throw new ArgumentException($"Provided {sentBy} is not recognized.", nameof(sentBy));

            // Get necessary data

            // Get database reference.
            LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

            // Get timestamp
            string timestamp = DateTime.Now.ToTimestamp();

            // Obtain saturn 5 spreadsheet id...
            string saturn5SpreadsheetId = this.Saturn5Repository.GetSaturn5LogSpreadsheetId(serialNumber);

            // ... and URL based on it.
            string saturn5SpreadsheetURL = this.GetUrlFromSpreadsheetId(saturn5SpreadsheetId);

            // Prepare data to commit

            // Create new row blueprint...
            IList<IList<string>> saturn5MovementLogRowData = new IList<string>[Saturns5MovementRepository.Saturns5Movement_EntryRowsCount]
            { new string[Saturns5MovementRepository.Saturns5Movement_ColumnsCount] };

            // .. and fill it with data.. (0 as first (and only) row of the data in the one-row-grid provided to 'AppendRows' method).

            // 'Timestamp:' - when send to IT has been logged.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Timestamp_ColumnsIndex] = timestamp;

            // 'Serial Number:' of the saturn 5 unit send to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_SerialNumber_ColumnsIndex] = serialNumber;

            // 'Consignment Number:' used to send unit to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ConsignmentNumber_ColumnsIndex] = consignmentNumber;

            // 'Incident Number:' associated with
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_IncidentNumber_ColumnsIndex] = incidentNumber;

            // 'Saturn 5 Spreadsheet URL:' last known saturn5 unit spreadsheetURL
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Saturn5SpreadsheetURL_ColumnsIndex] = saturn5SpreadsheetURL;

            // 'Received / Sent:' - Sent
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSent_ColumnsIndex] = Saturn5MovementType.Sent.ToString();

            // 'Received by/ Sent by:' Send by - Username.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByUsername_ColumnsIndex] = sentBy;

            // 'Physical damage:' - boolean flag indicating whether unit is damage or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_PhysicalDamage_ColumnsIndex] = true.ToString();

            // 'Damaged by username:' - Username of the user responsible for damage of the unit.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamagedByUser_ColumnsIndex] = damagedBy;

            // 'Non-physical issue::' - boolean flag indicating whether unit is faulty or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssue_ColumnsIndex] = false.ToString();

            // 'Non-physical issue reported by:' - Username of the user responsible for reporting unit fault.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssueReportedByUsername_ColumnsIndex] = null;

            // 'Damage/Issue description:' - damage description
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamageOrIssueDescription_ColumnsIndex] = damageDescription;

            // 'Received by note/ Sent by note:' - Movement note
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByNote_ColumnsIndex] = movementNote;


            // Commit data.
            this._movementSheet.AppendRows(db, Saturns5MovementRepository.Saturns5Movement_EntryRowsCount, saturn5MovementLogRowData);
        }

        public void AddSendToITOnlyFaultedLog(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string faultReportedBy, string faultDescription, string movementNote)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (sentBy is null) throw new ArgumentNullException(nameof(sentBy));
            else if (faultReportedBy is null) throw new ArgumentNullException(nameof(faultReportedBy));
            else if (faultReportedBy is null) throw new ArgumentNullException(nameof(faultReportedBy));
            else if (faultDescription is null) throw new ArgumentNullException(nameof(faultDescription));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided {serialNumber} is not recognized.", nameof(serialNumber));
            else if (!this.UserRepository.HasUsername(sentBy)) throw new ArgumentException($"Provided {sentBy} is not recognized.", nameof(sentBy));

            // Get necessary data

            // Get database reference.
            LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

            // Get timestamp
            string timestamp = DateTime.Now.ToTimestamp();

            // Obtain saturn 5 spreadsheet id...
            string saturn5SpreadsheetId = this.Saturn5Repository.GetSaturn5LogSpreadsheetId(serialNumber);

            // ... and URL based on it.
            string saturn5SpreadsheetURL = this.GetUrlFromSpreadsheetId(saturn5SpreadsheetId);

            // Prepare data to commit

            // Create new row blueprint...
            IList<IList<string>> saturn5MovementLogRowData = new IList<string>[Saturns5MovementRepository.Saturns5Movement_EntryRowsCount]
            { new string[Saturns5MovementRepository.Saturns5Movement_ColumnsCount] };

            // .. and fill it with data.. (0 as first (and only) row of the data in the one-row-grid provided to 'AppendRows' method).

            // 'Timestamp:' - when send to IT has been logged.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Timestamp_ColumnsIndex] = timestamp;

            // 'Serial Number:' of the saturn 5 unit send to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_SerialNumber_ColumnsIndex] = serialNumber;

            // 'Consignment Number:' used to send unit to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ConsignmentNumber_ColumnsIndex] = consignmentNumber;

            // 'Incident Number:' associated with
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_IncidentNumber_ColumnsIndex] = incidentNumber;

            // 'Saturn 5 Spreadsheet URL:' last known saturn5 unit spreadsheetURL
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Saturn5SpreadsheetURL_ColumnsIndex] = saturn5SpreadsheetURL;

            // 'Received / Sent:' - Sent
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSent_ColumnsIndex] = Saturn5MovementType.Sent.ToString();

            // 'Received by/ Sent by:' Send by - Username.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByUsername_ColumnsIndex] = sentBy;

            // 'Physical damage:' - boolean flag indicating whether unit is damage or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_PhysicalDamage_ColumnsIndex] = false.ToString();

            // 'Damaged by username:' - Username of the user responsible for damage of the unit.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamagedByUser_ColumnsIndex] = null;

            // 'Non-physical issue::' - boolean flag indicating whether unit is faulty or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssue_ColumnsIndex] = true.ToString();

            // 'Non-physical issue reported by:' - Username of the user responsible for reporting unit fault.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssueReportedByUsername_ColumnsIndex] = faultReportedBy;

            // 'Damage/Issue description:' - fault description
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamageOrIssueDescription_ColumnsIndex] = faultDescription;

            // 'Received by note/ Sent by note:' - Movement note
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByNote_ColumnsIndex] = movementNote;


            // Commit data.
            this._movementSheet.AppendRows(db, Saturns5MovementRepository.Saturns5Movement_EntryRowsCount, saturn5MovementLogRowData);
        }

        public void AddSendToITFullyFunctionalSurplusLog(string serialNumber, string consignmentNumber, string incidentNumber, string sentBy, string noIssueSentDescription, string movementNote)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (sentBy is null) throw new ArgumentNullException(nameof(sentBy));
            else if (noIssueSentDescription is null) throw new ArgumentNullException(nameof(noIssueSentDescription));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided {serialNumber} is not recognized.", nameof(serialNumber));
            else if (!this.UserRepository.HasUsername(sentBy)) throw new ArgumentException($"Provided {sentBy} is not recognized.", nameof(sentBy));

            // Get necessary data

            // Get database reference.
            LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

            // Get timestamp
            string timestamp = DateTime.Now.ToTimestamp();

            // Obtain saturn 5 spreadsheet id...
            string saturn5SpreadsheetId = this.Saturn5Repository.GetSaturn5LogSpreadsheetId(serialNumber);

            // ... and URL based on it.
            string saturn5SpreadsheetURL = this.GetUrlFromSpreadsheetId(saturn5SpreadsheetId);

            // Prepare data to commit

            // Create new row blueprint...
            IList<IList<string>> saturn5MovementLogRowData = new IList<string>[Saturns5MovementRepository.Saturns5Movement_EntryRowsCount]
            { new string[Saturns5MovementRepository.Saturns5Movement_ColumnsCount] };

            // .. and fill it with data.. (0 as first (and only) row of the data in the one-row-grid provided to 'AppendRows' method).

            // 'Timestamp:' - when send to IT has been logged.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Timestamp_ColumnsIndex] = timestamp;

            // 'Serial Number:' of the saturn 5 unit send to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_SerialNumber_ColumnsIndex] = serialNumber;

            // 'Consignment Number:' used to send unit to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ConsignmentNumber_ColumnsIndex] = consignmentNumber;

            // 'Incident Number:' associated with
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_IncidentNumber_ColumnsIndex] = incidentNumber;

            // 'Saturn 5 Spreadsheet URL:' last known saturn5 unit spreadsheetURL
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Saturn5SpreadsheetURL_ColumnsIndex] = saturn5SpreadsheetURL;

            // 'Received / Sent:' - Sent
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSent_ColumnsIndex] = Saturn5MovementType.Sent.ToString();

            // 'Received by/ Sent by:' Send by - Username.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByUsername_ColumnsIndex] = sentBy;

            // 'Physical damage:' - boolean flag indicating whether unit is damage or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_PhysicalDamage_ColumnsIndex] = false.ToString();

            // 'Damaged by username:' - Username of the user responsible for damage of the unit.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamagedByUser_ColumnsIndex] = null;

            // 'Non-physical issue::' - boolean flag indicating whether unit is faulty or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssue_ColumnsIndex] = false.ToString();

            // 'Non-physical issue reported by:' - Username of the user responsible for reporting unit fault.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssueReportedByUsername_ColumnsIndex] = null;

            // 'Damage/Issue description:' - no issue sent description
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamageOrIssueDescription_ColumnsIndex] = noIssueSentDescription;

            // 'Received by note/ Sent by note:' - Movement note
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByNote_ColumnsIndex] = movementNote;


            // Commit data.
            this._movementSheet.AppendRows(db, Saturns5MovementRepository.Saturns5Movement_EntryRowsCount, saturn5MovementLogRowData);
        }

        public void AddReceiveFromITLog(string serialNumber, string consignmentNumber, string receivedBy, string noIssueReceivedDescription, string movementNote)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (receivedBy is null) throw new ArgumentNullException(nameof(receivedBy));
            else if (noIssueReceivedDescription is null) throw new ArgumentNullException(nameof(noIssueReceivedDescription));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided {serialNumber} is not recognized.", nameof(serialNumber));
            else if (!this.UserRepository.HasUsername(receivedBy)) throw new ArgumentException($"Provided {receivedBy} is not recognized.", nameof(receivedBy));

            // Get necessary data

            // Get database reference.
            LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

            // Get timestamp
            string timestamp = DateTime.Now.ToTimestamp();

            // Obtain saturn 5 spreadsheet id...
            string saturn5SpreadsheetId = this.Saturn5Repository.GetSaturn5LogSpreadsheetId(serialNumber);

            // ... and URL based on it.
            string saturn5SpreadsheetURL = this.GetUrlFromSpreadsheetId(saturn5SpreadsheetId);

            // Prepare data to commit

            // Create new row blueprint...
            IList<IList<string>> saturn5MovementLogRowData = new IList<string>[Saturns5MovementRepository.Saturns5Movement_EntryRowsCount]
            { new string[Saturns5MovementRepository.Saturns5Movement_ColumnsCount] };

            // .. and fill it with data.. (0 as first (and only) row of the data in the one-row-grid provided to 'AppendRows' method).

            // 'Timestamp:' - when send to IT has been logged.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Timestamp_ColumnsIndex] = timestamp;

            // 'Serial Number:' of the saturn 5 unit send to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_SerialNumber_ColumnsIndex] = serialNumber;

            // 'Consignment Number:' used to send unit to IT.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ConsignmentNumber_ColumnsIndex] = consignmentNumber;

            // 'Incident Number:' associated with
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_IncidentNumber_ColumnsIndex] = null;

            // 'Saturn 5 Spreadsheet URL:' last known saturn5 unit spreadsheetURL
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Saturn5SpreadsheetURL_ColumnsIndex] = saturn5SpreadsheetURL;

            // 'Received / Sent:' - Sent
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSent_ColumnsIndex] = Saturn5MovementType.Received.ToString();

            // 'Received by/ Sent by:' Send by - Username.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByUsername_ColumnsIndex] = receivedBy;

            // 'Physical damage:' - boolean flag indicating whether unit is damage or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_PhysicalDamage_ColumnsIndex] = false.ToString();

            // 'Damaged by username:' - Username of the user responsible for damage of the unit.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamagedByUser_ColumnsIndex] = null;

            // 'Non-physical issue::' - boolean flag indicating whether unit is faulty or not.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssue_ColumnsIndex] = false.ToString();

            // 'Non-physical issue reported by:' - Username of the user responsible for reporting unit fault.
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssueReportedByUsername_ColumnsIndex] = null;

            // 'Damage/Issue description:' - no issue received description
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamageOrIssueDescription_ColumnsIndex] = noIssueReceivedDescription;

            // 'Received by note/ Sent by note:' - Movement note
            saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByNote_ColumnsIndex] = movementNote;


            // Commit data.
            this._movementSheet.AppendRows(db, Saturns5MovementRepository.Saturns5Movement_EntryRowsCount, saturn5MovementLogRowData);
        }

        #region Old - ref only
        //public void AddNewLog(Saturn5 saturn5, string consignmentNumber, string incidentNumber, string saturn5SpreadsheetId, Saturn5MovementType movementType, string movementByUsername, bool physicalDamage, string damagedByUsername, bool nonPhysicalIssue, string nonPhysicalIssueReportedByUsername, string damageOrIssueDescription, string movementNote)
        //{
        //    if (saturn5 is null) throw new ArgumentNullException(nameof(saturn5));
        //    else if (consignmentNumber is null) throw new ArgumentNullException(nameof(consignmentNumber));
        //    else if (saturn5SpreadsheetId is null) throw new ArgumentNullException(nameof(saturn5SpreadsheetId));
        //    else if (movementByUsername is null) throw new ArgumentNullException(nameof(movementByUsername));

        //    LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

        //    IList<IList<string>> saturn5MovementLogRowData = new IList<string>[Saturns5MovementRepository.Saturns5Movement_EntryRowsCount]
        //    {
        //        new string[Saturns5MovementRepository.Saturns5Movement_ColumnsCount]
        //    };


        //    // 0 as first (and only) row of the data 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Timestamp_ColumnsIndex] = DateTime.Now.ToTimestamp(); 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_SerialNumber_ColumnsIndex] = saturn5.SerialNumber; 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ConsignmentNumber_ColumnsIndex] = consignmentNumber; 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_IncidentNumber_ColumnsIndex] = incidentNumber; 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_Saturn5SpreadsheetURL_ColumnsIndex] = this.GetUrlFromSpreadsheetId(saturn5SpreadsheetId); 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSent_ColumnsIndex] = movementType.ToString(); 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByUsername_ColumnsIndex] = movementByUsername; 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_PhysicalDamage_ColumnsIndex] = physicalDamage.ToString(); 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamagedByUser_ColumnsIndex] = damagedByUsername; 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssue_ColumnsIndex] = nonPhysicalIssue.ToString(); 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_NonPhysicalIssueReportedByUsername_ColumnsIndex] = nonPhysicalIssueReportedByUsername; 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_DamageOrIssueDescription_ColumnsIndex] = damageOrIssueDescription; 
        //    saturn5MovementLogRowData[0][Saturns5MovementRepository.Saturns5Movement_ReceivedOrSentByNote_ColumnsIndex] = movementNote; 

        //    this._movementSheet.AppendRows(db, Saturns5MovementRepository.Saturns5Movement_EntryRowsCount, saturn5MovementLogRowData);
        //}
        #endregion
        #endregion

        #region Private Helpers
        private string GetUrlFromSpreadsheetId(string spreadsheetId)
        {
            return SaturnLog.Repository.Saturns5DashboardRepository.GoogleSpreadsheetsURLAppendix + spreadsheetId;
        }
        #endregion

        #region IDisposable - support
        public void Dispose()
        {
            this._dashboardSpreadsheet?.Dispose();

            this._dashboardSpreadsheet = null;

            this._movementSheet = null;

            this._dataRepository = null;
        }
        #endregion
    }
}
