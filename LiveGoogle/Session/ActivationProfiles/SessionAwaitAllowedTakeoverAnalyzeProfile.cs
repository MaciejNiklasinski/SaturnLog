using LiveGoogle.Sheets;
using LiveGoogle.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveGoogle.Session
{
    internal partial class SessionsRepository
    {
        private class SessionAwaitAllowedTakeoverAnalyzeProfile
        {
            public string OriginalForcedToDisconnectStamp { get; set; }
            public string OriginalDisconnectedStamp { get; set; }

            public string OriginalConnectedStamp { get; set; }
            public string OriginalLastStamp { get; set; }

            public string OriginalConnectedIn15Stamp { get; set; }
            public string OriginalConnectedIn30Stamp { get; set; }
            public string OriginalConnectedIn45Stamp { get; set; }
            public string OriginalConnectedIn60Stamp { get; set; }
            public string OriginalConnectedIn90Stamp { get; set; }
            public string OriginalConnectedIn120Stamp { get; set; }

            public SessionAwaitAllowedTakeoverAnalyzeProfile(SessionPreActivationAnalyzeProfile preActivationAnalyze)
            {
                this.OriginalForcedToDisconnectStamp = preActivationAnalyze.OriginalForcedToDisconnectStamp;
                this.OriginalDisconnectedStamp = preActivationAnalyze.OriginalDisconnectedStamp;
                this.OriginalLastStamp = preActivationAnalyze.OriginalLastStamp;
                this.OriginalConnectedStamp = preActivationAnalyze.OriginalConnectedStamp;

                this.OriginalConnectedIn15Stamp = preActivationAnalyze.OriginalConnectedIn15Stamp;
                this.OriginalConnectedIn30Stamp = preActivationAnalyze.OriginalConnectedIn30Stamp;
                this.OriginalConnectedIn45Stamp = preActivationAnalyze.OriginalConnectedIn45Stamp;
                this.OriginalConnectedIn60Stamp = preActivationAnalyze.OriginalConnectedIn60Stamp;
                this.OriginalConnectedIn90Stamp = preActivationAnalyze.OriginalConnectedIn90Stamp;
                this.OriginalConnectedIn120Stamp = preActivationAnalyze.OriginalConnectedIn120Stamp;
            }
        }
    }
}
