using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBConnectionFailureException : Exception
    {
        public DBConnectionFailureException() : base(GetDefaultMessage())
        {
        }

        public DBConnectionFailureException(string message) : base(message)
        {
        }
        public DBConnectionFailureException(string message, Exception inner) : base(message ?? GetDefaultMessage(), inner)
        {
        }
        protected DBConnectionFailureException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Established database connection failed.";
        }
    }
}
