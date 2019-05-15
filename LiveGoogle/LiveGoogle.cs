using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Util.Store;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LiveGoogle.Sheets;
using LiveGoogle.Session;
using LiveGoogle.Exceptions;

namespace LiveGoogle
{
    public class LiveGoogle : IDisposable
    {
        #region Private Fields        
        private readonly string _dbIndexSpreadsheetId;
        private readonly string _sessionsSpreadsheetId;

        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json

        // Level of authority of google apis sheets service.
        private string[] _apisSheetsServiceScopes = { SheetsService.Scope.Drive };

        // Google apis user credentials
        private UserCredential _googleApisUser;

        // 
        //private CancellationTokenSource _googleApisUserTokenSource;

        // Google apis sheets service.
        private SheetsService _apisSheetsService;

        // Google apis drive service.
        private DriveService _apisDriveService;

        // Live Spreadsheets database.
        private LiveSpreadsheetsDb _spreadsheetsDb;

        // Responsible for activating, maintaining, deactivating when other application instance
        // will be attempting to start its own active session, or already started one.
        private SessionsRepository _sessionsRepository;

        private CancellationTokenSource _sessionTokenSource;
        #endregion

        #region Properties
        // Sessions repository
        internal SessionsRepository SessionsRepository { get { return _sessionsRepository; } }

        // Boolean flag indicating whether the google service has been successfully connected with google
        public bool? IsConnected { get; private set; } = false;

        // Seconds left till establishing new session will be possible. Null - unknown or never.
        public int? SecondsToNewSession { get { return this._sessionsRepository?.SecondsToNewSession; } }
        
        // Application name
        public string ApplicationName { get; }
        
        // Live Spreadsheets database.
        public LiveSpreadsheetsDb SpreadsheetsDb { get { return _spreadsheetsDb; } }
        #endregion

        #region Constructor
        public LiveGoogle(string applicationName, string dbIndexSpreadsheetId, string sessionsSpreadsheetId)
        {
            this._dbIndexSpreadsheetId = dbIndexSpreadsheetId;
            this._sessionsSpreadsheetId = sessionsSpreadsheetId;
            this.ApplicationName = applicationName;
        }
        #endregion

        #region Methods
        // OwnInstanceSessionIsActiveOrActivatingException - If live google is already connected or in a process of getting activated by the current instance of an application.

        // Thrown from StartNewSessionAsync only:
        // OperationCanceledException - On operation canceled...

        // OtherInstanceSessionAlreadyActiveException - When unable to start new session activation because other application instance currently owns active session, 
        // or failed during active session and didn't clean up 'Last' stamp after itself as recently that this stamp is not considered as unresponsive yet.

        // OtherInstanceSessionAlreadyStartingException - When unable to start new session activation because other application instance is currently in a process of starting new session, 
        // or failed during activation process and didn't clean up one of the 'ConnectedIn' stamps after itself as recently that this stamps are not considered as unresponsive yet.

        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        // SessionActivationInterruptedByOtherInstanceException - When ongoing new session activation has been interrupted and failed because other application instance,
        // either began the new session activation process, or activating session by updating value of 'Connected' stamp
        public async Task<Task> ConnectAsync(CancellationTokenSource cancellationTokenSource, bool forceOtherInstanceActiveSessionTakeover)
        {
            if (this.IsConnected != false)
                throw new OwnInstanceSessionIsActiveOrActivatingException();

            // Set connection state flag to null.
            this.IsConnected = null;

            // Assign allocation token source.
            this._sessionTokenSource = cancellationTokenSource;

            // Declare maintainConnectionTask variable.
            Task maintainConnectionTask;


            #region Try
            try
            {
                await Task.Run(() => 
                {
                    // Get and assign google apis user credentials.
                    this._googleApisUser = this.GetGoogleApisUserCredential();

                    // Get and assign google apis sheets service.
                    this._apisSheetsService = this.GetApisSheetsService();

                    // Get and assign google apis drive service.
                    this._apisDriveService = this.GetApisDriveService();
                });

                // Initialize session requests limiter.
                await SessionRequestsLimiter.Instance.StartQuotaCount(this._apisSheetsService, this._sessionsSpreadsheetId, this._sessionTokenSource.Token);

                await Task.Run(() =>
                {
                    // Get spreadsheets database.
                    this._spreadsheetsDb = this.GetSpreadsheetsDb();

                    // Get sessions repository
                    this._sessionsRepository = new SessionsRepository(this, this._sessionsSpreadsheetId);
                });

                // Await new active session to start
                maintainConnectionTask = await this._sessionsRepository.StartNewSessionAsync(this._sessionTokenSource, forceOtherInstanceActiveSessionTakeover);
            }
            #endregion
            #region Catch
            catch
            {
                // Stop available quota execution.
                SessionRequestsLimiter.Instance.StopQuotaCount();

                // Dispose sessions repository
                this._sessionsRepository?.Dispose();
                this._sessionsRepository = null;

                // Dispose spreadsheets database.
                this._spreadsheetsDb?.Dispose();
                this._spreadsheetsDb = null;

                // Dispose and assign google apis drive service.
                this._apisDriveService?.Dispose();
                this._apisDriveService = null;

                // Dispose and assign google apis sheets service.
                this._apisSheetsService?.Dispose();
                this._apisSheetsService = null;

                // Get and assign google apis user credentials.
                this._googleApisUser = null;

                // Set connection state flag to false.
                this.IsConnected = false;

                throw;
            }
            #endregion

            // Set connection state flag to true.
            this.IsConnected = true;

            // return connection maintenance task
            return maintainConnectionTask;
        }
        
