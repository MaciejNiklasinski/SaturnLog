using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBAlreadyConnectedException : Exception
    {
        public DBAlreadyConnectedException() : base(GetDefaultMessage())
        {
        }

        public DBAlreadyConnectedException(string message) : base(message)
        {
        }
        public DBAlreadyConnectedException(string message, Exception inner) : base(message ?? GetDefaultMessage(), inner)
        {
        }
        protected DBAlreadyConnectedException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Database is already connected.";
        }
    }
}
