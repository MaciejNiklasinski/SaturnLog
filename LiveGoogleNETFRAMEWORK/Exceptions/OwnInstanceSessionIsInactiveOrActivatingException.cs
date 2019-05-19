using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class OwnInstanceSessionIsInactiveOrActivatingException : Exception
    {       
        public OwnInstanceSessionIsInactiveOrActivatingException() : base(GetDefaultMessage())
        {
        }

        public OwnInstanceSessionIsInactiveOrActivatingException(string message) : base(message)
        {
        }
        public OwnInstanceSessionIsInactiveOrActivatingException(string message, Exception inner) : base(message, inner)
        {
        }
        protected OwnInstanceSessionIsInactiveOrActivatingException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return "This application instance session is inactive, or in a process of activating/deactivating.";
        }
    }
}
