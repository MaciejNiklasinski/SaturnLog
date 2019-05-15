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
    public class AttemptToRemoveLoggedInUserException : Exception
    {
        public const string DefaultMessage = "Unable to remove currently logged in user.";

        // User to whom faulty Saturn5 has been attempted to be allocated
        public User User { get; set; }

        public AttemptToRemoveLoggedInUserException(User user) : base(DefaultMessage)
        {
            this.User = user;
        }

        public AttemptToRemoveLoggedInUserException(User user, string message) : base(message)
        {
            this.User = user;
        }
        public AttemptToRemoveLoggedInUserException(User user, string message, Exception inner) : base(message, inner)
        {
            this.User = user;
        }
        protected AttemptToRemoveLoggedInUserException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            this.User = info.GetValue("User", typeof(User)) as User;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("User", this.User);
            base.GetObjectData(info, context);
        }
    }
}
