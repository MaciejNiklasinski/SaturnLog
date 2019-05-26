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
    public partial class ResolveSaturn5IssueReportCreationForm : Form
    {
        #region Event Handlers
        private void ResolveSaturn5IssueReportCreationForm_Shown(object sender, EventArgs e)
        {
            // Unsubscribe from shown event.
            this.Load -= ResolveSaturn5IssueReportCreationForm_Shown;

            // Refresh Issues list view to dis
            this.RefreshIssuesListView();
        }
        
        private void tbxResolvedHow_TextChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }
        
        private void rdbNotResolved_CheckedChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.IsValid = this.ValidateState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Set Commit flag to true..
            this.Commit = true;

            // Set Issue to be resolved based on the selected unresolved issue.
            this.Issue = ((Saturn5Issue)lv.SelectedItems[0].Tag);
            
            // Set user resolving the issue.
            this.ResolvedBy = this._app.UserServices.Get(this.tbxUsername.Text);

            // Set the way in which issue has been resolved
            if (this.rdbResolved.Checked)
                this.ResolvedHow = Saturn5IssueStatus.Resolved;
            else if (this.rdbKnownIssue.Checked)
                this.ResolvedHow = Saturn5IssueStatus.KnownIssue;
            else if (this.rdbCannotReplicate.Checked)
                this.ResolvedHow = Saturn5IssueStatus.CannotReplicate;
            else if (this.rdbNotAnIssue.Checked)
                this.ResolvedHow = Saturn5IssueStatus.NotAnIssue;
            
            // Set resolved how description
            this.ResolvedHowDescription = this.tbxResolvedHow.Text;

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

        // Instance of Saturn5 unit used by the form to create resolve issues report for.
        private Saturn5 _saturn5;

        // List of all unresolved faults and damages associated with the saturn 5 unit.
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

        public Saturn5Issue Issue { get; private set; }

        public User ResolvedBy { get; private set; }

        public Saturn5IssueStatus ResolvedHow { get; private set; }

        public string ResolvedHowDescription { get; private set; }
        #endregion

        #region Constructor
        // Default constructor
        public ResolveSaturn5IssueReportCreationForm(App app, Saturn5 saturn5)
        {
            // Set reference to domain layer facade...
            this._app = app;

            // Initializes designer components.
            this.InitializeComponent();

            // ... and Saturn5 unit for which created report will be designated for.
            this._saturn5 = saturn5;

            // Set the content of the serial number text box.
            this.tbxSerialNumber.Text = saturn5.SerialNumber;

            // Set content of user realted text boxes based on the currently logged user
            this.tbxUsername.Text = this._app.LoggedUsername;
            this.tbxFirstName.Text = this._app.LoggedUser.FirstName;
            this.tbxSurname.Text = this._app.LoggedUser.Surname;
            this.tbxUserType.Text = this._app.LoggedUsername;

            this.Shown += ResolveSaturn5IssueReportCreationForm_Shown;
        }
        #endregion

        #region Private Helpers
        private bool ValidateState()
        {
            if (!this.DataRetrieved)
                return false;

            // If this saturn 5 unit has no unresolved issues return false.
            if (!this.ValidateHasIssue())
                return false;

            // If list view containing unresolved issues does not have selected exactly one item.
            if (lv.SelectedItems.Count != 1)
                return false;

            // If selected item content does not represent unresolved fault or damage.
            if (((Saturn5Issue)lv.SelectedItems[0].Tag).Status != Saturn5IssueStatus.Reported
                && ((Saturn5Issue)lv.SelectedItems[0].Tag).Status != Saturn5IssueStatus.Damaged)
                return false;

            // If content of the 'Reported by Username' text box does not represent valid username.
            if (!this.ValidateUsernameTextBox())
                return false;

            // If non resolved how description has not been provided
            if (this.tbxResolvedHow.Text is null || this.tbxResolvedHow.Text.Trim() == "")
                return false;

            // Otherwise return true.
            return true;
        }

        private bool ValidateHasIssue()
        {
            return this._app.Saturn5Services.HasUnresolvedIssues(this._saturn5.SerialNumber);
        }

        private bool ValidateUsernameTextBox()
        {
            return this._app.UserServices.HasUsername(this.tbxUsername.Text);
        }

        private async void RefreshIssuesListView()
        {
            // Assign all saturn 5 issues into the appropriate private field.
            _issues = await this._app.Saturn5Services.GetUnresolvedIssuesAsync(this._saturn5.SerialNumber);

            this.DataRetrieved = true;

            // Clear issues list view
            this.lv.Items.Clear();

            // For each known saturn 5 issue construct and add appropriate list view item.
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
