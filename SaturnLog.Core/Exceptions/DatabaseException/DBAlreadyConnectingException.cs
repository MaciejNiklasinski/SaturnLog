using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBAlreadyConnectingException : Exception
    {
        public DBAlreadyConnectingException() : base(GetDefaultMessage())
        {
        }

        public DBAlreadyConnectingException(string message) : base(message)
        {
        }
        public DBAlreadyConnectingException(string message, Exception inner) : base(message, inner)
        {
        }
        protected DBAlreadyConnectingException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Database is already attempting to connect.";
        }
    }
}
