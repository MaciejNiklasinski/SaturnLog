using LiveGoogle.Sheets;
using SaturnLog.Core;
using SaturnLog.Repository.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaturnLog.Repository.Interfaces
{
    internal interface ISessionsRepository : IDisposable
    {
        int? SecondsToNewSession { get; }
        bool? SessionActive { get; }


        Task StartNewSessionAsync(CancellationTokenSource sessionCancellationSource, bool forceOtherInstanceSessionTakever);
        Task MaintainActiveSessionAsync();
        Task EndSessionAsync();
    }
}
