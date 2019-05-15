using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBAlreadyDisconnectedException : Exception
    {
        public DBAlreadyDisconnectedException() : base(GetDefaultMessage())
        {
        }

        public DBAlreadyDisconnectedException(string message) : base(message)
        {
        }
        public DBAlreadyDisconnectedException(string message, Exception inner) : base(message, inner)
        {
        }
        protected DBAlreadyDisconnectedException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Database is already disconnected.";
        }
    }
}
