using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserUsernameEventArgs : System.EventArgs
    {
        public string Username { get; private set; }

        public UserUsernameEventArgs(string username)
        {
            this.Username = username;
        }
    }
}
