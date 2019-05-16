using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
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
        private class ConsolesServices
        {
            #region Private Fields And Properties
            private MainForm _form;

            private App _app { get { return this._form._app; } }

            #region Consoles TextBoxes
            private RichTextBox rtbLogs { get { return this._form.rtbLogs; } }
            private RichTextBox rtbCurrently { get { return this._form.rtbCurrently; } }
            private RichTextBox rtbDoNow { get { return this._form.rtbDoNow; } }
            private RichTextBox rtbDBStatus { get { return this._form.rtbDBStatus; } }
            private RichTextBox rtbLoggedUser { get { return this._form.rtbLoggedUser; } }
            #endregion
            #endregion

            #region Constructor
            public ConsolesServices(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            #region Common
            public void OnApplicationStarted(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnApplicationStarted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLoggedUserServices.ShowPleaseConnectAndLogIn(this.rtbLoggedUser);
                    rtbLogsServices.AddApplicationStartedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingToConnectToDatabase(this.rtbCurrently);
                    rtbDoNowServices.ShowConnectToDatabase(this.rtbDoNow);
                }
            }

            public void OnCancelButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCancelButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Cancel button pressed.");
                }
            }

            public void OnBackToIdle(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnBackToIdle);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddBackToIdleLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region Retrieving Data
            #region Retrieving User Data
            public void OnRetrievingUserDataBegan(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRetrievingUserDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnRetrievingUserDataFailed(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRetrievingUserDataFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnRetrievingUserDataCanceled(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRetrievingUserDataCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnRetrievingUserDataCompleted(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRetrievingUserDataCompleted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            #region Retrieving Saturn5 Data
            #region Retrieving Saturn5 Data By Serial Number
            public void OnRetrievingSaturn5DataBySerialNumberBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRetrievingSaturn5DataBySerialNumberBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingSaturn5Data(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnRetrievingSaturn5DataBySerialNumberFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRetrievingSaturn5DataBySerialNumberFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnRetrievingSaturn5DataBySerialNumberCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRetrievingSaturn5DataBySerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            #region Retrieving Saturn5 Data By Short Id
            public void OnRetrievingSaturn5DataByShortIdBegan(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnRetrievingSaturn5DataByShortIdBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingSaturn5Data(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnRetrievingSaturn5DataByShortIdFailed(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnRetrievingSaturn5DataByShortIdFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnRetrievingSaturn5DataByShortIdCanceled(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnRetrievingSaturn5DataByShortIdCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            public void OnRetrievingSaturn5DataCompleted(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRetrievingSaturn5DataCompleted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            public void OnRetrievingSaturn5WithInvalidLastUserDataCompleted(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRetrievingSaturn5WithInvalidLastUserDataCompleted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnRetrievingUserAndSaturn5DataCompleted(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnRetrievingUserAndSaturn5DataCompleted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowRetrievingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            #region Uploading Data
            #region Uploading User Data
            public void OnUploadingUserDataBegan(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnUploadingUserDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            #region Uploading Saturn 5Data
            public void OnUploadingSaturn5DataBegan(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnUploadingSaturn5DataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5Data(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            #region Uploading Saturn5 Allocation to User Data
            public void OnUploadingSaturn5AllocationDataBegan(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnUploadingSaturn5AllocationDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5AllocationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            #region Uploading Saturn5 Confirmed Back In Depot
            public void OnUploadingSaturn5ConfirmedBackInDataBegan(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnUploadingSaturn5ConfirmedBackInDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5ConfirmationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            #region Uploading Saturn5 Confirmed Back In Depot Damaged
            public void OnUploadingDamagedSaturn5ConfirmedBackInDataBegan(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnUploadingDamagedSaturn5ConfirmedBackInDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingDamagedSaturn5ConfirmationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }
            #endregion

            #region Uploading Saturn5 Confirmed Back In Depot Faulty
            public void OnUploadingFaultySaturn5ConfirmedBackInDataBegan(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnUploadingFaultySaturn5ConfirmedBackInDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingFaultySaturn5ConfirmationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }
            #endregion
            #endregion

            #region Pre-Brief Tab
            #region AllocateSaturn5BySerialNumber
            public void OnAllocateSaturn5BySerialNumber_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Allocate Saturn5 unit to User by SERIAL NUMBER button pressed.");
                }
            }

            public void OnAllocateSaturn5BySerialNumber_AwaitingUsername(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_AwaitingUsername);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToAllocateSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_AwaitingUsernameCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_AwaitingUsernameCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToAllocateSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationBegan(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationBeganLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowValidatingUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationFailedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationCanceledLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5BySerialNumber_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_InvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5BySerialNumber_InvalidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_EmptyUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_EmptyUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptyUsernameProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToAllocateSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ValidSerialNumberProvided(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnAllocateSaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_InvalidSerialNumberProvided(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnAllocateSaturn5BySerialNumber_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_EmptySerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_UploadingSaturn5AllocationDataBegan(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnAllocateSaturn5BySerialNumber_UploadingSaturn5AllocationDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5AllocationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_Succeed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnAllocateSaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SuccesfullyAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_AttemptToAllocateDamagedSaturn5(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnAllocateSaturn5BySerialNumber_AttemptToAllocateDamagedSaturn5);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidDamagedSaturn5SerialNumberOrShortIdProvidedLog(this.rtbLogs, e.Saturn5.SerialNumber, e.Saturn5.ShortId);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_AttemptToAllocateFaultySaturn5(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnAllocateSaturn5BySerialNumber_AttemptToAllocateFaultySaturn5);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidFaultySaturn5SerialNumberOrShortIdProvidedLog(this.rtbLogs, e.Saturn5.SerialNumber, e.Saturn5.ShortId);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_Failed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnAllocateSaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5FailToAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5BySerialNumber_Canceled(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnAllocateSaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5CancelToAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region AllocateSaturn5ByShortId
            public void OnAllocateSaturn5ByShortId_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Allocate Saturn5 unit to User by BARCODE button pressed.");
                }
            }

            public void OnAllocateSaturn5ByShortId_AwaitingUsername(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_AwaitingUsername);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToAllocateSaturn5ByShortIdLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_AwaitingUsernameCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_AwaitingUsernameCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToAllocateSaturn5ByShortIdLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_ProvidedUsernameValidationBegan(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5ByShortId_ProvidedUsernameValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationBeganLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowValidatingUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_ProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5ByShortId_ProvidedUsernameValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationFailedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_ProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5ByShortId_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationCanceledLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_ValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5ByShortId_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_InvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnAllocateSaturn5ByShortId_InvalidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_EmptyUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_EmptyUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptyUsernameProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5ShortId(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_AwaitingShortId(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_AwaitingShortId);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5ShortId(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_AwaitingShortIdCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_AwaitingShortIdCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToAllocateSaturn5ByShortIdLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_ProvidedShortIdValidationBegan(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5ShortIdEventArgs> d = new Action<object, UserWithSaturn5ShortIdEventArgs>(OnAllocateSaturn5ByShortId_ProvidedShortIdValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ShortIdValidationBeganLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.ShowValidatingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_ProvidedShortIdValidationFailed(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5ShortIdEventArgs> d = new Action<object, UserWithSaturn5ShortIdEventArgs>(OnAllocateSaturn5ByShortId_ProvidedShortIdValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ShortIdValidationFailedLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_ProvidedShortIdValidationCanceled(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5ShortIdEventArgs> d = new Action<object, UserWithSaturn5ShortIdEventArgs>(OnAllocateSaturn5ByShortId_ProvidedShortIdValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ShortIdValidationCanceledLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_ValidShortIdProvided(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnAllocateSaturn5ByShortId_ValidShortIdProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5ShortIdProvidedLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_InvalidShortIdProvided(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5ShortIdEventArgs> d = new Action<object, UserWithSaturn5ShortIdEventArgs>(OnAllocateSaturn5ByShortId_InvalidShortIdProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5ShortIdProvidedLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.ShowValidatingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_EmptyShortIdProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_EmptyShortIdProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5ShortIdProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowValidatingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_UploadingSaturn5AllocationDataBegan(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnAllocateSaturn5ByShortId_UploadingSaturn5AllocationDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5AllocationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_Succeed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnAllocateSaturn5ByShortId_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SuccesfullyAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_AttemptToAllocateDamagedSaturn5(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnAllocateSaturn5ByShortId_AttemptToAllocateDamagedSaturn5);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidDamagedSaturn5SerialNumberOrShortIdProvidedLog(this.rtbLogs, e.Saturn5.SerialNumber, e.Saturn5.ShortId);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_AttemptToAllocateFaultySaturn5(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnAllocateSaturn5ByShortId_AttemptToAllocateFaultySaturn5);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidDamagedSaturn5SerialNumberOrShortIdProvidedLog(this.rtbLogs, e.Saturn5.SerialNumber, e.Saturn5.ShortId);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_Failed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnAllocateSaturn5ByShortId_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5FailToAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnAllocateSaturn5ByShortId_Canceled(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnAllocateSaturn5ByShortId_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5CancelToAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region EmergencyAllocateSaturn5BySerialNumber
            public void OnEmergencyAllocateSaturn5BySerialNumber_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Emergency allocate Saturn5 unit to User by SERIAL NUMBER button pressed.");
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_AwaitingUsername(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_AwaitingUsername);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToAllocateSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_AwaitingUsernameCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_AwaitingUsernameCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToEmergencyAllocateSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            
            public void OnEmergencyAllocateSaturn5BySerialNumber_ProvidedUsernameValidationBegan(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ProvidedUsernameValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationBeganLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowValidatingUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ProvidedUsernameValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationFailedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationCanceledLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_InvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_InvalidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowValidatingUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_EmptyUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_EmptyUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptyUsernameProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowValidatingUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToEmergencyAllocateSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
             
            public void OnEmergencyAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ValidSerialNumberProvided(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_InvalidSerialNumberProvided(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5SerialNumberEventArgs> d = new Action<object, UserWithSaturn5SerialNumberEventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_EmptySerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_UploadingSaturn5AllocationDataBegan(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_UploadingSaturn5AllocationDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5AllocationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_Succeed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SuccesfullyAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_Failed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5FailToAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_Canceled(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5CancelToAllocatedToUserLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion
            #endregion

            #region De-Brief Tab
            #region ConfirmBackInSaturn5BySerialNumber
            public void OnConfirmBackInSaturn5BySerialNumber_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Confirm Saturn5 unit back in Depot by SERIAL NUMBER button pressed.");
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToConfirmBackInSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCanceledToConfirmBackInSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            
            public void OnConfirmBackInSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInSaturn5BySerialNumber_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInSaturn5BySerialNumber_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInSaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_UploadingSaturn5ConfirmationDataBegan(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnConfirmBackInSaturn5BySerialNumber_UploadingSaturn5ConfirmationDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5ConfirmationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_Succeed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnConfirmBackInSaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SuccesfullyConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_Failed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnConfirmBackInSaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5FailToConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_Canceled(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnConfirmBackInSaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5CancelToConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region ConfirmSaturn5BackIByShortId
            public void OnConfirmBackInSaturn5ByShortId_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Confirm Saturn5 unit back in Depot by BARCODE button pressed.");
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_AwaitingShortId(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_AwaitingShortId);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToConfirmBackInSaturn5ByShortIdLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5ShortId(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_AwaitingShortIdCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_AwaitingShortIdCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToConfirmBackInSaturn5ByShortIdLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_ProvidedShortIdValidationBegan(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnConfirmBackInSaturn5ByShortId_ProvidedShortIdValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ShortIdValidationBeganLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.ShowValidatingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_ProvidedShortIdValidationFailed(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnConfirmBackInSaturn5ByShortId_ProvidedShortIdValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ShortIdValidationFailedLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_ProvidedShortIdValidationCanceled(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnConfirmBackInSaturn5ByShortId_ProvidedShortIdValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ShortIdValidationCanceledLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_EmptyShortIdProvided(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnConfirmBackInSaturn5ByShortId_EmptyShortIdProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5ShortIdProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5ShortId(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_InvalidShortIdProvided(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnConfirmBackInSaturn5ByShortId_InvalidShortIdProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5ShortIdProvidedLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.ShowAwaitingSaturn5ShortId(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5ShortId(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_ValidShortIdProvided(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnConfirmBackInSaturn5ByShortId_ValidShortIdProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5ShortIdProvidedLog(this.rtbLogs, e.ShortId);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_UploadingSaturn5ConfirmationDataBegan(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnConfirmBackInSaturn5ByShortId_UploadingSaturn5ConfirmationDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5ConfirmationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_Succeed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnConfirmBackInSaturn5ByShortId_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SuccesfullyConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_Failed(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnConfirmBackInSaturn5ByShortId_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5FailToConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_Canceled(object sender, UserWithSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5EventArgs> d = new Action<object, UserWithSaturn5EventArgs>(OnConfirmBackInSaturn5ByShortId_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5CancelToConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region ConfirmFaultySaturn5BackInDepot
            public void OnConfirmBackInFaultySaturn5BySerialNumber_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Confirm Faulty Saturn5 unit back in Depot by SERIAL NUMBER button pressed.");
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToConfirmBackInFaultySaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCanceledToConfirmBackInSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbLogsServices.AddCancelToConfirmBackInFaultySaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_AwaitingFaultReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_AwaitingFaultReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingSaturn5FaultReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideSaturn5FaultReport(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_AwaitingFaultReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_AwaitingFaultReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToConfirmBackInFaultySaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataBegan(object sender, UserWithSaturn5FaultReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5FaultReportEventArgs> d = new Action<object, UserWithSaturn5FaultReportEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5FaultData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataSucceed(object sender, UserWithFaultySaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithFaultySaturn5EventArgs> d = new Action<object, UserWithFaultySaturn5EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5FaultSuccesfullyLog(this.rtbLogs, e.Saturn5Fault);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataFailed(object sender, UserWithSaturn5FaultReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5FaultReportEventArgs> d = new Action<object, UserWithSaturn5FaultReportEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5FaultFailedLog(this.rtbLogs, e.Saturn5, e.Description);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataCanceled(object sender, UserWithSaturn5FaultReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5FaultReportEventArgs> d = new Action<object, UserWithSaturn5FaultReportEventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5FaultCanceledLog(this.rtbLogs, e.Saturn5, e.Description);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ConfirmationDataBegan(object sender, UserWithFaultySaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithFaultySaturn5EventArgs> d = new Action<object, UserWithFaultySaturn5EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ConfirmationDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingFaultySaturn5ConfirmationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_Succeed(object sender, UserWithFaultySaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithFaultySaturn5EventArgs> d = new Action<object, UserWithFaultySaturn5EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddFaultySaturn5SuccesfullyConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User, e.Saturn5Fault);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_Failed(object sender, UserWithFaultySaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithFaultySaturn5EventArgs> d = new Action<object, UserWithFaultySaturn5EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddFaultySaturn5FailToConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User, e.Saturn5Fault);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_Canceled(object sender, UserWithFaultySaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithFaultySaturn5EventArgs> d = new Action<object, UserWithFaultySaturn5EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddFaultySaturn5CancelToConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User, e.Saturn5Fault);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region ConfirmDamagedSaturn5BackInDepot
            public void OnConfirmBackInDamageSaturn5BySerialNumber_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Confirm Damaged Saturn5 unit back in Depot by SERIAL NUMBER button pressed.");
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToConfirmBackInDamageSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToConfirmBackInDamageSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingDamageReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingDamageReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingSaturn5DamageReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideSaturn5DamageReport(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingDamageReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingDamageReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToConfirmBackInDamageSaturn5BySerialNumberLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataBegan(object sender, UserWithSaturn5DamageReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5DamageReportEventArgs> d = new Action<object, UserWithSaturn5DamageReportEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5DamageData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataSucceed(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithDamagedSaturn5EventArgs> d = new Action<object, UserWithDamagedSaturn5EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5DamageSuccesfullyLog(this.rtbLogs, e.Saturn5Damage);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataFailed(object sender, UserWithSaturn5DamageReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5DamageReportEventArgs> d = new Action<object, UserWithSaturn5DamageReportEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5DamageFailedLog(this.rtbLogs, e.Saturn5, e.Description);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataCanceled(object sender, UserWithSaturn5DamageReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithSaturn5DamageReportEventArgs> d = new Action<object, UserWithSaturn5DamageReportEventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5DamageCanceledLog(this.rtbLogs, e.Saturn5, e.Description);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ConfirmationDataBegan(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithDamagedSaturn5EventArgs> d = new Action<object, UserWithDamagedSaturn5EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ConfirmationDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingDamagedSaturn5ConfirmationData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_Succeed(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithDamagedSaturn5EventArgs> d = new Action<object, UserWithDamagedSaturn5EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddDamagedSaturn5SuccesfullyConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User, e.Saturn5Damage);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_Failed(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithDamagedSaturn5EventArgs> d = new Action<object, UserWithDamagedSaturn5EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddDamagedSaturn5FailToConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User, e.Saturn5Damage);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_Canceled(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserWithDamagedSaturn5EventArgs> d = new Action<object, UserWithDamagedSaturn5EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddDamagedSaturn5CancelToConfirmBackInLog(this.rtbLogs, e.Saturn5, e.User, e.Saturn5Damage);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion
            #endregion

            #region Options Tab
            #region Connect
            public void OnConnect_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Connect button pressed.");
                }
            }

            public void OnConnect_Requested(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_Requested);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowConnecting(this.rtbDBStatus);
                    rtbLoggedUserServices.ShowPleaseLogIn(rtbLoggedUser);
                    rtbLogsServices.AddDatabaseAttemptingToConnectLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAttemptingToConnectToDatabase(this.rtbCurrently);
                    rtbDoNowServices.ShowConnectingDataWait(this.rtbDoNow);
                }
            }

            public void OnConnect_DBConnectionAvailabilityCountdown(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_DBConnectionAvailabilityCountdown);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowConnecting(this.rtbDBStatus, this._app.DBConnectionAvailabilityCountdown.GetValueOrDefault());
                    rtbLoggedUserServices.ShowPleaseLogIn(rtbLoggedUser);
                }
            }
            public void OnConnect_Operational(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_Operational);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowOngoingDataFetch(this.rtbDBStatus);
                    rtbLogsServices.AddDatabaseSuccessfullyConnectLog(this.rtbLogs);
                    rtbLogsServices.AddDatabaseOngoingDataFetchLog(this.rtbLogs);
                    rtbLoggedUserServices.ShowPleaseLogIn(this.rtbLoggedUser);
                    rtbCurrentlyServices.ShowAwaitingToLogIn(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConnect_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDisconnected(this.rtbDBStatus);
                    rtbLoggedUserServices.ShowPleaseConnectAndLogIn(this.rtbLoggedUser);
                    rtbLogsServices.AddDatabaseCancelToConnectLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingToConnectToDatabase(this.rtbCurrently);
                    rtbDoNowServices.ShowConnectToDatabase(this.rtbDoNow);
                }
            }

            public void OnConnect_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDatabaseFailure(this.rtbDBStatus);
                    rtbLoggedUserServices.ShowPleaseConnectAndLogIn(this.rtbLoggedUser);
                    rtbLogsServices.AddDatabaseFailToConnectLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingToConnectToDatabase(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConnect_ConnectionFailed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_ConnectionFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDatabaseFailure(this.rtbDBStatus);
                    rtbLogsServices.AddDatabaseConnectionFailedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingToConnectToDatabase(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConnect_ForcedToDisconnect(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_ForcedToDisconnect);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLoggedUserServices.ShowForcedToLogOut(this.rtbLoggedUser);
                    rtbDBStatusServices.ShowDatabaseForcedToDisconnectFailure(this.rtbDBStatus);
                    rtbLogsServices.AddDatabaseForcedToDisconnectLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingToConnectToDatabase(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConnect_DataFetchCompleted(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_DataFetchCompleted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDataFetchCompletedInitial(this.rtbDBStatus);
                    rtbLogsServices.AddDatabaseDataFetchCompletedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConnect_DataFetchCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_DataFetchCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDataFetchCanceledCompleted(this.rtbDBStatus);
                    rtbLogsServices.AddDatabaseDataFetchCanceledLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnConnect_DataFetchFailed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConnect_DataFetchFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDataFetchFailedCompleted(this.rtbDBStatus);
                    rtbLogsServices.AddDatabaseDataFetchFailedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region Disconnect
            public void OnDisconnect_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnDisconnect_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Disconnect button pressed.");
                }
            }

            public void OnDisconnect_Requested(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnDisconnect_Requested);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDisconnecting(this.rtbDBStatus);
                    rtbLogsServices.AddDatabaseAttemptingToDisconnectLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAttemptingToDisconnectFromDatabase(this.rtbCurrently);
                    rtbDoNowServices.ShowDisconnectingDataWait(this.rtbDoNow);
                }
            }

            public void OnDisconnect_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnDisconnect_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDisconnected(this.rtbDBStatus);
                    rtbLoggedUserServices.ShowPleaseConnectAndLogIn(this.rtbLoggedUser);
                    rtbLogsServices.AddDisconnectSuccessfullyLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingToConnectToDatabase(this.rtbCurrently);
                    rtbDoNowServices.ShowConnectToDatabase(this.rtbDoNow);
                }
            }

            public void OnDisconnect_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnDisconnect_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbDBStatusServices.ShowDatabaseFailure(this.rtbDBStatus);
                    rtbLogsServices.AddFailToDisconnectLog(this.rtbLogs);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region LogIn
            public void OnLogIn_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "'Log In' button pressed.");
                }
            }

            public void OnLogIn_AwaitingUsername(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_AwaitingUsername);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToLogInLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnLogIn_AwaitingUsernameCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_AwaitingUsernameCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddLogInCanceledLog(this.rtbLogs);
                    rtbLoggedUserServices.ShowPleaseLogIn(this.rtbLoggedUser);
                    rtbCurrentlyServices.ShowAwaitingToLogIn(this.rtbCurrently);
                    rtbDoNowServices.ShowLogIn(this.rtbDoNow);
                }
            }

            public void OnLogIn_ProvidedUsernameValidationBegan(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnLogIn_ProvidedUsernameValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationBeganLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowValidatingUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnLogIn_ProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnLogIn_ProvidedUsernameValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationFailedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnLogIn_ProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnLogIn_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationCanceledLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnLogIn_ValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnLogIn_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnLogIn_InvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnLogIn_InvalidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnLogIn_EmptyUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_EmptyUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptyUsernameProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnLogIn_Successfully(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnLogIn_Successfully);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddLogInSuccessfullyLog(this.rtbLogs);
                    rtbLoggedUserServices.ShowLoggedUserLabel(this.rtbLoggedUser, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnLogIn_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddLogInFailLog(this.rtbLogs);
                    rtbLoggedUserServices.ShowPleaseLogIn(this.rtbLoggedUser);
                    rtbCurrentlyServices.ShowAwaitingToLogIn(this.rtbCurrently);
                    rtbDoNowServices.ShowLogIn(this.rtbDoNow);

                }
            }

            public void OnLogIn_Cancel(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_Cancel);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddLogInCanceledLog(this.rtbLogs);
                    rtbLoggedUserServices.ShowPleaseLogIn(this.rtbLoggedUser);
                    rtbCurrentlyServices.ShowAwaitingToLogIn(this.rtbCurrently);
                    rtbDoNowServices.ShowLogIn(this.rtbDoNow);
                }
            }
            #endregion
            
            #region LogOut
            public void OnLogOut_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogOut_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Log out button pressed.");
                    rtbLogsServices.AddAttemptingToLogOutLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAttemptingToLogOut(this.rtbCurrently);
                    rtbDoNowServices.ShowRetrievingDataWait(this.rtbDoNow);
                }
            }

            public void OnLogOut_Successfully(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogOut_Successfully);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddLogOutSuccessfullyLog(this.rtbLogs);
                    rtbLoggedUserServices.ShowPleaseLogIn(this.rtbLoggedUser);
                    rtbCurrentlyServices.ShowAwaitingToLogIn(this.rtbCurrently);
                    rtbDoNowServices.ShowLogIn(this.rtbDoNow);
                }
            }
            #endregion
            #endregion

            #region Saturn5StockManagmentTab
            #region Saturn5ReceiveFromIT
            public void OnSaturn5ReceiveFromIT_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5ReceiveFromIT_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Create - Receive from IT button pressed.");
                }
            }

            public void OnSaturn5ReceiveFromIT_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5ReceiveFromIT_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToSaturn5ReceiveFromITLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5ReceiveFromITReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideSaturn5ReceiveFromITReport(this.rtbDoNow);
                }
            }

            public void OnSaturn5ReceiveFromIT_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5ReceiveFromIT_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToSaturn5ReceiveFromITLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5ReceiveFromIT_UploadingReportDataBegan(object sender, Saturn5ReceiveFromITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ReceiveFromITEventArgs> d = new Action<object, Saturn5ReceiveFromITEventArgs>(OnSaturn5ReceiveFromIT_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5ReceiveFromITData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnSaturn5ReceiveFromIT_ReportDataUploadSucceed(object sender, Saturn5ReceiveFromITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ReceiveFromITEventArgs> d = new Action<object, Saturn5ReceiveFromITEventArgs>(OnSaturn5ReceiveFromIT_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ReceiveFromITDataUploadSucceedLog(this.rtbLogs, e.SerialNumber, e.ShortId, e.PhoneNumber, e.ConsignmentNumber, e.MovementNote);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5ReceiveFromIT_ReportDataUploadFailed(object sender, Saturn5ReceiveFromITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ReceiveFromITEventArgs> d = new Action<object, Saturn5ReceiveFromITEventArgs>(OnSaturn5ReceiveFromIT_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ReceiveFromITDataUploadFailedLog(this.rtbLogs, e.SerialNumber, e.ShortId, e.PhoneNumber, e.ConsignmentNumber, e.MovementNote);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5ReceiveFromIT_ReportDataUploadCanceled(object sender, Saturn5ReceiveFromITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ReceiveFromITEventArgs> d = new Action<object, Saturn5ReceiveFromITEventArgs>(OnSaturn5ReceiveFromIT_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5ReceiveFromITDataUploadCanceledLog(this.rtbLogs, e.SerialNumber, e.ShortId, e.PhoneNumber, e.ConsignmentNumber, e.MovementNote);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region CreateSaturn5
            public void OnCreateSaturn5_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateSaturn5_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Create Saturn 5 button pressed.");
                }
            }

            public void OnCreateSaturn5_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateSaturn5_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToCreateSaturn5Log(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingCreateSaturn5Report(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideCreateSaturn5Report(this.rtbDoNow);
                }
            }

            public void OnCreateSaturn5_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateSaturn5_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToCreateSaturn5Log(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnCreateSaturn5_UploadingReportDataBegan(object sender, CreateSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, CreateSaturn5EventArgs> d = new Action<object, CreateSaturn5EventArgs>(OnCreateSaturn5_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingCreateSaturn5Data(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnCreateSaturn5_ReportDataUploadSucceed(object sender, CreateSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, CreateSaturn5EventArgs> d = new Action<object, CreateSaturn5EventArgs>(OnCreateSaturn5_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCreateSaturn5DataUploadSucceedLog(this.rtbLogs, e.SerialNumber, e.ShortId, e.PhoneNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnCreateSaturn5_ReportDataUploadFailed(object sender, CreateSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, CreateSaturn5EventArgs> d = new Action<object, CreateSaturn5EventArgs>(OnCreateSaturn5_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCreateSaturn5DataUploadFailedLog(this.rtbLogs, e.SerialNumber, e.ShortId, e.PhoneNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnCreateSaturn5_ReportDataUploadCanceled(object sender, CreateSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, CreateSaturn5EventArgs> d = new Action<object, CreateSaturn5EventArgs>(OnCreateSaturn5_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCreateSaturn5DataUploadCanceledLog(this.rtbLogs, e.SerialNumber, e.ShortId, e.PhoneNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region EditSaturn5
            public void OnEditSaturn5_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Edit Saturn 5 button pressed.");
                }
            }

            public void OnEditSaturn5_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToEditSaturn5Log(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToEditSaturn5Log(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            
            public void OnEditSaturn5_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnEditSaturn5_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnEditSaturn5_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnEditSaturn5_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnEditSaturn5_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnEditSaturn5_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnEditSaturn5_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_AwaitingReport(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnEditSaturn5_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingEditSaturn5Report(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideEditSaturn5Report(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_AwaitingReportCanceled(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnEditSaturn5_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToEditSaturn5Log(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_UploadingReportDataBegan(object sender, EditSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditSaturn5EventArgs> d = new Action<object, EditSaturn5EventArgs>(OnEditSaturn5_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingEditSaturn5Data(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_ReportDataUploadSucceed(object sender, EditSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditSaturn5EventArgs> d = new Action<object, EditSaturn5EventArgs>(OnEditSaturn5_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEditSaturn5DataUploadSucceedLog(this.rtbLogs, e.ToBeEdited, e.NewShortId, e.NewPhoneNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_ReportDataUploadFailed(object sender, EditSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditSaturn5EventArgs> d = new Action<object, EditSaturn5EventArgs>(OnEditSaturn5_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEditSaturn5DataUploadFailedLog(this.rtbLogs, e.ToBeEdited, e.NewShortId, e.NewPhoneNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditSaturn5_ReportDataUploadCanceled(object sender, EditSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditSaturn5EventArgs> d = new Action<object, EditSaturn5EventArgs>(OnEditSaturn5_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEditSaturn5DataUploadCanceledLog(this.rtbLogs, e.ToBeEdited, e.NewShortId, e.NewPhoneNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region Saturn5SendToIT
            public void OnSaturn5SendToIT_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Saturn 5 - Send To IT button pressed.");
                }
            }

            public void OnSaturn5SendToIT_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToSaturn5SendToITLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToSaturn5SendToITLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            
            public void OnSaturn5SendToIT_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnSaturn5SendToIT_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnSaturn5SendToIT_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnSaturn5SendToIT_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnSaturn5SendToIT_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnSaturn5SendToIT_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnSaturn5SendToIT_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_AwaitingReport(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnSaturn5SendToIT_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingSaturn5SendToITReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideSaturn5SendToITReport(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_AwaitingReportCanceled(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnSaturn5SendToIT_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToSaturn5SendToITLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_UploadingReportDataBegan(object sender, Saturn5SendToITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SendToITEventArgs> d = new Action<object, Saturn5SendToITEventArgs>(OnSaturn5SendToIT_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5SendToITData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_ReportDataUploadSucceed(object sender, Saturn5SendToITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SendToITEventArgs> d = new Action<object, Saturn5SendToITEventArgs>(OnSaturn5SendToIT_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SendToITDataUploadSucceedLog(this.rtbLogs, e.SerialNumber, e.ConsignmentNumber, e.IncidentNumber, e.MovementNote, e.Surplus);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_ReportDataUploadFailed(object sender, Saturn5SendToITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SendToITEventArgs> d = new Action<object, Saturn5SendToITEventArgs>(OnSaturn5SendToIT_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SendToITDataUploadFailedLog(this.rtbLogs, e.SerialNumber, e.ConsignmentNumber, e.IncidentNumber, e.MovementNote, e.Surplus);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_AttemptToSendToITLastSaturn5(object sender, Saturn5SendToITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SendToITEventArgs> d = new Action<object, Saturn5SendToITEventArgs>(OnSaturn5SendToIT_AttemptToSendToITLastSaturn5);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptToSentToITLastSaturn5Log(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnSaturn5SendToIT_ReportDataUploadCanceled(object sender, Saturn5SendToITEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SendToITEventArgs> d = new Action<object, Saturn5SendToITEventArgs>(OnSaturn5SendToIT_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SendToITDataUploadCanceledLog(this.rtbLogs, e.SerialNumber, e.ConsignmentNumber, e.IncidentNumber, e.MovementNote, e.Surplus);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region RemoveSaturn5
            public void OnRemoveSaturn5_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Remove Saturn 5 button pressed.");
                }
            }

            public void OnRemoveSaturn5_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToRemoveSaturn5Log(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToRemoveSaturn5Log(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            
            public void OnRemoveSaturn5_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRemoveSaturn5_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRemoveSaturn5_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRemoveSaturn5_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRemoveSaturn5_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRemoveSaturn5_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRemoveSaturn5_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_AwaitingReport(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRemoveSaturn5_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingRemoveSaturn5Report(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideRemoveSaturn5Report(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_AwaitingReportCanceled(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRemoveSaturn5_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToRemoveSaturn5Log(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_UploadingReportDataBegan(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRemoveSaturn5_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingRemoveSaturn5Data(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_AttemptToRemoveLastSaturn5(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRemoveSaturn5_AttemptToRemoveLastSaturn5);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptToRemoveLastSaturn5Log(this.rtbLogs, e.Saturn5.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_ReportDataUploadSucceed(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRemoveSaturn5_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddRemoveSaturn5DataUploadSucceedLog(this.rtbLogs, e.Saturn5);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_ReportDataUploadFailed(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRemoveSaturn5_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddRemoveSaturn5DataUploadFailedLog(this.rtbLogs, e.Saturn5);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveSaturn5_ReportDataUploadCanceled(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRemoveSaturn5_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddRemoveSaturn5DataUploadCanceledLog(this.rtbLogs, e.Saturn5);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region ReportSaturn5Fault
            public void OnReportSaturn5Fault_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Report Saturn5 fault button pressed.");
                }
            }

            public void OnReportSaturn5Fault_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToReportSaturn5FaultLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToReportSaturn5FaultLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            
            public void OnReportSaturn5Fault_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Fault_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Fault_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Fault_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Fault_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Fault_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Fault_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_AwaitingFaultReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_AwaitingFaultReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingSaturn5FaultReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideSaturn5FaultReport(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_AwaitingFaultReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_AwaitingFaultReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToReportSaturn5FaultLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_UploadingSaturn5ReportingFaultDataBegan(object sender, Saturn5FaultReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5FaultReportEventArgs> d = new Action<object, Saturn5FaultReportEventArgs>(OnReportSaturn5Fault_UploadingSaturn5ReportingFaultDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5FaultData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_Succeed(object sender, FaultySaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, FaultySaturn5EventArgs> d = new Action<object, FaultySaturn5EventArgs>(OnReportSaturn5Fault_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5FaultSuccesfullyLog(this.rtbLogs, e.Saturn5Fault);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_Failed(object sender, Saturn5FaultReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5FaultReportEventArgs> d = new Action<object, Saturn5FaultReportEventArgs>(OnReportSaturn5Fault_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5FaultFailedLog(this.rtbLogs, e.Saturn5, e.Description);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Fault_Canceled(object sender, Saturn5FaultReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5FaultReportEventArgs> d = new Action<object, Saturn5FaultReportEventArgs>(OnReportSaturn5Fault_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5FaultCanceledLog(this.rtbLogs, e.Saturn5, e.Description);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region ReportSaturn5Damage
            public void OnReportSaturn5Damage_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Report Saturn5 damage button pressed.");
                }
            }

            public void OnReportSaturn5Damage_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToReportSaturn5DamageLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToReportSaturn5DamageLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            
            public void OnReportSaturn5Damage_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Damage_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Damage_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Damage_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Damage_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Damage_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnReportSaturn5Damage_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_AwaitingDamageReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_AwaitingDamageReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingSaturn5DamageReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideSaturn5DamageReport(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_AwaitingDamageReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_AwaitingDamageReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToReportSaturn5DamageLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_UploadingSaturn5ReportingDamageDataBegan(object sender, Saturn5DamageReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5DamageReportEventArgs> d = new Action<object, Saturn5DamageReportEventArgs>(OnReportSaturn5Damage_UploadingSaturn5ReportingDamageDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingSaturn5DamageData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_Succeed(object sender, DamagedSaturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, DamagedSaturn5EventArgs> d = new Action<object, DamagedSaturn5EventArgs>(OnReportSaturn5Damage_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5DamageSuccesfullyLog(this.rtbLogs, e.Saturn5Damage);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_Failed(object sender, Saturn5DamageReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5DamageReportEventArgs> d = new Action<object, Saturn5DamageReportEventArgs>(OnReportSaturn5Damage_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5DamageFailedLog(this.rtbLogs, e.Saturn5, e.Description);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnReportSaturn5Damage_Canceled(object sender, Saturn5DamageReportEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5DamageReportEventArgs> d = new Action<object, Saturn5DamageReportEventArgs>(OnReportSaturn5Damage_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddReportSaturn5DamageCanceledLog(this.rtbLogs, e.Saturn5, e.Description);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region ResolveSaturn5Issue
            public void OnResolveSaturn5Issue_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Saturn 5 - Resolve Issue in Depot button pressed.");
                }
            }

            public void OnResolveSaturn5Issue_AwaitingSerialNumber(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_AwaitingSerialNumber);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToResolveSaturn5IssueLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_AwaitingSerialNumberCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_AwaitingSerialNumberCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToResolveSaturn5IssueLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_ProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnResolveSaturn5Issue_ProvidedSerialNumberValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationBeganLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowValidatingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_ProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnResolveSaturn5Issue_ProvidedSerialNumberValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationFailedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_ProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnResolveSaturn5Issue_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddSaturn5SerialNumberValidationCanceledLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_EmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnResolveSaturn5Issue_EmptySerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptySaturn5SerialNumberProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_InvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnResolveSaturn5Issue_InvalidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.ShowAwaitingSaturn5SerialNumber(this.rtbCurrently);
                    rtbDoNowServices.ShowScanSaturn5SerialNumber(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_ValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnResolveSaturn5Issue_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidSaturn5SerialNumberProvidedLog(this.rtbLogs, e.SerialNumber);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_AwaitingReport(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnResolveSaturn5Issue_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingResolveSaturn5IssueReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideResolveSaturn5IssueReport(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_AwaitingReportCanceled(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnResolveSaturn5Issue_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToResolveSaturn5IssueLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_UploadingReportDataBegan(object sender, ResolveSaturn5IssueEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, ResolveSaturn5IssueEventArgs> d = new Action<object, ResolveSaturn5IssueEventArgs>(OnResolveSaturn5Issue_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingResolveSaturn5IssueData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_ReportDataUploadSucceed(object sender, ResolveSaturn5IssueEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, ResolveSaturn5IssueEventArgs> d = new Action<object, ResolveSaturn5IssueEventArgs>(OnResolveSaturn5Issue_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddResolveSaturn5IssueDataUploadSucceedLog(this.rtbLogs, e.Issue, e.ResolvedBy, e.ResolvedHow, e.ResolvedHowDescription);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_ReportDataUploadFailed(object sender, ResolveSaturn5IssueEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, ResolveSaturn5IssueEventArgs> d = new Action<object, ResolveSaturn5IssueEventArgs>(OnResolveSaturn5Issue_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddResolveSaturn5IssueDataUploadFailedLog(this.rtbLogs, e.Issue, e.ResolvedBy, e.ResolvedHow, e.ResolvedHowDescription);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnResolveSaturn5Issue_ReportDataUploadCanceled(object sender, ResolveSaturn5IssueEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, ResolveSaturn5IssueEventArgs> d = new Action<object, ResolveSaturn5IssueEventArgs>(OnResolveSaturn5Issue_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddResolveSaturn5IssueDataUploadCanceledLog(this.rtbLogs, e.Issue, e.ResolvedBy, e.ResolvedHow, e.ResolvedHowDescription);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion
            #endregion

            #region Admin
            #region CreateUser
            public void OnCreateUser_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateUser_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Create User button pressed.");
                }
            }

            public void OnCreateUser_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateUser_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToCreateUserLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingCreateUserReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideCreateUserReport(this.rtbDoNow);
                }
            }

            public void OnCreateUser_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateUser_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToCreateUserLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnCreateUser_UploadingReportDataBegan(object sender, CreateUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, CreateUserEventArgs> d = new Action<object, CreateUserEventArgs>(OnCreateUser_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingCreateUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnCreateUser_ReportDataUploadSucceed(object sender, CreateUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, CreateUserEventArgs> d = new Action<object, CreateUserEventArgs>(OnCreateUser_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCreateUserDataUploadSucceedLog(this.rtbLogs, e.Username, e.FirstName, e.Surname, e.UserType);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnCreateUser_ReportDataUploadFailed(object sender, CreateUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, CreateUserEventArgs> d = new Action<object, CreateUserEventArgs>(OnCreateUser_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCreateUserDataUploadFailedLog(this.rtbLogs, e.Username, e.FirstName, e.Surname, e.UserType);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnCreateUser_ReportDataUploadCanceled(object sender, CreateUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, CreateUserEventArgs> d = new Action<object, CreateUserEventArgs>(OnCreateUser_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCreateUserDataUploadCanceledLog(this.rtbLogs, e.Username, e.FirstName, e.Surname, e.UserType);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region EditUser
            public void OnEditUser_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Edit User button pressed.");
                }
            }

            public void OnEditUser_AwaitingUsername(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_AwaitingUsername);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToEditUserLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnEditUser_AwaitingUsernameCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_AwaitingUsernameCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToEditUserLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            
            public void OnEditUser_ProvidedUsernameValidationBegan(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEditUser_ProvidedUsernameValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationBeganLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowValidatingUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnEditUser_ProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEditUser_ProvidedUsernameValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationFailedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditUser_ProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEditUser_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationCanceledLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditUser_EmptyUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEditUser_EmptyUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptyUsernameProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnEditUser_InvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEditUser_InvalidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnEditUser_ValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnEditUser_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditUser_AwaitingReport(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnEditUser_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingEditUserReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideEditUserReport(this.rtbDoNow);
                }
            }

            public void OnEditUser_AwaitingReportCanceled(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnEditUser_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToEditUserLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditUser_UploadingReportDataBegan(object sender, EditUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditUserEventArgs> d = new Action<object, EditUserEventArgs>(OnEditUser_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingEditUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnEditUser_ReportDataUploadSucceed(object sender, EditUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditUserEventArgs> d = new Action<object, EditUserEventArgs>(OnEditUser_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEditUserDataUploadSucceedLog(this.rtbLogs, e.ToBeEdited, e.NewFirstName, e.NewSurname, e.NewUserType);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditUser_AttemptToEditLoggedInUser(object sender, EditUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditUserEventArgs> d = new Action<object, EditUserEventArgs>(OnEditUser_AttemptToEditLoggedInUser);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptToEditLoggedInUserLog(this.rtbLogs);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditUser_ReportDataUploadFailed(object sender, EditUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditUserEventArgs> d = new Action<object, EditUserEventArgs>(OnEditUser_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEditUserDataUploadFailedLog(this.rtbLogs, e.ToBeEdited, e.NewFirstName, e.NewSurname, e.NewUserType);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnEditUser_ReportDataUploadCanceled(object sender, EditUserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EditUserEventArgs> d = new Action<object, EditUserEventArgs>(OnEditUser_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEditUserDataUploadCanceledLog(this.rtbLogs, e.ToBeEdited, e.NewFirstName, e.NewSurname, e.NewUserType);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion

            #region RemoveUser
            public void OnRemoveUser_ButtonPressed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_ButtonPressed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddButtonPressedLog(this.rtbLogs, "Remove User button pressed.");
                }
            }

            public void OnRemoveUser_AwaitingUsername(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_AwaitingUsername);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptingToRemoveUserLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_AwaitingUsernameCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_AwaitingUsernameCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToRemoveUserLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_ProvidedUsernameValidationBegan(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRemoveUser_ProvidedUsernameValidationBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationBeganLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowValidatingUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowValidatingingInputWait(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_ProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRemoveUser_ProvidedUsernameValidationFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationFailedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_ProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRemoveUser_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddUsernameValidationCanceledLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_EmptyUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRemoveUser_EmptyUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddEmptyUsernameProvidedLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_InvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRemoveUser_InvalidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddInvalidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.ShowAwaitingUserUsername(this.rtbCurrently);
                    rtbDoNowServices.ShowScanUserUsername(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_ValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRemoveUser_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddValidUsernameProvidedLog(this.rtbLogs, e.Username);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_AwaitingReport(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRemoveUser_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowAwaitingRemoveUserReport(this.rtbCurrently);
                    rtbDoNowServices.ShowProvideRemoveUserReport(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_AwaitingReportCanceled(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRemoveUser_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddCancelToRemoveUserLog(this.rtbLogs);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_UploadingReportDataBegan(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRemoveUser_UploadingReportDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbCurrentlyServices.ShowUploadingRemoveUserData(this.rtbCurrently);
                    rtbDoNowServices.ShowUploadingDataWait(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_ReportDataUploadSucceed(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRemoveUser_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddRemoveUserDataUploadSucceedLog(this.rtbLogs, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_AttemptToRemoveLoggedInUser(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRemoveUser_AttemptToRemoveLoggedInUser);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddAttemptToRemoveLoggedInUserLog(this.rtbLogs);
                    rtbCurrentlyServices.Clear(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_ReportDataUploadFailed(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRemoveUser_ReportDataUploadFailed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddRemoveUserDataUploadFailedLog(this.rtbLogs, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }

            public void OnRemoveUser_ReportDataUploadCanceled(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRemoveUser_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbLogsServices.AddRemoveUserDataUploadCanceledLog(this.rtbLogs, e.User);
                    rtbCurrentlyServices.ShowIdle(this.rtbCurrently);
                    rtbDoNowServices.Clear(this.rtbDoNow);
                }
            }
            #endregion
            #endregion
            #endregion
        }
    }
}
