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
    public partial class EditSaturn5ReportCreationForm : Form
    {
        #region Event Handlers
        private void tbxNewShortId_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void tbxNewPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // If content of either short id text box, or phone number text box contains changes
            if (this.tbxNewShortId.Text != null || this.tbxNewPhoneNumber.Text != null)
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

        // Saturn 5 unit to be edited
        public Saturn5 ToBeEdited { get; }

        // New Short Id for the edited Saturn 5 unit.
        public string NewShortId
        {
            get
            {
                if (this.tbxNewShortId.Text?.Trim() == "" || this.tbxNewShortId.Text?.Trim().ToUpper() == ToBeEdited.ShortId)
                    return null;
                else
                    return this.tbxNewShortId.Text?.Trim();
            }
        }

        // New Phone Number for the edited Saturn 5 unit.
        public string NewPhoneNumber
        {
            get
            {
                if (this.tbxNewPhoneNumber.Text?.Trim() == "" || this.tbxNewPhoneNumber.Text?.Trim() == ToBeEdited.PhoneNumber)
                    return null;
                else
                    return this.tbxNewPhoneNumber.Text?.Trim();
            }
        }
        #endregion

        #region Constructor
        // Default constructor
        public EditSaturn5ReportCreationForm(App app, Saturn5 toBeEdited)
        {
            // Set reference to domain layer facade...
            this._app = app;

            // .. and instance of Saturn 5 to be edited.
            this.ToBeEdited = toBeEdited;

            // Initializes designer components.
            this.InitializeComponent();

            // Set the content of text boxes
            this.tbxSerialNumber.Text = this.ToBeEdited.SerialNumber;
            this.tbxNewShortId.Text = this.ToBeEdited.ShortId;
            this.tbxNewPhoneNumber.Text = this.ToBeEdited.PhoneNumber;
        }
        #endregion

        #region Private Helpers
        private bool ValidateState()
        {
            if (this.ValidateShortIdTextBox())
                return true;

            if (this.ValidatePhoneNumberTextBox())
                return true;

            return false;
        }

        private bool ValidateShortIdTextBox()
        {
            return !this._app.Saturn5Services.HasShortId(this.tbxNewShortId.Text) && this.tbxNewShortId.Text != this.ToBeEdited.ShortId;
        }

        private bool ValidatePhoneNumberTextBox()
        {
            return this.ToBeEdited.PhoneNumber != this.tbxNewPhoneNumber.Text;
        }
        #endregion
    }
}
