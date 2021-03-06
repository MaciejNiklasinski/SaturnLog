﻿namespace SaturnLog.UI
{
    partial class EditSaturn5ReportCreationForm
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
            this.tbxNewShortId = new System.Windows.Forms.TextBox();
            this.lblShortId = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblPhoneNumber = new System.Windows.Forms.Label();
            this.tbxNewPhoneNumber = new System.Windows.Forms.TextBox();
            this.lblEdit = new System.Windows.Forms.Label();
            this.tbxSerialNumber = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(235, 147);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(196, 30);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(235, 111);
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
            // tbxNewShortId
            // 
            this.tbxNewShortId.BackColor = System.Drawing.Color.White;
            this.tbxNewShortId.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxNewShortId.Location = new System.Drawing.Point(12, 96);
            this.tbxNewShortId.Name = "tbxNewShortId";
            this.tbxNewShortId.Size = new System.Drawing.Size(204, 24);
            this.tbxNewShortId.TabIndex = 51;
            this.tbxNewShortId.TextChanged += new System.EventHandler(this.tbxNewShortId_TextChanged);
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
            // tbxNewPhoneNumber
            // 
            this.tbxNewPhoneNumber.BackColor = System.Drawing.Color.White;
            this.tbxNewPhoneNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxNewPhoneNumber.Location = new System.Drawing.Point(12, 144);
            this.tbxNewPhoneNumber.Name = "tbxNewPhoneNumber";
            this.tbxNewPhoneNumber.Size = new System.Drawing.Size(204, 24);
            this.tbxNewPhoneNumber.TabIndex = 54;
            this.tbxNewPhoneNumber.TextChanged += new System.EventHandler(this.tbxNewPhoneNumber_TextChanged);
            // 
            // lblEdit
            // 
            this.lblEdit.AutoSize = true;
            this.lblEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblEdit.Location = new System.Drawing.Point(86, 7);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(33, 18);
            this.lblEdit.TabIndex = 65;
            this.lblEdit.Text = "Edit";
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
            this.tbxSerialNumber.TabIndex = 68;
            // 
            // EditSaturn5ReportCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 192);
            this.Controls.Add(this.tbxSerialNumber);
            this.Controls.Add(this.lblEdit);
            this.Controls.Add(this.lblPhoneNumber);
            this.Controls.Add(this.tbxNewPhoneNumber);
            this.Controls.Add(this.lblSerialNumber);
            this.Controls.Add(this.lblShortId);
            this.Controls.Add(this.tbxNewShortId);
            this.Controls.Add(this.lblSaturn5);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditSaturn5ReportCreationForm";
            this.ShowInTaskbar = false;
            this.Text = "Saturn Log - Saturn 5 Fault Report Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblSaturn5;
        private System.Windows.Forms.TextBox tbxNewShortId;
        private System.Windows.Forms.Label lblShortId;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.Label lblPhoneNumber;
        private System.Windows.Forms.TextBox tbxNewPhoneNumber;
        private System.Windows.Forms.Label lblEdit;
        private System.Windows.Forms.TextBox tbxSerialNumber;
    }
}