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
    // Options tab presentation logic
    public partial class MainForm
    {
        public class AllocateSaturn5ByShortIdUITask : IUITask<EventArgs>
        {
            #region Events
            private event EventHandler AwaitingUsernameCanceled;

            private event EventHandler<UserUsernameEventArgs> ValidatingUsernameCanceled;

            private event EventHandler AwaitingShortIdCanceled;

            private event EventHandler<UserWithSaturn5ShortIdEventArgs> ValidatingShortIdCanceled;
            
            #region EventArgs
            private UserUsernameEventArgs _usernameValidationCancelationArgs;

            private UserWithSaturn5ShortIdEventArgs _shortIdValidationCancelationArgs;
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
            
            // Pre-Brief Tab Saturn5 Short Id text box
            private UserWithSaturn5ShortIdValidatingTextBox tbxPreBriefSaturn5Barcode { get { return this._form.tbxPreBriefSaturn5Barcode; } }
            #endregion

            #region Constructor
            // Default Constructor
            public AllocateSaturn5ByShortIdUITask(MainForm form)
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

                this.AwaitingShortIdCanceled?.Invoke(sender, e);
                this.ValidatingShortIdCanceled?.Invoke(sender, this._shortIdValidationCancelationArgs ?? throw new InvalidOperationException());
            }
            #endregion

            #region Private Helpers
            #region AllocateSaturn5ByShortIdUITask Operation EventHandlers - Operation Logic
            // Occurs whenever user provided valid username into the appropriate text box
            private void OnAwaitingUsername(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnAllocateSaturn5ByShortId_AwaitingUsername(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Start listening for valid username
                this.StartListenForUsernameInput();

                // Enables user username input text box
                this._controlsEnabler.OnAllocateSaturn5ByShortId_AwaitingUsername(sender, e);
            }

            // Occurs whenever user pressed cancel button while the UI task was waiting for the username
            private void OnAwaitingUsernameCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for username text box input.
                this.StopListenForUsernameInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5ByShortId_AwaitingUsernameCanceled(sender, e);

                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnAllocateSaturn5ByShortId_AwaitingUsernameCanceled(sender, e);
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
                // Subscribe to appropriate cancellation method 
                this.ChangeCancelationMethodForUsernameInput(e);

                // Displays appropriate logs informing user about empty username input being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_ProvidedUsernameValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedUsernameValidationFailed(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about empty username input being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_ProvidedUsernameValidationFailed(sender, e);

                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedUsernameValidationCanceled(object sender, UserUsernameEventArgs e)
            {
                // Stop listening for username text box input.
                this.StopListenForUsernameInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5ByShortId_ProvidedUsernameValidationCanceled(sender, e);

                // Displays appropriate logs informing
                this._consolesServices.OnAllocateSaturn5ByShortId_ProvidedUsernameValidationCanceled(sender, e);
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
                this._consolesServices.OnAllocateSaturn5ByShortId_EmptyUsernameProvided(sender, e);
            }

            // Occurs whenever user will provide invalid username.
            private void OnInvalidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Displays appropriate logs informing user about invalid username being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_InvalidUsernameProvided(sender, e);
            }

            // Occurs when user will provide valid username. (Or reattempt to log in after failure.)
            private void OnValidUsernameProvided(object sender, UserUsernameEventArgs e)
            {
                // Stop listening for user username
                this.StopListenForUsernameInput();
                
                // Displays appropriate logs informing user about valid username being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_ValidUsernameProvided(sender, e);

                // Disables user username input text box
                this._controlsEnabler.OnAllocateSaturn5ByShortId_ValidUsernameProvided(sender, e);

                // Displays appropriate logs informing user that application began to obtaining user data based on the provided username
                this._consolesServices.OnRetrievingUserDataBegan(sender, e);

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingUserDataBegan(sender, e);

                // Attempts to obtain user...
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

                // Await shortId number..
                this.OnAwaitingShortId(sender, e);
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
                this._controlsEnabler.OnAllocateSaturn5ByShortId_CancelToRetrieveUserData(sender, e);
            }
            
            // Occurs whenever user provided valid username and user data has been successfully retrieved.
            private void OnAwaitingShortId(object sender, UserEventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnAllocateSaturn5ByShortId_AwaitingShortId(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearSaturn5RelatedDisplayTextBoxes(sender, e);

                // Start listening for valid saturn 5 short id
                this.StartListenForShortIdInput();

                // Enable short id text box
                this._controlsEnabler.OnAllocateSaturn5ByShortId_AwaitingShortId(sender, e);
            }

            private void OnAwaitingShortIdCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for short id text box input.
                this.StopListenForShortIdInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5ByShortId_AwaitingShortIdCanceled(sender, e);

                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnAllocateSaturn5ByShortId_AwaitingShortIdCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }

            // Occurs whenever user pressed enter when appropriate for this UI task validation box is enable, 
            // and right before a validation of provided by the user value will begin.
            private void OnProvidedShortIdValidationBegan(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                // Subscribe to appropriate cancellation method 
                this.ChangeCancelationMethodForShortIdInput(e);

                // Displays appropriate logs informing user about empty short id input being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_ProvidedShortIdValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedShortIdValidationFailed(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                // Displays appropriate logs informing user about empty short id input being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_ProvidedShortIdValidationFailed(sender, e);

                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedShortIdValidationCanceled(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                // Stop listening for short idtext box input.
                this.StopListenForShortIdInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5ByShortId_ProvidedShortIdValidationCanceled(sender, e);

                // Displays appropriate logs informing
                this._consolesServices.OnAllocateSaturn5ByShortId_ProvidedShortIdValidationCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }

            // Occurs whenever user will provide empty value in place of valid short id number.
            private void OnEmptyShortIdProvided(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                // Displays appropriate logs informing user about empty short id number input being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_EmptyShortIdProvided(sender, e);
            }

            // Occurs whenever user will provide invalid short id.
            private void OnInvalidShortIdProvided(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                // Displays appropriate logs informing user about invalid short id being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_InvalidShortIdProvided(sender, e);
            }

            // Occurs whenever user provided valid short id into the appropriate preBrief text box
            private void OnValidShortIdProvided(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                // Stop listening for saturn 5 short id
                this.StopListenForShortIdInput();

                // Displays appropriate logs informing user about valid short id being provided.
                this._consolesServices.OnAllocateSaturn5ByShortId_ValidShortIdProvided(sender, new Saturn5ShortIdEventArgs(e.ShortId));

                // Disables saturn short id input text box
                this._controlsEnabler.OnAllocateSaturn5ByShortId_ValidShortIdProvided(sender, e);

                // Displays appropriate logs informing user that application began to obtaining Saturn5 data based on the provided short id
                this._consolesServices.OnRetrievingSaturn5DataByShortIdBegan(sender, new Saturn5ShortIdEventArgs(e.ShortId));

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingSaturn5DataByShortIdBegan(sender, new Saturn5ShortIdEventArgs(e.ShortId));

                // Attempt to obtain saturn 5 data...
                Task<Saturn5> getSaturn5Task = this._saturn5Services.GetByShortIdAsync(e.ShortId);
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
                this._consolesServices.OnAllocateSaturn5ByShortId_UploadingSaturn5AllocationDataBegan(sender, e);
               
                // Attempt to allocate saturn 5 by short id ...
                Task allocateSaturn5ByShortIdTask = this._preBriefServices.AllocateSaturn5ByShortIdAsync(e.Saturn5.ShortId, e.User.Username);
                // ... once finished...
                allocateSaturn5ByShortIdTask.ContinueWith((t) =>
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

            // Occurs whenever app fails to obtain saturn 5 data from provided valid short id.
            private void OnFailToRetrieveSaturn5Data(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                // Displays appropriate logs informing user that application failed to obtaining saturn 5 data .
                this._consolesServices.OnRetrievingSaturn5DataByShortIdFailed(sender, new Saturn5ShortIdEventArgs(e.ShortId));

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Application failed to obtain saturn 5 data using provided barcode: {e.ShortId} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to obtain saturn 5 data.", MessageBoxButtons.RetryCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        this.OnCancelToRetrieveSaturn5Data(sender, e);
                        break;
                    case DialogResult.Retry:
                        this.OnValidShortIdProvided(sender, e);
                        break;
                }
            }

            // Occurs whenever app canceled to obtain saturn 5 data from provided valid short id.
            private void OnCancelToRetrieveSaturn5Data(object sender, UserWithSaturn5ShortIdEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled obtaining saturn 5 data.
                this._consolesServices.OnRetrievingSaturn5DataByShortIdCanceled(sender, new Saturn5ShortIdEventArgs(e.ShortId));
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5ByShortId_CancelToRetrieveSaturn5Data(sender, e);
            }
            
            // Occurs whenever saturn 5 allocation completed successfully.
            private void OnSucceed(object sender, UserWithSaturn5EventArgs e)
            {
                // Informs user about success of the saturn 5 allocation
                this._consolesServices.OnAllocateSaturn5ByShortId_Succeed(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5ByShortId_Succeed(sender, e);
            }

            // Occurs whenever saturn5 unit provided for allocation has been found to be marked as damaged.
            private void OnAttemptToAllocateDamagedSaturn5(object sender, UserWithSaturn5EventArgs e)
            {
                // Inform user that saturn 5 unit provided by then for allocation is damaged and cannot be allocated.
                this._consolesServices.OnAllocateSaturn5ByShortId_AttemptToAllocateDamagedSaturn5(sender, new Saturn5EventArgs(e.Saturn5));
                DialogResult result = MessageBox.Show($"Saturn 5 serial number: {e.Saturn5.SerialNumber} has been marked as damaged and cannot be allocated to the user. Would you like to try to allocate different Saturn 5 unit (Yes) or cancel (No)?", "Unable to allocate damage Saturn 5 unit.", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    this.OnAwaitingShortId(sender, new UserEventArgs(e.User));
                else
                    this.OnCanceled(sender, e);
            }

            // Occurs whenever saturn5 unit provided for allocation has been found to be marked as faulty.
            private void OnAttemptToAllocateFaultySaturn5(object sender, UserWithSaturn5EventArgs e)
            {
                // Inform user that saturn 5 unit provided by then for allocation is faulty and cannot be allocated.
                this._consolesServices.OnAllocateSaturn5ByShortId_AttemptToAllocateFaultySaturn5(sender, new Saturn5EventArgs(e.Saturn5));
                DialogResult result = MessageBox.Show($"Saturn 5 serial number: {e.Saturn5.SerialNumber} has been marked as faulty and cannot be allocated to the user. Would you like to try to allocate different Saturn 5 unit (Yes) or cancel (No)?", "Unable to allocate damage Saturn 5 unit.", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes) this.OnAwaitingShortId(sender, new UserEventArgs(e.User));
                else
                    this.OnCanceled(sender, e);
            }

            // Occurs whenever saturn 5 allocation failed from some other reason then unit being faulty or damaged.
            private void OnFailed(object sender, UserWithSaturn5EventArgs e)
            {
                // Print operation failure logs.
                this._consolesServices.OnAllocateSaturn5ByShortId_Failed(sender, e);

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
                // Displays appropriate logs informing user that application canceled allocating saturn 5
                this._consolesServices.OnAllocateSaturn5ByShortId_Canceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnAllocateSaturn5ByShortId_Canceled(sender, e);
            }
            #endregion

            #region Subscribe/Unsubscribe events
            private void StartListenForUsernameInput()
            {
                if (this.tbxPreBriefUserUsername.InvokeRequired)
                {
                    Action d = new Action(StartListenForUsernameInput);
                    if (!this.tbxPreBriefUserUsername.IsDisposed)
                        this.tbxPreBriefUserUsername.Invoke(d);
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
                    if (!this.tbxPreBriefUserUsername.IsDisposed)
                        this.tbxPreBriefUserUsername.Invoke(d);
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
            
            private void StartListenForShortIdInput()
            {
                if (this.tbxPreBriefSaturn5Barcode.InvokeRequired)
                {
                    Action d = new Action(StartListenForShortIdInput);
                    if (!this.tbxPreBriefSaturn5Barcode.IsDisposed)
                        this.tbxPreBriefSaturn5Barcode.Invoke(d);
                }
                else if (!this.tbxPreBriefSaturn5Barcode.IsDisposed)
                {
                    this.AwaitingShortIdCanceled -= this.OnAwaitingShortIdCanceled;
                    this.AwaitingShortIdCanceled += this.OnAwaitingShortIdCanceled;

                    this.AwaitingShortIdCanceled -= this.OnAwaitingShortIdCanceled;
                    //this.AwaitingShortIdCanceled += this.OnAwaitingShortIdCanceled;

                    this.tbxPreBriefSaturn5Barcode.InputValidationBegan -= this.OnProvidedShortIdValidationBegan;
                    this.tbxPreBriefSaturn5Barcode.InputValidationBegan += this.OnProvidedShortIdValidationBegan;

                    this.tbxPreBriefSaturn5Barcode.InputValidationFailed -= this.OnProvidedShortIdValidationFailed;
                    this.tbxPreBriefSaturn5Barcode.InputValidationFailed += this.OnProvidedShortIdValidationFailed;

                    this.tbxPreBriefSaturn5Barcode.InputValidationCanceled -= this.OnProvidedShortIdValidationCanceled;
                    //this.tbxPreBriefSaturn5Barcode.InputValidationCanceled += this.OnProvidedShortIdValidationCanceled;

                    this.tbxPreBriefSaturn5Barcode.ValidInputValueProvided -= this.OnValidShortIdProvided;
                    this.tbxPreBriefSaturn5Barcode.ValidInputValueProvided += this.OnValidShortIdProvided;

                    this.tbxPreBriefSaturn5Barcode.EmptyInputValueProvided -= this.OnEmptyShortIdProvided;
                    this.tbxPreBriefSaturn5Barcode.EmptyInputValueProvided += this.OnEmptyShortIdProvided;

                    this.tbxPreBriefSaturn5Barcode.InvalidInputValueProvided += this.OnInvalidShortIdProvided;
                    this.tbxPreBriefSaturn5Barcode.InvalidInputValueProvided -= this.OnInvalidShortIdProvided;
                }
            }

            private void ChangeCancelationMethodForShortIdInput(UserWithSaturn5ShortIdEventArgs e)
            {
                if (this.tbxPreBriefSaturn5Barcode.InvokeRequired)
                {
                    Action<UserWithSaturn5ShortIdEventArgs> d = new Action<UserWithSaturn5ShortIdEventArgs>(ChangeCancelationMethodForShortIdInput);
                    if (!this.tbxPreBriefSaturn5Barcode.IsDisposed)
                        this.tbxPreBriefSaturn5Barcode.Invoke(d, e);
                }
                else if (!this.tbxPreBriefSaturn5Barcode.IsDisposed)
                {
                    this._shortIdValidationCancelationArgs = e;

                    this.AwaitingShortIdCanceled -= this.OnAwaitingShortIdCanceled;
                    //this.AwaitingShortIdCanceled += this.OnAwaitingShortIdCanceled;

                    this.ValidatingShortIdCanceled -= this.OnProvidedShortIdValidationCanceled;
                    this.ValidatingShortIdCanceled += this.OnProvidedShortIdValidationCanceled;

                    this.tbxPreBriefSaturn5Barcode.InputValidationCanceled -= this.OnProvidedShortIdValidationCanceled;
                    this.tbxPreBriefSaturn5Barcode.InputValidationCanceled += this.OnProvidedShortIdValidationCanceled;
                }
            }

            private void StopListenForShortIdInput()
            {
                if (this.tbxPreBriefSaturn5Barcode.InvokeRequired)
                {
                    Action d = new Action(StopListenForShortIdInput);
                    if (!this.tbxPreBriefSaturn5Barcode.IsDisposed)
                        this.tbxPreBriefSaturn5Barcode.Invoke(d);
                }
                else if (!this.tbxPreBriefSaturn5Barcode.IsDisposed)
                {
                    this.AwaitingShortIdCanceled -= this.OnAwaitingShortIdCanceled;
                    this.ValidatingShortIdCanceled -= this.OnProvidedShortIdValidationCanceled;
                    this.tbxPreBriefSaturn5Barcode.InputValidationBegan -= this.OnProvidedShortIdValidationBegan;
                    this.tbxPreBriefSaturn5Barcode.InputValidationFailed -= this.OnProvidedShortIdValidationFailed;
                    this.tbxPreBriefSaturn5Barcode.InputValidationCanceled -= this.OnProvidedShortIdValidationCanceled;
                    this.tbxPreBriefSaturn5Barcode.ValidInputValueProvided -= this.OnValidShortIdProvided;
                    this.tbxPreBriefSaturn5Barcode.EmptyInputValueProvided -= this.OnEmptyShortIdProvided;
                    this.tbxPreBriefSaturn5Barcode.InvalidInputValueProvided -= this.OnInvalidShortIdProvided;

                    this._shortIdValidationCancelationArgs = null;
                }
            }
            #endregion
            #endregion
        }
    }
}
