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
        private static class rtbPreBriefInfoServices
        {
            #region Private Fields
            private readonly static Color _logTimestampColor = Color.Gray;
            private readonly static Color _buttonPressedLogColor = Color.Gray;
            private readonly static Color _backToIdleColor = Color.Gray;

            private readonly static Color _defaultColor = Color.Black;
            private readonly static Color _failureColor = Color.Red;
            private readonly static Color _successColor = Color.Green;
            private readonly static Color _awaitDownloadColor = Color.Purple;
            private readonly static Color _awaitUploadColor = Color.DarkOrange;
            private readonly static Color _headerColor = Color.Blue;
            #endregion

            #region Methods
            public static void Clear(RichTextBox rtbPreBriefInfo)
            {
                rtbPreBriefInfo.Clear();
            }

            public static void ShowRetrievingUserData(RichTextBox rtbPreBriefInfo, string username)
            {
                rtbPreBriefInfo.Clear();
                rtbPreBriefInfo.AppendLog($"Retrieving user {username} data. Please wait...", _awaitDownloadColor);
            }

            public static void ShowRetrievingSaturn5DataBySerialNumber(RichTextBox rtbPreBriefInfo, string serialNumber)
            {
                rtbPreBriefInfo.Clear();
                rtbPreBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber}. Please wait...", _awaitDownloadColor);
            }

            public static void AddRetrievingSaturn5DataBySerialNumberFailedLog(RichTextBox rtbPreBriefInfo, string serialNumber)
            {
                rtbPreBriefInfo.Clear();
                rtbPreBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber} failed.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataBySerialNumberCanceledLog(RichTextBox rtbPreBriefInfo, string serialNumber)
            {
                rtbPreBriefInfo.Clear();
                rtbPreBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber} has been canceled.", _failureColor);
            }

            public static void ShowRetrievingSaturn5DataByShortId(RichTextBox rtbPreBriefInfo, string shortId)
            {
                rtbPreBriefInfo.Clear();
                rtbPreBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId}. Please wait...", _awaitDownloadColor);
            }

            public static void AddRetrievingSaturn5DataByShortIdFailedLog(RichTextBox rtbPreBriefInfo, string shortId)
            {
                rtbPreBriefInfo.Clear();
                rtbPreBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId} failed.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataByShortIdCanceledLog(RichTextBox rtbPreBriefInfo, string shortId)
            {
                rtbPreBriefInfo.Clear();
                rtbPreBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId} has been canceled.", _failureColor);
            }

            public static void AddRetrievingUserDataSucceedLog(RichTextBox rtbPreBriefInfo, User user)
            {
                rtbPreBriefInfo.AppendLog($"{user.Username} - {user.FirstName} {user.Surname} {user.Type}", _headerColor);
            }

            public static void AddRetrievingUserDataFailedLog(RichTextBox rtbPreBriefInfo, string username)
            {
                rtbPreBriefInfo.AppendLog($"Fail to obtain user data from the provided username: {username}.", _failureColor);
            }

            public static void AddRetrievingUserDataCanceledLog(RichTextBox rtbPreBriefInfo, string username)
            {
                rtbPreBriefInfo.AppendLog($"Operation canceled. Read user data the from provided username: {username}.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataSucceedLog(RichTextBox rtbPreBriefInfo, Saturn5 saturn5)
            {
                rtbPreBriefInfo.AppendLog($"Serial Number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId}", _headerColor);
            }
            
            public static void AddSaturn5IssuesAndDamagesLog(RichTextBox rtbPreBriefInfo, IList<Saturn5Issue> issuesOrDamages)
            {
                rtbPreBriefInfo.AppendLog("Issues and damages:", _defaultColor);

                foreach (Saturn5Issue issue in issuesOrDamages)
                    rtbPreBriefInfo.AppendLog($"{issue.Description}", _defaultColor);

                rtbPreBriefInfo.AppendLog("  ", _defaultColor);
            }
            #endregion
        }
    }
}
