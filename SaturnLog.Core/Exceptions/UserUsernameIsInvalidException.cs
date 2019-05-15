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
    public class UserUsernameIsInvalidException : Exception
    {
        public const string DefaultMessageBegining = "Provided username is not recognized: ";

        public string Username { get; set; }
        
        public UserUsernameIsInvalidException(string username) : base(DefaultMessageBegining + username)
        {
            this.Username = username;
        }

        public UserUsernameIsInvalidException(string username, string message) : base(message)
        {
            this.Username = username;
        }
        public UserUsernameIsInvalidException(string username, string message, Exception inner) : base(message, inner)
        {
            this.Username = username;
        }
        protected UserUsernameIsInvalidException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            this.Username = info.GetValue("Username", typeof(string)) as string;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("Username", this.Username);
            base.GetObjectData(info, context);
        }
    }
}
