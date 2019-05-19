using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core.EventArgs
{
    public class EditUserEventArgs : System.EventArgs
    {
        public User ToBeEdited { get; private set; }
        public string NewFirstName { get; private set; }
        public string NewSurname { get; private set; }
        public UserType? NewUserType { get; private set; }

        public EditUserEventArgs(User toBeEdited, string newFirstName, string newSurname, UserType? newUserType)
        {
            this.ToBeEdited = toBeEdited;
            this.NewFirstName = newFirstName;
            this.NewSurname = newSurname;
            this.NewUserType = newUserType;
        }
    }
}
