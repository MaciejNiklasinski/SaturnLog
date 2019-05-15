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
    public partial class FaultReportCreationForm : Form
    {
        #region Event Handlers
        private void FaultReportCreationForm_Shown(object sender, EventArgs e)
        {
            // Unsubscribe from shown event.
            this.Load -= FaultReportCreationForm_Shown;

            // Refresh Issues list view to dis
            this.RefreshIssuesListView();
        }

        private void tbxUsername_TextChanged(object sender, EventArgs e)
        {
            this.ByUsername = this.tbxUsername.Text;
            this.DataRetrieved = false;

            if (this.ValidateUsernameTextBox())
            {
                this.tbxFirstName.Clear();
                this.tbxSurname.Clear();
                this.tbxUserType.Clear();

                // Try to obtain user data...
                Task<User> getUserTask = this._app.UserServices.GetAsync(this.tbxUsername.Text);
                getUserTask.ContinueWith((t) =>
                {
                    this._user = t.Result;
                    DialogResult result;
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            this.tbxFirstName.Text = this._user.FirstName;
                            this.tbxSurname.Text = this._user.Surname;
                            this.tbxUserType.Text = this._user.Type.GetDisplayableString();

                            if (!(this._issues is null))
                                this.DataRetrieved = true;

                            this.IsValid = this.ValidateState();
                            break;

                        case TaskStatus.Canceled:
                            // Ask user what to do in case of failure
                            result = MessageBox.Show($"Application failed to obtain user data using provided username: {this.tbxUsername.Text} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to obtain user data.", MessageBoxButtons.RetryCancel);
                            if (result == DialogResult.Retry)
                                this.tbxUsername_TextChanged(sender, e);
                            else
                                this.Close();
                            break;

                        case TaskStatus.Faulted:
                            // Ask user what to do in case of failure
                            result = MessageBox.Show($"Application failed to obtain user data using provided username: {this.tbxUsername.Text} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to obtain user data.", MessageBoxButtons.RetryCancel);
                            if (result == DialogResult.Retry)
                                this.tbxUsername_TextChanged(sender, e);
                            else
                                this.Close();
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                this.tbxFirstName.Clear();
                this.tbxSurname.Clear();
                this.tbxUserType.Clear();
                this.IsValid = this.ValidateState();
            }
        }

        private void tbxDescription_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
            this.Description = this.tbxDescription.Text;
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

        // User reporting the fault.
        private User _user;

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

        public string ByUsername { get; private set; } 

        public string Description { get; private set; }
        #endregion

        #region Constructor
        // Default constructor
        public FaultReportCreationForm(App app, Saturn5 saturn5)
        {
            // Set reference to domain layer facade...
            this._app = app;

            // Initializes designer components.
            this.InitializeComponent();

            // ... and Saturn5 unit for which created report will be designated for.
            this._saturn5 = saturn5;

            // Set the content of the serial number text box.
            this.tbxSerialNumber.Text = saturn5.SerialNumber;
            this.tbxShortId.Text = saturn5.ShortId;
            this.tbxPhoneNumber.Text = saturn5.PhoneNumber;

            // Set content of tbxUsername to saturn 5 unit LastSeenUsername
            this.tbxUsername.Text = saturn5.LastSeenUsername;
            
            this.Shown += FaultReportCreationForm_Shown;
        }
        #endregion

        #region Private Helpers
        private bool ValidateState()
        {
            if (!this.DataRetrieved)
                return false;

            // If content of the 'Reported by Username' text box does not represent valid username.
            if (!this.ValidateUsernameTextBox())
                return false;
            
            // If non empty description has not been provided
            if (this.tbxDescription.Text is null || this.tbxDescription.Text.Trim() == "")
                return false;

            // Otherwise return true.
            return true;
        }

        private bool ValidateUsernameTextBox()
        {
            return this._app.UserServices.HasUsername(this.tbxUsername.Text);
        }

        private async void RefreshIssuesListView()
        {
            // Assign all saturn 5 issues into the appropriate private field.
            _issues = await this._app.Saturn5Services.GetIssuesAsync(this._saturn5.SerialNumber);

            if (!(_user is null))
                this.DataRetrieved = true;

            // Clear issues list view
            this.lv.Items.Clear();

            // For each known saturn 5 issue construct and add apropriate list view item.
            foreach (Saturn5Issue issue in _issues)
                lv.Items.Add(this.GetIssueListViewItem(issue));
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
