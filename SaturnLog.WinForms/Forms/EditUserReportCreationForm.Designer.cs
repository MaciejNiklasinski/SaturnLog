namespace SaturnLog.UI
{
    partial class EditUserReportCreationForm
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
            this.lblUser = new System.Windows.Forms.Label();
            this.lblEdit = new System.Windows.Forms.Label();
            this.lblUserType = new System.Windows.Forms.Label();
            this.cbxNewUserType = new System.Windows.Forms.ComboBox();
            this.tbxUsername = new System.Windows.Forms.TextBox();
            this.lblSurname = new System.Windows.Forms.Label();
            this.tbxNewSurname = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.tbxNewFirstName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(236, 137);
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
            this.btnSave.Location = new System.Drawing.Point(236, 101);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(196, 30);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.Location = new System.Drawing.Point(9, 3);
            this.lblUser.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(49, 24);
            this.lblUser.TabIndex = 43;
            this.lblUser.Text = "User";
            // 
            // lblEdit
            // 
            this.lblEdit.AutoSize = true;
            this.lblEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblEdit.Location = new System.Drawing.Point(61, 9);
            this.lblEdit.Name = "lblEdit";
            this.lblEdit.Size = new System.Drawing.Size(33, 18);
            this.lblEdit.TabIndex = 65;
            this.lblEdit.Text = "Edit";
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUserType.Location = new System.Drawing.Point(233, 26);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(80, 18);
            this.lblUserType.TabIndex = 78;
            this.lblUserType.Text = "User Type:";
            // 
            // cbxNewUserType
            // 
            this.cbxNewUserType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.cbxNewUserType.FormattingEnabled = true;
            this.cbxNewUserType.Location = new System.Drawing.Point(236, 47);
            this.cbxNewUserType.Name = "cbxNewUserType";
            this.cbxNewUserType.Size = new System.Drawing.Size(196, 26);
            this.cbxNewUserType.TabIndex = 77;
            this.cbxNewUserType.SelectedIndexChanged += new System.EventHandler(this.cbxNewUserType_SelectedIndexChanged);
            // 
            // tbxUsername
            // 
            this.tbxUsername.BackColor = System.Drawing.Color.White;
            this.tbxUsername.Enabled = false;
            this.tbxUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxUsername.Location = new System.Drawing.Point(13, 47);
            this.tbxUsername.Name = "tbxUsername";
            this.tbxUsername.Size = new System.Drawing.Size(204, 24);
            this.tbxUsername.TabIndex = 76;
            // 
            // lblSurname
            // 
            this.lblSurname.AutoSize = true;
            this.lblSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblSurname.Location = new System.Drawing.Point(10, 122);
            this.lblSurname.Name = "lblSurname";
            this.lblSurname.Size = new System.Drawing.Size(72, 18);
            this.lblSurname.TabIndex = 75;
            this.lblSurname.Text = "Surname:";
            // 
            // tbxNewSurname
            // 
            this.tbxNewSurname.BackColor = System.Drawing.Color.White;
            this.tbxNewSurname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxNewSurname.Location = new System.Drawing.Point(13, 143);
            this.tbxNewSurname.Name = "tbxNewSurname";
            this.tbxNewSurname.Size = new System.Drawing.Size(204, 24);
            this.tbxNewSurname.TabIndex = 74;
            this.tbxNewSurname.TextChanged += new System.EventHandler(this.tbxNewSurname_TextChanged);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblUsername.Location = new System.Drawing.Point(10, 26);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 18);
            this.lblUsername.TabIndex = 73;
            this.lblUsername.Text = "Username:";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblFirstName.Location = new System.Drawing.Point(10, 74);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(85, 18);
            this.lblFirstName.TabIndex = 72;
            this.lblFirstName.Text = "First Name:";
            // 
            // tbxNewFirstName
            // 
            this.tbxNewFirstName.BackColor = System.Drawing.Color.White;
            this.tbxNewFirstName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.tbxNewFirstName.Location = new System.Drawing.Point(13, 95);
            this.tbxNewFirstName.Name = "tbxNewFirstName";
            this.tbxNewFirstName.Size = new System.Drawing.Size(204, 24);
            this.tbxNewFirstName.TabIndex = 71;
            this.tbxNewFirstName.TextChanged += new System.EventHandler(this.tbxNewFirstName_TextChanged);
            // 
            // EditUserReportCreationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 184);
            this.Controls.Add(this.lblUserType);
            this.Controls.Add(this.cbxNewUserType);
            this.Controls.Add(this.tbxUsername);
            this.Controls.Add(this.lblSurname);
            this.Controls.Add(this.tbxNewSurname);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.tbxNewFirstName);
            this.Controls.Add(this.lblEdit);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EditUserReportCreationForm";
            this.ShowInTaskbar = false;
            this.Text = "Saturn Log - Saturn 5 Fault Report Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblEdit;
        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.ComboBox cbxNewUserType;
        private System.Windows.Forms.TextBox tbxUsername;
        private System.Windows.Forms.Label lblSurname;
        private System.Windows.Forms.TextBox tbxNewSurname;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox tbxNewFirstName;
    }
}