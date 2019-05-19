using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserWithSaturn5ShortIdEventArgs : System.EventArgs
    {
        public User User { get; private set; }
        public string ShortId { get; private set; }

        public UserWithSaturn5ShortIdEventArgs(User user, string shortId)
        {
            this.User = user;
            this.ShortId = shortId;
        }
    }
}
