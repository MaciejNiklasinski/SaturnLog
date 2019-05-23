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
    public partial class CreateUserReportCreationForm : Form
    {
        #region Event Handlers
        private void tbxUsername_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void tbxFirstName_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void tbxSurname_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void cbxUserType_SelectedIndexChanged(object sender, EventArgs e)
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
        // Domain layer facade
        App _app;

        private bool _isValid;
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

        // Username of the user to be created
        public string Username { get { return this.tbxUsername.Text; } }

        // FirstName of the user to be created
        public string FirstName { get { return this.tbxFirstName.Text; } }

        // Surname of the user to be created
        public string Surname { get { return this.tbxSurname.Text; } }

        // UserType of the user to be created.
        public UserType UserType { get { return UserTypeProvider.GetFromString(this.cbxUserType.SelectedItem.ToString()); } }
        #endregion

        #region Constructor
        // Default constructor
        public CreateUserReportCreationForm(App app)
        {
            // Set reference to domain layer facade...
            this._app = app;

            // Initializes designer components.
            this.InitializeComponent();

            // Fill User Type user box user type displayable string for each possible user type value
            this.RefreshUserTypeComboBoxContent();

            // Set User as a selected value of text box
            this.SelectDefaultUserType();
        }
        #endregion

        #region Private Helpers
        private bool ValidateState()
        {
            if (!this.ValidateUsernameTextBox())
                return false;


            if (!this.ValidateFirstNameTextBox())
                return false;


            if (!this.ValidateSurnameTextBox())
                return false;

            return true;
        }

        private bool ValidateUsernameTextBox()
        {
            return !this._app.UserServices.HasUsername(this.tbxUsername.Text);
        }

        private bool ValidateFirstNameTextBox()
        {
            return this.tbxFirstName.Text?.Trim() != "";
        }

        private bool ValidateSurnameTextBox()
        {
            return this.tbxSurname.Text?.Trim() != "";
        }

        private void RefreshUserTypeComboBoxContent()
        {
            // Clear content of the user type combo box
            this.cbxUserType.Items.Clear();

            // And fill it with user type displayable string for each possible user type value
            foreach (UserType userType in Enum.GetValues(typeof(UserType)))
                this.cbxUserType.Items.Add(userType.GetDisplayableString());            
        }

        private void SelectDefaultUserType()
        {
            string defaultType = UserType.User.GetDisplayableString();

            for (int i = 0; i < this.cbxUserType.Items.Count; i++)
                if (((string)this.cbxUserType.Items[i]) == defaultType)
                {
                    this.cbxUserType.SelectedIndex = i;
                    return;
                }

            throw new InvalidOperationException("cbxUserType doesn't contain UserType enum default value.");
        }
        #endregion
    }
}
