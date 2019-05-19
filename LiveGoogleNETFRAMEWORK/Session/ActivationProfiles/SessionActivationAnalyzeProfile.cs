using LiveGoogle.Sheets;
using LiveGoogle.Exceptions;
using LiveGoogle.Session;
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
        private class SessionActivationAnalyzeProfile
        {
            public bool SessionTakeoverRequired { get; set; } 
            public bool QuickStartAllowed { get; set; } 

            public string OriginalForcedToDisconnectStamp { get; set; }
            public string OriginalDisconnectedStamp { get; set; }
            public string OriginalLastStamp { get; set; }
            public string OriginalConnectedStamp { get; set; }

            public string ExpectedConnectedIn15Stamp { get; set; }
            public string ExpectedConnectedIn30Stamp { get; set; }
            public string ExpectedConnectedIn45Stamp { get; set; }
            public string ExpectedConnectedIn60Stamp { get; set; }
            public string ExpectedConnectedIn90Stamp { get; set; }
            public string ExpectedConnectedIn120Stamp { get; set; }

            public bool ConnectedAnalyzeRequired { get; set; }
            public bool ConnectedIn15AnalyzeRequired { get; set; }
            public bool ConnectedIn30AnalyzeRequired { get; set; }
            public bool ConnectedIn45AnalyzeRequired { get; set; }
            public bool ConnectedIn60AnalyzeRequired { get; set; }
            public bool ConnectedIn90AnalyzeRequired { get; set; }
            public bool ConnectedIn120AnalyzeRequired { get; set; }

            public bool ConnectedAnalyzeCompleted { get; set; }
            public bool ConnectedIn15AnalyzeCompleted { get; set; }
            public bool ConnectedIn30AnalyzeCompleted { get; set; }
            public bool ConnectedIn45AnalyzeCompleted { get; set; }
            public bool ConnectedIn60AnalyzeCompleted { get; set; }
            public bool ConnectedIn90AnalyzeCompleted { get; set; }
            public bool ConnectedIn120AnalyzeCompleted { get; set; }

            public SessionActivationAnalyzeProfile(SessionPreActivationAnalyzeProfile preActivationAnalyze)
            {
                if (preActivationAnalyze.IsLastDisconnectedRecently || preActivationAnalyze.LastSessionIsLongTimeUnresponsive)
                    QuickStartAllowed = true;

                this.OriginalForcedToDisconnectStamp = preActivationAnalyze.OriginalForcedToDisconnectStamp;
                this.OriginalDisconnectedStamp = preActivationAnalyze.OriginalDisconnectedStamp;
                this.OriginalLastStamp = preActivationAnalyze.OriginalLastStamp;
                this.OriginalConnectedStamp = preActivationAnalyze.OriginalConnectedStamp;
            }
        }
    }
}
