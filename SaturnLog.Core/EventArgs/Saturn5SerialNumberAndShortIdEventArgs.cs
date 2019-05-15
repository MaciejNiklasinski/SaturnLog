using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class Saturn5SerialNumberAndShortIdEventArgs : System.EventArgs
    {
        public string SerialNumber { get; private set; }
        public string ShortId { get; private set; }

        public Saturn5SerialNumberAndShortIdEventArgs(string serialNumber, string shortId)
        {
            this.SerialNumber = serialNumber;
            this.ShortId = shortId;
        }
    }
}
