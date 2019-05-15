using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
using SaturnLog.Core.Exceptions;
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
        // Removes user from the depot users list.
        public class RemoveUserUITask : IUITask<EventArgs>
        {
            #region Events
            private event EventHandler AwaitingUsernameCanceled;

            private event EventHandler<UserUsernameEventArgs> ValidatingUsernameCanceled;

            #region EventArgs
            private UserUsernameEventArgs _usernameValidationCancelationArgs;
            #endregion
            #endregion

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

            #region Text Boxes
            // User Admin Tab Username text box
            private UserUsernameValidatingTextBox tbxAdminUserUsername { get { return this._form.tbxAdminUserUsername; } }
            #endregion
            #endregion

            #region Constructor
            // Default constructor. 
            public RemoveUserUITask(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public void Trigger(object sender, EventArgs e)
            {
                this.OnAwaitingUsername(sender, e);
            }
            
            public void Cancel(object sender, EventArgs e)
            {
                this.AwaitingUsernameCanceled?.Invoke(sender, e);
                this.ValidatingUsernameCanceled?.Invoke(sender, this._usernameValidationCancelationArgs ?? throw new InvalidOperationException());
            }
            #endregion

            #region Private Helpers
            #region RemoveUser Operations EventHandlers - Operation Logic
            // Occurs whenever user pressed the button and UI task is expecting valid username to be provided into the appropriate text box.
            private void OnAwaitingUsername(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnRemoveUser_AwaitingUsername(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Start listening for valid user username.
                this.StartListenForUsernameInput();

                // Enables user username input text box
                this._controlsEnabler.OnRemoveUser_AwaitingUsername(sender, e);
            }

            private void OnAwaitingUsernameCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for user username text box input.
                this.StopListenForUsernameInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveUser_AwaitingUsernameCanceled(sender, e);

                // Displays appropriate logs informing user that application awaiting username
                this._consolesServices.OnRemoveUser_AwaitingUsernameCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }

            // Occurs whenever user pressed enter when appropriate for this UI task validation box is enable, 
            // and right before a validation of provided by the user value will begin.
            private void OnProvidedUsernameValidationBegan(object sender, UserUsernameEventArgs e)
            {
                // Subscribe to appropriate method of canceling awaiting for input.
                this.ChangeCancelationMethodForUsernameInput(e);

                // Displays appropriate logs informing user about empty username input being provided.
                this._consolesServices.OnRemoveUser_ProvidedUsernameValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about empty username input being provided.
                this._consolesServices.OnRemoveUser_ProvidedUsernameValidationFailed(sender, e);

                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForUsernameInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveUser_ProvidedUsernameValidationCanceled(sender, e);

                // Displays appropriate logs informing
                this._consolesServices.OnRemoveUser_ProvidedUsernameValidationCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }


            // Occurs whenever user will provide empty value in place of valid username.
            private void OnEmptyUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about empty username input being provided.
                this._consolesServices.OnRemoveUser_EmptyUsernameProvided(sender, e);
            }

            // Occurs whenever user will provide invalid username.
            private void OnInvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about invalid username being provided.
                this._consolesServices.OnRemoveUser_InvalidUsernameProvided(sender, e);
            }

            // Occurs whenever user provided valid username into the appropriate text box
            private void OnValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Stop listening for user username
                this.StopListenForUsernameInput();

                // Displays appropriate logs informing user about valid username being provided.
                this._consolesServices.OnRemoveUser_ValidUsernameProvided(sender, e);

                // Disables user username input text box
                this._controlsEnabler.OnRemoveUser_ValidUsernameProvided(sender, e);

                // Proceed further into the UITask into the part where User Data is getting attempted to be retrieved.
                this.OnRetrievingUserDataRequired(sender, e);
            }

            // Occurs whenever UITask required to retrieve user data to proceed. 
            private void OnRetrievingUserDataRequired(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user that application began to obtaining User data based on the provided username
                this._consolesServices.OnRetrievingUserDataBegan(sender, e);

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingUserDataBegan(sender, e);

                // Attempt to obtain user data...
                Task<User> getUserTask = this._userServices.GetAsync(e.Username);
                getUserTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if succeed to obtain user data.
                            this.OnSuccessfullyRetrievedUserData(sender, new UserEventArgs(t.Result));
                            break;
                        case TaskStatus.Faulted:
                            // ... execute if failed to obtain user data.
                            this.OnFailToRetrieveUserData(sender, e);
                            break;
                        case TaskStatus.Canceled:
                            // ... execute if attempt to obtain user data has been canceled.
                            this.OnCancelToRetrieveUserData(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs if application fails to obtain user data
            private void OnFailToRetrieveUserData(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user that application failed to obtaining user data.
                this._consolesServices.OnRetrievingUserDataFailed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Application failed to obtain user data using provided username: {e.Username} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to obtain User data.", MessageBoxButtons.RetryCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        this.OnCancelToRetrieveUserData(sender, e);
                        break;
                    case DialogResult.Retry:
                        this.OnRetrievingUserDataRequired(sender, e);
                        break;
                }
            }

            // Occurs if retrieving user data has been canceled
            private void OnCancelToRetrieveUserData(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnRetrievingUserDataCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnRemoveUser_CancelToRetrieveUserData(sender, e);
            }

            // Occurs whenever user data got successfully retrieved.
            private void OnSuccessfullyRetrievedUserData(object sender, UserEventArgs e)
            {
                // Displays appropriate logs informing user that application completed obtaining user data.
                this._consolesServices.OnRetrievingUserDataCompleted(sender, e);

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingUserDataCompleted(sender, e);

                // Trigger further part of UITask
                this.OnAwaitingReport(sender, e);
            }

            // Occurs whenever user data has been successfully retrieved and user removal report is awaited
            private void OnAwaitingReport(object sender, UserEventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnRemoveUser_AwaitingReport(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveUser_AwaitingReport(sender, e);

                // Creates and opens Remove User report creation form
                RemoveUserReportCreationForm removeUserReportCreationForm = new RemoveUserReportCreationForm(this._app, e.User);
                removeUserReportCreationForm.FormClosed += ((s, args) =>
                { 
                    // If RemoveUserReportCreationForm has been closed by user pressing save button...
                    if (removeUserReportCreationForm.Commit)
                        // .. attempt to commit RemoveUser data.
                        this.OnReportDataUploadRequired(sender, e);
                    // otherwise ..
                    else
                        // Cancel UI task.
                        this.OnAwaitingReportCanceled(sender, e);
                });

                removeUserReportCreationForm.ShowDialog(this._form);
            }

            // Occurs whenever user will press cancel button rather then save to close RemoveUserReportCreationForm
            private void OnAwaitingReportCanceled(object sender, UserEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled current task.
                this._consolesServices.OnRemoveUser_AwaitingReportCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveUser_AwaitingReportCanceled(sender, e);
            }

            // Occurs whenever user remove report has been provided and required to be committed.
            private void OnReportDataUploadRequired(object sender, UserEventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnRemoveUser_UploadingReportDataBegan(sender, e);

                // Attempt to commit user remove data
                Task removeUserTask = this._userServices.RemoveAsync(e.User.Username);
                removeUserTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if user remove data has been committed successfully
                            this.OnReportDataUploadSucceed(sender, e);
                            break;
                        case TaskStatus.Faulted:
                            // ... get flatten collection of exception responsible for the task failure.
                            IReadOnlyCollection<Exception> innerExceptions = t.Exception.Flatten().InnerExceptions;

                            // If task failed because designated for removal user is currently logged in..
                            if (innerExceptions.Any(ex => ex.GetType() == typeof(AttemptToRemoveLoggedInUserException)))
                                // .. OnAttemptToRemoveLogged
                                this.OnAttemptToRemoveLoggedInUser(sender, e);
                            // Otherwise if task failed because of some other reason...
                            else
                                this.OnReportDataUploadFailed(sender, e);
                            break;
                        case TaskStatus.Canceled:
                            // ... execute if user remove data has been canceled to commit
                            this.OnReportDataUploadCanceled(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever user received from IT data get uploaded successfully
            private void OnReportDataUploadSucceed(object sender, UserEventArgs e)
            {
                // Displays appropriate logs informing user that application successfully committed the data.
                this._consolesServices.OnRemoveUser_ReportDataUploadSucceed(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveUser_ReportDataUploadSucceed(sender, e);
            }

            private void OnAttemptToRemoveLoggedInUser(object sender, UserEventArgs e)
            {
                // Displays appropriate logs informing user about failed data upload.
                this._consolesServices.OnRemoveUser_AttemptToRemoveLoggedInUser(sender, e);

                // Informs user about application remove user because saturn 5 was 
                MessageBox.Show("Unable to remove currently logged in user.", "Unable to remove User.", MessageBoxButtons.OK);

                // Cancel saturn 5 removal operation.
                this.OnReportDataUploadCanceled(sender, e);
            }

            // Occurs whenever user remove data failed to get uploaded.
            private void OnReportDataUploadFailed(object sender, UserEventArgs e)
            {
                // Displays appropriate logs informing user about failed data upload.
                this._consolesServices.OnRemoveUser_ReportDataUploadFailed(sender, e);

                // Informs user about application failed to send user to IT and as such being unable to continue.
                MessageBox.Show("Application failed to remove user unit from the depot stock and must be closed.", "User remove failed.", MessageBoxButtons.OK);

                // Close the application.
                this._form.Close();
            }

            // Occurs whenever user remove it data canceled to get uploaded.
            private void OnReportDataUploadCanceled(object sender, UserEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled data upload
                this._consolesServices.OnRemoveUser_ReportDataUploadCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveUser_ReportDataUploadCanceled(sender, e);
            }
            #endregion

            #region Subscribe/Unsubscribe events
            private void StartListenForUsernameInput()
            {
                if (this._form.InvokeRequired)
                {
                    Action d = new Action(StartListenForUsernameInput);
                    if (!_form.IsDisposed)
                        _form.Invoke(d);
                }
                else if (!this.tbxAdminUserUsername.IsDisposed)
                {
                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    this.AwaitingUsernameCanceled += this.OnAwaitingUsernameCanceled;

                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    //this.ValidatingUsernameCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxAdminUserUsername.InputValidationBegan -= this.OnProvidedUsernameValidationBegan;
                    this.tbxAdminUserUsername.InputValidationBegan += this.OnProvidedUsernameValidationBegan;

                    this.tbxAdminUserUsername.InputValidationFailed -= this.OnProvidedUsernameValidationFailed;
                    this.tbxAdminUserUsername.InputValidationFailed += this.OnProvidedUsernameValidationFailed;

                    this.tbxAdminUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    //this.tbxAdminUserUsername.InputValidationCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxAdminUserUsername.ValidInputValueProvided -= this.OnValidUsernameProvided;
                    this.tbxAdminUserUsername.ValidInputValueProvided += this.OnValidUsernameProvided;

                    this.tbxAdminUserUsername.EmptyInputValueProvided -= this.OnEmptyUsernameProvided;
                    this.tbxAdminUserUsername.EmptyInputValueProvided += this.OnEmptyUsernameProvided;

                    this.tbxAdminUserUsername.InvalidInputValueProvided -= this.OnInvalidUsernameProvided;
                    this.tbxAdminUserUsername.InvalidInputValueProvided += this.OnInvalidUsernameProvided;
                }
            }

            private void ChangeCancelationMethodForUsernameInput(UserUsernameEventArgs e)
            {
                if (this.tbxAdminUserUsername.InvokeRequired)
                {
                    Action<UserUsernameEventArgs> d = new Action<UserUsernameEventArgs>(ChangeCancelationMethodForUsernameInput);
                    if (!this.tbxAdminUserUsername.IsDisposed)
                        this.tbxAdminUserUsername.Invoke(d, e);
                }
                else if (!this.tbxAdminUserUsername.IsDisposed)
                {
                    this._usernameValidationCancelationArgs = e;

                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    //this.AwaitingUsernameCanceled += this.OnAwaitingUsernameCanceled;

                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.ValidatingUsernameCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxAdminUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxAdminUserUsername.InputValidationCanceled += this.OnProvidedUsernameValidationCanceled;
                }
            }

            private void StopListenForUsernameInput()
            {
                if (this.tbxAdminUserUsername.InvokeRequired)
                {
                    Action d = new Action(StopListenForUsernameInput);
                    if (!tbxAdminUserUsername.IsDisposed)
                        tbxAdminUserUsername.Invoke(d);
                }
                else if (!this.tbxAdminUserUsername.IsDisposed)
                {
                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxAdminUserUsername.InputValidationBegan -= this.OnProvidedUsernameValidationBegan;
                    this.tbxAdminUserUsername.InputValidationFailed -= this.OnProvidedUsernameValidationFailed;
                    this.tbxAdminUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxAdminUserUsername.ValidInputValueProvided -= this.OnValidUsernameProvided;
                    this.tbxAdminUserUsername.EmptyInputValueProvided -= this.OnEmptyUsernameProvided;
                    this.tbxAdminUserUsername.InvalidInputValueProvided -= this.OnInvalidUsernameProvided;

                    this._usernameValidationCancelationArgs = null;
                }
            }
            #endregion
            #endregion
        }
    }
}
