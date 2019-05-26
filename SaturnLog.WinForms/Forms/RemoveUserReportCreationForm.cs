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
    public partial class RemoveUserReportCreationForm : Form
    {
        #region Event Handlers
        private void tbxConfirmation_TextChanged(object sender, EventArgs e)
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

        // Instance of User used by the form to create user removal report for.
        private User _toBeRemoved;
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

        public User ToBeRemoved { get { return this._toBeRemoved; } } 
        #endregion

        #region Constructor
        // Default constructor
        public RemoveUserReportCreationForm(App app, User toBeRemoved)
        {
            // Set reference to domain layer facade...
            this._app = app;

            // Initializes designer components.
            this.InitializeComponent();

            // ... and User unit for which created report will be designated for.
            this._toBeRemoved = toBeRemoved;

            // Set the content of the serial number text box.
            this.tbxUsername.Text = toBeRemoved.Username;
        }
        #endregion

        #region Private Helpers
        private bool ValidateState()
        {
            return this.tbxConfirmation.Text == $"{this._toBeRemoved.Username}-{this._app.LoggedUsername}";
        }
        #endregion
    }
}
