using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserWithSaturn5SerialNumberEventArgs : System.EventArgs
    {
        public User User { get; private set; }
        public string SerialNumber { get; private set; }

        public UserWithSaturn5SerialNumberEventArgs(User user, string serialNumber)
        {
            this.User = user;
            this.SerialNumber = serialNumber;
        }
    }
}
