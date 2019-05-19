using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public class Saturn5
    {
        public string SerialNumber { get; set; }
        public string ShortId { get; set; }
        public Saturn5Status Status { get; set; }
        public string LastSeenDate { get; set; }
        public string LastSeenTime { get; set; }
        public string LastSeenUsername { get; set; }
        public string PhoneNumber { get; set; }
        
        public Saturn5(string serialNumber, string shortId, Saturn5Status status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenUsername)
        {
            this.SerialNumber = serialNumber.Trim().ToUpper();
            this.ShortId = shortId.Trim().ToUpper();
            this.Status = status;
            this.PhoneNumber = phoneNumber;
            this.LastSeenDate = lastSeenDate;
            this.LastSeenTime = lastSeenTime;
            this.LastSeenUsername = lastSeenUsername.Trim().ToUpper();
        }
    }
}
