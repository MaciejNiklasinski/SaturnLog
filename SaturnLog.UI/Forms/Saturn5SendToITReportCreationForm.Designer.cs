namespace SaturnLog.UI
{
    partial class Saturn5SendToITReportCreationForm
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tbxSerialNumber = new System.Windows.Forms.TextBox();
            this.lblSaturn5 = new System.Windows.Forms.Label();
            this.lv = new System.Windows.Forms.ListView();
            this.chTimestamp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chReportedBy = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chReport = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tbxMovementNote = new System.Windows.Forms.TextBox();
            this.lblFaultsAndDamagesHistory = new System.Windows.Forms.Label();
            this.lblMovementNote = new System.Windows.Forms.Label();
            this.tbxUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblReportingFault = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.tbxFirstName = new System.Windows.Forms.TextBox();
            this.lblSurname = new System.Windows.Forms.Label();
            this.tbxSurname = new System.Windows.Forms.TextBox();
            this.lblUserType = new System.Windows.Forms.Label();
            this.tbxUserType = new System.Windows.Forms.TextBox();
            this.lblSendToIT = new System.Windows.Forms.Label();
            this.lblIncidentNumber = new System.Windows.Forms.Label();
            this.tbxIncidentNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxConsignmentNumber = new System.Windows.Forms.TextBox();
            this.pnlReplacementOrSurlpus = new System.Windows.Forms.Panel();
            this.rdbSurplus = new System.Windows.Forms.RadioButton();
            this.rdbReplacement = new System.Windows.Forms.RadioButton();
            this.lblDataStatus = new System.Windows.Forms.Label();
            this.pnlReplacementOrSurlpus.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(592, 491);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 30);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(592, 455);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(196, 30);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tbxSerialNumber
            // 
            this.tbxSerialNumber.BackColor = System.Drawing.Color.White;
            this.tbxSerialNumber.Enabled = false;
            this.tbxSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxSerialNumber.Location = new System.Drawing.Point(12, 48);
            this.tbxSerialNumber.Name = "tbxSerialNumber";
            this.tbxSerialNumber.ReadOnly = true;
            this.tbxSerialNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxSerialNumber.TabIndex = 44;
            // 
            // lblSaturn5
            // 
            this.lblSaturn5.AutoSize = true;
            this.lblSaturn5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaturn5.Location = new System.Drawing.Point(9, 3);
            this.lblSaturn5.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblSaturn5.Name = "lblSaturn5";
            this.lblSaturn5.Size = new System.Drawing.Size(74, 24);
            this.lblSaturn5.TabIndex = 43;
            this.lblSaturn5.Text = "Saturn5";
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chTimestamp,
            this.chStatus,
            this.chReportedBy,
            this.chReport});
            this.lv.Location = new System.Drawing.Point(223, 32);
            this.lv.MultiSelect = false;
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(565, 416);
            this.lv.TabIndex = 45;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            // 
            // chTimestamp
            // 
            this.chTimestamp.Text = "Timestamp:";
            this.chTimestamp.Width = 100;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status:";
            this.chStatus.Width = 100;
            // 
            // chReportedBy
            // 
            this.chReportedBy.Text = "Reported By:";
            this.chReportedBy.Width = 100;
            // 
            // chReport
            // 
            this.chReport.Text = "Report";
            this.chReport.Width = 500;
            // 
            // tbxMovementNote
            // 
            this.tbxMovementNote.Location = new System.Drawing.Point(15, 454);
            this.tbxMovementNote.Multiline = true;
            this.tbxMovementNote.Name = "tbxMovementNote";
            this.tbxMovementNote.Size = new System.Drawing.Size(571, 66);
            this.tbxMovementNote.TabIndex = 46;
            // 
            // lblFaultsAndDamagesHistory
            // 
            this.lblFaultsAndDamagesHistory.AutoSize = true;
            this.lblFaultsAndDamagesHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblFaultsAndDamagesHistory.Location = new System.Drawing.Point(220, 9);
            this.lblFaultsAndDamagesHistory.Name = "lblFaultsAndDamagesHistory";
            this.lblFaultsAndDamagesHistory.Size = new System.Drawing.Size(196, 18);
            this.lblFaultsAndDamagesHistory.TabIndex = 47;
            this.lblFaultsAndDamagesHistory.Text = "Faults and Damages history:";
            // 
            // lblMovementNote
            // 
            this.lblMovementNote.AutoSize = true;
            this.lblMovementNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblMovementNote.Location = new System.Drawing.Point(12, 433);
            this.lblMovementNote.Name = "lblMovementNote";
            this.lblMovementNote.Size = new System.Drawing.Size(118, 18);
            this.lblMovementNote.TabIndex = 48;
            this.lblMovementNote.Text = "Movement Note:";
            // 
            // tbxUsername
            // 
            this.tbxUsername.BackColor = System.Drawing.Color.White;
            this.tbxUsername.Enabled = false;
            this.tbxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxUsername.Location = new System.Drawing.Point(12, 251);
            this.tbxUsername.Name = "tbxUsername";
            this.tbxUsername.ReadOnly = true;
            this.tbxUsername.Size = new System.Drawing.Size(204, 24);
            this.tbxUsername.TabIndex = 49;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUsername.Location = new System.Drawing.Point(10, 230);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 18);
            this.lblUsername.TabIndex = 50;
            this.lblUsername.Text = "Username:";
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSerialNumber.Location = new System.Drawing.Point(9, 27);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(106, 18);
            this.lblSerialNumber.TabIndex = 53;
            this.lblSerialNumber.Text = "Serial Number:";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(9, 208);
            this.lblUser.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(54, 24);
            this.lblUser.TabIndex = 56;
            this.lblUser.Text = "User ";
            // 
            // lblReportingFault
            // 
            this.lblReportingFault.AutoSize = true;
            this.lblReportingFault.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblReportingFault.Location = new System.Drawing.Point(53, 212);
            this.lblReportingFault.Name = "lblReportingFault";
            this.lblReportingFault.Size = new System.Drawing.Size(123, 18);
            this.lblReportingFault.TabIndex = 57;
            this.lblReportingFault.Text = "sending unit to IT:";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblFirstName.Location = new System.Drawing.Point(10, 278);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(85, 18);
            this.lblFirstName.TabIndex = 59;
            this.lblFirstName.Text = "First Name:";
            // 
            // tbxFirstName
            // 
            this.tbxFirstName.BackColor = System.Drawing.Color.White;
            this.tbxFirstName.Enabled = false;
            this.tbxFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxFirstName.Location = new System.Drawing.Point(12, 299);
            this.tbxFirstName.Name = "tbxFirstName";
            this.tbxFirstName.ReadOnly = true;
            this.tbxFirstName.Size = new System.Drawing.Size(204, 24);
            this.tbxFirstName.TabIndex = 58;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSurname.Location = new System.Drawing.Point(10, 326);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(72, 18);
            this.lblSurname.TabIndex = 61;
            this.lblSurname.Text = "Surname:";
            // 
            // tbxSurname
            // 
            this.tbxSurname.BackColor = System.Drawing.Color.White;
            this.tbxSurname.Enabled = false;
            this.tbxSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxSurname.Location = new System.Drawing.Point(12, 347);
            this.tbxSurname.Name = "tbxSurname";
            this.tbxSurname.ReadOnly = true;
            this.tbxSurname.Size = new System.Drawing.Size(204, 24);
            this.tbxSurname.TabIndex = 60;
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUserType.Location = new System.Drawing.Point(10, 374);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(44, 18);
            this.lblUserType.TabIndex = 63;
            this.lblUserType.Text = "Type:";
            // 
            // tbxUserType
            // 
            this.tbxUserType.BackColor = System.Drawing.Color.White;
            this.tbxUserType.Enabled = false;
            this.tbxUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxUserType.Location = new System.Drawing.Point(12, 395);
            this.tbxUserType.Name = "tbxUserType";
            this.tbxUserType.ReadOnly = true;
            this.tbxUserType.Size = new System.Drawing.Size(204, 24);
            this.tbxUserType.TabIndex = 62;
            // 
            // lblSendToIT
            // 
            this.lblSendToIT.AutoSize = true;
            this.lblSendToIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSendToIT.Location = new System.Drawing.Point(77, 7);
            this.lblSendToIT.Name = "lblSendToIT";
            this.lblSendToIT.Size = new System.Drawing.Size(75, 18);
            this.lblSendToIT.TabIndex = 64;
            this.lblSendToIT.Text = "Send to IT";
            // 
            // lblIncidentNumber
            // 
            this.lblIncidentNumber.AutoSize = true;
            this.lblIncidentNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblIncidentNumber.Location = new System.Drawing.Point(9, 123);
            this.lblIncidentNumber.Name = "lblIncidentNumber";
            this.lblIncidentNumber.Size = new System.Drawing.Size(119, 18);
            this.lblIncidentNumber.TabIndex = 75;
            this.lblIncidentNumber.Text = "Incident Number:";
            // 
            // tbxIncidentNumber
            // 
            this.tbxIncidentNumber.BackColor = System.Drawing.Color.White;
            this.tbxIncidentNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxIncidentNumber.Location = new System.Drawing.Point(12, 144);
            this.tbxIncidentNumber.Name = "tbxIncidentNumber";
            this.tbxIncidentNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxIncidentNumber.TabIndex = 74;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(9, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 18);
            this.label2.TabIndex = 73;
            this.label2.Text = "Consignment Number:";
            // 
            // tbxConsignmentNumber
            // 
            this.tbxConsignmentNumber.BackColor = System.Drawing.Color.White;
            this.tbxConsignmentNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxConsignmentNumber.Location = new System.Drawing.Point(12, 96);
            this.tbxConsignmentNumber.Name = "tbxConsignmentNumber";
            this.tbxConsignmentNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxConsignmentNumber.TabIndex = 72;
            // 
            // pnlReplacementOrSurlpus
            // 
            this.pnlReplacementOrSurlpus.Controls.Add(this.rdbSurplus);
            this.pnlReplacementOrSurlpus.Controls.Add(this.rdbReplacement);
            this.pnlReplacementOrSurlpus.Location = new System.Drawing.Point(12, 174);
            this.pnlReplacementOrSurlpus.Name = "pnlReplacementOrSurlpus";
            this.pnlReplacementOrSurlpus.Size = new System.Drawing.Size(204, 31);
            this.pnlReplacementOrSurlpus.TabIndex = 76;
            // 
            // rdbSurplus
            // 
            this.rdbSurplus.AutoSize = true;
            this.rdbSurplus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rdbSurplus.Location = new System.Drawing.Point(128, 3);
            this.rdbSurplus.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdbSurplus.Name = "rdbSurplus";
            this.rdbSurplus.Size = new System.Drawing.Size(76, 22);
            this.rdbSurplus.TabIndex = 1;
            this.rdbSurplus.Text = "Surplus";
            this.rdbSurplus.UseVisualStyleBackColor = true;
            this.rdbSurplus.CheckedChanged += new System.EventHandler(this.rdbSurplus_CheckedChanged);
            // 
            // rdbReplacement
            // 
            this.rdbReplacement.AutoSize = true;
            this.rdbReplacement.Checked = true;
            this.rdbReplacement.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rdbReplacement.Location = new System.Drawing.Point(1, 3);
            this.rdbReplacement.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdbReplacement.Name = "rdbReplacement";
            this.rdbReplacement.Size = new System.Drawing.Size(113, 22);
            this.rdbReplacement.TabIndex = 0;
            this.rdbReplacement.TabStop = true;
            this.rdbReplacement.Text = "Replacement";
            this.rdbReplacement.UseVisualStyleBackColor = true;
            // 
            // lblDataStatus
            // 
            this.lblDataStatus.AutoSize = true;
            this.lblDataStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataStatus.ForeColor = System.Drawing.Color.Red;
            this.lblDataStatus.Location = new System.Drawing.Point(422, 7);
            this.lblDataStatus.Name = "lblDataStatus";
            this.lblDataStatus.Size = new System.Drawing.Size(223, 18);
            this.lblDataStatus.TabIndex = 77;
            this.lblDataStatus.Text = "Retrieving Data Please Wait.";
            // 
            // Saturn5SendToITReportCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 532);
            this.Controls.Add(this.lblDataStatus);
            this.Controls.Add(this.pnlReplacementOrSurlpus);
            this.Controls.Add(this.lblIncidentNumber);
            this.Controls.Add(this.tbxIncidentNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxConsignmentNumber);
            this.Controls.Add(this.lblSendToIT);
            this.Controls.Add(this.lblUserType);
            this.Controls.Add(this.tbxUserType);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.tbxSurname);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.tbxFirstName);
            this.Controls.Add(this.lblReportingFault);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.tbxUsername);
            this.Controls.Add(this.lblMovementNote);
            this.Controls.Add(this.lblFaultsAndDamagesHistory);
            this.Controls.Add(this.tbxMovementNote);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.tbxSerialNumber);
            this.Controls.Add(this.lblSaturn5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Saturn5SendToITReportCreationForm";
            this.ShowInTaskbar = false;
            this.Text = "Saturn Log - Saturn 5 Fault Report Creator";
            this.pnlReplacementOrSurlpus.ResumeLayout(false);
            this.pnlReplacementOrSurlpus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbxSerialNumber;
        private System.Windows.Forms.Label lblSaturn5;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.TextBox tbxMovementNote;
        private System.Windows.Forms.Label lblFaultsAndDamagesHistory;
        private System.Windows.Forms.Label lblMovementNote;
        private System.Windows.Forms.TextBox tbxUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblReportingFault;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox tbxFirstName;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.TextBox tbxSurname;
        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.TextBox tbxUserType;
        private System.Windows.Forms.Label lblSendToIT;
        private System.Windows.Forms.ColumnHeader chTimestamp;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chReportedBy;
        private System.Windows.Forms.ColumnHeader chReport;
        private System.Windows.Forms.Label lblIncidentNumber;
        private System.Windows.Forms.TextBox tbxIncidentNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxConsignmentNumber;
        private System.Windows.Forms.Panel pnlReplacementOrSurlpus;
        private System.Windows.Forms.RadioButton rdbSurplus;
        private System.Windows.Forms.RadioButton rdbReplacement;
        private System.Windows.Forms.Label lblDataStatus;
    }
}