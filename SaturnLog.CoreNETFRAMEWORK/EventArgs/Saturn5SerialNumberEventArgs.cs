﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class Saturn5SerialNumberEventArgs : System.EventArgs
    {
        public string SerialNumber { get; private set; }

        public Saturn5SerialNumberEventArgs(string serialNumber)
        {
            this.SerialNumber = serialNumber;
        }
    }
}
