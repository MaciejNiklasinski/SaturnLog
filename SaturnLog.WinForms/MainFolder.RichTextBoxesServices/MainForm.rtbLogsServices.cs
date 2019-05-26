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
        private static class rtbLogsServices
        {
            private readonly static string _applicationStartedLogText = "Application Started.";

            private readonly static Color _logTimestampColor = Color.Gray;
            private readonly static Color _buttonPressedLogColor = Color.Gray;
            private readonly static Color _backToIdleColor = Color.Gray;

            private readonly static Color _defaultColor = Color.Black;
            private readonly static Color _failureColor = Color.Red;
            private readonly static Color _successColor = Color.Green;
            private readonly static Color _awaitDownloadColor = Color.Purple;
            private readonly static Color _awaitUploadColor = Color.DarkOrange;

            #region Common
            public static void AddApplicationStartedLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestampSameLine(rtbLogs);
                rtbLogs.AppendText(_applicationStartedLogText, _defaultColor);
            }

            public static void AddButtonPressedLog(RichTextBox rtbLogs, string text)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText(text, _buttonPressedLogColor);
            }

            public static void AddBackToIdleLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Back to idle.", _backToIdleColor);
            }

            #region Username Provided/Validation
            public static void AddUsernameValidationBeganLog(RichTextBox rtbLogs, string toBeValidateUsername)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided username: {toBeValidateUsername} began.", _defaultColor);

            }
            public static void AddUsernameValidationFailedLog(RichTextBox rtbLogs, string toBeValidateUsername)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided username: {toBeValidateUsername} failed.", _failureColor);
            }
            public static void AddUsernameValidationCanceledLog(RichTextBox rtbLogs, string toBeValidateUsername)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided username: {toBeValidateUsername} has been canceled.", _defaultColor);
            }
            public static void AddInvalidUsernameProvidedLog(RichTextBox rtbLogs, string invalidUsername)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Invalid username: {invalidUsername} has been provided.", _failureColor);
            }
            public static void AddEmptyUsernameProvidedLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Empty value has been provided as username. Username cannot be empty.", _failureColor);
            }
            public static void AddValidUsernameProvidedLog(RichTextBox rtbLogs, string username)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Valid username has been provided: {username}.", _defaultColor);
            }
            #endregion

            #region Serial Number Provided/Validation
            public static void AddSaturn5SerialNumberValidationBeganLog(RichTextBox rtbLogs, string toBeValidateSerialNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided Saturn 5 serial number: {toBeValidateSerialNumber} began.", _defaultColor);

            }
            public static void AddSaturn5SerialNumberValidationFailedLog(RichTextBox rtbLogs, string toBeValidateSerialNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided Saturn 5 serial number: {toBeValidateSerialNumber} failed.", _failureColor);
            }
            public static void AddSaturn5SerialNumberValidationCanceledLog(RichTextBox rtbLogs, string toBeValidateSerialNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided Saturn 5 serial number: {toBeValidateSerialNumber} has been canceled.", _defaultColor);
            }
            public static void AddInvalidSaturn5SerialNumberProvidedLog(RichTextBox rtbLogs, string invalidSerialNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Invalid Saturn 5 serial number: {invalidSerialNumber} has been provided.", _failureColor);
            }
            public static void AddEmptySaturn5SerialNumberProvidedLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Empty value has been provided as Saturn 5 serial number. Saturn 5 serial number cannot be empty.", _failureColor);
            }
            public static void AddValidSaturn5SerialNumberProvidedLog(RichTextBox rtbLogs, string serialNumber)
            {

                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Valid Saturn 5 serial number has been provided. Serial number: {serialNumber}", _defaultColor);
            }
            #endregion

            #region Short Id Provided/Validation
            public static void AddSaturn5ShortIdValidationBeganLog(RichTextBox rtbLogs, string toBeValidateShortId)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided Saturn 5 barcode: {toBeValidateShortId} began.", _defaultColor);

            }
            public static void AddSaturn5ShortIdValidationFailedLog(RichTextBox rtbLogs, string toBeValidateShortId)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided Saturn 5 barcode: {toBeValidateShortId} failed.", _failureColor);
            }
            public static void AddSaturn5ShortIdValidationCanceledLog(RichTextBox rtbLogs, string toBeValidateShortId)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Validation of the provided Saturn 5 barcode: {toBeValidateShortId} has been canceled.", _defaultColor);
            }
            public static void AddInvalidSaturn5ShortIdProvidedLog(RichTextBox rtbLogs, string invalidShortId)
            {

                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Invalid Saturn 5 barcode: {invalidShortId} has been provided.", _failureColor);
            }
            public static void AddEmptySaturn5ShortIdProvidedLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Empty value has been provided as Saturn 5 barcode. Saturn 5 barcode cannot be empty.", _failureColor);
            }
            public static void AddValidSaturn5ShortIdProvidedLog(RichTextBox rtbLogs, string shortId)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Valid Saturn 5 barcode has been scanned. Barcode: {shortId}", _defaultColor);
            }
            #endregion
            #endregion

            #region PreBrief Tab                        
            public static void AddAttemptingToAllocateSaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to allocate Saturn5 unit to the User using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddCancelToAllocateSaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled to allocate Saturn5 unit to the User using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddAttemptingToAllocateSaturn5ByShortIdLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to allocate Saturn5 unit to the User using its BARCODE", _defaultColor);
            }

            public static void AddCancelToAllocateSaturn5ByShortIdLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled to allocate Saturn5 unit to the User using its BARCODE", _defaultColor);
            }

            public static void AddAttemptingToEmergencyAllocateSaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to emergency allocate Saturn5 unit to the User using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddCancelToEmergencyAllocateSaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled to emergency allocate Saturn5 unit to the User using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddValidDamagedSaturn5SerialNumberOrShortIdProvidedLog(RichTextBox rtbLogs, string serialNumber, string shortId)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Serial number: {serialNumber} Barcode: {shortId} is associated with damaged Saturn5 unit.", _defaultColor);
            }
            public static void AddValidFaultySaturn5SerialNumberOrShortIdProvidedLog(RichTextBox rtbLogs, string serialNumber, string shortId)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Serial number: {serialNumber} Barcode: {shortId} is associated with faulty Saturn5 unit.", _defaultColor);
            }

            public static void AddSaturn5SuccesfullyAllocatedToUserLog(RichTextBox rtbLogs, Saturn5 saturn5, User user)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} has been successfully allocated to {user.Username} - {user.FirstName} {user.Surname}", _successColor);
            }
            public static void AddSaturn5FailToAllocatedToUserLog(RichTextBox rtbLogs, Saturn5 saturn5, User user)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} fail to get allocated to {user.Username} - {user.FirstName} {user.Surname}", _failureColor);
            }
            public static void AddSaturn5CancelToAllocatedToUserLog(RichTextBox rtbLogs, Saturn5 saturn5, User user)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId}, operation has been cancel before Saturn5 unit got allocated to {user.Username} - {user.FirstName} {user.Surname}", _failureColor);
            }
            #endregion

            #region DeBrief Tab
            public static void AddAttemptingToConfirmBackInSaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to confirm Saturn5 unit to back in depot using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddCanceledToConfirmBackInSaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled to confirm Saturn5 unit to back in depot using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddAttemptingToConfirmBackInSaturn5ByShortIdLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to confirm Saturn5 unit to back in depot using its BARCODE", _defaultColor);
            }

            public static void AddCancelToConfirmBackInSaturn5ByShortIdLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled to confirm Saturn5 unit to back in depot using its BARCODE", _defaultColor);
            }

            public static void AddSaturn5SuccesfullyConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} has been successfully confirmed back in depot. Returned by: {user.Username} - {user.FirstName} {user.Surname}", _successColor);
            }
            public static void AddSaturn5FailToConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} fail to confirmed back in depot. Returned by: {user.Username} - {user.FirstName} {user.Surname}", _failureColor);
            }
            public static void AddSaturn5CancelToConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId}, operation has been cancel before Saturn5 unit got confirmed back in depot. Not-Returned by:  {user.Username} - {user.FirstName} {user.Surname}", _failureColor);
            }

            public static void AddAttemptingToConfirmBackInFaultySaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to confirm damage Saturn5 unit to back in depot using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddCancelToConfirmBackInFaultySaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled to confirm damage Saturn5 unit to back in depot using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddFaultySaturn5SuccesfullyConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user, Saturn5Issue fault)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Faulty Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} has been successfully confirmed back in depot. Returned by: {user.Username} - {user.FirstName} {user.Surname}. Fault description: {fault.Description}", _successColor);
            }
            public static void AddFaultySaturn5FailToConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user, Saturn5Issue fault)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Faulty Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} fail to confirmed back in depot. Returned by: {user.Username} - {user.FirstName} {user.Surname}. Fault description: {fault.Description}", _failureColor);
            }
            public static void AddFaultySaturn5CancelToConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user, Saturn5Issue fault)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Faulty Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId}, operation has been cancel before Saturn5 unit got confirmed back in depot. Not-Returned by: {user.Username} - {user.FirstName} {user.Surname}. Fault description: {fault.Description}", _failureColor);
            }

            public static void AddAttemptingToConfirmBackInDamageSaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to confirm damage Saturn5 unit to back in depot using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddCancelToConfirmBackInDamageSaturn5BySerialNumberLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled to confirm damage Saturn5 unit to back in depot using its SERIAL NUMBER", _defaultColor);
            }

            public static void AddDamagedSaturn5SuccesfullyConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user, Saturn5Issue damage)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Damage Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} has been successfully confirmed back in depot. Returned by: {user.Username} - {user.FirstName} {user.Surname}. Damage description: {damage.Description}", _successColor);
            }
            public static void AddDamagedSaturn5FailToConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user, Saturn5Issue damage)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Damage Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} fail to confirmed back in depot. Returned by: {user.Username} - {user.FirstName} {user.Surname}. Damage description: {damage.Description}", _failureColor);
            }
            public static void AddDamagedSaturn5CancelToConfirmBackInLog(RichTextBox rtbLogs, Saturn5 saturn5, User user, Saturn5Issue damage)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Damage Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId}, operation has been cancel before Saturn5 unit got confirmed back in depot. Not-Returned by: {user.Username} - {user.FirstName} {user.Surname}. Damage description: {damage.Description}", _failureColor);
            }
            #endregion

            #region Options Tab
            #region Connect
            public static void AddDatabaseAttemptingToConnectLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to connect into the database.", _defaultColor);
            }

            public static void AddDatabaseSuccessfullyConnectLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Successfully connect into the database.", _successColor);
            }

            public static void AddDatabaseCancelToConnectLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempt to connect to database has been canceled, or database server request has been canceled.", _failureColor);
            }

            public static void AddDatabaseFailToConnectLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Fail to connect into the database.", _failureColor);
            }

            public static void AddDatabaseConnectionFailedLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Database connection failed.", _failureColor);
            }

            public static void AddDatabaseForcedToDisconnectLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Application has been forced to disconnect from database. This has been enforced by other instance of the application to allow it connect into the database itself.", _failureColor);
            }

            public static void AddDatabaseOngoingDataFetchLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Fetching data from the database is ongoing. Please be aware that until that is finished application might work slower.", _awaitDownloadColor);
            }

            public static void AddDatabaseDataFetchCompletedLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Fetching data from the database has been completed.", _successColor);
            }

            public static void AddDatabaseDataFetchFailedLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Fetching data from the database failed.", _failureColor);
            }

            public static void AddDatabaseDataFetchCanceledLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Fetching data from the database has been canceled.", _failureColor);
            }
            #endregion

            #region Disconnect 
            public static void AddDatabaseAttemptingToDisconnectLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to disconnect from the database.", _defaultColor);
            }
            public static void AddDisconnectSuccessfullyLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Successfully disconnected from the database.", _successColor);
            }
            public static void AddFailToDisconnectLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Failed to disconnect from the database.", _failureColor);
            }
            #endregion

            #region LogIn / LogOut
            public static void AddAttemptingToLogInLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to log in.", _defaultColor);
            }

            public static void AddLogInSuccessfullyLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Successfully logged in.", _successColor);
            }

            public static void AddLogInFailLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Fail to log in.", _failureColor);
            }

            public static void AddLogInCanceledLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Log in canceled.", _failureColor);
            }

            public static void AddAttemptingToLogOutLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to log out.", _defaultColor);
            }

            public static void AddLogOutSuccessfullyLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Successfully logged out.", _successColor);
            }

            public static void AddLogOutFailLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Fail to log out.", _failureColor);
            }
            #endregion
            #endregion

            #region Saturn 5 Stock Management            
            public static void AddAttemptingToSaturn5ReceiveFromITLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to add to the stock Saturn 5 unit received from IT.", _defaultColor);
            }

            public static void AddCancelToSaturn5ReceiveFromITLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled to add Saturn 5 unit received from IT.", _defaultColor);
            }

            public static void AddSaturn5ReceiveFromITDataUploadSucceedLog(RichTextBox rtbLogs, string serialNumber, string shortId, string phoneNumber, string consignmentNumber, string movementNote)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} ShortId/Barcode: {shortId} received from IT has been successfully added into the depot stock.", _successColor);

                if (!(consignmentNumber is null) && consignmentNumber.Trim() != "")
                    rtbLogs.AppendText($" Unit shipment consignment number: {consignmentNumber}", _successColor);

                if (!(movementNote is null) && movementNote.Trim() != "")
                    rtbLogs.AppendText($" Movement note: {movementNote}", _successColor);
            }
            public static void AddSaturn5ReceiveFromITDataUploadFailedLog(RichTextBox rtbLogs, string serialNumber, string shortId, string phoneNumber, string consignmentNumber, string movementNote)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} ShortId/Barcode: {shortId} received from IT failed to get added into the depot stock.", _failureColor);
            }
            public static void AddSaturn5ReceiveFromITDataUploadCanceledLog(RichTextBox rtbLogs, string serialNumber, string shortId, string phoneNumber, string consignmentNumber, string movementNote)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} ShortId/Barcode: {shortId} received from IT and adding into the depot stock got canceled.", _defaultColor);
            }


            public static void AddAttemptingToCreateSaturn5Log(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to add Saturn 5 unit into the depot stock.", _defaultColor);
            }

            public static void AddCancelToCreateSaturn5Log(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to add Saturn 5 unit into the depot stock.", _defaultColor);
            }

            public static void AddCreateSaturn5DataUploadSucceedLog(RichTextBox rtbLogs, string serialNumber, string shortId, string phoneNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} ShortId/Barcode: {shortId} has been successfully added into the depot stock.", _successColor);
            }
            public static void AddCreateSaturn5DataUploadFailedLog(RichTextBox rtbLogs, string serialNumber, string shortId, string phoneNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} ShortId/Barcode: {shortId} failed to get added into the depot stock.", _failureColor);
            }
            public static void AddCreateSaturn5DataUploadCanceledLog(RichTextBox rtbLogs, string serialNumber, string shortId, string phoneNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} ShortId/Barcode: {shortId} adding into the depot stock got canceled.", _defaultColor);
            }


            public static void AddAttemptingToEditSaturn5Log(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to edit Saturn 5 unit.", _defaultColor);
            }

            public static void AddCancelToEditSaturn5Log(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to edit Saturn 5 unit.", _defaultColor);
            }

            public static void AddEditSaturn5DataUploadSucceedLog(RichTextBox rtbLogs, Saturn5 toBeEdited, string newShortId, string newPhoneNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);

                if (!(newShortId is null) && toBeEdited.ShortId != newShortId.Trim().ToUpper() && !(newPhoneNumber is null) && toBeEdited.PhoneNumber != newPhoneNumber.Trim().ToUpper())
                    rtbLogs.AppendText($"Saturn5 serial number: {toBeEdited.SerialNumber} ShortId/Barcode changed from: {toBeEdited.ShortId} to: {newShortId.Trim().ToUpper()}, " +
                        $"Phone number changed from: {toBeEdited.PhoneNumber} to: {newPhoneNumber.Trim().ToUpper()}.", _successColor);

                else if (!(newShortId is null) && toBeEdited.ShortId != newShortId.Trim().ToUpper())
                    rtbLogs.AppendText($"Saturn5 serial number: {toBeEdited.SerialNumber} ShortId/Barcode changed from: {toBeEdited.ShortId} to: {newShortId.Trim().ToUpper()}.", _successColor);

                else
                    rtbLogs.AppendText($"Saturn5 serial number: {toBeEdited.SerialNumber} Phone number changed from: {toBeEdited.PhoneNumber} to: {newPhoneNumber.Trim().ToUpper()}.", _successColor);
            }
            public static void AddEditSaturn5DataUploadFailedLog(RichTextBox rtbLogs, Saturn5 toBeEdited, string newShortId, string newPhoneNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {toBeEdited.SerialNumber} edit failed.", _failureColor);
            }
            public static void AddEditSaturn5DataUploadCanceledLog(RichTextBox rtbLogs, Saturn5 toBeEdited, string newShortId, string newPhoneNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {toBeEdited.SerialNumber} canceled attempt to edit.", _defaultColor);
            }


            public static void AddAttemptingToSaturn5SendToITLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to remove Saturn 5 unit from the depot stock and send it to IT.", _defaultColor);
            }

            public static void AddCancelToSaturn5SendToITLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to remove Saturn 5 unit from the depot stock and send it to IT.", _defaultColor);
            }

            public static void AddAttemptToSentToITLastSaturn5Log(RichTextBox rtbLogs, string serialNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Serial number: {serialNumber} is associated with last Saturn5 unit in the stock and as such it cannot be sent to IT.", _failureColor);
            }

            public static void AddSaturn5SendToITDataUploadSucceedLog(RichTextBox rtbLogs, string serialNumber, string consignmentNumber, string incidentNumber, string movementNote, bool surplus)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);

                if (surplus)
                    rtbLogs.AppendText($"Surplus Saturn5 serial number: {serialNumber} unit has been successfully send to IT.", _successColor);
                else
                    rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} unit has been successfully send to IT.", _successColor);

                if (!(consignmentNumber is null) && consignmentNumber.Trim() != "")
                    rtbLogs.AppendText($" Unit shipment consignment number: {consignmentNumber}", _successColor);

                if (!(consignmentNumber is null) && consignmentNumber.Trim() != "")
                    rtbLogs.AppendText($" Associated incident number: {consignmentNumber}", _successColor);

                if (!(movementNote is null) && movementNote.Trim() != "")
                    rtbLogs.AppendText($" Movement note: {movementNote}", _successColor);
            }
            public static void AddSaturn5SendToITDataUploadFailedLog(RichTextBox rtbLogs, string serialNumber, string consignmentNumber, string incidentNumber, string movementNote, bool surplus)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} unit has been failed to get send to IT.", _failureColor);
            }
            public static void AddSaturn5SendToITDataUploadCanceledLog(RichTextBox rtbLogs, string serialNumber, string consignmentNumber, string incidentNumber, string movementNote, bool surplus)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {serialNumber} unit has been canceled to get send to IT.", _defaultColor);
            }


            public static void AddAttemptingToRemoveSaturn5Log(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to remove Saturn 5 unit from the depot stock.", _defaultColor);
            }

            public static void AddCancelToRemoveSaturn5Log(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to remove Saturn 5 unit from the depot stock.", _defaultColor);
            }

            public static void AddAttemptToRemoveLastSaturn5Log(RichTextBox rtbLogs, string serialNumber)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Serial number: {serialNumber} is associated with last Saturn5 unit in the stock and as such it cannot be removed.", _failureColor);
            }

            public static void AddRemoveSaturn5DataUploadSucceedLog(RichTextBox rtbLogs, Saturn5 toBeRemoved)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {toBeRemoved.SerialNumber} unit has been successfully removed.", _successColor);
            }
            public static void AddRemoveSaturn5DataUploadFailedLog(RichTextBox rtbLogs, Saturn5 toBeRemoved)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {toBeRemoved.SerialNumber} unit has failed to get remove.", _failureColor);
            }
            public static void AddRemoveSaturn5DataUploadCanceledLog(RichTextBox rtbLogs, Saturn5 toBeRemoved)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {toBeRemoved.SerialNumber} unit has been canceled to get removed.", _defaultColor);
            }


            public static void AddAttemptingToReportSaturn5FaultLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to report Saturn 5 fault.", _defaultColor);
            }

            public static void AddCancelToReportSaturn5FaultLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to report Saturn 5 fault.", _defaultColor);
            }

            public static void AddReportSaturn5FaultSuccesfullyLog(RichTextBox rtbLogs, Saturn5Issue faultReport)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Valid saturn 5 fault report has been provided. Serial Number: {faultReport.SerialNumber} Timestamp: {faultReport.Timestamp} Report: {faultReport.Description}.", _successColor);
            }
            public static void AddReportSaturn5FaultFailedLog(RichTextBox rtbLogs, Saturn5 saturn5, string faultDescription)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} fail to report fault: {faultDescription}", _failureColor);
            }
            public static void AddReportSaturn5FaultCanceledLog(RichTextBox rtbLogs, Saturn5 saturn5, string faultDescription)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} canceled to report fault: {faultDescription}.", _defaultColor);
            }


            public static void AddAttemptingToReportSaturn5DamageLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to report Saturn 5 damage.", _defaultColor);
            }

            public static void AddCancelToReportSaturn5DamageLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to report Saturn 5 damage.", _defaultColor);
            }

            public static void AddReportSaturn5DamageSuccesfullyLog(RichTextBox rtbLogs, Saturn5Issue damageReport)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Valid saturn 5 damage report has been provided. Serial Number: {damageReport.SerialNumber} Timestamp: {damageReport.Timestamp} Report: {damageReport.Description}.", _successColor);
            }
            public static void AddReportSaturn5DamageFailedLog(RichTextBox rtbLogs, Saturn5 saturn5, string damageDescription)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} fail to report damage: {damageDescription}", _failureColor);
            }
            public static void AddReportSaturn5DamageCanceledLog(RichTextBox rtbLogs, Saturn5 saturn5, string damageDescription)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {saturn5.SerialNumber} Barcode: {saturn5.ShortId} canceled to report damage: {damageDescription}", _defaultColor);
            }




            public static void AddAttemptingToResolveSaturn5IssueLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to resolve Saturn 5 issue in the depot.", _defaultColor);
            }

            public static void AddCancelToResolveSaturn5IssueLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to resolve Saturn 5 issue in the depot.", _defaultColor);
            }

            public static void AddResolveSaturn5IssueDataUploadSucceedLog(RichTextBox rtbLogs, Saturn5Issue issue, User resolvedBy, Saturn5IssueStatus resolvedHow, string resolvedHowDescription)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {issue.SerialNumber} unit issue has been resolved by: {resolvedBy.Username} {resolvedBy.FirstName} {resolvedBy.Surname}. Issue description: {issue.Description} Resolved how: {resolvedHowDescription}", _successColor);
            }
            public static void AddResolveSaturn5IssueDataUploadFailedLog(RichTextBox rtbLogs, Saturn5Issue issue, User resolvedBy, Saturn5IssueStatus resolvedHow, string resolvedHowDescription)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {issue.SerialNumber} unit issue failed to get resolved by: {resolvedBy.Username} {resolvedBy.FirstName} {resolvedBy.Surname}. Issue description: {issue.Description} Resolved how: {resolvedHowDescription}", _failureColor);
            }
            public static void AddResolveSaturn5IssueDataUploadCanceledLog(RichTextBox rtbLogs, Saturn5Issue issue, User resolvedBy, Saturn5IssueStatus resolvedHow, string resolvedHowDescription)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Saturn5 serial number: {issue.SerialNumber} unit issue canceled to get resolved by: {resolvedBy.Username} {resolvedBy.FirstName} {resolvedBy.Surname}. Issue description: {issue.Description} Resolved how: {resolvedHowDescription}", _defaultColor);
            }
            #endregion

            #region Admin
            public static void AddAttemptingToCreateUserLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to add User into the depot user list.", _defaultColor);
            }

            public static void AddCancelToCreateUserLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to add User into the depot user list.", _defaultColor);
            }

            public static void AddCreateUserDataUploadSucceedLog(RichTextBox rtbLogs, string username, string firstName, string surname, UserType userType)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"User username: {username} {firstName} {surname} type: {userType.GetDisplayableString()} has been successfully added into the depot user list.", _successColor);
            }
            public static void AddCreateUserDataUploadFailedLog(RichTextBox rtbLogs, string username, string firstName, string surname, UserType userType)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"User username: {username} {firstName} {surname} type: {userType.GetDisplayableString()} has failed to get added into the depot user list.", _failureColor);
            }
            public static void AddCreateUserDataUploadCanceledLog(RichTextBox rtbLogs, string username, string firstName, string surname, UserType userType)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"User username: {username} {firstName} {surname} type: {userType.GetDisplayableString()} has canceled to get added into the depot user list.", _defaultColor);
            }


            public static void AddAttemptingToEditUserLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to edit a User.", _defaultColor);
            }

            public static void AddCancelToEditUserLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to edit a User.", _defaultColor);
            }

            public static void AddAttemptToEditLoggedInUserLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Unable to edit currently logged in user.", _failureColor);
            }

            public static void AddEditUserDataUploadSucceedLog(RichTextBox rtbLogs, User toBeEdited, string newFirstName, string newSurname, UserType? newUserType)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);

                if (!(newFirstName is null) && toBeEdited.FirstName != newFirstName.Trim().ToUpperFirstCharOnly()
                    && !(newSurname is null) && toBeEdited.Surname != newSurname.Trim().ToUpperFirstCharOnly()
                    && !(newUserType is null) && toBeEdited.Type != newUserType)
                    rtbLogs.AppendText($"User username: {toBeEdited.Username} first name changed from: {toBeEdited.FirstName} to: {newFirstName.Trim().ToUpperFirstCharOnly()}, " +
                        $"surname changed from: {toBeEdited.Surname} to: {newSurname.Trim().ToUpperFirstCharOnly()}." +
                        $"user type changed from: {toBeEdited.Type.GetDisplayableString()} to: {newUserType?.GetDisplayableString()}.", _successColor);

                else if (!(newFirstName is null) && toBeEdited.FirstName != newFirstName.Trim().ToUpperFirstCharOnly()
                    && !(newSurname is null) && toBeEdited.Surname != newSurname.Trim().ToUpperFirstCharOnly())
                    rtbLogs.AppendText($"User username: {toBeEdited.Username} first name changed from: {toBeEdited.FirstName} to: {newFirstName.Trim().ToUpperFirstCharOnly()}, " +
                        $"surname changed from: {toBeEdited.Surname} to: {newSurname.Trim().ToUpperFirstCharOnly()}.", _successColor);

                else if (!(newFirstName is null) && toBeEdited.FirstName != newFirstName.Trim().ToUpperFirstCharOnly()
                    && !(newUserType is null) && toBeEdited.Type != newUserType)
                    rtbLogs.AppendText($"User username: {toBeEdited.Username} first name changed from: {toBeEdited.FirstName} to: {newFirstName.Trim().ToUpperFirstCharOnly()}, " +
                        $"user type changed from: {toBeEdited.Type.GetDisplayableString()} to: {newUserType?.GetDisplayableString()}.", _successColor);

                else if (!(newSurname is null) && toBeEdited.Surname != newSurname.Trim().ToUpperFirstCharOnly()
                    && !(newUserType is null) && toBeEdited.Type != newUserType)
                    rtbLogs.AppendText($"User username: {toBeEdited.Username} surname changed from: {toBeEdited.Surname} to: {newSurname.Trim().ToUpperFirstCharOnly()}, " +
                        $"user type changed from: {toBeEdited.Type.GetDisplayableString()} to: {newUserType?.GetDisplayableString()}.", _successColor);


                else if (!(newFirstName is null) && toBeEdited.FirstName != newFirstName.Trim().ToUpperFirstCharOnly())
                    rtbLogs.AppendText($"User username: {toBeEdited.Username} first name changed from: {toBeEdited.FirstName} to: {newFirstName.Trim()}.", _successColor);

                else if (!(newSurname is null) && toBeEdited.Surname != newSurname.Trim().ToUpperFirstCharOnly())
                    rtbLogs.AppendText($"User username: {toBeEdited.Username} surname changed from: {toBeEdited.Surname} to: {newSurname.Trim()}.", _successColor);

                else
                    rtbLogs.AppendText($"User username: {toBeEdited.Username} user type changed from: {toBeEdited.Type.GetDisplayableString()} to: {newUserType?.GetDisplayableString()}.", _successColor);

            }
            public static void AddEditUserDataUploadFailedLog(RichTextBox rtbLogs, User toBeEdited, string newFirstName, string newSurname, UserType? newUserType)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"User username: {toBeEdited.Username} edit failed.", _failureColor);
            }
            public static void AddEditUserDataUploadCanceledLog(RichTextBox rtbLogs, User toBeEdited, string newFirstName, string newSurname, UserType? newUserType)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"User username: {toBeEdited.Username} canceled attempt to edit.", _defaultColor);
            }



            public static void AddAttemptingToRemoveUserLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Attempting to remove User from the depot users list.", _defaultColor);
            }

            public static void AddCancelToRemoveUserLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText("Canceled attempt to remove User from the depot users list.", _defaultColor);
            }

            public static void AddAttemptToRemoveLoggedInUserLog(RichTextBox rtbLogs)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"Unable to remove currently logged in user.", _failureColor);
            }

            public static void AddRemoveUserDataUploadSucceedLog(RichTextBox rtbLogs, User toBeRemoved)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"User username: {toBeRemoved.Username} has been successfully removed.", _successColor);
            }
            public static void AddRemoveUserDataUploadFailedLog(RichTextBox rtbLogs, User toBeRemoved)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"User username: {toBeRemoved.Username} unit has failed to get remove.", _failureColor);
            }
            public static void AddRemoveUserDataUploadCanceledLog(RichTextBox rtbLogs, User toBeRemoved)
            {
                rtbLogsServices.AppendTimestamp(rtbLogs);
                rtbLogs.AppendText($"User username: {toBeRemoved.Username} unit has been canceled to get removed.", _defaultColor);
            }
            #endregion

            #region Private Helpers
            private static void AppendTimestamp(RichTextBox rtbLogs)
            {
                DateTime now = DateTime.Now;
                rtbLogs.AppendLog($"{now.Hour:D2}:{now.Minute:D2}:{now.Second:D2} ", _logTimestampColor);
            }

            private static void AppendTimestampSameLine(RichTextBox rtbLogs)
            {
                DateTime now = DateTime.Now;
                rtbLogs.AppendText($"{now.Hour:D2}:{now.Minute:D2}:{now.Second:D2} ", _logTimestampColor);
            }
            #endregion
        }
    }
}
