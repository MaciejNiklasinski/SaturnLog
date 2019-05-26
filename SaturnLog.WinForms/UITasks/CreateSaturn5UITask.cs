using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
using SaturnLog.UI.UITasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI
{
    public partial class MainForm
    {
        // Adds Saturn5 unit into the depot stock.
        public class CreateSaturn5UITask : IUITask<EventArgs>
        {
            #region Private Fields
            // Main form
            private MainForm _form;

            // Domain layer facade
            private App _app { get { return this._form._app; } }

            // Helper class providing user related services.
            private UserServices _userServices { get { return this._form._app.UserServices; } }

            // Helper class providing saturn5 related services.
            private Saturn5Services _saturn5Services { get { return this._form._app.Saturn5Services; } }

            // Helper class responsible for printing to 'Consoles' text boxes.
            private ConsolesServices _consolesServices { get { return this._form._consolesServices; } }

            // Helper class responsible for displaying specific user/saturn5 etc related data.
            private DataDisplayServices _dataDisplayServices { get { return this._form._dataDisplayServices; } }

            // Helper class responsible for enabling and disabling tabs/buttons/text boxes
            private ControlsEnabler _controlsEnabler { get { return this._form._controlsEnabler; } }
            #endregion

            #region Constructor
            // Default constructor. 
            public CreateSaturn5UITask(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public void Trigger(object sender, EventArgs e)
            {
                this.OnAwaitingReport(sender, e);
            }
            
            public void Cancel(object sender, EventArgs e)
            {

            }
            #endregion

            #region Private Helpers
            #region CreateSaturn5UITask Operations EventHandlers - Operation Logic
            // Occurs whenever user will press create saturn 5 button
            private void OnAwaitingReport(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnCreateSaturn5_AwaitingReport(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnCreateSaturn5_AwaitingReport(sender, e);

                // Creates and opens Create Saturn 5 report creation form
                CreateSaturn5ReportCreationForm createSaturn5ReportCreationForm = new CreateSaturn5ReportCreationForm(this._app);
                createSaturn5ReportCreationForm.FormClosed += ((s, args) =>
                {
                    // If CreateSaturn5ReportCreationForm has been closed by user pressing save button...
                    if (createSaturn5ReportCreationForm.Commit)
                        // .. attempt to commit CreateSaturn5 data.
                        this.OnReportDataUploadRequired(sender, new CreateSaturn5EventArgs(
                            createSaturn5ReportCreationForm.SerialNumber,
                            createSaturn5ReportCreationForm.ShortId,
                            createSaturn5ReportCreationForm.PhoneNumber));
                    // otherwise ..
                    else
                        // Cancel UI task.
                        this.OnAwaitingReportCanceled(sender, e);
                });

                createSaturn5ReportCreationForm.ShowDialog(this._form);
            }

            // Occurs whenever user will press cancel button rather then save to close CreateSaturn5ReportCreationForm
            private void OnAwaitingReportCanceled(object sender, System.EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled current task.
                this._consolesServices.OnCreateSaturn5_AwaitingReportCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnCreateSaturn5_AwaitingReportCanceled(sender, e);
            }

            // Occurs whenever create saturn 5 report has been provided and required to be committed.
            private void OnReportDataUploadRequired(object sender, CreateSaturn5EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnCreateSaturn5_UploadingReportDataBegan(sender, e);

                // Attempt to commit create saturn 5 data
                Task createSaturn5Task = this._saturn5Services.CreateAsync(e.SerialNumber, e.ShortId, e.PhoneNumber);
                createSaturn5Task.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if saturn 5 receive from IT has been committed successfully
                            this.OnReportDataUploadSucceed(sender, e);
                            break;
                        case TaskStatus.Faulted:
                            // ... execute if saturn 5 receive from IT has fail to commit
                            this.OnReportDataUploadFailed(sender, e);
                            break;
                        case TaskStatus.Canceled:
                            // ... execute if saturn 5 receive from IT has been canceled to commit
                            this.OnReportDataUploadCanceled(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever saturn 5 received from IT data get uploaded successfully
            private void OnReportDataUploadSucceed(object sender, CreateSaturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application successfully committed the data.
                this._consolesServices.OnCreateSaturn5_ReportDataUploadSucceed(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnCreateSaturn5_ReportDataUploadSucceed(sender, e);
            }
            
            // Occurs whenever create saturn 5 data failed to get uploaded.
            private void OnReportDataUploadFailed(object sender, CreateSaturn5EventArgs e)
            {
                // Displays appropriate logs informing user about failed data upload.
                this._consolesServices.OnCreateSaturn5_ReportDataUploadFailed(sender, e);

                // Informs user about application failed to create saturn 5 and as such being unable to continue.
                MessageBox.Show("Application failed to create saturn 5 and add it into the depot stock and must be closed.", "Create Saturn 5 failed.", MessageBoxButtons.OK);

                // Close the application.
                this._form.Close();
            }

            // Occurs whenever create saturn 5 data canceled to get uploaded.
            private void OnReportDataUploadCanceled(object sender, CreateSaturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled data upload
                this._consolesServices.OnCreateSaturn5_ReportDataUploadCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnCreateSaturn5_ReportDataUploadCanceled(sender, e);
            }
            #endregion
            #endregion
        }
    }
}
