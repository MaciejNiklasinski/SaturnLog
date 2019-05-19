using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class SessionMaintenanceIsAlreadyOngoingException : Exception
    {       
        public SessionMaintenanceIsAlreadyOngoingException() : base(GetDefaultMessage())
        {
        }

        public SessionMaintenanceIsAlreadyOngoingException(string message) : base(message)
        {
        }
        public SessionMaintenanceIsAlreadyOngoingException(string message, Exception inner) : base(message, inner)
        {
        }
        protected SessionMaintenanceIsAlreadyOngoingException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Active session maintenance is already ongoing.";
        }
    }
}
