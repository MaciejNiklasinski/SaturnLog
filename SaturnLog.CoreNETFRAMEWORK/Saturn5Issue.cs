using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public class Saturn5Issue
    {
        public string SerialNumber { get; set; }
        public string Timestamp { get; set; }
        public string ReportedByUsername { get; set; }
        public string Description { get; set; }
        public Saturn5IssueStatus Status { get; set; }
        public string ResolvedHowDescription{ get; set; }
        public string ResolvedByUsername { get; set; }

        public Saturn5Issue(string serialNumber, string timestamp, string reportedByUsername, string description, Saturn5IssueStatus status, string resolvedHowDescription, string resolvedByUsername)
        {
            this.SerialNumber = serialNumber;
            this.Timestamp = timestamp;
            this.ReportedByUsername = reportedByUsername;
            this.Description = description;
            this.Status = status;
            this.ResolvedHowDescription = resolvedHowDescription;
            this.ResolvedByUsername = resolvedByUsername;
        }
    }
}
