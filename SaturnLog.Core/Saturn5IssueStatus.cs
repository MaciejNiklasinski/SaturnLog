using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public enum Saturn5IssueStatus
    {
        Reported = 0,
        Damaged = 1,
        Resolved = 2,
        ResolvedByIT = 3,
        KnownIssue = 4,
        CannotReplicate = 5,
        NotAnIssue = 6
    }

    public static class Saturn5IssueStatusExtensions
    {
        public static Saturn5IssueStatus GetFromString(string issueStatusString)
        {
            if (issueStatusString is null) throw new ArgumentNullException(nameof(issueStatusString));

            issueStatusString = issueStatusString.Trim().ToUpper();

            if (issueStatusString == "REPORTED")
                return Saturn5IssueStatus.Reported;
            else if (issueStatusString == "RESOLVED")
                return Saturn5IssueStatus.Resolved;
            else if (issueStatusString == "RESOLVEDBYIT" 
                || issueStatusString == "RESOLVED BY IT")
                return Saturn5IssueStatus.ResolvedByIT;
            else if (issueStatusString == "KNOWNISSUE"
                || issueStatusString == "KNOWN ISSUE")
                return Saturn5IssueStatus.KnownIssue;
            else if (issueStatusString == "CANNOTREPLICATE"
                || issueStatusString == "CANNOT REPLICATE")
                return Saturn5IssueStatus.CannotReplicate;
            else if (issueStatusString == "NOTANISSUE"
                || issueStatusString == "NOT AN ISSUE")
                return Saturn5IssueStatus.NotAnIssue;
            else if (issueStatusString == "DAMAGED")
                return Saturn5IssueStatus.Damaged;
            else
                throw new ArgumentException("Provided issueStatusString doesn't represent any valid Saturn5IssueSatus enum state.", nameof(issueStatusString));
        }
    
        public static Saturn5IssueStatus GetFromValueString(string issueStatusValueString)
        {
            if (issueStatusValueString is null) throw new ArgumentNullException(nameof(issueStatusValueString));

            if (int.TryParse(issueStatusValueString, out int parsedIssueStatusValueString)
                && parsedIssueStatusValueString >= 0
                && parsedIssueStatusValueString <= 6)
                return (Saturn5IssueStatus)parsedIssueStatusValueString;
            else
                throw new ArgumentException("Provided issueStatusValueString doesn't represent any valid Saturn5IssueSatus enum state.", nameof(issueStatusValueString));
        }

        public static string ToDisplayString(this Saturn5IssueStatus status)
        {
            switch (status)
            {
                case Saturn5IssueStatus.Reported:
                    return "REPORTED";
                case Saturn5IssueStatus.Damaged:
                    return "DAMAGED";
                case Saturn5IssueStatus.Resolved:
                    return "RESOLVED";
                case Saturn5IssueStatus.ResolvedByIT:
                    return "RESOLVED BY IT";
                case Saturn5IssueStatus.KnownIssue:
                    return "KNOWN ISSUE";
                case Saturn5IssueStatus.CannotReplicate:
                    return "CANNOT REPLICATE";
                case Saturn5IssueStatus.NotAnIssue:
                    return "NOT AN ISSUE";
                default:
                    throw new ArgumentException("Provided status doesn't represent any valid Saturn5IssueSatus enum state.", nameof(status));
            }
        }
    }
}
