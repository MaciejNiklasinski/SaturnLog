using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Session
{
    internal class SessionStamp
    {
        public DateTime ConstructedDateTime { get; }
        
        public int LastAvailableRequests { get; set; }
        public string LastForcedToDisconnect { get; set; }
        public string Disconnected { get; set; }
        public string Last { get; set; }
        public string Connected { get; set; }
        public string ConnectedIn15 { get; set; }
        public string ConnectedIn30 { get; set; }
        public string ConnectedIn45 { get; set; }
        public string ConnectedIn60 { get; set; }
        public string ConnectedIn90 { get; set; }
        public string ConnectedIn120 { get; set; }

        public SessionStamp(int lastAvailableRequests, string lastForcedToDisconnect, string disconnected, string last, string connected, string connectedIn15, string connectedIn30, string connectedIn45, string connectedIn60, string connectedIn90, string connectedIn120)
        {
            this.ConstructedDateTime = DateTimeExtensions.GetNISTNow();

            this.LastAvailableRequests = lastAvailableRequests;
            this.LastForcedToDisconnect = lastForcedToDisconnect;
            this.Disconnected = disconnected;
            this.Last = last;
            this.Connected = connected;
            this.ConnectedIn15 = connectedIn15;
            this.ConnectedIn30 = connectedIn30;
            this.ConnectedIn45 = connectedIn45;
            this.ConnectedIn60 = connectedIn60;
            this.ConnectedIn90 = connectedIn90;
            this.ConnectedIn120 = connectedIn120;
        }
    }
}
