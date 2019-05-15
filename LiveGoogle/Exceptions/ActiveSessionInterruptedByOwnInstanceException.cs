using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class ActiveSessionInterruptedByOwnInstanceException : Exception
    {       
        public ActiveSessionInterruptedByOwnInstanceException() : base(GetDefaultMessage())
        {
        }

        public ActiveSessionInterruptedByOwnInstanceException(string message) : base(message)
        {
        }
        public ActiveSessionInterruptedByOwnInstanceException(string message, Exception inner) : base(message, inner)
        {
        }
        protected ActiveSessionInterruptedByOwnInstanceException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Current active session has been interrupted by other session created by own instance.";
        }
    }
}
