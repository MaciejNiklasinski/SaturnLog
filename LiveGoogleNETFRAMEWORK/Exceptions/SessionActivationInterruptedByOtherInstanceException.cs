using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class SessionActivationInterruptedByOtherInstanceException : Exception
    {       
        public SessionActivationInterruptedByOtherInstanceException() : base(GetDefaultMessage())
        {
        }

        public SessionActivationInterruptedByOtherInstanceException(string message) : base(message)
        {
        }
        public SessionActivationInterruptedByOtherInstanceException(string message, Exception inner) : base(message, inner)
        {
        }
        protected SessionActivationInterruptedByOtherInstanceException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"New session activation has been interrupted by other application instance.";
        }
    }
}
