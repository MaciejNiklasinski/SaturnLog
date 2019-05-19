using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    internal class OtherInstanceActiveSessionHasBeenDisconnectedException : Exception
    {       
        public OtherInstanceActiveSessionHasBeenDisconnectedException() : base(GetDefaultMessage())
        {
        }

        public OtherInstanceActiveSessionHasBeenDisconnectedException(string message) : base(message)
        {
        }
        public OtherInstanceActiveSessionHasBeenDisconnectedException(string message, Exception inner) : base(message, inner)
        {
        }
        protected OtherInstanceActiveSessionHasBeenDisconnectedException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return "Other application instance active session has been disconnected.";
        }
    }
}
