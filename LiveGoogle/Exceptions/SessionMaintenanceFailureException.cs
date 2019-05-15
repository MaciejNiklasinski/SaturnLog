using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    public class SessionMaintenanceFailureException : Exception
    {       
        public SessionMaintenanceFailureException() : base(GetDefaultMessage())
        {
        }

        public SessionMaintenanceFailureException(string message) : base(message)
        {
        }
        public SessionMaintenanceFailureException(string message, Exception inner) : base(message, inner)
        {
        }
        protected SessionMaintenanceFailureException(System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public static string GetDefaultMessage()
        {
            return $"Ongoing session maintenance failed.";
        }
    }
}
