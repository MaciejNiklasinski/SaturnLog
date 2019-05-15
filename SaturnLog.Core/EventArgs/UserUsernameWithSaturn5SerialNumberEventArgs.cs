﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class UserUsernameWithSaturn5SerialNumberEventArgs : System.EventArgs
    {
        public string Username { get; private set; }
        public string SerialNumber { get; private set; }

        public UserUsernameWithSaturn5SerialNumberEventArgs(string username, string serialNumber)
        {
            this.Username = username;
            this.SerialNumber = serialNumber;
        }
    }
}
