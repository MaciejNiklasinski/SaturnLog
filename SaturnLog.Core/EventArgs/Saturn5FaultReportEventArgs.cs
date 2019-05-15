using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class Saturn5FaultReportEventArgs : System.EventArgs
    {
        public Saturn5 Saturn5 { get; private set; }
        public string Username { get; private set; }
        public string Description { get; private set; }

        public Saturn5FaultReportEventArgs(Saturn5 saturn5, string username, string description)
        {
            this.Saturn5 = saturn5;
            this.Username = username;
            this.Description = description;
        }
    }
}
