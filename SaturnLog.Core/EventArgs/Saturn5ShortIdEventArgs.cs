using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class Saturn5ShortIdEventArgs : System.EventArgs
    {
        public string ShortId { get; private set; }

        public Saturn5ShortIdEventArgs(string shortId)
        {
            this.ShortId = shortId;
        }
    }
}
