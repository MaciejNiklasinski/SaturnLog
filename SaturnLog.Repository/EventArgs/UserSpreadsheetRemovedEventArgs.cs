using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveGoogle.Sheets;

namespace SaturnLog.Repository.EventArgs
{
    public class UserSpreadsheetRemovedEventArgs : System.EventArgs
    {
        public string Username { get; set; }

        public UserSpreadsheetRemovedEventArgs(string username)
        {
            this.Username = username;
        }
    }
}
