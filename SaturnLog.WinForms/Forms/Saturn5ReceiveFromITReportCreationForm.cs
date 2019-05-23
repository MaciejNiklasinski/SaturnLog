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
    public partial class Saturn5ReceiveFromITReportCreationForm : Form
    {
        #region Event Handlers
        private void btnSave_Click(object sender, EventArgs e)
        {
            // If form failed to validate serial number text box content...
            if (!this.ValidateSerialNumberTextBox())
            {
                // .. inform the user and return without saving.
                MessageBox.Show($"Provided Serial Number: {this.tbxSerialNumber.Text} is associated with a Saturn 5 unit already located in a depot stock.", "Invalid Serial Number.");
                return;
            }

            // Otherwise If form failed to validate short id text box content...
            if (!this.ValidateShortIdTextBox())
            {
                // .. inform the user and return without saving.
                MessageBox.Show($"Provided Short Id/Barcode: {this.tbxShortId.Text} is associated with a Saturn 5 unit already located in a depot stock.", "Short Id/Barcode");
                return;

            }

            // If successfully validated form state...

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
        #endregion

        #region Properties
        // Flag indicating whether the form was closed with intention to commit the changes.
        public bool Commit { get; private set; }
        
        // Serial Number of the Saturn 5 unit received from IT
        public string SerialNumber { get { return this.tbxSerialNumber.Text; } } 
        
        // Short Id of the Saturn 5 unit received from IT
        public string ShortId { get { return this.tbxShortId.Text; } }
        
        // Phone Number of the Saturn 5 unit received from IT
        public string PhoneNumber
        {
            get
            {
                if (this.tbxPhoneNumber.Text?.Trim() == "")
                    return null;
                else
                    return this.tbxPhoneNumber.Text?.Trim();
            }
        }

        // Consignment number associated with Saturn 5 shipment from IT
        public string ConsignmentNumber
        {
            get
            {
                if (this.tbxConsignmentNumber.Text?.Trim() == "")
                    return null;
                else
                    return this.tbxConsignmentNumber.Text?.Trim();
            }
        }

        // Note describing Saturn 5 shipment from IT
        public string MovementNote
        {
            get
            {
                if (this.tbxMovementNote.Text?.Trim() == "")
                    return null;
                else
                    return this.tbxMovementNote.Text?.Trim();
            }
        }
        #endregion

        #region Constructor
        // Default constructor
        public Saturn5ReceiveFromITReportCreationForm(App app)
        {
            // Set reference to domain layer facade...
            this._app = app;

            // Initializes designer components.
            this.InitializeComponent();
        }
        #endregion

        #region Private Helpers
        private bool ValidateSerialNumberTextBox()
        {
            return !this._app.Saturn5Services.HasSerialNumber(this.tbxSerialNumber.Text);
        }

        private bool ValidateShortIdTextBox()
        {
            return !this._app.Saturn5Services.HasShortId(this.tbxShortId.Text);
        }
        #endregion
    }
}
