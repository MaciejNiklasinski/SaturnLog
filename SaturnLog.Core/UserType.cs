using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public enum UserType
    {
        User = 0,
        StarUser = 1,
        Manager = 2,
        PreBrief = 3,
        DeBrief = 4,
        Admin = 1000
    }

    public static class UserTypeExtensions
    {
        public static Saturn5Status GetWithUserSaturn5Status(this UserType userType)
        {
            if (userType == UserType.Manager) return Saturn5Status.WithManager;
            else if (userType == UserType.StarUser) return Saturn5Status.WithStarUser;
            else return Saturn5Status.WithUser;
        }

        public static string GetDisplayableString(this UserType userType)
        {
            switch (userType)
            {
                case UserType.User:
                    return "USER";
                case UserType.StarUser:
                    return "STAR USER";
                case UserType.Manager:
                    return "MANAGER";
                case UserType.PreBrief:
                    return "PRE-BRIEF";
                case UserType.DeBrief:
                    return "DE-BRIEF";
                case UserType.Admin:
                    return "ADMIN";
                default:
                    throw new NotImplementedException($"Value {userType.ToString()} of UserType enum has not been implemented.");
            }
        }

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

    public static class UserTypeProvider
    {
        //public static string GetDisplayableString(UserType userType)
        //{
        //    switch (userType)
        //    {
        //        case UserType.User:
        //            return "USER";
        //        case UserType.StarUser:
        //            return "STAR USER";
        //        case UserType.Manager:
        //            return "MANAGER";
        //        case UserType.PreBrief:
        //            return "PRE-BRIEF";
        //        case UserType.DeBrief:
        //            return "DE-BRIEF";
        //        case UserType.Admin:
        //            return "ADMIN";
        //        default:
        //            throw new NotImplementedException($"Value {userType.ToString()} of UserType enum has not been implemented.");
        //            break;
        //    }
        //}
        public static UserType GetFromStringOrDefault(string userTypeString)
        {
            if (userTypeString is null
                || userTypeString == "")
                return UserType.User;

            userTypeString = userTypeString.Trim().ToUpper();

            if (userTypeString.Contains("STAR") && userTypeString.Contains("USER"))
                return UserType.StarUser;
            else if (userTypeString == "MANAGER")
                return UserType.Manager;
            else if (userTypeString == "PREBRIEF" || userTypeString == "PRE-BRIEF" || userTypeString == "PRE BRIEF")
                return UserType.PreBrief;
            else if (userTypeString == "DEBRIEF" || userTypeString == "DE-BRIEF" || userTypeString == "DE BRIEF")
                return UserType.DeBrief;
            else if (userTypeString == "ADMIN")
                return UserType.Admin;
            else
                return UserType.User;
        }

        public static UserType GetFromValueStringOrDefault(string userTypeValueString)
        {

            if (userTypeValueString is null)
                return UserType.User;

            if (int.TryParse(userTypeValueString, out int parsedUserTypeValueType))
                return (UserType)parsedUserTypeValueType;
            else
                return UserType.User;
        }

        public static UserType GetFromString(string userTypeString)
        {
            if (userTypeString is null) throw new ArgumentNullException(userTypeString);

            userTypeString = userTypeString.Trim().ToUpper();

            if (userTypeString.Contains("STAR") && userTypeString.Contains("USER"))
                return UserType.StarUser;
            else if (userTypeString == "USER")
                return UserType.User;
            else if (userTypeString == "MANAGER")
                return UserType.Manager;
            else if (userTypeString == "PREBRIEF" || userTypeString == "PRE-BRIEF" || userTypeString == "PRE BRIEF")
                return UserType.PreBrief;
            else if (userTypeString == "DEBRIEF" || userTypeString == "DE-BRIEF" || userTypeString == "DE BRIEF")
                return UserType.DeBrief;
            else if (userTypeString == "ADMIN")
                return UserType.Admin;
            else
                throw new ArgumentException("Provided userTypeString value does not represent any valid value of the UserType enumeration.", nameof(userTypeString));
        }

        public static UserType GetFromValueString(string userTypeValueString)
        {
            if (userTypeValueString is null) throw new ArgumentNullException(userTypeValueString);

            if (int.TryParse(userTypeValueString, out int parsedUserTypeValueType) && parsedUserTypeValueType >= 0 && (parsedUserTypeValueType <= 4 || parsedUserTypeValueType == (int)UserType.Admin))
                return (UserType)parsedUserTypeValueType;
            else
                throw new ArgumentException("Provided userTypeValueString value does not represent any valid value of the UserType enumeration.", nameof(userTypeValueString));
        }
    }
}
