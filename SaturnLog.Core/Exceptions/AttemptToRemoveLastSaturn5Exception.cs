using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace SaturnLog.Core.Exceptions
{
    [Serializable]
    public class AttemptToRemoveLastSaturn5Exception : Exception
    {
        public const string DefaultMessage = "Unable to remove last Saturn 5 in stock.";
        
        // Serial number
        public string SerialNumber { get; set; }

        public AttemptToRemoveLastSaturn5Exception(string serialNumber) : base(DefaultMessage)
        {
            this.SerialNumber = serialNumber;
        }

        public AttemptToRemoveLastSaturn5Exception(string serialNumber, string message) : base(message)
        {
            this.SerialNumber = serialNumber;
        }
        public AttemptToRemoveLastSaturn5Exception(string serialNumber, string message, Exception inner) : base(message, inner)
        {
            this.SerialNumber = serialNumber;
        }
        protected AttemptToRemoveLastSaturn5Exception(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            this.SerialNumber = info.GetValue("SerialNumber", typeof(string)) as string;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("SerialNumber", this.SerialNumber);
            base.GetObjectData(info, context);
        }
    }
}
