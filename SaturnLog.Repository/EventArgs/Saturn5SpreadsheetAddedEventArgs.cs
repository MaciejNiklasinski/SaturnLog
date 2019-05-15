using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveGoogle.Sheets;

namespace SaturnLog.Repository.EventArgs
{
    public class Saturn5SpreadsheetAddedEventArgs : System.EventArgs
    {
        public LiveSpreadsheet Spreadsheet { get; set; }

        public Saturn5SpreadsheetAddedEventArgs(LiveSpreadsheet spreadsheet)
        {
            this.Spreadsheet = spreadsheet;
        }
    }
}
