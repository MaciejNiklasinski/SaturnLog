using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class OwnInstanceSessionIsNotActiveException : Exception
    {       
        public OwnInstanceSessionIsNotActiveException() : base(GetDefaultMessage())
        {
        }

        public OwnInstanceSessionIsNotActiveException(string message) : base(message)
        {
        }
        public OwnInstanceSessionIsNotActiveException(string message, Exception inner) : base(message, inner)
        {
        }
        protected OwnInstanceSessionIsNotActiveException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return "This application instance session is not active.";
        }
    }
}
