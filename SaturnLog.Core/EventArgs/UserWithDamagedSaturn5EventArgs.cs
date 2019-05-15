using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserWithDamagedSaturn5EventArgs : System.EventArgs
        {
            public User User { get; private set; }
            public Saturn5 Saturn5 { get; private set; }
            public Saturn5Issue Saturn5Damage { get; private set; }

            public UserWithDamagedSaturn5EventArgs(User user, Saturn5 saturn5, Saturn5Issue saturn5Damage)
            {
                this.User = user;
                this.Saturn5 = saturn5;
                this.Saturn5Damage = saturn5Damage;
            }
        }
    }
