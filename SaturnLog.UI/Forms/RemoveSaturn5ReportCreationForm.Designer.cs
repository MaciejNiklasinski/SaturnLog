namespace SaturnLog.UI
{
    partial class RemoveSaturn5ReportCreationForm
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
            this.tbxConfirmation = new System.Windows.Forms.TextBox();
            this.lblFaultsAndDamagesHistory = new System.Windows.Forms.Label();
            this.lblConfirmation = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblRemove = new System.Windows.Forms.Label();
            this.lblDataStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(592, 200);
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
            this.btnSave.Location = new System.Drawing.Point(592, 164);
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
            this.lv.Size = new System.Drawing.Size(565, 100);
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
            // tbxConfirmation
            // 
            this.tbxConfirmation.Location = new System.Drawing.Point(12, 164);
            this.tbxConfirmation.Multiline = true;
            this.tbxConfirmation.Name = "tbxConfirmation";
            this.tbxConfirmation.Size = new System.Drawing.Size(574, 66);
            this.tbxConfirmation.TabIndex = 46;
            this.tbxConfirmation.TextChanged += new System.EventHandler(this.tbxConfirmation_TextChanged);
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
            // lblConfirmation
            // 
            this.lblConfirmation.AutoSize = true;
            this.lblConfirmation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblConfirmation.Location = new System.Drawing.Point(9, 143);
            this.lblConfirmation.Name = "lblConfirmation";
            this.lblConfirmation.Size = new System.Drawing.Size(526, 18);
            this.lblConfirmation.TabIndex = 48;
            this.lblConfirmation.Text = "Type in \"Serial Number-Logged User Username\" to confirm removal of the unit.";
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
            // lblRemove
            // 
            this.lblRemove.AutoSize = true;
            this.lblRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblRemove.Location = new System.Drawing.Point(77, 7);
            this.lblRemove.Name = "lblRemove";
            this.lblRemove.Size = new System.Drawing.Size(64, 18);
            this.lblRemove.TabIndex = 64;
            this.lblRemove.Text = "Remove";
            // 
            // lblDataStatus
            // 
            this.lblDataStatus.AutoSize = true;
            this.lblDataStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataStatus.ForeColor = System.Drawing.Color.Red;
            this.lblDataStatus.Location = new System.Drawing.Point(422, 7);
            this.lblDataStatus.Name = "lblDataStatus";
            this.lblDataStatus.Size = new System.Drawing.Size(223, 18);
            this.lblDataStatus.TabIndex = 80;
            this.lblDataStatus.Text = "Retrieving Data Please Wait.";
            // 
            // RemoveSaturn5ReportCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 241);
            this.Controls.Add(this.lblDataStatus);
            this.Controls.Add(this.lblRemove);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.lblConfirmation);
            this.Controls.Add(this.lblFaultsAndDamagesHistory);
            this.Controls.Add(this.tbxConfirmation);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.tbxSerialNumber);
            this.Controls.Add(this.lblSaturn5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RemoveSaturn5ReportCreationForm";
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
        private System.Windows.Forms.TextBox tbxConfirmation;
        private System.Windows.Forms.Label lblFaultsAndDamagesHistory;
        private System.Windows.Forms.Label lblConfirmation;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblRemove;
        private System.Windows.Forms.ColumnHeader chTimestamp;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chReportedBy;
        private System.Windows.Forms.ColumnHeader chReport;
        private System.Windows.Forms.Label lblDataStatus;
    }
}