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
        private static class rtbDoNowServices
        {
            private readonly static Color _defaultColor = Color.Black;
            private readonly static Color _validatingingInputWaitColor = Color.Brown;
            private readonly static Color _retrivingDataWaitColor = Color.Purple;
            private readonly static Color _uploadingDataWaitColor = Color.DarkOrange;
            private readonly static Color _provideColor = Color.Blue;

            public static void Clear(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
            }

            public static void ShowConnectToDatabase(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please connect into the database.", _defaultColor);
            }

            public static void ShowLogIn(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please log in.", _defaultColor);
            }

            public static void ShowConnectingDataWait(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Connecting to database. Please wait.", _validatingingInputWaitColor);
            }

            public static void ShowDisconnectingDataWait(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Disconnecting to database. Please wait.", _validatingingInputWaitColor);
            }

            public static void ShowValidatingingInputWait(RichTextBox rtbDoNow) 
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Validating provided value. Please wait.", _validatingingInputWaitColor);
            }

            public static void ShowRetrievingDataWait(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Retrieving data. Please wait.", _retrivingDataWaitColor);
            }

            public static void ShowUploadingDataWait(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Uploading data. Please wait.", _uploadingDataWaitColor);
            }

            public static void ShowScanUserUsername(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Username.", _provideColor);
            }

            public static void ShowScanSaturn5SerialNumber(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Saturn5 SERIAL NUMBER.", _provideColor);
            }

            public static void ShowScanSaturn5ShortId(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Saturn5 BARCODE.", _provideColor);
            }

            public static void ShowProvideSaturn5FaultReport(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Saturn5 Fault report.", _provideColor);
            }

            public static void ShowProvideSaturn5DamageReport(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Saturn5 Damage report.", _provideColor);
            }

            public static void ShowProvideSaturn5ReceiveFromITReport(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Saturn5 receive from IT report.", _provideColor);
            }

            public static void ShowProvideCreateSaturn5Report(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide create Saturn5 report.", _provideColor);
            }

            public static void ShowProvideEditSaturn5Report(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide edit Saturn5 report.", _provideColor);
            }

            public static void ShowProvideSaturn5SendToITReport(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Saturn5 send to IT report.", _provideColor);
            }

            public static void ShowProvideRemoveSaturn5Report(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Saturn5 remove report.", _provideColor);
            }

            public static void ShowProvideResolveSaturn5IssueReport(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide Saturn5 issue resolved report.", _provideColor);
            }

            public static void ShowProvideCreateUserReport(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide create User report.", _provideColor);
            }

            public static void ShowProvideEditUserReport(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide edit User report.", _provideColor);
            }

            public static void ShowProvideRemoveUserReport(RichTextBox rtbDoNow)
            {
                rtbDoNow.Clear();
                rtbDoNow.AppendText("Please provide User remove report.", _provideColor);
            }
        }
    }
}
