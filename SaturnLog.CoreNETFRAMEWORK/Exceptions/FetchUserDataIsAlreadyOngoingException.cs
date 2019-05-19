using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class FetchUserDataIsAlreadyOngoingException : Exception
    {
        public FetchUserDataIsAlreadyOngoingException() : base(GetDefaultMessage())
        {
        }

        public FetchUserDataIsAlreadyOngoingException(string message) : base(message)
        {
        }
        public FetchUserDataIsAlreadyOngoingException(string message, Exception inner) : base(message, inner)
        {
        }
        protected FetchUserDataIsAlreadyOngoingException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Fetching users data is already ongoing.";
        }
    }
}
