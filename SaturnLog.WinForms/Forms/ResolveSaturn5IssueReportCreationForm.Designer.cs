namespace SaturnLog.UI
{
    partial class ResolveSaturn5IssueReportCreationForm
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
            this.tbxResolvedHow = new System.Windows.Forms.TextBox();
            this.lblUnresolvedFaultsAndDamages = new System.Windows.Forms.Label();
            this.lblResolvedHowDescription = new System.Windows.Forms.Label();
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
            this.lblResolveIssue = new System.Windows.Forms.Label();
            this.rdbResolved = new System.Windows.Forms.RadioButton();
            this.rdbNotResolved = new System.Windows.Forms.RadioButton();
            this.pnlResolvedHow = new System.Windows.Forms.Panel();
            this.rdbCannotReplicate = new System.Windows.Forms.RadioButton();
            this.rdbKnownIssue = new System.Windows.Forms.RadioButton();
            this.rdbNotAnIssue = new System.Windows.Forms.RadioButton();
            this.lblResolvedHow = new System.Windows.Forms.Label();
            this.lblDataStatus = new System.Windows.Forms.Label();
            this.pnlResolvedHow.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(592, 487);
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
            this.btnSave.Location = new System.Drawing.Point(592, 451);
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
            this.lv.Size = new System.Drawing.Size(565, 413);
            this.lv.TabIndex = 45;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            // 
            // chTimestamp
            // 
            this.chTimestamp.Text = "Timestamp:";
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status:";
            // 
            // chReportedBy
            // 
            this.chReportedBy.Text = "Reported By:";
            // 
            // chReport
            // 
            this.chReport.Text = "Report";
            // 
            // tbxResolvedHow
            // 
            this.tbxResolvedHow.Location = new System.Drawing.Point(12, 451);
            this.tbxResolvedHow.Multiline = true;
            this.tbxResolvedHow.Name = "tbxResolvedHow";
            this.tbxResolvedHow.Size = new System.Drawing.Size(571, 66);
            this.tbxResolvedHow.TabIndex = 46;
            this.tbxResolvedHow.TextChanged += new System.EventHandler(this.tbxResolvedHow_TextChanged);
            // 
            // lblUnresolvedFaultsAndDamages
            // 
            this.lblUnresolvedFaultsAndDamages.AutoSize = true;
            this.lblUnresolvedFaultsAndDamages.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUnresolvedFaultsAndDamages.Location = new System.Drawing.Point(220, 9);
            this.lblUnresolvedFaultsAndDamages.Name = "lblUnresolvedFaultsAndDamages";
            this.lblUnresolvedFaultsAndDamages.Size = new System.Drawing.Size(227, 18);
            this.lblUnresolvedFaultsAndDamages.TabIndex = 47;
            this.lblUnresolvedFaultsAndDamages.Text = "Unresolved Faults and Damages:";
            // 
            // lblResolvedHowDescription
            // 
            this.lblResolvedHowDescription.AutoSize = true;
            this.lblResolvedHowDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblResolvedHowDescription.Location = new System.Drawing.Point(9, 430);
            this.lblResolvedHowDescription.Name = "lblResolvedHowDescription";
            this.lblResolvedHowDescription.Size = new System.Drawing.Size(188, 18);
            this.lblResolvedHowDescription.TabIndex = 48;
            this.lblResolvedHowDescription.Text = "Resolved How Description:";
            // 
            // tbxUsername
            // 
            this.tbxUsername.BackColor = System.Drawing.Color.White;
            this.tbxUsername.Enabled = false;
            this.tbxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxUsername.Location = new System.Drawing.Point(12, 253);
            this.tbxUsername.Name = "tbxUsername";
            this.tbxUsername.ReadOnly = true;
            this.tbxUsername.Size = new System.Drawing.Size(204, 24);
            this.tbxUsername.TabIndex = 49;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUsername.Location = new System.Drawing.Point(10, 232);
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
            this.lblUser.Location = new System.Drawing.Point(9, 210);
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
            this.lblReportingFault.Location = new System.Drawing.Point(53, 214);
            this.lblReportingFault.Name = "lblReportingFault";
            this.lblReportingFault.Size = new System.Drawing.Size(136, 18);
            this.lblReportingFault.TabIndex = 57;
            this.lblReportingFault.Text = "Resolving the Issue";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblFirstName.Location = new System.Drawing.Point(10, 280);
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
            this.tbxFirstName.Location = new System.Drawing.Point(12, 301);
            this.tbxFirstName.Name = "tbxFirstName";
            this.tbxFirstName.ReadOnly = true;
            this.tbxFirstName.Size = new System.Drawing.Size(204, 24);
            this.tbxFirstName.TabIndex = 58;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSurname.Location = new System.Drawing.Point(10, 328);
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
            this.tbxSurname.Location = new System.Drawing.Point(12, 349);
            this.tbxSurname.Name = "tbxSurname";
            this.tbxSurname.ReadOnly = true;
            this.tbxSurname.Size = new System.Drawing.Size(204, 24);
            this.tbxSurname.TabIndex = 60;
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUserType.Location = new System.Drawing.Point(10, 376);
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
            this.tbxUserType.Location = new System.Drawing.Point(12, 397);
            this.tbxUserType.Name = "tbxUserType";
            this.tbxUserType.ReadOnly = true;
            this.tbxUserType.Size = new System.Drawing.Size(204, 24);
            this.tbxUserType.TabIndex = 62;
            // 
            // lblResolveIssue
            // 
            this.lblResolveIssue.AutoSize = true;
            this.lblResolveIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblResolveIssue.Location = new System.Drawing.Point(77, 7);
            this.lblResolveIssue.Name = "lblResolveIssue";
            this.lblResolveIssue.Size = new System.Drawing.Size(101, 18);
            this.lblResolveIssue.TabIndex = 64;
            this.lblResolveIssue.Text = "Resolve Issue";
            // 
            // rdbResolved
            // 
            this.rdbResolved.AutoSize = true;
            this.rdbResolved.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rdbResolved.Location = new System.Drawing.Point(6, 22);
            this.rdbResolved.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdbResolved.Name = "rdbResolved";
            this.rdbResolved.Size = new System.Drawing.Size(88, 22);
            this.rdbResolved.TabIndex = 1;
            this.rdbResolved.Text = "Resolved";
            this.rdbResolved.UseVisualStyleBackColor = true;
            // 
            // rdbNotResolved
            // 
            this.rdbNotResolved.AutoSize = true;
            this.rdbNotResolved.Checked = true;
            this.rdbNotResolved.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rdbNotResolved.Location = new System.Drawing.Point(6, 3);
            this.rdbNotResolved.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdbNotResolved.Name = "rdbNotResolved";
            this.rdbNotResolved.Size = new System.Drawing.Size(116, 22);
            this.rdbNotResolved.TabIndex = 0;
            this.rdbNotResolved.TabStop = true;
            this.rdbNotResolved.Text = "Not Resolved";
            this.rdbNotResolved.UseVisualStyleBackColor = true;
            this.rdbNotResolved.CheckedChanged += new System.EventHandler(this.rdbNotResolved_CheckedChanged);
            // 
            // pnlResolvedHow
            // 
            this.pnlResolvedHow.Controls.Add(this.rdbCannotReplicate);
            this.pnlResolvedHow.Controls.Add(this.rdbKnownIssue);
            this.pnlResolvedHow.Controls.Add(this.rdbNotAnIssue);
            this.pnlResolvedHow.Controls.Add(this.rdbResolved);
            this.pnlResolvedHow.Controls.Add(this.rdbNotResolved);
            this.pnlResolvedHow.Location = new System.Drawing.Point(13, 96);
            this.pnlResolvedHow.Name = "pnlResolvedHow";
            this.pnlResolvedHow.Size = new System.Drawing.Size(204, 108);
            this.pnlResolvedHow.TabIndex = 77;
            // 
            // rdbCannotReplicate
            // 
            this.rdbCannotReplicate.AutoSize = true;
            this.rdbCannotReplicate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rdbCannotReplicate.Location = new System.Drawing.Point(6, 59);
            this.rdbCannotReplicate.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdbCannotReplicate.Name = "rdbCannotReplicate";
            this.rdbCannotReplicate.Size = new System.Drawing.Size(139, 22);
            this.rdbCannotReplicate.TabIndex = 4;
            this.rdbCannotReplicate.Text = "Cannot Replicate";
            this.rdbCannotReplicate.UseVisualStyleBackColor = true;
            // 
            // rdbKnownIssue
            // 
            this.rdbKnownIssue.AutoSize = true;
            this.rdbKnownIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rdbKnownIssue.Location = new System.Drawing.Point(6, 41);
            this.rdbKnownIssue.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdbKnownIssue.Name = "rdbKnownIssue";
            this.rdbKnownIssue.Size = new System.Drawing.Size(111, 22);
            this.rdbKnownIssue.TabIndex = 3;
            this.rdbKnownIssue.Text = "Known Issue";
            this.rdbKnownIssue.UseVisualStyleBackColor = true;
            // 
            // rdbNotAnIssue
            // 
            this.rdbNotAnIssue.AutoSize = true;
            this.rdbNotAnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.rdbNotAnIssue.Location = new System.Drawing.Point(6, 78);
            this.rdbNotAnIssue.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.rdbNotAnIssue.Name = "rdbNotAnIssue";
            this.rdbNotAnIssue.Size = new System.Drawing.Size(110, 22);
            this.rdbNotAnIssue.TabIndex = 2;
            this.rdbNotAnIssue.Text = "Not An Issue";
            this.rdbNotAnIssue.UseVisualStyleBackColor = true;
            // 
            // lblResolvedHow
            // 
            this.lblResolvedHow.AutoSize = true;
            this.lblResolvedHow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblResolvedHow.Location = new System.Drawing.Point(11, 75);
            this.lblResolvedHow.Name = "lblResolvedHow";
            this.lblResolvedHow.Size = new System.Drawing.Size(109, 18);
            this.lblResolvedHow.TabIndex = 78;
            this.lblResolvedHow.Text = "Resolved How:";
            // 
            // lblDataStatus
            // 
            this.lblDataStatus.AutoSize = true;
            this.lblDataStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataStatus.ForeColor = System.Drawing.Color.Red;
            this.lblDataStatus.Location = new System.Drawing.Point(442, 9);
            this.lblDataStatus.Name = "lblDataStatus";
            this.lblDataStatus.Size = new System.Drawing.Size(223, 18);
            this.lblDataStatus.TabIndex = 79;
            this.lblDataStatus.Text = "Retrieving Data Please Wait.";
            // 
            // ResolveSaturn5IssueReportCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 528);
            this.Controls.Add(this.lblDataStatus);
            this.Controls.Add(this.lblResolvedHow);
            this.Controls.Add(this.pnlResolvedHow);
            this.Controls.Add(this.lblResolveIssue);
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
            this.Controls.Add(this.lblResolvedHowDescription);
            this.Controls.Add(this.lblUnresolvedFaultsAndDamages);
            this.Controls.Add(this.tbxResolvedHow);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.tbxSerialNumber);
            this.Controls.Add(this.lblSaturn5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResolveSaturn5IssueReportCreationForm";
            this.ShowInTaskbar = false;
            this.Text = "Saturn Log - Saturn 5 Fault Report Creator";
            this.pnlResolvedHow.ResumeLayout(false);
            this.pnlResolvedHow.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbxSerialNumber;
        private System.Windows.Forms.Label lblSaturn5;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.TextBox tbxResolvedHow;
        private System.Windows.Forms.Label lblUnresolvedFaultsAndDamages;
        private System.Windows.Forms.Label lblResolvedHowDescription;
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
        private System.Windows.Forms.Label lblResolveIssue;
        private System.Windows.Forms.ColumnHeader chTimestamp;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chReportedBy;
        private System.Windows.Forms.ColumnHeader chReport;
        private System.Windows.Forms.RadioButton rdbResolved;
        private System.Windows.Forms.RadioButton rdbNotResolved;
        private System.Windows.Forms.Panel pnlResolvedHow;
        private System.Windows.Forms.RadioButton rdbCannotReplicate;
        private System.Windows.Forms.RadioButton rdbKnownIssue;
        private System.Windows.Forms.RadioButton rdbNotAnIssue;
        private System.Windows.Forms.Label lblResolvedHow;
        private System.Windows.Forms.Label lblDataStatus;
    }
}