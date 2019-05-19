using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class EditSaturn5EventArgs : System.EventArgs
    {
        public Saturn5 ToBeEdited { get; private set; }
        public string NewShortId { get; private set; }
        public string NewPhoneNumber { get; private set; }

        public EditSaturn5EventArgs(Saturn5 toBeEdited, string newShortId, string newPhoneNumber)
        {
            this.ToBeEdited = toBeEdited;
            this.NewShortId = newShortId;
            this.NewPhoneNumber = newPhoneNumber;
        }
    }
}
