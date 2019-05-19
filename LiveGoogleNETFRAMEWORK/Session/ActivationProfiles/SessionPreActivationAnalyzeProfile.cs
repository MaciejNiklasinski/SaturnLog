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
        private class SessionPreActivationAnalyzeProfile
        {
            public string OriginalForcedToDisconnectStamp { get; set; }
            public string OriginalDisconnectedStamp { get; set; }

            public string OriginalLastStamp { get; set; }
            public string OriginalConnectedStamp { get; set; }

            public string OriginalConnectedIn15Stamp { get; set; }
            public string OriginalConnectedIn30Stamp { get; set; }
            public string OriginalConnectedIn45Stamp { get; set; }
            public string OriginalConnectedIn60Stamp { get; set; }
            public string OriginalConnectedIn90Stamp { get; set; }
            public string OriginalConnectedIn120Stamp { get; set; }

            public bool IsOtherInstanceSessionActive { get; set; }

            public bool IsOtherInstanceSessionAttemtingToActivate { get; set; }

            public bool IsLastDisconnected { get; set; }

            public bool IsLastDisconnectedRecently { get; set; }

            public bool IsLastForcedToDisconnect { get; set; }

            public bool LastSessionIsUnresponsive { get; set; }

            public bool LastSessionIsLongTimeUnresponsive { get; set; }

            public bool HasBeenForcedToDisconnectRecently { get; set; }
        }
    }
}
