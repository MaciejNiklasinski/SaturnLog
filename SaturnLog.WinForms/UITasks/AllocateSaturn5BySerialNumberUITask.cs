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
        // Allocates Saturn5 unit to the specified user.
        public class AllocateSaturn5BySerialNumberUITask : IUITask<EventArgs>
        {
            #region Events
            private event EventHandler AwaitingUsernameCanceled;

            private event EventHandler<UserUsernameEventArgs> ValidatingUsernameCanceled;

            private event EventHandler AwaitingSerialNumberCanceled;

            private event EventHandler<UserWithSaturn5SerialNumberEventArgs> ValidatingSerialNumberCanceled;
            
            #region EventArgs
            private UserUsernameEventArgs _usernameValidationCancelationArgs;

            private UserWithSaturn5SerialNumberEventArgs _serialNumberValidationCancelationArgs;
            #endregion
            #endregion

            #region Private Fields
            // Main form
            private MainForm _form;

            // Domain layer facade
            private App _app { get { return this._form._app; } }

            // Helper class providing user related services.
            private UserServices _userServices { get { return this._form._app.UserServices; } }

            // Helper class providing saturn5 related services.
            private Saturn5Services _saturn5Services { get { return this._form._app.Saturn5Services; } }

            // Helper class providing Pre-brief operations related services.
            private PreBriefServices _preBriefServices { get { return this._form._app.PreBriefServices; } }
            
            // Helper class responsible for printing to 'Consoles' text boxes.
            private ConsolesServices _consolesServices { get { return this._form._consolesServices; } }

            // Helper class responsible for displaying specific user/saturn5 etc related data.
            private DataDisplayServices _dataDisplayServices { get { return this._form._dataDisplayServices; } }

            // Helper class responsible for enabling and disabling tabs/buttons/text boxes
            private ControlsEnabler _controlsEnabler { get { return this._form._controlsEnabler; } }

            // Pre-Brief Tab Username text box
            private UserUsernameValidatingTextBox tbxPreBriefUserUsername { get { return this._form.tbxPreBriefUserUsername; } }

            // Pre-Brief Tab Saturn5 Serial Number text box
            private UserWithSaturn5SerialNumberValidatingTextBox tbxPreBriefSaturn5SerialNumber { get { return this._form.tbxPreBriefSaturn5SerialNumber; } }
            #endregion

            //#region Properties - IUITask Support
            //// Flag indicating whether the task is currently retrieving data
            //public bool RetrievingData { get; private set; }
            //
            //// Flag indicating whether the task is currently uploading data
            //public bool UploadingData { get; private set; }
            //#endregion

            #region Constructor
            // Default constructor.
            public AllocateSaturn5BySerialNumberUITask(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods - IUITask Support
            //public async Task CancelRetrievingDataAsync()
            //{
            //
            //}
            //
            //public async Task AwaitRetrievingDataCompletionAsync()
            //{
            //
            //}
            //
            //
            //public async Task AwaitUploadingDataCompletionAsync()
            //{
            //
            //}

            public void Trigger(object sender, EventArgs e)
            {
                this.OnAwaitingUsername(sender, e);
            }
            
            public void Cancel(object sender, EventArgs e)
            {
                this.AwaitingUsernameCanceled?.Invoke(sender, e);
                this.ValidatingUsernameCanceled?.Invoke(sender, this._usernameValidationCancelationArgs ?? throw new InvalidOperationException());

                this.AwaitingSerialNumberCanceled?.Invoke(sender, e);
                this.ValidatingSerialNumberCanceled?.Invoke(sender, this._serialNumberValidationCancelationArgs ?? throw new InvalidOperationException());
            }
            #endregion

            #region Private Helpers
            #region AllocateSaturn5BySerialNumberUITask Operation EventHandlers - Operation Logic
            // Occurs whenever user provided valid username into the appropriate text box
            private void OnAwaitingUsername(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_AwaitingUsername(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Start listening for valid username
                this.StartListenForUsernameInput();

                // Enables user username input text box
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_AwaitingUsername(sender, e);
            }

            // Occurs whenever user pressed cancel button while the UI task was waiting for the username
            private void OnAwaitingUsernameCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for username text box input.
                this.StopListenForUsernameInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_AwaitingUsernameCanceled(sender, e);

                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_AwaitingUsernameCanceled(sender, e);
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
                this._consolesServices.OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about empty username input being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationFailed(sender, e);
                
                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedUsernameValidationCanceled (object sender, UserUsernameEventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForUsernameInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled(sender, e);

                // Displays appropriate logs informing
                this._consolesServices.OnAllocateSaturn5BySerialNumber_ProvidedUsernameValidationCanceled(sender, e);
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
                this._consolesServices.OnAllocateSaturn5BySerialNumber_EmptyUsernameProvided(sender, e);
            }

            // Occurs whenever user will provide invalid username.
            private void OnInvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about invalid username being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_InvalidUsernameProvided(sender, e);
            }

            // Occurs when user will provide valid username. (Or reattempt to log in after failure.)
            private void OnValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Stop listening for user username
                this.StopListenForUsernameInput();
                
                // Displays appropriate logs informing user about valid username being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_ValidUsernameProvided(sender, e);

                // Disables user username input text box
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_ValidUsernameProvided(sender, e);

                // Displays appropriate logs informing user that application began to obtaining user data based on the provided username
                this._consolesServices.OnRetrievingUserDataBegan(sender, e);

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingUserDataBegan(sender, e);

                // Attempt to retrieve user data.
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

            // Occurs whenever user data has been successfully retrieved from database. (Even if the data was already cached.)
            // (Or reattempt to log in after failure.)
            private void OnSuccessfullyRetrievedUserData(object sender, UserEventArgs e)
            {
                // Displays appropriate logs informing user that application completed obtaining user data.
                this._consolesServices.OnRetrievingUserDataCompleted(sender, e);

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingUserDataCompleted(sender, e);

                // Await serial number..
                this.OnAwaitingSerialNumber(sender, e);
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

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_CancelToRetrieveUserData(sender, e);
            }

            // Occurs whenever user provided valid username and user data has been successfully retrieved.
            private void OnAwaitingSerialNumber(object sender, UserEventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_AwaitingSerialNumber(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearSaturn5RelatedDisplayTextBoxes(sender, e);
                
                // Start listening for valid saturn 5 serial number
                this.StartListenForSerialNumberInput();

                // Enable serial number text box
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_AwaitingSerialNumber(sender, e);
            }
            
            // Occurs whenever canceled to await for serial number.
            private void OnAwaitingSerialNumberCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForSerialNumberInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_AwaitingSerialNumberCanceled(sender, e);

                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_AwaitingSerialNumberCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }

            // Occurs whenever user pressed enter when appropriate for this UI task validation box is enable, 
            // and right before a validation of provided by the user value will begin.
            private void OnProvidedSerialNumberValidationBegan(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                // Subscribe to appropriate method of canceling awaiting for input.
                this.ChangeCancelationMethodForSerialNumberInput(e);

                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedSerialNumberValidationFailed(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed(sender, e);

                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedSerialNumberValidationCanceled(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForSerialNumberInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(sender, e);

                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }

            // Occurs whenever user will provide empty value in place of valid serial number.
            private void OnEmptySerialNumberProvided(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_EmptySerialNumberProvided(sender, e);
            }

            // Occurs whenever user will provide invalid serial number.
            private void OnInvalidSerialNumberProvided(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about invalid serial number being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_InvalidSerialNumberProvided(sender, e);
            }

            // Occurs whenever user provided valid serialNumber into the appropriate preBrief text box
            private void OnValidSerialNumberProvided(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                // Stop listening for saturn 5 serial number
                this.StopListenForSerialNumberInput();

                // Displays appropriate logs informing user about valid serial number being provided.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_ValidSerialNumberProvided(sender, e);

                // Disables saturn serial number input text box
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_ValidSerialNumberProvided(sender, e);

                // Displays appropriate logs informing user that application began to obtaining Saturn5 data based on the provided serial number
                this._consolesServices.OnRetrievingSaturn5DataBySerialNumberBegan(sender, new Saturn5SerialNumberEventArgs(e.SerialNumber));
                
                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingSaturn5DataBySerialNumberBegan(sender, new Saturn5SerialNumberEventArgs(e.SerialNumber));

                // Attempt to obtain saturn 5 data...
                Task<Saturn5> getSaturn5Task = this._saturn5Services.GetBySerialNumberAsync(e.SerialNumber);
                getSaturn5Task.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if succeed to obtain saturn 5 data.
                            this.OnSuccessfullyRetrievedSaturn5Data(sender, new UserWithSaturn5EventArgs(e.User, t.Result));
                            break;
                        case TaskStatus.Faulted:
                            // ... execute if failed to obtain saturn 5 data.
                            this.OnFailToRetrieveSaturn5Data(sender, e);
                            break;
                        case TaskStatus.Canceled:
                            // ... execute if attempt to obtain saturn 5 data has been canceled.
                            this.OnCancelToRetrieveSaturn5Data(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            
            // Occurs whenever Saturn5 data got successfully retrieved.
            private void OnSuccessfullyRetrievedSaturn5Data(object sender, UserWithSaturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application completed obtaining saturn 5 data.
                this._consolesServices.OnRetrievingUserAndSaturn5DataCompleted(sender, e);

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingUserAndSaturn5DataCompleted(sender, e);

                // Displays appropriate logs informing user that application began uploading the saturn 5 allocation data.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_UploadingSaturn5AllocationDataBegan(sender, e);
                
                // Attempt to allocate saturn 5 by serial number, ...
                Task allocateSaturn5BySerialNumberTask = this._preBriefServices.AllocateSaturn5BySerialNumberAsync(e.Saturn5.SerialNumber, e.User.Username);
                // ... once finished...
                allocateSaturn5BySerialNumberTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        // ... if allocated saturn 5 successfully...
                        case TaskStatus.RanToCompletion:
                            // ...OnSucceed.
                            this.OnSucceed(sender, new UserWithSaturn5EventArgs(e.User, e.Saturn5));
                            break;
                        
                        // ... if failed to allocate saturn 5...
                        case TaskStatus.Faulted:
                            // ... get flatten collection of exception responsible for the task failure.
                            IReadOnlyCollection<Exception> innerExceptions = t.Exception.Flatten().InnerExceptions;

                            // If task failed because saturn 5 unit which allocation has been attempted is damaged...
                            if (innerExceptions.Any(ex => ex.GetType() == typeof(AttemptToAllocateDamagedSaturn5Exception)))
                                // .. OnAttemptToAllocateDamagedSaturn5
                                this.OnAttemptToAllocateDamagedSaturn5(sender, e);
                            // Otherwise if task failed because saturn 5 unit which allocation has been attempted is faulty...
                            else if (innerExceptions.Any(ex =>ex.GetType() == typeof(AttemptToAllocateFaultySaturn5Exception)))
                                // .. OnAttemptToAllocateFaultySaturn5
                                this.OnAttemptToAllocateFaultySaturn5(sender, e);
                            // Otherwise if task failed because of some other reason...
                            else
                                // .. OnFailed
                                this.OnFailed(sender, e);
                            break;

                        // .. if canceled to allocate saturn 5
                        case TaskStatus.Canceled:
                            // .. OnCanceled
                            this.OnCanceled(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever retrieval of the Saturn5 data failed.
            private void OnFailToRetrieveSaturn5Data(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user that application failed to obtaining saturn 5 data .
                this._consolesServices.OnRetrievingSaturn5DataBySerialNumberFailed(sender, new Saturn5SerialNumberEventArgs(e.SerialNumber));

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Application failed to obtain saturn 5 data using provided serial number: {e.SerialNumber} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to obtain saturn 5 data.", MessageBoxButtons.RetryCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        this.OnCancelToRetrieveSaturn5Data(sender, e);
                        break;
                    case DialogResult.Retry:
                        this.OnValidSerialNumberProvided(sender, e);
                        break;
                }
            }

            // Occurs whenever retrieval of the Saturn5 data has been cancel.
            private void OnCancelToRetrieveSaturn5Data(object sender, UserWithSaturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled obtaining saturn 5 data.
                this._consolesServices.OnRetrievingSaturn5DataBySerialNumberCanceled(sender, new Saturn5SerialNumberEventArgs(e.SerialNumber));
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_CancelToRetrieveSaturn5Data(sender, e);
            }
            
            // Occurs whenever saturn 5 allocation completed successfully.
            private void OnSucceed(object sender, UserWithSaturn5EventArgs e)
            {
                // Informs user about success of the saturn 5 allocation
                this._consolesServices.OnAllocateSaturn5BySerialNumber_Succeed(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_Succeed(sender, e);
            }

            // Occurs whenever saturn5 unit provided for allocation has been found to be marked as damaged.
            private void OnAttemptToAllocateDamagedSaturn5(object sender, UserWithSaturn5EventArgs e)
            {
                // Inform user that saturn 5 unit provided by then for allocation is damaged and cannot be allocated.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_AttemptToAllocateDamagedSaturn5(sender, new Saturn5EventArgs(e.Saturn5));
                DialogResult result = MessageBox.Show($"Saturn 5 serial number: {e.Saturn5.SerialNumber} has been marked as damaged and cannot be allocated to the user. Would you like to try to allocate different Saturn 5 unit (Yes) or cancel (No)?", "Unable to allocate damage Saturn 5 unit.", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    this.OnAwaitingSerialNumber(sender, new UserEventArgs(e.User));
                else
                    this.OnCanceled(sender, e);
            }

            // Occurs whenever saturn5 unit provided for allocation has been found to be marked as faulty.
            private void OnAttemptToAllocateFaultySaturn5(object sender, UserWithSaturn5EventArgs e)
            {
                // Inform user that saturn 5 unit provided by then for allocation is faulty and cannot be allocated.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_AttemptToAllocateFaultySaturn5(sender, new Saturn5EventArgs(e.Saturn5));
                DialogResult result = MessageBox.Show($"Saturn 5 serial number: {e.Saturn5.SerialNumber} has been marked as faulty and cannot be allocated to the user. Would you like to try to allocate different Saturn 5 unit (Yes) or cancel (No)?", "Unable to allocate damage Saturn 5 unit.", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    this.OnAwaitingSerialNumber(sender, new UserEventArgs(e.User));
                else
                    this.OnCanceled(sender, e);
            }

            // Occurs whenever saturn 5 allocation failed from some other reason then unit being faulty or damaged.
            private void OnFailed(object sender, UserWithSaturn5EventArgs e)
            {
                // Print operation failure logs.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_Failed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Failed to allocate Saturn 5. Would You like to retry? Or would you like to cancel and close the application.", "Failed to allocate Saturn 5.", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    // If user pressed 'Retry' button, reattempt to connect with database. 
                    this.OnSuccessfullyRetrievedSaturn5Data(sender, e);
                    return;
                }

                // Otherwise close the application. 
                this._form.Close();
            }

            // Occures whenever saturn 5 allocation process has been canceled.
            private void OnCanceled(object sender, UserWithSaturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnAllocateSaturn5BySerialNumber_Canceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5BySerialNumber_Canceled(sender, e);
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
                else if (!this.tbxPreBriefUserUsername.IsDisposed)
                {
                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    this.AwaitingUsernameCanceled += this.OnAwaitingUsernameCanceled;

                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    //this.ValidatingUsernameCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxPreBriefUserUsername.InputValidationBegan -= this.OnProvidedUsernameValidationBegan;
                    this.tbxPreBriefUserUsername.InputValidationBegan += this.OnProvidedUsernameValidationBegan;

                    this.tbxPreBriefUserUsername.InputValidationFailed -= this.OnProvidedUsernameValidationFailed;
                    this.tbxPreBriefUserUsername.InputValidationFailed += this.OnProvidedUsernameValidationFailed;

                    this.tbxPreBriefUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    //this.tbxPreBriefUserUsername.InputValidationCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxPreBriefUserUsername.ValidInputValueProvided -= this.OnValidUsernameProvided;
                    this.tbxPreBriefUserUsername.ValidInputValueProvided += this.OnValidUsernameProvided;

                    this.tbxPreBriefUserUsername.EmptyInputValueProvided -= this.OnEmptyUsernameProvided;
                    this.tbxPreBriefUserUsername.EmptyInputValueProvided += this.OnEmptyUsernameProvided;

                    this.tbxPreBriefUserUsername.InvalidInputValueProvided -= this.OnInvalidUsernameProvided;
                    this.tbxPreBriefUserUsername.InvalidInputValueProvided += this.OnInvalidUsernameProvided;
                }
            }

            private void ChangeCancelationMethodForUsernameInput(UserUsernameEventArgs e)
            {
                if (this.tbxPreBriefUserUsername.InvokeRequired)
                {
                    Action<UserUsernameEventArgs> d = new Action<UserUsernameEventArgs>(ChangeCancelationMethodForUsernameInput);
                    if (!this.tbxPreBriefUserUsername.IsDisposed)
                        this.tbxPreBriefUserUsername.Invoke(d, e);
                }
                else if (!this.tbxPreBriefUserUsername.IsDisposed)
                {
                    this._usernameValidationCancelationArgs = e;

                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    //this.AwaitingUsernameCanceled += this.OnAwaitingUsernameCanceled;

                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.ValidatingUsernameCanceled += this.OnProvidedUsernameValidationCanceled;

                    this.tbxPreBriefUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxPreBriefUserUsername.InputValidationCanceled += this.OnProvidedUsernameValidationCanceled;
                }
            }

            private void StopListenForUsernameInput()
            {
                if (this.tbxPreBriefUserUsername.InvokeRequired)
                {
                    Action d = new Action(StopListenForUsernameInput);
                    if (!tbxPreBriefUserUsername.IsDisposed)
                        tbxPreBriefUserUsername.Invoke(d);
                }
                else if (!this.tbxPreBriefUserUsername.IsDisposed)
                {
                    this.AwaitingUsernameCanceled -= this.OnAwaitingUsernameCanceled;
                    this.ValidatingUsernameCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxPreBriefUserUsername.InputValidationBegan -= this.OnProvidedUsernameValidationBegan;
                    this.tbxPreBriefUserUsername.InputValidationFailed -= this.OnProvidedUsernameValidationFailed;
                    this.tbxPreBriefUserUsername.InputValidationCanceled -= this.OnProvidedUsernameValidationCanceled;
                    this.tbxPreBriefUserUsername.ValidInputValueProvided -= this.OnValidUsernameProvided;
                    this.tbxPreBriefUserUsername.EmptyInputValueProvided -= this.OnEmptyUsernameProvided;
                    this.tbxPreBriefUserUsername.InvalidInputValueProvided -= this.OnInvalidUsernameProvided;

                    this._usernameValidationCancelationArgs = null;
                }
            }
            
            private void StartListenForSerialNumberInput()
            {
                if (this._form.InvokeRequired)
                {
                    Action d = new Action(StartListenForSerialNumberInput);
                    if (!_form.IsDisposed)
                        _form.Invoke(d);
                }
                else if (!this.tbxPreBriefSaturn5SerialNumber.IsDisposed)
                {
                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    this.AwaitingSerialNumberCanceled += this.OnAwaitingSerialNumberCanceled;

                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    //this.ValidatingSerialNumberCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxPreBriefSaturn5SerialNumber.InputValidationBegan -= this.OnProvidedSerialNumberValidationBegan;
                    this.tbxPreBriefSaturn5SerialNumber.InputValidationBegan += this.OnProvidedSerialNumberValidationBegan;

                    this.tbxPreBriefSaturn5SerialNumber.InputValidationFailed -= this.OnProvidedSerialNumberValidationFailed;
                    this.tbxPreBriefSaturn5SerialNumber.InputValidationFailed += this.OnProvidedSerialNumberValidationFailed;

                    this.tbxPreBriefSaturn5SerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    //this.tbxPreBriefSaturn5SerialNumber.InputValidationCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxPreBriefSaturn5SerialNumber.ValidInputValueProvided -= this.OnValidSerialNumberProvided;
                    this.tbxPreBriefSaturn5SerialNumber.ValidInputValueProvided += this.OnValidSerialNumberProvided;

                    this.tbxPreBriefSaturn5SerialNumber.EmptyInputValueProvided -= this.OnEmptySerialNumberProvided;
                    this.tbxPreBriefSaturn5SerialNumber.EmptyInputValueProvided += this.OnEmptySerialNumberProvided;

                    this.tbxPreBriefSaturn5SerialNumber.InvalidInputValueProvided -= this.OnInvalidSerialNumberProvided;
                    this.tbxPreBriefSaturn5SerialNumber.InvalidInputValueProvided += this.OnInvalidSerialNumberProvided;
                }
            }

            private void ChangeCancelationMethodForSerialNumberInput(UserWithSaturn5SerialNumberEventArgs e)
            {
                if (this.tbxPreBriefSaturn5SerialNumber.InvokeRequired)
                {
                    Action<UserWithSaturn5SerialNumberEventArgs> d = new Action<UserWithSaturn5SerialNumberEventArgs>(ChangeCancelationMethodForSerialNumberInput);
                    if (!this.tbxPreBriefSaturn5SerialNumber.IsDisposed)
                        this.tbxPreBriefSaturn5SerialNumber.Invoke(d, e);
                }
                else if (!this.tbxPreBriefSaturn5SerialNumber.IsDisposed)
                {
                    this._serialNumberValidationCancelationArgs = e;

                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    //this.AwaitingSerialNumberCanceled += this.OnAwaitingSerialNumberCanceled;

                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.ValidatingSerialNumberCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxPreBriefSaturn5SerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxPreBriefSaturn5SerialNumber.InputValidationCanceled += this.OnProvidedSerialNumberValidationCanceled;
                }
            }

            private void StopListenForSerialNumberInput()
            {
                if (this.tbxPreBriefSaturn5SerialNumber.InvokeRequired)
                {
                    Action d = new Action(StopListenForSerialNumberInput);
                    if (!tbxPreBriefSaturn5SerialNumber.IsDisposed)
                        tbxPreBriefSaturn5SerialNumber.Invoke(d);
                }
                else if (!this.tbxPreBriefSaturn5SerialNumber.IsDisposed)
                {
                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxPreBriefSaturn5SerialNumber.InputValidationBegan -= this.OnProvidedSerialNumberValidationBegan;
                    this.tbxPreBriefSaturn5SerialNumber.InputValidationFailed -= this.OnProvidedSerialNumberValidationFailed;
                    this.tbxPreBriefSaturn5SerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxPreBriefSaturn5SerialNumber.ValidInputValueProvided -= this.OnValidSerialNumberProvided;
                    this.tbxPreBriefSaturn5SerialNumber.EmptyInputValueProvided -= this.OnEmptySerialNumberProvided;
                    this.tbxPreBriefSaturn5SerialNumber.InvalidInputValueProvided -= this.OnInvalidSerialNumberProvided;

                    this._serialNumberValidationCancelationArgs = null;
                }
            }
            #endregion
            #endregion
        }
    }
}
