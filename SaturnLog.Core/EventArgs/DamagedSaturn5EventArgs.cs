using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class DamagedSaturn5EventArgs : System.EventArgs
    {
        public Saturn5 Saturn5 { get; private set; }
        public Saturn5Issue Saturn5Damage { get; private set; }

        public DamagedSaturn5EventArgs(Saturn5 saturn5, Saturn5Issue saturn5Damage)
        {
            this.Saturn5 = saturn5;
            this.Saturn5Damage = saturn5Damage;
        }
    }
}
