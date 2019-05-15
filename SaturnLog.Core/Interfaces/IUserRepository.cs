using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public interface IUserRepository : IDisposable
    {
        void Create(string username, string firstName, string surname, UserType type);

        // Read
        bool HasUsername(string username);

        IEnumerable<string> GetAllUsernames();

        string GetUserFirstName(string username);

        string GetUserSurname(string username);

        UserType GetUserType(string username);

        string GetUserLogSpreadsheetId(string username);

        User Read(string username);
        
        IList<User> ReadAll();

        // Update
        void AddUserLog(string username, string userLog);
            
        void Update(string username, string firstName = null, string surname = null, UserType? type = null);

        // Delete
        void Delete(string username);
    }
}
