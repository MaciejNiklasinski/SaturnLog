using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public UserType Type { get; set; }

        public User(string username, string firstName, string surname, UserType type)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.Surname = surname;
            this.Type = type;
        }
    }
}
