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
        // Confirms damage Saturn 5 unit back in depot using its serial number
        public class ConfirmBackInDamageSaturn5BySerialNumberUITask : IUITask<EventArgs>
        {
            #region Events
            private event EventHandler AwaitingSerialNumberCanceled;

            private event EventHandler<Saturn5SerialNumberEventArgs> ValidatingSerialNumberCanceled;

            #region EventArgs
            private Saturn5SerialNumberEventArgs _serialNumberValidationCancelationArgs;
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

            // Helper class providing De-brief operations related services.
            private DeBriefServices _deBriefServices { get { return this._form._app.DeBriefServices; } }

            // Helper class responsible for printing to 'Consoles' text boxes.
            private ConsolesServices _consolesServices { get { return this._form._consolesServices; } }

            // Helper class responsible for displaying specific user/saturn5 etc related data.
            private DataDisplayServices _dataDisplayServices { get { return this._form._dataDisplayServices; } }

            // Helper class responsible for enabling and disabling tabs/buttons/text boxes
            private ControlsEnabler _controlsEnabler { get { return this._form._controlsEnabler; } }

            private bool _throwOnLastSeenWithStarUser = true;

            #region Text Boxes
            // De-Brief Tab Saturn5 Serial Number text box
            private Saturn5SerialNumberValidatingTextBox tbxDeBriefSaturn5SerialNumber { get { return this._form.tbxDeBriefSaturn5SerialNumber; } }
            #endregion
            #endregion

            #region Constructor
            // Default constructor. 
            public ConfirmBackInDamageSaturn5BySerialNumberUITask(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public void Trigger(object sender, EventArgs e)
            {
                this._throwOnLastSeenWithStarUser = true;
                this.OnAwaitingSerialNumber(sender, e);
            }
            
            public void Cancel(object sender, EventArgs e)
            {
                this.AwaitingSerialNumberCanceled?.Invoke(sender, e);
                this.ValidatingSerialNumberCanceled?.Invoke(sender, this._serialNumberValidationCancelationArgs ?? throw new InvalidOperationException());
            }
            #endregion

            #region Private Helpers
            #region ConfirmBackInDamageSaturn5BySerialNumber Operation EventHandlers - Operation Logic
            // Occurs whenever user pressed the button and UI task is expecting valid serial number to be provided into the appropriate text box.
            private void OnAwaitingSerialNumber(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingSerialNumber(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Start listening for valid serial number.
                this.StartListenForSerialNumberInput();

                // Enables saturn 5 serial number input text box
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingSerialNumber(sender, e);
            }
            
            // Occurs whenever user decided to cancel ui task when it was awaiting for serial number
            private void OnAwaitingSerialNumberCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForSerialNumberInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingSerialNumberCanceled(sender, e);

                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingSerialNumberCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }
            
            // Occurs whenever user pressed enter when appropriate for this UI task validation box is enable, 
            // and right before a validation of provided by the user value will begin.
            private void OnProvidedSerialNumberValidationBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Subscribe to appropriate method of canceling awaiting for input.
                this.ChangeCancelationMethodForSerialNumberInput(e);

                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationFailed(sender, e);

                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForSerialNumberInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(sender, e);

                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_ProvidedSerialNumberValidationCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
            }

            // Occurs whenever user will provide empty value in place of valid serial number.
            private void OnEmptySerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_EmptySerialNumberProvided(sender, e);
            }

            // Occurs whenever user will provide invalid serial number.
            private void OnInvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about invalid serial number being provided.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_InvalidSerialNumberProvided(sender, e);
            }

            // Occurs whenever user provided valid serialNumber into the appropriate text box
            private void OnValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Stop listening for saturn 5 serial number
                this.StopListenForSerialNumberInput();

                // Displays appropriate logs informing user about valid serial number being provided.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_ValidSerialNumberProvided(sender, e);

                // Disables saturn 5 serial number input text box
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_ValidSerialNumberProvided(sender, e);

                // Proceed further into the UITask into the part where Saturn 5 Data is getting attempted to be retrieved using serial number.
                this.OnRetrievingSaturn5DataRequired(sender, e);
            }

            // Occurs whenever UITask required to retrieve saturn 5 data to proceed. 
            private void OnRetrievingSaturn5DataRequired(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user that application began to obtaining Saturn5 data based on the provided serial number
                this._consolesServices.OnRetrievingSaturn5DataBySerialNumberBegan(sender, e);

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingSaturn5DataBySerialNumberBegan(sender, e);

                // Attempt to obtain saturn 5 data...
                Task<Saturn5> getSaturn5Task = this._saturn5Services.GetBySerialNumberAsync(e.SerialNumber);
                getSaturn5Task.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if succeed to obtain saturn 5 data.
                            this.OnSuccessfullyRetrievedSaturn5Data(sender, new Saturn5EventArgs(t.Result));
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

            // Occurs if application fails to obtain saturn 5 data
            private void OnFailToRetrieveSaturn5Data(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user that application failed to obtaining saturn 5 data.
                this._consolesServices.OnRetrievingSaturn5DataBySerialNumberFailed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Application failed to obtain saturn 5 data using provided serial number: {e.SerialNumber} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to obtain saturn 5 data.", MessageBoxButtons.RetryCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        this.OnCancelToRetrieveSaturn5Data(sender, e);
                        break;
                    case DialogResult.Retry:
                        this.OnRetrievingSaturn5DataRequired(sender, e);
                        break;
                }
            }

            // Occurs if retrieving saturn 5 data has been canceled
            private void OnCancelToRetrieveSaturn5Data(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled obtaining saturn 5 data.
                this._consolesServices.OnRetrievingSaturn5DataBySerialNumberCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);
                
                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_CancelToRetrieveSaturn5Data(sender, e);
            }

            // Occurs whenever saturn 5 data got successfully retrieved.
            private void OnSuccessfullyRetrievedSaturn5Data(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application completed obtaining saturn 5 data.
                this._consolesServices.OnRetrievingSaturn5DataCompleted(sender, new Saturn5EventArgs(e.Saturn5));

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingSaturn5DataCompleted(sender, e);

                // Trigger further part of UITask
                this.OnRetrievingUserDataRequired(sender, e);
            }

            // Occurs whenever user provided valid serialNumber into the appropriate text box
            private void OnRetrievingUserDataRequired(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application began to obtaining user data based on the provided username
                this._consolesServices.OnRetrievingUserDataBegan(sender, new UserUsernameEventArgs(e.Saturn5.LastSeenUsername));

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingUserDataBegan(sender, new UserUsernameEventArgs(e.Saturn5.LastSeenUsername));

                // Attempt to obtain user data...
                Task<User> getUserTask = this._userServices.GetAsync(e.Saturn5.LastSeenUsername);
                getUserTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if successfully obtained user data.
                            this.OnSuccessfullyRetrievedUserData(sender, new UserWithSaturn5EventArgs(t.Result, e.Saturn5));
                            break;

                        case TaskStatus.Faulted:
                            // ... get flatten collection of exception responsible for the task failure.
                            IReadOnlyCollection<Exception> innerExceptions = t.Exception.Flatten().InnerExceptions;
                            // If task failed because saturn 5 unit provided for confirmation 
                            // is associated by content of its property LastSeenUsername with invalid user.
                            if (innerExceptions.Any(ex => ex.GetType() == typeof(ArgumentException) && ((ArgumentException)ex).ParamName == "username"))
                                // .. OnSuccessfullyRetrievedSaturn5HasInvalidLastUser
                                this.OnSuccessfullyRetrievedSaturn5HasInvalidLastUser(sender, e);
                            // Otherwise if task failed because of some other reason...
                            else
                                // ... execute if failed to obtain user data.
                                this.OnFailToRetrieveUserData(sender, new UserUsernameWithSaturn5(e.Saturn5.LastSeenUsername, e.Saturn5));
                            break;

                        case TaskStatus.Canceled:
                            // ... execute if attempt to retrieve user data has been canceled.
                            this.OnCancelToRetrieveUserData(sender, new UserUsernameWithSaturn5(e.Saturn5.LastSeenUsername, e.Saturn5));
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs when successfully retrieved saturn5 data but its last seen username is not recognized.
            private void OnSuccessfullyRetrievedSaturn5HasInvalidLastUser(object sender, Saturn5EventArgs e)
            {
                // Ask user what to do in case of saturn 5 unit being allocated to unrecognized/removed user. 
                DialogResult result = MessageBox.Show($"Last known user of the Saturn 5 unit serial number: {e.Saturn5.SerialNumber} has unrecognized username {e.Saturn5.LastSeenUsername}. " +
                    $"In this case application can continue, however currently logged in user: {this._app.LoggedUser.Username} {this._app.LoggedUser.FirstName} {this._app.LoggedUser.Surname}, " +
                    "will be used instead as a last user of the unit. Would You like to Continue (Yes), or Cancel (No).", "Saturn 5 last user is unrecognized.", MessageBoxButtons.YesNo);

                // .. if user asked to cancel....
                if (result == DialogResult.No)
                    this.OnCancelToRetrieveUserData(sender, new UserUsernameWithSaturn5(e.Saturn5.LastSeenUsername, e.Saturn5));
                // .. if user asked to continue....
                else
                    this.OnEmergencyAllocateSaturn5BySerialNumberRequired(sender, e);
            }

            // Occurs when user asked to continue when logged user if forced to take responsibility for the unit as last seen username
            // associated with the saturn 5 unit is not valid.
            private void OnEmergencyAllocateSaturn5BySerialNumberRequired(object sender, Saturn5EventArgs e)
            {
                // Attempt to allocate saturn 5 by serial number, ...
                Task emergencyAllocateSaturn5BySerialNumberTask = this._preBriefServices.EmergencyAllocateSaturn5BySerialNumberAsync(e.Saturn5.SerialNumber, this._app.LoggedUsername);
                emergencyAllocateSaturn5BySerialNumberTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        // ... if emergency allocated saturn 5...
                        case TaskStatus.RanToCompletion:
                            // ...Succeed.
                            this.OnRetrievingSaturn5DataRequired(sender, new Saturn5SerialNumberEventArgs(e.Saturn5.SerialNumber));
                            break;
                        case TaskStatus.Faulted:
                            // ... Failed.
                            this.OnFailToEmergencyAllocateSaturn5BySerialNumber(sender, new UserUsernameWithSaturn5(e.Saturn5.LastSeenUsername, e.Saturn5));
                            break;
                        case TaskStatus.Canceled:
                            // ... Canceled.
                            this.OnCancelToEmergencyAllocateSaturn5BySerialNumber(sender, new UserUsernameWithSaturn5(e.Saturn5.LastSeenUsername, e.Saturn5));
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());                
            }

            // Occurs if application failed to emergency allocate saturn 5 to the logged in user
            private void OnFailToEmergencyAllocateSaturn5BySerialNumber(object sender, UserUsernameWithSaturn5 e)
            {
                // Displays appropriate logs informing user that application canceled to (emergency) allocate the saturn 5 unit into the logged in user.
                this._consolesServices.OnEmergencyAllocateSaturn5BySerialNumber_Canceled(sender, new UserWithSaturn5EventArgs(this._app.LoggedUser, e.Saturn5));

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Application failed to emergency allocate saturn 5 unit {e.Saturn5.SerialNumber} into the currently logged in user {this._app.LoggedUser.Username} {this._app.LoggedUser.FirstName} {this._app.LoggedUser.Surname} user data using provided username: {e.Username} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to emergency allocate the saturn 5 unit.", MessageBoxButtons.RetryCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        this.OnCancelToEmergencyAllocateSaturn5BySerialNumber(sender, e);
                        break;
                    case DialogResult.Retry:
                        this.OnEmergencyAllocateSaturn5BySerialNumberRequired(sender, new Saturn5EventArgs(e.Saturn5));
                        break;
                }
            }

            // Occurs if application canceled to emergency allocate saturn 5 to the logged in user
            private void OnCancelToEmergencyAllocateSaturn5BySerialNumber(object sender, UserUsernameWithSaturn5 e)
            {
                // Displays appropriate logs informing user that application failed to (emergency) allocate the saturn 5 unit into the logged in user.
                this._consolesServices.OnEmergencyAllocateSaturn5BySerialNumber_Failed(sender, new UserWithSaturn5EventArgs(this._app.LoggedUser, e.Saturn5));
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_CancelToRetrieveUserData(sender, e);
            }

            // Occurs if application fails to obtain user data
            private void OnFailToRetrieveUserData(object sender, UserUsernameWithSaturn5 e)
            {
                // Displays appropriate logs informing user that application failed to obtaining user data .
                this._consolesServices.OnRetrievingUserDataFailed(sender, new UserUsernameEventArgs(e.Username));

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Application failed to obtain user data using provided username: {e.Username} {Environment.NewLine}Would You like to Retry or Cancel the operation?", "Failed to obtain user data.", MessageBoxButtons.RetryCancel);
                switch (result)
                {
                    case DialogResult.Cancel:
                        this.OnCancelToRetrieveUserData(sender, e);
                        break;
                    case DialogResult.Retry:
                        this.OnRetrievingUserDataRequired(sender, new Saturn5EventArgs(e.Saturn5));
                        break;
                }
            }

            // Occurs if retrieving user data has been canceled
            private void OnCancelToRetrieveUserData(object sender, UserUsernameWithSaturn5 e)
            {
                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnRetrievingUserDataCanceled(sender, new UserUsernameEventArgs(e.Username));
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_CancelToRetrieveUserData(sender, e);
            }

            // Occurs when canceled whilst application was attempting to retrieve user data.
            private void OnSuccessfullyRetrievedUserData(object sender, UserWithSaturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application completed obtaining user and saturn 5 data.
                this._consolesServices.OnRetrievingUserAndSaturn5DataCompleted(sender, e);
                
                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingUserAndSaturn5DataCompleted(sender, e);
                
                // Proceed withing UITask
                this.OnAwaitingDamageReport(sender, e);
            }

            // Occurs whenever 
            private void OnAwaitingDamageReport(object sender, UserWithSaturn5EventArgs e)
            {
                // Display appropriate logs informing user that application is await to provide damage report.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingDamageReport(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingDamageReport(sender, e);


                // Open damage report creation form .. 
                DamageReportCreationForm damageReportCreationForm = new DamageReportCreationForm(this._app, e.Saturn5);
                damageReportCreationForm.FormClosed += (s, args) => 
                {
                    // If DamageReportCreationForm has been closed by user pressing save button...
                    if (damageReportCreationForm.Commit)
                        this.OnReportDataUploadRequired(sender, new UserWithSaturn5DamageReportEventArgs(e.User, e.Saturn5, damageReportCreationForm.Description));
                    else
                        // .. execute UITask cancellation ..
                        this.OnAwaitingDamageReportCanceled(sender, e);
                };

                damageReportCreationForm.ShowDialog(this._form);
            }

            // Occurs whenever UI task is expecting valid saturn 5 damage report to be provided.
            private void OnReportDataUploadRequired(object sender, UserWithSaturn5DamageReportEventArgs e)
            {
                // Displays appropriate logs informing user that application began uploading the saturn 5 confirmation data.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataBegan(sender, e);
                
                // Report the damage based on the DamageReportCreationForm
                Task reportingDamageTask = this._saturn5Services.ReportDamageAsync(e.Saturn5.SerialNumber, e.User.Username, e.Description);
                reportingDamageTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if successfully reported the damage
                            // Get just reported damage:
                            Saturn5Issue damage = this._saturn5Services.GetLastUnresolvedDamage(e.Saturn5.SerialNumber);
                            this.OnUploadingSaturn5ReportingDamageDataSucceed(sender, new UserWithDamagedSaturn5EventArgs(e.User, e.Saturn5, damage));
                            break;

                        case TaskStatus.Faulted:
                            // ... execute if failed to report the damage
                            this.OnUploadingSaturn5ReportingDamageDataFailed(sender, e);
                            break;

                        case TaskStatus.Canceled:
                            // ... execute if attempt to report the damage has been canceled.
                            this.OnUploadingSaturn5ReportingDamageDataCanceled(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever valid damage report has been provided and it require to be persisted.
            private void OnAwaitingDamageReportCanceled(object sender, UserWithSaturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingDamageReportCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_AwaitingDamageReportCanceled(sender, e);
            }

            // Occurs whenever attempt to commit damage data upload failed
            private void OnUploadingSaturn5ReportingDamageDataFailed(object sender, UserWithSaturn5DamageReportEventArgs e)
            {
                // Print operation failure logs.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataFailed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Failed to report Saturn 5 unit damage. Would You like to retry? Or would you like to cancel and close the application.", "Failed to report Saturn 5 unit damage.", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    // If user pressed 'Retry' button, reattempt to connect with database. 
                    this.OnReportDataUploadRequired(sender, e);
                    return;
                }

                // Otherwise close the application. 
                this._form.Close();
            }

            // Occurs whenever attempt to commit damage data upload has been canceled
            private void OnUploadingSaturn5ReportingDamageDataCanceled(object sender, UserWithSaturn5DamageReportEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataCanceled(sender, e);
            }

            // Occurs whenever attempt to commit damage data upload has been completed successfully
            private void OnUploadingSaturn5ReportingDamageDataSucceed(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                // Displays appropriate logs informing user about valid serial number being provided.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataSucceed(sender, e);

                // Disables saturn 5 serial number input text box
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ReportingDamageDataSucceed(sender, e);

                // Proceed withing UITask
                this.OnDataUploadRequired(sender, e);
            }
            
            // Occurs whenever UITask is ready/require data upload to proceed.
            private void OnDataUploadRequired(object sender, UserWithDamagedSaturn5EventArgs e)
            {                
                // Displays appropriate logs informing user that application began uploading the saturn 5 confirmation data.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_UploadingSaturn5ConfirmationDataBegan(sender, e);
                
                // Attempt to confirm saturn 5 unit in depot by serial number, ...
                Task confirmBackInSaturn5BySerialNumberTask = this._deBriefServices.ConfirmInDepotSaturn5BySerialNumberAsync(e.Saturn5.SerialNumber, _throwOnLastSeenWithStarUser);
                // ... once finished...
                confirmBackInSaturn5BySerialNumberTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        // ... if confirmed saturn 5 unit back in depot successfully...
                        case TaskStatus.RanToCompletion:
                            // ...OnSucceed.
                            this.OnSucceed(sender, e);
                            break;

                        // ... if failed to confirmed saturn 5 unit back in depot ...
                        case TaskStatus.Faulted:
                            // Get flatten collection of exceptions which caused task to fail.
                            IList<Exception> connectTaskExceptions = t.Exception.Flatten().InnerExceptions;

                            // If task failed because saturn5 attempted to be confirm back in depot is currently allocated to star user..
                            if (connectTaskExceptions.Any((ex) => { return (ex.GetType() == typeof(AttemptToConfirmStarUserSaturn5Exception)); }))
                                // execute appropriate method to give user a choice to proceeding or canceling the UITAsk.
                                this.OnAttemptToConfirmStarUserSaturn5(sender, e);
                            // .. otherwise if failed to obtain saturn 5 data for any other reason.
                            else
                                this.OnFailed(sender, e);
                            break;
                        // .. if canceled attempt to confirmed saturn 5 unit back in depot ...
                        case TaskStatus.Canceled:
                            // .. OnCanceled
                            this.OnCanceled(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            private void OnAttemptToConfirmStarUserSaturn5(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                // Ask user what to do 
                DialogResult result = MessageBox.Show($"You are attempting to confirm back Saturn5 unit currently allocated to one of the STAR USERs: {e.User.Username} - {e.User.FirstName}  {e.User.Surname}. Are you sure you want to do that?", "Attempt to confirm Star User allocated Saturn5.",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                // If user pressed 'Yes' button, reattempt to confirm back in saturn 5 unit
                {
                    this._throwOnLastSeenWithStarUser = false;
                    this.OnDataUploadRequired(sender, e);
                }
                // .. otherwise ..
                else
                    // .. cancel attempt confirm back in saturn 5 unit
                    this.OnCanceled(sender, e);
            }

            // Occurs whenever saturn 5 confirmation completed successfully.
            private void OnSucceed(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                // Informs user about success of the saturn 5 confirmation
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_Succeed(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_Succeed(sender, e);
            }

            // Occurs whenever saturn 5 confirmation failed.
            private void OnFailed(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                // Print operation failure logs.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_Failed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Failed to confirm back in damage Saturn 5 unit. Would You like to retry? Or would you like to cancel and close the application.", "Failed to damage confirm back in Saturn 5 unit.", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    // If user pressed 'Retry' button, reattempt to connect with database. 
                    this.OnDataUploadRequired(sender, e);
                    return;
                }

                // Otherwise close the application. 
                this._form.Close();
            }

            // Occurs whenever saturn 5 confirmation process has been canceled.
            private void OnCanceled(object sender, UserWithDamagedSaturn5EventArgs e)
            {
                // Print appropriate logs.
                this._consolesServices.OnConfirmBackInDamageSaturn5BySerialNumber_Canceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Enable appropriate controls.
                this._controlsEnabler.OnConfirmBackInDamageSaturn5BySerialNumber_Canceled(sender, e);
            }
            #endregion

            #region Subscribe/Unsubscribe events
            private void StartListenForSerialNumberInput()
            {
                if (this._form.InvokeRequired)
                {
                    Action d = new Action(StartListenForSerialNumberInput);
                    if (!_form.IsDisposed)
                        _form.Invoke(d);
                }
                else if (!this.tbxDeBriefSaturn5SerialNumber.IsDisposed)
                {
                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    this.AwaitingSerialNumberCanceled += this.OnAwaitingSerialNumberCanceled;

                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    //this.ValidatingSerialNumberCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxDeBriefSaturn5SerialNumber.InputValidationBegan -= this.OnProvidedSerialNumberValidationBegan;
                    this.tbxDeBriefSaturn5SerialNumber.InputValidationBegan += this.OnProvidedSerialNumberValidationBegan;

                    this.tbxDeBriefSaturn5SerialNumber.InputValidationFailed -= this.OnProvidedSerialNumberValidationFailed;
                    this.tbxDeBriefSaturn5SerialNumber.InputValidationFailed += this.OnProvidedSerialNumberValidationFailed;

                    this.tbxDeBriefSaturn5SerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    //this.tbxDeBriefSaturn5SerialNumber.InputValidationCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxDeBriefSaturn5SerialNumber.ValidInputValueProvided -= this.OnValidSerialNumberProvided;
                    this.tbxDeBriefSaturn5SerialNumber.ValidInputValueProvided += this.OnValidSerialNumberProvided;

                    this.tbxDeBriefSaturn5SerialNumber.EmptyInputValueProvided -= this.OnEmptySerialNumberProvided;
                    this.tbxDeBriefSaturn5SerialNumber.EmptyInputValueProvided += this.OnEmptySerialNumberProvided;

                    this.tbxDeBriefSaturn5SerialNumber.InvalidInputValueProvided -= this.OnInvalidSerialNumberProvided;
                    this.tbxDeBriefSaturn5SerialNumber.InvalidInputValueProvided += this.OnInvalidSerialNumberProvided;
                }
            }

            private void ChangeCancelationMethodForSerialNumberInput(Saturn5SerialNumberEventArgs e)
            {
                if (this.tbxDeBriefSaturn5SerialNumber.InvokeRequired)
                {
                    Action<Saturn5SerialNumberEventArgs> d = new Action<Saturn5SerialNumberEventArgs>(ChangeCancelationMethodForSerialNumberInput);
                    if (!this.tbxDeBriefSaturn5SerialNumber.IsDisposed)
                        this.tbxDeBriefSaturn5SerialNumber.Invoke(d, e);
                }
                else if (!this.tbxDeBriefSaturn5SerialNumber.IsDisposed)
                {
                    this._serialNumberValidationCancelationArgs = e;

                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    //this.AwaitingSerialNumberCanceled += this.OnAwaitingSerialNumberCanceled;

                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.ValidatingSerialNumberCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxDeBriefSaturn5SerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxDeBriefSaturn5SerialNumber.InputValidationCanceled += this.OnProvidedSerialNumberValidationCanceled;
                }
            }

            private void StopListenForSerialNumberInput()
            {
                if (this._form.InvokeRequired)
                {
                    Action d = new Action(StopListenForSerialNumberInput);
                    if (!_form.IsDisposed)
                        _form.Invoke(d);
                }
                else if (!this.tbxDeBriefSaturn5SerialNumber.IsDisposed)
                {
                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxDeBriefSaturn5SerialNumber.InputValidationBegan -= this.OnProvidedSerialNumberValidationBegan;
                    this.tbxDeBriefSaturn5SerialNumber.InputValidationFailed -= this.OnProvidedSerialNumberValidationFailed;
                    this.tbxDeBriefSaturn5SerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxDeBriefSaturn5SerialNumber.ValidInputValueProvided -= this.OnValidSerialNumberProvided;
                    this.tbxDeBriefSaturn5SerialNumber.EmptyInputValueProvided -= this.OnEmptySerialNumberProvided;
                    this.tbxDeBriefSaturn5SerialNumber.InvalidInputValueProvided -= this.OnInvalidSerialNumberProvided;

                    this._serialNumberValidationCancelationArgs = null;
                }
            }
            #endregion
            #endregion
        }
    }
}
