namespace SaturnLog.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.rtbCurrently = new System.Windows.Forms.RichTextBox();
            this.lblLogs = new System.Windows.Forms.Label();
            this.rtbDBStatus = new System.Windows.Forms.RichTextBox();
            this.lblCurrently = new System.Windows.Forms.Label();
            this.lblDBStatus = new System.Windows.Forms.Label();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpPreBrief = new System.Windows.Forms.TabPage();
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP = new System.Windows.Forms.Button();
            this.btnEmergencyAllocateSaturn5BySerialNumber = new System.Windows.Forms.Button();
            this.tbxPreBriefUserType = new System.Windows.Forms.TextBox();
            this.rtbPreBriefInfo = new System.Windows.Forms.RichTextBox();
            this.lblPreBriefSaturn5 = new System.Windows.Forms.Label();
            this.tbxPreBriefSaturn5Barcode = new SaturnLog.UI.UserWithSaturn5ShortIdValidatingTextBox();
            this.tbxPreBriefSaturn5SerialNumber = new SaturnLog.UI.UserWithSaturn5SerialNumberValidatingTextBox();
            this.lblPreBriefUser = new System.Windows.Forms.Label();
            this.tbxPreBriefUserSurname = new System.Windows.Forms.TextBox();
            this.tbxPreBriefUserFirstName = new System.Windows.Forms.TextBox();
            this.tbxPreBriefUserUsername = new SaturnLog.UI.UserUsernameValidatingTextBox();
            this.lblPreBriefOperations = new System.Windows.Forms.Label();
            this.btnAllocateSaturn5ToBySerialnumberHELP = new System.Windows.Forms.Button();
            this.btnAllocateSaturn5ByShortIdHELP = new System.Windows.Forms.Button();
            this.btnAllocateSaturn5BySerialNumber = new System.Windows.Forms.Button();
            this.btnAllocateSaturn5ByShortId = new System.Windows.Forms.Button();
            this.tpDeBrief = new System.Windows.Forms.TabPage();
            this.tbxDeBriefUserType = new System.Windows.Forms.TextBox();
            this.rtbDeBriefInfo = new System.Windows.Forms.RichTextBox();
            this.lblDeBriefSaturn5 = new System.Windows.Forms.Label();
            this.lblDeBriefUser = new System.Windows.Forms.Label();
            this.tbxDeBriefUserFirstName = new System.Windows.Forms.TextBox();
            this.tbxDeBriefUserSurname = new System.Windows.Forms.TextBox();
            this.tbxDeBriefUserUsername = new System.Windows.Forms.TextBox();
            this.lblDeBriefOperations = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.btnConfrimDamagedSaturn5BySerialNumber = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnConfrimFaultySaturn5BySerialNumber = new System.Windows.Forms.Button();
            this.btnConfrimSaturn5BySerialNumberHELP = new System.Windows.Forms.Button();
            this.btnConfrimSaturn5BySerialNumber = new System.Windows.Forms.Button();
            this.btnConfirmSaturn5BackInShortIdHELP = new System.Windows.Forms.Button();
            this.btnConfrimSaturn5ByShortId = new System.Windows.Forms.Button();
            this.tbxDeBriefSaturn5Barcode = new SaturnLog.UI.Saturn5ShortIdValidatingTextBox();
            this.tbxDeBriefSaturn5SerialNumber = new SaturnLog.UI.Saturn5SerialNumberValidatingTextBox();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.btnDisconnectHELP = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnIncreaseConsoleFont = new System.Windows.Forms.Button();
            this.btnIncreaseConsoleFontHELP = new System.Windows.Forms.Button();
            this.btnDecreaseConsoleFont = new System.Windows.Forms.Button();
            this.btnDecreaseConsoleFontHELP = new System.Windows.Forms.Button();
            this.lblOptionsAccess = new System.Windows.Forms.Label();
            this.btnSignOutHELP = new System.Windows.Forms.Button();
            this.btnSignInHELP = new System.Windows.Forms.Button();
            this.btnConnectHELP = new System.Windows.Forms.Button();
            this.lblOptionsSaturn5 = new System.Windows.Forms.Label();
            this.rtbOptionsInfo = new System.Windows.Forms.RichTextBox();
            this.tbxOptionsUserType = new System.Windows.Forms.TextBox();
            this.lblOptionsUser = new System.Windows.Forms.Label();
            this.tbxOptionsUserFirstName = new System.Windows.Forms.TextBox();
            this.tbxOptionsUserSurname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOptionsConnectDisconnect = new System.Windows.Forms.Label();
            this.btnSignOut = new System.Windows.Forms.Button();
            this.btnSignIn = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbxOptionsSaturn5Barcode = new System.Windows.Forms.TextBox();
            this.tbxOptionsSaturn5SerialNumber = new System.Windows.Forms.TextBox();
            this.tbxOptionsUserUsername = new SaturnLog.UI.UserUsernameValidatingTextBox();
            this.tpSaturn5StockManagement = new System.Windows.Forms.TabPage();
            this.lblSaturn5Stock = new System.Windows.Forms.Label();
            this.lblSaturn5SMIssues = new System.Windows.Forms.Label();
            this.btnSaturn5SMReportFaultHelp = new System.Windows.Forms.Button();
            this.btnSaturn5SMResolveIssueHELP = new System.Windows.Forms.Button();
            this.btnSaturn5SMReportDamageHELP = new System.Windows.Forms.Button();
            this.btnSaturn5SMResolveIssue = new System.Windows.Forms.Button();
            this.btnSaturn5SMReportDamage = new System.Windows.Forms.Button();
            this.btnSaturn5SMReportFault = new System.Windows.Forms.Button();
            this.rtbSaturn5SMInfo = new System.Windows.Forms.RichTextBox();
            this.lblSaturn5SMSaturn5 = new System.Windows.Forms.Label();
            this.btnSaturn5SMRemoveHELP = new System.Windows.Forms.Button();
            this.btnSaturn5SMSendToITHELP = new System.Windows.Forms.Button();
            this.btnSaturn5SMEditHELP = new System.Windows.Forms.Button();
            this.btnSaturn5SMCreateHelp = new System.Windows.Forms.Button();
            this.btnSaturn5SMReceiveFromITHELP = new System.Windows.Forms.Button();
            this.btnSaturn5SMReceiveFromIT = new System.Windows.Forms.Button();
            this.btnSaturn5SMSendToIT = new System.Windows.Forms.Button();
            this.btnSaturn5SMRemove = new System.Windows.Forms.Button();
            this.btnSaturn5SMEdit = new System.Windows.Forms.Button();
            this.btnSaturn5SMCreate = new System.Windows.Forms.Button();
            this.tbxSaturn5SMBarcode = new SaturnLog.UI.Saturn5ShortIdValidatingTextBox();
            this.tbxSaturn5SMSerialNumber = new SaturnLog.UI.Saturn5SerialNumberValidatingTextBox();
            this.tpAdmin = new System.Windows.Forms.TabPage();
            this.lblAdminUsers = new System.Windows.Forms.Label();
            this.rtbAdminInfo = new System.Windows.Forms.RichTextBox();
            this.tbxAdminUserType = new System.Windows.Forms.TextBox();
            this.lblAdminUser = new System.Windows.Forms.Label();
            this.tbxAdminUserFirstName = new System.Windows.Forms.TextBox();
            this.tbxAdminUserSurname = new System.Windows.Forms.TextBox();
            this.btnAdminRemoveUserHELP = new System.Windows.Forms.Button();
            this.btnAdminEditUserHELP = new System.Windows.Forms.Button();
            this.btnAdminCreateUserHELP = new System.Windows.Forms.Button();
            this.btnAdminRemoveUser = new System.Windows.Forms.Button();
            this.btnAdminEditUser = new System.Windows.Forms.Button();
            this.btnAdminCreateUser = new System.Windows.Forms.Button();
            this.tbxAdminUserUsername = new SaturnLog.UI.UserUsernameValidatingTextBox();
            this.lblDoNow = new System.Windows.Forms.Label();
            this.rtbDoNow = new System.Windows.Forms.RichTextBox();
            this.rtbLoggedUser = new System.Windows.Forms.RichTextBox();
            this.lblUserStatus = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancelHELP = new System.Windows.Forms.Button();
            this.btnCloseHELP = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpPreBrief.SuspendLayout();
            this.tpDeBrief.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.tpSaturn5StockManagement.SuspendLayout();
            this.tpAdmin.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbLogs
            // 
            this.rtbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLogs.BackColor = System.Drawing.Color.White;
            this.rtbLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rtbLogs.Location = new System.Drawing.Point(23, 74);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ReadOnly = true;
            this.rtbLogs.Size = new System.Drawing.Size(961, 243);
            this.rtbLogs.TabIndex = 0;
            this.rtbLogs.Text = "";
            this.rtbLogs.TextChanged += new System.EventHandler(this.rtbLogs_TextChanged);
            // 
            // rtbCurrently
            // 
            this.rtbCurrently.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbCurrently.BackColor = System.Drawing.Color.White;
            this.rtbCurrently.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.rtbCurrently.Location = new System.Drawing.Point(23, 338);
            this.rtbCurrently.Name = "rtbCurrently";
            this.rtbCurrently.ReadOnly = true;
            this.rtbCurrently.Size = new System.Drawing.Size(961, 30);
            this.rtbCurrently.TabIndex = 1;
            this.rtbCurrently.Text = "";
            // 
            // lblLogs
            // 
            this.lblLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogs.AutoSize = true;
            this.lblLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogs.Location = new System.Drawing.Point(20, 56);
            this.lblLogs.Name = "lblLogs";
            this.lblLogs.Size = new System.Drawing.Size(37, 15);
            this.lblLogs.TabIndex = 2;
            this.lblLogs.Text = "Logs:";
            // 
            // rtbDBStatus
            // 
            this.rtbDBStatus.BackColor = System.Drawing.SystemColors.Control;
            this.rtbDBStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDBStatus.Enabled = false;
            this.rtbDBStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDBStatus.ForeColor = System.Drawing.Color.Red;
            this.rtbDBStatus.Location = new System.Drawing.Point(23, 28);
            this.rtbDBStatus.Name = "rtbDBStatus";
            this.rtbDBStatus.ReadOnly = true;
            this.rtbDBStatus.Size = new System.Drawing.Size(493, 25);
            this.rtbDBStatus.TabIndex = 3;
            this.rtbDBStatus.Text = "NOT CONNECTED.";
            // 
            // lblCurrently
            // 
            this.lblCurrently.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrently.AutoSize = true;
            this.lblCurrently.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrently.Location = new System.Drawing.Point(20, 320);
            this.lblCurrently.Name = "lblCurrently";
            this.lblCurrently.Size = new System.Drawing.Size(58, 15);
            this.lblCurrently.TabIndex = 4;
            this.lblCurrently.Text = "Currently:";
            // 
            // lblDBStatus
            // 
            this.lblDBStatus.AutoSize = true;
            this.lblDBStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBStatus.Location = new System.Drawing.Point(20, 10);
            this.lblDBStatus.Name = "lblDBStatus";
            this.lblDBStatus.Size = new System.Drawing.Size(64, 15);
            this.lblDBStatus.TabIndex = 5;
            this.lblDBStatus.Text = "DB Status:";
            // 
            // tcMain
            // 
            this.tcMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMain.Controls.Add(this.tpPreBrief);
            this.tcMain.Controls.Add(this.tpDeBrief);
            this.tcMain.Controls.Add(this.tpOptions);
            this.tcMain.Controls.Add(this.tpSaturn5StockManagement);
            this.tcMain.Controls.Add(this.tpAdmin);
            this.tcMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMain.Location = new System.Drawing.Point(23, 432);
            this.tcMain.Margin = new System.Windows.Forms.Padding(10);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.Padding = new System.Drawing.Point(25, 3);
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(961, 284);
            this.tcMain.TabIndex = 6;
            // 
            // tpPreBrief
            // 
            this.tpPreBrief.Controls.Add(this.btnEmergencyAllocateSaturn5BySerialNumberHELP);
            this.tpPreBrief.Controls.Add(this.btnEmergencyAllocateSaturn5BySerialNumber);
            this.tpPreBrief.Controls.Add(this.tbxPreBriefUserType);
            this.tpPreBrief.Controls.Add(this.rtbPreBriefInfo);
            this.tpPreBrief.Controls.Add(this.lblPreBriefSaturn5);
            this.tpPreBrief.Controls.Add(this.tbxPreBriefSaturn5Barcode);
            this.tpPreBrief.Controls.Add(this.tbxPreBriefSaturn5SerialNumber);
            this.tpPreBrief.Controls.Add(this.lblPreBriefUser);
            this.tpPreBrief.Controls.Add(this.tbxPreBriefUserSurname);
            this.tpPreBrief.Controls.Add(this.tbxPreBriefUserFirstName);
            this.tpPreBrief.Controls.Add(this.tbxPreBriefUserUsername);
            this.tpPreBrief.Controls.Add(this.lblPreBriefOperations);
            this.tpPreBrief.Controls.Add(this.btnAllocateSaturn5ToBySerialnumberHELP);
            this.tpPreBrief.Controls.Add(this.btnAllocateSaturn5ByShortIdHELP);
            this.tpPreBrief.Controls.Add(this.btnAllocateSaturn5BySerialNumber);
            this.tpPreBrief.Controls.Add(this.btnAllocateSaturn5ByShortId);
            this.tpPreBrief.Location = new System.Drawing.Point(4, 27);
            this.tpPreBrief.Name = "tpPreBrief";
            this.tpPreBrief.Padding = new System.Windows.Forms.Padding(3);
            this.tpPreBrief.Size = new System.Drawing.Size(953, 253);
            this.tpPreBrief.TabIndex = 0;
            this.tpPreBrief.Text = "Pre-Brief";
            this.tpPreBrief.UseVisualStyleBackColor = true;
            // 
            // btnEmergencyAllocateSaturn5BySerialNumberHELP
            // 
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP.Location = new System.Drawing.Point(905, 103);
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP.Name = "btnEmergencyAllocateSaturn5BySerialNumberHELP";
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP.Size = new System.Drawing.Size(30, 30);
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP.TabIndex = 37;
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP.Text = "?";
            this.btnEmergencyAllocateSaturn5BySerialNumberHELP.UseVisualStyleBackColor = true;
            // 
            // btnEmergencyAllocateSaturn5BySerialNumber
            // 
            this.btnEmergencyAllocateSaturn5BySerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEmergencyAllocateSaturn5BySerialNumber.BackColor = System.Drawing.Color.Magenta;
            this.btnEmergencyAllocateSaturn5BySerialNumber.Enabled = false;
            this.btnEmergencyAllocateSaturn5BySerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmergencyAllocateSaturn5BySerialNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEmergencyAllocateSaturn5BySerialNumber.Location = new System.Drawing.Point(450, 103);
            this.btnEmergencyAllocateSaturn5BySerialNumber.Name = "btnEmergencyAllocateSaturn5BySerialNumber";
            this.btnEmergencyAllocateSaturn5BySerialNumber.Size = new System.Drawing.Size(449, 30);
            this.btnEmergencyAllocateSaturn5BySerialNumber.TabIndex = 36;
            this.btnEmergencyAllocateSaturn5BySerialNumber.Text = "Emergency Allocate Saturn5 unit to User by SERIAL NUMBER";
            this.btnEmergencyAllocateSaturn5BySerialNumber.UseVisualStyleBackColor = false;
            this.btnEmergencyAllocateSaturn5BySerialNumber.Click += new System.EventHandler(this.btnEmergencyAllocateSaturn5BySerialNumber_Click);
            // 
            // tbxPreBriefUserType
            // 
            this.tbxPreBriefUserType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxPreBriefUserType.BackColor = System.Drawing.Color.White;
            this.tbxPreBriefUserType.Enabled = false;
            this.tbxPreBriefUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxPreBriefUserType.Location = new System.Drawing.Point(223, 31);
            this.tbxPreBriefUserType.Name = "tbxPreBriefUserType";
            this.tbxPreBriefUserType.ReadOnly = true;
            this.tbxPreBriefUserType.Size = new System.Drawing.Size(204, 24);
            this.tbxPreBriefUserType.TabIndex = 35;
            // 
            // rtbPreBriefInfo
            // 
            this.rtbPreBriefInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rtbPreBriefInfo.BackColor = System.Drawing.Color.White;
            this.rtbPreBriefInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.rtbPreBriefInfo.Location = new System.Drawing.Point(13, 150);
            this.rtbPreBriefInfo.Name = "rtbPreBriefInfo";
            this.rtbPreBriefInfo.ReadOnly = true;
            this.rtbPreBriefInfo.Size = new System.Drawing.Size(414, 89);
            this.rtbPreBriefInfo.TabIndex = 34;
            this.rtbPreBriefInfo.Text = "";
            // 
            // lblPreBriefSaturn5
            // 
            this.lblPreBriefSaturn5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPreBriefSaturn5.AutoSize = true;
            this.lblPreBriefSaturn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblPreBriefSaturn5.Location = new System.Drawing.Point(10, 93);
            this.lblPreBriefSaturn5.Name = "lblPreBriefSaturn5";
            this.lblPreBriefSaturn5.Size = new System.Drawing.Size(435, 18);
            this.lblPreBriefSaturn5.TabIndex = 33;
            this.lblPreBriefSaturn5.Text = "Saturn5 (Serial Number / Barcode / Existing Issues or Damages):";
            // 
            // tbxPreBriefSaturn5Barcode
            // 
            this.tbxPreBriefSaturn5Barcode.AllowEmptyInput = false;
            this.tbxPreBriefSaturn5Barcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxPreBriefSaturn5Barcode.BackColor = System.Drawing.Color.White;
            this.tbxPreBriefSaturn5Barcode.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxPreBriefSaturn5Barcode.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxPreBriefSaturn5Barcode.ClearOnEnable = true;
            this.tbxPreBriefSaturn5Barcode.DisableOnEmptyInput = false;
            this.tbxPreBriefSaturn5Barcode.DisableOnInvalidInput = false;
            this.tbxPreBriefSaturn5Barcode.DisableOnValidInput = true;
            this.tbxPreBriefSaturn5Barcode.Enabled = false;
            this.tbxPreBriefSaturn5Barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxPreBriefSaturn5Barcode.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxPreBriefSaturn5Barcode.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxPreBriefSaturn5Barcode.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxPreBriefSaturn5Barcode.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxPreBriefSaturn5Barcode.GetOtherEArgsCreationParamFunc = null;
            this.tbxPreBriefSaturn5Barcode.InputProvidedEArgsCreationFunc = null;
            this.tbxPreBriefSaturn5Barcode.Location = new System.Drawing.Point(223, 114);
            this.tbxPreBriefSaturn5Barcode.Name = "tbxPreBriefSaturn5Barcode";
            this.tbxPreBriefSaturn5Barcode.Size = new System.Drawing.Size(204, 24);
            this.tbxPreBriefSaturn5Barcode.TabIndex = 32;
            this.tbxPreBriefSaturn5Barcode.ValidationFunc = null;
            this.tbxPreBriefSaturn5Barcode.ValueIsEmptyWatermark = "Must Not Be Empty";
            this.tbxPreBriefSaturn5Barcode.ValueIsInvalidWatermark = "Must Be Valid";
            this.tbxPreBriefSaturn5Barcode.Watermark = "Barcode/Short Id";
            // 
            // tbxPreBriefSaturn5SerialNumber
            // 
            this.tbxPreBriefSaturn5SerialNumber.AllowEmptyInput = false;
            this.tbxPreBriefSaturn5SerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxPreBriefSaturn5SerialNumber.BackColor = System.Drawing.Color.White;
            this.tbxPreBriefSaturn5SerialNumber.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxPreBriefSaturn5SerialNumber.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxPreBriefSaturn5SerialNumber.ClearOnEnable = true;
            this.tbxPreBriefSaturn5SerialNumber.DisableOnEmptyInput = false;
            this.tbxPreBriefSaturn5SerialNumber.DisableOnInvalidInput = false;
            this.tbxPreBriefSaturn5SerialNumber.DisableOnValidInput = true;
            this.tbxPreBriefSaturn5SerialNumber.Enabled = false;
            this.tbxPreBriefSaturn5SerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxPreBriefSaturn5SerialNumber.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxPreBriefSaturn5SerialNumber.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxPreBriefSaturn5SerialNumber.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxPreBriefSaturn5SerialNumber.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxPreBriefSaturn5SerialNumber.GetOtherEArgsCreationParamFunc = null;
            this.tbxPreBriefSaturn5SerialNumber.InputProvidedEArgsCreationFunc = null;
            this.tbxPreBriefSaturn5SerialNumber.Location = new System.Drawing.Point(13, 114);
            this.tbxPreBriefSaturn5SerialNumber.Name = "tbxPreBriefSaturn5SerialNumber";
            this.tbxPreBriefSaturn5SerialNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxPreBriefSaturn5SerialNumber.TabIndex = 30;
            this.tbxPreBriefSaturn5SerialNumber.ValidationFunc = null;
            this.tbxPreBriefSaturn5SerialNumber.ValueIsEmptyWatermark = "Must Not Be Empty";
            this.tbxPreBriefSaturn5SerialNumber.ValueIsInvalidWatermark = "Must Be Valid";
            this.tbxPreBriefSaturn5SerialNumber.Watermark = "Serial Number";
            // 
            // lblPreBriefUser
            // 
            this.lblPreBriefUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPreBriefUser.AutoSize = true;
            this.lblPreBriefUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblPreBriefUser.Location = new System.Drawing.Point(10, 10);
            this.lblPreBriefUser.Name = "lblPreBriefUser";
            this.lblPreBriefUser.Size = new System.Drawing.Size(271, 18);
            this.lblPreBriefUser.TabIndex = 29;
            this.lblPreBriefUser.Text = "User (username / first name / surname):";
            // 
            // tbxPreBriefUserSurname
            // 
            this.tbxPreBriefUserSurname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxPreBriefUserSurname.BackColor = System.Drawing.Color.White;
            this.tbxPreBriefUserSurname.Enabled = false;
            this.tbxPreBriefUserSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxPreBriefUserSurname.Location = new System.Drawing.Point(223, 61);
            this.tbxPreBriefUserSurname.Name = "tbxPreBriefUserSurname";
            this.tbxPreBriefUserSurname.ReadOnly = true;
            this.tbxPreBriefUserSurname.Size = new System.Drawing.Size(204, 24);
            this.tbxPreBriefUserSurname.TabIndex = 28;
            // 
            // tbxPreBriefUserFirstName
            // 
            this.tbxPreBriefUserFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxPreBriefUserFirstName.BackColor = System.Drawing.Color.White;
            this.tbxPreBriefUserFirstName.Enabled = false;
            this.tbxPreBriefUserFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxPreBriefUserFirstName.Location = new System.Drawing.Point(13, 61);
            this.tbxPreBriefUserFirstName.Name = "tbxPreBriefUserFirstName";
            this.tbxPreBriefUserFirstName.ReadOnly = true;
            this.tbxPreBriefUserFirstName.Size = new System.Drawing.Size(204, 24);
            this.tbxPreBriefUserFirstName.TabIndex = 27;
            // 
            // tbxPreBriefUserUsername
            // 
            this.tbxPreBriefUserUsername.AllowEmptyInput = false;
            this.tbxPreBriefUserUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxPreBriefUserUsername.BackColor = System.Drawing.Color.White;
            this.tbxPreBriefUserUsername.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxPreBriefUserUsername.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxPreBriefUserUsername.ClearOnEnable = true;
            this.tbxPreBriefUserUsername.DisableOnEmptyInput = false;
            this.tbxPreBriefUserUsername.DisableOnInvalidInput = false;
            this.tbxPreBriefUserUsername.DisableOnValidInput = true;
            this.tbxPreBriefUserUsername.Enabled = false;
            this.tbxPreBriefUserUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxPreBriefUserUsername.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxPreBriefUserUsername.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxPreBriefUserUsername.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxPreBriefUserUsername.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxPreBriefUserUsername.InputProvidedEArgsCreationFunc = null;
            this.tbxPreBriefUserUsername.Location = new System.Drawing.Point(13, 31);
            this.tbxPreBriefUserUsername.Name = "tbxPreBriefUserUsername";
            this.tbxPreBriefUserUsername.Size = new System.Drawing.Size(204, 24);
            this.tbxPreBriefUserUsername.TabIndex = 26;
            this.tbxPreBriefUserUsername.ValidationFunc = null;
            this.tbxPreBriefUserUsername.ValueIsEmptyWatermark = "Must Not Be Empty";
            this.tbxPreBriefUserUsername.ValueIsInvalidWatermark = "Must Be Valid";
            this.tbxPreBriefUserUsername.Watermark = "Username";
            // 
            // lblPreBriefOperations
            // 
            this.lblPreBriefOperations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreBriefOperations.AutoSize = true;
            this.lblPreBriefOperations.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblPreBriefOperations.Location = new System.Drawing.Point(446, 10);
            this.lblPreBriefOperations.Name = "lblPreBriefOperations";
            this.lblPreBriefOperations.Size = new System.Drawing.Size(147, 18);
            this.lblPreBriefOperations.TabIndex = 25;
            this.lblPreBriefOperations.Text = "Pre-Brief Operations:";
            // 
            // btnAllocateSaturn5ToBySerialnumberHELP
            // 
            this.btnAllocateSaturn5ToBySerialnumberHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllocateSaturn5ToBySerialnumberHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllocateSaturn5ToBySerialnumberHELP.Location = new System.Drawing.Point(904, 31);
            this.btnAllocateSaturn5ToBySerialnumberHELP.Name = "btnAllocateSaturn5ToBySerialnumberHELP";
            this.btnAllocateSaturn5ToBySerialnumberHELP.Size = new System.Drawing.Size(30, 30);
            this.btnAllocateSaturn5ToBySerialnumberHELP.TabIndex = 16;
            this.btnAllocateSaturn5ToBySerialnumberHELP.Text = "?";
            this.btnAllocateSaturn5ToBySerialnumberHELP.UseVisualStyleBackColor = true;
            // 
            // btnAllocateSaturn5ByShortIdHELP
            // 
            this.btnAllocateSaturn5ByShortIdHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllocateSaturn5ByShortIdHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllocateSaturn5ByShortIdHELP.Location = new System.Drawing.Point(904, 67);
            this.btnAllocateSaturn5ByShortIdHELP.Name = "btnAllocateSaturn5ByShortIdHELP";
            this.btnAllocateSaturn5ByShortIdHELP.Size = new System.Drawing.Size(30, 30);
            this.btnAllocateSaturn5ByShortIdHELP.TabIndex = 15;
            this.btnAllocateSaturn5ByShortIdHELP.Text = "?";
            this.btnAllocateSaturn5ByShortIdHELP.UseVisualStyleBackColor = true;
            // 
            // btnAllocateSaturn5BySerialNumber
            // 
            this.btnAllocateSaturn5BySerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllocateSaturn5BySerialNumber.Enabled = false;
            this.btnAllocateSaturn5BySerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllocateSaturn5BySerialNumber.Location = new System.Drawing.Point(449, 31);
            this.btnAllocateSaturn5BySerialNumber.Name = "btnAllocateSaturn5BySerialNumber";
            this.btnAllocateSaturn5BySerialNumber.Size = new System.Drawing.Size(449, 30);
            this.btnAllocateSaturn5BySerialNumber.TabIndex = 14;
            this.btnAllocateSaturn5BySerialNumber.Text = "Allocate Saturn5 unit to User by SERIAL NUMBER";
            this.btnAllocateSaturn5BySerialNumber.UseVisualStyleBackColor = true;
            this.btnAllocateSaturn5BySerialNumber.Click += new System.EventHandler(this.btnAllocateSaturn5BySerialNumber_Click);
            // 
            // btnAllocateSaturn5ByShortId
            // 
            this.btnAllocateSaturn5ByShortId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllocateSaturn5ByShortId.Enabled = false;
            this.btnAllocateSaturn5ByShortId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllocateSaturn5ByShortId.Location = new System.Drawing.Point(449, 67);
            this.btnAllocateSaturn5ByShortId.Name = "btnAllocateSaturn5ByShortId";
            this.btnAllocateSaturn5ByShortId.Size = new System.Drawing.Size(449, 30);
            this.btnAllocateSaturn5ByShortId.TabIndex = 13;
            this.btnAllocateSaturn5ByShortId.Text = "Allocate Saturn5 unit to User by BARCODE";
            this.btnAllocateSaturn5ByShortId.UseVisualStyleBackColor = true;
            this.btnAllocateSaturn5ByShortId.Click += new System.EventHandler(this.btnAllocateSaturn5ByShortId_Click);
            // 
            // tpDeBrief
            // 
            this.tpDeBrief.Controls.Add(this.tbxDeBriefUserType);
            this.tpDeBrief.Controls.Add(this.rtbDeBriefInfo);
            this.tpDeBrief.Controls.Add(this.lblDeBriefSaturn5);
            this.tpDeBrief.Controls.Add(this.lblDeBriefUser);
            this.tpDeBrief.Controls.Add(this.tbxDeBriefUserFirstName);
            this.tpDeBrief.Controls.Add(this.tbxDeBriefUserSurname);
            this.tpDeBrief.Controls.Add(this.tbxDeBriefUserUsername);
            this.tpDeBrief.Controls.Add(this.lblDeBriefOperations);
            this.tpDeBrief.Controls.Add(this.button7);
            this.tpDeBrief.Controls.Add(this.btnConfrimDamagedSaturn5BySerialNumber);
            this.tpDeBrief.Controls.Add(this.button2);
            this.tpDeBrief.Controls.Add(this.btnConfrimFaultySaturn5BySerialNumber);
            this.tpDeBrief.Controls.Add(this.btnConfrimSaturn5BySerialNumberHELP);
            this.tpDeBrief.Controls.Add(this.btnConfrimSaturn5BySerialNumber);
            this.tpDeBrief.Controls.Add(this.btnConfirmSaturn5BackInShortIdHELP);
            this.tpDeBrief.Controls.Add(this.btnConfrimSaturn5ByShortId);
            this.tpDeBrief.Controls.Add(this.tbxDeBriefSaturn5Barcode);
            this.tpDeBrief.Controls.Add(this.tbxDeBriefSaturn5SerialNumber);
            this.tpDeBrief.Location = new System.Drawing.Point(4, 27);
            this.tpDeBrief.Name = "tpDeBrief";
            this.tpDeBrief.Padding = new System.Windows.Forms.Padding(3);
            this.tpDeBrief.Size = new System.Drawing.Size(953, 253);
            this.tpDeBrief.TabIndex = 1;
            this.tpDeBrief.Text = "De-Brief";
            this.tpDeBrief.UseVisualStyleBackColor = true;
            // 
            // tbxDeBriefUserType
            // 
            this.tbxDeBriefUserType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxDeBriefUserType.BackColor = System.Drawing.Color.White;
            this.tbxDeBriefUserType.Enabled = false;
            this.tbxDeBriefUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxDeBriefUserType.Location = new System.Drawing.Point(223, 31);
            this.tbxDeBriefUserType.Name = "tbxDeBriefUserType";
            this.tbxDeBriefUserType.ReadOnly = true;
            this.tbxDeBriefUserType.Size = new System.Drawing.Size(204, 24);
            this.tbxDeBriefUserType.TabIndex = 42;
            // 
            // rtbDeBriefInfo
            // 
            this.rtbDeBriefInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rtbDeBriefInfo.BackColor = System.Drawing.Color.White;
            this.rtbDeBriefInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.rtbDeBriefInfo.Location = new System.Drawing.Point(13, 150);
            this.rtbDeBriefInfo.Name = "rtbDeBriefInfo";
            this.rtbDeBriefInfo.ReadOnly = true;
            this.rtbDeBriefInfo.Size = new System.Drawing.Size(411, 89);
            this.rtbDeBriefInfo.TabIndex = 41;
            this.rtbDeBriefInfo.Text = "";
            // 
            // lblDeBriefSaturn5
            // 
            this.lblDeBriefSaturn5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDeBriefSaturn5.AutoSize = true;
            this.lblDeBriefSaturn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblDeBriefSaturn5.Location = new System.Drawing.Point(10, 93);
            this.lblDeBriefSaturn5.Name = "lblDeBriefSaturn5";
            this.lblDeBriefSaturn5.Size = new System.Drawing.Size(435, 18);
            this.lblDeBriefSaturn5.TabIndex = 40;
            this.lblDeBriefSaturn5.Text = "Saturn5 (Serial Number / Barcode / Existing Issues or Damages):";
            // 
            // lblDeBriefUser
            // 
            this.lblDeBriefUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDeBriefUser.AutoSize = true;
            this.lblDeBriefUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblDeBriefUser.Location = new System.Drawing.Point(10, 10);
            this.lblDeBriefUser.Name = "lblDeBriefUser";
            this.lblDeBriefUser.Size = new System.Drawing.Size(271, 18);
            this.lblDeBriefUser.TabIndex = 37;
            this.lblDeBriefUser.Text = "User (username / first name / surname):";
            // 
            // tbxDeBriefUserFirstName
            // 
            this.tbxDeBriefUserFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxDeBriefUserFirstName.BackColor = System.Drawing.Color.White;
            this.tbxDeBriefUserFirstName.Enabled = false;
            this.tbxDeBriefUserFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxDeBriefUserFirstName.Location = new System.Drawing.Point(13, 61);
            this.tbxDeBriefUserFirstName.Name = "tbxDeBriefUserFirstName";
            this.tbxDeBriefUserFirstName.ReadOnly = true;
            this.tbxDeBriefUserFirstName.Size = new System.Drawing.Size(204, 24);
            this.tbxDeBriefUserFirstName.TabIndex = 36;
            // 
            // tbxDeBriefUserSurname
            // 
            this.tbxDeBriefUserSurname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxDeBriefUserSurname.BackColor = System.Drawing.Color.White;
            this.tbxDeBriefUserSurname.Enabled = false;
            this.tbxDeBriefUserSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxDeBriefUserSurname.Location = new System.Drawing.Point(223, 61);
            this.tbxDeBriefUserSurname.Name = "tbxDeBriefUserSurname";
            this.tbxDeBriefUserSurname.ReadOnly = true;
            this.tbxDeBriefUserSurname.Size = new System.Drawing.Size(204, 24);
            this.tbxDeBriefUserSurname.TabIndex = 35;
            // 
            // tbxDeBriefUserUsername
            // 
            this.tbxDeBriefUserUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxDeBriefUserUsername.BackColor = System.Drawing.Color.White;
            this.tbxDeBriefUserUsername.Enabled = false;
            this.tbxDeBriefUserUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxDeBriefUserUsername.Location = new System.Drawing.Point(13, 31);
            this.tbxDeBriefUserUsername.Name = "tbxDeBriefUserUsername";
            this.tbxDeBriefUserUsername.Size = new System.Drawing.Size(204, 24);
            this.tbxDeBriefUserUsername.TabIndex = 34;
            // 
            // lblDeBriefOperations
            // 
            this.lblDeBriefOperations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeBriefOperations.AutoSize = true;
            this.lblDeBriefOperations.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblDeBriefOperations.Location = new System.Drawing.Point(447, 10);
            this.lblDeBriefOperations.Name = "lblDeBriefOperations";
            this.lblDeBriefOperations.Size = new System.Drawing.Size(143, 18);
            this.lblDeBriefOperations.TabIndex = 24;
            this.lblDeBriefOperations.Text = "De-Brief Operations:";
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(905, 137);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(30, 30);
            this.button7.TabIndex = 23;
            this.button7.Text = "?";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // btnConfrimDamagedSaturn5BySerialNumber
            // 
            this.btnConfrimDamagedSaturn5BySerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfrimDamagedSaturn5BySerialNumber.BackColor = System.Drawing.Color.Red;
            this.btnConfrimDamagedSaturn5BySerialNumber.Enabled = false;
            this.btnConfrimDamagedSaturn5BySerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfrimDamagedSaturn5BySerialNumber.Location = new System.Drawing.Point(450, 137);
            this.btnConfrimDamagedSaturn5BySerialNumber.Name = "btnConfrimDamagedSaturn5BySerialNumber";
            this.btnConfrimDamagedSaturn5BySerialNumber.Size = new System.Drawing.Size(449, 30);
            this.btnConfrimDamagedSaturn5BySerialNumber.TabIndex = 22;
            this.btnConfrimDamagedSaturn5BySerialNumber.Text = "Confirm DAMAGED Saturn5 Unit back in Depot";
            this.btnConfrimDamagedSaturn5BySerialNumber.UseVisualStyleBackColor = false;
            this.btnConfrimDamagedSaturn5BySerialNumber.Click += new System.EventHandler(this.btnConfrimDamagedSaturn5BySerialNumber_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(905, 101);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(30, 30);
            this.button2.TabIndex = 21;
            this.button2.Text = "?";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnConfrimFaultySaturn5BySerialNumber
            // 
            this.btnConfrimFaultySaturn5BySerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfrimFaultySaturn5BySerialNumber.BackColor = System.Drawing.Color.OrangeRed;
            this.btnConfrimFaultySaturn5BySerialNumber.Enabled = false;
            this.btnConfrimFaultySaturn5BySerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfrimFaultySaturn5BySerialNumber.Location = new System.Drawing.Point(450, 101);
            this.btnConfrimFaultySaturn5BySerialNumber.Name = "btnConfrimFaultySaturn5BySerialNumber";
            this.btnConfrimFaultySaturn5BySerialNumber.Size = new System.Drawing.Size(449, 30);
            this.btnConfrimFaultySaturn5BySerialNumber.TabIndex = 20;
            this.btnConfrimFaultySaturn5BySerialNumber.Text = "Confirm FAULTY Saturn5 Unit back in Depot";
            this.btnConfrimFaultySaturn5BySerialNumber.UseVisualStyleBackColor = false;
            this.btnConfrimFaultySaturn5BySerialNumber.Click += new System.EventHandler(this.btnConfrimFaultySaturn5BySerialNumber_Click);
            // 
            // btnConfrimSaturn5BySerialNumberHELP
            // 
            this.btnConfrimSaturn5BySerialNumberHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfrimSaturn5BySerialNumberHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfrimSaturn5BySerialNumberHELP.Location = new System.Drawing.Point(904, 29);
            this.btnConfrimSaturn5BySerialNumberHELP.Name = "btnConfrimSaturn5BySerialNumberHELP";
            this.btnConfrimSaturn5BySerialNumberHELP.Size = new System.Drawing.Size(30, 30);
            this.btnConfrimSaturn5BySerialNumberHELP.TabIndex = 19;
            this.btnConfrimSaturn5BySerialNumberHELP.Text = "?";
            this.btnConfrimSaturn5BySerialNumberHELP.UseVisualStyleBackColor = true;
            // 
            // btnConfrimSaturn5BySerialNumber
            // 
            this.btnConfrimSaturn5BySerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfrimSaturn5BySerialNumber.Enabled = false;
            this.btnConfrimSaturn5BySerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfrimSaturn5BySerialNumber.Location = new System.Drawing.Point(450, 29);
            this.btnConfrimSaturn5BySerialNumber.Name = "btnConfrimSaturn5BySerialNumber";
            this.btnConfrimSaturn5BySerialNumber.Size = new System.Drawing.Size(449, 30);
            this.btnConfrimSaturn5BySerialNumber.TabIndex = 18;
            this.btnConfrimSaturn5BySerialNumber.Text = "Confirm Saturn5 Unit back in Depot by SERIAL NUMBER\r\n";
            this.btnConfrimSaturn5BySerialNumber.UseVisualStyleBackColor = true;
            this.btnConfrimSaturn5BySerialNumber.Click += new System.EventHandler(this.btnConfrimSaturn5BySerialNumber_Click);
            // 
            // btnConfirmSaturn5BackInShortIdHELP
            // 
            this.btnConfirmSaturn5BackInShortIdHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmSaturn5BackInShortIdHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmSaturn5BackInShortIdHELP.Location = new System.Drawing.Point(904, 67);
            this.btnConfirmSaturn5BackInShortIdHELP.Name = "btnConfirmSaturn5BackInShortIdHELP";
            this.btnConfirmSaturn5BackInShortIdHELP.Size = new System.Drawing.Size(30, 30);
            this.btnConfirmSaturn5BackInShortIdHELP.TabIndex = 17;
            this.btnConfirmSaturn5BackInShortIdHELP.Text = "?";
            this.btnConfirmSaturn5BackInShortIdHELP.UseVisualStyleBackColor = true;
            // 
            // btnConfrimSaturn5ByShortId
            // 
            this.btnConfrimSaturn5ByShortId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfrimSaturn5ByShortId.Enabled = false;
            this.btnConfrimSaturn5ByShortId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfrimSaturn5ByShortId.Location = new System.Drawing.Point(450, 67);
            this.btnConfrimSaturn5ByShortId.Name = "btnConfrimSaturn5ByShortId";
            this.btnConfrimSaturn5ByShortId.Size = new System.Drawing.Size(449, 30);
            this.btnConfrimSaturn5ByShortId.TabIndex = 16;
            this.btnConfrimSaturn5ByShortId.Text = "Confirm Saturn5 Unit back in Depot by BARCODE";
            this.btnConfrimSaturn5ByShortId.UseVisualStyleBackColor = true;
            this.btnConfrimSaturn5ByShortId.Click += new System.EventHandler(this.btnConfrimSaturn5ByShortId_Click);
            // 
            // tbxDeBriefSaturn5Barcode
            // 
            this.tbxDeBriefSaturn5Barcode.AllowEmptyInput = false;
            this.tbxDeBriefSaturn5Barcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxDeBriefSaturn5Barcode.BackColor = System.Drawing.Color.White;
            this.tbxDeBriefSaturn5Barcode.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxDeBriefSaturn5Barcode.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxDeBriefSaturn5Barcode.ClearOnEnable = true;
            this.tbxDeBriefSaturn5Barcode.DisableOnEmptyInput = false;
            this.tbxDeBriefSaturn5Barcode.DisableOnInvalidInput = false;
            this.tbxDeBriefSaturn5Barcode.DisableOnValidInput = true;
            this.tbxDeBriefSaturn5Barcode.Enabled = false;
            this.tbxDeBriefSaturn5Barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxDeBriefSaturn5Barcode.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxDeBriefSaturn5Barcode.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxDeBriefSaturn5Barcode.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxDeBriefSaturn5Barcode.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxDeBriefSaturn5Barcode.InputProvidedEArgsCreationFunc = null;
            this.tbxDeBriefSaturn5Barcode.Location = new System.Drawing.Point(223, 114);
            this.tbxDeBriefSaturn5Barcode.Name = "tbxDeBriefSaturn5Barcode";
            this.tbxDeBriefSaturn5Barcode.Size = new System.Drawing.Size(204, 24);
            this.tbxDeBriefSaturn5Barcode.TabIndex = 39;
            this.tbxDeBriefSaturn5Barcode.ValidationFunc = null;
            this.tbxDeBriefSaturn5Barcode.ValueIsEmptyWatermark = "Must Not Be Empty";
            this.tbxDeBriefSaturn5Barcode.ValueIsInvalidWatermark = "Must Be Valid";
            this.tbxDeBriefSaturn5Barcode.Watermark = "Barcode/Short Id";
            // 
            // tbxDeBriefSaturn5SerialNumber
            // 
            this.tbxDeBriefSaturn5SerialNumber.AllowEmptyInput = false;
            this.tbxDeBriefSaturn5SerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxDeBriefSaturn5SerialNumber.BackColor = System.Drawing.Color.White;
            this.tbxDeBriefSaturn5SerialNumber.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxDeBriefSaturn5SerialNumber.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxDeBriefSaturn5SerialNumber.ClearOnEnable = true;
            this.tbxDeBriefSaturn5SerialNumber.DisableOnEmptyInput = false;
            this.tbxDeBriefSaturn5SerialNumber.DisableOnInvalidInput = false;
            this.tbxDeBriefSaturn5SerialNumber.DisableOnValidInput = true;
            this.tbxDeBriefSaturn5SerialNumber.Enabled = false;
            this.tbxDeBriefSaturn5SerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxDeBriefSaturn5SerialNumber.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxDeBriefSaturn5SerialNumber.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxDeBriefSaturn5SerialNumber.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxDeBriefSaturn5SerialNumber.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxDeBriefSaturn5SerialNumber.InputProvidedEArgsCreationFunc = null;
            this.tbxDeBriefSaturn5SerialNumber.Location = new System.Drawing.Point(13, 114);
            this.tbxDeBriefSaturn5SerialNumber.Name = "tbxDeBriefSaturn5SerialNumber";
            this.tbxDeBriefSaturn5SerialNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxDeBriefSaturn5SerialNumber.TabIndex = 38;
            this.tbxDeBriefSaturn5SerialNumber.ValidationFunc = null;
            this.tbxDeBriefSaturn5SerialNumber.ValueIsEmptyWatermark = "Must Not Be Empty";
            this.tbxDeBriefSaturn5SerialNumber.ValueIsInvalidWatermark = "Must Be Valid";
            this.tbxDeBriefSaturn5SerialNumber.Watermark = "Serial Number";
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.btnDisconnectHELP);
            this.tpOptions.Controls.Add(this.btnDisconnect);
            this.tpOptions.Controls.Add(this.btnIncreaseConsoleFont);
            this.tpOptions.Controls.Add(this.btnIncreaseConsoleFontHELP);
            this.tpOptions.Controls.Add(this.btnDecreaseConsoleFont);
            this.tpOptions.Controls.Add(this.btnDecreaseConsoleFontHELP);
            this.tpOptions.Controls.Add(this.lblOptionsAccess);
            this.tpOptions.Controls.Add(this.btnSignOutHELP);
            this.tpOptions.Controls.Add(this.btnSignInHELP);
            this.tpOptions.Controls.Add(this.btnConnectHELP);
            this.tpOptions.Controls.Add(this.lblOptionsSaturn5);
            this.tpOptions.Controls.Add(this.rtbOptionsInfo);
            this.tpOptions.Controls.Add(this.tbxOptionsUserType);
            this.tpOptions.Controls.Add(this.lblOptionsUser);
            this.tpOptions.Controls.Add(this.tbxOptionsUserFirstName);
            this.tpOptions.Controls.Add(this.tbxOptionsUserSurname);
            this.tpOptions.Controls.Add(this.label1);
            this.tpOptions.Controls.Add(this.lblOptionsConnectDisconnect);
            this.tpOptions.Controls.Add(this.btnSignOut);
            this.tpOptions.Controls.Add(this.btnSignIn);
            this.tpOptions.Controls.Add(this.btnConnect);
            this.tpOptions.Controls.Add(this.tbxOptionsSaturn5Barcode);
            this.tpOptions.Controls.Add(this.tbxOptionsSaturn5SerialNumber);
            this.tpOptions.Controls.Add(this.tbxOptionsUserUsername);
            this.tpOptions.Location = new System.Drawing.Point(4, 27);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Size = new System.Drawing.Size(953, 253);
            this.tpOptions.TabIndex = 2;
            this.tpOptions.Text = "Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // btnDisconnectHELP
            // 
            this.btnDisconnectHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnectHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnectHELP.Location = new System.Drawing.Point(905, 65);
            this.btnDisconnectHELP.Name = "btnDisconnectHELP";
            this.btnDisconnectHELP.Size = new System.Drawing.Size(30, 30);
            this.btnDisconnectHELP.TabIndex = 61;
            this.btnDisconnectHELP.Text = "?";
            this.btnDisconnectHELP.UseVisualStyleBackColor = true;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(774, 65);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(125, 30);
            this.btnDisconnect.TabIndex = 60;
            this.btnDisconnect.Text = "Disconnnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnIncreaseConsoleFont
            // 
            this.btnIncreaseConsoleFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIncreaseConsoleFont.Location = new System.Drawing.Point(774, 209);
            this.btnIncreaseConsoleFont.Name = "btnIncreaseConsoleFont";
            this.btnIncreaseConsoleFont.Size = new System.Drawing.Size(125, 30);
            this.btnIncreaseConsoleFont.TabIndex = 59;
            this.btnIncreaseConsoleFont.Text = "(+) Font";
            this.btnIncreaseConsoleFont.UseVisualStyleBackColor = true;
            this.btnIncreaseConsoleFont.Click += new System.EventHandler(this.btnIncreaseConsoleFont_Click);
            // 
            // btnIncreaseConsoleFontHELP
            // 
            this.btnIncreaseConsoleFontHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIncreaseConsoleFontHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncreaseConsoleFontHELP.Location = new System.Drawing.Point(905, 209);
            this.btnIncreaseConsoleFontHELP.Name = "btnIncreaseConsoleFontHELP";
            this.btnIncreaseConsoleFontHELP.Size = new System.Drawing.Size(30, 30);
            this.btnIncreaseConsoleFontHELP.TabIndex = 58;
            this.btnIncreaseConsoleFontHELP.Text = "?";
            this.btnIncreaseConsoleFontHELP.UseVisualStyleBackColor = true;
            // 
            // btnDecreaseConsoleFont
            // 
            this.btnDecreaseConsoleFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecreaseConsoleFont.Location = new System.Drawing.Point(774, 174);
            this.btnDecreaseConsoleFont.Name = "btnDecreaseConsoleFont";
            this.btnDecreaseConsoleFont.Size = new System.Drawing.Size(125, 30);
            this.btnDecreaseConsoleFont.TabIndex = 57;
            this.btnDecreaseConsoleFont.Text = "(-) Font";
            this.btnDecreaseConsoleFont.UseVisualStyleBackColor = true;
            this.btnDecreaseConsoleFont.Click += new System.EventHandler(this.btnDecreaseConsoleFont_Click);
            // 
            // btnDecreaseConsoleFontHELP
            // 
            this.btnDecreaseConsoleFontHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDecreaseConsoleFontHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecreaseConsoleFontHELP.Location = new System.Drawing.Point(905, 174);
            this.btnDecreaseConsoleFontHELP.Name = "btnDecreaseConsoleFontHELP";
            this.btnDecreaseConsoleFontHELP.Size = new System.Drawing.Size(30, 30);
            this.btnDecreaseConsoleFontHELP.TabIndex = 56;
            this.btnDecreaseConsoleFontHELP.Text = "?";
            this.btnDecreaseConsoleFontHELP.UseVisualStyleBackColor = true;
            // 
            // lblOptionsAccess
            // 
            this.lblOptionsAccess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOptionsAccess.AutoSize = true;
            this.lblOptionsAccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblOptionsAccess.Location = new System.Drawing.Point(762, 153);
            this.lblOptionsAccess.Name = "lblOptionsAccess";
            this.lblOptionsAccess.Size = new System.Drawing.Size(61, 18);
            this.lblOptionsAccess.TabIndex = 55;
            this.lblOptionsAccess.Text = "Access:";
            // 
            // btnSignOutHELP
            // 
            this.btnSignOutHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignOutHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignOutHELP.Location = new System.Drawing.Point(606, 65);
            this.btnSignOutHELP.Name = "btnSignOutHELP";
            this.btnSignOutHELP.Size = new System.Drawing.Size(30, 30);
            this.btnSignOutHELP.TabIndex = 54;
            this.btnSignOutHELP.Text = "?";
            this.btnSignOutHELP.UseVisualStyleBackColor = true;
            // 
            // btnSignInHELP
            // 
            this.btnSignInHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignInHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSignInHELP.Location = new System.Drawing.Point(606, 29);
            this.btnSignInHELP.Name = "btnSignInHELP";
            this.btnSignInHELP.Size = new System.Drawing.Size(30, 30);
            this.btnSignInHELP.TabIndex = 53;
            this.btnSignInHELP.Text = "?";
            this.btnSignInHELP.UseVisualStyleBackColor = true;
            // 
            // btnConnectHELP
            // 
            this.btnConnectHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnectHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectHELP.Location = new System.Drawing.Point(904, 29);
            this.btnConnectHELP.Name = "btnConnectHELP";
            this.btnConnectHELP.Size = new System.Drawing.Size(30, 30);
            this.btnConnectHELP.TabIndex = 52;
            this.btnConnectHELP.Text = "?";
            this.btnConnectHELP.UseVisualStyleBackColor = true;
            // 
            // lblOptionsSaturn5
            // 
            this.lblOptionsSaturn5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOptionsSaturn5.AutoSize = true;
            this.lblOptionsSaturn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblOptionsSaturn5.Location = new System.Drawing.Point(10, 93);
            this.lblOptionsSaturn5.Name = "lblOptionsSaturn5";
            this.lblOptionsSaturn5.Size = new System.Drawing.Size(435, 18);
            this.lblOptionsSaturn5.TabIndex = 51;
            this.lblOptionsSaturn5.Text = "Saturn5 (Serial Number / Barcode / Existing Issues or Damages):";
            // 
            // rtbOptionsInfo
            // 
            this.rtbOptionsInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rtbOptionsInfo.BackColor = System.Drawing.Color.White;
            this.rtbOptionsInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.rtbOptionsInfo.Location = new System.Drawing.Point(13, 150);
            this.rtbOptionsInfo.Name = "rtbOptionsInfo";
            this.rtbOptionsInfo.ReadOnly = true;
            this.rtbOptionsInfo.Size = new System.Drawing.Size(411, 89);
            this.rtbOptionsInfo.TabIndex = 48;
            this.rtbOptionsInfo.Text = "";
            // 
            // tbxOptionsUserType
            // 
            this.tbxOptionsUserType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxOptionsUserType.BackColor = System.Drawing.Color.White;
            this.tbxOptionsUserType.Enabled = false;
            this.tbxOptionsUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxOptionsUserType.Location = new System.Drawing.Point(223, 31);
            this.tbxOptionsUserType.Name = "tbxOptionsUserType";
            this.tbxOptionsUserType.ReadOnly = true;
            this.tbxOptionsUserType.Size = new System.Drawing.Size(204, 24);
            this.tbxOptionsUserType.TabIndex = 47;
            // 
            // lblOptionsUser
            // 
            this.lblOptionsUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOptionsUser.AutoSize = true;
            this.lblOptionsUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblOptionsUser.Location = new System.Drawing.Point(10, 10);
            this.lblOptionsUser.Name = "lblOptionsUser";
            this.lblOptionsUser.Size = new System.Drawing.Size(271, 18);
            this.lblOptionsUser.TabIndex = 46;
            this.lblOptionsUser.Text = "User (username / first name / surname):";
            // 
            // tbxOptionsUserFirstName
            // 
            this.tbxOptionsUserFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxOptionsUserFirstName.BackColor = System.Drawing.Color.White;
            this.tbxOptionsUserFirstName.Enabled = false;
            this.tbxOptionsUserFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxOptionsUserFirstName.Location = new System.Drawing.Point(13, 61);
            this.tbxOptionsUserFirstName.Name = "tbxOptionsUserFirstName";
            this.tbxOptionsUserFirstName.ReadOnly = true;
            this.tbxOptionsUserFirstName.Size = new System.Drawing.Size(204, 24);
            this.tbxOptionsUserFirstName.TabIndex = 45;
            // 
            // tbxOptionsUserSurname
            // 
            this.tbxOptionsUserSurname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxOptionsUserSurname.BackColor = System.Drawing.Color.White;
            this.tbxOptionsUserSurname.Enabled = false;
            this.tbxOptionsUserSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxOptionsUserSurname.Location = new System.Drawing.Point(223, 61);
            this.tbxOptionsUserSurname.Name = "tbxOptionsUserSurname";
            this.tbxOptionsUserSurname.ReadOnly = true;
            this.tbxOptionsUserSurname.Size = new System.Drawing.Size(204, 24);
            this.tbxOptionsUserSurname.TabIndex = 44;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(646, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "Sign In/ Sign Out:";
            // 
            // lblOptionsConnectDisconnect
            // 
            this.lblOptionsConnectDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOptionsConnectDisconnect.AutoSize = true;
            this.lblOptionsConnectDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblOptionsConnectDisconnect.Location = new System.Drawing.Point(771, 10);
            this.lblOptionsConnectDisconnect.Name = "lblOptionsConnectDisconnect";
            this.lblOptionsConnectDisconnect.Size = new System.Drawing.Size(147, 18);
            this.lblOptionsConnectDisconnect.TabIndex = 18;
            this.lblOptionsConnectDisconnect.Text = "Connect/Disconnect:";
            // 
            // btnSignOut
            // 
            this.btnSignOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignOut.Enabled = false;
            this.btnSignOut.Location = new System.Drawing.Point(642, 65);
            this.btnSignOut.Name = "btnSignOut";
            this.btnSignOut.Size = new System.Drawing.Size(125, 30);
            this.btnSignOut.TabIndex = 17;
            this.btnSignOut.Text = "Sign Out";
            this.btnSignOut.UseVisualStyleBackColor = true;
            this.btnSignOut.Click += new System.EventHandler(this.btnSignOut_Click);
            // 
            // btnSignIn
            // 
            this.btnSignIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSignIn.Enabled = false;
            this.btnSignIn.Location = new System.Drawing.Point(642, 29);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(125, 30);
            this.btnSignIn.TabIndex = 16;
            this.btnSignIn.Text = "Sign In";
            this.btnSignIn.UseVisualStyleBackColor = true;
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(773, 29);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(125, 30);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbxOptionsSaturn5Barcode
            // 
            this.tbxOptionsSaturn5Barcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxOptionsSaturn5Barcode.BackColor = System.Drawing.Color.White;
            this.tbxOptionsSaturn5Barcode.Enabled = false;
            this.tbxOptionsSaturn5Barcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxOptionsSaturn5Barcode.Location = new System.Drawing.Point(223, 114);
            this.tbxOptionsSaturn5Barcode.Name = "tbxOptionsSaturn5Barcode";
            this.tbxOptionsSaturn5Barcode.Size = new System.Drawing.Size(204, 24);
            this.tbxOptionsSaturn5Barcode.TabIndex = 50;
            // 
            // tbxOptionsSaturn5SerialNumber
            // 
            this.tbxOptionsSaturn5SerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxOptionsSaturn5SerialNumber.BackColor = System.Drawing.Color.White;
            this.tbxOptionsSaturn5SerialNumber.Enabled = false;
            this.tbxOptionsSaturn5SerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxOptionsSaturn5SerialNumber.Location = new System.Drawing.Point(13, 114);
            this.tbxOptionsSaturn5SerialNumber.Name = "tbxOptionsSaturn5SerialNumber";
            this.tbxOptionsSaturn5SerialNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxOptionsSaturn5SerialNumber.TabIndex = 49;
            // 
            // tbxOptionsUserUsername
            // 
            this.tbxOptionsUserUsername.AllowEmptyInput = false;
            this.tbxOptionsUserUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxOptionsUserUsername.BackColor = System.Drawing.Color.White;
            this.tbxOptionsUserUsername.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxOptionsUserUsername.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxOptionsUserUsername.ClearOnEnable = true;
            this.tbxOptionsUserUsername.DisableOnEmptyInput = false;
            this.tbxOptionsUserUsername.DisableOnInvalidInput = false;
            this.tbxOptionsUserUsername.DisableOnValidInput = true;
            this.tbxOptionsUserUsername.Enabled = false;
            this.tbxOptionsUserUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxOptionsUserUsername.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxOptionsUserUsername.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxOptionsUserUsername.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxOptionsUserUsername.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxOptionsUserUsername.InputProvidedEArgsCreationFunc = null;
            this.tbxOptionsUserUsername.Location = new System.Drawing.Point(13, 31);
            this.tbxOptionsUserUsername.Name = "tbxOptionsUserUsername";
            this.tbxOptionsUserUsername.Size = new System.Drawing.Size(204, 24);
            this.tbxOptionsUserUsername.TabIndex = 43;
            this.tbxOptionsUserUsername.ValidationFunc = null;
            this.tbxOptionsUserUsername.ValueIsEmptyWatermark = "Must Not Be Empty";
            this.tbxOptionsUserUsername.ValueIsInvalidWatermark = "Must Be Valid";
            this.tbxOptionsUserUsername.Watermark = "Username";
            // 
            // tpSaturn5StockManagement
            // 
            this.tpSaturn5StockManagement.Controls.Add(this.lblSaturn5Stock);
            this.tpSaturn5StockManagement.Controls.Add(this.lblSaturn5SMIssues);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMReportFaultHelp);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMResolveIssueHELP);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMReportDamageHELP);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMResolveIssue);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMReportDamage);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMReportFault);
            this.tpSaturn5StockManagement.Controls.Add(this.rtbSaturn5SMInfo);
            this.tpSaturn5StockManagement.Controls.Add(this.lblSaturn5SMSaturn5);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMRemoveHELP);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMSendToITHELP);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMEditHELP);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMCreateHelp);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMReceiveFromITHELP);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMReceiveFromIT);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMSendToIT);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMRemove);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMEdit);
            this.tpSaturn5StockManagement.Controls.Add(this.btnSaturn5SMCreate);
            this.tpSaturn5StockManagement.Controls.Add(this.tbxSaturn5SMBarcode);
            this.tpSaturn5StockManagement.Controls.Add(this.tbxSaturn5SMSerialNumber);
            this.tpSaturn5StockManagement.Location = new System.Drawing.Point(4, 27);
            this.tpSaturn5StockManagement.Name = "tpSaturn5StockManagement";
            this.tpSaturn5StockManagement.Size = new System.Drawing.Size(953, 253);
            this.tpSaturn5StockManagement.TabIndex = 4;
            this.tpSaturn5StockManagement.Text = "Saturn5 Stock Management";
            this.tpSaturn5StockManagement.UseVisualStyleBackColor = true;
            // 
            // lblSaturn5Stock
            // 
            this.lblSaturn5Stock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSaturn5Stock.AutoSize = true;
            this.lblSaturn5Stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSaturn5Stock.Location = new System.Drawing.Point(692, 10);
            this.lblSaturn5Stock.Name = "lblSaturn5Stock";
            this.lblSaturn5Stock.Size = new System.Drawing.Size(51, 18);
            this.lblSaturn5Stock.TabIndex = 53;
            this.lblSaturn5Stock.Text = "Stock:";
            // 
            // lblSaturn5SMIssues
            // 
            this.lblSaturn5SMIssues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSaturn5SMIssues.AutoSize = true;
            this.lblSaturn5SMIssues.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSaturn5SMIssues.Location = new System.Drawing.Point(483, 44);
            this.lblSaturn5SMIssues.Name = "lblSaturn5SMIssues";
            this.lblSaturn5SMIssues.Size = new System.Drawing.Size(55, 18);
            this.lblSaturn5SMIssues.TabIndex = 52;
            this.lblSaturn5SMIssues.Text = "Issues:";
            // 
            // btnSaturn5SMReportFaultHelp
            // 
            this.btnSaturn5SMReportFaultHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMReportFaultHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaturn5SMReportFaultHelp.Location = new System.Drawing.Point(451, 65);
            this.btnSaturn5SMReportFaultHelp.Name = "btnSaturn5SMReportFaultHelp";
            this.btnSaturn5SMReportFaultHelp.Size = new System.Drawing.Size(30, 30);
            this.btnSaturn5SMReportFaultHelp.TabIndex = 51;
            this.btnSaturn5SMReportFaultHelp.Text = "?";
            this.btnSaturn5SMReportFaultHelp.UseVisualStyleBackColor = true;
            // 
            // btnSaturn5SMResolveIssueHELP
            // 
            this.btnSaturn5SMResolveIssueHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMResolveIssueHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaturn5SMResolveIssueHELP.Location = new System.Drawing.Point(450, 137);
            this.btnSaturn5SMResolveIssueHELP.Name = "btnSaturn5SMResolveIssueHELP";
            this.btnSaturn5SMResolveIssueHELP.Size = new System.Drawing.Size(30, 30);
            this.btnSaturn5SMResolveIssueHELP.TabIndex = 50;
            this.btnSaturn5SMResolveIssueHELP.Text = "?";
            this.btnSaturn5SMResolveIssueHELP.UseVisualStyleBackColor = true;
            // 
            // btnSaturn5SMReportDamageHELP
            // 
            this.btnSaturn5SMReportDamageHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMReportDamageHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaturn5SMReportDamageHELP.Location = new System.Drawing.Point(451, 101);
            this.btnSaturn5SMReportDamageHELP.Name = "btnSaturn5SMReportDamageHELP";
            this.btnSaturn5SMReportDamageHELP.Size = new System.Drawing.Size(30, 30);
            this.btnSaturn5SMReportDamageHELP.TabIndex = 49;
            this.btnSaturn5SMReportDamageHELP.Text = "?";
            this.btnSaturn5SMReportDamageHELP.UseVisualStyleBackColor = true;
            // 
            // btnSaturn5SMResolveIssue
            // 
            this.btnSaturn5SMResolveIssue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMResolveIssue.BackColor = System.Drawing.Color.LawnGreen;
            this.btnSaturn5SMResolveIssue.Location = new System.Drawing.Point(486, 137);
            this.btnSaturn5SMResolveIssue.Name = "btnSaturn5SMResolveIssue";
            this.btnSaturn5SMResolveIssue.Size = new System.Drawing.Size(203, 30);
            this.btnSaturn5SMResolveIssue.TabIndex = 48;
            this.btnSaturn5SMResolveIssue.Text = "Resolve Issue in Depot";
            this.btnSaturn5SMResolveIssue.UseVisualStyleBackColor = false;
            this.btnSaturn5SMResolveIssue.Click += new System.EventHandler(this.btnSaturn5SMResolveIssue_Click);
            // 
            // btnSaturn5SMReportDamage
            // 
            this.btnSaturn5SMReportDamage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMReportDamage.BackColor = System.Drawing.Color.Red;
            this.btnSaturn5SMReportDamage.Location = new System.Drawing.Point(486, 101);
            this.btnSaturn5SMReportDamage.Name = "btnSaturn5SMReportDamage";
            this.btnSaturn5SMReportDamage.Size = new System.Drawing.Size(203, 30);
            this.btnSaturn5SMReportDamage.TabIndex = 47;
            this.btnSaturn5SMReportDamage.Text = "Report Damage";
            this.btnSaturn5SMReportDamage.UseVisualStyleBackColor = false;
            this.btnSaturn5SMReportDamage.Click += new System.EventHandler(this.btnSaturn5SMReportDamage_Click);
            // 
            // btnSaturn5SMReportFault
            // 
            this.btnSaturn5SMReportFault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMReportFault.BackColor = System.Drawing.Color.OrangeRed;
            this.btnSaturn5SMReportFault.Location = new System.Drawing.Point(486, 65);
            this.btnSaturn5SMReportFault.Name = "btnSaturn5SMReportFault";
            this.btnSaturn5SMReportFault.Size = new System.Drawing.Size(203, 30);
            this.btnSaturn5SMReportFault.TabIndex = 46;
            this.btnSaturn5SMReportFault.Text = "Report Fault";
            this.btnSaturn5SMReportFault.UseVisualStyleBackColor = false;
            this.btnSaturn5SMReportFault.Click += new System.EventHandler(this.btnSaturn5SMReportFault_Click);
            // 
            // rtbSaturn5SMInfo
            // 
            this.rtbSaturn5SMInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rtbSaturn5SMInfo.BackColor = System.Drawing.Color.White;
            this.rtbSaturn5SMInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.rtbSaturn5SMInfo.Location = new System.Drawing.Point(13, 150);
            this.rtbSaturn5SMInfo.Name = "rtbSaturn5SMInfo";
            this.rtbSaturn5SMInfo.ReadOnly = true;
            this.rtbSaturn5SMInfo.Size = new System.Drawing.Size(411, 89);
            this.rtbSaturn5SMInfo.TabIndex = 45;
            this.rtbSaturn5SMInfo.Text = "";
            // 
            // lblSaturn5SMSaturn5
            // 
            this.lblSaturn5SMSaturn5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSaturn5SMSaturn5.AutoSize = true;
            this.lblSaturn5SMSaturn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSaturn5SMSaturn5.Location = new System.Drawing.Point(10, 93);
            this.lblSaturn5SMSaturn5.Name = "lblSaturn5SMSaturn5";
            this.lblSaturn5SMSaturn5.Size = new System.Drawing.Size(435, 18);
            this.lblSaturn5SMSaturn5.TabIndex = 44;
            this.lblSaturn5SMSaturn5.Text = "Saturn5 (Serial Number / Barcode / Existing Issues or Damages):";
            // 
            // btnSaturn5SMRemoveHELP
            // 
            this.btnSaturn5SMRemoveHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMRemoveHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaturn5SMRemoveHELP.Location = new System.Drawing.Point(904, 174);
            this.btnSaturn5SMRemoveHELP.Name = "btnSaturn5SMRemoveHELP";
            this.btnSaturn5SMRemoveHELP.Size = new System.Drawing.Size(30, 30);
            this.btnSaturn5SMRemoveHELP.TabIndex = 27;
            this.btnSaturn5SMRemoveHELP.Text = "?";
            this.btnSaturn5SMRemoveHELP.UseVisualStyleBackColor = true;
            // 
            // btnSaturn5SMSendToITHELP
            // 
            this.btnSaturn5SMSendToITHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMSendToITHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaturn5SMSendToITHELP.Location = new System.Drawing.Point(904, 138);
            this.btnSaturn5SMSendToITHELP.Name = "btnSaturn5SMSendToITHELP";
            this.btnSaturn5SMSendToITHELP.Size = new System.Drawing.Size(30, 30);
            this.btnSaturn5SMSendToITHELP.TabIndex = 26;
            this.btnSaturn5SMSendToITHELP.Text = "?";
            this.btnSaturn5SMSendToITHELP.UseVisualStyleBackColor = true;
            // 
            // btnSaturn5SMEditHELP
            // 
            this.btnSaturn5SMEditHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMEditHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaturn5SMEditHELP.Location = new System.Drawing.Point(904, 101);
            this.btnSaturn5SMEditHELP.Name = "btnSaturn5SMEditHELP";
            this.btnSaturn5SMEditHELP.Size = new System.Drawing.Size(30, 30);
            this.btnSaturn5SMEditHELP.TabIndex = 25;
            this.btnSaturn5SMEditHELP.Text = "?";
            this.btnSaturn5SMEditHELP.UseVisualStyleBackColor = true;
            // 
            // btnSaturn5SMCreateHelp
            // 
            this.btnSaturn5SMCreateHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMCreateHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaturn5SMCreateHelp.Location = new System.Drawing.Point(904, 66);
            this.btnSaturn5SMCreateHelp.Name = "btnSaturn5SMCreateHelp";
            this.btnSaturn5SMCreateHelp.Size = new System.Drawing.Size(30, 30);
            this.btnSaturn5SMCreateHelp.TabIndex = 24;
            this.btnSaturn5SMCreateHelp.Text = "?";
            this.btnSaturn5SMCreateHelp.UseVisualStyleBackColor = true;
            // 
            // btnSaturn5SMReceiveFromITHELP
            // 
            this.btnSaturn5SMReceiveFromITHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMReceiveFromITHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaturn5SMReceiveFromITHELP.Location = new System.Drawing.Point(904, 29);
            this.btnSaturn5SMReceiveFromITHELP.Name = "btnSaturn5SMReceiveFromITHELP";
            this.btnSaturn5SMReceiveFromITHELP.Size = new System.Drawing.Size(30, 30);
            this.btnSaturn5SMReceiveFromITHELP.TabIndex = 23;
            this.btnSaturn5SMReceiveFromITHELP.Text = "?";
            this.btnSaturn5SMReceiveFromITHELP.UseVisualStyleBackColor = true;
            // 
            // btnSaturn5SMReceiveFromIT
            // 
            this.btnSaturn5SMReceiveFromIT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMReceiveFromIT.Location = new System.Drawing.Point(695, 29);
            this.btnSaturn5SMReceiveFromIT.Name = "btnSaturn5SMReceiveFromIT";
            this.btnSaturn5SMReceiveFromIT.Size = new System.Drawing.Size(203, 30);
            this.btnSaturn5SMReceiveFromIT.TabIndex = 22;
            this.btnSaturn5SMReceiveFromIT.Text = "Create - Receive from IT";
            this.btnSaturn5SMReceiveFromIT.UseVisualStyleBackColor = true;
            this.btnSaturn5SMReceiveFromIT.Click += new System.EventHandler(this.btnSaturn5SMReceiveFromIT_Click);
            // 
            // btnSaturn5SMSendToIT
            // 
            this.btnSaturn5SMSendToIT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMSendToIT.Location = new System.Drawing.Point(695, 137);
            this.btnSaturn5SMSendToIT.Name = "btnSaturn5SMSendToIT";
            this.btnSaturn5SMSendToIT.Size = new System.Drawing.Size(203, 30);
            this.btnSaturn5SMSendToIT.TabIndex = 21;
            this.btnSaturn5SMSendToIT.Text = "Remove - Send to IT";
            this.btnSaturn5SMSendToIT.UseVisualStyleBackColor = true;
            this.btnSaturn5SMSendToIT.Click += new System.EventHandler(this.btnSaturn5SMSendToIT_Click);
            // 
            // btnSaturn5SMRemove
            // 
            this.btnSaturn5SMRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMRemove.Location = new System.Drawing.Point(695, 173);
            this.btnSaturn5SMRemove.Name = "btnSaturn5SMRemove";
            this.btnSaturn5SMRemove.Size = new System.Drawing.Size(203, 30);
            this.btnSaturn5SMRemove.TabIndex = 20;
            this.btnSaturn5SMRemove.Text = "Remove";
            this.btnSaturn5SMRemove.UseVisualStyleBackColor = true;
            this.btnSaturn5SMRemove.Click += new System.EventHandler(this.btnSaturn5SMRemove_Click);
            // 
            // btnSaturn5SMEdit
            // 
            this.btnSaturn5SMEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMEdit.Location = new System.Drawing.Point(695, 101);
            this.btnSaturn5SMEdit.Name = "btnSaturn5SMEdit";
            this.btnSaturn5SMEdit.Size = new System.Drawing.Size(203, 30);
            this.btnSaturn5SMEdit.TabIndex = 19;
            this.btnSaturn5SMEdit.Text = "Edit";
            this.btnSaturn5SMEdit.UseVisualStyleBackColor = true;
            this.btnSaturn5SMEdit.Click += new System.EventHandler(this.btnSaturn5SMEdit_Click);
            // 
            // btnSaturn5SMCreate
            // 
            this.btnSaturn5SMCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaturn5SMCreate.Location = new System.Drawing.Point(695, 65);
            this.btnSaturn5SMCreate.Name = "btnSaturn5SMCreate";
            this.btnSaturn5SMCreate.Size = new System.Drawing.Size(203, 30);
            this.btnSaturn5SMCreate.TabIndex = 18;
            this.btnSaturn5SMCreate.Text = "Create";
            this.btnSaturn5SMCreate.UseVisualStyleBackColor = true;
            this.btnSaturn5SMCreate.Click += new System.EventHandler(this.btnSaturn5SMCreate_Click);
            // 
            // tbxSaturn5SMBarcode
            // 
            this.tbxSaturn5SMBarcode.AllowEmptyInput = false;
            this.tbxSaturn5SMBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxSaturn5SMBarcode.BackColor = System.Drawing.Color.White;
            this.tbxSaturn5SMBarcode.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxSaturn5SMBarcode.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxSaturn5SMBarcode.ClearOnEnable = true;
            this.tbxSaturn5SMBarcode.DisableOnEmptyInput = false;
            this.tbxSaturn5SMBarcode.DisableOnInvalidInput = false;
            this.tbxSaturn5SMBarcode.DisableOnValidInput = true;
            this.tbxSaturn5SMBarcode.Enabled = false;
            this.tbxSaturn5SMBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxSaturn5SMBarcode.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxSaturn5SMBarcode.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxSaturn5SMBarcode.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxSaturn5SMBarcode.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxSaturn5SMBarcode.InputProvidedEArgsCreationFunc = null;
            this.tbxSaturn5SMBarcode.Location = new System.Drawing.Point(223, 114);
            this.tbxSaturn5SMBarcode.Name = "tbxSaturn5SMBarcode";
            this.tbxSaturn5SMBarcode.Size = new System.Drawing.Size(204, 24);
            this.tbxSaturn5SMBarcode.TabIndex = 43;
            this.tbxSaturn5SMBarcode.ValidationFunc = null;
            this.tbxSaturn5SMBarcode.ValueIsEmptyWatermark = null;
            this.tbxSaturn5SMBarcode.ValueIsInvalidWatermark = null;
            this.tbxSaturn5SMBarcode.Watermark = null;
            // 
            // tbxSaturn5SMSerialNumber
            // 
            this.tbxSaturn5SMSerialNumber.AllowEmptyInput = false;
            this.tbxSaturn5SMSerialNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxSaturn5SMSerialNumber.BackColor = System.Drawing.Color.White;
            this.tbxSaturn5SMSerialNumber.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxSaturn5SMSerialNumber.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxSaturn5SMSerialNumber.ClearOnEnable = true;
            this.tbxSaturn5SMSerialNumber.DisableOnEmptyInput = false;
            this.tbxSaturn5SMSerialNumber.DisableOnInvalidInput = false;
            this.tbxSaturn5SMSerialNumber.DisableOnValidInput = true;
            this.tbxSaturn5SMSerialNumber.Enabled = false;
            this.tbxSaturn5SMSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxSaturn5SMSerialNumber.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxSaturn5SMSerialNumber.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxSaturn5SMSerialNumber.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxSaturn5SMSerialNumber.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxSaturn5SMSerialNumber.InputProvidedEArgsCreationFunc = null;
            this.tbxSaturn5SMSerialNumber.Location = new System.Drawing.Point(13, 114);
            this.tbxSaturn5SMSerialNumber.Name = "tbxSaturn5SMSerialNumber";
            this.tbxSaturn5SMSerialNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxSaturn5SMSerialNumber.TabIndex = 42;
            this.tbxSaturn5SMSerialNumber.ValidationFunc = null;
            this.tbxSaturn5SMSerialNumber.ValueIsEmptyWatermark = "Must Not Be Empty";
            this.tbxSaturn5SMSerialNumber.ValueIsInvalidWatermark = "Must Be Valid";
            this.tbxSaturn5SMSerialNumber.Watermark = "Serial Number";
            // 
            // tpAdmin
            // 
            this.tpAdmin.Controls.Add(this.lblAdminUsers);
            this.tpAdmin.Controls.Add(this.rtbAdminInfo);
            this.tpAdmin.Controls.Add(this.tbxAdminUserType);
            this.tpAdmin.Controls.Add(this.lblAdminUser);
            this.tpAdmin.Controls.Add(this.tbxAdminUserFirstName);
            this.tpAdmin.Controls.Add(this.tbxAdminUserSurname);
            this.tpAdmin.Controls.Add(this.btnAdminRemoveUserHELP);
            this.tpAdmin.Controls.Add(this.btnAdminEditUserHELP);
            this.tpAdmin.Controls.Add(this.btnAdminCreateUserHELP);
            this.tpAdmin.Controls.Add(this.btnAdminRemoveUser);
            this.tpAdmin.Controls.Add(this.btnAdminEditUser);
            this.tpAdmin.Controls.Add(this.btnAdminCreateUser);
            this.tpAdmin.Controls.Add(this.tbxAdminUserUsername);
            this.tpAdmin.Location = new System.Drawing.Point(4, 27);
            this.tpAdmin.Name = "tpAdmin";
            this.tpAdmin.Size = new System.Drawing.Size(953, 253);
            this.tpAdmin.TabIndex = 3;
            this.tpAdmin.Text = "Admin";
            this.tpAdmin.UseVisualStyleBackColor = true;
            // 
            // lblAdminUsers
            // 
            this.lblAdminUsers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAdminUsers.AutoSize = true;
            this.lblAdminUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblAdminUsers.Location = new System.Drawing.Point(693, 58);
            this.lblAdminUsers.Name = "lblAdminUsers";
            this.lblAdminUsers.Size = new System.Drawing.Size(52, 18);
            this.lblAdminUsers.TabIndex = 54;
            this.lblAdminUsers.Text = "Users:";
            // 
            // rtbAdminInfo
            // 
            this.rtbAdminInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rtbAdminInfo.BackColor = System.Drawing.Color.White;
            this.rtbAdminInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.rtbAdminInfo.Location = new System.Drawing.Point(13, 150);
            this.rtbAdminInfo.Name = "rtbAdminInfo";
            this.rtbAdminInfo.ReadOnly = true;
            this.rtbAdminInfo.Size = new System.Drawing.Size(411, 89);
            this.rtbAdminInfo.TabIndex = 53;
            this.rtbAdminInfo.Text = "";
            // 
            // tbxAdminUserType
            // 
            this.tbxAdminUserType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxAdminUserType.BackColor = System.Drawing.Color.White;
            this.tbxAdminUserType.Enabled = false;
            this.tbxAdminUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxAdminUserType.Location = new System.Drawing.Point(223, 31);
            this.tbxAdminUserType.Name = "tbxAdminUserType";
            this.tbxAdminUserType.ReadOnly = true;
            this.tbxAdminUserType.Size = new System.Drawing.Size(204, 24);
            this.tbxAdminUserType.TabIndex = 52;
            // 
            // lblAdminUser
            // 
            this.lblAdminUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAdminUser.AutoSize = true;
            this.lblAdminUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblAdminUser.Location = new System.Drawing.Point(10, 10);
            this.lblAdminUser.Name = "lblAdminUser";
            this.lblAdminUser.Size = new System.Drawing.Size(271, 18);
            this.lblAdminUser.TabIndex = 51;
            this.lblAdminUser.Text = "User (username / first name / surname):";
            // 
            // tbxAdminUserFirstName
            // 
            this.tbxAdminUserFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxAdminUserFirstName.BackColor = System.Drawing.Color.White;
            this.tbxAdminUserFirstName.Enabled = false;
            this.tbxAdminUserFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxAdminUserFirstName.Location = new System.Drawing.Point(13, 61);
            this.tbxAdminUserFirstName.Name = "tbxAdminUserFirstName";
            this.tbxAdminUserFirstName.ReadOnly = true;
            this.tbxAdminUserFirstName.Size = new System.Drawing.Size(204, 24);
            this.tbxAdminUserFirstName.TabIndex = 50;
            // 
            // tbxAdminUserSurname
            // 
            this.tbxAdminUserSurname.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxAdminUserSurname.BackColor = System.Drawing.Color.White;
            this.tbxAdminUserSurname.Enabled = false;
            this.tbxAdminUserSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxAdminUserSurname.Location = new System.Drawing.Point(223, 61);
            this.tbxAdminUserSurname.Name = "tbxAdminUserSurname";
            this.tbxAdminUserSurname.ReadOnly = true;
            this.tbxAdminUserSurname.Size = new System.Drawing.Size(204, 24);
            this.tbxAdminUserSurname.TabIndex = 49;
            // 
            // btnAdminRemoveUserHELP
            // 
            this.btnAdminRemoveUserHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminRemoveUserHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminRemoveUserHELP.Location = new System.Drawing.Point(905, 152);
            this.btnAdminRemoveUserHELP.Name = "btnAdminRemoveUserHELP";
            this.btnAdminRemoveUserHELP.Size = new System.Drawing.Size(30, 30);
            this.btnAdminRemoveUserHELP.TabIndex = 36;
            this.btnAdminRemoveUserHELP.Text = "?";
            this.btnAdminRemoveUserHELP.UseVisualStyleBackColor = true;
            // 
            // btnAdminEditUserHELP
            // 
            this.btnAdminEditUserHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminEditUserHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminEditUserHELP.Location = new System.Drawing.Point(905, 115);
            this.btnAdminEditUserHELP.Name = "btnAdminEditUserHELP";
            this.btnAdminEditUserHELP.Size = new System.Drawing.Size(30, 30);
            this.btnAdminEditUserHELP.TabIndex = 35;
            this.btnAdminEditUserHELP.Text = "?";
            this.btnAdminEditUserHELP.UseVisualStyleBackColor = true;
            // 
            // btnAdminCreateUserHELP
            // 
            this.btnAdminCreateUserHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminCreateUserHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdminCreateUserHELP.Location = new System.Drawing.Point(905, 80);
            this.btnAdminCreateUserHELP.Name = "btnAdminCreateUserHELP";
            this.btnAdminCreateUserHELP.Size = new System.Drawing.Size(30, 30);
            this.btnAdminCreateUserHELP.TabIndex = 34;
            this.btnAdminCreateUserHELP.Text = "?";
            this.btnAdminCreateUserHELP.UseVisualStyleBackColor = true;
            // 
            // btnAdminRemoveUser
            // 
            this.btnAdminRemoveUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminRemoveUser.Location = new System.Drawing.Point(696, 151);
            this.btnAdminRemoveUser.Name = "btnAdminRemoveUser";
            this.btnAdminRemoveUser.Size = new System.Drawing.Size(203, 30);
            this.btnAdminRemoveUser.TabIndex = 31;
            this.btnAdminRemoveUser.Text = "Remove User";
            this.btnAdminRemoveUser.UseVisualStyleBackColor = true;
            this.btnAdminRemoveUser.Click += new System.EventHandler(this.btnAdminRemoveUser_Click);
            // 
            // btnAdminEditUser
            // 
            this.btnAdminEditUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminEditUser.Location = new System.Drawing.Point(696, 115);
            this.btnAdminEditUser.Name = "btnAdminEditUser";
            this.btnAdminEditUser.Size = new System.Drawing.Size(203, 30);
            this.btnAdminEditUser.TabIndex = 29;
            this.btnAdminEditUser.Text = "Edit User";
            this.btnAdminEditUser.UseVisualStyleBackColor = true;
            this.btnAdminEditUser.Click += new System.EventHandler(this.btnAdminEditUser_Click);
            // 
            // btnAdminCreateUser
            // 
            this.btnAdminCreateUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdminCreateUser.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAdminCreateUser.Location = new System.Drawing.Point(696, 79);
            this.btnAdminCreateUser.Name = "btnAdminCreateUser";
            this.btnAdminCreateUser.Size = new System.Drawing.Size(203, 30);
            this.btnAdminCreateUser.TabIndex = 28;
            this.btnAdminCreateUser.Text = "Create User";
            this.btnAdminCreateUser.UseVisualStyleBackColor = true;
            this.btnAdminCreateUser.Click += new System.EventHandler(this.btnAdminCreateUser_Click);
            // 
            // tbxAdminUserUsername
            // 
            this.tbxAdminUserUsername.AllowEmptyInput = false;
            this.tbxAdminUserUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxAdminUserUsername.BackColor = System.Drawing.Color.White;
            this.tbxAdminUserUsername.BackgroundActiveColor = System.Drawing.Color.LightBlue;
            this.tbxAdminUserUsername.BackgroundInactiveColor = System.Drawing.Color.White;
            this.tbxAdminUserUsername.ClearOnEnable = true;
            this.tbxAdminUserUsername.DisableOnEmptyInput = false;
            this.tbxAdminUserUsername.DisableOnInvalidInput = false;
            this.tbxAdminUserUsername.DisableOnValidInput = true;
            this.tbxAdminUserUsername.Enabled = false;
            this.tbxAdminUserUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxAdminUserUsername.ForegroundActiveColor = System.Drawing.Color.Black;
            this.tbxAdminUserUsername.ForegroundErrorWatermarkColor = System.Drawing.Color.Red;
            this.tbxAdminUserUsername.ForegroundInactiveColor = System.Drawing.Color.Black;
            this.tbxAdminUserUsername.ForegroundWatermarkColor = System.Drawing.Color.Gray;
            this.tbxAdminUserUsername.InputProvidedEArgsCreationFunc = null;
            this.tbxAdminUserUsername.Location = new System.Drawing.Point(13, 31);
            this.tbxAdminUserUsername.Name = "tbxAdminUserUsername";
            this.tbxAdminUserUsername.Size = new System.Drawing.Size(204, 24);
            this.tbxAdminUserUsername.TabIndex = 48;
            this.tbxAdminUserUsername.ValidationFunc = null;
            this.tbxAdminUserUsername.ValueIsEmptyWatermark = "Must Not Be Empty";
            this.tbxAdminUserUsername.ValueIsInvalidWatermark = "Must Be Valid";
            this.tbxAdminUserUsername.Watermark = "Username";
            // 
            // lblDoNow
            // 
            this.lblDoNow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDoNow.AutoSize = true;
            this.lblDoNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDoNow.Location = new System.Drawing.Point(20, 371);
            this.lblDoNow.Name = "lblDoNow";
            this.lblDoNow.Size = new System.Drawing.Size(155, 15);
            this.lblDoNow.TabIndex = 8;
            this.lblDoNow.Text = "Do Now - User Instructions:";
            // 
            // rtbDoNow
            // 
            this.rtbDoNow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDoNow.BackColor = System.Drawing.Color.White;
            this.rtbDoNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.rtbDoNow.Location = new System.Drawing.Point(23, 389);
            this.rtbDoNow.Name = "rtbDoNow";
            this.rtbDoNow.ReadOnly = true;
            this.rtbDoNow.Size = new System.Drawing.Size(961, 30);
            this.rtbDoNow.TabIndex = 7;
            this.rtbDoNow.Text = "";
            // 
            // rtbLoggedUser
            // 
            this.rtbLoggedUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLoggedUser.BackColor = System.Drawing.SystemColors.Control;
            this.rtbLoggedUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLoggedUser.Enabled = false;
            this.rtbLoggedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLoggedUser.ForeColor = System.Drawing.Color.Red;
            this.rtbLoggedUser.Location = new System.Drawing.Point(544, 28);
            this.rtbLoggedUser.Name = "rtbLoggedUser";
            this.rtbLoggedUser.ReadOnly = true;
            this.rtbLoggedUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.rtbLoggedUser.Size = new System.Drawing.Size(417, 25);
            this.rtbLoggedUser.TabIndex = 13;
            this.rtbLoggedUser.Text = "Please connect into the database, than Log in";
            // 
            // lblUserStatus
            // 
            this.lblUserStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserStatus.AutoSize = true;
            this.lblUserStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserStatus.Location = new System.Drawing.Point(867, 10);
            this.lblUserStatus.Name = "lblUserStatus";
            this.lblUserStatus.Size = new System.Drawing.Size(73, 15);
            this.lblUserStatus.TabIndex = 14;
            this.lblUserStatus.Text = "User Status:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(477, 718);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(449, 30);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCancelHELP
            // 
            this.btnCancelHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelHELP.Location = new System.Drawing.Point(932, 718);
            this.btnCancelHELP.Name = "btnCancelHELP";
            this.btnCancelHELP.Size = new System.Drawing.Size(30, 30);
            this.btnCancelHELP.TabIndex = 16;
            this.btnCancelHELP.Text = "?";
            this.btnCancelHELP.UseVisualStyleBackColor = true;
            // 
            // btnCloseHELP
            // 
            this.btnCloseHELP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCloseHELP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseHELP.Location = new System.Drawing.Point(40, 718);
            this.btnCloseHELP.Name = "btnCloseHELP";
            this.btnCloseHELP.Size = new System.Drawing.Size(30, 30);
            this.btnCloseHELP.TabIndex = 18;
            this.btnCloseHELP.Text = "?";
            this.btnCloseHELP.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(76, 719);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(139, 30);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 761);
            this.ControlBox = false;
            this.Controls.Add(this.btnCloseHELP);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCancelHELP);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblUserStatus);
            this.Controls.Add(this.rtbLoggedUser);
            this.Controls.Add(this.lblDoNow);
            this.Controls.Add(this.rtbDoNow);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.lblDBStatus);
            this.Controls.Add(this.lblCurrently);
            this.Controls.Add(this.rtbDBStatus);
            this.Controls.Add(this.lblLogs);
            this.Controls.Add(this.rtbCurrently);
            this.Controls.Add(this.rtbLogs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1025, 600);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "SaturnLog";
            this.tcMain.ResumeLayout(false);
            this.tpPreBrief.ResumeLayout(false);
            this.tpPreBrief.PerformLayout();
            this.tpDeBrief.ResumeLayout(false);
            this.tpDeBrief.PerformLayout();
            this.tpOptions.ResumeLayout(false);
            this.tpOptions.PerformLayout();
            this.tpSaturn5StockManagement.ResumeLayout(false);
            this.tpSaturn5StockManagement.PerformLayout();
            this.tpAdmin.ResumeLayout(false);
            this.tpAdmin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbLogs;
        private System.Windows.Forms.RichTextBox rtbCurrently;
        private System.Windows.Forms.Label lblLogs;
        private System.Windows.Forms.RichTextBox rtbDBStatus;
        private System.Windows.Forms.Label lblCurrently;
        private System.Windows.Forms.Label lblDBStatus;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpDeBrief;
        private System.Windows.Forms.Label lblDoNow;
        private System.Windows.Forms.RichTextBox rtbDoNow;
        private System.Windows.Forms.RichTextBox rtbLoggedUser;
        private System.Windows.Forms.Label lblUserStatus;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.TabPage tpSaturn5StockManagement;
        private System.Windows.Forms.TabPage tpAdmin;
        private System.Windows.Forms.Button btnConfirmSaturn5BackInShortIdHELP;
        private System.Windows.Forms.Button btnConfrimSaturn5ByShortId;
        private System.Windows.Forms.Button btnConfrimSaturn5BySerialNumberHELP;
        private System.Windows.Forms.Button btnConfrimSaturn5BySerialNumber;
        private System.Windows.Forms.Label lblDeBriefOperations;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button btnConfrimDamagedSaturn5BySerialNumber;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnConfrimFaultySaturn5BySerialNumber;
        private System.Windows.Forms.RichTextBox rtbDeBriefInfo;
        private System.Windows.Forms.Label lblDeBriefSaturn5;
        private SaturnLog.UI.Saturn5ShortIdValidatingTextBox tbxDeBriefSaturn5Barcode;
        private SaturnLog.UI.Saturn5SerialNumberValidatingTextBox tbxDeBriefSaturn5SerialNumber;
        private System.Windows.Forms.Label lblDeBriefUser;
        private System.Windows.Forms.TextBox tbxDeBriefUserFirstName;
        private System.Windows.Forms.TextBox tbxDeBriefUserSurname;
        private System.Windows.Forms.TextBox tbxDeBriefUserUsername;
        private System.Windows.Forms.TextBox tbxDeBriefUserType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOptionsConnectDisconnect;
        private System.Windows.Forms.Button btnSignOut;
        private System.Windows.Forms.Button btnSignIn;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbxOptionsUserType;
        private System.Windows.Forms.Label lblOptionsUser;
        private System.Windows.Forms.TextBox tbxOptionsUserFirstName;
        private System.Windows.Forms.TextBox tbxOptionsUserSurname;
        private SaturnLog.UI.UserUsernameValidatingTextBox tbxOptionsUserUsername;
        private System.Windows.Forms.RichTextBox rtbOptionsInfo;
        private System.Windows.Forms.Label lblOptionsSaturn5;
        private System.Windows.Forms.TextBox tbxOptionsSaturn5Barcode;
        private System.Windows.Forms.TextBox tbxOptionsSaturn5SerialNumber;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCancelHELP;
        private System.Windows.Forms.Button btnSignOutHELP;
        private System.Windows.Forms.Button btnSignInHELP;
        private System.Windows.Forms.Button btnConnectHELP;
        private System.Windows.Forms.Button btnSaturn5SMRemoveHELP;
        private System.Windows.Forms.Button btnSaturn5SMSendToITHELP;
        private System.Windows.Forms.Button btnSaturn5SMEditHELP;
        private System.Windows.Forms.Button btnSaturn5SMCreateHelp;
        private System.Windows.Forms.Button btnSaturn5SMReceiveFromITHELP;
        private System.Windows.Forms.Button btnSaturn5SMReceiveFromIT;
        private System.Windows.Forms.Button btnSaturn5SMSendToIT;
        private System.Windows.Forms.Button btnSaturn5SMRemove;
        private System.Windows.Forms.Button btnSaturn5SMEdit;
        private System.Windows.Forms.Button btnSaturn5SMCreate;
        private System.Windows.Forms.Button btnAdminRemoveUserHELP;
        private System.Windows.Forms.Button btnAdminEditUserHELP;
        private System.Windows.Forms.Button btnAdminCreateUserHELP;
        private System.Windows.Forms.Button btnAdminRemoveUser;
        private System.Windows.Forms.Button btnAdminEditUser;
        private System.Windows.Forms.Button btnAdminCreateUser;
        private System.Windows.Forms.TabPage tpPreBrief;
        private System.Windows.Forms.Button btnEmergencyAllocateSaturn5BySerialNumberHELP;
        private System.Windows.Forms.Button btnEmergencyAllocateSaturn5BySerialNumber;
        private System.Windows.Forms.TextBox tbxPreBriefUserType;
        private System.Windows.Forms.RichTextBox rtbPreBriefInfo;
        private System.Windows.Forms.Label lblPreBriefSaturn5;
        private UserWithSaturn5ShortIdValidatingTextBox tbxPreBriefSaturn5Barcode;
        private UserWithSaturn5SerialNumberValidatingTextBox tbxPreBriefSaturn5SerialNumber;
        private System.Windows.Forms.Label lblPreBriefUser;
        private System.Windows.Forms.TextBox tbxPreBriefUserSurname;
        private System.Windows.Forms.TextBox tbxPreBriefUserFirstName;
        private UserUsernameValidatingTextBox tbxPreBriefUserUsername;
        private System.Windows.Forms.Label lblPreBriefOperations;
        private System.Windows.Forms.Button btnAllocateSaturn5ToBySerialnumberHELP;
        private System.Windows.Forms.Button btnAllocateSaturn5ByShortIdHELP;
        private System.Windows.Forms.Button btnAllocateSaturn5BySerialNumber;
        private System.Windows.Forms.Button btnAllocateSaturn5ByShortId;
        private System.Windows.Forms.RichTextBox rtbSaturn5SMInfo;
        private System.Windows.Forms.Label lblSaturn5SMSaturn5;
        private Saturn5SerialNumberValidatingTextBox tbxSaturn5SMSerialNumber;
        private System.Windows.Forms.RichTextBox rtbAdminInfo;
        private System.Windows.Forms.TextBox tbxAdminUserType;
        private System.Windows.Forms.Label lblAdminUser;
        private System.Windows.Forms.TextBox tbxAdminUserFirstName;
        private System.Windows.Forms.TextBox tbxAdminUserSurname;
        private UserUsernameValidatingTextBox tbxAdminUserUsername;
        private System.Windows.Forms.Label lblOptionsAccess;
        private System.Windows.Forms.Button btnIncreaseConsoleFont;
        private System.Windows.Forms.Button btnIncreaseConsoleFontHELP;
        private System.Windows.Forms.Button btnDecreaseConsoleFont;
        private System.Windows.Forms.Button btnDecreaseConsoleFontHELP;
        private System.Windows.Forms.Label lblSaturn5Stock;
        private System.Windows.Forms.Label lblSaturn5SMIssues;
        private System.Windows.Forms.Button btnSaturn5SMReportFaultHelp;
        private System.Windows.Forms.Button btnSaturn5SMResolveIssueHELP;
        private System.Windows.Forms.Button btnSaturn5SMReportDamageHELP;
        private System.Windows.Forms.Button btnSaturn5SMResolveIssue;
        private System.Windows.Forms.Button btnSaturn5SMReportDamage;
        private System.Windows.Forms.Button btnSaturn5SMReportFault;
        private System.Windows.Forms.Label lblAdminUsers;
        private Saturn5ShortIdValidatingTextBox tbxSaturn5SMBarcode;
        private System.Windows.Forms.Button btnDisconnectHELP;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnCloseHELP;
        private System.Windows.Forms.Button btnClose;
    }
}

