using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public enum Saturn5Status
    {
        InDepotMissing = -1,
        InDepot = 0,
        InDepotIssuesReported = 1,
        InDepotFaulty = 1,
        InDepotDamaged = 2,
        WithUser = 3,
        WithStarUser = 4,
        WithManager = 5
    }

    public static class Saturn5StatusExtensions
    {
        public static bool IsWithUser(this Saturn5Status status)
        {
            return (status == Saturn5Status.WithUser || status == Saturn5Status.WithStarUser || status == Saturn5Status.WithManager);
        }

        public static bool IsInDepot(this Saturn5Status status)
        {
            return !status.IsWithUser();
        }

        public static string ToValueString(this Saturn5Status status)
        {
            return ((int)status).ToString();
        }

        public static Saturn5Status ValueToSaturn5Status(this string saturn5StatusValueString)
        {
            if (!int.TryParse(saturn5StatusValueString, out int result))
                throw new ArgumentException($"Unable to translate provided integer value string: {saturn5StatusValueString} into the integer value.", nameof(saturn5StatusValueString));
            else
                return (Saturn5Status)result;
        }
    }

    public static class Saturn5StatusService
    {
        public static string GetDashboardString(Saturn5Status status)
        {
            switch (status)
            {
                case Saturn5Status.InDepotMissing:
                    return "In Depot - Missing";
                case Saturn5Status.InDepot:
                    return "In Depot"; 
                case Saturn5Status.InDepotFaulty:
                    return "In Depot - Faulty";
                case Saturn5Status.InDepotDamaged:
                    return "In Depot - Damaged";
                case Saturn5Status.WithUser:
                    return "With User";
                case Saturn5Status.WithStarUser:
                    return "With Star User";
                case Saturn5Status.WithManager:
                    return "With Manager";
                default:
                    throw new NotImplementedException($"Saturn5Status value {status} has to be implemented.");
            }
        }

        public static Saturn5Status GetStatusFromDashboardString(string dashboardString)
        {
            if (dashboardString is null) throw new ArgumentNullException(nameof(dashboardString));


            if (dashboardString == "In Depot - Missing") return Saturn5Status.InDepotMissing;
            else if (dashboardString == "In Depot") return Saturn5Status.InDepot;
            else if (dashboardString == "In Depot - Issues Reported") return Saturn5Status.InDepotFaulty;
            else if (dashboardString == "In Depot - Faulty") return Saturn5Status.InDepotFaulty;
            else if (dashboardString == "In Depot - Damaged") return Saturn5Status.InDepotDamaged;
            else if (dashboardString == "With User") return Saturn5Status.WithUser;
            else if (dashboardString == "With Star User") return Saturn5Status.WithStarUser;
            else if (dashboardString == "With Manager") return Saturn5Status.WithManager;
            else throw new ArgumentException("Provided dashboardString {dashboardString} doesn't represent any recognized Saturn5Status enum value.", nameof(dashboardString));
        }
    }
}
