using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBNotConnectedException : Exception
    {
        public DBNotConnectedException() : base(GetDefaultMessage())
        {
        }

        public DBNotConnectedException(string message) : base(message)
        {
        }
        public DBNotConnectedException(string message, Exception inner) : base(message ?? GetDefaultMessage(), inner)
        {
        }
        protected DBNotConnectedException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Database is not connected.";
        }
    }
}
