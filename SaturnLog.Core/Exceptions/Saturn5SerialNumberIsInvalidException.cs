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
    public class Saturn5SerialNumberIsInvalidException : Exception
    {
        public const string DefaultMessageBegining = "Provided serial number is not recognized: ";

        public string SerialNumber { get; set; }
        
        public Saturn5SerialNumberIsInvalidException(string serialNumber) : base(DefaultMessageBegining + serialNumber)
        {
            this.SerialNumber = serialNumber;
        }

        public Saturn5SerialNumberIsInvalidException(string serialNumber, string message) : base(message)
        {
            this.SerialNumber = serialNumber;
        }
        public Saturn5SerialNumberIsInvalidException(string serialNumber, string message, Exception inner) : base(message, inner)
        {
            this.SerialNumber = serialNumber;
        }
        protected Saturn5SerialNumberIsInvalidException(System.Runtime.Serialization.SerializationInfo info,
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
