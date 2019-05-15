using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class SessionActivationInterruptedByOwnInstanceException : Exception
    {       
        public SessionActivationInterruptedByOwnInstanceException() : base(GetDefaultMessage())
        {
        }

        public SessionActivationInterruptedByOwnInstanceException(string message) : base(message)
        {
        }
        public SessionActivationInterruptedByOwnInstanceException(string message, Exception inner) : base(message, inner)
        {
        }
        protected SessionActivationInterruptedByOwnInstanceException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"New session activation has been interrupted by own application instance.";
        }
    }
}
