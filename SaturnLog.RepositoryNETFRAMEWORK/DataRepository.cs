using LiveGoogle.Exceptions;
using SaturnLog.Core;
using SaturnLog.Core.Exceptions;
using SaturnLog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaturnLog.Repository
{
    public class DataRepository : IDataRepository
    {
        #region Private Fields
        private static DataRepository _instance;

        //
        private static readonly SemaphoreSlim _dataLongRetrieveSemaphore = new SemaphoreSlim(1, 1);

        // Semaphore slim assuring that only one data commit operation will be performed at the time
        // as well as that forced database disconnect wont happened when any data commit process is ongoing.
        private static readonly SemaphoreSlim _dataCommitSemaphore = new SemaphoreSlim(1, 1);

        // Flag indicating whether the data repository is in the state where it has been forced to disconnect
        private bool _forcedToDisconnect;

        // Users repository.
        private UserRepository _userRepository;

        // Saturn 5 repository
        private Saturn5Repository _saturn5Repository;
                
        // Saturns 5 dashboard repository
        private Saturns5DashboardRepository _saturns5DashboardRepository;

        // Saturn 5 issues repository
        private Saturn5IssuesRepository _saturn5IssuesRepository;

        // Saturn 5 movement repository
        private Saturns5MovementRepository _saturns5MovementRepository;
        #endregion

        #region Internal Properties
        // Instance of Google Service
        internal LiveGoogle.LiveGoogle GoogleService { get; private set; }

        // Saturn 5 repository - must be initialized after UserRepository
        internal IUserDB UsersDB { get { return this._userRepository; } }

        // Saturn 5 repository - must be initialized after UserRepository
        internal ISaturn5DB Saturns5DB { get { return this._saturn5Repository; } }

        // Saturn 5 Issues repository 
        internal ISaturn5IssuesDB Saturns5IssuesDB { get { return this._saturn5IssuesRepository; } }
        
        //
        internal SemaphoreSlim LongRunningRetrieveDataSemaphore { get { return DataRepository._dataLongRetrieveSemaphore; } }

        // Semaphore slim assuring that only one data commit operation will be performed at the time
        // as well as that forced database disconnect wont happened when any data commit process is ongoing.
        internal SemaphoreSlim DataCommitSemaphore { get { return DataRepository._dataCommitSemaphore; } }
        #endregion

        #region Public Properties
        // public flag indicating whether the database is connected (null ongoing attempt to connect, not operational)
        public bool? Connected { get; private set; } = false;

        // Seconds left till establishing a db session will be possible.
        public int? DBConnectionAvailabilityCountdown { get { return this.GoogleService?.SecondsToNewSession; } }
        
        // Percentage of database throughput quota used.
        public int? DBQuotaUsagePercentage { get { return null; } }

        // Boolean flag indicating whether the users are cached (null ongoing).
        public bool? UsersDataCached { get; private set; } = false;

        // Boolean flag indicating whether the saturns 5 are cached (null ongoing).
        public bool? Saturns5DataCached { get; private set; } = false;

        // Boolean flag indicating whether the saturns 5 statuses has been already rebuild.
        public bool? Saturns5StatusRebuild { get; private set; } = false;

        // Boolean flag indicating whether the saturns 5 dashboard has been already rebuild.
        public bool? DBDashboardRebuild { get; private set; } = false;
        
        // Users repository.
        public IUserRepository UserRepository { get { return this._userRepository; } }

        // Saturn 5 repository.
        public ISaturn5Repository Saturn5Repository { get { return this._saturn5Repository; } }

        // Saturns 5 dashboard repository.
        public ISaturns5DashboardRepository Saturns5DashboardRepository { get { return this._saturns5DashboardRepository; } }
        
        // Saturns 5 issues repository.
        public ISaturn5IssuesRepository Saturn5IssuesRepository { get { return this._saturn5IssuesRepository; } }

        // Saturns 5 movement repository.
        public ISaturns5MovementRepository Saturns5MovementRepository { get { return this._saturns5MovementRepository; } }
        
        // Async tasks cancellation token sources
        public CancellationTokenSource ConnectAsyncCancel { get; private set; } = null;

        public CancellationTokenSource FetchDataAsyncCancel { get; private set; } = null;
        #endregion

        #region Constructor
        public DataRepository()
        {
            // If other instance of data repository is already instantiated..
            if (!(DataRepository._instance is null))
                // .. throw appropriate exception.
                throw new InvalidOperationException("Unable to instantiated another instance of data depository prior to disposing the first one.");
            else
                DataRepository._instance = this;
        }
        #endregion

        #region Methods
        // Lock DB Content changes to the thread executing this method
        public async Task LockDBContentAsync()
        {
            if (this.Connected != true)
                throw new DBNotConnectedException();
            else if (this.ConnectAsyncCancel.IsCancellationRequested)
                throw new DBNotConnectedException();
            else if (this._forcedToDisconnect)
                throw new DBForcedToBeTakenOverException();

            await this.DataCommitSemaphore.WaitAsync();

            if (this.Connected != true)
                throw new DBNotConnectedException();
            else if (this.ConnectAsyncCancel.IsCancellationRequested)
                throw new DBNotConnectedException();
            else if (this._forcedToDisconnect)
                throw new DBForcedToBeTakenOverException();
            else
                try { await this.GoogleService.AssureSessionControlAsync(); }
                catch
                {
                    this.DataCommitSemaphore.Release();
                    throw;
                }
        }

        // Releases DB content mutex semaphoreSlim
        public void ReleaseDBContent()
        {
            this.DataCommitSemaphore.Release();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectAsyncCancel"></param>
        /// <param name="forceDatabaseTakeoverConnect"></param>
        /// <exception cref="DBAlreadyConnectedException"></exception>
        /// <exception cref="DBAlreadyConnectingException"></exception>
        /// <exception cref="DBFailAttemptToConnectException"></exception>
        /// <exception cref="DBInUseByOtherApplicationInstanceException"></exception>
        /// <exception cref="DBForcedToBeTakenOverException"></exception>
        /// <exception cref=""></exception>
        /// <exception cref=""></exception>
        /// <exception cref=""></exception>
        /// <exception cref="DBFailAttemptToConnectException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <returns></returns>
        public async Task<Task> ConnectAsync(CancellationTokenSource connectAsyncCancel, bool forceDatabaseTakeoverConnect)
        {
            // If other instance of data repository is already instantiated..            
            if (this.Connected == true)
                // .. throw appropriate exception.d
                throw new DBAlreadyConnectedException();
            else if (this.Connected is null)
                // .. throw appropriate exception.d
                throw new DBAlreadyConnectingException();

            // Set 'Connected' boolean flag to indicate currently ongoing attempt to connect to database
            this.Connected = null;

            // Assign cancellation total source.
            this.ConnectAsyncCancel = connectAsyncCancel;

            // Declare maintainConnectionTask variable.
            Task maintainConnectionTask;

            #region Try - Connect with google service.
            try
            {
                // Start/Connect google service.
                this.GoogleService = new LiveGoogle.LiveGoogle("SaturnLog", DatasetConstLibrary.SpreadsheetsDbSpreadsheetId, DatasetConstLibrary.SessionsDbSpreadsheetId_SpreadsheetId);

                // Connect to google service and await connection maintenance task to be return.
                maintainConnectionTask = await this.GoogleService.ConnectAsync(connectAsyncCancel, forceDatabaseTakeoverConnect);
            }
            #endregion
            #region Catch - Exceptional attempt to connect cases
            // If live google is already connected or in a process of getting activated by the current instance of an application.
            catch (OwnInstanceSessionIsActiveOrActivatingException ex) { throw new DBFailAttemptToConnectException(null, ex); }

            // When unable to start new session activation because other application instance currently owns active session, or failed
            // during active session and didn't clean up 'Last' stamp after itself as recently that this stamp is not considered as unresponsive yet.
            catch (OtherInstanceSessionAlreadyActiveException ex)
            {
                // Disconnect google service.
                this.DisposeGoogleService();
                this.Connected = false;
                throw new DBInUseByOtherApplicationInstanceException(null, ex);
            }

            // When unable to start new session activation because other application instance is currently in a process of starting new session, 
            // or failed during activation process and didn't clean up one of the 'ConnectedIn' stamps after itself as recently that this stamps are not considered as unresponsive yet.
            catch (OtherInstanceSessionAlreadyStartingException ex)
            {
                this.DisposeGoogleService();
                this.Connected = false;
                throw new DBIsAttemptingToConnectToOtherApplicationInstanceException(null, ex);
            }

            // When ongoing new session activation has been interrupted and failed because own application instance attempted to start
            // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.
            catch (SessionActivationInterruptedByOwnInstanceException ex) { throw new DBFailAttemptToConnectException(null, ex); }

            // When ongoing new session activation has been interrupted and failed because other application instance,
            // either began the new session activation process, or activating session by updating value of 'Connected' stamp
            catch (SessionActivationInterruptedByOtherInstanceException ex)
            {
                this.DisposeGoogleService();
                this.Connected = false;
                throw new DBForcedToBeTakenOverException(null, ex);
            }
            catch (OperationCanceledException)
            {
                this.DisposeGoogleService();
                this.Connected = false;
                throw;
            }

            // Dispose all the elements already constructed by this method so far to bring the DataRepository back into the state prior to calling this method so far.
            catch (Exception ex)
            {
                this.DisposeGoogleService();
                this.Connected = false;

                throw new DBFailAttemptToConnectException(null, ex);
            }
            #endregion

            #region Try - Initialize all the repositories.
            try
            {
                // Construct all the necessary repositories
                await ConstructRepositoriesAsync();

                // This rebuild dashboard grid - must occur after UsersDB and Saturns5DB sheets has been successfully obtained,
                // but before the first individual saturn5 sheet has been loaded. 

                // (this assures serialNumber-rowIndex consistency between Saturn5Repository and Saturn5DashboardRepository 
                // but not any other data in the Saturn5DashboardRepository, that still has to be done by executing 
                // this._saturns5DashboardRepository.AssureDataConsistency() - after all the Saturn5 individual spreadsheets will get loaded);

                // (Saturns5DashboardRepository entry row index VS Saturn5Repository - is adjusted for the difference caused by the fact that 
                // the dashboards top row contains the header and as such associated entries are located on the row index greater by 1 
                // then the index of the associated Saturn5DB entry)
                await this._saturns5DashboardRepository.AssureGridDataConsistencyAsync();

                // Throw if cancellation requested
                connectAsyncCancel.Token.ThrowIfCancellationRequested();
            }
            #endregion
            #region Catch - Exceptional Data Repository parts initialization cases - clean up
            catch (OperationCanceledException)
            {
                // Dispose all the elements already constructed by this method so far to bring the DataRepository back into the state prior to calling this method so far.
                this.DisposeSaturns5IssuesRepository();
                this.DisposeSaturns5DashboardRepository();
                this.DisposeSaturn5Repository();
                this.DisposeUserRepository();

                // TODO move to google service itself 
                await this.GoogleService.DisconnectAsync();
                this.DisposeGoogleService();

                throw;
            }
            catch (Exception ex)
            {
                // Dispose all the elements already constructed by this method so far to bring the DataRepository back into the state prior to calling this method so far.
                this.DisposeSaturns5IssuesRepository();
                this.DisposeSaturns5DashboardRepository();
                this.DisposeSaturn5Repository();
                this.DisposeUserRepository();
                this.DisposeGoogleService();

                throw new DBFailAttemptToConnectException(null, ex);
            }
            #endregion

            // Set connected flag to indicate that database is connected
            this.Connected = true;

            #region Connection Maintenance
            return this.HandleOngoingConnectionMaintenance(maintainConnectionTask);
            #endregion
        }

        public async Task FetchDataAsync(CancellationTokenSource fetchDataAsyncCancel)
        {
            // Assign appropriate cancellation token
            this.FetchDataAsyncCancel = fetchDataAsyncCancel;

            // Await database long running data retrieve semaphore to indicate current method turn
            await this.LongRunningRetrieveDataSemaphore.WaitAsync();

            try
            {
                // Download all users data
                await this.FetchUserDataAsync(fetchDataAsyncCancel);

                // Download all saturns 5 data
                await this.FetchSaturn5DataAsync(fetchDataAsyncCancel);
            }
            catch (FetchUserDataIsAlreadyOngoingException) { throw; }
            catch (FetchSaturn5DataIsAlreadyOngoingException) { throw; }
            // Finally release the database semaphore
            finally { this.LongRunningRetrieveDataSemaphore.Release(); }
            
            // Await database commit semaphore to indicate current method turn
            await this.LockDBContentAsync();

            try
            {
                // TODO replace with better solution -> store all the saturns5 serial numbers currently with specific user
                // in a userLog spreadsheet of the user itself which allow change WithUser <-> WithStarUser <-> WithManager
                // immediately after editing user type, rather then after caching all the data

                // Adjust Saturn5Status of all of the Saturn5 which are not InDepot according the type of user the are hold by.
                // WithUser <-> WithStarUser <-> WithManager. 
                await this.DBSaturns5StatusDashboardAsync(fetchDataAsyncCancel);

                await this.DBRebuildDashboardAsync(fetchDataAsyncCancel);
            }
            // Finally release the database semaphore
            finally { this.ReleaseDBContent(); }
        }

        public void DisposeSaturns5IssuesRepository()
        {
            this._saturn5IssuesRepository?.Dispose();
            this._saturn5IssuesRepository = null;

        }
        public void DisposeSaturns5MovementRepository()
        {
            this._saturns5MovementRepository?.Dispose();
            this._saturns5MovementRepository = null;

        }
        public void DisposeSaturns5DashboardRepository()
        {
            this._saturns5DashboardRepository?.Dispose();
            this._saturns5DashboardRepository = null;

        }
        public void DisposeSaturn5Repository()
        {
            this._saturn5Repository?.Dispose();
            this._saturn5Repository = null;
        }
        public void DisposeUserRepository()
        {
            this._userRepository?.Dispose();
            this._userRepository = null;

        }
        internal void DisposeGoogleService()
        {
            // Dispose google service
            this.GoogleService?.Dispose();
            // Dispose google service
            this.GoogleService = null;
        }
        #endregion

        #region Private Helpers
        //
        private async Task ConstructRepositoriesAsync()
        {
            // Re-obtain content of dbIndex in case if it was edited during awaiting time
            await this.GoogleService.SpreadsheetsDb.ReObtainDBSpreadsheetsIndexAsync();

            // Get User repository
            this._userRepository = await SaturnLog.Repository.UserRepository.GetAsync(this, DatasetConstLibrary.UsersDB_SpreadsheetId);

            // Get saturn5 repository
            this._saturn5Repository = await SaturnLog.Repository.Saturn5Repository.GetAsync(this, DatasetConstLibrary.Saturns5DB_SpreadsheetId);

            // Get saturn5 repository
            this._saturns5DashboardRepository = await SaturnLog.Repository.Saturns5DashboardRepository.GetAsync(this, DatasetConstLibrary.Saturns5Dashboard_SpreadsheetId);

            // Get saturn5 movement repository
            this._saturns5MovementRepository = await SaturnLog.Repository.Saturns5MovementRepository.GetAsync(this, DatasetConstLibrary.Saturns5Dashboard_SpreadsheetId);

            // Get saturn5 issues repository (Has to be initialized, and subscribed to Saturn5SpreadsheetLoaded
            // Before first Saturn5 Spreadsheet data gonna get attempted to be obtained).
            this._saturn5IssuesRepository = await SaturnLog.Repository.Saturn5IssuesRepository.GetAsync(this, DatasetConstLibrary.Saturns5Dashboard_SpreadsheetId);
        }

        private async Task FetchUserDataAsync(CancellationTokenSource fetchUserDataAsyncCancel)
        {
            // If other instance of data repository is already instantiated..
            if (this.Connected == true
                && this.UsersDataCached != false)
                // .. throw appropriate exception.
                throw new FetchUserDataIsAlreadyOngoingException();

            this.UsersDataCached = null;
            //this.FetchUserDataAsyncCancel = fetchUserDataAsyncCancel;

            try
            {
                //await this._userRepository.AssureAllLoadedAsync(this.FetchUserDataAsyncCancel.Token);
                await this._userRepository.AssureAllLoadedAsync(fetchUserDataAsyncCancel.Token);

                this.UsersDataCached = true;
            }
            catch 
            {
                this.UsersDataCached = false;
                throw;
            }
        }

        private async Task FetchSaturn5DataAsync(CancellationTokenSource fetchSaturn5DataAsyncCancel)
        {
            // If other instance of data repository is already instantiated..
            if (this.Connected == true
                && this.Saturns5DataCached != false)
                // .. throw appropriate exception.
                throw new FetchSaturn5DataIsAlreadyOngoingException();

            this.Saturns5DataCached = null;
            //this.FetchSaturn5DataAsyncCancel = fetchSaturn5DataAsyncCancel;

            try
            {
                //await this._saturn5Repository.AssureAllLoadedAsync(this.FetchSaturn5DataAsyncCancel.Token);            
                await this._saturn5Repository.AssureAllLoadedAsync(fetchSaturn5DataAsyncCancel.Token);            

                this.Saturns5DataCached = true;
            }
            catch 
            {
                this.Saturns5DataCached = false;
                throw;
            }
        }

        private async Task DBSaturns5StatusDashboardAsync(CancellationTokenSource dbRebuildDashboardAsyncCancel)
        {
            // If other instance of data repository is already instantiated..
            if (this.Connected == true
                && this.UsersDataCached != true
                && this.Saturns5DataCached != true
                && this.Saturns5StatusRebuild != false
                && this.DBDashboardRebuild != false)
                // .. throw appropriate exception.
                throw new InvalidOperationException();

            this.Saturns5StatusRebuild = null;
            //this.DBRebuildDashboardAsyncCancel = dbRebuildDashboardAsyncCancel;

            try
            {
                await this._saturn5Repository.DBCorrectWithUserStatusAsync(dbRebuildDashboardAsyncCancel.Token);

                this.Saturns5StatusRebuild = true;
            }
            catch
            {
                this.Saturns5StatusRebuild = false;
                throw;
            }
        }

        private async Task DBRebuildDashboardAsync(CancellationTokenSource dbRebuildDashboardAsyncCancel)
        {
            // If other instance of data repository is already instantiated..
            if (this.Connected == true
                && this.UsersDataCached != true
                && this.Saturns5DataCached != true
                && this.Saturns5StatusRebuild != true
                && this.DBDashboardRebuild != false)
                // .. throw appropriate exception.
                throw new InvalidOperationException();

            this.DBDashboardRebuild = null;
            //this.DBRebuildDashboardAsyncCancel = dbRebuildDashboardAsyncCancel;

            try
            {
                //await this._saturns5DashboardRepository.AssureDataConsistencyAsync(this.DBRebuildDashboardAsyncCancel.Token);
                await this._saturns5DashboardRepository.AssureDataConsistencyAsync(dbRebuildDashboardAsyncCancel.Token);

                this.DBDashboardRebuild = true;
            }
            catch 
            {
                this.DBDashboardRebuild = false;
                throw;
            }
        }

        // Returns asynchronous Task handling triggering of all the necessary methods while awaiting provided maintainConnectionTask
        private Task HandleOngoingConnectionMaintenance(Task maintainConnectionTask)
        {
            return Task.Run(async () =>
            {
                // Maintain current live google connection session
                try { await maintainConnectionTask; }

                // Occurs when database is not connected when method is called
                catch (OwnInstanceSessionIsNotActiveException ex) { throw new DBNotConnectedException(null, ex); }

                // Occurs whenever DB connection failed once established
                catch (ActiveSessionInterruptedByOwnInstanceException ex)
                { throw new DBConnectionFailureException(null, ex); }

                // Occurs whenever data repository is forced to disconnect from database,
                // because other application instance have taken over database connection.
                catch (ActiveSessionInterruptedByOtherInstanceException ex) { await this.HandleOngoingConnectionMaintenanceForcedToDisconnectAsync(ex); }

                // Handle connection maintenance cancellation
                catch (OperationCanceledException ex) { await this.HandleOngoingConnectionMaintenanceCancellationAsync(ex); }

                // Occurs whenever DB connection failed once established
                catch (Exception ex)
                { throw new DBConnectionFailureException(null, ex); }
            });
        }

        // Handles executing of all of necessary methods when maintenance connection task indicates that db has been forced to disconnect.
        private async Task HandleOngoingConnectionMaintenanceForcedToDisconnectAsync(ActiveSessionInterruptedByOtherInstanceException ex)
        {
            // Set forcedToDisconnect flag to true, to assure that
            this._forcedToDisconnect = true;

            // Cancel user data fetch if ongoing...
            this.FetchDataAsyncCancel?.Cancel();
            do await Task.Delay(100);
            // .. and await it cancellation checking every 100ms.
            while (this.UsersDataCached == null || this.Saturns5DataCached == null || this.DBDashboardRebuild == null);

            // Assure that all the semaphores allow session to be ended.
            await this.LongRunningRetrieveDataSemaphore.WaitAsync();
            await this.DataCommitSemaphore.WaitAsync();
            try
            {
                // TODO move to google service itself 
                await this.GoogleService.DisconnectAsync();

                // Dispose all the elements already constructed by this method so far to bring the DataRepository back into the state prior to calling this method so far.
                this.DisposeSaturns5IssuesRepository();
                this.DisposeSaturns5DashboardRepository();
                this.DisposeSaturn5Repository();
                this.DisposeUserRepository();
                this.DisposeGoogleService();

                this.Connected = false;
            }
            finally
            {
                // release both semaphores.
                this.LongRunningRetrieveDataSemaphore.Release();
                this.DataCommitSemaphore.Release();
            }
            throw new DBForcedToBeTakenOverException(null, ex);
        }

        // Handles executing of all of the necessary methods when maintenance connection task indicates that db has been disconnected.
        private async Task HandleOngoingConnectionMaintenanceCancellationAsync(OperationCanceledException ex)
        {
            // Cancel user data fetch if ongoing...
            this.FetchDataAsyncCancel?.Cancel();
            do await Task.Delay(100);
            // .. and await it cancellation checking every 100ms.
            while (this.UsersDataCached == null || this.Saturns5DataCached == null || this.DBDashboardRebuild == null);

            // Assure that all the semaphores allow session to be ended.
            await this.LongRunningRetrieveDataSemaphore.WaitAsync();
            await this.DataCommitSemaphore.WaitAsync();
            try
            {
                // TODO move to google service itself 
                await this.GoogleService.DisconnectAsync();

                // Dispose all the elements already constructed by this method so far to bring the DataRepository back into the state prior to calling this method so far.
                this.DisposeSaturns5IssuesRepository();
                this.DisposeSaturns5DashboardRepository();
                this.DisposeSaturn5Repository();
                this.DisposeUserRepository();
                this.DisposeGoogleService();

                this.Connected = false;
            }
            // Finally release both semaphores.
            finally
            {
                this.LongRunningRetrieveDataSemaphore.Release();
                this.DataCommitSemaphore.Release();
            }
            throw new DBDisconnectedException(null, ex);
        }
        #endregion

        #region IDisposable Support
        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed)
                return;

            this._disposed = true;

            this.DisposeSaturns5IssuesRepository();

            this.DisposeSaturns5DashboardRepository();

            this.DisposeSaturn5Repository();

            this.DisposeUserRepository();

            this.DisposeGoogleService();

            DataRepository._instance = null;
        }
        #endregion
    }
}
