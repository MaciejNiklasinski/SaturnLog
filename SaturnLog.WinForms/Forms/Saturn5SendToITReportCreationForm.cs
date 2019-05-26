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
    public partial class Saturn5SendToITReportCreationForm : Form
    {
        #region Event Handlers
        private void Saturn5SendToITReportCreationForm_Shown(object sender, EventArgs e)
        {
            // Unsubscribe from shown event.
            this.Load -= Saturn5SendToITReportCreationForm_Shown;

            // Refresh Issues list view to dis
            this.RefreshIssuesListView();
        }

        private void rdbSurplus_CheckedChanged(object sender, EventArgs e)
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
        public bool _isValid = false;

        public bool _dataRetrieved = false;

        // Domain layer facade
        App _app;

        // Instance of Saturn5 unit used by the form to create fault report for.
        private Saturn5 _saturn5;

        // List of all (resolved and unresolved) faults and damages associated with the saturn 5 unit.
        private IList<Saturn5Issue> _issues;
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
                if (this.DataRetrieved)
                    this.btnSave.Enabled = value;
                else
                    this.btnSave.Enabled = false;
            }
        }

        public bool DataRetrieved
        {
            get { return this._dataRetrieved; }
            private set
            {
                this._dataRetrieved = value;
                if (value)
                {
                    this.lblDataStatus.ForeColor = Color.Green;
                    this.lblDataStatus.Text = "Data Retrieved.";
                }
                else
                {
                    this.lblDataStatus.ForeColor = Color.Red;
                    this.lblDataStatus.Text = "Retrieving Data Please Wait.";
                }

                if (this.IsValid)
                    this.btnSave.Enabled = value;
                else
                    this.btnSave.Enabled = false;
            }
        }

        public bool Surplus
        {
            get { return this.rdbSurplus.Checked; }
        }

        public string SerialNumber { get; } 

        public string ConsignmentNumber
        {
            get
            {
                if (this.tbxConsignmentNumber.Text is null || this.tbxConsignmentNumber.Text.Trim() == "")
                    return null;
                else
                    return this.tbxConsignmentNumber.Text;
            }
        } 

        public string IncidentNumber
        {
            get
            {
                if (this.tbxIncidentNumber.Text is null || this.tbxIncidentNumber.Text.Trim() == "")
                    return null;
                else
                    return this.tbxIncidentNumber.Text;
            }
        } 

        public string MovementNote
        {
            get
            {
                if (this.tbxMovementNote.Text is null || this.tbxMovementNote.Text.Trim() == "")
                    return null;
                else
                    return this.tbxMovementNote.Text;
            }
        }
        #endregion

        #region Constructor
        // Default constructor
        public Saturn5SendToITReportCreationForm(App app, Saturn5 saturn5)
        {
            // Set reference to domain layer facade...
            this._app = app;

            // Initializes designer components.
            this.InitializeComponent();

            // ... and Saturn5 unit for which created report will be designated for.
            this._saturn5 = saturn5;

            // Set the value of the serial number property
            this.SerialNumber = saturn5.SerialNumber;

            // Set the content of the serial number text box.
            this.tbxSerialNumber.Text = saturn5.SerialNumber;

            // Set content of user realted text boxes based on the currently logged user
            this.tbxUsername.Text = this._app.LoggedUsername;
            this.tbxFirstName.Text = this._app.LoggedUser.FirstName;
            this.tbxSurname.Text = this._app.LoggedUser.Surname;
            this.tbxUserType.Text = this._app.LoggedUsername;
            
            this.Shown += Saturn5SendToITReportCreationForm_Shown;
        }
        #endregion

        #region Private Helpers
        private bool ValidateState()
        {
            if (!this.DataRetrieved)
                return false;

            // If Saturn is not marked as surplus, and has no unresolved issues..
            if (!this.Surplus && !this.ValidateHasIssue())
                // .. return false.
                return false;

            // Otherwise return true.
            return true;
        }

        private bool ValidateHasIssue()
        {
            return this._app.Saturn5Services.HasUnresolvedIssues(this.SerialNumber);
        }

        private async void RefreshIssuesListView()
        {
            // Assign all saturn 5 issues into the appropriate private field.
            _issues = await this._app.Saturn5Services.GetIssuesAsync(this._saturn5.SerialNumber);

            this.DataRetrieved = true;

            // Clear issues list view
            this.lv.Items.Clear();

            // For each known saturn 5 issue construct and add appropriate list view item.
            foreach (Saturn5Issue issue in _issues)
                lv.Items.Add(this.GetIssueListViewItem(issue));

            // Validate state
            if (!this.btnSave.InvokeRequired && !this.btnSave.IsDisposed)
                this.IsValid = this.ValidateState();
            else if(!this.btnSave.IsDisposed)
                this.btnSave.Invoke(new Action(() => this.IsValid = this.ValidateState()));
        }

        private ListViewItem GetIssueListViewItem(Saturn5Issue issue)
        {
            ListViewItem listViewItem = new ListViewItem(new[] { issue.Timestamp, issue.Status.ToDisplayString(), issue.ReportedByUsername, issue.Description });
            listViewItem.Tag = issue;
            return listViewItem;
        }
        #endregion
    }
}
