using LiveGoogle.Sheets;
using SaturnLog.Core;
using SaturnLog.Repository.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaturnLog.Repository
{
    public class Saturns5DashboardRepository : ISaturns5DashboardRepository, IDisposable
    {
        #region Const
        public const string GoogleSpreadsheetsURLAppendix = @"https://docs.google.com/spreadsheets/d/";

        //public const string Saturns5Dashboard_SpreadsheetId = "1kb-36o8c6_YCdaMqNtE-AerPpvDPWSKque9p8nr6Y9E";
        //public const string Saturns5Dashboard_SpreadsheetId = "1lZ424RsfqNtKuBZMYnwpYwlYasOeRf7BHgKOctwqd2U";
        internal readonly string Saturns5Dashboard_SpreadsheetId;

        public const string Saturns5Dashboard_SheetId = "Saturns5 Dashboard";
        public const int Saturns5Dashboard_HeaderRowIndex = 0;
        public const int Saturns5Dashboard_InitialRowsCount = 1;
        public const int Saturns5Dashboard_EntryRowsCount = 1;
        public const int Saturns5Dashboard_NewEntryRowsCount = 1;
        public const int Saturns5Dashboard_ColumnsCount = 11;
        
        public readonly static string[] Saturn5Dashboard_HeaderRowContent = new string[Saturns5DashboardRepository.Saturns5Dashboard_ColumnsCount]
        { "Serial Number:", "Short Id:", "Saturn Spreadsheet URL:", "Phone Number:", "Status:", "Last Seen Date:", "Last Seen Time:", "Last Seen With Username:", "Last Seen With First Name:", "Last Seen With Surname:", "Last User Spreadsheet URL:" };

        public const int Saturns5Dashboard_SerialNumber = 0;
        public const int Saturns5Dashboard_ShortId = 1;
        public const int Saturns5Dashboard_SaturnSpreadsheetURL = 2;
        public const int Saturns5Dashboard_Status = 3;
        public const int Saturns5Dashboard_PhoneNumber = 4;
        public const int Saturns5Dashboard_LastSeenDate = 5;
        public const int Saturns5Dashboard_LastSeenTime = 6;
        public const int Saturns5Dashboard_LastSeenUserUsername = 7;
        public const int Saturns5Dashboard_LastSeenUserFirstName = 8;
        public const int Saturns5Dashboard_LastSeenUserSurname = 9;
        public const int Saturns5Dashboard_LastSeenUserSpreadsheetURL = 10;
        #endregion

        #region Private fields
        private object DashboardDataLock { get { return this._dataRepository.Saturns5DB.Saturns5DBLock; } }

        // Data repository 
        private DataRepository _dataRepository;

        private LiveSpreadsheet _dashboardSpreadsheet;

        private LiveSheet _dashboardSheet;
        
        // Key: serial number Value: row index in saturn5 db.
        private IDictionary<string, int> _dbRowIndexesBySerialNumbers { get { return this._dataRepository.Saturns5DB.RowIndexesBySerialNumbers; } }

        // Key: serial number Value: row index in saturns5 dashboard.
        // (adjusted for the difference caused by the fact that the dashboards top row contains the header and as such associated entries
        // are located on the row index greater by one then the index of the associated Saturn5DB entry)
        private IDictionary<string, int> _dashboardRowIndexesBySerialNumbers = new Dictionary<string, int>();
        #endregion

        #region Constructor
        private Saturns5DashboardRepository(string spreadsheetId) { this.Saturns5Dashboard_SpreadsheetId = spreadsheetId; }

        internal static async Task<Saturns5DashboardRepository> GetAsync(DataRepository dataRepository, string spreadsheetId)
        {
            //
            Saturns5DashboardRepository saturns5DashboardRepository = new Saturns5DashboardRepository(spreadsheetId);

            // Assign provided data repository.
            saturns5DashboardRepository._dataRepository = dataRepository;

            // Get live spreadsheets database reference.
            LiveSpreadsheetsDb db = saturns5DashboardRepository._dataRepository.GoogleService.SpreadsheetsDb;

            // Load spreadsheet containing saturns dashboard.
            await db.LoadSpreadsheetAsync(saturns5DashboardRepository.Saturns5Dashboard_SpreadsheetId);

            // Get and assign reference to Saturn5_Dashboard spreadsheet and sheet
            saturns5DashboardRepository._dashboardSpreadsheet = db[saturns5DashboardRepository.Saturns5Dashboard_SpreadsheetId];
            saturns5DashboardRepository._dashboardSheet = saturns5DashboardRepository._dashboardSpreadsheet[Saturns5DashboardRepository.Saturns5Dashboard_SheetId];

            // Re build serialNumber-rowIndex association index
            saturns5DashboardRepository.ReBuildDashboardSerialNumberIndex();

            saturns5DashboardRepository._dataRepository.UsersDB.UserSpreadsheetLoaded += saturns5DashboardRepository.OnUserSpreadsheetLoaded;
            saturns5DashboardRepository._dataRepository.UsersDB.UserSpreadsheetAdded += saturns5DashboardRepository.OnUserSpreadsheetAdded;
            saturns5DashboardRepository._dataRepository.UsersDB.UserSpreadsheetReplaced += saturns5DashboardRepository.OnUserSpreadsheetReplaced;
            saturns5DashboardRepository._dataRepository.UsersDB.UserSpreadsheetRemoved += saturns5DashboardRepository.OnUserSpreadsheetRemoved;

            // 
            return saturns5DashboardRepository;
        }

        private void OnUserSpreadsheetLoaded(object sender, UserSpreadsheetLoadedEventArgs e)
        {
            // TODO
        }

        private void OnUserSpreadsheetAdded(object sender, UserSpreadsheetAddedEventArgs e)
        {
            // TODO
        }

        private void OnUserSpreadsheetReplaced(object sender, UserSpreadsheetReplacedEventArgs e)
        {
            LiveSpreadsheet userSpreadsheet = e.Spreadsheet;

            LiveSheet metadataSheet = userSpreadsheet[UserRepository.UserLog_Metadata_SheetTitle];

            LiveCell usernameMetadataCell = metadataSheet[UserRepository.UserLog_Metadata_UsernameValue_RowIndex][UserRepository.UserLog_Metadata_UsernameValue_ColumnIndex];
            LiveCell firstNameMetadataCell = metadataSheet[UserRepository.UserLog_Metadata_FirstNameValue_RowIndex][UserRepository.UserLog_Metadata_FirstNameValue_ColumnIndex];
            LiveCell surnameMetadataCell = metadataSheet[UserRepository.UserLog_Metadata_SurnameValue_RowIndex][UserRepository.UserLog_Metadata_SurnameValue_ColumnIndex];

            string username = usernameMetadataCell.GetDataAsString();
            string firstName = firstNameMetadataCell.GetDataAsString();
            string surname = firstNameMetadataCell.GetDataAsString();
            string userSpreadsheetId = this._dataRepository.UserRepository.GetUserLogSpreadsheetId(username);
            string userSpreadsheetURL = this.GetUrlFromSpreadsheetId(userSpreadsheetId);

            // Get all the dashboard rows associated with currently looped through user
            IList<LiveRow> userAssociateRows = this._dashboardSheet.SheetRows.Where((row) =>
            {
                LiveCell usernameCell = row[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserUsername];
                return (usernameCell.GetDataAsString() == username);
            }).ToList();

            // Loop through each user associated rows, compare their user-related content, and update if changes are required.
            foreach (LiveRow userAssociatedRow in userAssociateRows)
            {
                if (userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName].GetDataAsString() != firstName
                    || userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname].GetDataAsString() != surname
                    || userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSpreadsheetURL].GetDataAsString() != userSpreadsheetURL)
                {
                    userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName].SetData(firstName);
                    userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname].SetData(surname);

                    userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSpreadsheetURL].SetData(userSpreadsheetURL);
                    userAssociatedRow.Upload(this._dataRepository.GoogleService.SpreadsheetsDb);
                }
            }
        }

        private void OnUserSpreadsheetRemoved(object sender, UserSpreadsheetRemovedEventArgs e)
        {
            // TODO
        }
        #endregion

        #region Methods - Saturns5DashboardRepository
        public void ReObtainData()
        {
            // Thread safety lock
            lock (this.DashboardDataLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Re-obtain users sheet data from google servers
                this._dashboardSheet.ReObtainData(db);
            }
        }

        // Assures grid data...

        // (exactly the same number of entries indexed to the same primary key serial number at the same row index
        // - adjusted for the difference caused by the fact that the dashboards top row contains the header and as such associated entries
        // are located on the row index greater by 1 then the index of the associated Saturn5DB entry) of this._dashboardSheet
        // and LiveSheet representing Saturns5DB sheet) 

        // ...consistency between current Saturns5DashboardRepository and Saturn5Repository by removing 
        // Saturns5DashbordEntries associated with unrecognized serial numbers, and correcting incorrect dashboard short ids,
        // As well as adding missing entries. (Content of other cells then dashboard entries serial numbers still has to be updated)
        internal async Task AssureGridDataConsistencyAsync()
        {
            await Task.Run(() =>
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // 1)
                this.RemoveSurplusSaturns5DashboardEntries(db);

                // 2)
                this.AddMissingSaturns5DashboardEntries(db);

                // 3) 
                this.AssureRowIndexLocationSaturns5DashboardEntries(db);

                // 4) Final rebuild of dashboard serial numbers - entry row quick access relativity dictionary index.
                this.ReBuildDashboardSerialNumberIndex();
            });
        }

        // TO BE EXECUTED ONLY AFTER ALL Saturn5 and users individual spreadsheets gonna get loaded
        internal async Task AssureDataConsistencyAsync(CancellationToken? token = null)
        {
            // Assures saturns related dashboard data consistency
            await this.AssureSaturnsDataConsistencyAsync(token);

            // Assures users related dashboard data consistency
            await this.AssureUsersDataConsistencyAsync(token);
        }

        // TO BE EXECUTED ONLY AFTER ALL saturn5 individual spreadsheets gonna get loaded
        internal async Task AssureSaturnsDataConsistencyAsync(CancellationToken? token = null)
        {
            await Task.Run(() => 
            {
                // Thread safety lock
                lock (this.DashboardDataLock)
                {
                    token.GetValueOrDefault().ThrowIfCancellationRequested();

                    foreach (string dbSerialNumber in this._dataRepository.Saturns5DB.RowIndexesBySerialNumbers.Keys)
                        if (!this.HasSerialNumberAssociatedEntry(dbSerialNumber))
                            throw new InvalidOperationException($"All individual Saturn5 spreadsheets has to be fully(partially metadata and issues sheets) prior to executing this method: {nameof(AssureSaturnsDataConsistencyAsync)}");

                    IList<Saturn5> saturns5 = this._dataRepository.Saturn5Repository.ReadAll();

                    foreach (Saturn5 saturn5 in saturns5)
                    {
                        token.GetValueOrDefault().ThrowIfCancellationRequested();

                        // Get dashboard row index of the dashboard row associated with currently looped through saturn 5...
                        int dashboardRowIndex = this._dbRowIndexesBySerialNumbers[saturn5.SerialNumber] + 1;
                        // ... and get the dashboard row containing associate dashboard entry.
                        LiveRow dashboardRow = this._dashboardSheet[dashboardRowIndex];

                        string saturn5SpreadsheetId = this._dataRepository.Saturn5Repository.GetSaturn5LogSpreadsheetId(saturn5.SerialNumber);
                        string saturn5SpreadsheetURL = this.GetUrlFromSpreadsheetId(saturn5SpreadsheetId);

                        // if any of the properties located on the dashboard doesn't match these ones obtained from the Saturn5Repository...
                        // ShortId
                        if (saturn5.ShortId != dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_ShortId].GetDataAsString()
                            // Saturn5 Spreadsheet URL
                            || saturn5SpreadsheetURL != dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_SaturnSpreadsheetURL].GetDataAsString()
                            // Saturn5Status
                            || Saturn5StatusService.GetDashboardString(saturn5.Status) != dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_Status].GetDataAsString()
                            // PhoneNumber
                            || saturn5.PhoneNumber != dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_PhoneNumber].GetDataAsString()
                            // LastSeenDate
                            || saturn5.LastSeenDate != dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenDate].GetDataAsString()
                            // LastSeenTime
                            || saturn5.LastSeenTime != dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenTime].GetDataAsString()
                            // LastSeenUsername
                            || saturn5.LastSeenUsername != dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserUsername].GetDataAsString())
                            // Update dashboard sheet accordingly
                            this.Update(saturn5);
                    }
                }
            });

            await AssureUsersDataConsistencyAsync(token);
        }

        // TO BE EXECUTED ONLY AFTER ALL users individual spreadsheets gonna get loaded
        internal async Task AssureUsersDataConsistencyAsync(CancellationToken? token = null)
        {
            await Task.Run(() =>
            {
                // Thread safety lock
                lock (this.DashboardDataLock)
                {
                    token.GetValueOrDefault().ThrowIfCancellationRequested();

                    // Get all users
                    IList<User> users = this._dataRepository.UserRepository.ReadAll();

                    // Loop through each user
                    foreach (User user in users)
                    {
                        token.GetValueOrDefault().ThrowIfCancellationRequested();

                        string userSpreadsheetId = this._dataRepository.UserRepository.GetUserLogSpreadsheetId(user.Username);
                        string userSpreadsheetURL = this.GetUrlFromSpreadsheetId(userSpreadsheetId);

                        // Get all the dashboard rows associated with currently looped through user
                        IList<LiveRow> userAssociateRows = this._dashboardSheet.SheetRows.Where((row) =>
                        {
                            LiveCell usernameCell = row[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserUsername];
                            return (usernameCell.GetDataAsString() == user.Username);
                        }).ToList();

                        // Loop through each user associated rows, compare their user-related content, and update if changes are required.
                        foreach (LiveRow userAssociatedRow in userAssociateRows)
                        {
                            if (userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName].GetDataAsString() != user.FirstName
                                || userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname].GetDataAsString() != user.Surname
                                || userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSpreadsheetURL].GetDataAsString() != userSpreadsheetURL)
                            {
                                userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName].SetData(user.FirstName);
                                userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname].SetData(user.Surname);

                                userAssociatedRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSpreadsheetURL].SetData(userSpreadsheetURL);
                                userAssociatedRow.Upload(this._dataRepository.GoogleService.SpreadsheetsDb);
                            }
                        }
                    }
                }
            });
        }
        #endregion

        #region Saturns5DashboardRepository - Methods
        // Creates new Saturns5DashboardEntry based on the saturn 5 unit existing withing Saturns5DB, and specified with provided serial number.
        public void Create(Saturn5 saturn5)
        {
            if (saturn5 is null) throw new ArgumentNullException(nameof(saturn5));
            if (this.HasSerialNumberAssociatedEntry(saturn5.SerialNumber))
                throw new ArgumentException("Saturns5DashboardRepository already contains dashboard entry associated with the provided Saturn5 serial number.", nameof(saturn5));

            // Thread safety lock
            lock (this.DashboardDataLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get new dashboard entry row data
                IList<string> dashboardEntryRowData = this.GetNewDashboardEntryRowData(saturn5);

                // Append dashboard sheet with the row representing dashboard entry associated with provided Saturn5
                this._dashboardSheet.AppendRows(db, Saturns5DashboardRepository.Saturns5Dashboard_NewEntryRowsCount,
                    new IList<string>[Saturns5DashboardRepository.Saturns5Dashboard_NewEntryRowsCount] { dashboardEntryRowData });

                // Creates serial number-rowIndex association in the repository for the newly created 
                this._dashboardRowIndexesBySerialNumbers.Add(saturn5.SerialNumber, this._dbRowIndexesBySerialNumbers[saturn5.SerialNumber] + 1);
            }
        }

        // Read
        public bool HasSerialNumberAssociatedEntry(string serialNumber)
        {
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            
            // Thread safety lock
            lock (this.DashboardDataLock)
                return this._dashboardRowIndexesBySerialNumbers.ContainsKey(serialNumber);
        }

        public Saturns5DashboardEntry Read(string serialNumber)
        {
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            if (!this.HasSerialNumberAssociatedEntry(serialNumber))
                throw new ArgumentException("Saturns5DashboardRepository doesn't contain dashboard entry associated with the provided serial number.", nameof(serialNumber));

            // Thread safety lock
            lock (this.DashboardDataLock)
            {
                int serialNumberAssociatedRowIndex = this._dbRowIndexesBySerialNumbers[serialNumber] - 1;
                LiveRow saturn5DashbordEntryRow = this._dashboardSheet[serialNumberAssociatedRowIndex];

                return new Saturns5DashboardEntry(
                    // Serial number
                    serialNumber,
                    // ShortId 
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_ShortId].GetDataAsString(),
                    // Status
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_Status].GetDataAsString().ValueToSaturn5Status(),
                    // Phone number
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_PhoneNumber].GetDataAsString(),
                    // Last seen date
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenDate].GetDataAsString(),
                    // Last seen time
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenTime].GetDataAsString(),
                    // Last seen username
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserUsername].GetDataAsString(),
                    // Last seen first username
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName].GetDataAsString(),
                    // Last seen surname
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname].GetDataAsString());
            }                
        }

        public IList<Saturns5DashboardEntry> ReadAll()
        {
            // Thread safety lock
            lock (this.DashboardDataLock)
            {
                IList<Saturns5DashboardEntry> dashboardEntries = new List<Saturns5DashboardEntry>();

                for (int i = 1; i < this._dashboardSheet.RowCount; i++)
                {
                    LiveRow saturn5DashbordEntryRow = this._dashboardSheet[i];

                    dashboardEntries.Add(new Saturns5DashboardEntry(
                    // Serial number
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_SerialNumber].GetDataAsString(),
                    // ShortId 
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_ShortId].GetDataAsString(),
                    // Status
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_Status].GetDataAsString().ValueToSaturn5Status(),
                    // Phone number
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_PhoneNumber].GetDataAsString(),
                    // Last seen date
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenDate].GetDataAsString(),
                    // Last seen time
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenTime].GetDataAsString(),
                    // Last seen username
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserUsername].GetDataAsString(),
                    // Last seen first username
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName].GetDataAsString(),
                    // Last seen surname
                    saturn5DashbordEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname].GetDataAsString()));
                }

                return dashboardEntries;
            }
        }

        // Update
        public void UpdateUserDetails(User user)
        {
            // Thread safety lock
            lock (this.DashboardDataLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Loop through all the rows in the dashboard
                // Loop through each of the rows (apart from the header row) in the LiveSheet containing saturn 5 database.
                for (int i = 1; i < this._dashboardSheet.RowCount; i++)
                {
                    // Obtain dashboard row located on the currently looped through index...
                    LiveRow dashboardRow = this._dashboardSheet[i];

                    // ... get the cell containing last seen Username for the specific saturn unit located on the dashboard row...
                    LiveCell usernameCell = dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserUsername];

                    // ... get the username data from that cell... 
                    string dashboardRowUsername = usernameCell.GetDataAsString();

                    // ... and compare provided user Username with dashboard row saturn 5 last seen username,
                    // if they are found being equal, update data in other user related cells.
                    if (user.Username == dashboardRowUsername)
                    {
                        // Boolean flag indicating necessity for updating the changes.
                        bool changesRequired = false;

                        LiveCell firstNameCell = dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName];
                        LiveCell surnameCell = dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname];
                        LiveCell userSpreadsheetURLCell = dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSpreadsheetURL];
                        LiveCell saturn5StatusCell = dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_Status];

                        string currentFirstName = firstNameCell.GetDataAsString();
                        string currentSurname = surnameCell.GetDataAsString();
                        string currentSpreadsheetURL = userSpreadsheetURLCell.GetDataAsString();

                        // Obtain status from the dashboard cell associated with the saturn 5 
                        // with (according dashboard) the same last user as the one provided for update.
                        Saturn5Status currentStatus = Saturn5StatusService.GetStatusFromDashboardString(saturn5StatusCell.GetDataAsString());

                        if (currentStatus.IsWithUser())
                        {
                            // Get saturn5 status according the type of the user the saturn5 is getting allocated to.
                            Saturn5Status status = user.Type.GetWithUserSaturn5Status();

                            if (currentStatus != status)
                            {
                                changesRequired = true;

                                saturn5StatusCell.SetData(Saturn5StatusService.GetDashboardString(status));
                            }

                        }

                        if (currentFirstName != user.FirstName)
                        {
                            changesRequired = true;

                            firstNameCell.SetData(user.FirstName);
                        }

                        if (currentSurname != user.Surname)
                        {
                            changesRequired = true;

                            surnameCell.SetData(user.Surname);
                        }

                        string userSpreadsheetId = this._dataRepository.UserRepository.GetUserLogSpreadsheetId(dashboardRowUsername);
                        string newSpreadsheetURL = this.GetUrlFromSpreadsheetId(userSpreadsheetId);

                        if (currentSpreadsheetURL != newSpreadsheetURL)
                        {
                            changesRequired = true;

                            userSpreadsheetURLCell.SetData(newSpreadsheetURL);
                        }

                        if (changesRequired)
                            dashboardRow.Upload(db);
                    }
                }
            }
        }

        public void Update(Saturn5 saturn5)
        {
            if (saturn5 is null) throw new ArgumentNullException(nameof(saturn5));
            if (!this.HasSerialNumberAssociatedEntry(saturn5.SerialNumber))
                throw new ArgumentException("Saturns5DashboardRepository doesn't contain dashboard entry associated with the provided Saturn5.", nameof(saturn5));

            // Thread safety lock
            lock (this.DashboardDataLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                IList<string> updatedDashboardEntryRowData = this.GetUpdatedDashboardEntryRowData(saturn5);

                int serialNumberDashboardRowIndex = this._dashboardRowIndexesBySerialNumbers[saturn5.SerialNumber];
                LiveRow saturns5DashboardEntry =  this._dashboardSheet[serialNumberDashboardRowIndex];

                saturns5DashboardEntry.SetDataFromStringsData(0, updatedDashboardEntryRowData);
                saturns5DashboardEntry.Upload(db);
            }
        }

        // Delete
        public void Delete(string serialNumber)
        {
            if (serialNumber is null) throw new ArgumentNullException(nameof(serialNumber));
            if (!this.HasSerialNumberAssociatedEntry(serialNumber))
                throw new ArgumentException("Saturns5DashboardRepository doesn't contain dashboard entry associated with the provided serial number.", nameof(serialNumber));

            // Thread safety lock
            lock (this.DashboardDataLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                int serialNumberDashboardRowIndex = this._dashboardRowIndexesBySerialNumbers[serialNumber];
                this._dashboardSheet.RemoveRows(db, serialNumberDashboardRowIndex, Saturns5DashboardRepository.Saturns5Dashboard_NewEntryRowsCount);

                // Rebuild serial number-dashboard row index association dictionary.
                this.ReBuildDashboardSerialNumberIndex();
            }
        }
        #endregion

        #region Private Helpers
        // Clears and builds from scratch dictionary of serialNumber-rowIndex association.
        private void ReBuildDashboardSerialNumberIndex()
        {
            // Clear existing relativity table.
            this._dashboardRowIndexesBySerialNumbers.Clear();

            // Loop through each of the rows (apart from the header row) in the LiveSheet containing saturn 5 database.
            for (int i = 1; i < this._dashboardSheet.RowCount; i++)
            {
                // Get saturn 5 dashboard entry row appropriate for the currently looped row through index.
                LiveRow saturns5DashboardEntryRow = this._dashboardSheet[i];

                // Obtain saturn 5 serial number.
                string serialNumber = saturns5DashboardEntryRow[Saturns5DashboardRepository.Saturns5Dashboard_SerialNumber].GetDataAsString();

                // Serial number might be null if method is called during dashboard recreation process
                if (serialNumber is null)
                    break;

                // Add serial number association with currently lopped through row index.
                this._dashboardRowIndexesBySerialNumbers.Add(serialNumber, i);
            }
        }

        private IList<string> GetNewDashboardEntryRowData(Saturn5 saturn5)
        {
            IList<string> dashboardEntryRowData = new string[Saturns5DashboardRepository.Saturns5Dashboard_ColumnsCount];

            string saturnSpreadsheetId = this._dataRepository.Saturn5Repository.GetSaturn5LogSpreadsheetId(saturn5.SerialNumber);
            
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_SerialNumber] = saturn5.SerialNumber;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_ShortId] = saturn5.ShortId;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_SaturnSpreadsheetURL] = this.GetUrlFromSpreadsheetId(saturnSpreadsheetId);
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_Status] = Saturn5StatusService.GetDashboardString(saturn5.Status);
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_PhoneNumber] = saturn5.PhoneNumber;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenDate] = saturn5.LastSeenDate;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenTime] = saturn5.LastSeenTime;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserUsername] = saturn5.LastSeenUsername;
            
            try
            {
                dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName] = this._dataRepository.UserRepository.GetUserFirstName(saturn5.LastSeenUsername);
                dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname] = this._dataRepository.UserRepository.GetUserSurname(saturn5.LastSeenUsername);

                string userSpreadsheetId = this._dataRepository.UserRepository.GetUserLogSpreadsheetId(saturn5.LastSeenUsername);

                dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSpreadsheetURL] = this.GetUrlFromSpreadsheetId(userSpreadsheetId);
            }
            // If last seen username is associated with user which has been deleted since, catch an exception.
            catch (ArgumentException ex) when (ex.ParamName == "username") { }

            // Return contructed row data.
            return dashboardEntryRowData;
        }
        
        private IList<string> GetUpdatedDashboardEntryRowData(Saturn5 saturn5)
        {
            IList<string> dashboardEntryRowData = new string[Saturns5DashboardRepository.Saturns5Dashboard_ColumnsCount];

            string saturnSpreadsheetId = this._dataRepository.Saturn5Repository.GetSaturn5LogSpreadsheetId(saturn5.SerialNumber);

            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_SerialNumber] = null;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_ShortId] = saturn5.ShortId;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_SaturnSpreadsheetURL] = this.GetUrlFromSpreadsheetId(saturnSpreadsheetId);
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_Status] = Saturn5StatusService.GetDashboardString(saturn5.Status);
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_PhoneNumber] = saturn5.PhoneNumber;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenDate] = saturn5.LastSeenDate;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenTime] = saturn5.LastSeenTime;
            dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserUsername] = saturn5.LastSeenUsername;

            try
            {
                dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserFirstName] = this._dataRepository.UserRepository.GetUserFirstName(saturn5.LastSeenUsername);
                dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSurname] = this._dataRepository.UserRepository.GetUserSurname(saturn5.LastSeenUsername);

                string userSpreadsheetId = this._dataRepository.UserRepository.GetUserLogSpreadsheetId(saturn5.LastSeenUsername);

                dashboardEntryRowData[Saturns5DashboardRepository.Saturns5Dashboard_LastSeenUserSpreadsheetURL] = this.GetUrlFromSpreadsheetId(userSpreadsheetId);
            }
            // If last seen username is associated with user which has been deleted since, catch an exception.
            catch (ArgumentException ex) when (ex.ParamName == "username") { }

            // Return constructed row data.
            return dashboardEntryRowData;
        }

        private string GetUrlFromSpreadsheetId(string spreadsheetId)
        {
            return GoogleSpreadsheetsURLAppendix + spreadsheetId;
        }

        #region AssureDataConsistency - TODO transfer into its own class
        // 1)
        private void RemoveSurplusSaturns5DashboardEntries(LiveSpreadsheetsDb db)
        {
            // REMOVE DASHBOARD ENTRIES NOT EXISITING IN THE SATURN5 DATABASE

            // Get all rows from this._dashboardSheet, excluding header row.
            IList<LiveRow> dashboardRowEntries = this._dashboardSheet.SheetRows.ToList();
            dashboardRowEntries.RemoveAt(Saturns5DashboardRepository.Saturns5Dashboard_HeaderRowIndex);

            foreach (LiveRow dashboardRow in dashboardRowEntries)
            {
                string dashboardRowSerialNumber = dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_SerialNumber].GetDataAsString();
                string dashboardRowShortId = dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_ShortId].GetDataAsString();

                if (!this._dataRepository.Saturn5Repository.HasSaturn5SerialNumber(dashboardRowSerialNumber))
                {
                    this._dashboardSheet.RemoveRows(db, dashboardRow.RowIndex, 1);

                    // Rebuild dashboard serial numbers - entry row index quick access relativity index
                    this.ReBuildDashboardSerialNumberIndex();
                }
            }
        }

        // 2)
        private void AddMissingSaturns5DashboardEntries(LiveSpreadsheetsDb db)
        {
            // ADD(append) MISSING ENTRIES IN THE DASHBOARD

            // Loop through all the serial numbers of the Saturn5 units existing in Saturns5Db,
            // and append dashboard with new row for each missing one.
            foreach (KeyValuePair<string, int> rowIndexBySerialNumber in this._dataRepository.Saturns5DB.RowIndexesBySerialNumbers.ToList())
            {
                // Get currently looped through saturns 5 database serial number
                string dbSerialNumber = rowIndexBySerialNumber.Key;

                if (this.HasSerialNumberAssociatedEntry(dbSerialNumber))
                    continue;

                // Get saturn5 associated with the serial number located on currently looped throw row in the Satruns5DB
                Saturn5 dbSaturn5 = this._dataRepository.Saturn5Repository.Read(dbSerialNumber);

                // Get new dashboard row entry data using obtained saturn.
                IList<string> newDashboardEntryRowData = this.GetNewDashboardEntryRowData(dbSaturn5);

                // Append current dashboard missing saturn 5 dashboard entry representing obtained saturn into this._dashboardSheet
                this._dashboardSheet.AppendRows(db, Saturns5DashboardRepository.Saturns5Dashboard_EntryRowsCount,
                    new IList<string>[Saturns5DashboardRepository.Saturns5Dashboard_EntryRowsCount] { newDashboardEntryRowData });

                // Rebuild dashboard serial numbers - entry row index quick access relativity index
                this.ReBuildDashboardSerialNumberIndex();
            }
        }

        // 3)
        private void AssureRowIndexLocationSaturns5DashboardEntries(LiveSpreadsheetsDb db)
        {
            // ASSURE ROW INDEXES INTEGRITY BETWEEN ALL ENTRIES EXISTING ENTRIES IN THE Saturns5DashboardsRepository and Saturn5Repository

            bool modified = false;
            do
            {
                modified = false;

                foreach (KeyValuePair<string, int> rowIndexBySerialNumber in this._dataRepository.Saturns5DB.RowIndexesBySerialNumbers.ToList())
                {
                    // Get currently looped through saturns 5 database serial number
                    string dbSerialNumber = rowIndexBySerialNumber.Key;

                    // Get currently saturns 5 database row index.
                    int dbRowIndex = rowIndexBySerialNumber.Value;

                    // Get row dashboard row currently located on the dbRow saturn 5 serial number associated row
                    LiveRow dashboardRow = this._dashboardSheet.SheetRows.ElementAtOrDefault(dbRowIndex + 1);

                    // Get saturn 5 serial number currently stored in the dashboard row entry located at the same rowIndex
                    // as the currently looped through saturn 5 DB row.
                    string dashboardRowSerialNumber = dashboardRow[Saturns5DashboardRepository.Saturns5Dashboard_SerialNumber].GetDataAsString();

                    // if dbRow index is in the scope of this._dashboardSheet
                    // and dashbordRow contains serial number matching serial number
                    // located on the row with matching index in the saturn 5 database
                    if (dbSerialNumber == dashboardRowSerialNumber)
                        // (TO BRAKE OUT OF DO WHILE LOOP - each of looped through entries in
                        // the foreach loop should terminate here).
                        continue;

                    #region WARRNING - int dashboardRowIndex variable
                    // WARRNING - DO NOT REPLACE with indexer from this._dashboardRowIndexesBySerialNumbers[]
                    // at this point this quick-access serialNumber-dashboardRowIndex dictionary is in INVALID state...

                    // (as content of this._dashboardSheet has been potentially already partially modified, 
                    // but quick-access  serialNumber -dashboardRowIndex dictionary index hasn't been modified accordingly yet
                    // - and it cannot be modified before all of the content of this._dashboardSheet will be corrected),

                    // ...and dashboardRowIndex has to be found using LINQ instead, to obtain valid data.
                    #endregion
                    // Get row index of the entry in this_dashboardSheet having equal serial number then the one in 
                    // the currently looped through Saturns5DB row.
                    int dashboardRowIndex = this._dashboardSheet.SheetRows.First((dashboardSheetRow) =>
                    {return dashboardSheetRow[Saturns5DashboardRepository.Saturns5Dashboard_SerialNumber].GetDataAsString() == dbSerialNumber;}
                    ).RowIndex;

                    // Else if serial number located on the currently looped through saturns5 database row,
                    // can be found one of this._dashbordSheet rows, but it is located ABOVE its intended location.
                    if (dashboardRowIndex != dbRowIndex)
                    {
                        // Get row data from its current position
                        IList<string> newDashboardEntryRowData = this._dashboardSheet[dashboardRowIndex].GetDataAsStrings();

                        // Remove it from its current position
                        this._dashboardSheet.RemoveRows(db, dashboardRowIndex, Saturns5DashboardRepository.Saturns5Dashboard_EntryRowsCount);

                        // Insert it into its designated position
                        this._dashboardSheet.InsertRows(db, dbRowIndex + 1, Saturns5DashboardRepository.Saturns5Dashboard_EntryRowsCount,
                            new IList<string>[Saturns5DashboardRepository.Saturns5Dashboard_EntryRowsCount] { newDashboardEntryRowData });

                        // Set flag to indicate that content of this._dashboardSheet has been modified
                        modified = true;
                    }
                    else throw new InvalidOperationException($"THIS SHOULD NOT HAPPENED - CLEARLY INVALID LOGIC IMPLEMENTATION - {nameof(Saturns5DashboardRepository)}");
                }
            } while (modified);
        }
        #endregion
        #endregion

        #region IDisposable supports
        public void Dispose()
        {
            if (!(this._dataRepository?.UsersDB is null))
            {
                this._dataRepository.UsersDB.UserSpreadsheetLoaded -= OnUserSpreadsheetLoaded;
                this._dataRepository.UsersDB.UserSpreadsheetAdded -= OnUserSpreadsheetAdded;
                this._dataRepository.UsersDB.UserSpreadsheetReplaced -= OnUserSpreadsheetReplaced;
                //this._dataRepository.UsersDB.UserSpreadsheetArchived -= OnUserSpreadsheetArchived;
                //this._dataRepository.UsersDB.UserSpreadsheetUnarchived -= OnUserSpreadsheetUnarchived;
                this._dataRepository.UsersDB.UserSpreadsheetRemoved -= OnUserSpreadsheetRemoved;
            }

            this._dashboardSpreadsheet?.Dispose();
            
            this._dashboardSpreadsheet = null;

            this._dashboardSheet = null;
            
            this._dataRepository = null;
        }
        #endregion
    }
}
