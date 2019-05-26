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
        // Adds User unit into the user list.
        public class CreateUserUITask : IUITask<EventArgs>
        {
            #region Private Fields
            // Main form
            private MainForm _form;

            // Domain layer facade
            private App _app { get { return this._form._app; } }

            // Helper class providing user related services.
            private UserServices _userServices { get { return this._form._app.UserServices; } }
            
            // Helper class responsible for printing to 'Consoles' text boxes.
            private ConsolesServices _consolesServices { get { return this._form._consolesServices; } }

            // Helper class responsible for displaying specific user/saturn5 etc related data.
            private DataDisplayServices _dataDisplayServices { get { return this._form._dataDisplayServices; } }

            // Helper class responsible for enabling and disabling tabs/buttons/text boxes
            private ControlsEnabler _controlsEnabler { get { return this._form._controlsEnabler; } }
            #endregion

            #region Constructor
            // Default constructor. 
            public CreateUserUITask(MainForm form)
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
            #region CreateUserUITask Operations EventHandlers - Operation Logic
            // Occurs whenever user will press create user button
            private void OnAwaitingReport(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnCreateUser_AwaitingReport(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnCreateUser_AwaitingReport(sender, e);

                // Creates and opens Create User report creation form
                CreateUserReportCreationForm createUserReportCreationForm = new CreateUserReportCreationForm(this._app);
                createUserReportCreationForm.FormClosed += ((s, args) =>
                { 
                    // If CreateUserReportCreationForm has been closed by user pressing save button...
                    if (createUserReportCreationForm.Commit)
                        // .. attempt to commit CreateUser data.
                        this.OnReportDataUploadRequired(sender, new CreateUserEventArgs(
                            createUserReportCreationForm.Username,
                            createUserReportCreationForm.FirstName,
                            createUserReportCreationForm.Surname,
                            createUserReportCreationForm.UserType));
                    // otherwise ..
                    else
                        // Cancel UI task.
                        this.OnAwaitingReportCanceled(sender, e);
                });

                createUserReportCreationForm.ShowDialog(this._form);
            }

            // Occurs whenever user will press cancel button rather then save to close CreateUserReportCreationForm
            private void OnAwaitingReportCanceled(object sender, System.EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled current task.
                this._consolesServices.OnCreateUser_AwaitingReportCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnCreateUser_AwaitingReportCanceled(sender, e);
            }

            // Occurs whenever create user report has been provided and required to be committed.
            private void OnReportDataUploadRequired(object sender, CreateUserEventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnCreateUser_UploadingReportDataBegan(sender, e);

                // Attempt to commit create user data
                Task createUserTask = this._userServices.CreateAsync(e.Username, e.FirstName, e.Surname, e.UserType);
                createUserTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if user creation has been committed successfully
                            this.OnReportDataUploadSucceed(sender, e);
                            break;
                        case TaskStatus.Faulted:
                            // ... execute if user creation has fail to commit
                            this.OnReportDataUploadFailed(sender, e);
                            break;
                        case TaskStatus.Canceled:
                            // ... execute if user creation has been canceled to commit
                            this.OnReportDataUploadCanceled(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever user creation data get uploaded successfully
            private void OnReportDataUploadSucceed(object sender, CreateUserEventArgs e)
            {
                // Displays appropriate logs informing user that application successfully committed the data.
                this._consolesServices.OnCreateUser_ReportDataUploadSucceed(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnCreateUser_ReportDataUploadSucceed(sender, e);
            }
            
            // Occurs whenever create user data failed to get uploaded.
            private void OnReportDataUploadFailed(object sender, CreateUserEventArgs e)
            {
                // Displays appropriate logs informing user about failed data upload.
                this._consolesServices.OnCreateUser_ReportDataUploadFailed(sender, e);

                // Informs user about application failed to create user and as such being unable to continue.
                MessageBox.Show("Application failed to create user and add it into the users list and must be closed.", "Create User failed.", MessageBoxButtons.OK);

                // Close the application.
                this._form.Close();
            }

            // Occurs whenever create user data canceled to get uploaded.
            private void OnReportDataUploadCanceled(object sender, CreateUserEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled data upload
                this._consolesServices.OnCreateUser_ReportDataUploadCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnCreateUser_ReportDataUploadCanceled(sender, e);
            }
            #endregion
            #endregion
        }
    }
}
