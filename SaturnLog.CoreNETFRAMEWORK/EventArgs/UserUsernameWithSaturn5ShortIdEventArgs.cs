using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserUsernameWithSaturn5ShortIdEventArgs : System.EventArgs
    {
        public string Username { get; private set; }
        public string ShortId { get; private set; }

        public UserUsernameWithSaturn5ShortIdEventArgs(string username, string shortId)
        {
            this.Username = username;
            this.ShortId = shortId;
        }
    }
}
