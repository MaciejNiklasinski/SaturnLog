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
    public class Saturn5LastSeenUsernameIsInvalidException : UserUsernameIsInvalidException
    {
        public const string DefaultParamNameSaturn5MessageBegining = "Provided Saturn5 instance referring to not recognized username, saturn5.LastSeenUsername: ";

        public Saturn5 Saturn5 { get; set; }
        
        public Saturn5LastSeenUsernameIsInvalidException(Saturn5 saturn5) : base(saturn5.LastSeenUsername, DefaultParamNameSaturn5MessageBegining + $"{saturn5.LastSeenUsername}")
        {
            this.Saturn5 = saturn5;
        }

        public Saturn5LastSeenUsernameIsInvalidException(Saturn5 saturn5, string message) : base(saturn5.LastSeenUsername, message)
        {
            this.Saturn5 = saturn5;
        }
        public Saturn5LastSeenUsernameIsInvalidException(Saturn5 saturn5, string message, Exception inner) : base(saturn5.LastSeenUsername, message, inner)
        {
            this.Saturn5 = saturn5;
        }
        protected Saturn5LastSeenUsernameIsInvalidException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            this.Saturn5 = info.GetValue("Saturn5", typeof(Saturn5)) as Saturn5;
            this.Username = info.GetValue("Username", typeof(string)) as string;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("Saturn5", this.Saturn5);
            info.AddValue("Username", this.Username);
            base.GetObjectData(info, context);
        }
    }
}
