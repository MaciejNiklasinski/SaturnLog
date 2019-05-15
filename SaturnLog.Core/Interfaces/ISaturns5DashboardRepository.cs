using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public interface ISaturns5DashboardRepository : IDisposable
    {
        // Creates new Saturns5DashboardEntry based on the saturn 5 unit existing withing Saturns5DB, and specified with provided serial number.
        void Create(Saturn5 saturn5);

        // Read
        bool HasSerialNumberAssociatedEntry(string serialNumber);

        Saturns5DashboardEntry Read(string serialNumber);

        IList<Saturns5DashboardEntry> ReadAll();

        // Update
        void UpdateUserDetails(User user);

        void Update(Saturn5 saturn5);

        // Delete
        void Delete(string serialNumber);
    }
}
