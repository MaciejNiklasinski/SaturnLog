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

        private static class rtbDBStatusServices
        {
            private readonly static Color _notConnectedColor = Color.Red;
            private readonly static string _notConnectedText = "NOT CONNECTED.";

            private readonly static Color _databaseForcedToDisconnectColor = Color.Red;
            private readonly static string _databaseForcedToDisconnectText = "DATABASE FORCED TO DISCONNECT. PLEASE WAIT.";

            private readonly static Color _databaseFailureColor = Color.Red;
            private readonly static string _databaseFailureText = "DATABASE FAILURE.";


            private readonly static Color _connectingNoWaitTimeColor = Color.Purple;
            private readonly static string _connectingNoWaitTimeText = "ATTEMPTING TO CONNECT. PLEASE WAIT.";

            private readonly static Color _connectingWithWaitTimeColor = Color.Purple;
            private readonly static string _connectingWithWaitTimeText = "ATTEMPTING TO CONNECT. PLEASE WAIT ";

            private readonly static Color _takingOverColor = Color.Purple;
            private readonly static string _takingOverText = "ATTEMPTING TO TAKE OVER DATABASE. PLEASE WAIT ";

            private readonly static Color _disconnectingColor = Color.Red;
            private readonly static string _disconnectingText = "ATTEMPTING TO DISCONNECT. PLEASE WAIT.";



            private readonly static Color _connectedOngoingDataFetchColor = Color.Purple;
            private readonly static string _connectedOngoingDataFetchText = "LOW PERFORMANCE - ONGING DATA FETCH.";
            
            private readonly static Color _connectedDataFetchCanceledColor = Color.Brown;
            private readonly static string _connectedDataFetchCanceledText = "LOW PERFORMANCE - DATA FETCH NOT CANCELED.";

            private readonly static Color _connectedDataFetchFailedColor = Color.Brown;
            private readonly static string _connectedDataFetchFailedText = "LOW PERFORMANCE - DATA FETCH NOT FAILED.";

            private readonly static Color _connectedDataFetchCompletedColor = Color.Green;
            private readonly static string _connectedDataFetchCompletedText = "FULL PERFORMANCE - DATA FETCH COMPLETED.";


            public static void ShowDisconnected(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_notConnectedText, _notConnectedColor);
            }

            public static void ShowDatabaseForcedToDisconnectFailure(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_databaseForcedToDisconnectText, _databaseForcedToDisconnectColor);
            }

            public static void ShowDatabaseFailure(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_databaseFailureText, _databaseFailureColor);
            }

            public static void ShowConnecting(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_connectingNoWaitTimeText, _connectingNoWaitTimeColor);
            }

            public static void ShowConnecting(RichTextBox rtbDBStatus, int waitSec)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_connectingWithWaitTimeText + $"{waitSec} SEC.", _connectingWithWaitTimeColor);
            }

            public static void ShowTakingOver(RichTextBox rtbDBStatus, int waitSec)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_takingOverText + $"{waitSec} SEC.", _takingOverColor);
            }

            public static void ShowDisconnecting(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_disconnectingText, _disconnectingColor);
            }

            public static void ShowOngoingDataFetch(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_connectedOngoingDataFetchText, _connectedOngoingDataFetchColor);
            }

            public static void ShowDataFetchCompletedInitial(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_connectedDataFetchCompletedText, _connectedDataFetchCompletedColor);
            }

            public static void ShowDataFetchCanceledCompleted(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_connectedDataFetchCanceledText, _connectedDataFetchCanceledColor);
            }

            public static void ShowDataFetchFailedCompleted(RichTextBox rtbDBStatus)
            {
                rtbDBStatus.Clear();
                rtbDBStatus.AppendText(_connectedDataFetchFailedText, _connectedDataFetchFailedColor);
            }
        }
    }
}
