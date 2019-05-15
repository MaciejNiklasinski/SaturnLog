using LiveGoogle.Sheets;
using SaturnLog.Core;
using SaturnLog.Repository.EventArgs;
using SaturnLog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Repository
{
    public class Saturn5IssuesRepository : ISaturn5IssuesRepository, ISaturn5IssuesDB, IDisposable
    {
        #region Const
        #region Saturn5 Issue Sheet
        public const string Saturn5Log_Metadata_SheetTitle = Repository.Saturn5Repository.Saturn5Log_Metadata_SheetTitle;

        public const int Saturn5Log_Metadata_RowsCount = Repository.Saturn5Repository.Saturn5Log_Metadata_RowsCount;
        public const int Saturn5Log_Metadata_ColumnsCount = Repository.Saturn5Repository.Saturn5Log_Metadata_ColumnsCount;
        public const int Saturn5Log_Metadata_SerialNumberValue_ColumnIndex = Repository.Saturn5Repository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex;
        public const int Saturn5Log_Metadata_SerialNumberValue_RowIndex = Repository.Saturn5Repository.Saturn5Log_Metadata_SerialNumberValue_RowIndex;

        public const string Saturn5Log_Issues_SheetTitle = Repository.Saturn5Repository.Saturn5Log_Issues_SheetTitle;
        public const int Saturn5Log_Issues_RowsCount = Repository.Saturn5Repository.Saturn5Log_Issues_RowsCount;
        public const int Saturn5Log_Issues_ColumnsCount = Repository.Saturn5Repository.Saturn5Log_Issues_ColumnsCount;
        public readonly static string[] Saturn5Log_Issues_InitialRowContent = Repository.Saturn5Repository.Saturn5Log_Issues_InitialHeaderRowContent;

        public const int Saturn5Log_Issues_Timestamp_ColumnIndex = Repository.Saturn5Repository.Saturn5Log_Issues_Timestamp_ColumnIndex;
        public const int Saturn5Log_Issues_IssueReportedBy_ColumnIndex = Repository.Saturn5Repository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex;
        public const int Saturn5Log_Issues_IssueDescription_ColumnIndex = Repository.Saturn5Repository.Saturn5Log_Issues_IssueDescription_ColumnIndex;
        public const int Saturn5Log_Issues_Resolved_ColumnIndex = Repository.Saturn5Repository.Saturn5Log_Issues_Resolved_ColumnIndex;
        public const int Saturn5Log_Issues_ResolvedHow_ColumnIndex = Repository.Saturn5Repository.Saturn5Log_Issues_ResolvedHow_ColumnIndex;
        public const int Saturn5Log_Issues_ResolvedBy_ColumnIndex = Repository.Saturn5Repository.Saturn5Log_Issues_ResolvedBy_ColumnIndex;
        #endregion

        #region Saturns5 Unresolved Issues - Saturn5_Dashboard sheet
        public readonly string Saturn5UnresolvedIssues_SpreadsheetId;

        public const string Saturn5UnresolvedIssues_SheetId = "Saturns5 Unresolved Issues";
        public const int Saturn5UnresolvedIssues_HeaderRowIndex = 0;
        public const int Saturn5UnresolvedIssues_InitialRowsCount = 1;
        public const int Saturn5UnresolvedIssues_EntryRowsCount = 1;
        public const int Saturn5UnresolvedIssues_ColumnsCount = 7;

        public const int Saturn5UnresolvedIssues_Timestamp_ColumnIndex = 0;
        public const int Saturn5UnresolvedIssues_SerialNumber_ColumnIndex = 1;
        public const int Saturn5UnresolvedIssues_PhysicallDamaged_ColumnIndex = 2;
        public const int Saturn5UnresolvedIssues_ByUsername_ColumnIndex = 3;
        public const int Saturn5UnresolvedIssues_ByFirstName_ColumnIndex = 4;
        public const int Saturn5UnresolvedIssues_BySurname_ColumnIndex = 5;
        public const int Saturn5UnresolvedIssues_IssueDescription_ColumnIndex = 6;

        public readonly static string[] Saturn5UnresolvedIssues_HeaderRowContent = new string[Saturn5IssuesRepository.Saturn5UnresolvedIssues_ColumnsCount]
        { "Timestamp:", "Serial Number:", "Physical Damage:", "Damaged/Reported by username:", "Damaged/Reported by first name:", "Damaged/Reported by surname:", "Issue description:" };
        #endregion
        #endregion

        #region Private fields and private properties
        private object IssuesDataLock { get { return this._dataRepository.Saturns5DB.Saturns5DBLock; } }

        // Data repository 
        private DataRepository _dataRepository;

        private LiveSpreadsheet _dashboardSpreadsheet;
        private LiveSheet _saturns5UnresolvedIssuesSheet;

        private IDictionary<string, LiveSheet> _issuesIndex = new Dictionary<string, LiveSheet>();

        private ISaturn5DB Saturn5Repository { get { return this._dataRepository.Saturns5DB; } }
        private IUserRepository UserRepository { get { return this._dataRepository.UserRepository; } }
        #endregion

        #region Constructor
        private Saturn5IssuesRepository(string spreadsheetId) { this.Saturn5UnresolvedIssues_SpreadsheetId = spreadsheetId; }
        internal static async Task<Saturn5IssuesRepository> GetAsync(DataRepository dataRepository, string spreadsheetId)
        {
            Saturn5IssuesRepository saturn5IssuesRepository = new Saturn5IssuesRepository(spreadsheetId);

            // Assign provided data repository.
            saturn5IssuesRepository._dataRepository = dataRepository;

            // Get live spreadsheets database reference.
            LiveSpreadsheetsDb db = saturn5IssuesRepository._dataRepository.GoogleService.SpreadsheetsDb;

            // Load spreadsheet containing saturns dashboard.
            await db.LoadSpreadsheetAsync(saturn5IssuesRepository.Saturn5UnresolvedIssues_SpreadsheetId);

            // Get and assign reference to unresolved issues spreadsheet and sheet
            saturn5IssuesRepository._dashboardSpreadsheet = db[saturn5IssuesRepository.Saturn5UnresolvedIssues_SpreadsheetId];
            saturn5IssuesRepository._saturns5UnresolvedIssuesSheet = saturn5IssuesRepository._dashboardSpreadsheet[Saturn5IssuesRepository.Saturn5UnresolvedIssues_SheetId];

            // Subscribe to appropriate events of Saturn 5 repository
            saturn5IssuesRepository.Saturn5Repository.Saturn5SpreadsheetLoaded += saturn5IssuesRepository.OnSaturn5IssuesSheetLoaded;
            saturn5IssuesRepository.Saturn5Repository.Saturn5SpreadsheetAdded += saturn5IssuesRepository.OnSaturn5IssuesSheetAdded;
            saturn5IssuesRepository.Saturn5Repository.Saturn5SpreadsheetReplaced += saturn5IssuesRepository.OnSaturn5IssuesSheetReplaced;
            saturn5IssuesRepository.Saturn5Repository.Saturn5SpreadsheetArchived += saturn5IssuesRepository.OnSaturn5IssuesSheetArchived;
            saturn5IssuesRepository.Saturn5Repository.Saturn5SpreadsheetUnarchived += saturn5IssuesRepository.OnSaturn5IssuesSheetUnarchived;
            saturn5IssuesRepository.Saturn5Repository.Saturn5SpreadsheetRemoved += saturn5IssuesRepository.OnSaturn5IssuesSheetRemoved;

            return saturn5IssuesRepository;
        }
        #endregion

        #region Saturn5IssuesRepository - Methods 
        // Gets, and cache issues LiveSheet related with loaded staurn5.
        public void OnSaturn5IssuesSheetLoaded(object sender, Saturn5SpreadsheetLoadedEventArgs e)
        {
            LiveSpreadsheet saturn5Spreadsheet = e.Spreadsheet;

            LiveSheet metadataSheet = saturn5Spreadsheet[Saturn5IssuesRepository.Saturn5Log_Metadata_SheetTitle];

            string serialNumber = metadataSheet[Saturn5IssuesRepository.Saturn5Log_Metadata_SerialNumberValue_RowIndex][Saturn5IssuesRepository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex].GetDataAsString();
            
            LiveSheet issuesSheet = saturn5Spreadsheet[Saturn5IssuesRepository.Saturn5Log_Issues_SheetTitle];

            // If Saturn 5 Issues Repository doesn't contain any sheet associated with provided serial number..
            if (!_issuesIndex.ContainsKey(serialNumber))
                // ... add one.
                this._issuesIndex.Add(serialNumber, issuesSheet);
            // Otherwise if Saturn 5 Issues Repository does contain the entry associated with provided serial number,
            // and its referring to the same issues live sheet...
            else if (this._issuesIndex[serialNumber].Equals(issuesSheet))
                // .. return as no action is required.
                return;
            // Otherwise if Saturn 5 Issues Repository does contain the entry associated with provided serial number,
            // by the entry its referring to different issues sheet, replace it.
            else
                // TODO consider throwing out an exception
                this._issuesIndex[serialNumber] = issuesSheet;
        }

        // Gets, and cache issues LiveSheet related with added staurn5.
        public void OnSaturn5IssuesSheetAdded(object sender, Saturn5SpreadsheetAddedEventArgs e)
        {
            LiveSpreadsheet saturn5Spreadsheet = e.Spreadsheet;

            LiveSheet metadataSheet = saturn5Spreadsheet[Saturn5IssuesRepository.Saturn5Log_Metadata_SheetTitle];

            string serialNumber = metadataSheet[Saturn5IssuesRepository.Saturn5Log_Metadata_SerialNumberValue_RowIndex][Saturn5IssuesRepository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex].GetDataAsString();

            LiveSheet issuesSheet = saturn5Spreadsheet[Saturn5IssuesRepository.Saturn5Log_Issues_SheetTitle];

            // If Saturn 5 Issues Repository doesn't contain any sheet associated with provided serial number..
            if (!_issuesIndex.ContainsKey(serialNumber))
                // ... add one.
                this._issuesIndex.Add(serialNumber, issuesSheet);
            else
                throw new InvalidOperationException($"Cannot add new issue sheet into the saturn 5 issues repository because the repository already contains the sheet associated with provided serial number {serialNumber}");

        }

        // Gets, and replaces cache issues LiveSheet related with staurn5 whose spreadsheet has been replaced.
        public void OnSaturn5IssuesSheetReplaced(object sender, Saturn5SpreadsheetReplacedEventArgs e)
        {
            LiveSpreadsheet saturn5Spreadsheet = e.Spreadsheet;

            LiveSheet metadataSheet = saturn5Spreadsheet[Saturn5IssuesRepository.Saturn5Log_Metadata_SheetTitle];

            string serialNumber = metadataSheet[Saturn5IssuesRepository.Saturn5Log_Metadata_SerialNumberValue_RowIndex][Saturn5IssuesRepository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex].GetDataAsString();

            LiveSheet issuesSheet = saturn5Spreadsheet[Saturn5IssuesRepository.Saturn5Log_Issues_SheetTitle];

            // If Saturn 5 Issues Repository does contain any sheet associated with provided serial number..
            if (_issuesIndex.ContainsKey(serialNumber))
                // ... replace it with new one.
                this._issuesIndex[serialNumber] = issuesSheet;
            else
                throw new InvalidOperationException($"Cannot replace existing issue sheet into the saturn 5 issues repository because the repository doesn't contains the sheet associated with the provided serial number {serialNumber}");
        }

        // Removes cached issues LiveSheet related with archived staurn5.
        public void OnSaturn5IssuesSheetArchived(object sender, Saturn5SpreadsheetArchivedEventArgs e)
        {
            string serialNumber = e.SerialNumber;

            // If Saturn 5 Issues Repository does contain any sheet associated with provided serial number..
            if (_issuesIndex.ContainsKey(serialNumber))
                // ... replace it with new one.
                this._issuesIndex.Remove(serialNumber);
            // Otherwise do nothing and return.
            else
                return;
        }

        // Gets, and cache issues LiveSheet related with unarchived staurn5.
        public void OnSaturn5IssuesSheetUnarchived(object sender, Saturn5SpreadsheetUnarchivedEventArgs e)
        {
            LiveSpreadsheet saturn5Spreadsheet = e.Spreadsheet;

            LiveSheet metadataSheet = saturn5Spreadsheet[Saturn5IssuesRepository.Saturn5Log_Metadata_SheetTitle];

            string serialNumber = metadataSheet[Saturn5IssuesRepository.Saturn5Log_Metadata_SerialNumberValue_RowIndex][Saturn5IssuesRepository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex].GetDataAsString();

            LiveSheet issuesSheet = saturn5Spreadsheet[Saturn5IssuesRepository.Saturn5Log_Issues_SheetTitle];

            // If Saturn 5 Issues Repository doesn't contain any sheet associated with provided serial number..
            if (!_issuesIndex.ContainsKey(serialNumber))
                // ... add one.
                this._issuesIndex.Add(serialNumber, issuesSheet);
            else
                throw new InvalidOperationException($"Cannot add new issue sheet into the saturn 5 issues repository because the repository already contains the sheet associated with provided serial number {serialNumber}");

        }

        // Removes cached issues LiveSheet related with removed staurn5.
        public void OnSaturn5IssuesSheetRemoved(object sender, Saturn5SpreadsheetRemovedEventArgs e)
        {
            string serialNumber = e.SerialNumber;

            // If Saturn 5 Issues Repository does contain any sheet associated with provided serial number..
            if (_issuesIndex.ContainsKey(serialNumber))
                // ... replace it with new one.
                this._issuesIndex.Remove(serialNumber);
            // Otherwise do nothing and return.
            else
                return;
        }
        
        // TO BE EXECUTED ONLY AFTER ALL saturn5 individual spreadsheets gonna get loaded

        // Assures that the content of Saturns5UnresolvedIssues sheet resemble all the unresolved issues
        // related with 
        public void AssureSaturns5UnresolvedIssueContent()
        {
            this.RemoveResolvedOrUnexistingIssues();
            
            this.AddMissingUnresolvedIssues();
        }

        // TO BE EXECUTED ONLY AFTER ALL saturn5 individual spreadsheets gonna get loaded
        public void RemoveResolvedOrUnexistingIssues()
        {
            lock (this.IssuesDataLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                for (int i = this._saturns5UnresolvedIssuesSheet.BottomIndex; i >= 1; i--)
                {
                    LiveRow unresolvedIssueRow = this._saturns5UnresolvedIssuesSheet[i];

                    bool toBeRemoved = false;

                    string unresolvedIssueSerialNumber = unresolvedIssueRow[Saturn5IssuesRepository.Saturn5UnresolvedIssues_SerialNumber_ColumnIndex].GetDataAsString();

                    if (!this._dataRepository.Saturn5Repository.HasSaturn5SerialNumber(unresolvedIssueSerialNumber))
                        toBeRemoved = true;
                    else
                    {
                        IList<Saturn5Issue> unresolvedIssuesOrDamages = this.GetUnresolvedIssues(unresolvedIssueSerialNumber);

                        if (!unresolvedIssuesOrDamages.Any((issue) => { return issue.Timestamp == unresolvedIssueRow[Saturn5IssuesRepository.Saturn5UnresolvedIssues_Timestamp_ColumnIndex].GetDataAsString(); }))
                            toBeRemoved = true;
                    }

                    if (!toBeRemoved)
                        continue;

                    // Remove row representing non existing or resolved issue/damage
                    this._saturns5UnresolvedIssuesSheet.RemoveRows(db, unresolvedIssueRow.RowIndex, Saturn5IssuesRepository.Saturn5UnresolvedIssues_EntryRowsCount);
                }
            }
        }
        
        // TO BE EXECUTED ONLY AFTER ALL saturn5 individual spreadsheets gonna get loaded
        public void AddMissingUnresolvedIssues()
        {
            lock (this.IssuesDataLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                List<Saturn5Issue> unresolvedIssues = new List<Saturn5Issue>();
                
                IList<Saturn5> saturns5 = this._dataRepository.Saturn5Repository.ReadAll();
                
                foreach (Saturn5 saturn5 in saturns5)
                    unresolvedIssues.AddRange(this.GetUnresolvedIssues(saturn5.SerialNumber));

                foreach (Saturn5Issue unresolvedIssue in unresolvedIssues)
                {
                    if (!this._saturns5UnresolvedIssuesSheet.SheetRows.Any((row) => 
                    { return row[Saturn5IssuesRepository.Saturn5UnresolvedIssues_Timestamp_ColumnIndex].GetDataAsString() == unresolvedIssue.Timestamp 
                        && row[Saturn5IssuesRepository.Saturn5UnresolvedIssues_SerialNumber_ColumnIndex].GetDataAsString() == unresolvedIssue.SerialNumber; }))
                    {
                        bool damaged = (unresolvedIssue.Status == Saturn5IssueStatus.Damaged);

                        this._saturns5UnresolvedIssuesSheet.AppendRows(db, Saturn5IssuesRepository.Saturn5UnresolvedIssues_EntryRowsCount, 
                            new IList<object>[Saturn5IssuesRepository.Saturn5UnresolvedIssues_EntryRowsCount] 
                            { this.ConstructSaturns5UnresolvedIssueRowData(unresolvedIssue.Timestamp, unresolvedIssue.SerialNumber, unresolvedIssue.ReportedByUsername, damaged, unresolvedIssue.Description) });
                    }
                }

            }
        }
        #endregion

        #region ISaturn5IssuesRepository - Methods

        #region Create
        public void AddNewFault(string serialNumber, string byUsername, string desctiption)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            if (byUsername is null) throw new ArgumentNullException(nameof(byUsername));
            else if (desctiption is null) throw new ArgumentNullException(nameof(desctiption));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));
            else if (!this.UserRepository.HasUsername(byUsername)) throw new ArgumentException($"Provided User username {byUsername} is not recognized.", nameof(byUsername));
            else if (desctiption.Trim() == "") throw new ArgumentException("Fault description cannot be empty.", nameof(desctiption));
            
            // Add new issues into the db
            this.CommitAddNewIssueData(serialNumber, byUsername, false, desctiption.Trim());
        }

        public void AddNewDamage(string serialNumber, string byUsername, string desctiption)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            if (byUsername is null) throw new ArgumentNullException(nameof(byUsername));
            else if (desctiption is null) throw new ArgumentNullException(nameof(desctiption));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));
            else if (!this.UserRepository.HasUsername(byUsername)) throw new ArgumentException($"Provided User username {byUsername} is not recognized.", nameof(byUsername));
            else if (desctiption.Trim() == "") throw new ArgumentException("Damage description cannot be empty.", nameof(desctiption));

            // Add new issues into the db
            this.CommitAddNewIssueData(serialNumber, byUsername, true, desctiption.Trim());
        }
        #endregion

        #region Read
        public bool HasUnresolvedIssues(string serialNumber)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // If at least one unresolved issue related with provided serial number can be found return true,
            // otherwise return false.
            return this.GetUnresolvedIssues(serialNumber).Count > 0;
        }

        public bool HasUnresolvedFaults(string serialNumber)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // If at least one unresolved issue related with provided serial number can be found return true,
            // otherwise return false.
            return this.GetUnresolvedFaults(serialNumber).Count > 0;
        }

        public bool HasUnresolvedDamages(string serialNumber)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // If at least one unresolved issue related with provided serial number can be found return true,
            // otherwise return false.
            return this.GetUnresolvedDamages(serialNumber).Count > 0;
        }

        public IList<Saturn5Issue> GetIssues(string serialNumber)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // Get issue sheet associated with provided serial number
            LiveSheet issuesSheet = _issuesIndex[serialNumber];

            // Declare list of issues
            IList<Saturn5Issue> issuesOrDamages = new List<Saturn5Issue>();

            // for each row in issues sheet excluding first row - header row...
            for (int i = 1; i < issuesSheet.RowCount; i++)
            {
                // Get issue row located on the currently looped through index
                LiveRow issueRow = issuesSheet[i];

                // Get cell containing status of the issue from the issue row...
                LiveCell issueStatusCell = issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex];

                // ... and translate string value into the appropriate enum
                Saturn5IssueStatus issueStatus = Saturn5IssueStatusExtensions.GetFromString(issueStatusCell.GetDataAsString());

                // Add it into the issuesOrDamages list 
                issuesOrDamages.Add(new Saturn5Issue(
                        // serialNumber 
                        serialNumber,
                        // Timestamp
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Timestamp_ColumnIndex].GetDataAsString(),
                        // reported by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex].GetDataAsString(),
                        // description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueDescription_ColumnIndex].GetDataAsString(),
                        // status
                        Saturn5IssueStatusExtensions.GetFromString(issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex].GetDataAsString()),
                        // resolved how description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedHow_ColumnIndex].GetDataAsString(),
                        // resolved by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedBy_ColumnIndex].GetDataAsString()));
            }

            return issuesOrDamages;
        }

        public IList<Saturn5Issue> GetUnresolvedIssues(string serialNumber)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // Get issue sheet associated with provided serial number
            LiveSheet issuesSheet = _issuesIndex[serialNumber];

            // Declare list of unresolved issues
            IList<Saturn5Issue> unresolvedIssuesOrDamages = new List<Saturn5Issue>();

            // for each row in issues sheet excluding first row - header row...
            for (int i = 1; i < issuesSheet.RowCount; i++)
            {
                // Get issue row located on the currently looped through index
                LiveRow issueRow = issuesSheet[i];

                // Get cell containing status of the issue from the issue row...
                LiveCell issueStatusCell = issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex];

                // ... and translate string value into the appropriate enum
                Saturn5IssueStatus issueStatus = Saturn5IssueStatusExtensions.GetFromString(issueStatusCell.GetDataAsString());

                // If issue is still unresolved, add it into the unresolvedIssuesOrDamages list
                if (issueStatus == Saturn5IssueStatus.Reported || issueStatus == Saturn5IssueStatus.Damaged)
                    unresolvedIssuesOrDamages.Add(new Saturn5Issue(
                        // serialNumber 
                        serialNumber,
                        // Timestamp
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Timestamp_ColumnIndex].GetDataAsString(),
                        // reported by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex].GetDataAsString(),
                        // description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueDescription_ColumnIndex].GetDataAsString(),
                        // status
                        Saturn5IssueStatusExtensions.GetFromString(issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex].GetDataAsString()),
                        // resolved how description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedHow_ColumnIndex].GetDataAsString(),
                        // resolved by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedBy_ColumnIndex].GetDataAsString()));
            }

            return unresolvedIssuesOrDamages;
        }

        public IList<Saturn5Issue> GetUnresolvedFaults(string serialNumber)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // Get issue sheet associated with provided serial number
            LiveSheet issuesSheet = _issuesIndex[serialNumber];

            // Declare list of unresolved issues
            IList<Saturn5Issue> unresolvedIssues = new List<Saturn5Issue>();

            // for each row in issues sheet excluding first row - header row...
            for (int i = 1; i < issuesSheet.RowCount; i++)
            {
                // Get issue row located on the currently looped through index
                LiveRow issueRow = issuesSheet[i];

                // Get cell containing status of the issue from the issue row...
                LiveCell issueStatusCell = issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex];

                // ... and translate string value into the appropriate enum
                Saturn5IssueStatus issueStatus = Saturn5IssueStatusExtensions.GetFromString(issueStatusCell.GetDataAsString());

                // If issue is still unresolved, add it into the unresolvedIssues list
                if (issueStatus == Saturn5IssueStatus.Reported)
                    unresolvedIssues.Add(new Saturn5Issue(
                        // serialNumber 
                        serialNumber,
                        // Timestamp
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Timestamp_ColumnIndex].GetDataAsString(),
                        // reported by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex].GetDataAsString(),
                        // description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueDescription_ColumnIndex].GetDataAsString(),
                        // status
                        Saturn5IssueStatusExtensions.GetFromString(issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex].GetDataAsString()),
                        // resolved how description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedHow_ColumnIndex].GetDataAsString(),
                        // resolved by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedBy_ColumnIndex].GetDataAsString() ));
            }

            return unresolvedIssues;
        }

        public IList<Saturn5Issue> GetUnresolvedDamages(string serialNumber)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // Get issue sheet associated with provided serial number
            LiveSheet issuesSheet = _issuesIndex[serialNumber];

            // Declare list of unresolved issues
            IList<Saturn5Issue> unresolvedDamages = new List<Saturn5Issue>();

            // for each row in issues sheet excluding first row - header row...
            for (int i = 1; i < issuesSheet.RowCount; i++)
            {
                // Get issue row located on the currently looped through index
                LiveRow issueRow = issuesSheet[i];

                // Get cell containing status of the issue from the issue row...
                LiveCell issueStatusCell = issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex];

                // ... and translate string value into the appropriate enum
                Saturn5IssueStatus issueStatus = Saturn5IssueStatusExtensions.GetFromString(issueStatusCell.GetDataAsString());

                // If issue is still unresolved, add it into the unresolvedDamages list
                if (issueStatus == Saturn5IssueStatus.Damaged)
                    unresolvedDamages.Add(new Saturn5Issue(
                        // serialNumber 
                        serialNumber,
                        // Timestamp
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Timestamp_ColumnIndex].GetDataAsString(),
                        // reported by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex].GetDataAsString(),
                        // description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueDescription_ColumnIndex].GetDataAsString(),
                        // status
                        Saturn5IssueStatusExtensions.GetFromString(issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex].GetDataAsString()),
                        // resolved how description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedHow_ColumnIndex].GetDataAsString(),
                        // resolved by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedBy_ColumnIndex].GetDataAsString()));
            }

            return unresolvedDamages;
        }

        public IList<Saturn5Issue> GetIssuesTimerange(string serialNumber, DateTime timeRangeStart, DateTime timeRangeEnd)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided Saturn5 serial number {serialNumber} is not recognized.", nameof(serialNumber));
            else if (timeRangeStart > DateTime.Now) throw new ArgumentException($"Provided timeRangeStart cannot represent date in future from now.", nameof(timeRangeStart));
            else if (timeRangeEnd < timeRangeStart) throw new ArgumentException($"Provided timeRangeEnd cannot represent date earlier then the one represented by provided timeRangeStart.", nameof(timeRangeEnd));

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // Get issue sheet associated with provided serial number
            LiveSheet issuesSheet = _issuesIndex[serialNumber];

            // Declare list of unresolved issues
            IList<Saturn5Issue> issuesTimerange = new List<Saturn5Issue>();

            // for each row in issues sheet excluding first row - header row...
            for (int i = 1; i < issuesSheet.RowCount; i++)
            {
                // Get issue row located on the currently looped through index
                LiveRow issueRow = issuesSheet[i];

                // Get cell containing status of the issue from the issue row...
                LiveCell timestampCell = issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex];

                // ... and obtain issue timestamp from, it.
                string timestamp = timestampCell.GetDataAsString();
                
                // If issue timestamp is contained in the provided time range
                if (timeRangeStart.IsEarlierThan(timestamp) && timeRangeEnd.IsLaterThan(timestamp))
                    issuesTimerange.Add(new Saturn5Issue(
                        // serialNumber 
                        serialNumber,
                        // Timestamp
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Timestamp_ColumnIndex].GetDataAsString(),
                        // reported by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex].GetDataAsString(),
                        // description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueDescription_ColumnIndex].GetDataAsString(),
                        // status
                        Saturn5IssueStatusExtensions.GetFromString(issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex].GetDataAsString()),
                        // resolved how description
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedHow_ColumnIndex].GetDataAsString(),
                        // resolved by username
                        issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedBy_ColumnIndex].GetDataAsString()));
            }

            return issuesTimerange;
        }

        // Returns last unresolved saturn 5 fault or damage associated with the provided serial number, or throw appropriate exception if not available.
        public Saturn5Issue GetLastUnresolvedIssue(string serialNumber)
        {
            // Get all the unresolved faults related with the unit specified by provided serial number..
            IList<Saturn5Issue> unresolvedIssues = this.GetUnresolvedIssues(serialNumber);

            // .. and return last one, or throw appropriate exception if one is not available.
            return unresolvedIssues.LastOrDefault() ?? throw new ArgumentException("Provided serial number is associated with a Saturn5 unit which currently has no unresolved faults or damages.", nameof(serialNumber));
        }

        // Returns last unresolved saturn 5 fault (but not damage) associated with the provided serial number, or throw appropriate exception if not available.
        public Saturn5Issue GetLastUnresolvedFault(string serialNumber)
        {
            // Get all the unresolved faults related with the unit specified by provided serial number..
            IList<Saturn5Issue> unresolvedFaults = this.GetUnresolvedFaults(serialNumber);

            // .. and return last one, or throw appropriate exception if one is not available.
            return unresolvedFaults.LastOrDefault() ?? throw new ArgumentException("Provided serial number is associated with a Saturn5 unit which currently has no unresolved faults.", nameof(serialNumber));
        }

        // Returns last unresolved saturn 5 damage (but not fault) associated with the provided serial number, or throw appropriate exception if not available.
        public Saturn5Issue GetLastUnresolvedDamage(string serialNumber)
        {
            // Get all the unresolved damages related with the unit specified by provided serial number..
            IList<Saturn5Issue> unresolvedDamages = this.GetUnresolvedDamages(serialNumber);

            // .. and return last one, or throw appropriate exception if one is not available.
            return unresolvedDamages.LastOrDefault() ?? throw new ArgumentException("Provided serial number is associated with a Saturn5 unit which currently has no unresolved damages.",nameof(serialNumber));
        }
        #endregion

        #region Update
        public void Update(Saturn5Issue issue)
        {
            // Validate parameters
            if (issue is null) throw new ArgumentNullException(nameof(issue));
            else if (!this.Saturn5Repository.HasSaturn5SerialNumber(issue.SerialNumber)) throw new ArgumentException($"Provided Saturn5Issue serial number {issue.SerialNumber} is not recognized as associated with any existing, non-archived Saturn5 unit.", nameof(issue));
            
            // Get live spreadsheets database reference.
            LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

            // Assures that saturn 5 issue sheet is loaded... (which will trigger this.OnSaturn5IssuesSheetLoaded associated with provided Saturn5 serial number).
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(issue.SerialNumber);

            // Gets issueSheet.
            LiveSheet issueSheet = this._issuesIndex[issue.SerialNumber];

            // Get issue row associated with the same timestamp as provided issue...
            LiveRow issueRow = issueSheet.SheetRows.First((row) => { return (issue.Timestamp == row[Saturn5IssuesRepository.Saturn5Log_Issues_Timestamp_ColumnIndex].GetDataAsString()); });

            // ... and get current issue state ...
            Saturn5IssueStatus currentSaturn5IssueStatus = Saturn5IssueStatusExtensions.GetFromString(issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex].GetDataAsString());

            // If issue update changing issue from unresolved to resolved
            if ((currentSaturn5IssueStatus == Saturn5IssueStatus.Reported
                || currentSaturn5IssueStatus == Saturn5IssueStatus.Damaged)
                && (issue.Status != Saturn5IssueStatus.Reported 
                && issue.Status != Saturn5IssueStatus.Damaged))
            {
                // ... find unresolved issue row entry in the unresolved issues sheet
                LiveRow unresolvedIssueRow = this._saturns5UnresolvedIssuesSheet.SheetRows.FirstOrDefault((row) => 
                {
                    return (row[Saturn5IssuesRepository.Saturn5UnresolvedIssues_Timestamp_ColumnIndex].GetDataAsString() == issue.Timestamp
                    && row[Saturn5IssuesRepository.Saturn5UnresolvedIssues_SerialNumber_ColumnIndex].GetDataAsString() == issue.SerialNumber);
                });
                
                // ... an if such a entry has been found...
                if (!(unresolvedIssueRow is null))                    
                {
                    // ... remove it
                    int unresolvedIssueRowIndex = unresolvedIssueRow.RowIndex;
                    this._saturns5UnresolvedIssuesSheet.RemoveRows(db, unresolvedIssueRowIndex, Saturn5IssuesRepository.Saturn5UnresolvedIssues_EntryRowsCount);
                }
            }
            
            //... and update values of all the other cells with the ones obtained from the provided issue.
            issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex].SetData(issue.ReportedByUsername);
            issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueDescription_ColumnIndex].SetData(issue.Description);
            issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex].SetData(issue.Status.ToDisplayString());
            issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedHow_ColumnIndex].SetData(issue.ResolvedHowDescription);
            issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex].SetData(issue.ReportedByUsername);
            issueRow[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedBy_ColumnIndex].SetData(issue.ResolvedByUsername);

            // Commit data changes into the google database
            issueRow.Upload(db);
        }
        #endregion
        #endregion

        #region Private Helpers
        private IList<object> ConstructNewIssuesData(string timestamp, string username, bool damaged, string issueReport)
        {
            IList<object> issueData = new object[Saturn5IssuesRepository.Saturn5Log_Issues_ColumnsCount];
            issueData[Saturn5IssuesRepository.Saturn5Log_Issues_Timestamp_ColumnIndex] = timestamp;
            issueData[Saturn5IssuesRepository.Saturn5Log_Issues_IssueReportedBy_ColumnIndex] = username;
            issueData[Saturn5IssuesRepository.Saturn5Log_Issues_IssueDescription_ColumnIndex] = issueReport;

            if (!damaged)
                issueData[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex] = Saturn5IssueStatus.Reported.ToDisplayString();
            else
                issueData[Saturn5IssuesRepository.Saturn5Log_Issues_Resolved_ColumnIndex] = Saturn5IssueStatus.Damaged.ToDisplayString();

            issueData[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedHow_ColumnIndex] = null;
            issueData[Saturn5IssuesRepository.Saturn5Log_Issues_ResolvedBy_ColumnIndex] = null;

            return issueData;
        }
        
        private IList<object> ConstructSaturns5UnresolvedIssueRowData(string timestamp, string serialNumber, string username, bool damaged, string issueReport)
        {
            IList<object> saturns5UnresolvedIssueRowData = new object[Saturn5IssuesRepository.Saturn5UnresolvedIssues_ColumnsCount];
            saturns5UnresolvedIssueRowData[Saturn5IssuesRepository.Saturn5UnresolvedIssues_Timestamp_ColumnIndex] = timestamp;
            saturns5UnresolvedIssueRowData[Saturn5IssuesRepository.Saturn5UnresolvedIssues_SerialNumber_ColumnIndex] = serialNumber;

            if (damaged)
                saturns5UnresolvedIssueRowData[Saturn5IssuesRepository.Saturn5UnresolvedIssues_PhysicallDamaged_ColumnIndex] = "True";
            else
                saturns5UnresolvedIssueRowData[Saturn5IssuesRepository.Saturn5UnresolvedIssues_PhysicallDamaged_ColumnIndex] = "False";

            saturns5UnresolvedIssueRowData[Saturn5IssuesRepository.Saturn5UnresolvedIssues_ByUsername_ColumnIndex] = username;
            saturns5UnresolvedIssueRowData[Saturn5IssuesRepository.Saturn5UnresolvedIssues_ByFirstName_ColumnIndex] = this._dataRepository.UserRepository.GetUserFirstName(username);
            saturns5UnresolvedIssueRowData[Saturn5IssuesRepository.Saturn5UnresolvedIssues_BySurname_ColumnIndex] = this._dataRepository.UserRepository.GetUserSurname(username); ;
            saturns5UnresolvedIssueRowData[Saturn5IssuesRepository.Saturn5UnresolvedIssues_IssueDescription_ColumnIndex] = issueReport;

            return saturns5UnresolvedIssueRowData;
        }
        
        #region CommitData
        private void CommitAddNewIssueData(string serialNumber, string username, bool damaged, string issueReport)
        {
            // Get live spreadsheets database reference.
            LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

            // Obtain current time
            DateTime now = DateTime.Now;
            string timestamp = now.ToTimestamp();

            // Assures that saturn 5 issue sheet is loaded...
            this.Saturn5Repository.AssureSaturn5DataIsLoaded(serialNumber);

            // ... and obtain such a LiveSheet
            string saturn5SpreadsheetId = this.Saturn5Repository.GetSaturn5LogSpreadsheetId(serialNumber);
            LiveSheet saturn5IssuesSheet = db[saturn5SpreadsheetId][Repository.Saturn5Repository.Saturn5Log_Issues_SheetTitle];

            // Get issue report data 
            IList<object> issuesReportRangeData = this.ConstructNewIssuesData(timestamp, username, damaged, issueReport);

            // Append content of the saturn 5 issues sheet with constructed issues data.
            saturn5IssuesSheet.AppendRows(db, 1, new IList<object>[1] { issuesReportRangeData });

            // Append Saturns6UnsresolvedIssues sheet
            IList<object> saturns5UnresolvedIssuesRowData = this.ConstructSaturns5UnresolvedIssueRowData(timestamp, serialNumber, username, damaged, issueReport);
            this._saturns5UnresolvedIssuesSheet.AppendRows(db, 1, new IList<object>[1] { saturns5UnresolvedIssuesRowData });
        }
        #endregion
        #endregion

        #region IDisposable Support
        public void Dispose()
        {
            if (!(this._dataRepository.Saturn5Repository is null))
            {
                this.Saturn5Repository.Saturn5SpreadsheetLoaded -= this.OnSaturn5IssuesSheetLoaded;
                this.Saturn5Repository.Saturn5SpreadsheetAdded -= this.OnSaturn5IssuesSheetAdded;
                this.Saturn5Repository.Saturn5SpreadsheetReplaced -= this.OnSaturn5IssuesSheetReplaced;
                this.Saturn5Repository.Saturn5SpreadsheetArchived -= this.OnSaturn5IssuesSheetArchived;
                this.Saturn5Repository.Saturn5SpreadsheetUnarchived -= this.OnSaturn5IssuesSheetUnarchived;
                this.Saturn5Repository.Saturn5SpreadsheetRemoved -= this.OnSaturn5IssuesSheetRemoved;
            }

            this._issuesIndex = null;

            this._dashboardSpreadsheet?.Dispose();

            this._dashboardSpreadsheet = null;

            this._saturns5UnresolvedIssuesSheet = null;

            this._dataRepository = null;
        }
        #endregion
    }
}
