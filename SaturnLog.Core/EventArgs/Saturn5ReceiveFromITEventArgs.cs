using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class Saturn5ReceiveFromITEventArgs : System.EventArgs
    {
        public string SerialNumber { get; private set; }
        public string ShortId { get; private set; }
        public string PhoneNumber { get; private set; }
        public string ConsignmentNumber { get; private set; }
        public string MovementNote { get; private set; }

        public Saturn5ReceiveFromITEventArgs(string serialNumber, string shortId, string phoneNumber, string consignmentNumber, string movementNote)
        {
            this.SerialNumber = serialNumber;
            this.ShortId = shortId;
            this.PhoneNumber = phoneNumber;
            this.ConsignmentNumber = consignmentNumber;
            this.MovementNote = movementNote;
        }
    }
}
