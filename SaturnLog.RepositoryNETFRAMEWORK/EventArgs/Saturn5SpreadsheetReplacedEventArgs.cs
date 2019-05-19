using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveGoogle.Sheets;

namespace SaturnLog.Repository.EventArgs
{
    public class Saturn5SpreadsheetReplacedEventArgs : System.EventArgs
    {
        public LiveSpreadsheet Spreadsheet { get; set; }

        public Saturn5SpreadsheetReplacedEventArgs(LiveSpreadsheet spreadsheet)
        {
            this.Spreadsheet = spreadsheet;
        }
    }
}
