using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserWithFaultySaturn5EventArgs : System.EventArgs
    {
        public User User { get; private set; }
        public Saturn5 Saturn5 { get; private set; }
        public Saturn5Issue Saturn5Fault { get; private set; }

        public UserWithFaultySaturn5EventArgs(User user, Saturn5 saturn5, Saturn5Issue saturn5Fault)
        {
            this.User = user;
            this.Saturn5 = saturn5;
            this.Saturn5Fault = saturn5Fault;
        }
    }
}
