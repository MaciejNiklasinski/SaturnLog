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
    public class AttemptToAllocateFaultySaturn5Exception : Exception
    {
        public const string DefaultMessage = "Unable to allocate faulty saturn 5 unit.";

        // User to whom faulty Saturn5 has been attempted to be allocated
        public User User { get; set; }

        // Faulty Saturn5 unit
        public Saturn5 Saturn5 { get; set; }

        public AttemptToAllocateFaultySaturn5Exception(User user, Saturn5 saturn5) : base(DefaultMessage)
        {
            this.User = user;
            this.Saturn5 = saturn5;
        }

        public AttemptToAllocateFaultySaturn5Exception(User user, Saturn5 saturn5, string message) : base(message)
        {
            this.User = user;
            this.Saturn5 = saturn5;
        }
        public AttemptToAllocateFaultySaturn5Exception(User user, Saturn5 saturn5, string message, Exception inner) : base(message, inner)
        {
            this.User = user;
            this.Saturn5 = saturn5;
        }
        protected AttemptToAllocateFaultySaturn5Exception(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            this.User = info.GetValue("User", typeof(User)) as User;
            this.Saturn5 = info.GetValue("Saturn5", typeof(Saturn5)) as Saturn5;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("User", this.User);
            info.AddValue("Saturn5", this.Saturn5);
            base.GetObjectData(info, context);
        }
    }
}
