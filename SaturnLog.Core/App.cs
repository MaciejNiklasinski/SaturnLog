using SaturnLog.Core.EventArgs;
using SaturnLog.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public class App : IDisposable
    {        
        #region PrivateFields
        private Assembly _repositoriesAssembly = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + "SaturnLog.Repository.dll");

        private IDataRepository _dataRepository;        

        private static readonly string _dataRepositoryName = "SaturnLog.Repository.DataRepository";        
        #endregion

        #region Properties
        #region Internal Properties - Repositories 
        internal IDataRepository DataRepository { get { return this._dataRepository; } }
        internal IUserRepository UserRepository { get { return this._dataRepository.UserRepository; } }
        internal ISaturn5Repository Saturn5Repository { get { return this._dataRepository.Saturn5Repository; } }
        internal ISaturns5DashboardRepository Saturns5DashboardRepository { get { return this._dataRepository.Saturns5DashboardRepository; } }
        internal ISaturn5IssuesRepository Saturn5IssuesRepository { get { return this._dataRepository.Saturn5IssuesRepository; } }
        internal ISaturns5MovementRepository Saturns5MovementRepository { get { return this._dataRepository.Saturns5MovementRepository; } }
        #endregion

        // Flag indicating whether the application database is connected or not. True - Connected, Null - Connecting/Disconnecting, False - Disconnected.
        public bool? DBConnected { get; private set; } = false;

        // Number of seconds has to be awaited prior to database connection being allowed to be establish.
        public int? DBConnectionAvailabilityCountdown { get { return this._dataRepository?.DBConnectionAvailabilityCountdown; } } 

        // Percentage of database data throughput quota currently used. 0% = All Available, 100% = All Used.
        public int? DBQuotaUsagePercentage { get { return this._dataRepository?.DBQuotaUsagePercentage; } } 

        // Flag indicating whether any user is currently logged in.
        public bool LoggedIn { get; private set; }// { get { return this._dataRepository.LoggedIn; } private set { this._dataRepository.LoggedIn = value; } }

        // Username of the logged in username
        public string LoggedUsername { get; private set; }// { get { return this._dataRepository.LoggedUsername; } private set { this._dataRepository.LoggedUsername = value; } }

        // Logged user type.
        public UserType? LoggedUserType { get; private set; }// { get { return this._dataRepository.LoggedUserType; } private set { this._dataRepository.LoggedUserType = value; } }

        // Logged User.
        public User LoggedUser { get; private set; }
        
        #region Service Helpers classes references
        // Helper class encapsulating logic of build operation string logs.
        public LogsContentConstructor LogsContentConstructor { get; }

        // User related services. Basic CRUD operations.
        public UserServices UserServices { get;  }

        // Saturn5 related services. Basic CRUD operations. Saturn5 Stock and Issues Management.
        public Saturn5Services Saturn5Services { get; }

        // Pre-Brief/De-Brief related services. Allocating Saturn5 units to the user and confirming them back in depot.
        public PreBriefServices PreBriefServices { get;}
        public DeBriefServices DeBriefServices { get; }
        #endregion
        #endregion
        
        #region Constructor
        // Default constructor. Domain layer entry point.
        public App()
        {
            this.LogsContentConstructor = new LogsContentConstructor(this);

            this.UserServices = new UserServices(this);
            this.Saturn5Services = new Saturn5Services(this);
            this.PreBriefServices = new PreBriefServices(this);
            this.DeBriefServices = new DeBriefServices(this);
        }
        #endregion

        #region Methods
        // Connect into the database.
        public async Task<Task> ConnectAsync(CancellationTokenSource cancellationTokenSource, bool forceDBTakeover)
        {
            // Listen for db errors task
            Task listenDBErrorsTask;

            try
            {
                if (this.DBConnected is true)
                    throw new DBAlreadyConnectedException("Unable to connect to database. Database is already connected.");
                else if (this.DBConnected is null)
                    throw new DBAlreadyConnectingException("Unable to connect to database. Database is currently attempting to connect or disconnect.");

                // Set DBConnected flag to null, to indicating currently ongoing attempt to connect to database.
                this.DBConnected = null;

                // Obtain data repository instance from reflection
                this._dataRepository = this._repositoriesAssembly
                    .GetType(App._dataRepositoryName)
                    .GetConstructor(Type.EmptyTypes)
                    .Invoke(new object[0]) as IDataRepository;

                listenDBErrorsTask = await this._dataRepository.ConnectAsync(cancellationTokenSource, forceDBTakeover);

                // Set DBConnected flag to true, to indicating the database is currently connected.
                this.DBConnected = true;
            }
            catch
            {
                this._dataRepository.Dispose();
                this.DBConnected = false;
                throw;
            }

            // Database connection maintenance.
            return this.HandleListeningDBConnectionErrors(listenDBErrorsTask);
        }

        // Disconnect from database.
        public async Task DisconnectAsync()
        {
            if (this.DBConnected is false)
                throw new InvalidOperationException("Unable to disconnect from database. Database is not connected.");
            this.DBConnected = null;

            // Stop any ongoing data fetch.
            this._dataRepository?.FetchDataAsyncCancel?.Cancel();

            // Stop any ongoing connection, or attempt to establish it.
            this._dataRepository?.ConnectAsyncCancel?.Cancel();

            // Await for database to disconnect.
            while (this.DBConnected != false)
                await Task.Delay(100);

            // Dispose data repository once database got disconnected.
            this._dataRepository?.Dispose();
            this._dataRepository = null;
        }

        // Download and cache all the data from database.
        public async Task FetchDataAsync(CancellationTokenSource cancellationTokenSource)
        {
            await this._dataRepository.FetchDataAsync(cancellationTokenSource);
        }
        
        // Log in the user associated with provided username
        public async Task LogInAsync(string username)
        {
            // Await database commit semaphore to indicate current method turn
            await this._dataRepository.LockDBContentAsync();

            try
            {
                await Task.Run(() =>
                {
                    // Validate parameter.
                    if (username is null)
                        throw new ArgumentNullException("Username is required to obtain access into the application database.", nameof(username));

                    // Assure that all the characters used in provided username string are upper case, and 'empty space' doesn't perpend, or append the username.
                    username = username.Trim().ToUpper();

                    if (!this.UserRepository.HasUsername(username))
                        throw new ArgumentException($"Unable to obtain application data access. user associated with provided username: {username}. Provided username is not associated with any existing user.", nameof(username));
                    else if (this.LoggedIn)
                        throw new InvalidOperationException("Application is already logged in, and required to be logged out first.");

                    // Get and assign the user associated with the provided username.
                    this.LoggedUser = this.UserRepository.Read(username);

                    // Set logged in boolean flag to true.
                    this.LoggedIn = true;

                    // Set logged username based on the provided parameter
                    this.LoggedUsername = username;

                    // Obtain user type of a user associated with the provided username.
                    this.LoggedUserType = this.LoggedUser.Type;
                });
            }
            // Finally release the database semaphore
            finally { this._dataRepository.ReleaseDBContent(); }
        }

        // Log user out.
        public async Task LogOutAsync()
        {
            // Flag indicating whether the data repository has been successfully locked and as such requires to be released after log out task completion.
            bool succesfullyLocked = false;

            // Try to await database commit semaphore to indicate current method turn
            try
            {
                await this._dataRepository.LockDBContentAsync();

                // Indicate that database commits have been successfully locked.
                succesfullyLocked = true;
            }
            // If database lock failed because DB is already disconnected, exception can be simply ignored as locking DBContent is not necessary. (Remember to ignore releasing semaphore as well in this case).
            catch (DBNotConnectedException) { }
            // If database lock failed because DB has been recently forced to get disconnected, and its currently awaiting all the ongoing data commits to be completed, prior to disconnecting and disposing itself.
            // Await till database gonna get disconnected and simply ignore error after that. (Remember to ignore releasing semaphore as well in this case).
            catch (DBForcedToBeTakenOverException) { do { await Task.Delay(100); } while(this.DBConnected != false);  }

            try
            {
                await Task.Run(() =>
                {
                    if (!this.LoggedIn)
                        throw new InvalidOperationException("Application is not logged in, and as such cannot to be logged out.");

                    // Set logged in boolean flag to false.
                    this.LoggedIn = false;

                    // Set logged username based on the provided parameter
                    this.LoggedUsername = "";

                    // Obtain user type of a user associated with the provided username.
                    this.LoggedUserType = null;
                });
            }
            // Finally release the database semaphore id it has been successfully locked.
            finally
            {
                if (succesfullyLocked)
                    this._dataRepository.ReleaseDBContent();
            }
        }
        #endregion

        #region Private Helpers
        private Task HandleListeningDBConnectionErrors(Task listenDBErrorsTask)
        {
            return Task.Run(async () =>
            {
                // Await any errors occurred during db connection, and perform relevant actions for specific errors
                try { await listenDBErrorsTask; }
                catch (DBConnectionFailureException) // When DB active connection failed.
                {
                    // Await data repository to disposal.
                    await DisposeDataRepositoryAsync();

                    // Set DBConnected flag to 
                    this.DBConnected = false;

                    // Proceed up the stack
                    throw;
                }
                catch (DBForcedToBeTakenOverException) // When DB has been forced to disconnect
                {
                    // If logged in... log out.
                    if (this.LoggedIn) await this.LogOutAsync();

                    // Await data repository to disposal.
                    await DisposeDataRepositoryAsync();

                    // Set DBConnected flag to 
                    this.DBConnected = false;

                    // Proceed up the stack
                    throw;
                }
                catch (DBDisconnectedException) // When DB willfully disconnected. (Catch and Swallow.)
                {
                    // Await data repository to disposal.
                    await DisposeDataRepositoryAsync();
                    // Set DBConnected flag to 
                    this.DBConnected = false;
                }
                catch { throw; } // Re-throw all other exceptions
            });
        }

        // Dispose Data Repository in asynchronous manner.
        private async Task DisposeDataRepositoryAsync()
        {
            await Task.Run(() => { this._dataRepository.Dispose(); });
        }
        #endregion

        #region IDisposable support
        // Basically same as DisconnectAsync apart of using Thread.Sleep rather then Task.Delay
        public void Dispose()
        {
            // Stop any ongoing data fetch.
            this._dataRepository?.FetchDataAsyncCancel?.Cancel();

            // Stop any ongoing connection, or attempt to establish it.
            this._dataRepository?.ConnectAsyncCancel?.Cancel();

            // Await for database to disconnect.
            while (this.DBConnected != false)
                Thread.Sleep(100);

            // Dispose data repository once database got disconnected.
            this._dataRepository?.Dispose();
            this._dataRepository = null;
        }
        #endregion
    }
}
