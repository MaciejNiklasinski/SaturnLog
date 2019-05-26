using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
using SaturnLog.UI.ControlsExtensions;
using SaturnLog.UI.UITasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI
{
    public partial class MainForm : Form
    {
        #region Event Handlers
        private void btnDecreaseConsoleFont_Click(object sender, EventArgs e)
        {
            this.rtbLogs.SelectAll();

            FontFamily currentFontFamily = this.rtbLogs.SelectionFont.FontFamily;
            float currentFontSize = this.rtbLogs.SelectionFont.Size;

            if (currentFontSize > 3)
                currentFontSize--;

            this.rtbLogs.SelectionFont = new Font(currentFontFamily, currentFontSize);
        }

        private void btnIncreaseConsoleFont_Click(object sender, EventArgs e)
        {
            this.rtbLogs.SelectAll();

            FontFamily currentFontFamily = this.rtbLogs.SelectionFont.FontFamily;
            float currentFontSize = this.rtbLogs.SelectionFont.Size;

            if (currentFontSize < 25)
                currentFontSize++;

            this.rtbLogs.SelectionFont = new Font(currentFontFamily, currentFontSize);
        }

        // Assures that main logs console text box scroll down automatically on input.
        private void rtbLogs_TextChanged(object sender, EventArgs e)
        {
            rtbLogs.SelectionStart = rtbLogs.Text.Length;
            rtbLogs.ScrollToCaret();
        }

        #region Buttons Event Handlers - UITasks trigger points
        private async void btnClose_Click(object sender, EventArgs e)
        {
            if (this._app?.DBConnected == false)
            {
                this.Close();
            }
            else
            {
                //this._connectUITask.Cancel(sender, e);
                this._disconnectUITask.Trigger(sender, e);

                while (this._app?.DBConnected != false)
                    await Task.Delay(500);

                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this._consolesServices.OnCancelButtonPressed(sender, e);

            this._activeUITask.Cancel(sender, e);
        }

        #region PreBrief Tab
        // Triggers process of allocating Saturn 5 unit to the user using unit serial number.
        private void btnAllocateSaturn5BySerialNumber_Click(object sender, System.EventArgs e)
        {
            this._activeUITask = this._allocateSaturn5BySerialNumberUITask;

            this._consolesServices.OnAllocateSaturn5BySerialNumber_ButtonPressed(sender, e);
            this._controlsEnabler.OnAllocateSaturn5BySerialNumber_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        // Triggers process of allocating Saturn 5 unit to the user using unit barcode/short id.
        private void btnAllocateSaturn5ByShortId_Click(object sender, System.EventArgs e)
        {
            this._activeUITask = this._allocateSaturn5ByShortIdUITask;

            this._consolesServices.OnAllocateSaturn5ByShortId_ButtonPressed(sender, e);
            this._controlsEnabler.OnAllocateSaturn5ByShortId_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        // Triggers process of emergency allocating Saturn 5 unit to the user using unit serial number.
        private void btnEmergencyAllocateSaturn5BySerialNumber_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._emergencyAllocateSaturn5BySerialNumberUITask;

            this._consolesServices.OnEmergencyAllocateSaturn5BySerialNumber_ButtonPressed(sender, e);
            this._controlsEnabler.OnEmergencyAllocateSaturn5BySerialNumber_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }
        #endregion

        #region DeBreif Tab
        // Triggers process of confirming Saturn 5 unit in the depot using unit serial number.
        private void btnConfrimSaturn5BySerialNumber_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._confirmBackInSaturn5BySerialNumber;

            this._consolesServices.OnConfirmBackInSaturn5BySerialNumber_ButtonPressed(sender, e);
            this._controlsEnabler.OnConfirmBackInSaturn5BySerialNumber_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        // Triggers process of confirming Saturn 5 unit in the depot using unit short id.
        private void btnConfrimSaturn5ByShortId_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._confirmBackInSaturn5ByShortId;

            this._consolesServices.OnConfirmBackInSaturn5ByShortId_ButtonPressed(sender, e);
            this._controlsEnabler.OnConfirmBackInSaturn5ByShortId_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        // Triggers process of reporting Saturn 5 unit fault simultaneously confirming it back in the depot using unit serial number.
        private void btnConfrimFaultySaturn5BySerialNumber_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._confirmBackInFaultySaturn5BySerialNumber;

            this._consolesServices.OnConfirmBackInFaultySaturn5BySerialNumber_ButtonPressed(sender, e);
            this._controlsEnabler.OnConfirmBackInFaultySaturn5BySerialNumber_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        // Triggers process of reporting Saturn 5 unit fault simultaneously confirming it back in the depot using unit short id.
        private void btnConfrimDamagedSaturn5BySerialNumber_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._confirmBackInDamageSaturn5BySerialNumber;

            this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_ButtonPressed(sender, e);
            this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }
        #endregion

        #region Options Tab
        // Triggers process of connecting into the database.
        private void btnConnect_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._connectUITask;

            this._consolesServices.OnConnect_ButtonPressed(sender, e);
            this._controlsEnabler.OnConnect_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._disconnectUITask;

            this._consolesServices.OnDisconnect_ButtonPressed(sender, e);
            this._controlsEnabler.OnDisconnect_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        // Triggers process of signing user.
        private void btnSignIn_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._logInUITask;

            this._consolesServices.OnLogIn_ButtonPressed(sender, e);
            this._controlsEnabler.OnLogIn_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        // Triggers process of signing user out.
        private void btnSignOut_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._logOutUITask;

            this._consolesServices.OnLogOut_ButtonPressed(sender, e);
            this._controlsEnabler.OnLogOut_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }
        #endregion

        #region Saturn5 Stock Management Tab
        private void btnSaturn5SMReceiveFromIT_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._saturn5ReceiveFromITUITask;

            this._consolesServices.OnSaturn5ReceiveFromIT_ButtonPressed(sender, e);
            this._controlsEnabler.OnSaturn5ReceiveFromIT_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnSaturn5SMCreate_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._createSaturn5UITask;

            this._consolesServices.OnCreateSaturn5_ButtonPressed(sender, e);
            this._controlsEnabler.OnCreateSaturn5_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnSaturn5SMEdit_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._editSaturn5UITask;

            this._consolesServices.OnEditSaturn5_ButtonPressed(sender, e);
            this._controlsEnabler.OnEditSaturn5_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnSaturn5SMSendToIT_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._saturn5SendToITUITask;

            this._consolesServices.OnSaturn5SendToIT_ButtonPressed(sender, e);
            this._controlsEnabler.OnSaturn5SendToIT_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnSaturn5SMRemove_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._removeSaturn5UITask;

            this._consolesServices.OnRemoveSaturn5_ButtonPressed(sender, e);
            this._controlsEnabler.OnRemoveSaturn5_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnSaturn5SMReportFault_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._reportSaturn5FaultUITask;

            this._consolesServices.OnReportSaturn5Fault_ButtonPressed(sender, e);
            this._controlsEnabler.OnReportSaturn5Fault_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnSaturn5SMReportDamage_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._reportSaturn5DamageUITask;

            this._consolesServices.OnReportSaturn5Damage_ButtonPressed(sender, e);
            this._controlsEnabler.OnReportSaturn5Damage_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnSaturn5SMResolveIssue_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._resolveSaturn5IssueUITask;

            this._consolesServices.OnResolveSaturn5Issue_ButtonPressed(sender, e);
            this._controlsEnabler.OnResolveSaturn5Issue_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }
        #endregion

        #region Admin
        private void btnAdminCreateUser_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._createUserUITask;

            this._consolesServices.OnCreateUser_ButtonPressed(sender, e);
            this._controlsEnabler.OnCreateUser_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnAdminEditUser_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._editUserUITask;

            this._consolesServices.OnEditUser_ButtonPressed(sender, e);
            this._controlsEnabler.OnEditUser_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }

        private void btnAdminRemoveUser_Click(object sender, EventArgs e)
        {
            this._activeUITask = this._removeUserUITask;

            this._consolesServices.OnRemoveUser_ButtonPressed(sender, e);
            this._controlsEnabler.OnRemoveUser_ButtonPressed(sender, e);

            this._activeUITask.Trigger(sender, e);
        }
        #endregion
        #endregion
        #endregion

        #region Private fields
        // Domain layer facade and source point for all the domain level helper classes.
        private readonly App _app = new App();

        // Operates on 3 'consoles' text boxes based on the progress in the currently ongoing UITask
        private readonly ConsolesServices _consolesServices;

        // Helper class responsible for displaying specific user/saturn5 etc related data.
        private readonly DataDisplayServices _dataDisplayServices;

        // Enables/Disables tabs/buttons/textboxes based on the progress in the currently ongoing UITask
        private readonly ControlsEnabler _controlsEnabler;

        // Tab specific services
        private readonly PreBriefTabServices _preBriefTabServices;
        private readonly DeBriefTabServices _deBriefTabServices;
        private readonly OptionsTabServices _optionsTabServices;
        private readonly Saturn5StockManagementTabServices _saturn5StockManagementTabServices;
        private readonly AdminTabServices _adminTabServices;

        #region UITasks - Logic of the UI operations sequence
        private IUITask<EventArgs> _activeUITask;

        // Pre-Brief
        private readonly AllocateSaturn5BySerialNumberUITask _allocateSaturn5BySerialNumberUITask;
        private readonly AllocateSaturn5ByShortIdUITask _allocateSaturn5ByShortIdUITask;
        private readonly EmergencyAllocateSaturn5BySerialNumberUITask _emergencyAllocateSaturn5BySerialNumberUITask;

        // De-Brief
        private readonly ConfirmBackInSaturn5BySerialNumberUITask _confirmBackInSaturn5BySerialNumber;
        private readonly ConfirmBackInSaturn5ByShortIdUITask _confirmBackInSaturn5ByShortId;

        private readonly ConfirmBackInFaultySaturn5BySerialNumberUITask _confirmBackInFaultySaturn5BySerialNumber;
        private readonly ConfirmBackInDamageSaturn5BySerialNumberUITask _confirmBackInDamageSaturn5BySerialNumber;

        // Options
        private readonly ConnectUITask _connectUITask;
        private readonly DisconnectUITask _disconnectUITask;
        private readonly LogInUITask _logInUITask;
        private readonly LogOutUITask _logOutUITask;

        // Saturn5 Stock Management
        private readonly Saturn5ReceiveFromITUITask _saturn5ReceiveFromITUITask;
        private readonly CreateSaturn5UITask _createSaturn5UITask;
        private readonly EditSaturn5UITask _editSaturn5UITask;
        private readonly Saturn5SendToITUITask _saturn5SendToITUITask;
        private readonly RemoveSaturn5UITask _removeSaturn5UITask;
                         
        private readonly ReportSaturn5FaultUITask _reportSaturn5FaultUITask;
        private readonly ReportSaturn5DamageUITask _reportSaturn5DamageUITask;
        private readonly ResolveSaturn5IssueUITask _resolveSaturn5IssueUITask;
                         
        // Admin                                                                                                                                             
        private readonly CreateUserUITask _createUserUITask;
        private readonly EditUserUITask _editUserUITask;
        private readonly RemoveUserUITask _removeUserUITask;
        #endregion

        #region Text boxes functions sources
        private readonly ValidationFunctionsSource _validationFunctionsSource;
        private readonly InputProvidedEArgsFunctionsSource _inputProvidedEArgsFunctionsSource;
        #endregion
        #endregion

        #region Constructor - Entry point
        public MainForm()
        {
            // Pre- MainForm.InitializeComponent();

            // Instantiate sources of input validation, and inputProvidedEArgs creation
            // functions used by InputValidating derived text boxes.
            this._validationFunctionsSource = new ValidationFunctionsSource(this);
            this._inputProvidedEArgsFunctionsSource = new InputProvidedEArgsFunctionsSource(this);

            // Instantiate general helpers classes
            this._consolesServices = new ConsolesServices(this);
            this._dataDisplayServices = new DataDisplayServices(this);
            this._controlsEnabler = new ControlsEnabler(this);

            // Instantiate tab specific helpers classes
            this._preBriefTabServices = new PreBriefTabServices(this);
            this._deBriefTabServices = new DeBriefTabServices(this);
            this._optionsTabServices = new OptionsTabServices(this);
            this._saturn5StockManagementTabServices = new Saturn5StockManagementTabServices(this);
            this._adminTabServices = new AdminTabServices(this);

            // Instantiate UITasks
            this._allocateSaturn5BySerialNumberUITask = new AllocateSaturn5BySerialNumberUITask(this);
            this._allocateSaturn5ByShortIdUITask = new AllocateSaturn5ByShortIdUITask(this);
            this._emergencyAllocateSaturn5BySerialNumberUITask = new EmergencyAllocateSaturn5BySerialNumberUITask(this);

            this._confirmBackInSaturn5BySerialNumber = new ConfirmBackInSaturn5BySerialNumberUITask(this);
            this._confirmBackInSaturn5ByShortId = new ConfirmBackInSaturn5ByShortIdUITask(this);
            this._confirmBackInFaultySaturn5BySerialNumber = new ConfirmBackInFaultySaturn5BySerialNumberUITask(this);
            this._confirmBackInDamageSaturn5BySerialNumber = new ConfirmBackInDamageSaturn5BySerialNumberUITask(this);

            this._connectUITask = new ConnectUITask(this);
            this._disconnectUITask = new DisconnectUITask(this);
            this._logInUITask = new LogInUITask(this);
            this._logOutUITask = new LogOutUITask(this);

            this._saturn5ReceiveFromITUITask = new Saturn5ReceiveFromITUITask(this);
            this._createSaturn5UITask = new CreateSaturn5UITask(this);
            this._editSaturn5UITask = new EditSaturn5UITask(this);
            this._saturn5SendToITUITask = new Saturn5SendToITUITask(this);
            this._removeSaturn5UITask = new RemoveSaturn5UITask(this);

            this._reportSaturn5FaultUITask = new ReportSaturn5FaultUITask(this);
            this._reportSaturn5DamageUITask = new ReportSaturn5DamageUITask(this);
            this._resolveSaturn5IssueUITask = new ResolveSaturn5IssueUITask(this);

            this._createUserUITask = new CreateUserUITask(this);
            this._editUserUITask = new EditUserUITask(this);
            this._removeUserUITask = new RemoveUserUITask(this);

            // Initialize Designer generated components.
            this.InitializeComponent();
            
            // Past- MainForm.InitializeComponent();

            // Assign input validation, and inputProvidedEArgs creation
            // functions used by InputValidating derived text boxes.
            this.AssignInputValidatingTextBoxesFunctions();

            // On application started. Print appropriate informations to the user...
            this._consolesServices.OnApplicationStarted(this, System.EventArgs.Empty);
            // .. as well as assure that appropriate controls are enabled.
            this._controlsEnabler.OnApplicationStarted(this, System.EventArgs.Empty);

            // Assures that options tab is selected on application start.
            this.tcMain.SelectedTab = tpOptions;
        }
        #endregion

        #region Private Helpers
        // Assign input validation, and inputProvidedEArgs creation
        // functions used by InputValidating derived text boxes.
        private void AssignInputValidatingTextBoxesFunctions()
        {
            this._preBriefTabServices.AssignTextBoxesValidationFunctions();
            this._preBriefTabServices.AssignTextBoxesInputProvidedEArgsCreationFunctions();

            this._deBriefTabServices.AssignTextBoxesValidationFunctions();
            this._deBriefTabServices.AssignTextBoxesInputProvidedEArgsCreationFunctions();
        
            this._optionsTabServices.AssignTextBoxesValidationFunctions();
            this._optionsTabServices.AssignTextBoxesInputProvidedEArgsCreationFunctions();

            this._saturn5StockManagementTabServices.AssignTextBoxesValidationFunctions();
            this._saturn5StockManagementTabServices.AssignTextBoxesInputProvidedEArgsCreationFunctions();

            this._adminTabServices.AssignTextBoxesValidationFunctions();
            this._adminTabServices.AssignTextBoxesInputProvidedEArgsCreationFunctions();
        }
        #endregion
    }
}
