using SaturnLog.Core;
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
    public partial class EditUserReportCreationForm : Form
    {
        #region Event Handlers
        private void tbxNewFirstName_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void tbxNewSurname_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void cbxNewUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Set Commit flag to true..
            this.Commit = true;

            // .. and close the form.
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Private Fields
        // Is valid flag underlying field.
        public bool _isValid;

        // Domain layer facade
        App _app;
        #endregion

        #region Properties
        // Flag indicating whether the form was closed with intention to commit the changes.
        public bool Commit { get; private set; }

        public bool IsValid
        {
            get { return this._isValid; }
            private set
            {
                this._isValid = value;
                this.btnSave.Enabled = value;
            }
        }

        // User unit to be edited
        public User ToBeEdited { get; }

        // New Short Id for the edited Saturn 5 unit.
        public string NewFirstName
        {
            get
            {
                if (this.tbxNewFirstName.Text?.Trim() == "" || this.tbxNewFirstName.Text?.Trim().ToUpperFirstCharOnly() == this.ToBeEdited.FirstName)
                    return null;
                else
                    return this.tbxNewFirstName.Text;
            }
        }
        
        public string NewSurname
        {
            get
            {
                if (this.tbxNewSurname.Text?.Trim() == "" || this.tbxNewSurname.Text?.Trim().ToUpperFirstCharOnly() == this.ToBeEdited.Surname)
                    return null;
                else
                    return this.tbxNewSurname.Text;
            }
        }

        public UserType? NewUserType
        {
            get
            {
                if (this.cbxNewUserType.SelectedItem is null)
                    return null;

                UserType selectedUserType = UserTypeProvider.GetFromString(this.cbxNewUserType.SelectedItem.ToString());

                if (selectedUserType == this.ToBeEdited.Type)
                    return null;
                else
                    return selectedUserType;
            }
        }
        #endregion

        #region Constructor
        // Default constructor
        public EditUserReportCreationForm(App app, User toBeEdited)
        {
            // Set reference to domain layer facade...
            this._app = app;

            // .. and instance of User to be edited.
            this.ToBeEdited = toBeEdited;

            // Initializes designer components.
            this.InitializeComponent();

            // Set the content of text boxes
            this.tbxUsername.Text = this.ToBeEdited.Username;
            this.tbxNewFirstName.Text = this.ToBeEdited.FirstName;
            this.tbxNewSurname.Text = this.ToBeEdited.Surname;
            this.RefreshUserTypeComboBoxContent();
            this.SelectCurrentUserType();
        }
        #endregion

        #region Private Helpers
        private bool ValidateState()
        {
            if (this.ValidateFirstNameTextBox())
                return true;

            if (this.ValidateSurnameTextBox())
                return true;

            if (this.ValidateUserType())
                return true;

            return false;
        }

        private bool ValidateFirstNameTextBox()
        {
            return this.tbxNewFirstName.Text?.Trim() != "" &&
                this.tbxNewFirstName.Text?.Trim().ToUpperFirstCharOnly() != ToBeEdited.FirstName;
        }

        private bool ValidateSurnameTextBox()
        {
            return this.tbxNewSurname.Text?.Trim() != "" &&
                this.tbxNewSurname.Text?.Trim().ToUpperFirstCharOnly() != ToBeEdited.Surname;
        }

        private bool ValidateUserType()
        {
            if (this.cbxNewUserType.SelectedItem is null)
                return false;

            UserType selectedUserType = UserTypeProvider.GetFromString(this.cbxNewUserType.SelectedItem.ToString() );

            return selectedUserType != ToBeEdited.Type;

        }

        private void RefreshUserTypeComboBoxContent()
        {
            // Clear content of the user type combo box
            this.cbxNewUserType.Items.Clear();

            // And fill it with user type displayable string for each possible user type value
            foreach (UserType userType in Enum.GetValues(typeof(UserType)))
                this.cbxNewUserType.Items.Add(userType.GetDisplayableString());
        }

        private void SelectCurrentUserType()
        {
            string currentType = this.ToBeEdited.Type.GetDisplayableString();

            for (int i = 0; i < this.cbxNewUserType.Items.Count; i++)
                if (((string)this.cbxNewUserType.Items[i]) == currentType)
                {
                    this.cbxNewUserType.SelectedIndex = i;
                    return;
                }

            throw new InvalidOperationException($"cbxNewUserType doesn't contain UserType enum {currentType} value.");
        }
        #endregion
    }
}
