using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.Exceptions
{
    public class FetchSaturn5DataIsAlreadyOngoingException : Exception
    {
        public FetchSaturn5DataIsAlreadyOngoingException() : base(GetDefaultMessage())
        {
        }

        public FetchSaturn5DataIsAlreadyOngoingException(string message) : base(message)
        {
        }
        public FetchSaturn5DataIsAlreadyOngoingException(string message, Exception inner) : base(message, inner)
        {
        }
        protected FetchSaturn5DataIsAlreadyOngoingException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Fetching Saturns 5 data is already ongoing.";
        }
    }
}
