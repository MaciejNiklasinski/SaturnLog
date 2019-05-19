using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public class Saturns5DashboardEntry
    {
        public string SerialNumber { get; set; }
        public string ShortId { get; set; }
        public Saturn5Status Status { get; set; }
        public string PhoneNumber { get; set; }
        public string LastSeenDate { get; set; }
        public string LastSeenTime { get; set; }
        public string LastSeenUsername { get; set; }
        public string LastSeenFirstName { get; set; }
        public string LastSeenSurname { get; set; }

        public Saturns5DashboardEntry()
        {

        }

        public Saturns5DashboardEntry(string serialNumber, string shortId, Saturn5Status status, string phoneNumber, string lastSeenDate, string lastSeenTime, string lastSeenUsername, string lastSeenFirstName, string lastSeenSurname)
        {
            this.SerialNumber = serialNumber.Trim().ToUpper();
            this.ShortId = shortId.Trim().ToUpper();
            this.Status = status;
            this.PhoneNumber = phoneNumber;
            this.LastSeenDate = lastSeenDate;
            this.LastSeenFirstName = lastSeenFirstName;
            this.LastSeenUsername = lastSeenUsername.Trim().ToUpper();
        }
    }
}
