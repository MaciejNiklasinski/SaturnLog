using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class CreateSaturn5EventArgs : System.EventArgs
    {
        public string SerialNumber { get; private set; }
        public string ShortId { get; private set; }
        public string PhoneNumber { get; private set; }

        public CreateSaturn5EventArgs(string serialNumber, string shortId, string phoneNumber)
        {
            this.SerialNumber = serialNumber;
            this.ShortId = shortId;
            this.PhoneNumber = phoneNumber;
        }
    }
}
