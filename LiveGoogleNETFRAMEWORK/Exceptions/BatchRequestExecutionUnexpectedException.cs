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
    public class BatchRequestExecutionUnexpectedException : Exception
    {
        public BatchRequest BatchRequest { get; set; }
        
        public BatchRequestExecutionUnexpectedException(BatchRequest batchRequest, Exception innerException) : base(GetDefaultMessage(), innerException)
        {
            this.BatchRequest = batchRequest;
        }

        protected BatchRequestExecutionUnexpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.BatchRequest = info.GetValue("BatchRequest", typeof(BatchRequest)) as BatchRequest;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("BatchRequest", this.BatchRequest);
            base.GetObjectData(info, context);
        }

        private static string GetDefaultMessage()
        {
            return "Unexpected google service batch request execution exception. See request, and inner exception for details";
        }
    }
}
