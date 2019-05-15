using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class FaultySaturn5EventArgs : System.EventArgs
    {
        public Saturn5 Saturn5 { get; private set; }
        public Saturn5Issue Saturn5Fault { get; private set; }

        public FaultySaturn5EventArgs(Saturn5 saturn5, Saturn5Issue saturn5Fault)
        {
            this.Saturn5 = saturn5;
            this.Saturn5Fault = saturn5Fault;
        }
    }
}
