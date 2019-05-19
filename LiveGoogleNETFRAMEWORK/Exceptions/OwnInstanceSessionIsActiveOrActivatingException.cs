using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class OwnInstanceSessionIsActiveOrActivatingException : Exception
    {       
        public OwnInstanceSessionIsActiveOrActivatingException() : base(GetDefaultMessage())
        {
        }

        public OwnInstanceSessionIsActiveOrActivatingException(string message) : base(message)
        {
        }
        public OwnInstanceSessionIsActiveOrActivatingException(string message, Exception inner) : base(message, inner)
        {
        }
        protected OwnInstanceSessionIsActiveOrActivatingException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return "This application instance session is already active, or in a process of activating.";
        }
    }
}
