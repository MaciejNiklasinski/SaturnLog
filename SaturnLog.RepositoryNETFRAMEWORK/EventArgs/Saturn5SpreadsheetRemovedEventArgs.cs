using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveGoogle.Sheets;

namespace SaturnLog.Repository.EventArgs
{
    public class Saturn5SpreadsheetRemovedEventArgs : System.EventArgs
    {
        public string SerialNumber { get; set; }

        public Saturn5SpreadsheetRemovedEventArgs(string serialNumber)
        {
            this.SerialNumber = serialNumber;
        }
    }
}
