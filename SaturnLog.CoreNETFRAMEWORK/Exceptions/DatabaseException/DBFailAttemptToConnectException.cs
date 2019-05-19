using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBFailAttemptToConnectException : Exception
    {
        public DBFailAttemptToConnectException() : base(GetDefaultMessage())
        {
        }

        public DBFailAttemptToConnectException(string message) : base(message)
        {
        }
        public DBFailAttemptToConnectException(string message, Exception inner) : base(message ?? GetDefaultMessage(), inner)
        {
        }
        protected DBFailAttemptToConnectException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Failed to establish connection with the database.";
        }
    }
}
