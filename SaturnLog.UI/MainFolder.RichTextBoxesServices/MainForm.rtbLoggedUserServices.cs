using SaturnLog.Core;
using SaturnLog.UI.ControlsExtensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI
{
    public partial class MainForm
    {
        private static class rtbLoggedUserServices
        {
            private readonly static Color _pleaseLogInColor = Color.Red;
            private readonly static string _pleaseConnectAndLogInText = "Please connect into the database, than Log in";
            private readonly static string _pleaseLogInText = "Please Log in";
            private readonly static Color _forcedToLogOutColor = Color.Red;
            private readonly static string _forcedToLogOutText = "Please Log in";

            private readonly static Color _loggedUserLabelColor = Color.Green;
            
            public static void ShowPleaseConnectAndLogIn(RichTextBox rtbLoggedUser)
            {
                rtbLoggedUser.Clear();
                rtbLoggedUser.AppendText(_pleaseConnectAndLogInText, _pleaseLogInColor);
            }

            public static void ShowPleaseLogIn(RichTextBox rtbLoggedUser)
            {
                rtbLoggedUser.Clear();
                rtbLoggedUser.AppendText(_pleaseLogInText, _pleaseLogInColor);
            }

            public static void ShowForcedToLogOut(RichTextBox rtbLoggedUser)
            {
                rtbLoggedUser.Clear();
                rtbLoggedUser.AppendText(_forcedToLogOutText, _pleaseLogInColor);
            }

            public static void ShowLoggedUserLabel(RichTextBox rtbLoggedUser, User loggedUser)
            {
                string username = loggedUser.Username;
                string firstName = loggedUser.FirstName;
                string surname = loggedUser.Surname;

                rtbLoggedUser.Clear();
                rtbLoggedUser.AppendText($"{username} - {firstName} {surname}", _loggedUserLabelColor);
            }
        }
    }
}
