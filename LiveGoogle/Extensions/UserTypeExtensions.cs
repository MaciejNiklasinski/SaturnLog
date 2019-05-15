using SaturnLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveGoogle.Extensions
{
    public static class UserTypeExtensions
    {
        public static string ToValueString(this UserType userType)
        {
            return ((int)userType).ToString();
        }

        public static UserType ValueToUserType(this string userTypeValueString)
        {
            if (!int.TryParse(userTypeValueString, out int result))
                return UserType.User;
            else
                return (UserType)result;
        }
    }
}
