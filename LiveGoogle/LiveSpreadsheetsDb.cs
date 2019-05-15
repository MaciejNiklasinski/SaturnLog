using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using LiveGoogle.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace LiveGoogle.Sheets
{

    public class LiveSpreadsheetsDb : IDisposable
    {
        // TODO const into the config file

        //private const string SpreadsheetsDbSpreadsheetId = "1ErmTEZ0xR15T6Ly5PzEPXc73Zo8bA-zONrcL3FXTRLM";
        //private const string SpreadsheetsDbSpreadsheetId = "1KhG9_pSUgBz4kiQTaMT2SU_2y-EHlD7zcMvRutZF-x0";
        internal readonly string SpreadsheetsDbSpreadsheetId;

        private const string SpreadsheetsDbSheetTitleId = "Index";
        private const int SpreadsheetsDbSpradsheetIdColumnIndex = 0;
        private const int SpreadsheetsDbSpradsheetTitleColumnIndex = 1;

        private const string SpreadsheetsDbTimestampSheetTitleId = "Version Timestamp";

        private const int SpreadsheetsDbTimestampSheetColumnIndex = 0;

        private const int SpreadsheetsDbTimestampSheetRowIndex = 0;

        #region Private Fields/Properties
        // Database threadsafety lock
        private readonly SemaphoreSlim _dbMutex = new SemaphoreSlim(1,1);

        // Google apis sheets service.
        private SheetsService _apisSheetsService;

        // Google apis drive service.
        private DriveService _apisDriveService;

        // LiveSpreadsheet instance representing the live spreadsheets 
        private LiveSpreadsheet _dbSpreadsheetsIndexSpreadsheet;

        // LiveSpreadsheet instance representing the live spreadsheets 
        private LiveSheet DbSpreadsheetsIndexSheet
        { get { return this._dbSpreadsheetsIndexSpreadsheet[LiveSpreadsheetsDb.SpreadsheetsDbSheetTitleId]; } }
        #endregion

        #region Properties
        // Google apis sheets service.
        internal SheetsService ApisSheetsService { get { return this._apisSheetsService; } }

        // List of all the LiveSpreadsheets contained by the current LiveSpreadsheetDb (apart of the db spreadsheets index itself)
        public IDictionary<string, LiveSpreadsheet> LoadedSpreadsheets
        {
            get
            {
                // Obtain all existing spreadsheets...
                IDictionary<string, LiveSpreadsheet> spreadsheets = LiveSpreadsheet.Factory.GetAllExisting();

                // ... remove one representing db spreadsheets index..
                spreadsheets.Remove(this.SpreadsheetsDbSpreadsheetId);

                // ... and return the dictionary.
                return spreadsheets;
            }
        }
        #endregion

        #region Indexers
        // Returns spreadsheet associated with provided id.
        public LiveSpreadsheet this[string spreadsheetId] { get { return LiveSpreadsheet.Factory.GetExistingSpreadsheet(spreadsheetId); } }
        #endregion

        #region Constructors
        public LiveSpreadsheetsDb(Google.Apis.Sheets.v4.SheetsService apisSheetsService, Google.Apis.Drive.v3.DriveService apisDriveService, string spreadsheetId)
        {
            // Assign DBIndex spreadsheet id.
            this.SpreadsheetsDbSpreadsheetId = spreadsheetId;

            // Assign provided google apis sheets service
            this._apisSheetsService = apisSheetsService;

            // Assign provided google apis sheets service
            this._apisDriveService = apisDriveService;

            // Obtains database spreadsheets index.
            this.ObtainDBSpreadsheetsIndex();
        }
        #endregion

        #region Methods
        public void ReObtainDBSpreadsheetsIndex()
        {
            // Dispose current spreadsheets index.
            this._dbSpreadsheetsIndexSpreadsheet?.Dispose();

            // Obtains database spreadsheets index.
            this.ObtainDBSpreadsheetsIndex();
        }

        public async Task ReObtainDBSpreadsheetsIndexAsync()
        {
            // Dispose current spreadsheets index.
            this._dbSpreadsheetsIndexSpreadsheet?.Dispose();

            // Obtains database spreadsheets index.
            await this.ObtainDBSpreadsheetsIndexAsync();
        }

        public bool HasSpreadsheetShell(string spreadsheetId)
        {
            return this.LoadedSpreadsheets.ContainsKey(spreadsheetId);
        }


        public bool HasSpreadsheetId(string spreadsheetId)
        {
            return ((IList<LiveRow>)this.DbSpreadsheetsIndexSheet).Any((row) =>
            {
                return row[LiveSpreadsheetsDb.SpreadsheetsDbSpradsheetIdColumnIndex].GetDataAsString() == spreadsheetId;
            });
        }

        #region Assure Spreadsheet permission.
        public void AssureSpreadsheetViewIsPublic(string spreadsheetId)
        {
            if (!this.HasSpreadsheetId(spreadsheetId))
                throw new InvalidOperationException("Unable to load spreadsheet shell using unrecognized spreadsheet id.");

            this._apisDriveService.AssureSpreadsheetViewIsPublicSync(spreadsheetId);
        }

        public void AssureSpreadsheetViewIsDomainPermitted(string spreadsheetId, string domain)
        {
            if (!this.HasSpreadsheetId(spreadsheetId))
                throw new InvalidOperationException("Unable to load spreadsheet shell using unrecognized spreadsheet id.");

            this._apisDriveService.AssureSpreadsheetViewIsDomainPermittedSync(spreadsheetId, domain);
        }

        public void AssureSpreadsheetViewIsEmailPermitted(string spreadsheetId, string email)
        {
            if (!this.HasSpreadsheetId(spreadsheetId))
                throw new InvalidOperationException("Unable to load spreadsheet shell using unrecognized spreadsheet id.");

            this._apisDriveService.AssureSpreadsheetViewIsEmailPermittedSync(spreadsheetId, email);
        }

        public void AssureSpreadsheetEditIsPublic(string spreadsheetId)
        {
            if (!this.HasSpreadsheetId(spreadsheetId))
                throw new InvalidOperationException("Unable to load spreadsheet shell using unrecognized spreadsheet id.");

            this._apisDriveService.AssureSpreadsheetIsEditPublicSync(spreadsheetId);
        }

        public void AssureSpreadsheetEditIsDomainPermitted(string spreadsheetId, string domain)
        {
            if (!this.HasSpreadsheetId(spreadsheetId))
                throw new InvalidOperationException("Unable to load spreadsheet shell using unrecognized spreadsheet id.");

            this._apisDriveService.AssureSpreadsheetEditIsDomainPermittedSync(spreadsheetId, domain);
        }

        public void AssureSpreadsheetEditIsEmailPermitted(string spreadsheetId, string email)
        {
            if (!this.HasSpreadsheetId(spreadsheetId))
                throw new InvalidOperationException("Unable to load spreadsheet shell using unrecognized spreadsheet id.");

            this._apisDriveService.AssureSpreadsheetEditIsEmailPermittedSync(spreadsheetId, email);
        }
        #endregion

        #region LoadSpreadsheet
        public void LoadSpreadsheetShell(string spreadsheetId)
        {
            this._dbMutex.Wait();

            try
            {
                if (!(this.HasSpreadsheetId(spreadsheetId)))
                    throw new ArgumentException("Unable to load spreadsheet shell using unrecognized spreadsheet id.", nameof(spreadsheetId));

                Spreadsheet spreadsheetShellData = this._apisSheetsService.GetSpreadsheetShellSync(spreadsheetId);

                LiveSpreadsheet.Factory.Get(spreadsheetShellData);
            }
            finally { this._dbMutex.Release(); }
        }

        public async Task LoadSpreadsheetShellAsync(string spreadsheetId)
        {
            await this._dbMutex.WaitAsync();

            try
            {
                if (!(this.HasSpreadsheetId(spreadsheetId)))
                    throw new ArgumentException("Unable to load spreadsheet shell using unrecognized spreadsheet id.", nameof(spreadsheetId));

                Spreadsheet spreadsheetShellData = await this._apisSheetsService.GetSpreadsheetShellAsync(spreadsheetId);

                LiveSpreadsheet.Factory.Get(spreadsheetShellData);
            }
            finally { this._dbMutex.Release(); }
        }

        public void LoadSpreadsheetPartially(string spreadsheetId, string[] sheetTitleIds)
        {
            this._dbMutex.Wait();

            try
            {
                if (!(this.HasSpreadsheetId(spreadsheetId)))
                    throw new ArgumentException("Unable to load spreadsheet partially using unrecognized spreadsheet id.", nameof(spreadsheetId));

                Spreadsheet spreadsheetPartialData = this._apisSheetsService.GetSpreadsheetPartiallySync(spreadsheetId, sheetTitleIds);

                LiveSpreadsheet.Factory.Get(spreadsheetPartialData);
            }
            finally { this._dbMutex.Release(); }
        }

        public async Task LoadSpreadsheetPartiallyAsync(string spreadsheetId, string[] sheetTitleIds)
        {
            await this._dbMutex.WaitAsync();

            try
            {
                if (!(this.HasSpreadsheetId(spreadsheetId)))
                    throw new ArgumentException("Unable to load spreadsheet partially using unrecognized spreadsheet id.", nameof(spreadsheetId));

                Spreadsheet spreadsheetPartialData = await this._apisSheetsService.GetSpreadsheetPartiallyAsync(spreadsheetId, sheetTitleIds);

                LiveSpreadsheet.Factory.Get(spreadsheetPartialData);
            }
            finally { this._dbMutex.Release(); }
        }

        public void LoadSpreadsheet(string spreadsheetId)
        {
            this._dbMutex.Wait();

            try
            {
                if (!(this.HasSpreadsheetId(spreadsheetId)))
                    throw new ArgumentException("Unable to load spreadsheet using unrecognized spreadsheet id.", nameof(spreadsheetId));

                Spreadsheet spreadsheetFullData = this._apisSheetsService.GetSpreadsheetSync(spreadsheetId);

                LiveSpreadsheet.Factory.Get(spreadsheetFullData);
            }
            finally { this._dbMutex.Release(); }
        }

        public async Task LoadSpreadsheetAsync(string spreadsheetId)
        {
            await this._dbMutex.WaitAsync();

            try
            {
                if (!(this.HasSpreadsheetId(spreadsheetId)))
                    throw new ArgumentException("Unable to load spreadsheet using unrecognized spreadsheet id.", nameof(spreadsheetId));

                Spreadsheet spreadsheetFullData = await this._apisSheetsService.GetSpreadsheetAsync(spreadsheetId);

                LiveSpreadsheet.Factory.Get(spreadsheetFullData);
            }
            finally { this._dbMutex.Release(); }
        }

        public void UnloadSpreadsheet(string spreadsheetId)
        {
            this._dbMutex.Wait();

            try
            {
                if (!(this.HasSpreadsheetId(spreadsheetId)))
                    throw new ArgumentException("Unable to unload spreadsheet using spreadsheet id which cannot be found in the database spreadsheets index.", nameof(spreadsheetId));

                this.LoadedSpreadsheets[spreadsheetId].Dispose();
            }
            finally { this._dbMutex.Release(); }
        }

        public async Task UnloadSpreadsheetAsync(string spreadsheetId)
        {
            await this._dbMutex.WaitAsync();

            try
            {
                if (!(this.HasSpreadsheetId(spreadsheetId)))
                    throw new ArgumentException("Unable to unload spreadsheet using spreadsheet id which cannot be found in the database spreadsheets index.", nameof(spreadsheetId));

                await Task.Run(() => {this.LoadedSpreadsheets[spreadsheetId].Dispose(); });
            }
            finally { this._dbMutex.Release(); }
        }
        #endregion

        #region Add/Remove Spreadsheet
        public void AddSpreadsheet(out string spreadsheetId, string spreadsheetTitle)
        {
            IList<Tuple<string, int, int>> spreadsheetSizeBlueprint = new List<Tuple<string, int, int>>();
            spreadsheetSizeBlueprint.Add(new Tuple<string, int, int>("Sheet1",  1, 1));

            this.AddSpreadsheet(out spreadsheetId, spreadsheetTitle, spreadsheetSizeBlueprint);
        }

        public void AddSpreadsheet(out string spreadsheetId, string spreadsheetTitle, IList<Tuple<string, int, int>> spreadsheetSizeBlueprint)
        {
            // Add Sheet.
            Spreadsheet spreadsheetData = this._apisSheetsService.AddSpreadsheetSync(spreadsheetTitle, spreadsheetSizeBlueprint);
            LiveSpreadsheet addedSpreadsheet = LiveSpreadsheet.Factory.Get(spreadsheetData);

            // Assign value of the out spreadsheetId parameter from constructed spreadsheet
            spreadsheetId = addedSpreadsheet.SpreadsheetId;

            // Construct row entry data - representing newly added spreadsheet in the database spreadsheet index.
            IList<string> rowData = new List<string>();
            rowData.Add(addedSpreadsheet.SpreadsheetId);
            rowData.Add(addedSpreadsheet.Title);

            // Insert row in the DBIndex representing newly created spreadsheet
            this.DbSpreadsheetsIndexSheet.InsertRows(this, this.DbSpreadsheetsIndexSheet.RowCount, 1, new IList<string>[1] { rowData });
        }

        public void AddSpreadsheet(out string spreadsheetId, string spreadsheetTitle, IList<Tuple<string, int, int, IList<IList<string>>>> spreadsheetBlueprint)
        {
            // Add Sheet.
            Spreadsheet spreadsheetData = this._apisSheetsService.AddSpreadsheetSync(spreadsheetTitle, spreadsheetBlueprint);
            LiveSpreadsheet addedSpreadsheet = LiveSpreadsheet.Factory.Get(spreadsheetData);
            
            // Assign value of the out spreadsheetId parameter from constructed spreadsheet
            spreadsheetId = addedSpreadsheet.SpreadsheetId;

            // Construct row entry data - representing newly added spreadsheet in the database spreadsheet index.
            IList<string> rowData = new List<string>();
            rowData.Add(addedSpreadsheet.SpreadsheetId);
            rowData.Add(addedSpreadsheet.Title);
            
            // Insert row in the DBIndex representing newly created spreadsheet
            this.DbSpreadsheetsIndexSheet.InsertRows(this, this.DbSpreadsheetsIndexSheet.RowCount, 1, new IList<string>[1] { rowData });
        }

        // Removes spreadsheet associated with provided spreadsheet id.
        public void RemoveSpreadsheet(string spreadsheetId)
        {
            // Get row index of the database spreadsheet index row containing entry associated with provided spreadsheet id.
            int spreadsheetIndex = this.GetSpreadsheetDbRowIndex(spreadsheetId);

            // Delete spreadsheet file from Google drive.
            this._apisDriveService.RemoveSpreadsheetSync(spreadsheetId);
            
            // Obtains the index of the row in the database spreadsheets index LiveSheet.
            this.DbSpreadsheetsIndexSheet.RemoveRows(this, spreadsheetIndex, 1);
        }
        #endregion
        #endregion

        #region Private Helpers
        // Obtains the current version of the db spreadsheets index.
        private void ObtainDBSpreadsheetsIndex()
        {
            // Get spreadsheets index, spreadsheet status.
            this._dbSpreadsheetsIndexSpreadsheet
                = LiveSpreadsheet.Factory.Get(this._apisSheetsService.GetSpreadsheetSync(this.SpreadsheetsDbSpreadsheetId), true, true);
        }

        // Obtains the current version of the db spreadsheets index.
        private async Task ObtainDBSpreadsheetsIndexAsync()
        {
            // Get spreadsheets index, spreadsheet status.
            this._dbSpreadsheetsIndexSpreadsheet
                = LiveSpreadsheet.Factory.Get(await this._apisSheetsService.GetSpreadsheetAsync(this.SpreadsheetsDbSpreadsheetId), true, true);
        }

        // Obtains the index of the row in the database spreadsheets index LiveSheet.
        private int GetSpreadsheetDbRowIndex(string spreadsheetId)
        {
            for (int rowIndex = 0; rowIndex < this.DbSpreadsheetsIndexSheet.RowCount; rowIndex++)
                if (this.DbSpreadsheetsIndexSheet[rowIndex][cellRowRelativeIndex: 0].GetDataAsString() == spreadsheetId)
                    return rowIndex;

            throw new ArgumentException($"Unable to find provided spreadsheetId {spreadsheetId} in the spreadsheets database", nameof(spreadsheetId));
        }
        #endregion

        #region IDisposable Support
        private bool disposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    // Obtain all of the loaded spreadsheets requiring destruction 
                    foreach (LiveSpreadsheet spreadsheet in LiveSpreadsheet.Factory.GetAllExisting().Values)
                        spreadsheet.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                this._apisSheetsService = null;
                this._dbSpreadsheetsIndexSpreadsheet = null;

                // Set disposed flag value to be true
                this.disposed = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
         ~LiveSpreadsheetsDb() {
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
