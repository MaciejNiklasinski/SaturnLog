using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBDisconnectedException : Exception
    {
        public DBDisconnectedException() : base(GetDefaultMessage())
        {
        }

        public DBDisconnectedException(string message) : base(message)
        {
        }
        public DBDisconnectedException(string message, Exception inner) : base(message ?? GetDefaultMessage(), inner)
        {
        }
        protected DBDisconnectedException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Database has been disconnected exception.";
        }
    }
}