        // Assures unique control over current session 
        public async Task AssureSessionControlAsync()
        {
            await this.SessionsRepository.AssureSessionControlAsync();
        }

        // Log out off the google apis
        public async Task DisconnectAsync()
        {
            // Set connection state flag to null.
            if (this.IsConnected != false)
                this.IsConnected = null;

            // Stop available quota execution.
            SessionRequestsLimiter.Instance.StopQuotaCount();

            // Await ending session
            await this._sessionsRepository.EndSessionAsync();

            // Dispose _sessions repository ...
            this._sessionsRepository?.Dispose();
            this._sessionsRepository = null;

            // Dispose google apis service...
            this._apisSheetsService?.Dispose();
            this._apisSheetsService = null;

            // .. and google drive service.
            this._apisDriveService.Dispose();
            this._apisDriveService = null;

            // Dispose live spreadsheets data...
            this._spreadsheetsDb.Dispose();
            this._spreadsheetsDb = null;

            // Dispose google apis user credentials
            this._googleApisUser = null;

            // Set connection state flag to false.
            this.IsConnected = false;
        }        
        #endregion

        #region Private Fields
        // Gets google apis user credential
        private UserCredential GetGoogleApisUserCredential()
        {
            // Declare FileStream to be available across try catch finally.
            FileStream stream = null;

            try
            {
                // Get stream of the file containing google credentials
                stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read);

                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";

                // Return result of synchronized authorization. 
                return GoogleWebAuthorizationBroker.AuthorizeAsync(
                    // Provide credentials.json as a GoogleClientSecrets
                    GoogleClientSecrets.Load(stream).Secrets,
                    // Google Sheets level of authority.
                    this._apisSheetsServiceScopes,
                    // ????????????????????????????????????????
                    "user",
                    // Empty CancellationToken
                    CancellationToken.None,
                    // ?????????????? can that be used to get rid of physical *.json credentials files? 
                    new FileDataStore(credPath, true)
                    // Get result of async task
                    ).Result;
            }
            catch (AggregateException ex)
            {
                throw new InvalidOperationException("Currently unable to obtain google api user credential, check Internet connection, and if problem persist replace google credentials.json. See inner exception for details.", ex.InnerException);
            }
            finally
            { 
                // Dispose the credentials file stream.
                stream.Dispose();
            }

            

        }

        // Gets google apis sheets services.
        private SheetsService GetApisSheetsService()
        {
            // Create and return new google sheets apis service.
            return new SheetsService(
                // Get new instance of google client service initializer.
                new BaseClientService.Initializer()
                {
                    // Assing google apis user credential to google service initializer.
                    HttpClientInitializer = this._googleApisUser,
                    // Assign application name
                    ApplicationName = this.ApplicationName,
                });
        }

        // Gets google apis sheets services.
        private DriveService GetApisDriveService()
        {
            // Create and return new google sheets apis service.
            return new DriveService(
                // Get new instance of google client service initializer.
                new BaseClientService.Initializer()
                {
                    // Assing google apis user credential to google service initializer.
                    HttpClientInitializer = this._googleApisUser,
                    // Assign application name
                    ApplicationName = this.ApplicationName,
                });
        }

        // Gets Live spreadsheets database index.
        private LiveSpreadsheetsDb GetSpreadsheetsDb()
        {
            return new LiveSpreadsheetsDb(this._apisSheetsService, this._apisDriveService, this._dbIndexSpreadsheetId);
        }
        #endregion

        #region IDisposable Support
        // Dispose entire LiveGoogle service
        public void Dispose()
        {
            // Stop available quota execution.
            SessionRequestsLimiter.Instance.StopQuotaCount();

            // Dispose sessions repository
            this._sessionsRepository?.Dispose();
            this._sessionsRepository = null;

            // Dispose google apis sheets service
            this._apisSheetsService?.Dispose();
            this._apisSheetsService = null;

            // Dispose google apis drive service
            this._apisDriveService?.Dispose();
            this._apisDriveService = null;

            // Dispose spreadsheetsDb together with all its content.
            this._spreadsheetsDb?.Dispose();
            this._spreadsheetsDb = null;

            // Cancel and get rid of user credentials 
            this._googleApisUser = null;
        }
        #endregion
    }
}
