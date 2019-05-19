using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class DBForcedToBeTakenOverException : Exception
    {
        public DBForcedToBeTakenOverException() : base(GetDefaultMessage())
        {
        }

        public DBForcedToBeTakenOverException(string message) : base(message)
        {
        }
        public DBForcedToBeTakenOverException(string message, Exception inner) : base(message, inner)
        {
        }
        protected DBForcedToBeTakenOverException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Database has been forced to disconnect and taken over by other application instance.";
        }
    }
}
