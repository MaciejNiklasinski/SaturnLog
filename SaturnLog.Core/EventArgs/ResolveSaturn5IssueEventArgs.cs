using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class ResolveSaturn5IssueEventArgs : System.EventArgs
    {
        public Saturn5Issue Issue { get; private set; }
        public User ResolvedBy { get; private set; }
        public Saturn5IssueStatus ResolvedHow { get; private set; }
        public string ResolvedHowDescription { get; private set; }

        public ResolveSaturn5IssueEventArgs(Saturn5Issue issue, User resolvedBy, Saturn5IssueStatus resolvedHow, string resolvedHowDescription)
        {
            this.Issue = issue;
            this.ResolvedBy = resolvedBy;
            this.ResolvedHow = resolvedHow;
            this.ResolvedHowDescription = resolvedHowDescription;
        }
    }
}
