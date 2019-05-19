using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserEventArgs : System.EventArgs
    {
        public User User { get; private set; }

        public UserEventArgs(User user)
        {
            this.User = user;
        }
    }
}
