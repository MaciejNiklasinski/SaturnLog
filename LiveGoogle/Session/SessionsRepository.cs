using LiveGoogle.Sheets;
using LiveGoogle.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskExtensions;

namespace LiveGoogle.Session
{
    internal partial class SessionsRepository : IDisposable
    {
        #region Const
        //private const string SessionsDbSpreadsheetId_SpreadsheetId = "1sPVkd8_4U93aH6r1NBU6CJJJxciIzfCI37aCpdjurvE";
        //private const string SessionsDbSpreadsheetId_SpreadsheetId = "1CHE_oeA_jztLoXJ7lWmMzUKQlqu7F61_IhpQLckFvsI";
        public readonly string SessionsDbSpreadsheetId_SpreadsheetId;

        public const string SessionsDbSpreadsheetId_SheetId = "Sessions";
        public const int SessionsDbSpreadsheetId_DataRowIndex = 0;

        public const int SessionsDbSpreadsheetId_LastAvailableRequests_ColumnsIndex = 0;
        public const int SessionsDbSpreadsheetId_LastForcedtoDisconnectStamp_ColumnsIndex = 1;
        public const int SessionsDbSpreadsheetId_DisconnectedStamp_ColumnsIndex = 2;
        public const int SessionsDbSpreadsheetId_LastStamp_ColumnsIndex = 3;
        public const int SessionsDbSpreadsheetId_ConnectedStamp_ColumnsIndex = 4;
        public const int SessionsDbSpreadsheetId_ConnectedIn15Stamp_ColumnsIndex = 5;
        public const int SessionsDbSpreadsheetId_ConnectedIn30Stamp_ColumnsIndex = 6;
        public const int SessionsDbSpreadsheetId_ConnectedIn45Stamp_ColumnsIndex = 7;
        public const int SessionsDbSpreadsheetId_ConnectedIn60Stamp_ColumnsIndex = 8;
        public const int SessionsDbSpreadsheetId_ConnectedIn90Stamp_ColumnsIndex = 9;
        public const int SessionsDbSpreadsheetId_ConnectedIn120Stamp_ColumnsIndex = 10;
        #endregion

        #region Private Fields/Properties
        //private static readonly object _sessionsLock = new object();
        private static readonly SemaphoreSlim _sessionsSemaphore = new SemaphoreSlim(1, 1);
        private static readonly SemaphoreSlim _endSessionSemaphore = new SemaphoreSlim(1, 1);

        // True active/not yet aware of being forced to disconnect by other application instance.
        // Null currently activating or deactivating.
        // False disconnected/inactive.
        private static bool? _sessionActive = false;
        private static bool _sessionMaintenanceActive = false;
        private bool _sessionForcedToDisconnect = false;

        private static readonly string _sessionId = Guid.NewGuid().ToString();

        private static CancellationTokenSource _sessionCancellationSource = null;
        private static CancellationToken _token { get { return _sessionCancellationSource?.Token ?? throw new InvalidOperationException("Session cancellation token source is null and as such token cannot be obtained from it."); } }

        // Number of seconds which has to pass prior to stamp being considered for representing unresponsive session.
        private static readonly int _unresponsiveSecondsLimit = 120;
        private static readonly int _unresponsiveMsLimit = _unresponsiveSecondsLimit * 1000;
        private static readonly int _longTimeUnresponsiveSecondsLimit = _unresponsiveSecondsLimit * 5;
        private static readonly int _maintenanceAwaitSec = 5;
        private static readonly int _maintenanceAwaitMs = _maintenanceAwaitSec * 1000;

        private readonly DataOperator _dataOperator;

        private LiveGoogle _googleService;

        private LiveSpreadsheet _sessionsSpreadsheet;

        private LiveSheet _sessionsSheet;

        // Last session stamp confirming the session
        private SessionStamp _cachedSessionStamp = null;
        #endregion

        #region Properties
        public static readonly int SesionIdLenght = SessionsRepository._sessionId.Length;
        public static readonly int TimestampLenght = DateTimeExtensions.GetNISTNow().ToTimestamp().Length;
        public static readonly int SessionStampLenght = SessionsRepository.TimestampLenght + SessionsRepository.SesionIdLenght;

        public int? SecondsToNewSession { get; private set; } = null;

        public bool? IsSessionActivated { get { return SessionsRepository._sessionActive; } }
        #endregion

        #region Constructors
        // Must be constructed after Saturn5Dashboard repository has been constructed withing dataRepository
        public SessionsRepository(LiveGoogle googleService, string spreadsheetId)
        {
            //
            this.SessionsDbSpreadsheetId_SpreadsheetId = spreadsheetId;

            // Assign new data operator.
            this._dataOperator = new DataOperator(this);

            // Assign provided google service.
            this._googleService = googleService;

            // Get live spreadsheets database reference.
            LiveSpreadsheetsDb db = this._googleService.SpreadsheetsDb;

            // Load spreadsheet containing saturns dashboard.
            db.LoadSpreadsheet(this.SessionsDbSpreadsheetId_SpreadsheetId);

            // Get and assign reference to Movement spreadsheet and sheet
            this._sessionsSpreadsheet = db[this.SessionsDbSpreadsheetId_SpreadsheetId];
            this._sessionsSheet = this._sessionsSpreadsheet[SessionsRepository.SessionsDbSpreadsheetId_SheetId];
        }
        #endregion

        #region Static Methods
        public static string GetNowStamp()
        {
            return SessionsRepository.GetSessionStamp(DateTimeExtensions.GetNISTNow());
        }

        public static string GetSessionStamp(DateTime dateTime)
        {
            return dateTime.ToTimestamp() + SessionsRepository._sessionId;
        }
        
        public static int RetrieveInitialAvailableQuota(Google.Apis.Sheets.v4.SheetsService sheetsService, string sessionsSpreadsheetId)
        {
            return DataOperator.RetrieveInitialAvailableQuota(sheetsService, sessionsSpreadsheetId);
        }
        #endregion

        #region Methods
        // OperationCanceledException - On operation canceled...

        // OwnInstanceSessionIsActiveOrActivatingException - If new session is already active, or in process of getting activated by other thread when this method is call.

        // OtherInstanceSessionAlreadyActiveException - When unable to start new session activation because other application instance currently owns active session, 
        // or failed during active session and didn't clean up 'Last' stamp after itself as recently that this stamp is not considered as unresponsive yet.

        // OtherInstanceSessionAlreadyStartingException - When unable to start new session activation because other application instance is currently in a process of starting new session, 
        // or failed during activation process and didn't clean up one of the 'ConnectedIn' stamps after itself as recently that this stamps are not considered as unresponsive yet.

        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        // SessionActivationInterruptedByOtherInstanceException - When ongoing new session activation has been interrupted and failed because other application instance,
        // either began the new session activation process, or activating session by updating value of 'Connected' stamp
        public async Task<Task> StartNewSessionAsync(CancellationTokenSource sessionCancellationSource, bool forceOtherInstanceActiveSessionTakeover)
        {            
            // If current instance of the application already owns an active session throw appropriate exception.
            if (!(SessionsRepository._sessionActive is false)) throw new OwnInstanceSessionIsActiveOrActivatingException();

            // Await session semaphore 
            await SessionsRepository._sessionsSemaphore.WaitAsync();

            try
            {
                // Assign new session cancellation source.
                SessionsRepository._sessionCancellationSource = sessionCancellationSource;

                // Set session active flag to null to indicated that starting new session is now ongoing.
                SessionsRepository._sessionActive = null;

                // Declare session pre activation analyze profile.
                SessionPreActivationAnalyzeProfile analyze = await AssesNewSessionStartViability(forceOtherInstanceActiveSessionTakeover);

                // Create SessionActivationAnalyzeProfile based on the previously provided SessionPreActivationAnalyzeProfile
                SessionActivationAnalyzeProfile activationAnalyze = new SessionActivationAnalyzeProfile(analyze);

                // Await till starting new session will be confirmed to be allowed
                await AwaitStartNewSessionToBeAllowed(analyze, activationAnalyze);
                
                // Attempt to start new session
                await AttemptToStartNewSession(activationAnalyze);
            }
            // Finally release session semaphore 
            finally { SessionsRepository._sessionsSemaphore.Release(); }

            // Start active session maintenance, returning it as a awaitable task.
            return MaintainActiveSessionAsync();
        }
        
        // Assures unique control over current session. Execution without throwing an exception means that the session control has been successfully assured.
        public async Task AssureSessionControlAsync()
        {
            // If current application instance does not own an active session, or maintenance thread of this active session failed and is no-longer ongoing...
            if (SessionsRepository._sessionActive != true || !SessionsRepository._sessionMaintenanceActive)
                // .. throw appropriate exception,
                throw new OwnInstanceSessionIsInactiveOrActivatingException();

            // If active session has been already marked as '_sessionForcedToDisconnect' throw appropriate exception.
            else if (this._sessionForcedToDisconnect)
                throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.Connected));
            
