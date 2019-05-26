using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI
{
    public partial class MainForm
    {
        // Helper class managing enabling/disabling tabs/buttons/textboxes.
        public class ControlsEnabler
        {
            #region Public Event Handlers
            public void OnApplicationStarted(object sender, System.EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnApplicationStarted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.DisableButtons_NotConnectedLoggedOut();
                    this.EnableButtons_NotConnectedLoggedOut();

                    // Text boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            #region PreBriefTab
            #region AllocateSaturn5BySerialNumberUITask
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnablePreBriefUsernameTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ValidUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnablePreBriefSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5BySerialNumber_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region AllocateSaturn5ByShortIdUITaskUITask
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnablePreBriefUsernameTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5ByShortId_ProvidedUsernameValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            
            public void OnAllocateSaturn5ByShortId_ValidUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5ByShortId_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnablePreBriefShortIdTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5ByShortId_ProvidedShortIdValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_ProvidedShortIdValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5ByShortId_ValidShortIdProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_ValidShortIdProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5ByShortId_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5ByShortId_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5ByShortId_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnAllocateSaturn5ByShortId_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnAllocateSaturn5ByShortId_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region EmergencyAllocateSaturn5BySerialNumberUITask
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnablePreBriefUsernameTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ValidUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnablePreBriefSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            
            public void OnEmergencyAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButPreBriefTab();

                    // Buttons
                    this.DisableButtons_PreBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEmergencyAllocateSaturn5BySerialNumber_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEmergencyAllocateSaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion
            #endregion

            #region DeBriefTab
            #region ConfirmBackInSaturn5BySerialNumberUITask
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableDeBriefSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5BySerialNumber_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region ConfirmBackInSaturn5ByShortIdUITask
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableDeBriefShortIdTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_ProvidedShortIdValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_ProvidedShortIdValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_ValidShortIdProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_ValidShortIdProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInSaturn5ByShortId_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInSaturn5ByShortId_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region ConfirmBackInFaultySaturn5BySerialNumberUITask
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableDeBriefSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }


            public void OnConfirmBackInFaultySaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_UploadingSaturn5ReportingFaultDataCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInFaultySaturn5BySerialNumber_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInFaultySaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region ConfirmBackInDamagedSaturn5BySerialNumberUITask
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableDeBriefSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            
            public void OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButDeBriefTab();

                    // Buttons
                    this.DisableButtons_DeBriefOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnConfirmBackInDamageSaturn5BySerialNumber_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnConfirmBackInDamageSaturn5BySerialNumber_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion
            #endregion

            #region OptionsTab
            #region ConnectUITask          
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_OptionsOperationBegan();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_OptionsOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.DisableButtons_ConnectedLoggedOut();
                    this.DisableCancelButton();
                    this.EnableButtons_ConnectedLoggedOut();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_NotConnectedLoggedOut();
                    this.DisableCancelButton();
                    this.EnableButtons_NotConnectedLoggedOut();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_NotConnectedLoggedOut();
                    this.DisableCancelButton();
                    this.EnableButtons_NotConnectedLoggedOut();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region DisconnectUITask          
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_OptionsOperationBegan();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_OptionsOperationBegan();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_NotConnectedLoggedOut();
                    this.EnableButtons_NotConnectedLoggedOut();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region LogInUITask
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_OptionsOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnLogIn_AwaitingUsername(object sender, System.EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_AwaitingUsername);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_OptionsOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableOptionsUsernameTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.DisableButtons_ConnectedLoggedOut();
                    this.EnableButtons_ConnectedLoggedOut();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnLogIn_ProvidedUsernameValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.DisableButtons_ConnectedLoggedOut();
                    this.EnableButtons_ConnectedLoggedOut();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnLogIn_ValidUsernameProvided(object sender, System.EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_OptionsOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnLogIn_Canceled(object sender, System.EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.DisableButtons_ConnectedLoggedOut();
                    this.EnableButtons_ConnectedLoggedOut();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnLogIn_Succesfully(object sender, System.EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnLogIn_Succesfully);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.DisableButtons_LoggedInAndUIIsInIdle();
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region LogOutUITask
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
                    // Tabs
                    this.DisableAllButOptionsTab();

                    // Buttons
                    this.DisableButtons_ConnectedLoggedOut();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.DisableButtons_ConnectedLoggedOut();
                    this.EnableButtons_ConnectedLoggedOut();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnSaturn5ReceiveFromIT_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5ReceiveFromIT_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnSaturn5ReceiveFromIT_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5ReceiveFromIT_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnCreateSaturn5_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateSaturn5_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnCreateSaturn5_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateSaturn5_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();
                    
                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableStockManagementSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditSaturn5_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            
            public void OnEditSaturn5_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditSaturn5_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            
            public void OnEditSaturn5_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditSaturn5_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditSaturn5_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditSaturn5_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditSaturn5_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableStockManagementSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnSaturn5SendToIT_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            
            public void OnSaturn5SendToIT_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnSaturn5SendToIT_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnSaturn5SendToIT_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnSaturn5SendToIT_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnSaturn5SendToIT_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnSaturn5SendToIT_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnSaturn5SendToIT_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableStockManagementSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveSaturn5_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveSaturn5_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveSaturn5_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveSaturn5_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveSaturn5_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveSaturn5_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveSaturn5_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveSaturn5_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region ReportSaturn5FaultUITask
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableStockManagementSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Fault_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Fault_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Fault_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Fault_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Fault_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Fault_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Fault_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Fault_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region ReportSaturn5DamageUITask
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableStockManagementSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Damage_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Damage_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Damage_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Damage_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Damage_Succeed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_Succeed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Damage_Failed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_Failed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnReportSaturn5Damage_Canceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnReportSaturn5Damage_Canceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region ResolveSaturn5IssueUITask
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableStockManagementSerialNumberTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnResolveSaturn5Issue_ProvidedSerialNumberValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_ProvidedSerialNumberValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnResolveSaturn5Issue_ValidSerialNumberProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_ValidSerialNumberProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnResolveSaturn5Issue_CancelToRetrieveSaturn5Data(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_CancelToRetrieveSaturn5Data);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnResolveSaturn5Issue_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButSaturn5StockManagementTab();

                    // Buttons
                    this.DisableButtons_Saturn5StockManagementOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnResolveSaturn5Issue_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnResolveSaturn5Issue_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnResolveSaturn5Issue_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnResolveSaturn5Issue_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion
            #endregion

            #region Admin
            #region CreateSaturn5
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
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnCreateUser_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateUser_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnCreateUser_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnCreateUser_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableAdminUsernameTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditUser_ProvidedUsernameValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditUser_ValidUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditUser_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditUser_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditUser_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditUser_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnEditUser_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnEditUser_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion

            #region Remove User
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
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
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
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.EnableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                    this.EnableAdminUsernameTextBox();
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
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveUser_ProvidedUsernameValidationCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_ProvidedUsernameValidationCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveUser_ValidUsernameProvided(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_ValidUsernameProvided);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveUser_CancelToRetrieveUserData(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_CancelToRetrieveUserData);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveUser_AwaitingReport(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_AwaitingReport);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.DisableAllButAdminTab();

                    // Buttons
                    this.DisableButtons_AdminOperationBegan();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveUser_AwaitingReportCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_AwaitingReportCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveUser_ReportDataUploadSucceed(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_ReportDataUploadSucceed);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }

            public void OnRemoveUser_ReportDataUploadCanceled(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(OnRemoveUser_ReportDataUploadCanceled);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Tabs
                    this.EnableTabs();

                    // Buttons
                    this.EnableButtons_LoggedInAndUIIsInIdle();
                    this.DisableCancelButton();

                    // Text Boxes
                    this.DisableAllInputTextBoxes();
                }
            }
            #endregion
            #endregion
            #endregion

            #region Private Fields
            // Main form
            private MainForm _form;

            // Domain layer facade.
            private App _app { get { return _form._app; } }

            // Main tab control
            private TabControl tcMain { get { return this._form.tcMain; } }

            #region MainForm Tabs
            private TabPage tpPreBrief { get { return this._form.tpPreBrief; } }
            private TabPage tpDeBrief { get { return this._form.tpDeBrief; } }
            private TabPage tpOptions { get { return this._form.tpOptions; } }
            private TabPage tpSaturn5StockManagement { get { return this._form.tpSaturn5StockManagement; } }
            private TabPage tpAdmin { get { return this._form.tpAdmin; } }
            #endregion

            #region Buttons
            #region PreBrief Tab Buttons
            private Button btnAllocateSaturn5BySerialNumber { get { return this._form.btnAllocateSaturn5BySerialNumber; } }
            private Button btnAllocateSaturn5ByShortId { get { return this._form.btnAllocateSaturn5ByShortId; } }
            private Button btnEmergencyAllocateSaturn5BySerialNumber { get { return this._form.btnEmergencyAllocateSaturn5BySerialNumber; } }
            #endregion

            #region DeBrief Tab Buttons
            private Button btnConfrimSaturn5BySerialNumber { get { return this._form.btnConfrimSaturn5BySerialNumber; } }
            private Button btnConfrimSaturn5ByShortId { get { return this._form.btnConfrimSaturn5ByShortId; } }
            private Button btnConfrimFaultySaturn5BySerialNumber { get { return this._form.btnConfrimFaultySaturn5BySerialNumber; } }
            private Button btnConfrimDamagedSaturn5BySerialNumber { get { return this._form.btnConfrimDamagedSaturn5BySerialNumber; } }
            #endregion

            #region Options Tab Buttons
            private Button btnConnect { get { return this._form.btnConnect; } }
            private Button btnDisconnect { get { return this._form.btnDisconnect; } }
            private Button btnSignIn { get { return this._form.btnSignIn; } }
            private Button btnSignOut { get { return this._form.btnSignOut; } }
            #endregion

            #region Saturn5 Stock Management
            private Button btnSaturn5SMReceiveFromIT { get { return this._form.btnSaturn5SMReceiveFromIT; } }
            private Button btnSaturn5SMCreate { get { return this._form.btnSaturn5SMCreate; } }
            private Button btnSaturn5SMEdit { get { return this._form.btnSaturn5SMEdit; } }
            private Button btnSaturn5SMSendToIT { get { return this._form.btnSaturn5SMSendToIT; } }
            private Button btnSaturn5SMRemove { get { return this._form.btnSaturn5SMRemove; } }
            
            private Button btnSaturn5SMReportFault { get { return this._form.btnSaturn5SMReportFault; } }
            private Button btnSaturn5SMReportDamage { get { return this._form.btnSaturn5SMReportDamage; } }
            private Button btnSaturn5SMResolveIssue { get { return this._form.btnSaturn5SMResolveIssue; } }
            #endregion

            #region Admin
            private Button btnAdminCreateUser { get { return this._form.btnAdminCreateUser; } }
            private Button btnAdminEditUser { get { return this._form.btnAdminEditUser; } }
            private Button btnAdminRemoveUser { get { return this._form.btnAdminRemoveUser; } }
            #endregion

            // Cancel button.
            private Button btnCancel { get { return this._form.btnCancel; } }
            #endregion

            #region TextBoxes / InputValidatingTextBoxes
            #region PreBrief Tab Content TextBoxes
            private UserUsernameValidatingTextBox tbxPreBriefUserUsername { get { return this._form.tbxPreBriefUserUsername; } }
            private TextBox tbxPreBriefUserType { get { return this._form.tbxPreBriefUserType; } }
            private TextBox tbxPreBriefUserFirstName { get { return this._form.tbxPreBriefUserFirstName; } }
            private TextBox tbxPreBriefUserSurname { get { return this._form.tbxPreBriefUserSurname; } }

            private UserWithSaturn5SerialNumberValidatingTextBox tbxPreBriefSaturn5SerialNumber { get { return this._form.tbxPreBriefSaturn5SerialNumber; } }
            private UserWithSaturn5ShortIdValidatingTextBox tbxPreBriefSaturn5Barcode { get { return this._form.tbxPreBriefSaturn5Barcode; } }

            private RichTextBox rtbPreBriefInfo { get { return this._form.rtbPreBriefInfo; } }
            #endregion

            #region DeBrief Tab Content TextBoxes
            private TextBox tbxDeBriefUserUsername { get { return this._form.tbxDeBriefUserUsername; } }
            private TextBox tbxDeBriefUserType { get { return this._form.tbxDeBriefUserType; } }
            private TextBox tbxDeBriefUserFirstName { get { return this._form.tbxDeBriefUserFirstName; } }
            private TextBox tbxDeBriefUserSurname { get { return this._form.tbxDeBriefUserSurname; } }

            private Saturn5SerialNumberValidatingTextBox tbxDeBriefSaturn5SerialNumber { get { return this._form.tbxDeBriefSaturn5SerialNumber; } }
            private Saturn5ShortIdValidatingTextBox tbxDeBriefSaturn5Barcode { get { return this._form.tbxDeBriefSaturn5Barcode; } }

            private RichTextBox rtbDeBriefInfo { get { return this._form.rtbDeBriefInfo; } }
            #endregion

            #region Options Tab
            private UserUsernameValidatingTextBox tbxOptionsUserUsername { get { return this._form.tbxOptionsUserUsername; } }
            private TextBox tbxOptionsUserType { get { return this._form.tbxOptionsUserType; } }
            private TextBox tbxOptionsUserFirstName { get { return this._form.tbxOptionsUserFirstName; } }
            private TextBox tbxOptionsUserSurname { get { return this._form.tbxOptionsUserSurname; } }
            #endregion

            #region Saturn 5 Stock Management
            private Saturn5SerialNumberValidatingTextBox tbxSaturn5SMSerialNumber { get { return this._form.tbxSaturn5SMSerialNumber; } }
            private TextBox tbxSaturn5SMBarcode { get { return this._form.tbxSaturn5SMBarcode; } }

            private RichTextBox rtbSaturn5SMInfo { get { return this._form.rtbSaturn5SMInfo; } }
            #endregion

            #region Admin
            private UserUsernameValidatingTextBox tbxAdminUserUsername { get { return this._form.tbxAdminUserUsername; } }
            private TextBox tbxAdminUserType { get { return this._form.tbxAdminUserType; } }
            private TextBox tbxAdminUserFirstName { get { return this._form.tbxAdminUserFirstName; } }
            private TextBox tbxAdminUserSurname { get { return this._form.tbxAdminUserSurname; } }

            private RichTextBox rtbAdminInfo { get { return this._form.rtbAdminInfo; } }
            #endregion
            #endregion
            #endregion

            #region Constructor
            public ControlsEnabler(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Private Helpers
            #region Enable/Disable tabs
            #region Enable
            private void EnableTabs()
            {
                UserType? loggedUserType = this._app.LoggedUser?.Type;

                switch (loggedUserType)
                {
                    case null:
                        this.EnableOptionsTabOnly();
                        break;
                    case UserType.User:
                        this.EnableUserTabs();
                        break;
                    case UserType.StarUser:
                        this.EnableUserTabs();
                        break;
                    case UserType.Manager:
                        this.EnableManagerTabs();
                        break;
                    case UserType.PreBrief:
                        this.EnablePreBriefTabs();
                        break;
                    case UserType.DeBrief:
                        this.EnableDeBriefTabs();
                        break;
                    case UserType.Admin:
                        this.EnableAdminTabs();
                        break;
                    default:
                        throw new NotImplementedException($"UserType value {loggedUserType} hasn't been implemented inside this method.");
                }
            }

            private void EnableOptionsTabOnly()
            {
                this.tpPreBrief.Enabled = false;
                this.tpDeBrief.Enabled = false;
                this.tpOptions.Enabled = true;
                this.tpSaturn5StockManagement.Enabled = false;
                this.tpAdmin.Enabled = false;
            }

            private void EnableAdminTabs()
            {
                this.tpPreBrief.Enabled = true;
                this.tpDeBrief.Enabled = true;
                this.tpOptions.Enabled = true;
                this.tpSaturn5StockManagement.Enabled = true;
                this.tpAdmin.Enabled = true;
            }

            private void EnableManagerTabs()
            {
                this.tpPreBrief.Enabled = true;
                this.tpDeBrief.Enabled = true;
                this.tpOptions.Enabled = true;
                this.tpSaturn5StockManagement.Enabled = true;
            }

            private void EnablePreBriefTabs()
            {
                this.tpPreBrief.Enabled = true;
                this.tpDeBrief.Enabled = true;
                this.tpOptions.Enabled = true;
                this.tpSaturn5StockManagement.Enabled = true;
            }                 
                              
            private void EnableDeBriefTabs()
            {
                this.tpPreBrief.Enabled = true;
                this.tpDeBrief.Enabled = true;
                this.tpOptions.Enabled = true;
                this.tpSaturn5StockManagement.Enabled = true;
            }

            private void EnableUserTabs()
            {
                this.tpPreBrief.Enabled = true;
                this.tpDeBrief.Enabled = true;
                this.tpOptions.Enabled = true;
                this.tpSaturn5StockManagement.Enabled = true;
            }
            #endregion

            #region Disable 
            private void DisableAllButPreBriefTab()
            {
                this.tpPreBrief.Enabled = true;
                this.tpDeBrief.Enabled = false;
                this.tpOptions.Enabled = false;
                this.tpSaturn5StockManagement.Enabled = false;
                this.tpAdmin.Enabled = false;

                if (this.tcMain.SelectedTab != this.tpPreBrief)
                    this.tcMain.SelectedTab = tpPreBrief;
            }

            private void DisableAllButDeBriefTab()
            {
                this.tpPreBrief.Enabled = false;
                this.tpDeBrief.Enabled = true;
                this.tpOptions.Enabled = false;
                this.tpSaturn5StockManagement.Enabled = false;
                this.tpAdmin.Enabled = false;

                if (this.tcMain.SelectedTab != this.tpDeBrief)
                    this.tcMain.SelectedTab = tpDeBrief;
            }

            private void DisableAllButOptionsTab()
            {
                this.tpOptions.Enabled = true;
                this.tpPreBrief.Enabled = false;
                this.tpDeBrief.Enabled = false;
                this.tpSaturn5StockManagement.Enabled = false;
                this.tpAdmin.Enabled = false;

                if (this.tcMain.SelectedTab != this.tpOptions)
                    this.tcMain.SelectedTab = tpOptions;
            }

            private void DisableAllButSaturn5StockManagementTab()
            {
                this.tpPreBrief.Enabled = false;
                this.tpDeBrief.Enabled = false;
                this.tpOptions.Enabled = false;
                this.tpSaturn5StockManagement.Enabled = true;
                this.tpAdmin.Enabled = false;

                if (this.tcMain.SelectedTab != this.tpSaturn5StockManagement)
                    this.tcMain.SelectedTab = tpSaturn5StockManagement;
            }

            private void DisableAllButAdminTab()
            {
                this.tpPreBrief.Enabled = false;
                this.tpDeBrief.Enabled = false;
                this.tpOptions.Enabled = false;
                this.tpSaturn5StockManagement.Enabled = false;
                this.tpAdmin.Enabled = true;

                if (this.tcMain.SelectedTab != this.tpAdmin)
                    this.tcMain.SelectedTab = tpAdmin;
            }
            #endregion
            #endregion

            #region Enable/Disable Buttons
            private void EnableCancelButton()
            {
                this.btnCancel.Enabled = true;
            }

            private void DisableCancelButton()
            {
                this.btnCancel.Enabled = false;
            }

            #region Enable
            private void EnableButtons_NotConnectedLoggedOut()
            {
                this.btnConnect.Enabled = true;
            }

            private void EnableButtons_ConnectedLoggedOut()
            {
                this.btnSignIn.Enabled = true;
                this.btnDisconnect.Enabled = true;
            }

            private void EnableButtons_LoggedInAndUIIsInIdle()
            {
                UserType loggedUserType = this._app.LoggedUser?.Type ?? throw new InvalidOperationException("Cannot enable operations tabs buttons until a user will be sign in.");

                switch (loggedUserType)
                {
                    case UserType.User:
                        this.EnableUserLoggedInAndUIIsInIdleStateButtons();
                        break;
                    case UserType.StarUser:
                        this.EnableUserLoggedInAndUIIsInIdleStateButtons();
                        break;
                    case UserType.Manager:
                        this.EnableManagerLoggedInAndUIIsInIdleStateButtons();
                        break;
                    case UserType.PreBrief:
                        this.EnablePreBriefLoggedInAndUIIsInIdleStateButtons();
                        break;
                    case UserType.DeBrief:
                        this.EnableDeBriefLoggedInAndUIIsInIdleStateButtons();
                        break;
                    case UserType.Admin:
                        this.EnableAdminLoggedInAndUIIsInIdleStateButtons();
                        break;
                    default:
                        throw new NotImplementedException($"UserType value {loggedUserType} hasn't been implemented inside this method.");
                }
            }            

            // Not to be called directly
            private void EnableAdminLoggedInAndUIIsInIdleStateButtons()
            {
                // Pre-Brief
                this.btnAllocateSaturn5BySerialNumber.Enabled = true;
                this.btnAllocateSaturn5ByShortId.Enabled = true;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = true;

                // De-Brief
                this.btnConfrimSaturn5BySerialNumber.Enabled = true;
                this.btnConfrimSaturn5ByShortId.Enabled = true;
                this.btnConfrimFaultySaturn5BySerialNumber.Enabled = true;
                this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = true;

                // Options
                this.btnSignOut.Enabled = true;

                // Saturn5 Stock Management
                this.btnSaturn5SMReceiveFromIT.Enabled = true;
                this.btnSaturn5SMCreate.Enabled = true;
                this.btnSaturn5SMEdit.Enabled = true;
                this.btnSaturn5SMSendToIT.Enabled = true;
                this.btnSaturn5SMRemove.Enabled = true;
                this.btnSaturn5SMReportFault.Enabled = true;
                this.btnSaturn5SMReportDamage.Enabled = true;
                this.btnSaturn5SMResolveIssue.Enabled = true;
            
                // Admin
                this.btnAdminCreateUser.Enabled = true;
                this.btnAdminEditUser.Enabled = true;
                this.btnAdminRemoveUser.Enabled = true;
            }
            private void EnableManagerLoggedInAndUIIsInIdleStateButtons()
            {
                // Pre-Brief
                this.btnAllocateSaturn5BySerialNumber.Enabled = true;
                this.btnAllocateSaturn5ByShortId.Enabled = true;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = true;

                // De-Brief
                this.btnConfrimSaturn5BySerialNumber.Enabled = true;
                this.btnConfrimSaturn5ByShortId.Enabled = true;
                this.btnConfrimFaultySaturn5BySerialNumber.Enabled = true;
                this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = true;

                // Options
                this.btnSignOut.Enabled = true;

                // Saturn5 Stock Management
                this.btnSaturn5SMReceiveFromIT.Enabled = true;

                this.btnSaturn5SMEdit.Enabled = true;
                this.btnSaturn5SMSendToIT.Enabled = true;

                this.btnSaturn5SMReportFault.Enabled = true;
                this.btnSaturn5SMReportDamage.Enabled = true;
                this.btnSaturn5SMResolveIssue.Enabled = true;

                // Admin
                this.btnAdminCreateUser.Enabled = true;
                this.btnAdminEditUser.Enabled = true;
                this.btnAdminRemoveUser.Enabled = true;
            }
            private void EnablePreBriefLoggedInAndUIIsInIdleStateButtons()
            {
                // Pre-Brief
                this.btnAllocateSaturn5BySerialNumber.Enabled = true;
                this.btnAllocateSaturn5ByShortId.Enabled = true;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = true;

                // De-Brief
                this.btnConfrimSaturn5BySerialNumber.Enabled = true;
                this.btnConfrimSaturn5ByShortId.Enabled = true;
                this.btnConfrimFaultySaturn5BySerialNumber.Enabled = true;
                this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = true;

                // Options
                this.btnSignOut.Enabled = true;
                
                // Saturn5 Stock Management
                this.btnSaturn5SMReceiveFromIT.Enabled = true;

                this.btnSaturn5SMEdit.Enabled = true;
                this.btnSaturn5SMSendToIT.Enabled = true;

                this.btnSaturn5SMReportFault.Enabled = true;
                this.btnSaturn5SMReportDamage.Enabled = true;
                this.btnSaturn5SMResolveIssue.Enabled = true;

                // Admin
                this.btnAdminCreateUser.Enabled = true;
                this.btnAdminEditUser.Enabled = true;
                this.btnAdminRemoveUser.Enabled = true;
            }
            private void EnableDeBriefLoggedInAndUIIsInIdleStateButtons()
            {
                // Pre-Brief
                this.btnAllocateSaturn5BySerialNumber.Enabled = true;
                this.btnAllocateSaturn5ByShortId.Enabled = true;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = true;

                // De-Brief
                this.btnConfrimSaturn5BySerialNumber.Enabled = true;
                this.btnConfrimSaturn5ByShortId.Enabled = true;
                this.btnConfrimFaultySaturn5BySerialNumber.Enabled = true;
                this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = true;

                // Options
                this.btnSignOut.Enabled = true;

                // Saturn5 Stock Management
                this.btnSaturn5SMReceiveFromIT.Enabled = true;

                this.btnSaturn5SMEdit.Enabled = true;
                this.btnSaturn5SMSendToIT.Enabled = true;

                this.btnSaturn5SMReportFault.Enabled = true;
                this.btnSaturn5SMReportDamage.Enabled = true;
                this.btnSaturn5SMResolveIssue.Enabled = true;

                // Admin
                this.btnAdminCreateUser.Enabled = true;
                this.btnAdminEditUser.Enabled = true;
                this.btnAdminRemoveUser.Enabled = true;
            }
            private void EnableUserLoggedInAndUIIsInIdleStateButtons()
            {
                // Pre-Brief
                this.btnAllocateSaturn5BySerialNumber.Enabled = true;
                this.btnAllocateSaturn5ByShortId.Enabled = true;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = true;

                // De-Brief

                // Options
                this.btnSignOut.Enabled = true;

                // Saturn5 Stock Management

                this.btnSaturn5SMReportFault.Enabled = true;
                this.btnSaturn5SMReportDamage.Enabled = true;


                // Admin

            }
            #endregion

            #region Disable
            private void DisableButtons_NotConnectedLoggedOut()
            {
                // Pre brief tab page buttons
                this.btnAllocateSaturn5BySerialNumber.Enabled = false;
                this.btnAllocateSaturn5ByShortId.Enabled = false;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = false;

                // De Brief tab page buttons
                this.btnConfrimSaturn5BySerialNumber.Enabled = false;
                this.btnConfrimSaturn5ByShortId.Enabled = false;
                this.btnConfrimFaultySaturn5BySerialNumber.Enabled = false;
                this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = false;

                // Options tab page buttons
                this.btnSignIn.Enabled = false;
                this.btnDisconnect.Enabled = false;
                this.btnSignOut.Enabled = false;

                // Pre brief tab page buttons
                this.btnSaturn5SMReceiveFromIT.Enabled = false;
                this.btnSaturn5SMCreate.Enabled = false;
                this.btnSaturn5SMEdit.Enabled = false;
                this.btnSaturn5SMSendToIT.Enabled = false;
                this.btnSaturn5SMRemove.Enabled = false;
                this.btnSaturn5SMReportFault.Enabled = false;
                this.btnSaturn5SMReportDamage.Enabled = false;
                this.btnSaturn5SMResolveIssue.Enabled = false;

                // Admin tab page buttons
                this.btnAdminCreateUser.Enabled = false;
                this.btnAdminEditUser.Enabled = false;
                this.btnAdminRemoveUser.Enabled = false;
            }

            private void DisableButtons_ConnectedLoggedOut()
            {
                // Pre brief tab page buttons
                this.btnAllocateSaturn5BySerialNumber.Enabled = false;
                this.btnAllocateSaturn5ByShortId.Enabled = false;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = false;

                // De Brief tab page buttons
                this.btnConfrimSaturn5BySerialNumber.Enabled = false;
                this.btnConfrimSaturn5ByShortId.Enabled = false;
                this.btnConfrimFaultySaturn5BySerialNumber.Enabled = false;
                this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = false;

                // Options tab page buttons
                this.btnConnect.Enabled = false;
                this.btnDisconnect.Enabled = false;
                this.btnSignOut.Enabled = false;

                // Pre brief tab page buttons
                this.btnSaturn5SMReceiveFromIT.Enabled = false;
                this.btnSaturn5SMCreate.Enabled = false;
                this.btnSaturn5SMEdit.Enabled = false;
                this.btnSaturn5SMSendToIT.Enabled = false;
                this.btnSaturn5SMRemove.Enabled = false;
                this.btnSaturn5SMReportFault.Enabled = false;
                this.btnSaturn5SMReportDamage.Enabled = false;
                this.btnSaturn5SMResolveIssue.Enabled = false;

                // Admin tab page buttons
                this.btnAdminCreateUser.Enabled = false;
                this.btnAdminEditUser.Enabled = false;
                this.btnAdminRemoveUser.Enabled = false;
            }

            private void DisableButtons_LoggedInAndUIIsInIdle()
            {
                // Pre brief tab page buttons
                this.btnAllocateSaturn5BySerialNumber.Enabled = false;
                this.btnAllocateSaturn5ByShortId.Enabled = false;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = false;

                // De Brief tab page buttons
                this.btnConfrimSaturn5BySerialNumber.Enabled = false;
                this.btnConfrimSaturn5ByShortId.Enabled = false;
                this.btnConfrimFaultySaturn5BySerialNumber.Enabled = false;
                this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = false;

                // Options tab page buttons
                this.btnConnect.Enabled = false;
                this.btnDisconnect.Enabled = false;
                this.btnSignIn.Enabled = false;

                // Pre brief tab page buttons
                this.btnSaturn5SMReceiveFromIT.Enabled = false;
                this.btnSaturn5SMCreate.Enabled = false;
                this.btnSaturn5SMEdit.Enabled = false;
                this.btnSaturn5SMSendToIT.Enabled = false;
                this.btnSaturn5SMRemove.Enabled = false;
                this.btnSaturn5SMReportFault.Enabled = false;
                this.btnSaturn5SMReportDamage.Enabled = false;
                this.btnSaturn5SMResolveIssue.Enabled = false;

                // Admin tab page buttons
                this.btnAdminCreateUser.Enabled = false;
                this.btnAdminEditUser.Enabled = false;
                this.btnAdminRemoveUser.Enabled = false;
            }

            private void DisableButtons_PreBriefOperationBegan()
            {
                // Pre brief tab page buttons
                this.btnAllocateSaturn5BySerialNumber.Enabled = false;
                this.btnAllocateSaturn5ByShortId.Enabled = false;
                this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = false;
            }

            private void DisableButtons_DeBriefOperationBegan()
            {
                // De Brief tab page buttons
                this.btnConfrimSaturn5BySerialNumber.Enabled = false;
                this.btnConfrimSaturn5ByShortId.Enabled = false;
                this.btnConfrimFaultySaturn5BySerialNumber.Enabled = false;
                this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = false;
            }

            private void DisableButtons_OptionsOperationBegan()
            {
                // Options tab page buttons
                this.btnConnect.Enabled = false;
                this.btnDisconnect.Enabled = false;
                this.btnSignIn.Enabled = false;
                this.btnSignOut.Enabled = false;
            }

            private void DisableButtons_Saturn5StockManagementOperationBegan()
            {
                // Saturn 5 Stock Management tab page buttons
                this.btnSaturn5SMReceiveFromIT.Enabled = false;
                this.btnSaturn5SMCreate.Enabled = false;
                this.btnSaturn5SMEdit.Enabled = false;
                this.btnSaturn5SMSendToIT.Enabled = false;
                this.btnSaturn5SMRemove.Enabled = false;
                this.btnSaturn5SMReportFault.Enabled = false;
                this.btnSaturn5SMReportDamage.Enabled = false;
                this.btnSaturn5SMResolveIssue.Enabled = false;
            }

            private void DisableButtons_AdminOperationBegan()
            {
                // Admin tab page buttons
                this.btnAdminCreateUser.Enabled = false;
                this.btnAdminEditUser.Enabled = false;
                this.btnAdminRemoveUser.Enabled = false;
            }
            #endregion
            #endregion

            #region Enable/Disable TextBoxes
            private void DisableAllInputTextBoxes()
            {
                // PreBrief tab text input controls
                this.tbxPreBriefUserUsername.Enabled = false;
                this.tbxPreBriefUserFirstName.Enabled = false;
                this.tbxPreBriefUserSurname.Enabled = false;
                this.tbxPreBriefUserType.Enabled = false;

                this.tbxPreBriefSaturn5SerialNumber.Enabled = false;
                this.tbxPreBriefSaturn5Barcode.Enabled = false;

                // DeBrief tab text input controls
                this.tbxDeBriefUserUsername.Enabled = false;
                this.tbxDeBriefUserFirstName.Enabled = false;
                this.tbxDeBriefUserSurname.Enabled = false;
                this.tbxDeBriefUserType.Enabled = false;

                this.tbxDeBriefSaturn5SerialNumber.Enabled = false;
                this.tbxDeBriefSaturn5Barcode.Enabled = false;

                // Options tab text input controls
                this.tbxOptionsUserUsername.Enabled = false;
                this.tbxOptionsUserFirstName.Enabled = false;
                this.tbxOptionsUserSurname.Enabled = false;
                this.tbxOptionsUserType.Enabled = false;

                // Saturn 5 stock management tab input controls
                this.tbxSaturn5SMSerialNumber.Enabled = false;
                this.tbxSaturn5SMBarcode.Enabled = false;

                // Admin tab text input controls
                this.tbxAdminUserUsername.Enabled = false;
                this.tbxAdminUserFirstName.Enabled = false;
                this.tbxAdminUserSurname.Enabled = false;
                this.tbxAdminUserType.Enabled = false;
            }
            
            private void EnablePreBriefUsernameTextBox()
            {
                this.tbxPreBriefUserUsername.Enabled = true;
            }
            private void DisablePreBriefUsernameTextBox()
            {
                this.tbxPreBriefUserUsername.Enabled = false;
            }

            private void EnablePreBriefSerialNumberTextBox()
            {
                this.tbxPreBriefSaturn5SerialNumber.Enabled = true;
            }
            private void DisablePreBriefSerialNumberTextBox()
            {
                this.tbxPreBriefSaturn5SerialNumber.Enabled = false;
            }
            
            private void EnablePreBriefShortIdTextBox()
            {
                this.tbxPreBriefSaturn5Barcode.Enabled = true;
            }
            private void DisablePreBriefShortIdTextBox()
            {
                this.tbxPreBriefSaturn5Barcode.Enabled = false;
            }
                        
            private void EnableDeBriefSerialNumberTextBox()
            {
                this.tbxDeBriefSaturn5SerialNumber.Enabled = true;
            }
            private void DisableDeBriefSerialNumberTextBox()
            {
                this.tbxDeBriefSaturn5SerialNumber.Enabled = false;
            }
            
            private void EnableDeBriefShortIdTextBox()
            {
                this.tbxDeBriefSaturn5Barcode.Enabled = true;
            }
            private void DisableDeBriefShortIdTextBox()
            {
                this.tbxDeBriefSaturn5Barcode.Enabled = false;
            }

            private void EnableOptionsUsernameTextBox()
            {
                this.tbxOptionsUserUsername.Enabled = true;
            }
            private void DisableOptionsUsernameTextBox()
            {
                this.tbxOptionsUserUsername.Enabled = false;
            }

            private void EnableStockManagementSerialNumberTextBox()
            {
                this.tbxSaturn5SMSerialNumber.Enabled = true;
            }
            private void DisableStockManagementSerialNumberTextBox()
            {
                this.tbxSaturn5SMSerialNumber.Enabled = false;
            }

            private void EnableAdminUsernameTextBox()
            {
                this.tbxAdminUserUsername.Enabled = true;
            }
            private void DisableAdminUsernameTextBox()
            {
                this.tbxAdminUserUsername.Enabled = false;
            }
            #endregion
            #endregion
        }
    }
}
