using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class Saturn5SendToITEventArgs : System.EventArgs
    {
        public string SerialNumber { get; private set; }
        public string ConsignmentNumber { get; private set; }
        public string IncidentNumber { get; private set; }
        public string MovementNote { get; private set; }
        public bool Surplus { get; private set; }

        public Saturn5SendToITEventArgs(string serialNumber, string consignmentNumber, string incidentNumber, string movementNote, bool surplus)
        {
            this.SerialNumber = serialNumber;
            this.ConsignmentNumber = consignmentNumber;
            this.IncidentNumber = incidentNumber;
            this.MovementNote = movementNote;
            this.Surplus = surplus;
        }
    }
}
