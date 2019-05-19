using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBIsAttemptingToConnectToOtherApplicationInstanceException : Exception
    {
        public DBIsAttemptingToConnectToOtherApplicationInstanceException() : base(GetDefaultMessage())
        {
        }

        public DBIsAttemptingToConnectToOtherApplicationInstanceException(string message) : base(message)
        {
        }
        public DBIsAttemptingToConnectToOtherApplicationInstanceException(string message, Exception inner) : base(message ?? GetDefaultMessage(), inner)
        {
        }
        protected DBIsAttemptingToConnectToOtherApplicationInstanceException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Database is currently in use by other application instance.";
        }
    }
}
