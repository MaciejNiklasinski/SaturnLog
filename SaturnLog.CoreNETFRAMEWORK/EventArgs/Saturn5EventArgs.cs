using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class Saturn5EventArgs : System.EventArgs
    {
        public Saturn5 Saturn5 { get; private set; }

        public Saturn5EventArgs(Saturn5 saturn5)
        {
            this.Saturn5 = saturn5;
        }
    }
}
