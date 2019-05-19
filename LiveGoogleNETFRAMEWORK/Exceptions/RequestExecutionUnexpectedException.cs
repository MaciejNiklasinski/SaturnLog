using Google.Apis.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Exceptions
{
    [Serializable]
    public class RequestExecutionUnexpectedException : Exception
    {
        public IClientServiceRequest Request { get; set; }
        
        public RequestExecutionUnexpectedException(IClientServiceRequest request, Exception innerException) : base(GetDefaultMessage(), innerException)
        {
            this.Request = request;
        }

        protected RequestExecutionUnexpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.Request = info.GetValue("Request", typeof(IClientServiceRequest)) as IClientServiceRequest;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("Request", this.Request);
            base.GetObjectData(info, context);
        }

        private static string GetDefaultMessage()
        {
            return "Unexpected google service request execution exception. See request, and inner exception for details";
        }
    }
}
