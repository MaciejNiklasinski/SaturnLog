using LiveGoogle.Extensions;
using LiveGoogle.Sheets;
using SaturnLog.Core;
using SaturnLog.Repository.EventArgs;
using SaturnLog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaturnLog.Repository
{
    public class UserRepository : IUserRepository, IUserDB, IDisposable
    {
        #region Events
        public event EventHandler<UserSpreadsheetLoadedEventArgs> UserSpreadsheetLoaded;
        public event EventHandler<UserSpreadsheetAddedEventArgs> UserSpreadsheetAdded;
        public event EventHandler<UserSpreadsheetReplacedEventArgs> UserSpreadsheetReplaced;
        //public event EventHandler<UserSpreadsheetArchivedEventArgs> UserSpreadsheetArchived;
        //public event EventHandler<UserSpreadsheetUnarchivedEventArgs> UserSpreadsheetUnarchived;
        public event EventHandler<UserSpreadsheetRemovedEventArgs> UserSpreadsheetRemoved;
        #endregion

        #region Const
        #region UsersDB
        // UsersDB spreadsheet id.
        //public const string UsersDB_SpreadsheetId = "1D9u4tAZbppdKSiR0plKk1jCBdvpoNbFOQcQYmYctbDM";
        //public const string UsersDB_SpreadsheetId = "1PNXK1civKEdgmhDBR9XB31UVL69DYS2RRJM4S5l6Q34";
        internal readonly string UsersDB_SpreadsheetId;

        // UsersDB sheet title id.
        public const string UsersDB_SheetId = "UsersDB";

        public const int UserDB_NewEntryColumnsCount = 5;
        public const int UserDB_NewEntryRowsCount = 1;

        public const int UsersDB_Username_ColumnIndex = 0;
        public const int UsersDB_FirstName_ColumnIndex = 1;
        public const int UsersDB_Surname_ColumnIndex = 2;
        public const int UsersDB_Type_ColumnIndex = 3;
        public const int UsersDB_UserSpreadsheetId_ColumnIndex = 4;
        #endregion

        #region UserLog
        // User log metadata sheet title id.
        public const string UserLog_Metadata_SheetTitle = "Metadata";

        public const int UserLog_Metadata_RowsCount = 2;
        public const int UserLog_Metadata_ColumnsCount = 6;
        public const int UserLog_Metadata_LegendRowIndex = 0;
        public const int UserLog_Metadata_ValuesRowIndex = 1;

        public const int UserLog_Metadata_UsernameValue_ColumnIndex = 0;
        public const int UserLog_Metadata_UsernameValue_RowIndex = 1;

        public const int UserLog_Metadata_FirstNameValue_ColumnIndex = 1;
        public const int UserLog_Metadata_FirstNameValue_RowIndex = 1;

        public const int UserLog_Metadata_SurnameValue_ColumnIndex = 2;
        public const int UserLog_Metadata_SurnameValue_RowIndex = 1;

        public const int UserLog_Metadata_TypeValue_ColumnIndex = 3;
        public const int UserLog_Metadata_TypeValue_RowIndex = 1;

        public const int UserLog_Metadata_YearValue_ColumnIndex = 4;
        public const int UserLog_Metadata_YearValue_RowIndex = 1;

        public const int UserLog_Metadata_PreviousSpreadsheetIdValue_ColumnIndex = 5;
        public const int UserLog_Metadata_PreviousSpreadsheetIdValue_RowIndex = 1;

        public const int UserLog_LogsSheetColumnsCount = 2;
        public const int UserLog_LogsSheetInitialRowsCount = 1;
        public const int UserLog_LogsSheet_TimestampColumnIndex = 0;
        public const int UserLog_LogsSheet_LogColumnIndex = 1;
        public const int UserLog_LogsSheet_TimestampColumnWidth = 150;
        public const int UserLog_LogsSheet_LogColumnWidth = 1500;
        public readonly static string[] UserLog_LogsSheetInitialRowContent = new string[UserRepository.UserLog_LogsSheetColumnsCount] { "Timestamp:", "Log:" };
        #endregion
        #endregion

        #region Private Fields
        // Stores data repository.
        private DataRepository _dataRepository;

        // Instance of the live sheet containing users db
        private LiveSheet _usersDbSheet;

        // Key: username Value: row index in user db.
        private IDictionary<string, int> _rowIndexesByUsernames = new Dictionary<string, int>();
        #endregion

        #region IUserDB - Properties
        // Users data consistency lock
        public object UsersDBLock { get; } = new object();

        // Key: username Value: row index in saturn5 db.
        public IDictionary<string, int> RowIndexesByUsernames { get { return this._rowIndexesByUsernames; } }
        
        public LiveSheet DbSheet { get { return this._usersDbSheet; } }
        #endregion

        #region Constructor
        // Default constructor
        private UserRepository(string spreadsheetId) { this.UsersDB_SpreadsheetId = spreadsheetId; }
        
        //
        internal static async Task<UserRepository> GetAsync(DataRepository dataRepository, string spreadsheetId)
        {
            //
            UserRepository userRepository = new UserRepository(spreadsheetId);
            
            // Assign provided data repository.
            userRepository._dataRepository = dataRepository;

            // Get live spreadsheets database reference.
            LiveSpreadsheetsDb db = userRepository._dataRepository.GoogleService.SpreadsheetsDb;

            // Load spreadsheet containing users db.
            await db.LoadSpreadsheetAsync(userRepository.UsersDB_SpreadsheetId);

            // Get live spreadsheet containing users db.
            LiveSpreadsheet usersDbSpreadsheet = db[userRepository.UsersDB_SpreadsheetId];

            // Get sheet containing users db, and assign it into the appropriate field.
            userRepository._usersDbSheet = usersDbSpreadsheet[UserRepository.UsersDB_SheetId];

            // Constructs username index associating each username with appropriate row index.
            userRepository.ReBuildUsernameIndex();

            //
            return userRepository;
        }
        #endregion

        #region Methods - UserRepository
        public void ReObtainDBData()
        {
            // Users DB thread safety lock
            lock (this.UsersDBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Re-obtain users sheet data from google servers
                this._usersDbSheet.ReObtainData(db);

                // Clears and recreates username-rowIndex UserRepository associations dictionary
                this.ReBuildUsernameIndex();
            }
        }

        public async Task AssureAllLoadedAsync(CancellationToken? token = null)
        {
            // WARNING - DO NOT ENCAPSULATE IN THREAD SAFETY LOCK OR YOU WILL PREVENT 
            // OTHER THERDS FROM EDITING COMPLEATELY TILL ALL DATA HAVE BEEN ASSURED

            try
            {
                // Loop through all the usernames in the username-row index relativity index
                foreach (KeyValuePair<string, int> rowIndexByUsername in this._rowIndexesByUsernames)
                {
                    // Wait 100ms to allow other threads edit
                    // thread safety lock is applied most of the time within
                    // this.AssureUserDataIsLoaded(username) method
                    await Task.Delay(10);

                    // If cancellation has been requested throw OperationCanceledException
                    token?.ThrowIfCancellationRequested();

                    // Get currently looped through username
                    string username = rowIndexByUsername.Key;

                    // Try to assure that user data is loaded using currently looped through username...
                    try { await Task.Run(() => this.AssureUserDataIsLoaded(username)); }
                    // ... and swallow exception if it has been specifically caused by username
                    // unrecognized in the UserRepository. (That should happened only if user have been
                    // removed from other thread then current method.)
                    catch (ArgumentException ex) when (ex.ParamName == "username") { }

                }
            }
            // If this._rowIndexesByUsernames has been modified by other thread,
            // retry assuring that all users spreadsheets are loaded.
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

        public void AssureUserDataIsLoaded(string username)
        {
            // Validate parameters
            if (username is null) throw new ArgumentNullException();
            else if (!this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Get current user spreadsheet id.
                string userSpreadsheetId = this.GetUserLogSpreadsheetId(username);

                // Assures that specified user spreadsheet will get loaded (metadata)
                this.AssureUserDataIsLoadedFromSpecificSpreadsheet(db, userSpreadsheetId);
            }                
        }
        #endregion

        #region IUserRepository - Methods
        #region Create
        // Create user base on the provided parameters
        public void Create(string username, string firstName, string surname, UserType type)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (firstName is null) throw new ArgumentNullException(nameof(firstName));
            else if (surname is null) throw new ArgumentNullException(nameof(surname));
            else if (this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is associated with existing user in the UserRepository.", nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // 1) Add current user log public spreadsheet
                this.CreateNewUserLogSpreadsheet(out string spreadsheetId, db, username, firstName, surname, type);

                // 2) Add user into the user db sheet
                this.CreateNewUserDbEntry(db, username, firstName, surname, type, spreadsheetId);

                // 3) Get that spreadsheet ...
                LiveSpreadsheet spreadsheet = db[spreadsheetId];

                // ... and use it to build instance of necessary EventArgs class
                // to trigger UserSpreadsheetAdded event.
                this.UserSpreadsheetAdded?.Invoke(this, new UserSpreadsheetAddedEventArgs(spreadsheet));
            }
        }

        #region this.Create(string username, string firstName, string surname, UserType type)
        // Creates new publicly viewable UserLog spreadsheet and adds it into the DBIndex of all the spreadsheets owned by the db
        private void CreateNewUserLogSpreadsheet(out string spreadsheetId, LiveSpreadsheetsDb db, string username, string firstName, string surname, UserType type)
        {
            // Get string representation of the UserType integer value.
            string usertype = type.ToValueString();

            // Get user spreadsheet blueprint.
            IList<Tuple<string, int, int, IList<IList<string>>>> userSpreadsheetBlueprint = this.GetUserSpreadsheetBlueprint(out DateTime now, out string spraedsheetTitle, username, firstName, surname, usertype);

            // Add new user spreadsheet into the spreadsheet database.
            db.AddSpreadsheet(out spreadsheetId, spraedsheetTitle, userSpreadsheetBlueprint);

            // Assures that newly created user log spreadsheet can be view by anyone.
            db.AssureSpreadsheetViewIsPublic(spreadsheetId);

            // Create columns ranges dimension adjustment index
            IList<Tuple<int, int, int>> columnsRangesDimensionsAdjustmentBlueprint = new List<Tuple<int, int, int>>();
            
            // Get current, just created saturn log sheet to adjust its columns width dimensions
            LiveSheet userCurrentLogSheetShell = db[spreadsheetId][this.GetUserLogCurrentSheetTitle(now.Month)];
            // Assure width of timestamp column.
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(UserRepository.UserLog_LogsSheet_TimestampColumnIndex, 1, UserRepository.UserLog_LogsSheet_TimestampColumnWidth));
            columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(UserRepository.UserLog_LogsSheet_LogColumnIndex, 1, UserRepository.UserLog_LogsSheet_LogColumnWidth));

            // Adjust new UserLog ranges columns with
            userCurrentLogSheetShell.AdjustMultipleColumnsRangesWidthDiemansions(db, columnsRangesDimensionsAdjustmentBlueprint);

        }

        private void CreateNewUserDbEntry(LiveSpreadsheetsDb db, string username, string firstName, string surname, UserType type, string spreadsheetId)
        {
            // Get string representation of the UserType integer value.
            string usertype = type.ToValueString();

            // Get index of the first unexisting row in the users db sheet
            int newUserRowIndex = this._usersDbSheet.RowCount;
            // Append users db sheet content with new user entry
            this._usersDbSheet.InsertRows(db, newUserRowIndex, UserRepository.UserDB_NewEntryRowsCount, new IList<object>[UserRepository.UserDB_NewEntryRowsCount]
                { new object[UserRepository.UserDB_NewEntryColumnsCount] { username, firstName, surname, usertype, spreadsheetId } });

            // Creates username-rowIndex association in the repository for the newly created 
            this._rowIndexesByUsernames.Add(username, newUserRowIndex);
        }
        #endregion
        #endregion

        #region Read
        // Answers the question whether the User associated with specific username can be found in the current UserRepository
        public bool HasUsername(string username)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
                return this._rowIndexesByUsernames.ContainsKey(username);
        }

        public IEnumerable<string> GetAllUsernames()
        {
            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
                return this._rowIndexesByUsernames.Keys;
        }

        // Returns first name of the user associated with the provided username.
        public string GetUserFirstName(string username)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (!this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {            
                // Get row index related with the provided username
                int usernameAssociatedRowIndex = this._rowIndexesByUsernames[username];

                // Obtain string representing user first name
                return this._usersDbSheet[usernameAssociatedRowIndex][UserRepository.UserLog_Metadata_FirstNameValue_ColumnIndex].GetDataAsString();
            }
        }

        // Returns surname of the user associated with the provided username.
        public string GetUserSurname(string username)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (!this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get row index related with the provided username
                int usernameAssociatedRowIndex = this._rowIndexesByUsernames[username];

                // Obtain string representing user surname
                return this._usersDbSheet[usernameAssociatedRowIndex][UserRepository.UserLog_Metadata_SurnameValue_ColumnIndex].GetDataAsString();
            }
        }

        // Returns UserType of the existing user
        public UserType GetUserType(string username)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (!this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get row index related with the provided username
                int usernameAssociatedRowIndex = this._rowIndexesByUsernames[username];

                // Obtain string representing appropriate integer/UserType value...
                return this._usersDbSheet[usernameAssociatedRowIndex][UserRepository.UsersDB_Type_ColumnIndex].GetDataAsString()
                    // and returns enum which value has been established based on the integer value parse from that string. (Invalid parse returns UserType.User)
                    .ValueToUserType();
            }
        }

        // Returns currently assigned user log spreadsheet id of the existing user
        public string GetUserLogSpreadsheetId(string username)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (!this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get row index related with the provided username
                int usernameAssociatedRowIndex = this._rowIndexesByUsernames[username];

                // Obtain string representing user spreadsheet id
                return this._usersDbSheet[usernameAssociatedRowIndex][UserRepository.UsersDB_UserSpreadsheetId_ColumnIndex].GetDataAsString();
            }
        }

        // Returns user associated with provided username.
        public User Read(string username)
        {
            // Parameter Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (!this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));


            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get row index related with the provided username
                int usernameAssociatedRowIndex = this._rowIndexesByUsernames[username];

                // Get data stored on the row associated with appropriate row index.
                IList<string> userRowData = this._usersDbSheet[usernameAssociatedRowIndex].GetDataAsStrings();

                // Obtain integer value representing appropriate UserType from parsed string
                UserType userType = userRowData[UserRepository.UsersDB_Type_ColumnIndex].ValueToUserType();

                // Create and return new instance of User
                return new User(
                    // Username
                    userRowData[UserRepository.UsersDB_Username_ColumnIndex],
                    // First name
                    userRowData[UserRepository.UsersDB_FirstName_ColumnIndex],
                    // Surname
                    userRowData[UserRepository.UsersDB_Surname_ColumnIndex],
                    // Type
                    userType);
            }
        }

        // Return all the users stored by the 
        public IList<User> ReadAll()
        {
            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Declare users placeholder list.
                IList<User> users = new List<User>();

                // For each username associated with row index...
                foreach (int usernameAssociatedRowIndex in this._rowIndexesByUsernames.Values)
                {
                    // Get data stored on the row associated with appropriate row index.
                    IList<string> userRowData = this._usersDbSheet[usernameAssociatedRowIndex].GetDataAsStrings();

                    // Obtain integer value representing appropriate UserType from parsed string
                    UserType userType = userRowData[UserRepository.UsersDB_Type_ColumnIndex].ValueToUserType();

                    // User individual spreadsheet id.
                    string userSpreadsheetId = userRowData[UserRepository.UsersDB_UserSpreadsheetId_ColumnIndex];

                    User user = new User(
                    // User username
                    userRowData[UserRepository.UsersDB_Username_ColumnIndex],
                    // User first name
                    userRowData[UserRepository.UsersDB_FirstName_ColumnIndex],
                    // User surname
                    userRowData[UserRepository.UsersDB_Surname_ColumnIndex],
                    // Type
                    userType);

                    // Add user to users placeholder list
                    users.Add(user);
                }

                // Return constructed list of all users
                return users;
            }
        }
        #endregion

        #region Update
        // Append current user log sheet with entry build based on the provided userLog string.
        public void AddUserLog(string username, string userLog)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (userLog is null) throw new ArgumentNullException(nameof(userLog));
            else if (!this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get user spreadsheet id
                string userSpreadsheetId = this.GetUserLogSpreadsheetId(username);

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Obtain current time
                DateTime now = DateTime.Now;

                // Get user year-associated user spreadsheet shell.
                LiveSpreadsheet userSpreadsheetShell = this.GetUserLogSpreadsheet(db, userSpreadsheetId, username, now.Year);

                // Get current user log sheet to append the provided log to
                LiveSheet userCurrentLogSheetShell = this.GetUserLogSheet(db, userSpreadsheetShell, now.Month, now.ToTimestamp());


                // Create user log row data.
                IList<IList<object>> userLogRowData = new IList<object>[1] { new string[UserRepository.UserLog_LogsSheetColumnsCount] { now.ToTimestamp(), userLog } };

                // Append user individual log sheet with new row, representing provided userLog. 
                userCurrentLogSheetShell.AppendRows(db, 1, userLogRowData);

                // After user log sheet data has been committed into the google service
                // clear the data from the LiveSheet as they are not needed, and over time
                // when combined the amount of memory needed to store 
                // all the logs from all the users would be to much.
                userCurrentLogSheetShell[userCurrentLogSheetShell.BottomIndex].ClearData();
            }
        }

        // Update existing user properties
        public void Update(string username, string firstName = null, string surname = null, UserType? type = null)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (!this.HasUsername(username)) 
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // 1) Update user data in the UserDb
                this.UpdateUserInUsersDbLiveSheet(db, username, firstName, surname, type);

                // 2) Update that data in the user log spreadsheet metadata sheet.
                this.UpdateUserLogMetadataForUserUpdate(db, username, firstName, surname, type);
            }
        }

        #region this.Update(string username, string firstName = null, string surname = null, UserType? type = null, string userSpreadshetId = null) - Private Helpers
        // 1) Update user data in the UserDb
        private void UpdateUserInUsersDbLiveSheet(LiveSpreadsheetsDb db, string username, string firstName, string surname, UserType? type)
        {
            // Get user row associated with the provided username
            int usernameAssociatedRowIndex = this._rowIndexesByUsernames[username];
            LiveRow userRow = this._usersDbSheet[usernameAssociatedRowIndex];

            // Build update user row data
            IList<string> userRowNewData = new string[this._usersDbSheet.ColumnCount];
            userRowNewData[UserRepository.UsersDB_Username_ColumnIndex] = null;
            userRowNewData[UserRepository.UsersDB_FirstName_ColumnIndex] = firstName;
            userRowNewData[UserRepository.UsersDB_Surname_ColumnIndex] = surname;
            userRowNewData[UserRepository.UsersDB_Type_ColumnIndex] = type?.ToValueString();
            userRowNewData[UserRepository.UsersDB_UserSpreadsheetId_ColumnIndex] = null;

            // Upload user row with constructed/updated data.
            userRow.SetDataFromStringsData(0, userRowNewData);
            // ... and update data on the google spreadsheet.
            userRow.Upload(db);
        }
        
        // 2) Update that data in the user log spreadsheet metadata sheet.
        private void UpdateUserLogMetadataForUserUpdate(LiveSpreadsheetsDb db, string username, string firstName, string surname, UserType? type)
        {
            string userlogSpreadsheetId = this.GetUserLogSpreadsheetId(username);

            // Get current user log spreadsheet.
            LiveSpreadsheet curentUserLogSpreadsheet = this.GetUserLogSpreadsheet(db, userlogSpreadsheetId, username, DateTime.Now.Year);

            // Get current user log spreadsheet.
            LiveSheet currentUserLogMetadataSheet = curentUserLogSpreadsheet[UserRepository.UserLog_Metadata_SheetTitle];

            // Build update user row data
            IList<string> metadataValuesRowNewData = new string[UserRepository.UserLog_Metadata_ColumnsCount];
            metadataValuesRowNewData[UserRepository.UserLog_Metadata_UsernameValue_ColumnIndex] = null;
            metadataValuesRowNewData[UserRepository.UserLog_Metadata_FirstNameValue_ColumnIndex] = firstName;
            metadataValuesRowNewData[UserRepository.UserLog_Metadata_SurnameValue_ColumnIndex] = surname;
            metadataValuesRowNewData[UserRepository.UserLog_Metadata_TypeValue_ColumnIndex] = type?.ToValueString();
            metadataValuesRowNewData[UserRepository.UserLog_Metadata_YearValue_ColumnIndex] = null;
            metadataValuesRowNewData[UserRepository.UserLog_Metadata_PreviousSpreadsheetIdValue_ColumnIndex] = null;

            // Set metadata sheet values row cells values and upload the changes into the google servers.
            LiveRow metadataValuesRow = currentUserLogMetadataSheet[UserRepository.UserLog_Metadata_ValuesRowIndex];
            metadataValuesRow.SetDataFromStringsData(0, metadataValuesRowNewData);
            metadataValuesRow.Upload(db);
        }
        #endregion
        #endregion

        #region Delete
        // Removes existing, non-last user.
        public void Delete(string username)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            else if (!this.HasUsername(username))
                throw new ArgumentException($"Provided username: {username} is not recognized by the UserRepository.", nameof(username));
            else if (this._usersDbSheet.RowCount == 1) throw new InvalidOperationException("Last user cannot be removed.");

            // UsersDBLock thread safety lock
            lock (this.UsersDBLock)
            {
                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // 1) Remove all the user log spreadsheet associated with the provided username.
                // (one google sheet service request per user log spreadsheet associated with the provided username)
                this.DeleteUserLogSpreadsheets(db, username);

                // 2) Remove the row from user db sheet, representing user associated with the provided username.
                // (one google sheet service request)
                this.DeleteUserRowFromUserDbSheet(db, username);

                // 3) Adjust content of the user repository row indexes by username dictionary.
                this.ReBuildUsernameIndex();

                // 4) Execute appropriate event to let all the subscribers know that User associated with the specified username has been removed.
                this.UserSpreadsheetRemoved?.Invoke(this, new UserSpreadsheetRemovedEventArgs(username));

            }
        }

        #region this.Delete(string username) - Private Helpers
        // 1) Remove all the user log spreadsheet associated with the provided username.
        // (one google sheet service request per user log spreadsheet associated with the provided username)
        private void DeleteUserLogSpreadsheets(LiveSpreadsheetsDb db, string username)
        {            
            // Get all the user related log spreadsheets and remove them.
            IList<string> userlogSpreadsheetsIds = this.GetUserCurrentLogSpreadsheetsIds(db, username);

            // Remove all user log spreadsheets.
            foreach (string userlogSpreadsheetId in userlogSpreadsheetsIds)
                if (!(userlogSpreadsheetId is null) && userlogSpreadsheetId != "")
                    // Remove and dispose user associated with currently looped through userlogSpreadsheetId.
                    db.RemoveSpreadsheet(userlogSpreadsheetId);
        }

        // 2) Remove the row from user db sheet, representing user associated with the provided username.
        // (one google sheet service request)
        private void DeleteUserRowFromUserDbSheet(LiveSpreadsheetsDb db, string username)
        {
            // Get user row associated with the provided username
            int usernameAssociatedRowIndex = this._rowIndexesByUsernames[username];

            // Remove entry 
            this._usersDbSheet.RemoveRows(db, usernameAssociatedRowIndex, 1);
        }
        #endregion
        #endregion
        #endregion

        #region Private Helpers
        // Clears and builds from scratch dictionary of username-rowIndex association.
        public void ReBuildUsernameIndex()
        {
            // Clear all the UserRepository existing associations username-rowIndex
            this._rowIndexesByUsernames.Clear();

            // Loop through all non-empty rows in UsersDB and create username-rowIndex association in the repository for each.
            foreach (LiveRow userRow in this._usersDbSheet.SheetRows)
                // Add row index, indexed by the associated username.
                this._rowIndexesByUsernames.Add(userRow[UsersDB_Username_ColumnIndex].GetDataAsString(), userRow.RowIndex);
        }
        
        private void AssureUserDataIsLoadedFromSpecificSpreadsheet(LiveSpreadsheetsDb db, string userSpreadsheetId)
        {
            // Check whether database has at least shell of appropriate spreadsheet and if not...
            if (!db.HasSpreadsheetShell(userSpreadsheetId))
            {
                // .. load spreadsheet partially - metadata - ...
                db.LoadSpreadsheetPartially(userSpreadsheetId, new string[1] { UserRepository.UserLog_Metadata_SheetTitle });

                // ... get that spreadsheet ...
                LiveSpreadsheet spreadsheet = db[userSpreadsheetId];

                // ... and use it to build instance of necessary EventArgs class
                // to trigger UserSpreadsheetLoaded event. (If anything assigned to it)
                this.UserSpreadsheetLoaded?.Invoke(this, new UserSpreadsheetLoadedEventArgs(spreadsheet));
            }
        }

        // Find first empty row index
        private int GetFirstEmptyRowIndex()
        {
            for (int i = 0; i < this._usersDbSheet.RowCount; i++)
                if (this._usersDbSheet[i][UserRepository.UsersDB_Username_ColumnIndex].GetDataAsString() is null)
                    return i;

            // If no row have been found empty, throw appropriate exception
            throw new InvalidOperationException("Users database sheet is full.");
        }

        // Returns year-associated user log spreadsheet.
        private LiveSpreadsheet GetUserLogSpreadsheet(LiveSpreadsheetsDb db, string userSpreadsheetId, string username, int nowYear)
        {
            this.AssureUserDataIsLoadedFromSpecificSpreadsheet(db, userSpreadsheetId);

            // Get user spreadsheet shell
            LiveSpreadsheet userSpreadsheetShell = db.LoadedSpreadsheets[userSpreadsheetId];

            LiveCell userlogYearCell = userSpreadsheetShell[UserRepository.UserLog_Metadata_SheetTitle]
                [UserRepository.UserLog_Metadata_YearValue_RowIndex]
                [UserRepository.UserLog_Metadata_YearValue_ColumnIndex];

            if (!userlogYearCell.HasValue())
                userlogYearCell.ReObtainData(db);
            
            // If current year is not equal...
            if (nowYear.ToString("D4") !=
                // ... with content of Year-Value cell in user log spreadsheet, metadata sheet.
                userlogYearCell.GetDataAsString())
            {
                // Get row index related with the provided username
                int usernameAssociatedRowIndex = this._rowIndexesByUsernames[username];
                LiveRow userRow = this._usersDbSheet[usernameAssociatedRowIndex];

                // Obtain user new spreadsheet blueprint...
                IList<Tuple<string, int, int, IList<IList<string>>>> newUserSpreadsheetBlueprint 
                    = this.GetUserSpreadsheetBlueprint(
                    // (Blueprint timestamp - out parameter)
                    out DateTime now, 
                    // ... obtain new user log spreadsheet title... (out parameter)
                    out string newUserSpreadsheetTitle,
                    // Assign user username
                    userRow[UserRepository.UsersDB_Username_ColumnIndex].GetDataAsString(),
                    // assigns user first name
                    userRow[UserRepository.UsersDB_FirstName_ColumnIndex].GetDataAsString(),
                    // assigns user surname
                    userRow[UserRepository.UsersDB_Surname_ColumnIndex].GetDataAsString(),
                    // assigns user type
                    userRow[UserRepository.UsersDB_Type_ColumnIndex].GetDataAsString(),
                    // assign spreadsheet id of the currently used and outdated 
                    // user spreadsheet log as a "Last year user log spreadsheet id"
                    userSpreadsheetShell.SpreadsheetId);

                // Add new user spreadsheet into the spreadsheet database.
                db.AddSpreadsheet(out string newUserSpreadsheetId, newUserSpreadsheetTitle, newUserSpreadsheetBlueprint);
                
                // Assures that newly created user log spreadsheet can be view by anyone.
                db.AssureSpreadsheetViewIsPublic(newUserSpreadsheetId);

                // Replace user-related user log spreadsheet id with new one.
                this.SetUserDBUserSpreadsheetId(db, userRow, newUserSpreadsheetId);

                // assign newly created spreadsheet into user spreadsheet shell to be returned by the method.
                userSpreadsheetShell = db[newUserSpreadsheetId];

                // Invoke appropriate event to let all the subscribers know that the personal User Spreadsheet has been replaced.
                this.UserSpreadsheetReplaced?.Invoke(this, new UserSpreadsheetReplacedEventArgs(userSpreadsheetShell));
            }

            // returns user spreadsheet shell.
            return userSpreadsheetShell;
        }

        // Replace user-related user log spreadsheet id with new one. Provided userRow
        // must represent user-related UserDb sheet row.
        private void SetUserDBUserSpreadsheetId(LiveSpreadsheetsDb db, LiveRow userRow, string newUserSpreadsheetId)
        {
            // Set data of the user db user spreadsheet cell...
            userRow[UserRepository.UsersDB_UserSpreadsheetId_ColumnIndex].SetData(newUserSpreadsheetId);

            // .. and update cell data into the google servers.
            userRow[UserRepository.UsersDB_UserSpreadsheetId_ColumnIndex].Upload(db);
        }

        // Returns month-associated user log sheet.
        private LiveSheet GetUserLogSheet(LiveSpreadsheetsDb db, LiveSpreadsheet userSpreadsheetShell, int nowMonth, string timestamp)
        {
            // Get user log sheet title id - mount dependent.
            string currentUserLogSheetTitleId = this.GetUserLogCurrentSheetTitle(nowMonth);

            // If current user log spreadsheet doesn't contain sheet with appropriate title...
            if (!userSpreadsheetShell.ContainsKey(currentUserLogSheetTitleId))
            {
                // ... add such a sheet.
                userSpreadsheetShell.AddSheet<IList<string>, string>(db, currentUserLogSheetTitleId,
                    UserRepository.UserLog_LogsSheetColumnsCount, 1,
                    new IList<string>[1] { UserRepository.UserLog_LogsSheetInitialRowContent });
                
                // Create columns ranges dimension adjustment index
                IList<Tuple<int, int, int>> columnsRangesDimensionsAdjustmentBlueprint = new List<Tuple<int, int, int>>();

                // Create and add adjust issues sheet columns dimensions blueprints
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(UserRepository.UserLog_LogsSheet_TimestampColumnIndex, 1, UserRepository.UserLog_LogsSheet_TimestampColumnWidth));
                columnsRangesDimensionsAdjustmentBlueprint.Add(new Tuple<int, int, int>(UserRepository.UserLog_LogsSheet_LogColumnIndex, 1, UserRepository.UserLog_LogsSheet_LogColumnWidth));
                LiveSheet userCurrentLogSheetShell = userSpreadsheetShell[currentUserLogSheetTitleId];
                userCurrentLogSheetShell.AdjustMultipleColumnsRangesWidthDiemansions(db, columnsRangesDimensionsAdjustmentBlueprint);
            }

            // Get current user log sheet to append the provided log to
            return userSpreadsheetShell[currentUserLogSheetTitleId];
        }

        private string GetUserLogCurrentSpreadsheetTitle(string username, DateTime now)
        {
            // Return individual spreadsheet title
            return $"UserLog{now.Year}_{username}";
        }

        private string GetUserLogCurrentSheetTitle(int month)
        {
            // Return individual sheet title
            return $"Log_{month:D2}";
        }

        private IList<string> GetUserCurrentLogSpreadsheetsIds(LiveSpreadsheetsDb db, string username)
        {
            // Parameters Validation
            if (username is null) throw new ArgumentNullException(nameof(username));
            if (!this.HasUsername(username)) throw new ArgumentException($"Specified user {username} is not recognized.", nameof(username));
            
            // Declare new list containing current user log spreadsheet id.
            IList<string> userlogSpreadsheetsIds = new List<string>(new string[1] { this.GetUserLogSpreadsheetId(username)});

            // Declare string variable designated to store the value of the previous user log spreadsheet id.
            string previousSpreadsheetId = null;

            // Loop through till no more previous user log spreadsheet id can be found.
            do
            {
                // Get spreadsheet id representing spreadsheet containing user log from the year
                // prior to the year of the user log which is last member of the userlogSpreadsheetsIds list.
                previousSpreadsheetId = this.GetUserLogPreviousSpreadsheetId(db, userlogSpreadsheetsIds.Last());

                // If currently looped through spreadsheet doest contain spreadsheet id
                // of its previous user log spreadsheet id, return spreadsheets ids list.
                if (previousSpreadsheetId is null)
                    return userlogSpreadsheetsIds;
                // Otherwise add obtained user log previous spreadsheet id to spreadheetsIds list.
                else
                    userlogSpreadsheetsIds.Add(previousSpreadsheetId);
            }
            while (!(previousSpreadsheetId is null));

            return userlogSpreadsheetsIds;
        }

        private string GetUserLogPreviousSpreadsheetId(LiveSpreadsheetsDb db, string spreadsheetId)
        {
            // Parameters validation
            if (spreadsheetId is null) throw new ArgumentNullException(nameof(spreadsheetId));

            // If spreadsheet associated with provided spreadsheet id is not recognized return null.
            if (!db.HasSpreadsheetId(spreadsheetId))
                return null;
            // If spreadsheet id is recognized, but spreadsheet, or at least it's shell is not even loaded...
            else if (!db.HasSpreadsheetShell(spreadsheetId))
                // ... load user log spreadsheet shell associated with provided spreadsheet id.
                db.LoadSpreadsheetShell(spreadsheetId);

            // Get user log spreadsheet (or its shell) associated with provided spreadsheet id.
            LiveSpreadsheet spreadsheet = db[spreadsheetId];

            // Get metadata sheet.
            LiveSheet metadataSheet = spreadsheet[UserRepository.UserLog_Metadata_SheetTitle];

            // Obtain cell containing spreadsheet id of the user log for the year prior to the year of the user log associated with the provided spreadsheet id...
            LiveCell previousSpreadsheetIdCell = metadataSheet[UserRepository.UserLog_Metadata_PreviousSpreadsheetIdValue_RowIndex][UserRepository.UserLog_Metadata_PreviousSpreadsheetIdValue_ColumnIndex];
            // .. and check whether it holds any value (it will not hold any value if only shell of the spreadsheet has been loaded),
            // if obtained instance of previousSpreadsheetIdCell doesn't hold any value...
            if (!previousSpreadsheetIdCell.HasValue())
                // Obtain data from the google servers.
                previousSpreadsheetIdCell.ReObtainData(db);

            // Return spreadsheet id of the previous user log spreadsheet.
            // (This will return null if the provided spreadsheet id 
            // representing spreadsheet containing users original
            // user log spreadsheet created on user addition.)
            return previousSpreadsheetIdCell.GetDataAsString();
        }

        #region Build user log spreadsheet blueprint
        // Build blueprint of the user spreadsheet.
        private IList<Tuple<string, int, int, IList<IList<string>>>> GetUserSpreadsheetBlueprint(out DateTime now, out string spreadsheetTitle, string username, string firstName, string surname, string type, string oldUserSpreadsheetId = null)
        {
            // Get current DateTime
            now = DateTime.Now;

            // Assign individual spreadsheet title
            spreadsheetTitle = this.GetUserLogCurrentSpreadsheetTitle(username, now);
            
            // Declares sheets blueprints placeholder
            List<Tuple<string, int, int, IList<IList<string>>>> spreadsheetBlueprint = new List<Tuple<string, int, int, IList<IList<string>>>>();

            // Add User log metadata sheet blueprint into the spreadsheet blueprint.
            spreadsheetBlueprint.Add(this.GetUserLogMetadataSheetBlueprint(username, firstName, surname, type, now, oldUserSpreadsheetId));

            // Add User log current sheet blueprint into the spreadsheet blueprint.
            spreadsheetBlueprint.Add(this.GetUserLogCurrentSheetBlueprint(username, firstName, surname, type, now));

            // Return spreadsheet blueprint
            return spreadsheetBlueprint;
        }

        private Tuple<string, int, int, IList<IList<string>>> GetUserLogMetadataSheetBlueprint(string username, string firstName, string surname, string type, DateTime now, string oldUserSpreadsheetId)
        {
            IList<IList<string>> metadataSheetData = new IList<string>[UserRepository.UserLog_Metadata_RowsCount]
            {
                new string[UserRepository.UserLog_Metadata_ColumnsCount] { "Username:", "First name:", "Surname:", "Type:", "Year", "Last year user log spreadsheet id:" },
                new string[UserRepository.UserLog_Metadata_ColumnsCount] { username, firstName, surname, type, now.Year.ToString(), oldUserSpreadsheetId }
            };

            return new Tuple<string, int, int, IList<IList<string>>>(UserRepository.UserLog_Metadata_SheetTitle, UserRepository.UserLog_Metadata_ColumnsCount, UserRepository.UserLog_Metadata_RowsCount, metadataSheetData);
        }

        private Tuple<string, int, int, IList<IList<string>>> GetUserLogCurrentSheetBlueprint(string username, string firstName, string surname, string type, DateTime now)
        {
            IList<IList<string>> logSheetData = new IList<string>[UserRepository.UserLog_LogsSheetInitialRowsCount]
            {
                UserRepository.UserLog_LogsSheetInitialRowContent
            };

            return new Tuple<string, int, int, IList<IList<string>>>(this.GetUserLogCurrentSheetTitle(now.Month), UserRepository.UserLog_LogsSheetColumnsCount, UserRepository.UserLog_LogsSheetInitialRowsCount, logSheetData);
        }
        #endregion
        #endregion

        #region IDisposable Support
        private bool disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.

                // Get live spreadsheets database reference.
                LiveSpreadsheetsDb db = this._dataRepository.GoogleService.SpreadsheetsDb;

                // Un-Load spreadsheet containing users db.
                db?.UnloadSpreadsheet(UsersDB_SpreadsheetId);

                // TODO: set large fields to null.
                this._usersDbSheet = null;
                
                disposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~UserRepository() {
          // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
          Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
             GC.SuppressFinalize(this);
        }
        #endregion
    }
}
