using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using Google.Apis.Sheets.v4;
using LiveGoogle.Session;

namespace LiveGoogle
{
    internal class SessionRequestsLimiter
    {
        #region Const
        public const int QuotaRequestsLimit = 200;
        public const int QuotaTimeLimit = 100;
        public const int QuotaRequestsPerSec = SessionRequestsLimiter.QuotaRequestsLimit / SessionRequestsLimiter.QuotaTimeLimit;
        public const int DelayOn1Percent = 5000;
        public const int DelayOn10Percent = 1000;
        public const int DelayOn25Percent = 500;
        public const int DelayOn50Percent = 100;
        #endregion

        #region Static Fields and Properties
        private static readonly SemaphoreSlim _limiterSemaphore = new SemaphoreSlim(1, 1);

        // Singleton instance.
        public static SessionRequestsLimiter Instance { get; } = new SessionRequestsLimiter();
        #endregion
        
        #region Private fields
        private bool _initializationMode = false;
        private bool _stopped = false;
        #endregion 

        #region Public Properties
        public int AvailableQuota { get; private set; } = 0;
        public int AvailableQuotaPercentage { get { return (int)((double)AvailableQuota / (double)QuotaRequestsLimit * 100d); } }
        #endregion

        #region Singleton Private Constructor
        // Default singleton constructor
        private SessionRequestsLimiter() { }
        #endregion

        // Begin counting available requests.
        internal async Task StartQuotaCount(SheetsService sheetsService, string sessionsSpreadsheetId, CancellationToken cancellationToken)
        {
            // Await limiter repository
            await SessionRequestsLimiter._limiterSemaphore.WaitAsync();

            try
            {
                // Throw if cancellation has been requested after semaphore has been released
                cancellationToken.ThrowIfCancellationRequested();

                // Engage initialization mode to await deadlock when obtaining initial available quota
                this._initializationMode = true;
                this._stopped = false;

                await Task.Run(() => this.AvailableQuota = SessionsRepository.RetrieveInitialAvailableQuota(sheetsService, sessionsSpreadsheetId));
            }
            // Finally release limiter repository
            finally
            {
                this._initializationMode = false;
                SessionRequestsLimiter._limiterSemaphore.Release();
            }

            // Do not await
#pragma warning disable CS4014 // Do not await task to return on operational and still maintain data consistency.
            Task.Run(async () => 
            {
                while (!this._stopped)
                {
                    await Task.Delay(1000, cancellationToken);

                    if ((this.AvailableQuota + SessionRequestsLimiter.QuotaRequestsPerSec) > SessionRequestsLimiter.QuotaRequestsLimit)
                        this.AvailableQuota = SessionRequestsLimiter.QuotaRequestsLimit;
                    else
                        this.AvailableQuota += SessionRequestsLimiter.QuotaRequestsPerSec;
                }
            }, cancellationToken);
#pragma warning restore CS4014
        }

        internal void StopQuotaCount()
        {
            this._stopped = true;
        }

        internal void ClearAvailableQuota()
        {
            this.AvailableQuota = 0;
        }

        internal void Wait()
        {
            // Return immediately, without waiting if in initialization mode
            if (this._initializationMode) return;


            // Await limiter repository
            SessionRequestsLimiter._limiterSemaphore.Wait();

            try
            {
                // Below 1% available quota
                if (this.AvailableQuotaPercentage < 1)
                    Thread.Sleep(SessionRequestsLimiter.DelayOn1Percent);
                // Below 10% available quota
                else if (this.AvailableQuotaPercentage < 10)
                    Thread.Sleep(SessionRequestsLimiter.DelayOn10Percent);
                // Below 25% available quota
                else if (this.AvailableQuota < 25)
                    Thread.Sleep(SessionRequestsLimiter.DelayOn25Percent);
                // Below 50% available quota
                else if (this.AvailableQuota < 50)
                    Thread.Sleep(SessionRequestsLimiter.DelayOn50Percent);
                // 50% available quota or above
                //else
                    //Thread.Sleep(1);

                this.AvailableQuota--;
            }
            // Finally release limiter repository
            finally { SessionRequestsLimiter._limiterSemaphore.Release(); }
        }

        internal async Task WaitAsync()
        {
            // Return immediately, without waiting if in initialization mode
            if (this._initializationMode) return;
            
            // Await limiter repository
            await SessionRequestsLimiter._limiterSemaphore.WaitAsync();
            
            try
            {
                // Below 1% available quota
                if (this.AvailableQuotaPercentage < 1)
                    await Task.Delay(SessionRequestsLimiter.DelayOn1Percent);
                // Below 10% available quota
                else if (this.AvailableQuotaPercentage < 10)
                    await Task.Delay(SessionRequestsLimiter.DelayOn10Percent);
                // Below 25% available quota
                else if (this.AvailableQuota < 25)
                    await Task.Delay(SessionRequestsLimiter.DelayOn25Percent);
                // Below 50% available quota
                else if (this.AvailableQuota < 50)
                    await Task.Delay(SessionRequestsLimiter.DelayOn50Percent);
                // 50% available quota or above
                //else
                    //await Task.Delay(1);

                this.AvailableQuota--;
            }
            // Finally release limiter repository
            finally { SessionRequestsLimiter._limiterSemaphore.Release(); }

        }
    }
}