            // Get currently cachedStamp and store it in a variable in case if it would get refreshed during its analyze.
            SessionStamp cachedSessionStamp = this._cachedSessionStamp;

            // If currently cached 'Last' stamp is associated with sessionId different to current application instance sessionId, and that different 'Last' stamp does not represent non active session...
            if (!StampsTranslator.IsRepresentingThisSession(cachedSessionStamp.Last) && !StampsTranslator.IsRepresentingNonActiveLastStamp(cachedSessionStamp.Last))
                // throw appropriate exception.
                throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(cachedSessionStamp.Last));

            // If currently cached 'Connected' stamp indicates that other session has been connected since this instance application current active session has been originally connected..
            else if (!StampsTranslator.IsRepresentingThisSession(cachedSessionStamp.Connected))
                // .. throw appropriate exception.
                throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(cachedSessionStamp.Connected));

            // If currently cached 'Last' stamp is associated with current application sessionId but its indicating that the current session has been disconnected
            if (StampsTranslator.IsRepresentingThisSession(cachedSessionStamp.Last) && StampsTranslator.IsRepresentingNonActiveLastStamp(cachedSessionStamp.Last))
                // .. throw appropriate exception.
                throw new ActiveSessionInterruptedByOwnInstanceException();

            // The only purpose of this try statement is to capture ActiveSessionInterruptedByOtherInstanceException indicating that the current active session has been forced to disconnect
            // and set _sessionForcedToDisconnect indicating it before re-throwing this exception up the stack. This try statement doesn't capturing above ActiveSessionInterruptedByOtherInstanceException
            // purposely as the getting thrown based on the content of this._cachedSessionStamp and as such if any of them indicates such a event,
            // this._sessionForcedToDisconnect should be already set to true by SessionRepository session maintenance loop.
            try
            {
                // If age of cached 'Last' stamp can be compared
                if (!StampsTranslator.IsRepresentingNonActiveLastStamp(cachedSessionStamp.Last))
                {
                    string lastTimestamp = StampsTranslator.GetTimestampStampSection(cachedSessionStamp.Last);
                    DateTime lastDateTime = DateTimeExtensions.FromTimestamp(lastTimestamp);
                    int secondsElpasedFromLastStamp = (DateTimeExtensions.GetNISTNow() - lastDateTime).Seconds;

                    // If currently owned cached stamp is not older than maintenance refresh await period,
                    // simply return assuming that session control has been assured.
                    if (secondsElpasedFromLastStamp < SessionsRepository._maintenanceAwaitSec)
                        return;
                    
                    // If currently owned cached stamp is older than maintenance refresh await period, adhoc refresh it, and analyze it.
                    else if (secondsElpasedFromLastStamp < SessionsRepository._longTimeUnresponsiveSecondsLimit)
                        // Analyze current session state to asses whether no other instance
                        // of the application is currently attempting to established new session, or
                        // already started new session, and if so throw appropriate exception.
                        await this.AnalyzeActiveSessionStateAsync();

                    // If currently cached stamp hasn't been refreshed so long that is currently considered for long time unresponsive...
                    else
                        // ... throw appropriate exception
                        throw new SessionMaintenanceFailureException();
                }

                // Otherwise if age of cached 'Last' stamp cannot be compared
                else if (StampsTranslator.IsRepresentingNonActiveLastStamp(cachedSessionStamp.Last))
                    // Analyze current session state to asses whether no other instance
                    // of the application is currently attempting to established new session, or
                    // already started new session, and if so throw appropriate exception.
                    await this.AnalyzeActiveSessionStateAsync();
            }
            // Catch exception indicating that the active session has been taken over by other instance application session, set appropriate bool flag indicating that event to true and re-throw the exception
            catch (ActiveSessionInterruptedByOtherInstanceException)
            {
                this._sessionForcedToDisconnect = true;
                throw;
            }
        }

        // Ends active session or attempt to activate one.
        public async Task EndSessionAsync()
        {
            await SessionsRepository._endSessionSemaphore.WaitAsync();
            
            try
            {
                if (SessionsRepository._sessionActive is false) return;

                SessionsRepository._sessionActive = null;

                // Update end active session appropriate stamps
                await this._dataOperator.UpdateEndActiveSessionAsync(StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow()));

                // Refresh cached sessions stamp
                await this._dataOperator.RefreshCachedSessionStampAsync();

                // Clear all own 'ConnectedIn' stamps
                await this._dataOperator.UpdateClearOwnConnectedInStampsAsync();

                SessionsRepository._sessionActive = false;
            }
            finally { SessionsRepository._endSessionSemaphore.Release(); }
        }
        #endregion

        #region Private Helper
        // OperationCanceledException
        // OwnInstanceSessionIsInactiveOrActivatingException
        private async Task MaintainActiveSessionAsync()
        {
            // If '_sessionActive' flag indicates that session is not active...
            if (!(SessionsRepository._sessionActive is true))
                // .. throw appropriate exception.
                throw new OwnInstanceSessionIsInactiveOrActivatingException();

            // If '_sessionMaintenanceActive' indicates that session maintenance is already ongoing
            else if (SessionsRepository._sessionMaintenanceActive)
                // .. throw appropriate exception.
                throw new SessionMaintenanceIsAlreadyOngoingException();

            // Await session maintenance semaphore to allow to proceed...
            await SessionsRepository._sessionsSemaphore.WaitAsync();

            // .. than retry all the already performed above checks..
            try
            {
                // If '_sessionActive' flag indicates that session is not active...
                if (!(SessionsRepository._sessionActive is true))
                    // .. throw appropriate exception.
                    throw new OwnInstanceSessionIsInactiveOrActivatingException();

                // If '_sessionMaintenanceActive' indicates that session maintenance is already ongoing
                else if (SessionsRepository._sessionMaintenanceActive)
                    // .. throw appropriate exception.
                    throw new SessionMaintenanceIsAlreadyOngoingException();

                // Set '_sessionMaintenanceActive' flag to indicate that active session maintenance is ongoing.
                SessionsRepository._sessionMaintenanceActive = true;
            }
            // finally release semaphore...
            finally { SessionsRepository._sessionsSemaphore.Release(); }

            try
            {
                DateTime lastTaskDelayStarted = DateTime.Now;
                DateTime lastTaskDelayCompleted = DateTime.Now;

                // Repeat till not canceled
                while (true)
                {
                    // .. await _maintenance await ms const, relative to last loop cycle time.
                    lastTaskDelayStarted = DateTime.Now;
                    await TaskProvider.DelayRelativeToLastDelay(lastTaskDelayStarted, lastTaskDelayCompleted, SessionsRepository._maintenanceAwaitMs, SessionsRepository._token);
                    lastTaskDelayCompleted = DateTime.Now;

                    //
                    SessionsRepository._token.ThrowIfCancellationRequested();

                    // .. then analyze current session state to asses whether no other instance
                    // of the application is currently attempting to established new session, or
                    // already started new session, and if so throw appropriate exception.
                    await this.AnalyzeActiveSessionStateAsync();

                    //
                    SessionsRepository._token.ThrowIfCancellationRequested();

                    // If session active flag still indicates that the session is active update 'Last' stamp value.
                    await this._dataOperator.UpdateMaintainActiveSessionAsync(StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow()));
                }
            }
            // If forced to disconnect active session, update appropriate end session stamp.
            catch (ActiveSessionInterruptedByOtherInstanceException)
            {
                await this.EndSessionOnForcedToDisconnectAsync();
                throw;
            }
            catch (Exception)
            {
                await this.EndSessionAsync();
                throw;
            }
            finally { SessionsRepository._sessionMaintenanceActive = false; }
        }

        private async Task<SessionPreActivationAnalyzeProfile> AssesNewSessionStartViability(bool forceOtherInstanceActiveSessionTakeover)
        {

            // Perform PreActivation analyze
            try
            {
                // Analyze current pre activation conditions
                SessionPreActivationAnalyzeProfile analyze = await this.GetAnalyzeNewSessionPreActivationConditions();

                // If cancellation of the task has been requested, cancel the task by throwing TaskCanceledException
                SessionsRepository._token.ThrowIfCancellationRequested();

                // OtherInstanceSessionAlreadyActiveException - When unable to start new session activation because other application instance currently owns active session, 
                // or failed during active session and didn't clean up 'Last' stamp after itself as recently that this stamp is not considered as unresponsive yet, as well
                // as provided forceOtherInstanceActiveSessionTakeover flag indicates that takeover of active session is not allowed.
                if (analyze.IsOtherInstanceSessionActive && !forceOtherInstanceActiveSessionTakeover)
                    throw new OtherInstanceSessionAlreadyActiveException();

                // OtherInstanceSessionAlreadyStartingException - When unable to start new session activation because other application instance is currently in a process of starting new session, 
                // or failed during activation process and didn't clean up one of the 'ConnectedIn' stamps after itself as recently that this stamps are not considered as unresponsive yet.
                else if (analyze.IsOtherInstanceSessionAttemtingToActivate && !forceOtherInstanceActiveSessionTakeover)
                    throw new OtherInstanceSessionAlreadyStartingException();

                return analyze;
            }
            // If failed or canceled pre activation conditions ..
            catch
            {
                // correct _sessionActive value to indicate session being inactive..
                _sessionActive = false;
                // .. and re-throw exception
                throw;
            }
        }

        private async Task AwaitStartNewSessionToBeAllowed(SessionPreActivationAnalyzeProfile analyze, SessionActivationAnalyzeProfile activationAnalyze)
        {
            // Await till attempt to start new session will be allowed to start new session.
            try
            {
                // if other instance application session is active
                if (analyze.IsOtherInstanceSessionActive)
                {
                    activationAnalyze.SessionTakeoverRequired = true;

                    try
                    {
                        // If session has been forced to be disconnected recently await additional time prior to forcing session takeover.
                        if (analyze.HasBeenForcedToDisconnectRecently)
                        {
                            // Build empty SessionAwaitAllowedTakeoverAnalyzeProfile and fill it with data based on the previously obtained 
                            SessionAwaitAllowedTakeoverAnalyzeProfile awaitAllowedTakeoverAnalyze = new SessionAwaitAllowedTakeoverAnalyzeProfile(analyze);

                            // Await session takeover to become allowed.
                            await this.AwaitAllowedSessionTakeoverAfterRecentTakeover(awaitAllowedTakeoverAnalyze);

                            // Assign current values from the _cachedSessionStamp to appropriate analyze profile variables...
                            activationAnalyze.OriginalForcedToDisconnectStamp = this._cachedSessionStamp.LastForcedToDisconnect;
                            activationAnalyze.OriginalDisconnectedStamp = this._cachedSessionStamp.Disconnected;
                            activationAnalyze.OriginalLastStamp = this._cachedSessionStamp.Last;
                            activationAnalyze.OriginalConnectedStamp = this._cachedSessionStamp.Connected;
                        }
                    }
                    // If awaiting session takeover to be allowed has been interrupted because session which was gonna be taken over willingly disconnect in a meantime...
                    catch (OtherInstanceActiveSessionHasBeenDisconnectedException)
                    // Start new session quick instead of taking it over...
                    {
                        activationAnalyze.SessionTakeoverRequired = false;
                        activationAnalyze.QuickStartAllowed = true;
                    }
                }

                // if other instance application session is currently attempting to 
                else if (analyze.IsOtherInstanceSessionAttemtingToActivate)
                {
                    activationAnalyze.SessionTakeoverRequired = true;

                    try
                    {
                        // Build empty SessionAwaitAllowedTakeoverAnalyzeProfile and fill it with data based on the previously obtained 
                        SessionAwaitAllowedTakeoverAnalyzeProfile awaitAllowedTakeoverAnalyze = new SessionAwaitAllowedTakeoverAnalyzeProfile(analyze);
                        awaitAllowedTakeoverAnalyze.OriginalConnectedIn15Stamp = this._cachedSessionStamp.ConnectedIn15;
                        awaitAllowedTakeoverAnalyze.OriginalConnectedIn30Stamp = this._cachedSessionStamp.ConnectedIn30;
                        awaitAllowedTakeoverAnalyze.OriginalConnectedIn45Stamp = this._cachedSessionStamp.ConnectedIn45;
                        awaitAllowedTakeoverAnalyze.OriginalConnectedIn60Stamp = this._cachedSessionStamp.ConnectedIn60;
                        awaitAllowedTakeoverAnalyze.OriginalConnectedIn90Stamp = this._cachedSessionStamp.ConnectedIn90;
                        awaitAllowedTakeoverAnalyze.OriginalConnectedIn120Stamp = this._cachedSessionStamp.ConnectedIn120;

                        // Await session takeover to become allowed.
                        await this.AwaitAllowedSessionTakeoverAfterSessionActivation(awaitAllowedTakeoverAnalyze);

                        // Assign current values from the _cachedSessionStamp to appropriate analyze profile variables...
                        activationAnalyze.OriginalForcedToDisconnectStamp = this._cachedSessionStamp.LastForcedToDisconnect;
                        activationAnalyze.OriginalDisconnectedStamp = this._cachedSessionStamp.Disconnected;
                        activationAnalyze.OriginalLastStamp = this._cachedSessionStamp.Last;
                        activationAnalyze.OriginalConnectedStamp = this._cachedSessionStamp.Connected;
                    }
                    // If awaiting session takeover to be allowed has been interrupted because session which was gonna be taken over willingly disconnect in a meantime...
                    catch (OtherInstanceActiveSessionHasBeenDisconnectedException)
                    // Start new session quick instead of taking it over...
                    {
                        activationAnalyze.SessionTakeoverRequired = false;
                        activationAnalyze.QuickStartAllowed = true;
                    }
                }
                // if 'Last' stamp indicates that session has been disconnected, but it hasn't happened recently...
                else if ((analyze.IsLastDisconnected && !(analyze.IsLastDisconnectedRecently))
                        // .. or is long time unresponsive...
                        || analyze.LastSessionIsLongTimeUnresponsive)
                        // allow quick start
                        activationAnalyze.QuickStartAllowed = true;
            }
            // Should happened only when cross threaded operation interrupts own new session activation or takeover process
            catch (SessionActivationInterruptedByOwnInstanceException) { throw; }
            // If Session failed to get established because new session activation or takeover process has been got interrupted
            // by other application instance initializing, or refreshing its own session, throw appropriate exception.
            catch (SessionActivationInterruptedByOtherInstanceException)
            {
                // Set session active flag to false
                SessionsRepository._sessionActive = false;

                // re-throw exception
                throw;
            }
            // or if session activation cancellation has been requested..
            catch (Exception ex) when (ex.GetType().IsAssignableFrom(typeof(TaskCanceledException)))
            {
                // Set session active flag to false
                SessionsRepository._sessionActive = false;

                // re-throw exception
                throw;
            }
        }

        private async Task AttemptToStartNewSession(SessionActivationAnalyzeProfile activationAnalyze)
        {
            // Attempt to start new session.
            try
            {
                // If session takeover is required perform it...
                if (activationAnalyze.SessionTakeoverRequired)
                    try { await this.StartOtherInstanceSessionTakeover(activationAnalyze); }
                    catch (OtherInstanceActiveSessionHasBeenDisconnectedException)
                    { await this.StartNewSessionQuick(activationAnalyze); }
                // If quick start is allowed perform it...
                else if (activationAnalyze.QuickStartAllowed)
                    await this.StartNewSessionQuick(activationAnalyze);
                // ... otherwise perform long start.
                else
                    await this.StartNewSessionLong(activationAnalyze);

                // Execute appropriate event.
                SessionsRepository._sessionActive = true;
            }
            // Should happened only when cross threaded operation interrupts own new session activation or takeover process
            catch (SessionActivationInterruptedByOwnInstanceException) { throw; }
            // If Session failed to get established because new session activation or takeover process has been got interrupted
            // by other application instance initializing, or refreshing its own session, throw appropriate exception.
            catch (SessionActivationInterruptedByOtherInstanceException)
            {
                // Await till EndSessionOnForcedToDisconnectAsync() will assure that session activation process will be canceled
                // correctly, and that 'ForcedToDisconnect', 'Disconnected' and 'Last' stamps will get updated with appropriate values.
                await this.EndSessionOnForcedToDisconnectAsync();

                // re-throw exception
                throw;
            }
            // or if session activation cancellation has been requested..
            catch (Exception ex) when (ex.GetType().IsAssignableFrom(typeof(TaskCanceledException)))
            {
                // Await till EndSessionActivationAsync() will assure that session activation process will be canceled
                // correctly, and that 'Last' stamp spreadsheet will get updated with appropriate stamp.
                await this.EndSessionActivationAsync();

                // re-throw exception
                throw;
            }
        }
        
        // Ends session or attempt to activate one.
        // SHOULD BE USE ONLY BEFORE MaintainActiveSessionAsync() started
        private async Task EndSessionActivationAsync()
        {
            await SessionsRepository._endSessionSemaphore.WaitAsync();

            try
            {
                if (SessionsRepository._sessionActive is false) return;

                SessionsRepository._sessionActive = null;

                // Update end session activation appropriate stamps
                await this._dataOperator.UpdateEndSessionActivationAsync();

                // Refresh cached sessions stamp
                await this._dataOperator.RefreshCachedSessionStampAsync();

                // Clear all own 'ConnectedIn' stamps
                await this._dataOperator.UpdateClearOwnConnectedInStampsAsync();

                SessionsRepository._sessionActive = false;
            }
            finally { SessionsRepository._endSessionSemaphore.Release(); }
        }

        // Ends active session or attempt to activate one (when action is forced by other application instance).
        private async Task EndSessionOnForcedToDisconnectAsync()
        {
            // Set '_sessionForcedToDisconnect' flag before awaiting for semaphore to assure that
            // to prevent any other session to lock db content while 
            this._sessionForcedToDisconnect = true;

            await SessionsRepository._endSessionSemaphore.WaitAsync();

            try
            {
                if (SessionsRepository._sessionActive is false) return;

                SessionsRepository._sessionActive = null;
                
                // Update forced to end active session appropriate stamps
                await this._dataOperator.UpdateForcedEndActiveSessionAsync(StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow()));
                
                // Refresh cached sessions stamp
                await this._dataOperator.RefreshCachedSessionStampAsync();
                
                // Clear all own 'ConnectedIn' stamps
                await this._dataOperator.UpdateClearOwnConnectedInStampsAsync();

                SessionsRepository._sessionActive = false;
            }
            finally { SessionsRepository._endSessionSemaphore.Release(); }
}

        // Await till other instance session which currently owns recently forced to take over session disconnect, or 180 sec
        // till it will be allowed to forced disconnect on that session itself.
        private async Task AwaitAllowedSessionTakeoverAfterRecentTakeover(SessionAwaitAllowedTakeoverAnalyzeProfile awaitAllowedTakeoverAnalyze)
        {
            // Get timestamp representing last time when any application instance has been forced to log out, and DateTime value associated with it.
            string lastForcedToDisconnectTimestamp = StampsTranslator.GetTimestampStampSection(awaitAllowedTakeoverAnalyze.OriginalForcedToDisconnectStamp);
            DateTime lastForcedToDisconnectDateTime = DateTimeExtensions.FromTimestamp(lastForcedToDisconnectTimestamp);

            // Calculate number of second elapsed from the time when
            int stampAgeSec = (int)(DateTimeExtensions.GetNISTNow() - lastForcedToDisconnectDateTime).TotalSeconds;

            // If last forced to disconnect time indicate that forced disconnect occurred more then 180 seconds ago,
            // return as awaiting session takeover is now longer not-allowed.
            if (stampAgeSec > (300 - 120))
                return;

            // Set SecondsToNewSession to await appropriate amount of seconds
            // to assure that at least (300 - 120) 180 seconds elapsed  from the point
            // when one session has been forced to disconnect, to the point when current
            // session attempting active session takeover.(which will take further 120 seconds)
            this.SecondsToNewSession = 300 - stampAgeSec;

            // Await at least 180 seconds elapsed from the time of last forced disconnect.
            // Or till the session which have taken over has willingly disconnected.
            while (this.SecondsToNewSession > 120)
            {
                // Await one second.
                await Task.Delay(1000, SessionsRepository._token);
                // Decrease number of seconds to await by one.
                this.SecondsToNewSession--;

                // If current value of seconds to id divisible by 5
                if (this.SecondsToNewSession % 5 == 0)
                    // .. analyze whether awaiting for allowed session takeover is still a valid behavior
                    // considering the changes in a session stamp after it being refreshed.
                    try { await this.AnalyzeAwaitAllowedSessionTakeoverAfterRecentTakeoverStateAsync(awaitAllowedTakeoverAnalyze); }
                    catch (OtherInstanceActiveSessionHasBeenDisconnectedException) { this.SecondsToNewSession = 120; }
            }
        }

        // Await till other instance session which currently attempting to activate new session
        // will activate its session, then await another 180 sec prior to beginning 120 sec process of session takeover.
        private async Task AwaitAllowedSessionTakeoverAfterSessionActivation(SessionAwaitAllowedTakeoverAnalyzeProfile awaitAllowedTakeoverAnalyze)
        {
            // Declare empty list of string..
            IDictionary<int, string> connectedIn = new Dictionary<int, string>();

            // .. and add all non empty connectedIn stamps to it
            if (!string.IsNullOrWhiteSpace(awaitAllowedTakeoverAnalyze.OriginalConnectedIn15Stamp))
                connectedIn.Add(15, awaitAllowedTakeoverAnalyze.OriginalConnectedIn15Stamp);

            if (!string.IsNullOrWhiteSpace(awaitAllowedTakeoverAnalyze.OriginalConnectedIn30Stamp))
                connectedIn.Add(30, awaitAllowedTakeoverAnalyze.OriginalConnectedIn30Stamp);

            if (!string.IsNullOrWhiteSpace(awaitAllowedTakeoverAnalyze.OriginalConnectedIn45Stamp))
                connectedIn.Add(45, awaitAllowedTakeoverAnalyze.OriginalConnectedIn45Stamp);

            if (!string.IsNullOrWhiteSpace(awaitAllowedTakeoverAnalyze.OriginalConnectedIn60Stamp))
                connectedIn.Add(60, awaitAllowedTakeoverAnalyze.OriginalConnectedIn60Stamp);

            if (!string.IsNullOrWhiteSpace(awaitAllowedTakeoverAnalyze.OriginalConnectedIn90Stamp))
                connectedIn.Add(90, awaitAllowedTakeoverAnalyze.OriginalConnectedIn90Stamp);

            if (!string.IsNullOrWhiteSpace(awaitAllowedTakeoverAnalyze.OriginalConnectedIn120Stamp))
                connectedIn.Add(120, awaitAllowedTakeoverAnalyze.OriginalConnectedIn120Stamp);

            // If no valid 'ConnectedIn' stamps found, throw appropriate exception.
            if (connectedIn.Count == 0)
                throw new ArgumentException($"Provided SessionAwaitAllowedTakeoverAnalyzeProfile is not valid to be used with {nameof(AwaitAllowedSessionTakeoverAfterSessionActivation)} method, " +
                    $"because it does not contain any valid 'ConnectedIn' stamps.", nameof(awaitAllowedTakeoverAnalyze));

            // Get first connected in stamp and assign it as lastConnectedInStamp variable.
            int lastConnectedInStampInt = connectedIn.First().Key;
            string lastConnectedInStampString = connectedIn.First().Value;

            // Loop through all the 'ConnectedIn' stamps found and find the one representing latest time.
            foreach (KeyValuePair<int, string> item in connectedIn)
            {
                int connectedInStampInt = item.Key;
                string connectedInStampString = item.Value;

                if (StampsTranslator.IsRepresentingLaterSession(connectedInStampString, lastConnectedInStampString))
                {
                    lastConnectedInStampInt = connectedInStampInt;
                    lastConnectedInStampString = connectedInStampString;
                }
            }

            // Get last connected in timestamp
            string lastConnectedInTimestamp = StampsTranslator.GetTimestampStampSection(lastConnectedInStampString);
            DateTime lastConnectedInDateTime = DateTimeExtensions.FromTimestamp(lastConnectedInTimestamp);

            // Get timestamp representing estimated time when other application instance will complete activating new session plus 180 seconds.
            DateTime estimatedConnectDateTime = lastConnectedInDateTime.AddSeconds(lastConnectedInStampInt + 180);
            string estimatedConnectTimestamp = estimatedConnectDateTime.ToTimestamp();

            // Calculate number of second must elapsed so other application instance would manage to complete its session activation process. from the time when 
            int stampAwaitSec = (int)(estimatedConnectDateTime - DateTimeExtensions.GetNISTNow()).TotalSeconds;

            // Set SecondsToNewSession to await estimated amount of time
            // it will take other application instance session to activate + 180 sec 
            this.SecondsToNewSession = stampAwaitSec;

            // Await at least 180 seconds elapsed from the time of last forced disconnect.
            // Or till the session which have taken over has willingly disconnected.
            while (this.SecondsToNewSession > 120)
            {
                // Await one second.
                await Task.Delay(1000, SessionsRepository._token);
                // Decrease number of seconds to await by one.
                this.SecondsToNewSession--;

                // If current value of seconds to id divisible by 5
                if (this.SecondsToNewSession % 5 == 0)
                    // .. analyze whether awaiting for allowed session takeover is still a valid behavior
                    // considering the changes in a session stamp after it being refreshed.
                    await this.AnalyzeAwaitAllowedSessionTakeoverAfterSessionActivationStateAsync(awaitAllowedTakeoverAnalyze, lastConnectedInStampString);
            }
        }


        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        // SessionActivationInterruptedByOtherInstanceException - When ongoing new session activation has been interrupted and failed because other application instance,
        // either began the new session activation process, or activating session by updating value of 'Connected' stamp
        private async Task StartOtherInstanceSessionTakeover(SessionActivationAnalyzeProfile activationAnalyze)
        {
            activationAnalyze.ConnectedAnalyzeRequired = false;
            activationAnalyze.ConnectedIn15AnalyzeRequired = false;
            activationAnalyze.ConnectedIn30AnalyzeRequired = false;
            activationAnalyze.ConnectedIn45AnalyzeRequired = false;
            activationAnalyze.ConnectedIn60AnalyzeRequired = false;
            activationAnalyze.ConnectedIn90AnalyzeRequired = false;
            activationAnalyze.ConnectedIn120AnalyzeRequired = true;


            // Set SecondsToNewSession to await 120 sec before establishing forced new session
            this.SecondsToNewSession = 120;

            //
            string stamp = StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow());
            await this._dataOperator.UpdateSessionActivationConnectedIn120Async(stamp);
            activationAnalyze.ExpectedConnectedIn120Stamp = stamp;

            DateTime lastTaskDelayStarted = DateTime.Now;
            DateTime lastTaskDelayCompleted = DateTime.Now;

            // Await 30 sec
            while (this.SecondsToNewSession >= 0)
            {
                // Await one second relative to last loop time.
                lastTaskDelayStarted = DateTime.Now;
                await TaskProvider.DelayRelativeToLastDelay(lastTaskDelayStarted, lastTaskDelayCompleted, 1000, SessionsRepository._token);
                lastTaskDelayCompleted = DateTime.Now;
                
                // Decrease number of seconds to await by one.
                this.SecondsToNewSession--;

                // If current value of seconds to id divisible by 5
                if (this.SecondsToNewSession % 5 == 0)
                    // .. analyze whether session activation process is still valid.
                    await this.AnalyzeCurrentForcedSessionActivationStateAsync(activationAnalyze);

                // Get new current stamp
                stamp = StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow());
                
                // If currently 90 sec left...
                if (this.SecondsToNewSession == 90)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn120To90Async(stamp);
                    activationAnalyze.ExpectedConnectedIn90Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn90AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn120AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn120AnalyzeCompleted = true;
                }

                // If currently 60 sec left...
                else if (this.SecondsToNewSession == 60)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn90To60Async(stamp);
                    activationAnalyze.ExpectedConnectedIn60Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn60AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn90AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn90AnalyzeCompleted = true;
                }

                // If currently 45 sec left...
                else if(this.SecondsToNewSession == 45)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn60To45Async(stamp);
                    activationAnalyze.ExpectedConnectedIn45Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn45AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn60AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn60AnalyzeCompleted = true;
                }

                // If currently 30 sec left...
                else if (this.SecondsToNewSession == 30)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn45To30Async(stamp);
                    activationAnalyze.ExpectedConnectedIn30Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn30AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn45AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn45AnalyzeCompleted = true;
                }

                // If currently 15 sec left...
                else if (this.SecondsToNewSession == 15)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn30To15Async(stamp);
                    activationAnalyze.ExpectedConnectedIn15Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn15AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn30AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn30AnalyzeCompleted = true;
                }

                // If currently 0 sec left...
                else if (this.SecondsToNewSession == 0)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn15ToConnectedAsync(stamp);
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    await this._dataOperator.UpdateClearOwnConnectedInStampsAsync();
                    activationAnalyze.ConnectedAnalyzeRequired = true;
                    activationAnalyze.ConnectedIn15AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn15AnalyzeCompleted = true;
                }
            }

        }

        
        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        // SessionActivationInterruptedByOtherInstanceException - When ongoing new session activation has been interrupted and failed because other application instance,
        // either began the new session activation process, or activating session by updating value of 'Connected' stamp
        private async Task StartNewSessionLong(SessionActivationAnalyzeProfile activationAnalyze)
        {
            activationAnalyze.ConnectedAnalyzeRequired = false;
            activationAnalyze.ConnectedIn15AnalyzeRequired = false;
            activationAnalyze.ConnectedIn30AnalyzeRequired = false;
            activationAnalyze.ConnectedIn45AnalyzeRequired = false;
            activationAnalyze.ConnectedIn60AnalyzeRequired = true;
            activationAnalyze.ConnectedIn90AnalyzeRequired = false;
            activationAnalyze.ConnectedIn120AnalyzeRequired = false;

            // Set SecondsToNewSession to await 60 sec before establishing new session
            this.SecondsToNewSession = 60;

            //
            string stamp = StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow());
            await this._dataOperator.UpdateSessionActivationConnectedIn60Async(stamp);
            activationAnalyze.ExpectedConnectedIn60Stamp = stamp;

            DateTime lastTaskDelayStarted = DateTime.Now;
            DateTime lastTaskDelayCompleted = DateTime.Now;

            // Await 30 sec
            while (this.SecondsToNewSession >= 0)
            {
                // Await one second relative to last loop time.
                lastTaskDelayStarted = DateTime.Now;
                await TaskProvider.DelayRelativeToLastDelay(lastTaskDelayStarted, lastTaskDelayCompleted, 1000, SessionsRepository._token);
                lastTaskDelayCompleted = DateTime.Now;

                // Decrease number of seconds to await by one.
                this.SecondsToNewSession--;

                // If current value of seconds to id divisible by 5
                if (this.SecondsToNewSession % 5 == 0)
                    // .. analyze whether session activation process is still valid.
                    await this.AnalyzeCurrentSessionActivationStateAsync(activationAnalyze);

                // Get new current stamp
                stamp = StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow());

                // If currently 45 sec left...
                if (this.SecondsToNewSession == 45)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn60To45Async(stamp);
                    activationAnalyze.ExpectedConnectedIn45Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn45AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn60AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn60AnalyzeCompleted = true;
                }

                // If currently 30 sec left...
                else if (this.SecondsToNewSession == 30)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn45To30Async(stamp);
                    activationAnalyze.ExpectedConnectedIn30Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn30AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn45AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn45AnalyzeCompleted = true;
                }

                // If currently 15 sec left...
                else if (this.SecondsToNewSession == 15)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn30To15Async(stamp);
                    activationAnalyze.ExpectedConnectedIn15Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn15AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn30AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn30AnalyzeCompleted = true;
                }

                // If currently 0 sec left...
                else if (this.SecondsToNewSession == 0)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn15ToConnectedAsync(stamp);
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    await this._dataOperator.UpdateClearOwnConnectedInStampsAsync();
                    activationAnalyze.ConnectedAnalyzeRequired = true;
                    activationAnalyze.ConnectedIn15AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn15AnalyzeCompleted = true;
                }
            }
        }

        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        // SessionActivationInterruptedByOtherInstanceException - When ongoing new session activation has been interrupted and failed because other application instance,
        // either began the new session activation process, or activating session by updating value of 'Connected' stamp
        private async Task StartNewSessionQuick(SessionActivationAnalyzeProfile activationAnalyze)
        {
            activationAnalyze.ConnectedAnalyzeRequired = false;
            activationAnalyze.ConnectedIn15AnalyzeRequired = false;
            activationAnalyze.ConnectedIn30AnalyzeRequired = true;
            activationAnalyze.ConnectedIn45AnalyzeRequired = false;
            activationAnalyze.ConnectedIn60AnalyzeRequired = false;
            activationAnalyze.ConnectedIn90AnalyzeRequired = false;
            activationAnalyze.ConnectedIn120AnalyzeRequired = false;

            // Set SecondsToNewSession to await 30 sec before establishing new session
            this.SecondsToNewSession = 30;
            
            //
            string stamp = StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow());
            await this._dataOperator.UpdateSessionActivationConnectedIn30Async(stamp);
            activationAnalyze.ExpectedConnectedIn30Stamp = stamp;

            //
            DateTime lastTaskDelayStarted = DateTime.Now;
            DateTime lastTaskDelayCompleted = DateTime.Now;

            // Await 30 sec
            while (this.SecondsToNewSession >= 0)
            {
                // Await one second relative to last loop time.
                lastTaskDelayStarted = DateTime.Now;
                await TaskProvider.DelayRelativeToLastDelay(lastTaskDelayStarted, lastTaskDelayCompleted, 1000, SessionsRepository._token);
                lastTaskDelayCompleted = DateTime.Now;

                // Decrease number of seconds to await by one.
                this.SecondsToNewSession--;

                // If current value of seconds to id divisible by 5
                if (this.SecondsToNewSession % 5 == 0)
                    // .. analyze whether session activation process is still valid.
                    await this.AnalyzeCurrentSessionActivationStateAsync(activationAnalyze);

                // Get current stamp
                stamp = StampsTranslator.GetStamp(DateTimeExtensions.GetNISTNow());

                // If currently 15 sec left...
                if (this.SecondsToNewSession == 15)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn30To15Async(stamp);
                    activationAnalyze.ExpectedConnectedIn15Stamp = stamp;
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    activationAnalyze.ConnectedIn15AnalyzeRequired = true;
                    activationAnalyze.ConnectedIn30AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn30AnalyzeCompleted = true;
                }

                // If currently 0 sec left...
                else if (this.SecondsToNewSession == 0)
                {
                    await this._dataOperator.UpdateSessionActivationConnectedIn15ToConnectedAsync(stamp);
                    await this._dataOperator.RefreshCachedSessionStampAsync();
                    await this._dataOperator.UpdateClearOwnConnectedInStampsAsync();
                    activationAnalyze.ConnectedAnalyzeRequired = true;
                    activationAnalyze.ConnectedIn15AnalyzeRequired = false;
                    activationAnalyze.ConnectedIn15AnalyzeCompleted = true;
                }
            }
        }

        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        // SessionActivationInterruptedByOtherInstanceException - When ongoing new session activation has been interrupted and failed because other application instance,
        // either began the new session activation process, or activating session by updating value of 'Connected' stamp

        // OtherInstanceActiveSessionHasBeenDisconnectedException - occurs whenever active session which takeover is awaited to be allowed has been marked as disconnected.
        private async Task AnalyzeAwaitAllowedSessionTakeoverAfterRecentTakeoverStateAsync(SessionAwaitAllowedTakeoverAnalyzeProfile awaitAllowedTakeoverAnalyze)
        {
            if (!(_sessionActive is null)) throw new SessionActivationInterruptedByOwnInstanceException();
            else
            {
                // Refresh cached session stamp
                await this._dataOperator.RefreshCachedSessionStampAsync();

                // If 'Connected' stamp changed over the course off session awaiting for allowed active session takeover...
                if (this._cachedSessionStamp.Connected != awaitAllowedTakeoverAnalyze.OriginalConnectedStamp)
                    // .. if 'Connected' stamp changed by own application instance
                    if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Connected))
                        throw new SessionActivationInterruptedByOwnInstanceException();
                    // .. if 'Connected' stamp changed by other application instance
                    else
                        throw new SessionActivationInterruptedByOtherInstanceException();

                // Check whether any other instances of the application are indicating creating new session..
                if (StampsTranslator.IsOtherInstanceStartingSession(this._cachedSessionStamp.ConnectedIn15, this._cachedSessionStamp.ConnectedIn30, this._cachedSessionStamp.ConnectedIn45,
                    this._cachedSessionStamp.ConnectedIn60, this._cachedSessionStamp.ConnectedIn90, this._cachedSessionStamp.ConnectedIn120))
                    // .. throw appropriate exception.
                    throw new SessionActivationInterruptedByOtherInstanceException();

                // If session awaited to be allowed to be taken over has been disconnected, what has been confirmed by comparing sessionId and timestamp
                // of the session to be take over with last disconnected session, as well as assuring that it was no forced disconnect.
                if (StampsTranslator.IsRepresentingSameSession(this._cachedSessionStamp.Disconnected, awaitAllowedTakeoverAnalyze.OriginalConnectedStamp)
                    && StampsTranslator.IsRepresentingLaterSession(this._cachedSessionStamp.Disconnected, awaitAllowedTakeoverAnalyze.OriginalConnectedStamp)
                    && (this._cachedSessionStamp.LastForcedToDisconnect != this._cachedSessionStamp.Disconnected))
                    // .. throw appropriate exception.
                    throw new OtherInstanceActiveSessionHasBeenDisconnectedException();
            }
        }
        

        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        // SessionActivationInterruptedByOtherInstanceException - When ongoing new session activation has been interrupted and failed because other application instance,
        // either began the new session activation process, or activating session by updating value of 'Connected' stamp

        private async Task AnalyzeAwaitAllowedSessionTakeoverAfterSessionActivationStateAsync(SessionAwaitAllowedTakeoverAnalyzeProfile awaitAllowedTakeoverAnalyze, string lastConnectedInStamp)
        {
            if (!(_sessionActive is null)) throw new SessionActivationInterruptedByOwnInstanceException();
            else
            {
                // Refresh cached session stamp
                await this._dataOperator.RefreshCachedSessionStampAsync();

                // If 'Connected' stamp changed over the course off session awaiting for allowed active session takeover...
                if (this._cachedSessionStamp.Connected != awaitAllowedTakeoverAnalyze.OriginalConnectedStamp)
                    // .. if 'Connected' stamp changed by own application instance
                    if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Connected))
                        throw new SessionActivationInterruptedByOwnInstanceException();
                    // .. if 'Connected' stamp changed, by the change does indicate connection of the other application instance session then
                    // the one which is awaited by the current application instance to complete session activation process, before
                    // the current application instance will be allowed to force new session activation itself.
                    else if (!StampsTranslator.IsRepresentingSameSession(this._cachedSessionStamp.Connected, lastConnectedInStamp))
                        throw new SessionActivationInterruptedByOtherInstanceException();

                string lastConnectedInSessionId = StampsTranslator.GetSessionIdStampSection(lastConnectedInStamp);

                // Assure that 'ConnectedIn' stamps
                string connectedIn15 = this._cachedSessionStamp.ConnectedIn15;
                string connectedIn30 = this._cachedSessionStamp.ConnectedIn30;
                string connectedIn45 = this._cachedSessionStamp.ConnectedIn45;
                string connectedIn60 = this._cachedSessionStamp.ConnectedIn60;
                string connectedIn90 = this._cachedSessionStamp.ConnectedIn90;
                string connectedIn120 = this._cachedSessionStamp.ConnectedIn120;

                if (!string.IsNullOrWhiteSpace(connectedIn15) && StampsTranslator.IsRepresentingSameSession(connectedIn15, lastConnectedInSessionId))
                    connectedIn15 = null;
                if (!string.IsNullOrWhiteSpace(connectedIn30) && StampsTranslator.IsRepresentingSameSession(connectedIn30, lastConnectedInSessionId))
                    connectedIn30 = null;
                if (!string.IsNullOrWhiteSpace(connectedIn45) && StampsTranslator.IsRepresentingSameSession(connectedIn45, lastConnectedInSessionId))
                    connectedIn45 = null;
                if (!string.IsNullOrWhiteSpace(connectedIn60) && StampsTranslator.IsRepresentingSameSession(connectedIn60, lastConnectedInSessionId))
                    connectedIn60 = null;
                if (!string.IsNullOrWhiteSpace(connectedIn90) && StampsTranslator.IsRepresentingSameSession(connectedIn90, lastConnectedInSessionId))
                    connectedIn90 = null;
                if (!string.IsNullOrWhiteSpace(connectedIn120) && StampsTranslator.IsRepresentingSameSession(connectedIn120, lastConnectedInSessionId))
                    connectedIn120 = null;

                // Check whether any other instances of the application are indicating creating new session..
                if (StampsTranslator.IsOtherInstanceStartingSession(connectedIn15, connectedIn30, connectedIn45, connectedIn60, connectedIn90, connectedIn120))
                    // .. throw appropriate exception.
                    throw new SessionActivationInterruptedByOtherInstanceException();
            }
        }

        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        private async Task<SessionPreActivationAnalyzeProfile> GetAnalyzeNewSessionPreActivationConditions()
        {
            // If current instance of session repository is no longer in a state of activating new session  
            if (!(_sessionActive is null)) throw new SessionActivationInterruptedByOwnInstanceException();
            else
            {
                // Refresh cached session stamp
                await this._dataOperator.RefreshCachedSessionStampAsync();
                
                // Construct empty session pre-activation analyze profile
                SessionPreActivationAnalyzeProfile analyzeProfile = new SessionPreActivationAnalyzeProfile();

                // Assign current values from the _cachedSessionStamp to appropriate analyze profile variables...
                analyzeProfile.OriginalForcedToDisconnectStamp = this._cachedSessionStamp.LastForcedToDisconnect;
                analyzeProfile.OriginalDisconnectedStamp = this._cachedSessionStamp.Disconnected;
                analyzeProfile.OriginalLastStamp = this._cachedSessionStamp.Last;
                analyzeProfile.OriginalConnectedStamp = this._cachedSessionStamp.Connected;


                // Check whether last session is disconnected.
                analyzeProfile.IsLastDisconnected = StampsTranslator.IsRepresentingDisconnectedStamp(this._cachedSessionStamp.Disconnected, this._cachedSessionStamp.Last);

                // If last session stamp indicate that session is connected..
                if (!analyzeProfile.IsLastDisconnected)
                {
                    // .. set lastSessionDisconnectedRecently to false
                    analyzeProfile.IsLastDisconnectedRecently = false;
                    // .. set lastSessionForcedToDisconnect to false
                    analyzeProfile.IsLastForcedToDisconnect = false;
                }
                else
                {
                    // .. check whether disconnected stamp represent recent disconnect. Recent equal - would not be considered for long unresponsive.
                    analyzeProfile.IsLastDisconnectedRecently = !StampsTranslator.IsRepresentingLongTimeUnresponsiveStamp(this._cachedSessionStamp.Disconnected);
                    // .. return true if 'LastForcedToDisconnect' 'Disconnected' stamp referring to this same session.
                    analyzeProfile.IsLastForcedToDisconnect = StampsTranslator.IsRepresentingForcedToDisconnectStamp(this._cachedSessionStamp.LastForcedToDisconnect, this._cachedSessionStamp.Disconnected, this._cachedSessionStamp.Last);
                }

                // Check whether this session has been recently forced to disconnect.
                analyzeProfile.HasBeenForcedToDisconnectRecently = StampsTranslator.IsRepresentingForcedToDisconnectStamp(this._cachedSessionStamp.LastForcedToDisconnect, this._cachedSessionStamp.Disconnected, SessionsRepository._sessionId)
                    && !StampsTranslator.IsRepresentingLongTimeUnresponsiveStamp(this._cachedSessionStamp.Disconnected);

                // Check whether any other instances of the application is indicating creating new session.
                if (StampsTranslator.IsOtherInstanceStartingSession(this._cachedSessionStamp.ConnectedIn15, this._cachedSessionStamp.ConnectedIn30, this._cachedSessionStamp.ConnectedIn45,
                    this._cachedSessionStamp.ConnectedIn60, this._cachedSessionStamp.ConnectedIn90, this._cachedSessionStamp.ConnectedIn120))
                    analyzeProfile.IsOtherInstanceSessionAttemtingToActivate = true;
                else
                    analyzeProfile.IsOtherInstanceSessionAttemtingToActivate = false;


                // Removes all 'ConnectedIn' stamps representing unresponsive stamp
                this._dataOperator.UpdateClearConnectedInUnresponsiveStamps(); 


                // Check whether last session is unresponsive
                analyzeProfile.LastSessionIsUnresponsive = StampsTranslator.IsRepresentingUnresponsiveStamp(this._cachedSessionStamp.Last) && !StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Last);
                if (!analyzeProfile.LastSessionIsUnresponsive)
                    analyzeProfile.LastSessionIsLongTimeUnresponsive = false;
                else
                    analyzeProfile.LastSessionIsLongTimeUnresponsive = StampsTranslator.IsRepresentingLongTimeUnresponsiveStamp(this._cachedSessionStamp.Last);
                
                // Check whether last session is active and throw appropriate exception if it is.
                if (this._cachedSessionStamp.Last.Length == SessionsRepository.SessionStampLenght && !analyzeProfile.LastSessionIsUnresponsive && !analyzeProfile.IsLastDisconnected && !StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Last)
                    && StampsTranslator.IsRepresentingSameSession(this._cachedSessionStamp.Last, this._cachedSessionStamp.Connected))
                    analyzeProfile.IsOtherInstanceSessionActive = true;
                else
                    analyzeProfile.IsOtherInstanceSessionActive = false;

                // Return pre activation analyze profile.
                return analyzeProfile;
            }
        }

        // SessionActivationInterruptedByOwnInstanceException - When ongoing new session activation has been interrupted and failed because own application instance attempted to start
        // another new session when process of activating new session if already ongoing. Should occur only in case of cross thread calls, which the method should be protected from with _sessionsLock.

        // SessionActivationInterruptedByOtherInstanceException - When ongoing new session activation has been interrupted and failed because other application instance,
        // either began the new session activation process, or activating session by updating value of 'Connected' stamp

        // OtherInstanceActiveSessionHasBeenDisconnectedException - occurs whenever session which is attempted to be taken over
        #region New Session Activation process state analyze
        // Assures that the new session activation process is followed correctly and throws appropriate exception accordingly if its not.
        private async Task AnalyzeCurrentSessionActivationStateAsync(SessionActivationAnalyzeProfile activationAnalyzeProfile)
        {
            try { await this.AnalyzeCurrentForcedSessionActivationStateAsync(activationAnalyzeProfile); }
            catch (OtherInstanceActiveSessionHasBeenDisconnectedException) { }            
        }

        // Assures that the new session activation process is followed correctly and throws appropriate exception accordingly if its not.
        private async Task AnalyzeCurrentForcedSessionActivationStateAsync(SessionActivationAnalyzeProfile activationAnalyzeProfile)
        {
            // If current instance of session repository is no longer in a state of activating new session  
            if (!(_sessionActive is null)) throw new SessionActivationInterruptedByOwnInstanceException();
            else
            {
                // Refresh cached session stamp
                await this._dataOperator.RefreshCachedSessionStampAsync();

                // If 'Last' stamp changed over the course off session activation process...
                if (this._cachedSessionStamp.Last != activationAnalyzeProfile.OriginalLastStamp)
                    // .. if 'Last' stamp changed by own application instance
                    if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Last))
                        throw new SessionActivationInterruptedByOwnInstanceException();
                    // .. if 'Last' stamp changed by other application instance, and it changed into the stamp not representing end of the session
                    else if (!StampsTranslator.IsRepresentingDisconnectedStamp(this._cachedSessionStamp.Disconnected, this._cachedSessionStamp.Last))
                        throw new SessionActivationInterruptedByOtherInstanceException();
                    // .. if 'Last' stamp representing disconnected, not own session, with sessionId equal to originalConnectedStamp
                    else if (StampsTranslator.IsRepresentingSameSession(this._cachedSessionStamp.Last, activationAnalyzeProfile.OriginalLastStamp))
                        throw new OtherInstanceActiveSessionHasBeenDisconnectedException();// This is thrown to speed up activation awaiting process

                // If 'Connected' stamp changed over the course off session activation process...
                if (this._cachedSessionStamp.Connected != activationAnalyzeProfile.OriginalConnectedStamp)
                    // .. if 'Connected' stamp changed by own application instance
                    if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Connected))
                        throw new SessionActivationInterruptedByOwnInstanceException();
                    // .. if 'Connected' stamp changed by other application instance
                    else
                        throw new SessionActivationInterruptedByOtherInstanceException();

                // Check whether any other instances of the application are indicating creating new session.
                if (StampsTranslator.IsOtherInstanceStartingSession(this._cachedSessionStamp.ConnectedIn15, this._cachedSessionStamp.ConnectedIn30, this._cachedSessionStamp.ConnectedIn45,
                    this._cachedSessionStamp.ConnectedIn60,this._cachedSessionStamp.ConnectedIn90, this._cachedSessionStamp.ConnectedIn120))
                    throw new SessionActivationInterruptedByOtherInstanceException();

                // If analyze ConnectedIn120 is required and haven't been completed, perform it.
                if (activationAnalyzeProfile.ConnectedIn120AnalyzeRequired && !activationAnalyzeProfile.ConnectedIn120AnalyzeCompleted)
                    this.AnalyzeCurrentSessionActivationState120(activationAnalyzeProfile.ExpectedConnectedIn120Stamp);

                // If analyze ConnectedIn90 is required and haven't been completed, perform it.
                if (activationAnalyzeProfile.ConnectedIn90AnalyzeRequired && !activationAnalyzeProfile.ConnectedIn90AnalyzeCompleted)
                    this.AnalyzeCurrentSessionActivationState90(activationAnalyzeProfile.ExpectedConnectedIn90Stamp);

                // If analyze ConnectedIn60 is required and haven't been completed, perform it.
                if (activationAnalyzeProfile.ConnectedIn60AnalyzeRequired && !activationAnalyzeProfile.ConnectedIn60AnalyzeCompleted)
                    this.AnalyzeCurrentSessionActivationState60(activationAnalyzeProfile.ExpectedConnectedIn60Stamp);

                // If analyze ConnectedIn45 is required and haven't been completed, perform it.
                if (activationAnalyzeProfile.ConnectedIn45AnalyzeRequired && !activationAnalyzeProfile.ConnectedIn45AnalyzeCompleted)
                    this.AnalyzeCurrentSessionActivationState45(activationAnalyzeProfile.ExpectedConnectedIn45Stamp);

                // If analyze ConnectedIn30 is required and haven't been completed, perform it.
                if (activationAnalyzeProfile.ConnectedIn30AnalyzeRequired && !activationAnalyzeProfile.ConnectedIn30AnalyzeCompleted)
                    this.AnalyzeCurrentSessionActivationState30(activationAnalyzeProfile.ExpectedConnectedIn30Stamp);

                // If analyze ConnectedIn15 is required and haven't been completed, perform it.
                if (activationAnalyzeProfile.ConnectedIn15AnalyzeRequired && !activationAnalyzeProfile.ConnectedIn15AnalyzeCompleted)
                    this.AnalyzeCurrentSessionActivationState15(activationAnalyzeProfile.ExpectedConnectedIn15Stamp);
            }
        }

        // Assures that the new session activation process is followed correctly and throws appropriate exception accordingly if its not.
        private void AnalyzeCurrentSessionActivationState120(string expectedConnectedIn120Stamp)
        {
            // Check original connectedIn120 stamp committed at the beginning of the session await 120 sec step
            if (this._cachedSessionStamp.ConnectedIn120 != expectedConnectedIn120Stamp)
                // If 'ConnectedIn120' stamp does represent this instance, but stamp change, throw appropriate exception.
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn120))
                    throw new SessionActivationInterruptedByOwnInstanceException();
                // If 'ConnectedIn120' stamp does no longer represents this instance, throw appropriate exception.
                else
                    throw new SessionActivationInterruptedByOtherInstanceException();
        }

        // Assures that the new session activation process is followed correctly and throws appropriate exception accordingly if its not.
        private void AnalyzeCurrentSessionActivationState90(string expectedConnectedIn90Stamp)
        {
            // Check original connectedIn90 stamp committed at the beginning of the session await 90 sec step
            if (this._cachedSessionStamp.ConnectedIn90 != expectedConnectedIn90Stamp)
                // If 'ConnectedIn90' stamp does represent this instance, but stamp change, throw appropriate exception.
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn90))
                    throw new SessionActivationInterruptedByOwnInstanceException();
                // If 'ConnectedIn90' stamp does no longer represents this instance, throw appropriate exception.
                else
                    throw new SessionActivationInterruptedByOtherInstanceException();
        }

        // Assures that the new session activation process is followed correctly and throws appropriate exception accordingly if its not.
        private void AnalyzeCurrentSessionActivationState60(string expectedConnectedIn60Stamp)
        {
            // Check original connectedIn60 stamp committed at the beginning of the session await 60 sec step
            if (this._cachedSessionStamp.ConnectedIn60 != expectedConnectedIn60Stamp)
                // If 'ConnectedIn60' stamp does represent this instance, but stamp change, throw appropriate exception.
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn60))
                    throw new SessionActivationInterruptedByOwnInstanceException();
                // If 'ConnectedIn60' stamp does no longer represents this instance, throw appropriate exception.
                else
                    throw new SessionActivationInterruptedByOtherInstanceException();
        }

        // Assures that the new session activation process is followed correctly and throws appropriate exception accordingly if its not.
        private void AnalyzeCurrentSessionActivationState45(string expectedConnectedIn45Stamp)
        {
            // Check original connectedIn45 stamp committed at the beginning of the session await 45 sec step
            if (this._cachedSessionStamp.ConnectedIn45 != expectedConnectedIn45Stamp)
                // If 'ConnectedIn45' stamp does represent this instance, but stamp change, throw appropriate exception.
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn45))
                    throw new SessionActivationInterruptedByOwnInstanceException();
                // If 'ConnectedIn45' stamp does no longer represents this instance, throw appropriate exception.
                else
                    throw new SessionActivationInterruptedByOtherInstanceException();
        }

        // Assures that the new session activation process is followed correctly and throws appropriate exception accordingly if its not.
        private void AnalyzeCurrentSessionActivationState30(string expectedConnectedIn30Stamp)
        {
            // Check original connectedIn30 stamp committed at the beginning of the session await 30 sec step
            if (this._cachedSessionStamp.ConnectedIn30 != expectedConnectedIn30Stamp)
                // If 'ConnectedIn30' stamp does represent this instance, but stamp change, throw appropriate exception.
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn30))
                    throw new SessionActivationInterruptedByOwnInstanceException();
                // If 'ConnectedIn30' stamp does no longer represents this instance, throw appropriate exception.
                else
                    throw new SessionActivationInterruptedByOtherInstanceException();
        }

        // Assures that the new session activation process is followed correctly and throws appropriate exception accordingly if its not.
        private void AnalyzeCurrentSessionActivationState15(string expectedConnectedIn15Stamp)
        {
            // Check original connectedIn15 stamp committed at the beginning of the session await 15 sec step
            if (this._cachedSessionStamp.ConnectedIn15 != expectedConnectedIn15Stamp)
                // If 'ConnectedIn15' stamp does represent this instance, but stamp change, throw appropriate exception.
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn15))
                    throw new SessionActivationInterruptedByOwnInstanceException();
                // If 'ConnectedIn15' stamp does no longer represents this instance, throw appropriate exception.
                else
                    throw new SessionActivationInterruptedByOtherInstanceException();
        }
        #endregion

        #region Active Session Maintenance process state analyze
        // ActiveSessionInterruptedByOwnInstanceException
        // ActiveSessionInterruptedByOtherInstanceException
        // Assures that the active session maintaining process is followed correctly and throws appropriate exception accordingly if its not.
        private async Task AnalyzeActiveSessionStateAsync()
        {
            // If session is not active throw appropriate exception
            if (!(_sessionActive is true)) throw new ActiveSessionInterruptedByOwnInstanceException();
            else
            {
                // Refresh cached session stamp
                await this._dataOperator.RefreshCachedSessionStampAsync();

                // If Last Session stamp indicates that this session has been marked as not active.
                if (StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Last) && StampsTranslator.IsRepresentingNonActiveLastStamp(this._cachedSessionStamp.Last))
                    throw new ActiveSessionInterruptedByOwnInstanceException();
                // Else if Last Session stamp value has been overridden by other application instance with stamp indicating something else that being inactive.
                else if (!StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Last) && !StampsTranslator.IsRepresentingNonActiveLastStamp(this._cachedSessionStamp.Last))
                    throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.Last));

                // If this 'Connected' stamp indicates that other session go connected, throw appropriate exception.
                else if (!StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.Connected))
                    throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.Connected));

                // If this 'ConnectedIn15' stamp indicates that a instance of the application attempts to start new session,
                // check whether it is own application instance, or not and throw appropriate exception accordingly.
                else if (!string.IsNullOrWhiteSpace(this._cachedSessionStamp.ConnectedIn15))
                    if (!StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn15))
                        throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.ConnectedIn15));
                    else
                        throw new ActiveSessionInterruptedByOwnInstanceException();

                // If this 'ConnectedIn30' stamp indicates that a instance of the application attempts to start new session,
                // check whether it is own application instance, or not and throw appropriate exception accordingly.
                else if (!string.IsNullOrWhiteSpace(this._cachedSessionStamp.ConnectedIn30))
                    if (!StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn30))
                        throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.ConnectedIn30));
                    else
                        throw new ActiveSessionInterruptedByOwnInstanceException();

                // If this 'ConnectedIn45' stamp indicates that a instance of the application attempts to start new session,
                // check whether it is own application instance, or not and throw appropriate exception accordingly.
                else if (!string.IsNullOrWhiteSpace(this._cachedSessionStamp.ConnectedIn45))
                    if (!StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn45))
                        throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.ConnectedIn45));
                    else
                        throw new ActiveSessionInterruptedByOwnInstanceException();

                // If this 'ConnectedIn60' stamp indicates that a instance of the application attempts to start new session,
                // check whether it is own application instance, or not and throw appropriate exception accordingly.
                else if (!string.IsNullOrWhiteSpace(this._cachedSessionStamp.ConnectedIn60))
                    if (!StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn60))
                        throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.ConnectedIn60));
                    else
                        throw new ActiveSessionInterruptedByOwnInstanceException();

                // If this 'ConnectedIn90' stamp indicates that a instance of the application attempts to start new session,
                // check whether it is own application instance, or not and throw appropriate exception accordingly.
                else if (!string.IsNullOrWhiteSpace(this._cachedSessionStamp.ConnectedIn90))
                    if (!StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn90))
                        throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.ConnectedIn90));
                    else
                        throw new ActiveSessionInterruptedByOwnInstanceException();

                // If this 'ConnectedIn120' stamp indicates that a instance of the application attempts to start new session,
                // check whether it is own application instance, or not and throw appropriate exception accordingly.
                else if (!string.IsNullOrWhiteSpace(this._cachedSessionStamp.ConnectedIn120))
                    if (!StampsTranslator.IsRepresentingThisSession(this._cachedSessionStamp.ConnectedIn120))
                        throw new ActiveSessionInterruptedByOtherInstanceException(SessionsRepository._sessionId, StampsTranslator.GetSessionIdStampSection(this._cachedSessionStamp.ConnectedIn120));
                    else
                        throw new ActiveSessionInterruptedByOwnInstanceException();
            }
        }
        #endregion
        #endregion

        #region IDisposable - support
        public void Dispose()
        {
            this._sessionsSpreadsheet?.Dispose();

            this._sessionsSpreadsheet = null;
            this._sessionsSheet = null;
            this._googleService = null;
        }
        #endregion
    }
}
