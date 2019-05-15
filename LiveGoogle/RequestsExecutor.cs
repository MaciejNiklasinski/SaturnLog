using Google.Apis.Requests;
using LiveGoogle.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveGoogle
{
    internal class RequestsExecutor
    {
        #region Private Fields
        private static readonly int _internalErrorCooldown = 250;
        private static readonly int _internalErrorAttemptsLimit = 5;

        private static readonly int _cancellationErrorCooldown = 250;
        private static readonly int _cancellationErrorAttemptsLimit = 5;
        #endregion

        #region Properties
        // Singleton instance.
        public static RequestsExecutor Instance { get; } = new RequestsExecutor();
        #endregion

        #region Constructor
        private RequestsExecutor() { }
        #endregion

        #region Methods
        public static void SafeExecuteSync(BatchRequest batchRequest)
        {
            RequestsExecutor.SafeExecuteSync(batchRequest, 0);
        }

        public static async Task SafeExecuteAsync(BatchRequest batchRequest)
        {
            await RequestsExecutor.SafeExecuteAsync(batchRequest, 0);
        }

        public static TResponse SafeExecuteSync<TResponse>(IClientServiceRequest<TResponse> request)
        {
            return RequestsExecutor.SafeExecuteSync<TResponse>(request, 0);
        }

        public static async Task<TResponse> SafeExecuteAsync<TResponse>(IClientServiceRequest<TResponse> request)
        {
            return await RequestsExecutor.SafeExecuteAsync<TResponse>(request, 0);
        }
        #endregion

        #region Private Helpers
        private static void SafeExecuteSync(BatchRequest batchRequest, int attemptOrderNumber)
        {
            // Attempt to execute provided request.
            try { Task.Run(async () => { await batchRequest.ExecuteAsync(); }).Wait(); }
            
            // On request operation canceled
            catch (OperationCanceledException ex)
            {
                // Increase attempt order number by one.
                attemptOrderNumber++;

                // Validate whether request reattempt limit hasn't been exceeded
                if (RequestsExecutor._cancellationErrorAttemptsLimit < attemptOrderNumber)
                    // ... and throw appropriate exception if has. attempt
                    throw new BatchRequestExecutionUnexpectedException(batchRequest, ex);

                // Await appropriate cooldown period.
                Thread.Sleep(RequestsExecutor._cancellationErrorCooldown);

                // Reattempt.
                RequestsExecutor.SafeExecuteSync(batchRequest, attemptOrderNumber);
            }

            // On google internal error encounter, retry 
            catch (Google.GoogleApiException ex) when (ex.Message.Contains("Internal error encountered. [500]"))
            {
                // Increase attempt order number by one.
                attemptOrderNumber++;

                // Validate whether request reattempt limit hasn't been exceeded
                if (RequestsExecutor._internalErrorAttemptsLimit < attemptOrderNumber)
                    // ... and throw appropriate exception if has. attempt
                    throw new BatchRequestExecutionUnexpectedException(batchRequest, ex);

                // Await appropriate cooldown period.
                Thread.Sleep(RequestsExecutor._internalErrorCooldown);

                // Reattempt.
                RequestsExecutor.SafeExecuteSync(batchRequest, attemptOrderNumber);
            }

            // On used all available requests quota
            catch (Google.GoogleApiException ex) when (ex.Message.Contains("Quota exceeded for quota group"))
            {
                // Set session request limiter state to indicate no available google requests quota at this time.
                SessionRequestsLimiter.Instance.ClearAvailableQuota();

                // Reattempt.
                RequestsExecutor.SafeExecuteSync(batchRequest, attemptOrderNumber);
            }

            // On any other unexpected database error
            catch (Google.GoogleApiException ex) { throw new BatchRequestExecutionUnexpectedException(batchRequest, ex); }
        }

        private static async Task SafeExecuteAsync(BatchRequest batchRequest, int attemptOrderNumber)
        {
            // Attempt to execute provided request.
            try { await batchRequest.ExecuteAsync(); }

            // On request operation canceled
            catch (OperationCanceledException ex)
            {
                // Increase attempt order number by one.
                attemptOrderNumber++;

                // Validate whether request reattempt limit hasn't been exceeded
                if (RequestsExecutor._cancellationErrorAttemptsLimit < attemptOrderNumber)
                    // ... and throw appropriate exception if has. attempt
                    throw new BatchRequestExecutionUnexpectedException(batchRequest, ex);

                // Await appropriate cooldown period.
                await Task.Delay(RequestsExecutor._cancellationErrorCooldown);

                // Reattempt.
                await RequestsExecutor.SafeExecuteAsync(batchRequest, attemptOrderNumber);
            }

            // On google internal error encounter, retry 
            catch (Google.GoogleApiException ex) when (ex.Message.Contains("Internal error encountered. [500]"))
            {
                // Increase attempt order number by one.
                attemptOrderNumber++;

                // Validate whether request reattempt limit hasn't been exceeded
                if (RequestsExecutor._internalErrorAttemptsLimit < attemptOrderNumber)
                    // ... and throw appropriate exception if has. attempt
                    throw new BatchRequestExecutionUnexpectedException(batchRequest, ex);

                // Await appropriate cooldown period.
                await Task.Delay(RequestsExecutor._internalErrorCooldown);

                // Reattempt.
                await RequestsExecutor.SafeExecuteAsync(batchRequest, attemptOrderNumber);
            }

            // On used all available requests quota
            catch (Google.GoogleApiException ex) when (ex.Message.Contains("Quota exceeded for quota group"))
            {
                // Set session request limiter state to indicate no available google requests quota at this time.
                SessionRequestsLimiter.Instance.ClearAvailableQuota();

                // Reattempt.
                await RequestsExecutor.SafeExecuteAsync(batchRequest, attemptOrderNumber);
            }

            // On any other unexpected database error
            catch (Google.GoogleApiException ex) { throw new BatchRequestExecutionUnexpectedException(batchRequest, ex); }
        }

        private static TResponse SafeExecuteSync<TResponse>(IClientServiceRequest<TResponse> request, int attemptOrderNumber)
        {
            // Attempt to execute provided request.
            try { return request.Execute(); }

            // On request operation canceled
            catch (OperationCanceledException ex)
            {
                // Increase attempt order number by one.
                attemptOrderNumber++;

                // Validate whether request reattempt limit hasn't been exceeded
                if (RequestsExecutor._cancellationErrorAttemptsLimit < attemptOrderNumber)
                    // ... and throw appropriate exception if has. attempt
                    throw new RequestExecutionUnexpectedException(request, ex);

                // Await appropriate cooldown period.
                Thread.Sleep(RequestsExecutor._cancellationErrorCooldown);

                // Reattempt.
                return RequestsExecutor.SafeExecuteSync(request, attemptOrderNumber);
            }

            // On google internal error encounter, retry 
            catch (Google.GoogleApiException ex) when (ex.Message.Contains("Internal error encountered. [500]"))
            {
                // Increase attempt order number by one.
                attemptOrderNumber++;

                // Validate whether request reattempt limit hasn't been exceeded
                if (RequestsExecutor._internalErrorAttemptsLimit < attemptOrderNumber)
                    // ... and throw appropriate exception if has. attempt
                    throw new RequestExecutionUnexpectedException(request, ex);

                // Await appropriate cooldown period.
                Thread.Sleep(RequestsExecutor._internalErrorCooldown);

                // Reattempt.
                return RequestsExecutor.SafeExecuteSync<TResponse>(request, attemptOrderNumber);
            }

            // On used all available requests quota
            catch (Google.GoogleApiException ex) when (ex.Message.Contains("Quota exceeded for quota group"))
            {
                // Set session request limiter state to indicate no available google requests quota at this time.
                SessionRequestsLimiter.Instance.ClearAvailableQuota();

                // Reattempt.
                return RequestsExecutor.SafeExecuteSync<TResponse>(request, attemptOrderNumber);
            }

            // On any other unexpected database error
            catch (Google.GoogleApiException ex) { throw new RequestExecutionUnexpectedException(request, ex); }
        }

        private static async Task<TResponse> SafeExecuteAsync<TResponse>(IClientServiceRequest<TResponse> request, int attemptOrderNumber)
        {
            // Attempt to execute provided request.
            try { return await request.ExecuteAsync(); }

            // On request operation canceled
            catch (OperationCanceledException ex)
            {
                // Increase attempt order number by one.
                attemptOrderNumber++;

                // Validate whether request reattempt limit hasn't been exceeded
                if (RequestsExecutor._cancellationErrorAttemptsLimit < attemptOrderNumber)
                    // ... and throw appropriate exception if has. attempt
                    throw new RequestExecutionUnexpectedException(request, ex);

                // Await appropriate cooldown period.
                await Task.Delay(RequestsExecutor._cancellationErrorCooldown);

                // Reattempt.
                return await RequestsExecutor.SafeExecuteAsync(request, attemptOrderNumber);
            }

            // On google internal error encounter, retry 
            catch (Google.GoogleApiException ex) when (ex.Message.Contains("Internal error encountered. [500]"))
            {
                // Increase attempt order number by one.
                attemptOrderNumber++;

                // Validate whether request reattempt limit hasn't been exceeded
                if (RequestsExecutor._internalErrorAttemptsLimit < attemptOrderNumber)
                    // ... and throw appropriate exception if has. attempt
                    throw new RequestExecutionUnexpectedException(request, ex);

                // Await appropriate cooldown period.
                await Task.Delay(RequestsExecutor._internalErrorCooldown);

                // Reattempt.
                return await RequestsExecutor.SafeExecuteAsync<TResponse>(request, attemptOrderNumber);
            }

            // On used all available requests quota
            catch (Google.GoogleApiException ex) when (ex.Message.Contains("Quota exceeded for quota group"))
            {
                // Set session request limiter state to indicate no available google requests quota at this time.
                SessionRequestsLimiter.Instance.ClearAvailableQuota();

                // Reattempt.
                return await RequestsExecutor.SafeExecuteAsync<TResponse>(request, attemptOrderNumber);
            }

            // On any other unexpected database error
            catch (Google.GoogleApiException ex) { throw new RequestExecutionUnexpectedException(request, ex); }
        }
        #endregion

        private static bool IsInternetConnectionAvailable()
        {
            try
            {
                using (WebClient client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
