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
        private static class rtbCurrentlyServices
        {           
            #region Private readonly static fields
            private readonly static Color _awaitingToConnectToDBColor = Color.Black;
            private readonly static string _awaitingToConnectToDBText = "Awaiting for user to connect into the database.";
            
            private readonly static Color _awaitingToLogInColor = Color.Black;
            private readonly static string _awaitingToLogInText = "Awaiting for user to log in.";

            private readonly static Color _awaitingToLogOutColor = Color.Black;
            private readonly static string _awaitingToLogOutText = "Awaiting for user to log out.";

            private readonly static Color _awaitingUserUsernameColor = Color.Black;
            private readonly static string _awaitingUserUsernameText = "Awaiting for Username.";

            private readonly static Color _awaitingSaturn5SerialNumberColor = Color.Black;
            private readonly static string _awaitingSaturn5SerialNumberText = "Awaiting for Saturn5 SERIAL NUMBER.";

            private readonly static Color _awaitingSaturn5ShortIdColor = Color.Black;
            private readonly static string _awaitingSaturn5ShortIdText = "Awaiting for Saturn5 BARCODE.";

            private readonly static Color _awaitingSaturn5FaultReportColor = Color.Black;
            private readonly static string _awaitingSaturn5FaultReportText = "Awaiting for Saturn5 Fault report.";

            private readonly static Color _awaitingSaturn5DamageReportColor = Color.Black;
            private readonly static string _awaitingSaturn5DamageReportText = "Awaiting for Saturn 5 Damage report.";

            private readonly static Color _awaitingSaturn5ReceiveFromITReportColor = Color.Black;
            private readonly static string _awaitingSaturn5ReceiveFromITReportText = "Awaiting for Saturn 5 receive from IT report.";

            private readonly static Color _awaitingCreateSaturn5ReportColor = Color.Black;
            private readonly static string _awaitingCreateSaturn5ReportText = "Awaiting for Saturn 5 create report.";

            private readonly static Color _awaitingEditSaturn5ReportColor = Color.Black;
            private readonly static string _awaitingEditSaturn5ReportText = "Awaiting for Saturn 5 create report.";

            private readonly static Color _awaitingSaturn5SendToITReportColor = Color.Black;
            private readonly static string _awaitingSaturn5SendToITReportText = "Awaiting for Saturn 5 send to IT report.";

            private readonly static Color _awaitingRemoveSaturn5ReportColor = Color.Black;
            private readonly static string _awaitingRemoveSaturn5ReportText = "Awaiting for Saturn 5 remove report.";

            private readonly static Color _awaitingResolveSaturn5IssueReportColor = Color.Black;
            private readonly static string _awaitingResolveSaturn5IssueReportText = "Awaiting for Saturn 5 issue resolved report.";

            private readonly static Color _awaitingCreateUserReportColor = Color.Black;
            private readonly static string _awaitingCreateUserReportText = "Awaiting for User create report.";

            private readonly static Color _awaitingEditUserReportColor = Color.Black;
            private readonly static string _awaitingEditUserReportText = "Awaiting for User edit report.";

            private readonly static Color _awaitingRemoveUserReportColor = Color.Black;
            private readonly static string _awaitingRemoveUserReportText = "Awaiting for User remove report.";


            private readonly static Color _showIdleColor = Color.Gray;
            private readonly static string _showIdleText = "Idle.";


            private readonly static Color _showValidatingUsernameColor = Color.Brown;
            private readonly static string _showValidatingUsernameText = "Validating provided username.";

            private readonly static Color _showValidatingSaturn5SerialNumberColor = Color.Brown;
            private readonly static string _showValidatingSaturn5SerialNumberText = "Validating provided Saturn 5 serial number.";

            private readonly static Color _showValidatingSaturn5ShortIdColor = Color.Brown;
            private readonly static string _showValidatingSaturn5ShortIdText = "Validating provided Saturn 5 barcode.";


            private readonly static Color _attemptingToConnectToDBColor = Color.Purple;
            private readonly static string _attemptingToConnectToDBText = "Attempting to connect to database.";

            private readonly static Color _attemptingToDisconnectFromDBColor = Color.Purple;
            private readonly static string _attemptingToDisconnectFromDBText = "Attempting to disconnect from database.";

            private readonly static Color _attemptingToLogInColor = Color.Purple;
            private readonly static string _attemptingToLogInText = "Attempting to log in.";

            private readonly static Color _attemptingToLogOutColor = Color.Purple;
            private readonly static string _attemptingToLogOutText = "Attempting to log out.";


            private readonly static Color _showRetrievingUserDataColor = Color.Purple;
            private readonly static string _showRetrievingUserDataText = "Retrieving User data.";

            private readonly static Color _showRetrievingSaturn5DataColor = Color.Purple;
            private readonly static string _showRetrievingSaturn5DataText = "Retrieving Saturn 5 data.";


            private readonly static Color _showUploadingUserDataColor = Color.DarkOrange;
            private readonly static string _showUploadingUserDataText = "Uploading User data.";

            private readonly static Color _showUploadingSaturn5DataColor = Color.DarkOrange;
            private readonly static string _showUploadingSaturn5DataText = "Uploading Saturn 5 data.";

            private readonly static Color _showUploadingSaturn5FaultDataColor = Color.DarkOrange;
            private readonly static string _showUploadingSaturn5FaultDataText = "Uploading Saturn 5 Fault data.";

            private readonly static Color _showUploadingSaturn5DamageDataColor = Color.DarkOrange;
            private readonly static string _showUploadingSaturn5DamageDataText = "Uploading Saturn 5 Damage data.";

            private readonly static Color _showUploadingSaturn5AllocationDataColor = Color.DarkOrange;
            private readonly static string _showUploadingSaturn5AllocationDataText = "Uploading: Saturn5 allocated to the user.";

            private readonly static Color _showUploadingSaturn5ConfirmedBackInDataColor = Color.DarkOrange;
            private readonly static string _showUploadingSaturn5ConfirmedBackInDataText = "Uploading: Saturn5 confirmed back in depot.";

            private readonly static Color _showUploadingFaultySaturn5ConfirmedBackInDataColor = Color.DarkOrange;
            private readonly static string _showUploadingFaultySaturn5ConfirmedBackInDataText = "Uploading: Faulty Saturn5 confirmed back in depot.";

            private readonly static Color _showUploadingDamagedSaturn5ConfirmedBackInDataColor = Color.DarkOrange;
            private readonly static string _showUploadingDamagedSaturn5ConfirmedBackInDataText = "Uploading: Damaged Saturn5 confirmed back in depot.";

            private readonly static Color _showUploadingSaturn5ReceiveFromITDataColor = Color.DarkOrange;
            private readonly static string _showUploadingSaturn5ReceiveFromITDataText = "Uploading: Saturn5 unit received from IT added into the depot stock.";

            private readonly static Color _showUploadingCreateSaturn5DataColor = Color.DarkOrange;
            private readonly static string _showUploadingCreateSaturn5DataText = "Uploading: Saturn5 unit added into the depot stock.";

            private readonly static Color _showUploadingEditSaturn5DataColor = Color.DarkOrange;
            private readonly static string _showUploadingEditSaturn5DataText = "Uploading: Saturn5 unit parameters edited.";

            private readonly static Color _showUploadingSaturn5SendToITDataColor = Color.DarkOrange;
            private readonly static string _showUploadingSaturn5SendToITDataText = "Uploading: Saturn5 unit send to IT and removed from the depot stock.";

            private readonly static Color _showUploadingRemoveSaturn5DataColor = Color.DarkOrange;
            private readonly static string _showUploadingRemoveSaturn5DataText = "Uploading: Saturn5 unit removed from depot stock.";

            private readonly static Color _showUploadingResolveSaturn5IssueDataColor = Color.DarkOrange;
            private readonly static string _showUploadingResolveSaturn5IssueDataText = "Uploading: Saturn5 unit issue resolved in depot.";

            private readonly static Color _showUploadingCreateUserDataColor = Color.DarkOrange;
            private readonly static string _showUploadingCreateUserDataText = "Uploading: User added into the depot user list.";

            private readonly static Color _showUploadingEditUserDataColor = Color.DarkOrange;
            private readonly static string _showUploadingEditUserDataText = "Uploading: User parameters edited.";

            private readonly static Color _showUploadingRemoveUserDataColor = Color.DarkOrange;
            private readonly static string _showUploadingRemoveUserDataText = "Uploading: User removed from depot users list.";
            #endregion

            #region Common
            public static void Clear(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
            }

            public static void ShowIdle(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showIdleText, _showIdleColor);
            }

            public static void ShowValidatingUsername(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showValidatingUsernameText, _showValidatingUsernameColor);

            }

            public static void ShowValidatingSaturn5SerialNumber(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showValidatingSaturn5SerialNumberText, _showValidatingSaturn5SerialNumberColor);
            }

            public static void ShowValidatingSaturn5ShortId(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showValidatingSaturn5ShortIdText, _showValidatingSaturn5ShortIdColor);
            }

            public static void ShowRetrievingUserData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showRetrievingUserDataText, _showRetrievingUserDataColor);
            }

            public static void ShowRetrievingSaturn5Data(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showRetrievingSaturn5DataText, _showRetrievingSaturn5DataColor);
            }

            public static void ShowUploadingUserData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingUserDataText, _showUploadingUserDataColor);
            }

            public static void ShowUploadingSaturn5Data(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingSaturn5DataText, _showUploadingSaturn5DataColor);
            }

            public static void ShowUploadingSaturn5FaultData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingSaturn5FaultDataText, _showUploadingSaturn5FaultDataColor);
            }

            public static void ShowUploadingSaturn5DamageData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingSaturn5DamageDataText, _showUploadingSaturn5DamageDataColor);
            }
            #endregion

            #region Uploading - Pre Brief exclusively            
            public static void ShowUploadingSaturn5AllocationData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingSaturn5AllocationDataText, _showUploadingSaturn5AllocationDataColor);
            }
            #endregion

            #region Uploading - De Brief exclusively
            public static void ShowUploadingSaturn5ConfirmationData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingSaturn5ConfirmedBackInDataText, _showUploadingSaturn5ConfirmedBackInDataColor);
            }

            public static void ShowUploadingFaultySaturn5ConfirmationData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingFaultySaturn5ConfirmedBackInDataText, _showUploadingFaultySaturn5ConfirmedBackInDataColor);
            }

            public static void ShowUploadingDamagedSaturn5ConfirmationData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingDamagedSaturn5ConfirmedBackInDataText, _showUploadingDamagedSaturn5ConfirmedBackInDataColor);
            }
            #endregion

            #region Uploading - Saturn 5 Stock Management exclusively
            public static void ShowUploadingSaturn5ReceiveFromITData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingSaturn5ReceiveFromITDataText, _showUploadingSaturn5ReceiveFromITDataColor);
            }

            public static void ShowUploadingCreateSaturn5Data(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingCreateSaturn5DataText, _showUploadingCreateSaturn5DataColor);
            }

            public static void ShowUploadingEditSaturn5Data(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingEditSaturn5DataText, _showUploadingEditSaturn5DataColor);
            }

            public static void ShowUploadingSaturn5SendToITData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingSaturn5SendToITDataText, _showUploadingSaturn5SendToITDataColor);
            }

            public static void ShowUploadingRemoveSaturn5Data(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingRemoveSaturn5DataText, _showUploadingRemoveSaturn5DataColor);
            }

            public static void ShowUploadingResolveSaturn5IssueData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingResolveSaturn5IssueDataText, _showUploadingResolveSaturn5IssueDataColor);
            }
            #endregion

            #region Uploading - Admin exclusively
            public static void ShowUploadingCreateUserData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingCreateUserDataText, _showUploadingCreateUserDataColor);
            }

            public static void ShowUploadingEditUserData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingEditUserDataText, _showUploadingEditUserDataColor);
            }

            public static void ShowUploadingRemoveUserData(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_showUploadingRemoveUserDataText, _showUploadingRemoveUserDataColor);
            }
            #endregion

            #region Database Connection ShowAttemptingToDisconnectFromDatabase
            public static void ShowAwaitingToConnectToDatabase(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingToConnectToDBText, _awaitingToConnectToDBColor);
            }

            public static void ShowAttemptingToConnectToDatabase(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_attemptingToConnectToDBText, _attemptingToConnectToDBColor);
            }

            public static void ShowAttemptingToDisconnectFromDatabase(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_attemptingToDisconnectFromDBText, _attemptingToDisconnectFromDBColor);
            }

            public static void ShowAwaitingToLogIn(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingToLogInText, _awaitingToLogInColor);
            }

            public static void ShowAttemptingToLogIn(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_attemptingToLogInText, _attemptingToLogInColor);
            }

            public static void ShowAwaitingToLogOut(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingToLogOutText, _awaitingToLogOutColor);
            }

            public static void ShowAttemptingToLogOut(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_attemptingToLogOutText, _attemptingToLogOutColor);
            }
            #endregion

            #region Awaiting...
            public static void ShowAwaitingUserUsername(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingUserUsernameText, _awaitingUserUsernameColor);
            }

            public static void ShowAwaitingSaturn5SerialNumber(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingSaturn5SerialNumberText, _awaitingSaturn5SerialNumberColor);
            }

            public static void ShowAwaitingSaturn5ShortId(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingSaturn5ShortIdText, _awaitingSaturn5ShortIdColor);
            }

            public static void ShowAwaitingSaturn5FaultReport(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingSaturn5FaultReportText, _awaitingSaturn5FaultReportColor);
            }

            public static void ShowAwaitingSaturn5DamageReport(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingSaturn5DamageReportText, _awaitingSaturn5DamageReportColor);
            }

            public static void ShowAwaitingSaturn5ReceiveFromITReport(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingSaturn5ReceiveFromITReportText, _awaitingSaturn5ReceiveFromITReportColor);
            }

            public static void ShowAwaitingCreateSaturn5Report(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingCreateSaturn5ReportText, _awaitingCreateSaturn5ReportColor);
            }

            public static void ShowAwaitingEditSaturn5Report(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingEditSaturn5ReportText, _awaitingEditSaturn5ReportColor);
            }

            public static void ShowAwaitingSaturn5SendToITReport(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingSaturn5SendToITReportText, _awaitingSaturn5SendToITReportColor);
            }

            public static void ShowAwaitingRemoveSaturn5Report(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingRemoveSaturn5ReportText, _awaitingRemoveSaturn5ReportColor);
            }

            public static void ShowAwaitingResolveSaturn5IssueReport(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingResolveSaturn5IssueReportText, _awaitingResolveSaturn5IssueReportColor);
            }

            public static void ShowAwaitingCreateUserReport(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingCreateUserReportText, _awaitingCreateUserReportColor);
            }

            public static void ShowAwaitingEditUserReport(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingEditUserReportText, _awaitingEditUserReportColor);
            }

            public static void ShowAwaitingRemoveUserReport(RichTextBox rtbCurrently)
            {
                rtbCurrently.Clear();
                rtbCurrently.AppendText(_awaitingRemoveUserReportText, _awaitingRemoveUserReportColor);
            }
            #endregion
        }
    }
}
