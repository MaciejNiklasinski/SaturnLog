using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserWithSaturn5EventArgs : System.EventArgs
        {
            public User User { get; private set; }
            public Saturn5 Saturn5 { get; private set; }

            public UserWithSaturn5EventArgs(User user, Saturn5 saturn5)
            {
                this.User = user;
                this.Saturn5 = saturn5;
            }
        }
    }
