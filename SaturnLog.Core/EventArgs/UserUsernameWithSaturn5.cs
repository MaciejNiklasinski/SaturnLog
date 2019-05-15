using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserUsernameWithSaturn5 : System.EventArgs
    {
        public string Username { get; private set; }
        public Saturn5 Saturn5 { get; private set; }

        public UserUsernameWithSaturn5(string username, Saturn5 saturn5)
        {
            this.Username = username;
            this.Saturn5 = saturn5;
        }
    }
}
