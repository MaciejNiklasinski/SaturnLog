using LiveGoogle.Sheets;
using LiveGoogle.Extensions;
using SaturnLog.Core;
using SaturnLog.Repository.Interfaces;
using SaturnLog.Repository.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SaturnLog.Repository
{
    public class Saturn5Repository : ISaturn5Repository, ISaturn5DB
    {
        #region Events
        public event EventHandler<Saturn5SpreadsheetLoadedEventArgs> Saturn5SpreadsheetLoaded;
        public event EventHandler<Saturn5SpreadsheetAddedEventArgs> Saturn5SpreadsheetAdded;
        public event EventHandler<Saturn5SpreadsheetReplacedEventArgs> Saturn5SpreadsheetReplaced;
        public event EventHandler<Saturn5SpreadsheetArchivedEventArgs> Saturn5SpreadsheetArchived;
        public event EventHandler<Saturn5SpreadsheetUnarchivedEventArgs> Saturn5SpreadsheetUnarchived;
        public event EventHandler<Saturn5SpreadsheetRemovedEventArgs> Saturn5SpreadsheetRemoved;
        #endregion

        #region EventsHandler 
        // Gets, and cache issues LiveSheet related with loaded staurn5.
        public void OnSaturn5SpreadsheetLoaded(object sender, Saturn5SpreadsheetLoadedEventArgs e)
        {
            // Get serial number associated with the saturn 5 spreadsheet which just have been loaded.
            string serialNumber = e.Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle][Saturn5Repository.Saturn5Log_Metadata_ValuesRowIndex]
                [Saturn5Repository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex].GetDataAsString();

            // Obtain current Saturn5Status of the unit associated with just loaded saturn 5 spreadsheet
            Saturn5Status status = e.Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle]
                    [Saturn5Repository.Saturn5Log_Metadata_StatusValue_RowIndex]
                    [Saturn5Repository.Saturn5Log_Metadata_StatusValue_ColumnIndex].GetDataAsString()
                    // and returns enum which value has been established based on the integer value parse from that string. (Invalid parse throws exception.)
                    .ValueToSaturn5Status();

            // If unit is in depot .. 
            if (status.IsInDepot())
                // ..do nothing and return.
                return;

            // Get saturn 5 lastSeenUsername from loaded spreadsheet.
            string lastSeenUsername = e.Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle][Saturn5Repository.Saturn5Log_Metadata_ValuesRowIndex]
                [Saturn5Repository.Saturn5Log_Metadata_LastSeenUsernameValue_ColumnIndex].GetDataAsString();

            // If lastSeenUsername represents invalid/unknown username ..
            if (!this._dataRepository.UserRepository.HasUsername(lastSeenUsername))
                // ..do nothing and return.
                return;

            // Get last user based on the provided lastSeenUsername
            User lastUser = this._dataRepository.UserRepository.Read(lastSeenUsername);

            // Get new with user status based on the 
            Saturn5Status newWithUserStatus = lastUser.Type.GetWithUserSaturn5Status();

            // If current status is already one obtained based on the UserType of the last user..
            if (status == newWithUserStatus)
                // ..do nothing and return.
                return;

            this.Update(serialNumber, null, status, null, null, null, null);

            // Commit changes into the Saturns5Dashboard
            this._dataRepository.Saturns5DashboardRepository.UpdateUserDetails(lastUser);
        }
        #endregion

        #region Const
        #region Saturns5DB
        //public const string Saturns5DB_SpreadsheetId = "1Q2fJXbhBnvHbMxmxDyd7_5CLZwarc1TH_SqiKdPDkyM";
        //public const string Saturns5DB_SpreadsheetId = "1mV9Iqgt38ohpTWLveMrJQuAIXHGrX1VZowEM_jlqj9s";
        internal readonly string Saturns5DB_SpreadsheetId;

        public const string Saturns5DB_SheetId = "Saturns5DB";

        public const int Saturns5DB_NewEntryColumnsCount = 3;
        public const int Saturns5DB_NewEntryRowsCount = 1;

        public const int Saturns5DB_SerialNumber_ColumnIndex = 0;
        public const int Saturns5DB_ShortId_ColumnIndex = 1;
        public const int Saturns5DB_SaturnSpreadsheetId_ColumnIndex = 2;
        #endregion

        #region Saturn5ArchiveDb
        public const string Saturns5ArchiveDB_SheetId = "Saturns5ArchiveDB";

        public const int Saturns5ArchiveDB_NewEntryColumnsCount = 6;
        public const int Saturns5ArchiveDB_NewEntryRowsCount = 1;
        public const int Saturns5ArchiveDB_SerialNumber_ColumnIndex = 0;
        public const int Saturns5ArchiveDB_ShortId_ColumnIndex = 1;
        public const int Saturns5ArchiveDB_SaturnSpreadsheetId_ColumnIndex = 2;
        public const int Saturns5ArchiveDB_ArchivisationReason_ColumnIndex = 3;
        public const int Saturns5ArchiveDB_ArchivisationTimestamp_ColumnIndex = 4;
        public const int Saturns5ArchiveDB_ArchivedByUsername_ColumnIndex = 5;
        #endregion

        #region Saturn5Log
        public const string Saturn5Log_Metadata_SheetTitle = "Metadata";

        public const int Saturn5Log_Metadata_RowsCount = 2;
        public const int Saturn5Log_Metadata_ColumnsCount = 9;
        public const int Saturn5Log_Metadata_LegendRowIndex = 0;
        public const int Saturn5Log_Metadata_ValuesRowIndex = 1;

        public const int Saturn5Log_Metadata_SerialNumberValue_ColumnIndex = 0;
        public const int Saturn5Log_Metadata_SerialNumberValue_RowIndex = 1;

        public const int Saturn5Log_Metadata_ShortIdValue_ColumnIndex = 1;
        public const int Saturn5Log_Metadata_ShortIdValue_RowIndex = 1;

        public const int Saturn5Log_Metadata_StatusValue_ColumnIndex = 2;
        public const int Saturn5Log_Metadata_StatusValue_RowIndex = 1;

        public const int Saturn5Log_Metadata_PhoneNumberValue_ColumnIndex = 3;
        public const int Saturn5Log_Metadata_PhoneNumberValue_RowIndex = 1;

        public const int Saturn5Log_Metadata_LastSeenUsernameValue_ColumnIndex = 4;
        public const int Saturn5Log_Metadata_LastSeenUsernameValue_RowIndex = 1;

        public const int Saturn5Log_Metadata_LastSeenDateValue_ColumnIndex = 5;
        public const int Saturn5Log_Metadata_LastSeenDateValue_RowIndex = 1;

        public const int Saturn5Log_Metadata_LastSeenTimeValue_ColumnIndex = 6;
        public const int Saturn5Log_Metadata_LastSeenTimeValue_RowIndex = 1;

        public const int Saturn5Log_Metadata_YearValue_ColumnIndex = 7;
        public const int Saturn5Log_Metadata_YearValue_RowIndex = 1;

        public const int Saturn5Log_Metadata_PreviousSpreadsheetIdValue_ColumnIndex = 8;
        public const int Saturn5Log_Metadata_PreviousSpreadsheetIdValue_RowIndex = 1;
        public readonly static string[] Saturn5Log_Metadata_InitialHeaderRowContent = new string[Saturn5Repository.Saturn5Log_Metadata_ColumnsCount]
        { "Serial Number:", "Short Id:", "Status:", "Phone Number:", "Last seen username:","Last seen date:", "Last seen time:", "Saturn5Log Spreadsheet Year", "Last year saturn 5 log spreadsheet id."};

        public const int Saturn5Log_LogsSheetColumnsCount = 2;
        public const int Saturn5Log_LogsSheetInitialRowsCount = 1;
        public const int Saturn5Log_LogsSheet_TimestampColumnIndex = 0;
        public const int Saturn5Log_LogsSheet_LogColumnIndex = 1;
        public const int Saturn5Log_LogsSheet_TimestampColumnWidth = 150;
        public const int Saturn5Log_LogsSheet_LogColumnWidth = 1500;
        public readonly static string[] Saturn5Log_LogsSheetInitialRowContent = new string[Saturn5Repository.Saturn5Log_LogsSheetColumnsCount]
        { "Timestamp:", "Log:" };

        public const string Saturn5Log_Issues_SheetTitle = "Issues";
        public const int Saturn5Log_Issues_RowsCount = 1;
        public const int Saturn5Log_Issues_ColumnsCount = 6;
        public readonly static string[] Saturn5Log_Issues_InitialHeaderRowContent = new string[Saturn5Repository.Saturn5Log_Issues_ColumnsCount]
        { "Timestamp:", "Issue reported by:", "Issue description:", "Resolved:", "Resolved how:", "Resolved by:" };

        public const int Saturn5Log_Issues_Timestamp_ColumnIndex = 0;
        public const int Saturn5Log_Issues_IssueReportedBy_ColumnIndex = 1;
        public const int Saturn5Log_Issues_IssueDescription_ColumnIndex = 2;
        public const int Saturn5Log_Issues_Resolved_ColumnIndex = 3;
        public const int Saturn5Log_Issues_ResolvedHow_ColumnIndex = 4;
        public const int Saturn5Log_Issues_ResolvedBy_ColumnIndex = 5;
        
        public const int Saturn5Log_Issues_ShortColumnWidth = 150;
        public const int Saturn5Log_Issues_LongColumnWidth = 800;
        public const int Saturn5Log_Issues_ResolvedColumnsColumnWidth = 300;
        #endregion
        #endregion

        #region Private fields
        // Data repository 
        private DataRepository _dataRepository;

        private LiveSheet _saturn5DBSheet;
        private LiveSheet _saturn5ArchiveDBSheet;

        // Key: serial number Value: row index in saturn5 db.
        private readonly IDictionary<string, int> _rowIndexesBySerialNumbers = new Dictionary<string, int>();

        // Key: short id Value: row index in saturn5 db.
        private readonly IDictionary<string, int> _rowIndexesByShortIds = new Dictionary<string, int>();
        #endregion

        #region ISaturn5DB - Properties
        // Saturn5 data consistency lock
        public object Saturns5DBLock { get; } = new object();

        // Key: serial number Value: row index in saturn5 db.
        public IDictionary<string, int> RowIndexesBySerialNumbers { get { return this._rowIndexesBySerialNumbers; } }

        // Key: short id Value: row index in saturn5 db.
        public IDictionary<string, int> RowIndexesByShortIds { get { return this._rowIndexesByShortIds; } }

        public LiveSheet DbSheet { get { return this._saturn5DBSheet; } }
        #endregion

        #region Constructor
        private Saturn5Repository(string spreadsheetId) { this.Saturns5DB_SpreadsheetId = spreadsheetId; }
        
        public static async Task<Saturn5Repository> GetAsync(DataRepository dataRepository, string spreadsheetId)
        {
            //
            Saturn5Repository saturn5Repository = new Saturn5Repository(spreadsheetId);

            // Assign provided data repository.
            saturn5Repository._dataRepository = dataRepository;

            // Get live spreadsheets database reference.
            LiveSpreadsheetsDb db = saturn5Repository._dataRepository.GoogleService.SpreadsheetsDb;

            // Load spreadsheet containing saturns db.
            await db.LoadSpreadsheetAsync(saturn5Repository.Saturns5DB_SpreadsheetId);

            // Get and assign reference to saturns 5 database sheet
            saturn5Repository._saturn5DBSheet = db[saturn5Repository.Saturns5DB_SpreadsheetId][Saturn5Repository.Saturns5DB_SheetId];
            saturn5Repository._saturn5ArchiveDBSheet = db[saturn5Repository.Saturns5DB_SpreadsheetId][Saturn5Repository.Saturns5ArchiveDB_SheetId];

            // Subscribe to appropriate events of Saturn 5 repository
            saturn5Repository.Saturn5SpreadsheetLoaded += saturn5Repository.OnSaturn5SpreadsheetLoaded;

            // Constructs serial number / short id relativity indexes.
            saturn5Repository.ReBuildSaturn5IdsRelativityIndexes();

            //
            return saturn5Repository;
        }
        #endregion

        #region Methods - Saturn5DataRepository
        public void ReObtainDBData()
        {
            // Saturns5DBLock thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Re-obtain users sheet data from google servers
                this._saturn5DBSheet.ReObtainData(db);

                // Clears and recreates serial number / short id - rowIndex Saturn5Repository associations dictionaries.
                this.ReBuildSaturn5IdsRelativityIndexes();
            }
        }

        //
        public async Task DBCorrectWithUserStatusAsync(CancellationToken token)
        {
            await Task.Run(() => 
            {
                lock (this.Saturns5DBLock)
                {
                    // Obtain all the saturn 5 units currently with user of any type WithUser <-> WithStarUser <-> WithManager
                    IEnumerable<Saturn5> allSaturns5WithUsers = this.ReadAll().Where((saturn5) => { return saturn5.Status.IsWithUser(); });

                    token.ThrowIfCancellationRequested();

                    // Obtain all the users
                    IList<User> allUsers = this._dataRepository.UserRepository.ReadAll();

                    token.ThrowIfCancellationRequested();

                    // Loop through all the known users..
                    foreach (User user in allUsers)
                        // .. then loop through all the saturns with this user if any.
                        foreach (Saturn5 saturn5 in allSaturns5WithUsers.Where((s5) => { return (s5.LastSeenUsername == user.Username); }))
                        {
                            // Get appropriate WitUser Saturn5Status associated with the type of the currently looped through user.
                            Saturn5Status withUserStatus = user.Type.GetWithUserSaturn5Status();

                            // if Saturn5 WithUser Saturn5Status does not represent appropriate type of user (WithUser <-> WithStarUser <-> WithManager)
                            if (saturn5.Status != withUserStatus)
                                // .. updated it current status based on the with user status obtained from currently looped through user type 
                                this.Update(saturn5.SerialNumber, null, withUserStatus, null, null, null, null);
                        }
                }
            }, token);
        }
        #endregion

        #region ISaturnsDB - Methods
        public async Task AssureAllLoadedAsync(CancellationToken? token = null)
        {
            // WARNING - DO NOT ENCAPSULATE IN THREAD SAFETY LOCK OR YOU WILL PREVENT 
            // OTHER THERDS FROM EDITING COMPLEATELY TILL ALL DATA HAVE BEEN ASSURED
            try
            {
                // Loop through all the serial numbers in the serial number - row index relativity index
                foreach (KeyValuePair<string, int> rowIndexeBySerialNumber in this._rowIndexesBySerialNumbers)
                {
                    // ... wait 100ms to allow other threads edit
                    // thread safety lock is applied most of the time within
                    // this.AssureSaturn5DataIsLoaded(serialNumber) method
                    await Task.Delay(10);
                    
                    // If cancellation has been requested throw OperationCanceledException
                    token?.ThrowIfCancellationRequested();

                    // Get currently looped through serial number
                    string serialNumber = rowIndexeBySerialNumber.Key;

                    // Try to assure that saturn5 data is loaded using currently looped through serial number...
                    try { await Task.Run(() => this.AssureSaturn5DataIsLoaded(serialNumber)); }
                    // ... and swallow exception if it has been specifically caused by serial number
                    // unrecognized in the Saturn5Repository. (That should happened only if saturn5 have been
                    // removed from other thread then current method.)
                    catch (ArgumentException ex) when (ex.ParamName == "serialNumber") { }
                }
            }
            // If this._rowIndexesBySerialNumbers has been modified by other thread,
            // retry assuring that all asturn5 spreadsheets are loaded.
            catch (InvalidOperationException ex) when (ex.Message.Contains("Collection was modified; enumeration operation may not execute"))
            {
                // ... wait extra 1000ms...
                await Task.Delay(1000);

                // ... and retry
                await this.AssureAllLoadedAsync(token);
            }
            // If operation canceled exception has been thrown on any level - re-throw it.
            catch (OperationCanceledException) { throw; }
        }

        public void AssureSaturn5DataIsLoaded(string serialNumber)
        {
            if (serialNumber is null) throw new ArgumentNullException(serialNumber);
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Saturns5DBLock thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get current saturn 5 spreadsheet id.
                string saturn5SpreadsheetId = this.GetSaturn5LogSpreadsheetId(serialNumber);

                this.AssureSaturn5DataIsLoadedFromSpecificSpreadsheet(db, saturn5SpreadsheetId);
            }
        }
        #endregion

        #region ISaturn5Repository - Methods

        #region Archive
        public void Archive(string serialNumber, string archivisationReason, string archivedByUsername)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (archivisationReason is null) throw new ArgumentNullException(nameof(archivisationReason));
            else if (this._saturn5DBSheet.RowCount == 1) throw new InvalidOperationException("Last saturn cannot be archived.");
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));
            else if (archivisationReason == "") throw new ArgumentException("Archivisation reason cannot be empty.");
            else if (!this._dataRepository.UserRepository.HasUsername(archivedByUsername)) throw new ArgumentException("Archivisation cannot be performed with invalid archivedByUsername.", nameof(archivedByUsername));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                string currentSpreadsheetId = this.GetSaturn5LogSpreadsheetId(serialNumber);

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get saturn 5 row associated with the provided serial number
                int saturn5AssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

                LiveRow saturn5DBRow = this._saturn5DBSheet[saturn5AssociatedRowIndex];

                IList<string> saturn5DBRowData = saturn5DBRow.GetDataAsStrings();

                IList<string> saturn5ArchiveDBRowData = new string[Saturns5ArchiveDB_NewEntryColumnsCount];

                saturn5ArchiveDBRowData[Saturn5Repository.Saturns5ArchiveDB_SerialNumber_ColumnIndex] = saturn5DBRowData[Saturn5Repository.Saturns5DB_SerialNumber_ColumnIndex];
                saturn5ArchiveDBRowData[Saturn5Repository.Saturns5ArchiveDB_ShortId_ColumnIndex] = saturn5DBRowData[Saturn5Repository.Saturns5DB_ShortId_ColumnIndex];
                saturn5ArchiveDBRowData[Saturn5Repository.Saturns5ArchiveDB_SaturnSpreadsheetId_ColumnIndex] = saturn5DBRowData[Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex];
                saturn5ArchiveDBRowData[Saturn5Repository.Saturns5ArchiveDB_ArchivisationReason_ColumnIndex] = archivisationReason;
                saturn5ArchiveDBRowData[Saturn5Repository.Saturns5ArchiveDB_ArchivisationTimestamp_ColumnIndex] = DateTime.Now.ToTimestamp();
                saturn5ArchiveDBRowData[Saturn5Repository.Saturns5ArchiveDB_ArchivedByUsername_ColumnIndex] = archivedByUsername;

                // 1) Append rows in the saturn5 archiveDB sheet
                this._saturn5ArchiveDBSheet.AppendRows(db, Saturn5Repository.Saturns5ArchiveDB_NewEntryRowsCount, new IList<string>[Saturn5Repository.Saturns5ArchiveDB_NewEntryRowsCount] { saturn5ArchiveDBRowData });

                // 2) Remove the row from saturn5 db sheet, representing saturn5 associated with the provided serial number.
                // (one google sheet service request)
                this.DeleteSaturn5RowFromSaturn5DbSheet(db, serialNumber);

                // 3) Rebuild serialNumber/shortId - row relativity index
                this.ReBuildSaturn5IdsRelativityIndexes();

                // 4) Unload archived spreadsheet to free up the memory and standardize behavior on creation/archivisation
                db.UnloadSpreadsheet(currentSpreadsheetId);

                // 5) Invokes appropriate event to let know all the subscribers that the saturn has been archived
                this.Saturn5SpreadsheetArchived.Invoke(this, new Saturn5SpreadsheetArchivedEventArgs(serialNumber));
            }
        }
        #endregion

        #region Create Or Unarchive
        public void Unarchive(string serialNumber, string shortId)
        {
            this.Unarchive(out string archivedSpreadsheetId, serialNumber, shortId);
        }

        public void Unarchive(out string archivedSpreadsheetId, string serialNumber, string shortId)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (shortId is null) throw new ArgumentNullException(nameof(shortId));
            else if (this.HasSaturn5ShortId(shortId)) throw new ArgumentException($"Provided short id: {shortId} is associated with existing saturn 5 in the Saturn5Repository. To unarchive saturn 5 provide new unique short id.", nameof(shortId));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // 1) Get and assign archived saturn live row and archived spreadsheet id.
                LiveRow archivedSaturn5Row = this.GetArchivedSaturn5Row(serialNumber);
                archivedSpreadsheetId = archivedSaturn5Row[Saturn5Repository.Saturns5ArchiveDB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();


                // 2) Create appropriate entry in the Saturn5Db live sheet using spreadsheet id obtained from archived saturn 5 row, combined with provided shortId.
                this.CreateNewSaturn5DbEntry(db, serialNumber, shortId, archivedSpreadsheetId);

                // 3) Removes appropriate row from _saturn5ArchiveDBSheet live sheer
                this._saturn5ArchiveDBSheet.RemoveRows(db, archivedSaturn5Row.RowIndex, Saturn5Repository.Saturns5ArchiveDB_NewEntryRowsCount);

                // 4) Load archived spreadsheet. And update short id stored in its metadata;
                db.LoadSpreadsheet(archivedSpreadsheetId);

                // TODO look into it happening anyway during latest call to this.Update
                LiveSheet archivedMetadataSheet = db[archivedSpreadsheetId][Saturn5Repository.Saturn5Log_Metadata_SheetTitle];
                LiveCell shortIdMetadataCell = archivedMetadataSheet[Saturn5Repository.Saturn5Log_Metadata_ShortIdValue_RowIndex][Saturn5Repository.Saturn5Log_Metadata_ShortIdValue_ColumnIndex];
                shortIdMetadataCell.SetData(shortId);
                shortIdMetadataCell.Upload(db);

                // 5) Invoke appropriate event to let subscribers know that the saturn5 has been unarchived.
                this.Saturn5SpreadsheetUnarchived?.Invoke(this, new Saturn5SpreadsheetUnarchivedEventArgs(db[archivedSpreadsheetId]));
            }
        }

        public void Create(string serialNumber, string shortId, Saturn5Status status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenUsername)
        {
            // Validate parameters
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (shortId is null) throw new ArgumentNullException(nameof(shortId));
            else if (lastSeenUsername is null) throw new ArgumentNullException(nameof(lastSeenUsername));
            else if (this.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Provided serial number: {serialNumber} is associated with existing saturn 5 in the Saturn5Repository.", nameof(serialNumber));
            else if (this.HasSaturn5ShortId(shortId)) throw new ArgumentException($"Provided short id: {shortId} is associated with existing saturn 5 in the Saturn5Repository.", nameof(shortId));
            else if (!this._dataRepository.UserRepository.HasUsername(lastSeenUsername)) throw new ArgumentException($"Provided lastSeenUsername {lastSeenUsername} is referring to unexisting user", nameof(lastSeenUsername));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                string spreadsheetId = null;

                if (!this.HasArchivedSaturn5(serialNumber))
                {
                    // 1a) Add new saturn5 log spreadsheet
                    this.CreateNewSaturn5LogSpreadsheet(out spreadsheetId, db, serialNumber, shortId, status, phoneNumber, lastSeenDate, lastSeenTime, lastSeenUsername);

                    // 2) Add Saturn 5 entry into the user db sheet.
                    this.CreateNewSaturn5DbEntry(db, serialNumber, shortId, spreadsheetId);

                    // 3) Get that spreadsheet ...
                    LiveSpreadsheet spreadsheet = db[spreadsheetId];

                    // ... and use it to build instance of necessary EventArgs class
                    // to trigger Saturn5SpreadsheetAdded event.
                    this.Saturn5SpreadsheetAdded?.Invoke(this, new Saturn5SpreadsheetAddedEventArgs(spreadsheet));
                }
                else
                {
                    // 1b) Add new saturn5 log spreadsheet
                    this.Unarchive(out spreadsheetId, serialNumber, shortId);

                    // 2) Update unarchive saturn 5 spreadsheet with provided creation parameters, this will also assure that appropriate unarchived saturn 5 is using appropriate (up to date)
                    // spreadsheet by calling private helper this.GetSaturn5LogSpreadsheet(LiveSpreadsheetsDb db, string saturn5SpreadsheetId, string serialNumber, int nowYear) down the stack Update method itself.
                    this.Update(serialNumber, null, status, phoneNumber, lastSeenDate, lastSeenTime, lastSeenUsername);
                }

            }
        }

        #region this.Create(string serialNumber, string shortId, string status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenWithUsername, string lastSeenWithFirstName, string lastSeenWithSurname)- Private Helpers
        private void CreateNewSaturn5LogSpreadsheet(out string spreadsheetId, LiveSpreadsheetsDb db, string serialNumber, string shortId, Saturn5Status status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenUsername)
        {
            // Get string representation of the Saturn5Status integer value.
            string saturn5StatusValue = status.ToValueString();

            // Get saturn 5 spreadsheet blueprint.
            IList<Tuple<string, int, int, IList<IList<string>>>> saturn5SpreadsheetBlueprint = this.GetSaturn5SpreadsheetBlueprint(out DateTime now, out string spraedsheetTitle, db, serialNumber, shortId, status, phoneNumber, lastSeenDate, lastSeenTime, lastSeenUsername);

            // Add new user  spreadsheet into the spreadsheet database.
            db.AddSpreadsheet(out spreadsheetId, spraedsheetTitle, saturn5SpreadsheetBlueprint);

            // Assures that newly created saturn5 log spreadsheet can be view by anyone.
            db.AssureSpreadsheetViewIsPublic(spreadsheetId);

            // Create columns ranges dimension adjustment index
            IList<Tuple<string, int, int, int>> columnsRangesDimensionsAdjustmentBlueprint = new List<Tuple<string, int, int, int>>();

            // Get just created saturn year-associated user spreadsheet shell.
            LiveSpreadsheet saturn5SpreadsheetShell = db[spreadsheetId];
            
            // Get current, just created saturn log sheet to adjust its columns width dimensions
            LiveSheet saturn5CurrentLogSheetShell = saturn5SpreadsheetShell[this.GetSaturn5LogCurrentSheetTitle(now.Month)];

            // Get current, just created saturn issues sheet to adjust its columns and width dimensions
            LiveSheet saturn5CurrentIssue = saturn5SpreadsheetShell[Saturn5Repository.Saturn5Log_Issues_SheetTitle];

            // Assure width of timestamp column.
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<string, int, int, int>(this.GetSaturn5LogCurrentSheetTitle(now.Month), Saturn5Repository.Saturn5Log_LogsSheet_TimestampColumnIndex, 1, Saturn5Repository.Saturn5Log_LogsSheet_TimestampColumnWidth));
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<string, int, int, int>(this.GetSaturn5LogCurrentSheetTitle(now.Month), Saturn5Repository.Saturn5Log_LogsSheet_LogColumnIndex, Saturn5Repository.Saturn5Log_Issues_ColumnsCount - 1, Saturn5Repository.Saturn5Log_LogsSheet_LogColumnWidth));

            // Adjust current 
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<string, int, int, int>(Saturn5Repository.Saturn5Log_Issues_SheetTitle, Saturn5Repository.Saturn5Log_Issues_Timestamp_ColumnIndex, 2, Saturn5Repository.Saturn5Log_Issues_ShortColumnWidth));
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<string, int, int, int>(Saturn5Repository.Saturn5Log_Issues_SheetTitle,Saturn5Repository.Saturn5Log_Issues_IssueDescription_ColumnIndex, 1, Saturn5Repository.Saturn5Log_Issues_LongColumnWidth));
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<string, int, int, int>(Saturn5Repository.Saturn5Log_Issues_SheetTitle,Saturn5Repository.Saturn5Log_Issues_Resolved_ColumnIndex, 1, Saturn5Repository.Saturn5Log_Issues_ShortColumnWidth));
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<string, int, int, int>(Saturn5Repository.Saturn5Log_Issues_SheetTitle, Saturn5Repository.Saturn5Log_Issues_ResolvedHow_ColumnIndex, 1, Saturn5Repository.Saturn5Log_Issues_LongColumnWidth));
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<string, int, int, int>(Saturn5Repository.Saturn5Log_Issues_SheetTitle, Saturn5Repository.Saturn5Log_Issues_ResolvedBy_ColumnIndex, 1, Saturn5Repository.Saturn5Log_Issues_ShortColumnWidth));

            saturn5SpreadsheetShell.AdjustMultipleSheetsColumnsRangesWidthDiemansions(db, columnsRangesDimensionsAdjustmentBlueprint);
        }
        
        private void CreateNewSaturn5DbEntry(LiveSpreadsheetsDb db, string serialNumber, string shortId, string spreadsheetId)
        {
            // Get index of the first unexisting row in the saturn 5 db sheet
            int newSaturn5RowIndex = this._saturn5DBSheet.RowCount;

            // Append users db sheet content with new user entry
            this._saturn5DBSheet.InsertRows(db, newSaturn5RowIndex, Saturn5Repository.Saturns5DB_NewEntryRowsCount, new IList<object>[Saturn5Repository.Saturns5DB_NewEntryRowsCount]
                { new object[Saturn5Repository.Saturns5DB_NewEntryColumnsCount] { serialNumber, shortId, spreadsheetId } });

            // Creates serial number / short id -rowIndex association in the repository for the newly created 
            this._rowIndexesBySerialNumbers.Add(serialNumber, newSaturn5RowIndex);
            this._rowIndexesByShortIds.Add(shortId, newSaturn5RowIndex);
        }
        #endregion
        #endregion

        #region Read
        public int Count()
        {
            return this._saturn5DBSheet?.RowCount ?? 0;
        }

        public bool HasArchivedSaturn5(string serialNumber)
        {
            // Loop through all the rows apart of the header row.
            for (int i = 1; i < this._saturn5ArchiveDBSheet.RowCount; i++)
            {
                LiveRow archivedSaturn5Row = this._saturn5ArchiveDBSheet[i];

                string archivedSerialNumber = archivedSaturn5Row[Saturn5Repository.Saturns5ArchiveDB_SerialNumber_ColumnIndex].GetDataAsString();

                if (serialNumber == archivedSerialNumber)
                    return true;
            }

            return false;
        }

        public bool HasSaturn5SerialNumber(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
                return this._rowIndexesBySerialNumbers.ContainsKey(serialNumber);
        }

        public bool HasSaturn5ShortId(string shortId)
        {
            // Parameters Validation
            if (shortId is null) throw new ArgumentNullException(nameof(shortId));

            // Thread safety lock
            lock (this.Saturns5DBLock)
                return this._rowIndexesByShortIds.ContainsKey(shortId);
        }
        
        public IEnumerable<string> GetAllSerialNumbers()
        {
            // UsersDBLock thread safety lock
            lock (this.Saturns5DBLock)
                return this._rowIndexesBySerialNumbers.Keys;
        }

        public IEnumerable<string> GetAllShortIds()
        {
            // UsersDBLock thread safety lock
            lock (this.Saturns5DBLock)
                return this._rowIndexesByShortIds.Keys;
        }

        public string GetSerialNumberFromShortId(string shortId)
        {
            // Parameters Validation
            if (shortId is null) throw new ArgumentNullException(nameof(shortId));
            else if (!this.HasSaturn5ShortId(shortId))
                throw new ArgumentException($"Provided saturn 5 short id {shortId} is not recognized by Saturn5Repository.", nameof(shortId));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get saturn 5 row index
                int shortIdAssociatedRowIndex = this._rowIndexesByShortIds[shortId];
                // ... and the saturn 5 row itself.
                LiveRow saturn5Row = this._saturn5DBSheet[shortIdAssociatedRowIndex];

                // Get cell containing saturn 5 serial number, obtain its value as string and return it.
                return saturn5Row[Saturn5Repository.Saturns5DB_SerialNumber_ColumnIndex].GetDataAsString();
            }
        }

        public string GetShortIdFromSerialNumber(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get saturn 5 row index
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];
                // ... and the saturn 5 row itself.
                LiveRow saturn5Row = this._saturn5DBSheet[serialNumberAssociatedRowIndex];

                // Get cell containing saturn 5 short id, obtain its value as string and return it.
                return saturn5Row[Saturn5Repository.Saturns5DB_ShortId_ColumnIndex].GetDataAsString();
            }
        }

        // Get phone number of the existing saturn5
        public string GetSaturn5PhoneNumber(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get row index related with the provided serial number
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

                // Obtain saturn 5 spreadsheet id associated with provided serial number. 
                string saturn5SpreadsheetId = this._saturn5DBSheet[serialNumberAssociatedRowIndex][Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get saturn 5 spreadsheet
                LiveSpreadsheet saturn5Spreadsheet = this.GetLastSaturn5LogSpreadsheet(db, saturn5SpreadsheetId, serialNumber);

                // Return data from the appropriate cell.
                return saturn5Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle]
                    [Saturn5Repository.Saturn5Log_Metadata_PhoneNumberValue_RowIndex]
                    [Saturn5Repository.Saturn5Log_Metadata_PhoneNumberValue_ColumnIndex].GetDataAsString();
            }
        }

        // Get Saturn5Status of the existing saturn5
        public Saturn5Status GetSaturn5Status(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get row index related with the provided serial number
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

                // Obtain saturn 5 spreadsheet id associated with provided serial number. 
                string saturn5SpreadsheetId = this._saturn5DBSheet[serialNumberAssociatedRowIndex][Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get saturn 5 spreadsheet
                LiveSpreadsheet saturn5Spreadsheet = this.GetLastSaturn5LogSpreadsheet(db, saturn5SpreadsheetId, serialNumber);

                // Obtain string representing appropriate integer/Saturn5Status value...
                return saturn5Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle]
                    [Saturn5Repository.Saturn5Log_Metadata_StatusValue_RowIndex]
                    [Saturn5Repository.Saturn5Log_Metadata_StatusValue_ColumnIndex].GetDataAsString()
                    // and returns enum which value has been established based on the integer value parse from that string. (Invalid parse throws exception.)
                    .ValueToSaturn5Status();
            }
        }

        // Get last seen username of the existing saturn5
        public string GetSaturn5LastSeenUsername(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get row index related with the provided serial number
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

                // Obtain saturn 5 spreadsheet id associated with provided serial number. 
                string saturn5SpreadsheetId = this._saturn5DBSheet[serialNumberAssociatedRowIndex][Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get saturn 5 spreadsheet
                LiveSpreadsheet saturn5Spreadsheet = this.GetLastSaturn5LogSpreadsheet(db, saturn5SpreadsheetId, serialNumber);

                // Return data from the appropriate cell.
                return saturn5Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle]
                    [Saturn5Repository.Saturn5Log_Metadata_LastSeenUsernameValue_RowIndex]
                    [Saturn5Repository.Saturn5Log_Metadata_LastSeenUsernameValue_ColumnIndex].GetDataAsString();
            }
        }

        // Get last seen date of the existing saturn5
        public string GetSaturn5LastSeenDate(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            {
                // Get row index related with the provided serial number
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

                // Obtain saturn 5 spreadsheet id associated with provided serial number. 
                string saturn5SpreadsheetId = this._saturn5DBSheet[serialNumberAssociatedRowIndex][Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get saturn 5 spreadsheet
                LiveSpreadsheet saturn5Spreadsheet = this.GetLastSaturn5LogSpreadsheet(db, saturn5SpreadsheetId, serialNumber);

                // Return data from the appropriate cell.
                return saturn5Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle]
                    [Saturn5Repository.Saturn5Log_Metadata_LastSeenDateValue_RowIndex]
                    [Saturn5Repository.Saturn5Log_Metadata_LastSeenDateValue_ColumnIndex].GetDataAsString();
            }
        }

        // Get plast seen time of the existing saturn5
        public string GetSaturn5LastSeenTime(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get row index related with the provided serial number
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

                // Obtain saturn 5 spreadsheet id associated with provided serial number. 
                string saturn5SpreadsheetId = this._saturn5DBSheet[serialNumberAssociatedRowIndex][Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get saturn 5 spreadsheet
                LiveSpreadsheet saturn5Spreadsheet = this.GetLastSaturn5LogSpreadsheet(db, saturn5SpreadsheetId, serialNumber);

                // Return data from the appropriate cell.
                return saturn5Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle]
                    [Saturn5Repository.Saturn5Log_Metadata_LastSeenTimeValue_RowIndex]
                    [Saturn5Repository.Saturn5Log_Metadata_LastSeenTimeValue_ColumnIndex].GetDataAsString();
            }
        }

        // Returns currently assigned saturn5 log spreadsheet id of the existing saturn5
        public string GetSaturn5LogSpreadsheetId(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get row index related with the provided serial number
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

                // Obtain string representing saturn spreadsheet id
                return this._saturn5DBSheet[serialNumberAssociatedRowIndex][Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();
            }
        }

        public Saturn5 Read(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get row index related with the provided serial number
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

                // Obtain saturn 5 spreadsheet id associated with provided serial number. 
                string saturn5SpreadsheetId = this._saturn5DBSheet[serialNumberAssociatedRowIndex][Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get saturn 5 spreadsheet
                LiveSpreadsheet saturn5Spreadsheet = this.GetLastSaturn5LogSpreadsheet(db, saturn5SpreadsheetId, serialNumber);

                // Get data stored on the row associated with appropriate row index.
                IList<string> saturn5RowData = saturn5Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle][Saturn5Repository.Saturn5Log_Metadata_ValuesRowIndex].GetDataAsStrings();

                // obtain current Saturn5Status enum
                Saturn5Status status = this.GetSaturn5Status(serialNumber);

                // Create and return new instance of Saturn5
                return new Saturn5(
                    // SerialNumber
                    saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex],
                    // ShortId
                    saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_ShortIdValue_ColumnIndex],
                    // Status
                    status,
                    // PhoneNumber
                    saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_PhoneNumberValue_ColumnIndex],
                    // LastSeenDate
                    saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_LastSeenDateValue_ColumnIndex],
                    // LastSeenTime                                      
                    saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_LastSeenTimeValue_ColumnIndex],
                    // LastSeenUsername                                  
                    saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_LastSeenUsernameValue_ColumnIndex]);
            }
        }

        public IList<Saturn5> ReadAll()
        {
            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Declare saturn placeholder list.
                IList<Saturn5> saturn5s = new List<Saturn5>();

                // For each serialNumber associated with row index...
                foreach (var rowIndexBySerialNumber in this._rowIndexesBySerialNumbers)
                {
                    // Obtain saturn 5 spreadsheet id associated with provided serial number. 
                    string saturn5SpreadsheetId = this._saturn5DBSheet[rowIndexBySerialNumber.Value][Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].GetDataAsString();

                    // Get saturn 5 spreadsheet
                    LiveSpreadsheet saturn5Spreadsheet = this.GetLastSaturn5LogSpreadsheet(db, saturn5SpreadsheetId, rowIndexBySerialNumber.Key);

                    // Get data stored on the row associated with appropriate row index.
                    IList<string> saturn5RowData = saturn5Spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle][Saturn5Repository.Saturn5Log_Metadata_ValuesRowIndex].GetDataAsStrings();

                    // obtain current Saturn5Status enum
                    Saturn5Status status = this.GetSaturn5Status(rowIndexBySerialNumber.Key);

                    // Create and add new instance of Saturn5
                    saturn5s.Add(new Saturn5(
                        // SerialNumber
                        saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex],
                        // ShortId
                        saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_ShortIdValue_ColumnIndex],
                        // Status
                        status,
                        // PhoneNumber
                        saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_PhoneNumberValue_ColumnIndex],
                        // LastSeenDate
                        saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_LastSeenDateValue_ColumnIndex],
                        // LastSeenTime                                      
                        saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_LastSeenTimeValue_ColumnIndex],
                        // LastSeenUsername                                  
                        saturn5RowData[Saturn5Repository.Saturn5Log_Metadata_LastSeenUsernameValue_ColumnIndex]));
                }

                return saturn5s;
            }
        }
        #endregion

        #region Update
        // Append current saturn5 log sheet with entry build based on the provided saturn5Log string.
        public void AddSaturn5Log(string serialNumber, string saturn5Log)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (saturn5Log is null) throw new ArgumentNullException(nameof(saturn5Log));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get user spreadsheet id
                string saturn5SpreadsheetId = this.GetSaturn5LogSpreadsheetId(serialNumber);

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Obtain current time
                DateTime now = DateTime.Now;

                // Get saturn year-associated user spreadsheet shell.
                LiveSpreadsheet saturn5SpreadsheetShell = this.GetSaturn5LogSpreadsheet(db, saturn5SpreadsheetId, serialNumber, now.Year);

                // Get current saturn log sheet to append the provided log to
                LiveSheet saturn5CurrentLogSheetShell = this.GetSaturn5LogSheet(db, saturn5SpreadsheetShell, now.Month, now.ToTimestamp());

                // Create saturn5 log row data.
                IList<IList<object>> saturn5LogRowData = new IList<object>[1] { new string[Saturn5Repository.Saturn5Log_LogsSheetColumnsCount] { now.ToTimestamp(), saturn5Log } };

                // Append saturn5 individual log sheet with new row, representing provided saturn5Log. 
                saturn5CurrentLogSheetShell.AppendRows(db, 1, saturn5LogRowData);

                // After saturn5 log sheet data has been committed into the google service
                // clear the data from the LiveSheet as they are not needed, and over time
                // when combined the amount of memory needed to store 
                // all the logs from all the saturn5 would be to much.
                saturn5CurrentLogSheetShell[saturn5CurrentLogSheetShell.BottomIndex].ClearData();
            }
        }

        public void Update(string serialNumber, string shortId = null, Saturn5Status? status = null, string phoneNumber = null, string lastSeenDate = null, string lastSeenTime = null, string lastSeenUsername = null)
        {
            // Parameters validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // If short id has been provided for update...
                if (!(shortId is null)
                // ... and its value is different then the current one...
                && shortId != this.GetShortIdFromSerialNumber(serialNumber)
                // ... but its the same as short id of other saturn 5, throw appropriate exception.
                && this.HasSaturn5ShortId(shortId))
                    throw new ArgumentException($"Unable to change saturn 5 short id to {shortId} as this saturn 5 short id is already associated with other saturn.", nameof(shortId));

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // 1) Update saturn5 data in the Saturn5DB
                this.UpdateSaturn5InSaturnDBSheet(db, serialNumber, shortId);

                // 2) Update that data in the saturn log spreadsheet metadata sheet.
                this.UpdateSaturn5LogMetadataForSaturn5Update(db, serialNumber, shortId, status, phoneNumber, lastSeenDate, lastSeenTime, lastSeenUsername);
            }
        }

        #region this.Update(string serialNumber, string shortId = null, Saturn5Status? status = null, string phoneNumber = null, string lastSeenDate = null, string lastSeenTime = null, string lastSeenUsername = null) - Private Helpers
        // 1) Update saturn5 data in the Saturn5DB
        private void UpdateSaturn5InSaturnDBSheet(LiveSpreadsheetsDb db, string serialNumber, string shortId)
        {
            // If new short id hasn't been provided, return as no changes in database will be committed.
            if (shortId is null) return;

            // ...get current short id form provided serial number
            string currentShortId = this.GetShortIdFromSerialNumber(serialNumber);

            // If new short id is equal with the one currently stored in the saturn5 db, return as no changes in database will be committed.
            if (shortId == currentShortId)
                return;

            // Replace shortId-saturn5DBRowIndex relativity entry.
            this._rowIndexesByShortIds.Remove(currentShortId);
            int rowIndex = this._rowIndexesBySerialNumbers[serialNumber];
            this._rowIndexesByShortIds.Add(shortId, rowIndex);

            // Get LiveRow from the LiveSheet representing saturn db.
            int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];
            LiveRow saturn5Row = this._saturn5DBSheet[serialNumberAssociatedRowIndex];

            // Build update saturn5 row data
            IList<string> saturn5RowNewData = new string[Saturn5Repository.Saturns5DB_NewEntryColumnsCount];
            saturn5RowNewData[Saturn5Repository.Saturns5DB_SerialNumber_ColumnIndex] = null;
            saturn5RowNewData[Saturn5Repository.Saturns5DB_ShortId_ColumnIndex] = shortId;
            saturn5RowNewData[Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex] = null;

            // Upload saturn5 row with constructed/updated data.
            saturn5Row.SetDataFromStringsData(0, saturn5RowNewData);
            // ... and update data on the google spreadsheet.
            saturn5Row.Upload(db);
        }

        // 2) Update that data in the saturn log spreadsheet metadata sheet.
        private void UpdateSaturn5LogMetadataForSaturn5Update(LiveSpreadsheetsDb db, string serialNumber, string shortId, Saturn5Status? status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenUsername)
        {
            string saturn5logSpreadsheetId = this.GetSaturn5LogSpreadsheetId(serialNumber);

            // Get current saturn5 log spreadsheet.
            LiveSpreadsheet currentSaturn5LogSpreadsheet = this.GetSaturn5LogSpreadsheet(db, saturn5logSpreadsheetId, serialNumber, DateTime.Now.Year);

            // Get current saturn5 log spreadsheet.
            LiveSheet currentSaturn5LogMetadataSheet = currentSaturn5LogSpreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle];

            // Build update saturn5 metadata row data
            IList<string> metadataValuesRowNewData = new string[Saturn5Repository.Saturn5Log_Metadata_ColumnsCount];
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_SerialNumberValue_ColumnIndex] = null;
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_PhoneNumberValue_ColumnIndex] = phoneNumber;
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_ShortIdValue_ColumnIndex] = shortId;
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_StatusValue_ColumnIndex] = status?.ToValueString();
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_LastSeenDateValue_ColumnIndex] = lastSeenDate;
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_LastSeenTimeValue_ColumnIndex] = lastSeenTime;
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_LastSeenUsernameValue_ColumnIndex] = lastSeenUsername;
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_YearValue_ColumnIndex] = null;
            metadataValuesRowNewData[Saturn5Repository.Saturn5Log_Metadata_PreviousSpreadsheetIdValue_ColumnIndex] = null;

            // Set metadata sheet values row cells values and upload the changes into the google servers.
            LiveRow metadataValuesRow = currentSaturn5LogMetadataSheet[Saturn5Repository.Saturn5Log_Metadata_ValuesRowIndex];
            metadataValuesRow.SetDataFromStringsData(0, metadataValuesRowNewData);
            metadataValuesRow.Upload(db);
        }
        #endregion
        #endregion

        #region Delete
        public void Delete(string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            else if (this._saturn5DBSheet.RowCount == 1) throw new InvalidOperationException("Last saturn cannot be removed.");
            else if (!this.HasSaturn5SerialNumber(serialNumber))
                throw new ArgumentException($"Provided saturn 5 serial number: {serialNumber} is not recognized by Saturn5Repository.", nameof(serialNumber));

            // Thread safety lock
            lock (this.Saturns5DBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // 1) Remove all the saturn log spreadsheet associated with the provided serial number.
                // (one google sheet service request per saturn log spreadsheet associated with the provided serial number)
                this.DeleteSaturn5LogSpreadsheets(db, serialNumber);

                // 2) Remove the row from saturn5 db sheet, representing saturn5 associated with the provided serial number.
                // (one google sheet service request)
                this.DeleteSaturn5RowFromSaturn5DbSheet(db, serialNumber);

                // 3) Rebuild serialNumber - row relativity index
                this.ReBuildSaturn5IdsRelativityIndexes();

                // 4) Execute appropriate event to let all the subscribers know that Saturn5 associated with the specified serial number has been removed.
                this.Saturn5SpreadsheetRemoved?.Invoke(this, new Saturn5SpreadsheetRemovedEventArgs(serialNumber));
            }
        }

        #region this.Delete(string serialNumber) - private helpers
        // 1) Remove all the saturn log spreadsheet associated with the provided serial number.
        private void DeleteSaturn5LogSpreadsheets(LiveSpreadsheetsDb db, string serialNumber)
        {
            // Get all the saturn5 related log spreadsheets and remove them.
            IList<string> saturn5logSpreadsheetsIds = this.GetSaturn5CurrentLogSpreadsheetsIds(db, serialNumber);

            // Remove all saturn5 log spreadsheets.
            foreach (string saturn5logSpreadsheetId in saturn5logSpreadsheetsIds)
                if (!(saturn5logSpreadsheetId is null) && saturn5logSpreadsheetId != "")
                    // Remove and dispose saturn5 associated with currently looped through saturn5logSpreadsheetId.
                    db.RemoveSpreadsheet(saturn5logSpreadsheetId);
        }

        // 2) Remove the row from saturn5 db sheet, representing saturn5 associated with the provided serial number.
        private void DeleteSaturn5RowFromSaturn5DbSheet(LiveSpreadsheetsDb db, string serialNumber)
        {
            // Get saturn 5 row associated with the provided serial number
            int saturn5AssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];

            // Remove entry 
            this._saturn5DBSheet.RemoveRows(db, saturn5AssociatedRowIndex, 1);
        }
        #endregion
        #endregion
        #endregion

        #region Private Helpers
        private LiveRow GetArchivedSaturn5Row(string serialNumber)
        {
            // Loop through all the rows apart of the header row.
            for (int i = 1; i < this._saturn5ArchiveDBSheet.RowCount; i++)
            {
                LiveRow archivedSaturn5Row = this._saturn5ArchiveDBSheet[i];

                string archivedSerialNumber = archivedSaturn5Row[Saturn5Repository.Saturns5ArchiveDB_SerialNumber_ColumnIndex].GetDataAsString();

                if (serialNumber == archivedSerialNumber)
                    return archivedSaturn5Row;
            }

            throw new ArgumentException("Provided serial number is not associated with any of the archived saturn 5 units.", nameof(serialNumber));
        }

        // Recreates from scratch table of relativity between saturn5 serial number
        private void ReBuildSaturn5IdsRelativityIndexes()
        {
            // Clear existing relativity table.
            this._rowIndexesBySerialNumbers.Clear();
            this._rowIndexesByShortIds.Clear();

            // Loop through each of the rows in the LiveSheet containing saturn 5 database.
            for (int i = 0; i < this._saturn5DBSheet.RowCount; i++)
            {
                // Get saturn 5 database row appropriate for the currently looped row through index.
                LiveRow saturn5DbRow = this._saturn5DBSheet[i];

                // Obtain saturn 5 serial number and short id.
                string serialNumber = saturn5DbRow[Saturn5Repository.Saturns5DB_SerialNumber_ColumnIndex].GetDataAsString();
                string shortId = saturn5DbRow[Saturn5Repository.Saturns5DB_ShortId_ColumnIndex].GetDataAsString();

                // Add serial number & short id association with currently lopped through row index.
                this._rowIndexesBySerialNumbers.Add(serialNumber, i);
                this._rowIndexesByShortIds.Add(shortId, i);
            }
        }

        private void AssureSaturn5DataIsLoadedFromSpecificSpreadsheet(LiveSpreadsheetsDb db, string saturn5SpreadsheetId)
        {
            // Check whether database has at least shell of appropriate spreadsheet and if not...
            if (!db.HasSpreadsheetShell(saturn5SpreadsheetId))
            {
                // .. load spreadsheet partially - metadata and issues - ...
                db.LoadSpreadsheetPartially(saturn5SpreadsheetId, new string[2] { Saturn5Repository.Saturn5Log_Metadata_SheetTitle, Saturn5Repository.Saturn5Log_Issues_SheetTitle });

                // ... get that spreadsheet ...
                LiveSpreadsheet spreadsheet = db[saturn5SpreadsheetId];

                // ... and use it to build instance of necessary EventArgs class
                // to trigger Saturn5SpreadsheetLoaded event.
                this.Saturn5SpreadsheetLoaded?.Invoke(this, new Saturn5SpreadsheetLoadedEventArgs(spreadsheet));
            }
        }
        
        // Returns last known saturn5 year-associated spreadsheet id.
        private LiveSpreadsheet GetLastSaturn5LogSpreadsheet(LiveSpreadsheetsDb db, string saturn5SpreadsheetId, string serialNumber)
        {
            // Assure that current saturn5 spreadsheet is loaded.
            this.AssureSaturn5DataIsLoaded(serialNumber);

            return db.LoadedSpreadsheets[saturn5SpreadsheetId];
        }

        // Returns saturn5 year-associated spreadsheet id.
        private LiveSpreadsheet GetSaturn5LogSpreadsheet(LiveSpreadsheetsDb db, string saturn5SpreadsheetId, string serialNumber, int nowYear)
        {
            // Assure that current saturn5 spreadsheet is loaded.
            this.AssureSaturn5DataIsLoaded(serialNumber);

            // Get saturn 5 spreadsheet shell
            LiveSpreadsheet saturn5SpreadsheetShell = db.LoadedSpreadsheets[saturn5SpreadsheetId];

            LiveCell saturn5logYearCell = saturn5SpreadsheetShell[Saturn5Repository.Saturn5Log_Metadata_SheetTitle]
                [Saturn5Repository.Saturn5Log_Metadata_YearValue_RowIndex]
                [Saturn5Repository.Saturn5Log_Metadata_YearValue_ColumnIndex];

            if (!saturn5logYearCell.HasValue())
                saturn5logYearCell.ReObtainData(db);

            // If current year is not equal with content of Year-Value cell in user log spreadsheet, metadata sheet, replace it with new sheet.
            if (nowYear.ToString("D4") != saturn5logYearCell.GetDataAsString())
            {
                // Get row index related with the provided serialNumber
                int serialNumberAssociatedRowIndex = this._rowIndexesBySerialNumbers[serialNumber];
                LiveRow userRow = this._saturn5DBSheet[serialNumberAssociatedRowIndex];

                // Get current Saturn5State
                string phoneNumber = this.GetSaturn5PhoneNumber(serialNumber);
                Saturn5Status currentState = this.GetSaturn5Status(serialNumber);
                string lastSeenDate = this.GetSaturn5LastSeenDate(serialNumber);
                string lastSeenTime = this.GetSaturn5LastSeenTime(serialNumber);
                string lastSeenUsername = this.GetSaturn5LastSeenUsername(serialNumber);

                // Obtain user new spreadsheet blueprint...
                IList<Tuple<string, int, int, IList<IList<string>>>> newSaturn5SpreadsheetBlueprint
                    = this.GetSaturn5SpreadsheetBlueprint(
                    // (Blueprint timestamp - out parameter)
                    out DateTime now,
                    // ... obtain new user log spreadsheet title... (out parameter)
                    out string newSaturn5SpreadsheetTitle,
                    // (LiveSpreadsheetsDb instance)
                    db,
                    // Assign saturn5 serial number
                    userRow[Saturn5Repository.Saturns5DB_SerialNumber_ColumnIndex].GetDataAsString(),
                    // assigns saturn5 short id
                    userRow[Saturn5Repository.Saturns5DB_ShortId_ColumnIndex].GetDataAsString(),
                    // assigns saturn5 status
                    currentState,
                    // assigns saturn5 phone number
                    phoneNumber,
                    // assigns saturn5 last seen date
                    lastSeenDate,
                    // assigns saturn5 last seen time
                    lastSeenTime,
                    // assigns saturn5 last seen username
                    lastSeenUsername,
                    // assign spreadsheet id of the currently used and outdated 
                    // saturn5 spreadsheet log as a "Last year saturn 5 log spreadsheet id"
                    saturn5SpreadsheetShell.SpreadsheetId);

                // Add new user spreadsheet into the spreadsheet database.
                db.AddSpreadsheet(out string newSaturn5SpreadsheetId, newSaturn5SpreadsheetTitle, newSaturn5SpreadsheetBlueprint);

                // Assures that newly created saturn5 log spreadsheet can be view by anyone.
                db.AssureSpreadsheetViewIsPublic(newSaturn5SpreadsheetId);

                // Create columns ranges dimension adjustment index
                IList<Tuple<int, int, int>> columnsRangesDimensionsAdjustmentBlueprint = new List<Tuple<int, int, int>>();
                
                // Adjust current 
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(Saturn5Repository.Saturn5Log_Issues_Timestamp_ColumnIndex, 2, Saturn5Repository.Saturn5Log_Issues_ShortColumnWidth));
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(Saturn5Repository.Saturn5Log_Issues_IssueDescription_ColumnIndex, 1, Saturn5Repository.Saturn5Log_Issues_LongColumnWidth));
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(Saturn5Repository.Saturn5Log_Issues_Resolved_ColumnIndex, 1, Saturn5Repository.Saturn5Log_Issues_ShortColumnWidth));
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(Saturn5Repository.Saturn5Log_Issues_ResolvedHow_ColumnIndex, 1, Saturn5Repository.Saturn5Log_Issues_LongColumnWidth));
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(Saturn5Repository.Saturn5Log_Issues_ResolvedBy_ColumnIndex, 1, Saturn5Repository.Saturn5Log_Issues_ShortColumnWidth));
                LiveSheet saturn5CurrentIssue = saturn5SpreadsheetShell[Saturn5Repository.Saturn5Log_Issues_SheetTitle];
                saturn5CurrentIssue.AdjustMultipleColumnsRangesWidthDiemansions(db, columnsRangesDimensionsAdjustmentBlueprint);

                // Replace saturn-related saturn log spreadsheet id with new one.
                this.SetSaturn5DBSaturnSpreadsheetId(db, userRow, newSaturn5SpreadsheetId);

                // assign newly created spreadsheet into saturn 5 spreadsheet shell to be returned by the method.
                saturn5SpreadsheetShell = db[newSaturn5SpreadsheetId];

                // Invoke appropriate event to let all the subscribers know that the personal Saturn5 Spreadsheet has been replaced.
                this.Saturn5SpreadsheetReplaced?.Invoke(this, new Saturn5SpreadsheetReplacedEventArgs(saturn5SpreadsheetShell));
            }

            // returns saturn 5 spreadsheet shell.
            return saturn5SpreadsheetShell;
        }

        // Returns month-associated saturn5 log sheet.
        private LiveSheet GetSaturn5LogSheet(LiveSpreadsheetsDb db, LiveSpreadsheet saturn5SpreadsheetShell, int nowMonth, string timestamp)
        {
            // Get saturn5 log sheet title id - mount dependent.
            string currentSaturn5LogSheetTitleId = this.GetSaturn5LogCurrentSheetTitle(nowMonth);

            // If current saturn5 log spreadsheet doesn't contain sheet with appropriate title...
            if (!saturn5SpreadsheetShell.ContainsKey(currentSaturn5LogSheetTitleId))
            {
                // ... add such a sheet.
                saturn5SpreadsheetShell.AddSheet<IList<string>, string>(db, currentSaturn5LogSheetTitleId,
                    Saturn5Repository.Saturn5Log_LogsSheetColumnsCount, 1,
                    new IList<string>[1] { Saturn5Repository.Saturn5Log_LogsSheetInitialRowContent });
                
                // Create columns ranges dimension adjustment index
                IList<Tuple<int, int, int>> columnsRangesDimensionsAdjustmentBlueprint = new List<Tuple<int, int, int>>();

                // Create and add adjust issues sheet columns dimensions blueprints
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(Saturn5Repository.Saturn5Log_LogsSheet_TimestampColumnIndex, 1, Saturn5Repository.Saturn5Log_LogsSheet_TimestampColumnWidth));
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(Saturn5Repository.Saturn5Log_LogsSheet_LogColumnIndex, 1, Saturn5Repository.Saturn5Log_LogsSheet_LogColumnWidth));                
                LiveSheet saturn5CurrentLogSheetShell = saturn5SpreadsheetShell[currentSaturn5LogSheetTitleId];
                saturn5CurrentLogSheetShell.AdjustMultipleColumnsRangesWidthDiemansions(db, columnsRangesDimensionsAdjustmentBlueprint);
            }

            // Get current user log sheet to append the provided log to
            return saturn5SpreadsheetShell[currentSaturn5LogSheetTitleId];
        }

        // Replace saturn-related saturn log spreadsheet id with new one. Provided saturnRow
        // must represent saturn-related Saturn5DB sheet row.
        private void SetSaturn5DBSaturnSpreadsheetId(LiveSpreadsheetsDb db, LiveRow saturnRow, string newSaturnSpreadsheetId)
        {
            // Set data of the appropriate cell...
            saturnRow[Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].SetData(newSaturnSpreadsheetId);

            // .. and update cell data into the google servers.
            saturnRow[Saturn5Repository.Saturns5DB_SaturnSpreadsheetId_ColumnIndex].Upload(db);
        }

        private string GetSaturn5LogCurrentSpreadsheetTitle(string serialNumber, DateTime now)
        {
            // Return individual spreadsheet title
            return $"Saturn5Log{now.Year}_{serialNumber}";
        }

        private string GetSaturn5LogCurrentSheetTitle(int month)
        {
            // Return individual sheet title
            return $"Log_{month:D2}";
        }

        private IList<string> GetSaturn5CurrentLogSpreadsheetsIds(LiveSpreadsheetsDb db, string serialNumber)
        {
            // Parameters Validation
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            if (!this.HasSaturn5SerialNumber(serialNumber)) throw new ArgumentException($"Specified saturn 5 SN: {serialNumber} is not recognized.", nameof(serialNumber));

            // Declare new list containing current saturn5 log spreadsheet id.
            IList<string> saturn5logSpreadsheetsIds = new List<string>(new string[1] { this.GetSaturn5LogSpreadsheetId(serialNumber) });

            // Declare string variable designated to store the value of the previous saturn5 log spreadsheet id.
            string previousSpreadsheetId = null;

            // Loop through till no more previous saturn5 log spreadsheet id can be found.
            do
            {
                // Get spreadsheet id representing spreadsheet containing saturn5 log from the year
                // prior to the year of the saturn5 log which is last member of the saturn5logSpreadsheetsIds list.
                previousSpreadsheetId = this.GetSaturn5LogPreviousSpreadsheetId(db, saturn5logSpreadsheetsIds.Last());

                // If currently looped through spreadsheet doest contain spreadsheet id
                // of its previous saturn5 log spreadsheet id, return spreadsheets ids list.
                if (previousSpreadsheetId is null)
                    return saturn5logSpreadsheetsIds;
                // Otherwise add obtained saturn5 log previous spreadsheet id to spreadheetsIds list.
                else
                    saturn5logSpreadsheetsIds.Add(previousSpreadsheetId);
            }
            while (!(previousSpreadsheetId is null));

            return saturn5logSpreadsheetsIds;
        }

        private string GetSaturn5LogPreviousSpreadsheetId(LiveSpreadsheetsDb db, string spreadsheetId)
        {
            // Parameters validation
            if (spreadsheetId is null) throw new ArgumentNullException(nameof(spreadsheetId));

            // If spreadsheet associated with provided spreadsheet id is not recognized return null.
            if (!db.HasSpreadsheetId(spreadsheetId))
                return null;
            // If spreadsheet id is recognized, but spreadsheet, or at least it's shell is not even loaded...
            else if (!db.HasSpreadsheetShell(spreadsheetId))
                // ... load saturn5 log spreadsheet shell associated with provided spreadsheet id.
                db.LoadSpreadsheetShell(spreadsheetId);

            // Get saturn5 log spreadsheet (or its shell) associated with provided spreadsheet id.
            LiveSpreadsheet spreadsheet = db[spreadsheetId];

            // Get metadata sheet.
            LiveSheet metadataSheet = spreadsheet[Saturn5Repository.Saturn5Log_Metadata_SheetTitle];

            // Obtain cell containing spreadsheet id of the saturn5 log for the year prior to the year of the saturn5 log associated with the provided spreadsheet id...
            LiveCell previousSpreadsheetIdCell = metadataSheet[Saturn5Repository.Saturn5Log_Metadata_PreviousSpreadsheetIdValue_RowIndex][Saturn5Repository.Saturn5Log_Metadata_PreviousSpreadsheetIdValue_ColumnIndex];
            // .. and check whether it holds any value (it will not hold any value if only shell of the spreadsheet has been loaded),
            // if obtained instance of previousSpreadsheetIdCell doesn't hold any value...
            if (!previousSpreadsheetIdCell.HasValue())
                // Obtain data from the google servers.
                previousSpreadsheetIdCell.ReObtainData(db);

            // Return spreadsheet id of the previous saturn5 log spreadsheet.
            // (This will return null if the provided spreadsheet id 
            // representing spreadsheet containing saturn5 original
            // saturn5 log spreadsheet created on the saturn5 addition.)
            return previousSpreadsheetIdCell.GetDataAsString();
        }

        #region Build saturn5 log spreadsheet blueprint
        private IList<Tuple<string, int, int, IList<IList<string>>>> GetSaturn5SpreadsheetBlueprint(out DateTime now, out string spreadsheetTitle, LiveSpreadsheetsDb db, string serialNumber, string shortId, Saturn5Status status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenUsername, string oldSaturn5SpreadsheetId = null)
        {
            // Get current DateTime
            now = DateTime.Now;

            // Assign individual spreadsheet title
            spreadsheetTitle = this.GetSaturn5LogCurrentSpreadsheetTitle(serialNumber, now);

            // Declares sheets blueprints placeholder
            List<Tuple<string, int, int, IList<IList<string>>>> spreadsheetBlueprint = new List<Tuple<string, int, int, IList<IList<string>>>>();

            // Add Saturn 5 log metadata sheet blueprint into the spreadsheet blueprint.
            spreadsheetBlueprint.Add(this.GetSaturn5LogMetadataSheetBlueprint(serialNumber, shortId, status, phoneNumber, lastSeenDate, lastSeenTime, lastSeenUsername, now, oldSaturn5SpreadsheetId));

            // Add Saturn 5 log issues sheet blueprint into the spreadsheet blueprint.
            spreadsheetBlueprint.Add(this.GetSaturn5LogIssuesSheetBlueprint(db, oldSaturn5SpreadsheetId));

            // Add Saturn 5 log current sheet blueprint into the spreadsheet blueprint.
            spreadsheetBlueprint.Add(this.GetSaturn5LogCurrentSheetBlueprint(serialNumber, shortId, now));

            // Return spreadsheet blueprint
            return spreadsheetBlueprint;
        }

        private Tuple<string, int, int, IList<IList<string>>> GetSaturn5LogMetadataSheetBlueprint(string serialNumber, string shortId, Saturn5Status status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenUsername, DateTime now, string oldSaturn5SpreadsheetId)
        {
            IList<IList<string>> metadataSheetData = new IList<string>[Saturn5Repository.Saturn5Log_Metadata_RowsCount]
            {
                Saturn5Repository.Saturn5Log_Metadata_InitialHeaderRowContent,
                new string[Saturn5Repository.Saturn5Log_Metadata_ColumnsCount] { serialNumber, shortId, status.ToValueString(), phoneNumber, lastSeenUsername, lastSeenDate, lastSeenTime, now.Year.ToString(), oldSaturn5SpreadsheetId }
            };

            return new Tuple<string, int, int, IList<IList<string>>>(Saturn5Repository.Saturn5Log_Metadata_SheetTitle, Saturn5Repository.Saturn5Log_Metadata_ColumnsCount, Saturn5Repository.Saturn5Log_Metadata_RowsCount, metadataSheetData);
        }

        private Tuple<string, int, int, IList<IList<string>>> GetSaturn5LogCurrentSheetBlueprint(string serialNumber, string shortId, DateTime now)
        {
            IList<IList<string>> logSheetData = new IList<string>[Saturn5Repository.Saturn5Log_LogsSheetInitialRowsCount]
            {
                Saturn5Repository.Saturn5Log_LogsSheetInitialRowContent
            };

            return new Tuple<string, int, int, IList<IList<string>>>(this.GetSaturn5LogCurrentSheetTitle(now.Month), Saturn5Repository.Saturn5Log_LogsSheetColumnsCount, Saturn5Repository.Saturn5Log_LogsSheetInitialRowsCount, logSheetData);
        }

        private Tuple<string, int, int, IList<IList<string>>> GetSaturn5LogIssuesSheetBlueprint(LiveSpreadsheetsDb db, string oldSpreadsheetId)
        {
            IList<IList<string>> issuesSheetData = null;

            if (oldSpreadsheetId is null)
            {
                issuesSheetData = new IList<string>[Saturn5Repository.Saturn5Log_Issues_RowsCount]
                { Saturn5Repository.Saturn5Log_Issues_InitialHeaderRowContent };


                return new Tuple<string, int, int, IList<IList<string>>>(Saturn5Repository.Saturn5Log_Issues_SheetTitle, Saturn5Repository.Saturn5Log_Issues_ColumnsCount, Saturn5Repository.Saturn5Log_Issues_RowsCount, issuesSheetData);
            }
            else
            {
                this.AssureSaturn5DataIsLoadedFromSpecificSpreadsheet(db, oldSpreadsheetId);

                LiveSheet oldSpreadsheetIssuesSheet = db[oldSpreadsheetId][Saturn5Repository.Saturn5Log_Issues_SheetTitle];

                issuesSheetData = oldSpreadsheetIssuesSheet.GetDataAsStringsGrid();

                return new Tuple<string, int, int, IList<IList<string>>>(Saturn5Repository.Saturn5Log_Issues_SheetTitle, Saturn5Repository.Saturn5Log_Issues_ColumnsCount, oldSpreadsheetIssuesSheet.RowCount, issuesSheetData);
            }
        }
        #endregion
        #endregion

        #region IDisposable Support
        public void Dispose()
        {
            if (!(this._dataRepository.Saturn5IssuesRepository is null))
                this._dataRepository.Saturn5IssuesRepository.Dispose();
            
            // Subscribe to appropriate events of Saturn 5 repository
            this.Saturn5SpreadsheetLoaded -= this.OnSaturn5SpreadsheetLoaded;

            this._saturn5DBSheet.Dispose();
            this._saturn5ArchiveDBSheet.Dispose();
        }
        #endregion
    }
}
