namespace SaturnLog.UI
{
    partial class FaultReportCreationForm
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
            this.tbxDescription = new System.Windows.Forms.TextBox();
            this.lblFaultsAndDamagesHistory = new System.Windows.Forms.Label();
            this.lblFaultDescription = new System.Windows.Forms.Label();
            this.tbxUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.tbxShortId = new System.Windows.Forms.TextBox();
            this.lblShortId = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.tbxPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblReportingFault = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.tbxFirstName = new System.Windows.Forms.TextBox();
            this.lblSurname = new System.Windows.Forms.Label();
            this.tbxSurname = new System.Windows.Forms.TextBox();
            this.lblUserType = new System.Windows.Forms.Label();
            this.tbxUserType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblDataStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(592, 453);
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
            this.btnSave.Location = new System.Drawing.Point(592, 417);
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
            this.lv.Size = new System.Drawing.Size(565, 379);
            this.lv.TabIndex = 45;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
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
            // tbxDescription
            // 
            this.tbxDescription.Location = new System.Drawing.Point(15, 417);
            this.tbxDescription.Multiline = true;
            this.tbxDescription.Name = "tbxDescription";
            this.tbxDescription.Size = new System.Drawing.Size(571, 66);
            this.tbxDescription.TabIndex = 46;
            this.tbxDescription.TextChanged += new System.EventHandler(this.tbxDescription_TextChanged);
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
            // lblFaultDescription
            // 
            this.lblFaultDescription.AutoSize = true;
            this.lblFaultDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblFaultDescription.Location = new System.Drawing.Point(12, 396);
            this.lblFaultDescription.Name = "lblFaultDescription";
            this.lblFaultDescription.Size = new System.Drawing.Size(182, 18);
            this.lblFaultDescription.TabIndex = 48;
            this.lblFaultDescription.Text = "Saturn 5 Fault Description:";
            // 
            // tbxUsername
            // 
            this.tbxUsername.BackColor = System.Drawing.Color.White;
            this.tbxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxUsername.Location = new System.Drawing.Point(12, 214);
            this.tbxUsername.Name = "tbxUsername";
            this.tbxUsername.Size = new System.Drawing.Size(204, 24);
            this.tbxUsername.TabIndex = 49;
            this.tbxUsername.TextChanged += new System.EventHandler(this.tbxUsername_TextChanged);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUsername.Location = new System.Drawing.Point(10, 193);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 18);
            this.lblUsername.TabIndex = 50;
            this.lblUsername.Text = "Username:";
            // 
            // tbxShortId
            // 
            this.tbxShortId.BackColor = System.Drawing.Color.White;
            this.tbxShortId.Enabled = false;
            this.tbxShortId.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxShortId.Location = new System.Drawing.Point(12, 96);
            this.tbxShortId.Name = "tbxShortId";
            this.tbxShortId.ReadOnly = true;
            this.tbxShortId.Size = new System.Drawing.Size(204, 24);
            this.tbxShortId.TabIndex = 51;
            // 
            // lblShortId
            // 
            this.lblShortId.AutoSize = true;
            this.lblShortId.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblShortId.Location = new System.Drawing.Point(9, 75);
            this.lblShortId.Name = "lblShortId";
            this.lblShortId.Size = new System.Drawing.Size(123, 18);
            this.lblShortId.TabIndex = 52;
            this.lblShortId.Text = "Short Id/Barcode:";
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
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.AutoSize = true;
            this.lblPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblPhoneNumber.Location = new System.Drawing.Point(10, 123);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(112, 18);
            this.lblPhoneNumber.TabIndex = 55;
            this.lblPhoneNumber.Text = "Phone Number:";
            // 
            // tbxPhoneNumber
            // 
            this.tbxPhoneNumber.BackColor = System.Drawing.Color.White;
            this.tbxPhoneNumber.Enabled = false;
            this.tbxPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxPhoneNumber.Location = new System.Drawing.Point(12, 144);
            this.tbxPhoneNumber.Name = "tbxPhoneNumber";
            this.tbxPhoneNumber.ReadOnly = true;
            this.tbxPhoneNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxPhoneNumber.TabIndex = 54;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(9, 171);
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
            this.lblReportingFault.Location = new System.Drawing.Point(53, 175);
            this.lblReportingFault.Name = "lblReportingFault";
            this.lblReportingFault.Size = new System.Drawing.Size(125, 18);
            this.lblReportingFault.TabIndex = 57;
            this.lblReportingFault.Text = "reporting the fault:";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblFirstName.Location = new System.Drawing.Point(10, 241);
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
            this.tbxFirstName.Location = new System.Drawing.Point(12, 262);
            this.tbxFirstName.Name = "tbxFirstName";
            this.tbxFirstName.ReadOnly = true;
            this.tbxFirstName.Size = new System.Drawing.Size(204, 24);
            this.tbxFirstName.TabIndex = 58;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSurname.Location = new System.Drawing.Point(10, 289);
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
            this.tbxSurname.Location = new System.Drawing.Point(12, 310);
            this.tbxSurname.Name = "tbxSurname";
            this.tbxSurname.ReadOnly = true;
            this.tbxSurname.Size = new System.Drawing.Size(204, 24);
            this.tbxSurname.TabIndex = 60;
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUserType.Location = new System.Drawing.Point(10, 337);
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
            this.tbxUserType.Location = new System.Drawing.Point(12, 358);
            this.tbxUserType.Name = "tbxUserType";
            this.tbxUserType.ReadOnly = true;
            this.tbxUserType.Size = new System.Drawing.Size(204, 24);
            this.tbxUserType.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(86, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 18);
            this.label1.TabIndex = 65;
            this.label1.Text = "Report Fault";
            // 
            // lblDataStatus
            // 
            this.lblDataStatus.AutoSize = true;
            this.lblDataStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataStatus.ForeColor = System.Drawing.Color.Red;
            this.lblDataStatus.Location = new System.Drawing.Point(464, 9);
            this.lblDataStatus.Name = "lblDataStatus";
            this.lblDataStatus.Size = new System.Drawing.Size(223, 18);
            this.lblDataStatus.TabIndex = 66;
            this.lblDataStatus.Text = "Retrieving Data Please Wait.";
            // 
            // FaultReportCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 494);
            this.Controls.Add(this.lblDataStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUserType);
            this.Controls.Add(this.tbxUserType);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.tbxSurname);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.tbxFirstName);
            this.Controls.Add(this.lblReportingFault);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblPhoneNumber);
            this.Controls.Add(this.tbxPhoneNumber);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.lblShortId);
            this.Controls.Add(this.tbxShortId);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.tbxUsername);
            this.Controls.Add(this.lblFaultDescription);
            this.Controls.Add(this.lblFaultsAndDamagesHistory);
            this.Controls.Add(this.tbxDescription);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.tbxSerialNumber);
            this.Controls.Add(this.lblSaturn5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FaultReportCreationForm";
            this.ShowInTaskbar = false;
            this.Text = "Saturn Log - Saturn 5 Fault Report Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox tbxSerialNumber;
        private System.Windows.Forms.Label lblSaturn5;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader chTimestamp;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chReportedBy;
        private System.Windows.Forms.ColumnHeader chReport;
        private System.Windows.Forms.TextBox tbxDescription;
        private System.Windows.Forms.Label lblFaultsAndDamagesHistory;
        private System.Windows.Forms.Label lblFaultDescription;
        private System.Windows.Forms.TextBox tbxUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox tbxShortId;
        private System.Windows.Forms.Label lblShortId;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox tbxPhoneNumber;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblReportingFault;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox tbxFirstName;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.TextBox tbxSurname;
        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.TextBox tbxUserType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDataStatus;
    }
}