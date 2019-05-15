using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class CreateUserEventArgs : System.EventArgs
    {
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string Surname {get; private set; }
        public UserType UserType {get; private set; }

        public CreateUserEventArgs(string username, string firstName, string surname, UserType userType)
        {
            this.Username = username;
            this.FirstName = firstName;
            this.Surname = surname;
            this.UserType = userType;
        }
    }
}
