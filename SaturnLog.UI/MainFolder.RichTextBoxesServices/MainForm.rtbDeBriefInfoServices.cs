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
        private static class rtbDeBriefInfoServices
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
            public static void Clear(RichTextBox rtbDeBriefInfo)
            {
                rtbDeBriefInfo.Clear();
            }

            public static void ShowRetrievingUserData(RichTextBox rtbDeBriefInfo, string username)
            {
                rtbDeBriefInfo.Clear();
                rtbDeBriefInfo.AppendLog($"Retrieving user {username} data. Please wait...", _awaitDownloadColor);
            }


            public static void ShowRetrievingSaturn5DataBySerialNumber(RichTextBox rtbDeBriefInfo, string serialNumber)
            {
                rtbDeBriefInfo.Clear();
                rtbDeBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber}. Please wait...", _awaitDownloadColor);
            }

            public static void AddRetrievingSaturn5DataBySerialNumberFailedLog(RichTextBox rtbDeBriefInfo, string serialNumber)
            {
                rtbDeBriefInfo.Clear();
                rtbDeBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber} failed.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataBySerialNumberCanceledLog(RichTextBox rtbDeBriefInfo, string serialNumber)
            {
                rtbDeBriefInfo.Clear();
                rtbDeBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber} has been canceled.", _failureColor);
            }

            public static void ShowRetrievingSaturn5DataByShortId(RichTextBox rtbDeBriefInfo, string shortId)
            {
                rtbDeBriefInfo.Clear();
                rtbDeBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId}. Please wait...", _awaitDownloadColor);
            }

            public static void AddRetrievingSaturn5DataByShortIdFailedLog(RichTextBox rtbDeBriefInfo, string shortId)
            {
                rtbDeBriefInfo.Clear();
                rtbDeBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId} failed.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataByShortIdCanceledLog(RichTextBox rtbDeBriefInfo, string shortId)
            {
                rtbDeBriefInfo.Clear();
                rtbDeBriefInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId} has been canceled.", _failureColor);
            }

            public static void AddRetrievingUserDataSucceedLog(RichTextBox rtbDeBriefInfo, User user)
            {
                rtbDeBriefInfo.AppendLog($"{user.Username} - {user.FirstName} {user.Surname} {user.Type}", _headerColor);
            }

            public static void AddRetrievingUserDataFailedLog(RichTextBox rtbDeBriefInfo, string username)
            {
                rtbDeBriefInfo.AppendLog($"Fail to obtain user data from the provided username: {username}.", _failureColor);
            }

            public static void AddRetrievingUserDataCanceledLog(RichTextBox rtbDeBriefInfo, string username)
            {
                rtbDeBriefInfo.AppendLog($"Operation canceled. Read user data the from provided username: {username}.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataSucceedLog(RichTextBox rtbDeBriefInfo, Saturn5 saturn5)
            {
                rtbDeBriefInfo.AppendLog($"Serial Number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId}", _headerColor);
            }
            
            public static void AddSaturn5IssuesAndDamagesLog(RichTextBox rtbDeBriefInfo, IList<Saturn5Issue> issuesOrDamages)
            {
                rtbDeBriefInfo.AppendLog("Issues and damages:", _defaultColor);

                foreach (Saturn5Issue issue in issuesOrDamages)
                    rtbDeBriefInfo.AppendLog($"{issue.Description}", _defaultColor);

                rtbDeBriefInfo.AppendLog("  ", _defaultColor);
            }
            #endregion
        }
    }
}
