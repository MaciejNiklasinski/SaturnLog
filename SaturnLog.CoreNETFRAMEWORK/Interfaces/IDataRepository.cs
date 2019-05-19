using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public interface IDataRepository : IDisposable
    {
        bool? Connected { get; }
        int? DBConnectionAvailabilityCountdown { get; }
        int? DBQuotaUsagePercentage { get; }

        bool? UsersDataCached { get; }
        bool? Saturns5DataCached { get; }

        bool? DBDashboardRebuild { get; }
        
        IUserRepository UserRepository { get; }
        ISaturn5Repository Saturn5Repository { get; }
        ISaturns5DashboardRepository Saturns5DashboardRepository { get; }
        ISaturn5IssuesRepository Saturn5IssuesRepository { get; }
        ISaturns5MovementRepository Saturns5MovementRepository { get; }
                
        CancellationTokenSource ConnectAsyncCancel { get; }
        CancellationTokenSource FetchDataAsyncCancel { get; }

        // CancellationTokenSource FetchUserDataAsyncCancel { get; }
        // CancellationTokenSource FetchSaturn5DataAsyncCancel { get; }
        // CancellationTokenSource DBRebuildDashboardAsyncCancel { get; }

        Task LockDBContentAsync();
        void ReleaseDBContent();
        
        Task<Task> ConnectAsync(CancellationTokenSource cancellationTokenSource, bool forceDBTakeover);
        Task FetchDataAsync(CancellationTokenSource cancellationTokenSource); 

        // Task FetchUserDataAsync(CancellationTokenSource cancellationTokenSource); 
        // Task FetchSaturn5DataAsync(CancellationTokenSource cancellationTokenSource); 
        // Task DBRebuildDashboardAsync(CancellationTokenSource cancellationTokenSource); 
    }
}
