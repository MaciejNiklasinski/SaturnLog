using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public interface ISaturn5IssuesRepository : IDisposable
    {
        // Create
        void AddNewFault(string serialNumber, string byUsername, string desctiption);

        void AddNewDamage(string serialNumber, string byUsername, string desctiption);

        // Read
        bool HasUnresolvedIssues(string serialNumber);

        bool HasUnresolvedFaults(string serialNumber);

        bool HasUnresolvedDamages(string serialNumber);

        IList<Saturn5Issue> GetIssues(string serialNumber);

        IList<Saturn5Issue> GetUnresolvedIssues(string serialNumber);

        IList<Saturn5Issue> GetUnresolvedFaults(string serialNumber);

        IList<Saturn5Issue> GetUnresolvedDamages(string serialNumber);

        IList<Saturn5Issue> GetIssuesTimerange(string serialNumber, DateTime timeRangeStart, DateTime timeRangeEnd);

        Saturn5Issue GetLastUnresolvedIssue(string serialNumber);

        Saturn5Issue GetLastUnresolvedFault(string serialNumber);

        Saturn5Issue GetLastUnresolvedDamage(string serialNumber);
        // Update
        void Update(Saturn5Issue issue);
    }
}
