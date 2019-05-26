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
        // Logs user in.
        public class LogInUITask : IUITask<EventArgs>
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

            // Options Tab Username text box
            private UserUsernameValidatingTextBox tbxOptionsUserUsername { get { return this._form.tbxOptionsUserUsername; } }            
            #endregion

            #region Constructor
            // Default constructor.
            public LogInUITask(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public void Trigger(object sender, EventArgs e)
            {
                this.OnAwaitingUsername(sender, e);
                this.ValidatingUsernameCanceled?.Invoke(sender, this._usernameValidationCancelationArgs ?? throw new InvalidOperationException());
            }
            
            public void Cancel(object sender, EventArgs e)
            {
                this.AwaitingUsernameCanceled?.Invoke(sender, e);
            }
            #endregion

            #region Private Helpers
            #region LogIn Operations EventHandlers - Operation Logic
            // Occurs whenever user provided valid username into the appropriate options text box
            private void OnAwaitingUsername(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnLogIn_AwaitingUsername(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Start listening for valid/invalid/empty username
                this.StartListenForUsernameInput();

                // Enables user username input text box and disables
                this._controlsEnabler.OnLogIn_AwaitingUsername(sender, e);
            }

            private void OnAwaitingUsernameCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for username text box input.
                this.StopListenForUsernameInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnLogIn_AwaitingUsernameCanceled(sender, e);

                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnLogIn_AwaitingUsernameCanceled(sender, e);
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
                this._consolesServices.OnLogIn_ProvidedUsernameValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about empty username input being provided.
                this._consolesServices.OnLogIn_ProvidedUsernameValidationFailed(sender, e);

                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForUsernameInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnLogIn_ProvidedUsernameValidationCanceled(sender, e);

                // Displays appropriate logs informing
                this._consolesServices.OnLogIn_ProvidedUsernameValidationCanceled(sender, e);
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
                this._consolesServices.OnLogIn_EmptyUsernameProvided(sender, e);
            }

            // Occurs whenever user will provide invalid username.
            private void OnInvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about invalid username being provided.
                this._consolesServices.OnLogIn_InvalidUsernameProvided(sender, e);
            }

            // Occurs when user will provide valid username. (Or reattempt to log in after failure.)
            private void OnValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Stop listening for valid/invalid/empty username
                this.StopListenForUsernameInput();
                
                // Displays appropriate logs informing user about valid username being provided.
                this._consolesServices.OnLogIn_ValidUsernameProvided(sender, e);

                // Disables user username input text box
                this._controlsEnabler.OnLogIn_ValidUsernameProvided(sender, e);                

                // Displays appropriate logs informing user that application began to obtaining user data based on the provided username
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
                            // ... execute if successfully obtained user data.
                            this.OnSuccessfullyRetrievedUserData(sender, new UserEventArgs(t.Result));
                            break;
                        case TaskStatus.Faulted:
                            // ... execute if failed to obtain user data.
                            this.OnFailToRetrieveUserData(sender, e);
                            break;
                        case TaskStatus.Canceled:
                            // ... execute if attempt to retrieve user data has been canceled.
                            this.OnCancelToRetrieveUserData(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever user data has been successfully retrieved from database. (Even if the data was already cached.)
            // (Or reattempt to log in after failure.)
            private void OnSuccessfullyRetrievedUserData(object sender, UserEventArgs e)
            {
                // Displays appropriate logs informing user that application completed obtaining user data.
                this._consolesServices.OnRetrievingUserDataCompleted(sender, e);

                // Set text boxes according to the current state of the ui task.
                this._dataDisplayServices.OnRetrievingUserDataCompleted(sender, e);

                // Attempt to log in...
                Task logInTask = this._app.LogInAsync(e.User.Username);
                logInTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if successfully logged in.
                            this.OnSucceed(sender, new UserEventArgs(e.User));
                            break;
                            // ... execute if failed to log in.
                        case TaskStatus.Faulted:
                            this.OnFailed(sender, e);
                            break;
                            // ... execute log in task has been canceled.
                        case TaskStatus.Canceled:
                            this.OnCanceled(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever application failed to retrieve user data.
            private void OnFailToRetrieveUserData(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user that application failed to obtaining user data based on the provided username.
                this._consolesServices.OnRetrievingUserDataFailed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Application failed to obtain user data using provided username: {e.Username} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to obtain user data.", MessageBoxButtons.RetryCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        this.OnCancelToRetrieveUserData(sender, e);
                        break;
                    case DialogResult.Retry:
                        this.OnValidUsernameProvided(sender, e);
                        break;
                }
            }

            // Occurs whenever attempt to retrieve user data has been canceled.
            private void OnCancelToRetrieveUserData(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnRetrievingUserDataCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Set text boxes according to the current state of the ui task.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears content of the info boxes.
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }
            
            // Occurs when user logged in successfully.
            private void OnSucceed(object sender, UserEventArgs e)
            {
                // Enables appropriate tabs and buttons depending on the type of logged in user
                this._consolesServices.OnLogIn_Successfully(sender, e);
                this._controlsEnabler.OnLogIn_Succesfully(sender, e);
            }

            // Occurs when user fail to log in.
            private void OnFailed(object sender, UserEventArgs e)
            {
                // Enables appropriate tabs and buttons depending on the type of logged in user
                this._consolesServices.OnLogIn_Failed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Application failed to log in using provided username: {e.User.Username} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to log in.", MessageBoxButtons.RetryCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        this.OnCanceled(sender, e);
                        break;
                    case DialogResult.Retry:
                        this.OnSuccessfullyRetrievedUserData(sender, e);
                        break;
                }
            }

            // Occurs when attempt to log in got canceled.
            private void OnCanceled(object sender, UserEventArgs e)
            {
                // Enables appropriate tabs and buttons depending on the type of logged in user
                this._consolesServices.OnLogIn_Cancel(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnLogIn_Canceled(sender, e);
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
                else if (!this.tbxOptionsUserUsername.IsDisposed)
                {
                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    this.AwaitingUsernameCanceled += this.OnAwaitingUsernameCanceled;

                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    //this.ValidatingUsernameCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxOptionsUserUsername.InputValidationBegan -= this.OnProvidedUsernameValidationBegan;
                    this.tbxOptionsUserUsername.InputValidationBegan += this.OnProvidedUsernameValidationBegan;

                    this.tbxOptionsUserUsername.InputValidationFailed -= this.OnProvidedUsernameValidationFailed;
                    this.tbxOptionsUserUsername.InputValidationFailed += this.OnProvidedUsernameValidationFailed;

                    this.tbxOptionsUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    //this.tbxOptionsUserUsername.InputValidationCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxOptionsUserUsername.ValidInputValueProvided -= this.OnValidUsernameProvided;
                    this.tbxOptionsUserUsername.ValidInputValueProvided += this.OnValidUsernameProvided;

                    this.tbxOptionsUserUsername.EmptyInputValueProvided -= this.OnEmptyUsernameProvided;
                    this.tbxOptionsUserUsername.EmptyInputValueProvided += this.OnEmptyUsernameProvided;

                    this.tbxOptionsUserUsername.InvalidInputValueProvided -= this.OnInvalidUsernameProvided;
                    this.tbxOptionsUserUsername.InvalidInputValueProvided += this.OnInvalidUsernameProvided;
                }
            }

            private void ChangeCancelationMethodForUsernameInput(UserUsernameEventArgs e)
            {
                if (this.tbxOptionsUserUsername.InvokeRequired)
                {
                    Action<UserUsernameEventArgs> d = new Action<UserUsernameEventArgs>(ChangeCancelationMethodForUsernameInput);
                    if (!this.tbxOptionsUserUsername.IsDisposed)
                        this.tbxOptionsUserUsername.Invoke(d, e);
                }
                else if (!this.tbxOptionsUserUsername.IsDisposed)
                {
                    this._usernameValidationCancelationArgs = e;

                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    //this.AwaitingUsernameCanceled += this.OnAwaitingUsernameCanceled;

                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.ValidatingUsernameCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxOptionsUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxOptionsUserUsername.InputValidationCanceled += this.OnProvidedUsernameValidationCanceled;
                }
            }

            private void StopListenForUsernameInput()
            {
                if (this.tbxOptionsUserUsername.InvokeRequired)
                {
                    Action d = new Action(StopListenForUsernameInput);
                    if (!tbxOptionsUserUsername.IsDisposed)
                        tbxOptionsUserUsername.Invoke(d);
                }
                else if (!this.tbxOptionsUserUsername.IsDisposed)
                {
                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxOptionsUserUsername.InputValidationBegan -= this.OnProvidedUsernameValidationBegan;
                    this.tbxOptionsUserUsername.InputValidationFailed -= this.OnProvidedUsernameValidationFailed;
                    this.tbxOptionsUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxOptionsUserUsername.ValidInputValueProvided -= this.OnValidUsernameProvided;
                    this.tbxOptionsUserUsername.EmptyInputValueProvided -= this.OnEmptyUsernameProvided;
                    this.tbxOptionsUserUsername.InvalidInputValueProvided -= this.OnInvalidUsernameProvided;

                    this._usernameValidationCancelationArgs = null;
                }
            }
            #endregion
            #endregion
        }
    }
}
