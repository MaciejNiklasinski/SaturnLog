namespace SaturnLog.UI
{
    partial class Saturn5ReceiveFromITReportCreationForm
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
            this.lblSaturn5 = new System.Windows.Forms.Label();
            this.tbxMovementNote = new System.Windows.Forms.TextBox();
            this.lblMovementNote = new System.Windows.Forms.Label();
            this.tbxShortId = new System.Windows.Forms.TextBox();
            this.lblShortId = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.tbxPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblReceiveFromIT = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxConsignmentNumber = new System.Windows.Forms.TextBox();
            this.tbxSerialNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(592, 193);
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
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(592, 157);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(196, 30);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            // tbxMovementNote
            // 
            this.tbxMovementNote.Location = new System.Drawing.Point(222, 48);
            this.tbxMovementNote.Multiline = true;
            this.tbxMovementNote.Name = "tbxMovementNote";
            this.tbxMovementNote.Size = new System.Drawing.Size(571, 103);
            this.tbxMovementNote.TabIndex = 46;
            // 
            // lblMovementNote
            // 
            this.lblMovementNote.AutoSize = true;
            this.lblMovementNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblMovementNote.Location = new System.Drawing.Point(219, 27);
            this.lblMovementNote.Name = "lblMovementNote";
            this.lblMovementNote.Size = new System.Drawing.Size(115, 18);
            this.lblMovementNote.TabIndex = 48;
            this.lblMovementNote.Text = "Movement note:";
            // 
            // tbxShortId
            // 
            this.tbxShortId.BackColor = System.Drawing.Color.White;
            this.tbxShortId.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxShortId.Location = new System.Drawing.Point(12, 96);
            this.tbxShortId.Name = "tbxShortId";
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
            this.lblPhoneNumber.Location = new System.Drawing.Point(9, 123);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(112, 18);
            this.lblPhoneNumber.TabIndex = 55;
            this.lblPhoneNumber.Text = "Phone Number:";
            // 
            // tbxPhoneNumber
            // 
            this.tbxPhoneNumber.BackColor = System.Drawing.Color.White;
            this.tbxPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxPhoneNumber.Location = new System.Drawing.Point(12, 144);
            this.tbxPhoneNumber.Name = "tbxPhoneNumber";
            this.tbxPhoneNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxPhoneNumber.TabIndex = 54;
            // 
            // lblReceiveFromIT
            // 
            this.lblReceiveFromIT.AutoSize = true;
            this.lblReceiveFromIT.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblReceiveFromIT.Location = new System.Drawing.Point(86, 7);
            this.lblReceiveFromIT.Name = "lblReceiveFromIT";
            this.lblReceiveFromIT.Size = new System.Drawing.Size(112, 18);
            this.lblReceiveFromIT.TabIndex = 65;
            this.lblReceiveFromIT.Text = "Receive from IT";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label2.Location = new System.Drawing.Point(9, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 18);
            this.label2.TabIndex = 67;
            this.label2.Text = "Consignment Number:";
            // 
            // tbxConsignmentNumber
            // 
            this.tbxConsignmentNumber.BackColor = System.Drawing.Color.White;
            this.tbxConsignmentNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxConsignmentNumber.Location = new System.Drawing.Point(12, 192);
            this.tbxConsignmentNumber.Name = "tbxConsignmentNumber";
            this.tbxConsignmentNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxConsignmentNumber.TabIndex = 66;
            // 
            // tbxSerialNumber
            // 
            this.tbxSerialNumber.BackColor = System.Drawing.Color.White;
            this.tbxSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxSerialNumber.Location = new System.Drawing.Point(12, 48);
            this.tbxSerialNumber.Name = "tbxSerialNumber";
            this.tbxSerialNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxSerialNumber.TabIndex = 68;
            // 
            // Saturn5ReceiveFromITReportCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 235);
            this.Controls.Add(this.tbxSerialNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxConsignmentNumber);
            this.Controls.Add(this.lblReceiveFromIT);
            this.Controls.Add(this.lblPhoneNumber);
            this.Controls.Add(this.tbxPhoneNumber);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.lblShortId);
            this.Controls.Add(this.tbxShortId);
            this.Controls.Add(this.lblMovementNote);
            this.Controls.Add(this.tbxMovementNote);
            this.Controls.Add(this.lblSaturn5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Saturn5ReceiveFromITReportCreationForm";
            this.ShowInTaskbar = false;
            this.Text = "Saturn Log - Saturn 5 Fault Report Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSaturn5;
        private System.Windows.Forms.TextBox tbxMovementNote;
        private System.Windows.Forms.Label lblMovementNote;
        private System.Windows.Forms.TextBox tbxShortId;
        private System.Windows.Forms.Label lblShortId;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox tbxPhoneNumber;
        private System.Windows.Forms.Label lblReceiveFromIT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxConsignmentNumber;
        private System.Windows.Forms.TextBox tbxSerialNumber;
    }
}