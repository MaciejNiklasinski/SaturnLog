using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class ActiveSessionInterruptedByOtherInstanceException : Exception
    {        
        public string InterruptedSessionId { get; }
        
        public string InterruptingSessionId { get; }

        public ActiveSessionInterruptedByOtherInstanceException(string interruptedSessionId, string interruptingSessionId) : base(GetDefaultMessage(interruptedSessionId, interruptingSessionId))
        {
            this.InterruptedSessionId = interruptedSessionId;
            this.InterruptingSessionId = interruptingSessionId;
        }

        public ActiveSessionInterruptedByOtherInstanceException(string interruptedSessionId, string interruptingSessionId, string message) : base(message)
        {
            this.InterruptedSessionId = interruptedSessionId;
            this.InterruptingSessionId = interruptingSessionId;
        }
        public ActiveSessionInterruptedByOtherInstanceException(string interruptedSessionId, string interruptingSessionId, string message, Exception inner) : base(message, inner)
        {
            this.InterruptedSessionId = interruptedSessionId;
            this.InterruptingSessionId = interruptingSessionId;
        }
        protected ActiveSessionInterruptedByOtherInstanceException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            this.InterruptedSessionId = info.GetValue("InterruptedSessionId", typeof(string)) as string;
            this.InterruptingSessionId = info.GetValue("InterruptingSessionId", typeof(string)) as string;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("InterruptedSessionId", this.InterruptedSessionId);
            info.AddValue("InterruptingSessionId", this.InterruptingSessionId);
            base.GetObjectData(info, context);
        }

        public static string GetDefaultMessage(string interruptedSessionId, string interruptingSessionId)
        {
            return $"Current active session {interruptedSessionId} active Session has been interrupted by other session. Interrupting session Id: {interruptingSessionId}";
        }
    }
}
