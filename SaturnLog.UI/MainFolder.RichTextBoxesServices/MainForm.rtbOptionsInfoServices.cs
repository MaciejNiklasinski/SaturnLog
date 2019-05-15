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
        private static class rtbOptionsInfoServices
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
            public static void Clear(RichTextBox rtbOptionsInfo)
            {
                rtbOptionsInfo.Clear();
            }

            public static void ShowRetrievingUserData(RichTextBox rtbOptionsInfo, string username)
            {
                rtbOptionsInfo.Clear();
                rtbOptionsInfo.AppendLog($"Retrieving user {username} data. Please wait...", _awaitDownloadColor);
            }

            public static void ShowRetrievingSaturn5DataBySerialNumber(RichTextBox rtbOptionsInfo, string serialNumber)
            {
                rtbOptionsInfo.Clear();
                rtbOptionsInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber}. Please wait...", _awaitDownloadColor);
            }

            public static void AddRetrievingSaturn5DataBySerialNumberFailedLog(RichTextBox rtbOptionsfInfo, string serialNumber)
            {
                rtbOptionsfInfo.Clear();
                rtbOptionsfInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber} failed.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataBySerialNumberCanceledLog(RichTextBox rtbOptionsfInfo, string serialNumber)
            {
                rtbOptionsfInfo.Clear();
                rtbOptionsfInfo.AppendLog($"Retrieving data of the Saturn 5 from serial number {serialNumber} has been canceled.", _failureColor);
            }

            public static void ShowRetrievingSaturn5DataByShortId(RichTextBox rtbOptionsInfo, string shortId)
            {
                rtbOptionsInfo.Clear();
                rtbOptionsInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId}. Please wait...", _awaitDownloadColor);
            }

            public static void AddRetrievingSaturn5DataByShortIdFailedLog(RichTextBox rtbOptionsInfo, string shortId)
            {
                rtbOptionsInfo.Clear();
                rtbOptionsInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId} failed.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataByShortIdCanceledLog(RichTextBox rtbOptionsInfo, string shortId)
            {
                rtbOptionsInfo.Clear();
                rtbOptionsInfo.AppendLog($"Retrieving data of the Saturn 5 from barcode {shortId} has been canceled.", _failureColor);
            }

            public static void AddRetrievingUserDataSucceedLog(RichTextBox rtbOptionsInfo, User user)
            {
                rtbOptionsInfo.AppendLog($"{user.Username} - {user.FirstName} {user.Surname} {user.Type}", _headerColor);
            }

            public static void AddRetrievingUserDataFailedLog(RichTextBox rtbOptionsInfo, string username)
            {
                rtbOptionsInfo.AppendLog($"Fail to obtain user data from the provided username: {username}.", _failureColor);
            }

            public static void AddRetrievingUserDataCanceledLog(RichTextBox rtbOptionsInfo, string username)
            {
                rtbOptionsInfo.AppendLog($"Operation canceled. Read user data the from provided username: {username}.", _failureColor);
            }

            public static void AddRetrievingSaturn5DataSucceedLog(RichTextBox rtbOptionsInfo, Saturn5 saturn5)
            {
                rtbOptionsInfo.AppendLog($"Serial Number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId}", _headerColor);
            }
            #endregion
        }
    }
}
