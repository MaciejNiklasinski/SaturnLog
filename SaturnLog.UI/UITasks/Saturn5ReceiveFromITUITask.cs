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
        // Adds Saturn5 unit received from IT into the depot stock.
        public class Saturn5ReceiveFromITUITask : IUITask<EventArgs>
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
            public Saturn5ReceiveFromITUITask(MainForm form)
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
            #region Saturn5ReceiveFromIT Operations EventHandlers - Operation Logic
            // Occurs whenever user will press receive from IT button.
            private void OnAwaitingReport(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnSaturn5ReceiveFromIT_AwaitingReport(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnSaturn5ReceiveFromIT_AwaitingReport(sender, e);

                // Creates and opens Saturn 5 receive from IT report creation form
                Saturn5ReceiveFromITReportCreationForm saturn5ReceiveFromITReportCreationForm = new Saturn5ReceiveFromITReportCreationForm(this._app);
                saturn5ReceiveFromITReportCreationForm.FormClosed += ((s, args) =>
                { 
                    // If Saturn5ReceiveFromITReportCreationForm has been closed by user pressing save button...
                    if (saturn5ReceiveFromITReportCreationForm.Commit)
                        // .. attempt to commit Saturn5ReceiveFromIT data.
                        this.OnReportDataUploadRequired(sender, new Saturn5ReceiveFromITEventArgs(
                            saturn5ReceiveFromITReportCreationForm.SerialNumber,
                            saturn5ReceiveFromITReportCreationForm.ShortId,
                            saturn5ReceiveFromITReportCreationForm.PhoneNumber,
                            saturn5ReceiveFromITReportCreationForm.ConsignmentNumber,
                            saturn5ReceiveFromITReportCreationForm.MovementNote));
                    // otherwise ..
                    else
                        // Cancel ui task.
                        this.OnAwaitingReportCanceled(sender, e);
                });

                saturn5ReceiveFromITReportCreationForm.ShowDialog(this._form);
            }

            // Occurs whenever user will press cancel button rather then save to close Saturn5ReceiveFromITReportCreationForm
            private void OnAwaitingReportCanceled(object sender, System.EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled UI task.
                this._consolesServices.OnSaturn5ReceiveFromIT_AwaitingReportCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnSaturn5ReceiveFromIT_AwaitingReportCanceled(sender, e);
            }

            // Occurs whenever saturn 5 receive from IT report has been provided and required to be committed.
            private void OnReportDataUploadRequired(object sender, Saturn5ReceiveFromITEventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnSaturn5ReceiveFromIT_UploadingReportDataBegan(sender, e);

                // Attempt to commit saturn 5 receive from it data
                Task receiveFromITTask = this._saturn5Services.ReceiveFromITAsync(e.SerialNumber, e.ShortId, e.PhoneNumber, e.ConsignmentNumber, e.MovementNote);
                receiveFromITTask.ContinueWith((t) =>
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
            private void OnReportDataUploadSucceed(object sender, Saturn5ReceiveFromITEventArgs e)
            {
                // Displays appropriate logs informing user that application successfully committed the data.
                this._consolesServices.OnSaturn5ReceiveFromIT_ReportDataUploadSucceed(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnSaturn5ReceiveFromIT_ReportDataUploadSucceed(sender, e);
            }
            
            // Occurs whenever saturn 5 received from IT data failed to get uploaded.
            private void OnReportDataUploadFailed(object sender, Saturn5ReceiveFromITEventArgs e)
            {
                // Displays appropriate logs informing user about failed data upload.
                this._consolesServices.OnSaturn5ReceiveFromIT_ReportDataUploadFailed(sender, e);

                // Informs user about application failed to receive saturn 5 form it and as such being unable to continue.
                MessageBox.Show("Application failed to add saturn 5 received from IT into the depot stock and must be closed.", "Saturn 5 receive from IT failed.", MessageBoxButtons.OK);

                // Close the application.
                this._form.Close();
            }

            // Occurs whenever saturn 5 received from IT data canceled to get uploaded.
            private void OnReportDataUploadCanceled(object sender, Saturn5ReceiveFromITEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled data upload.
                this._consolesServices.OnSaturn5ReceiveFromIT_ReportDataUploadCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnSaturn5ReceiveFromIT_ReportDataUploadCanceled(sender, e);
            }
            #endregion
            #endregion
        }
    }
}
