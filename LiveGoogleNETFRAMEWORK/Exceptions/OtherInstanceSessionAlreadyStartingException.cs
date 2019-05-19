using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class OtherInstanceSessionAlreadyStartingException : Exception
    {       
        public OtherInstanceSessionAlreadyStartingException() : base(GetDefaultMessage())
        {
        }

        public OtherInstanceSessionAlreadyStartingException(string message) : base(message)
        {
        }
        public OtherInstanceSessionAlreadyStartingException(string message, Exception inner) : base(message, inner)
        {
        }
        protected OtherInstanceSessionAlreadyStartingException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return "Other instance of the application is currently attempting to start new session.";
        }
    }
}
