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
        // Removes saturn 5 unit from the depot stock.
        public class RemoveSaturn5UITask : IUITask<EventArgs>
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

            // Helper class responsible for printing to 'Consoles' text boxes.
            private ConsolesServices _consolesServices { get { return this._form._consolesServices; } }

            // Helper class responsible for displaying specific user/saturn5 etc related data.
            private DataDisplayServices _dataDisplayServices { get { return this._form._dataDisplayServices; } }

            // Helper class responsible for enabling and disabling tabs/buttons/text boxes
            private ControlsEnabler _controlsEnabler { get { return this._form._controlsEnabler; } }

            #region Text Boxes
            // Saturn5 Stock Management Tab Serial Number text box
            private Saturn5SerialNumberValidatingTextBox tbxSaturn5SMSerialNumber { get { return this._form.tbxSaturn5SMSerialNumber; } }
            #endregion
            #endregion

            #region Constructor
            // Default constructor. 
            public RemoveSaturn5UITask(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public void Trigger(object sender, EventArgs e)
            {
                this.OnAwaitingSerialNumber(sender, e);
            }
            
            public void Cancel(object sender, EventArgs e)
            {
                this.AwaitingSerialNumberCanceled?.Invoke(sender, e);
                this.ValidatingSerialNumberCanceled?.Invoke(sender, this._serialNumberValidationCancelationArgs ?? throw new InvalidOperationException());
            }
            #endregion

            #region Private Helpers
            #region RemoveSaturn5 Operations EventHandlers - Operation Logic
            // Occurs whenever user pressed the button and UI task is expecting valid serial number to be provided into the appropriate text box.
            private void OnAwaitingSerialNumber(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnRemoveSaturn5_AwaitingSerialNumber(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Start listening for valid serial number.
                this.StartListenForSerialNumberInput();

                // Enables saturn 5 serial number input text box
                this._controlsEnabler.OnRemoveSaturn5_AwaitingSerialNumber(sender, e);
            }

            private void OnAwaitingSerialNumberCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForSerialNumberInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveSaturn5_AwaitingSerialNumberCanceled(sender, e);

                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnRemoveSaturn5_AwaitingSerialNumberCanceled(sender, e);
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
                this._consolesServices.OnRemoveSaturn5_ProvidedSerialNumberValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnRemoveSaturn5_ProvidedSerialNumberValidationFailed(sender, e);

                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForSerialNumberInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveSaturn5_ProvidedSerialNumberValidationCanceled(sender, e);

                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnRemoveSaturn5_ProvidedSerialNumberValidationCanceled(sender, e);
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
                this._consolesServices.OnRemoveSaturn5_EmptySerialNumberProvided(sender, e);
            }

            // Occurs whenever user will provide invalid serial number.
            private void OnInvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about invalid serial number being provided.
                this._consolesServices.OnRemoveSaturn5_InvalidSerialNumberProvided(sender, e);
            }

            // Occurs whenever user provided valid serialNumber into the appropriate text box
            private void OnValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Stop listening for saturn 5 serial number
                this.StopListenForSerialNumberInput();

                // Displays appropriate logs informing user about valid serial number being provided.
                this._consolesServices.OnRemoveSaturn5_ValidSerialNumberProvided(sender, e);

                // Disables saturn 5 serial number input text box
                this._controlsEnabler.OnRemoveSaturn5_ValidSerialNumberProvided(sender, e);

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
                this._controlsEnabler.OnRemoveSaturn5_CancelToRetrieveSaturn5Data(sender, e);
            }

            // Occurs whenever saturn 5 data got successfully retrieved.
            private void OnSuccessfullyRetrievedSaturn5Data(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application completed obtaining saturn 5 data.
                this._consolesServices.OnRetrievingSaturn5DataCompleted(sender, new Saturn5EventArgs(e.Saturn5));

                // Set text boxes according to the current state of the UITask.
                this._dataDisplayServices.OnRetrievingSaturn5DataCompleted(sender, e);

                // Trigger further part of UITask
                this.OnAwaitingReport(sender, e);
            }

            // Occurs whenever saturn 5 data has been successfully retrieved and saturn 5 removal report is awaited
            private void OnAwaitingReport(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnRemoveSaturn5_AwaitingReport(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveSaturn5_AwaitingReport(sender, e);

                // Creates and opens Remove Saturn 5 report creation form
                RemoveSaturn5ReportCreationForm removeSaturn5ReportCreationForm = new RemoveSaturn5ReportCreationForm(this._app, e.Saturn5);
                removeSaturn5ReportCreationForm.FormClosed += ((s, args) =>
                {
                    // If RemoveSaturn5ReportCreationForm has been closed by user pressing save button...
                    if (removeSaturn5ReportCreationForm.Commit)
                        // .. attempt to commit RemoveSaturn5 data.
                        this.OnReportDataUploadRequired(sender, e);
                    // otherwise ..
                    else
                        // Cancel UI task.
                        this.OnAwaitingReportCanceled(sender, e);
                });

                removeSaturn5ReportCreationForm.ShowDialog(this._form);
            }

            // Occurs whenever user will press cancel button rather then save to close RemoveSaturn5ReportCreationForm
            private void OnAwaitingReportCanceled(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled current task.
                this._consolesServices.OnRemoveSaturn5_AwaitingReportCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveSaturn5_AwaitingReportCanceled(sender, e);
            }

            // Occurs whenever saturn 5 sent to it report has been provided and required to be committed.
            private void OnReportDataUploadRequired(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnRemoveSaturn5_UploadingReportDataBegan(sender, e);

                // Attempt to commit saturn 5 remove data
                Task removeSaturn5Task = this._saturn5Services.RemoveAsync(e.Saturn5.SerialNumber);
                removeSaturn5Task.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if saturn 5 receive from IT has been committed successfully
                            this.OnReportDataUploadSucceed(sender, e);
                            break;
                        case TaskStatus.Faulted:
                            // ... get flatten collection of exception responsible for the task failure.
                            IReadOnlyCollection<Exception> innerExceptions = t.Exception.Flatten().InnerExceptions;

                            // If task failed because saturn 5 unit which removal has been attempted is the last unit in the depot stock...
                            if (innerExceptions.Any(ex => ex.GetType() == typeof(AttemptToRemoveLastSaturn5Exception)))
                                // .. AttemptToRemoveLastSaturn5Exception
                                this.OnAttemptToRemoveLastSaturn5(sender, e);
                            // Otherwise if task failed because of some other reason...
                            else
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
            private void OnReportDataUploadSucceed(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application successfully committed the data.
                this._consolesServices.OnRemoveSaturn5_ReportDataUploadSucceed(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveSaturn5_ReportDataUploadSucceed(sender, e);
            }
            
            private void OnAttemptToRemoveLastSaturn5(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user about failed data upload.
                this._consolesServices.OnRemoveSaturn5_AttemptToRemoveLastSaturn5(sender, e);

                // Informs user about application failed to send saturn 5 to IT and as such being unable to continue.
                MessageBox.Show("Unable to remove last Saturn 5 unit from the depot stock.", "Unable to remove Saturn 5.", MessageBoxButtons.OK);

                // Cancel saturn 5 removal operation.
                this.OnReportDataUploadCanceled(sender, e);
            }

            // Occurs whenever saturn 5 remove data failed to get uploaded.
            private void OnReportDataUploadFailed(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user about failed data upload.
                this._consolesServices.OnRemoveSaturn5_ReportDataUploadFailed(sender, e);

                // Informs user about application failed to send saturn 5 to IT and as such being unable to continue.
                MessageBox.Show("Application failed to remove saturn 5 unit from the depot stock and must be closed.", "Saturn 5 remove failed.", MessageBoxButtons.OK);

                // Close the application.
                this._form.Close();
            }

            // Occurs whenever saturn 5 remove it data canceled to get uploaded.
            private void OnReportDataUploadCanceled(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled data upload
                this._consolesServices.OnRemoveSaturn5_ReportDataUploadCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnRemoveSaturn5_ReportDataUploadCanceled(sender, e);
            }
            #endregion

            #region Subscribe/Unsubscribe events
            private void StartListenForSerialNumberInput()
            {
                if (this.tbxSaturn5SMSerialNumber.InvokeRequired)
                {
                    Action d = new Action(StartListenForSerialNumberInput);
                    if (!tbxSaturn5SMSerialNumber.IsDisposed)
                        tbxSaturn5SMSerialNumber.Invoke(d);
                }
                else if (!this.tbxSaturn5SMSerialNumber.IsDisposed)
                {
                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    this.AwaitingSerialNumberCanceled += this.OnAwaitingSerialNumberCanceled;

                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    //this.ValidatingSerialNumberCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxSaturn5SMSerialNumber.InputValidationBegan -= this.OnProvidedSerialNumberValidationBegan;
                    this.tbxSaturn5SMSerialNumber.InputValidationBegan += this.OnProvidedSerialNumberValidationBegan;

                    this.tbxSaturn5SMSerialNumber.InputValidationFailed -= this.OnProvidedSerialNumberValidationFailed;
                    this.tbxSaturn5SMSerialNumber.InputValidationFailed += this.OnProvidedSerialNumberValidationFailed;

                    this.tbxSaturn5SMSerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    //this.tbxSaturn5SMSerialNumber.InputValidationCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxSaturn5SMSerialNumber.ValidInputValueProvided -= this.OnValidSerialNumberProvided;
                    this.tbxSaturn5SMSerialNumber.ValidInputValueProvided += this.OnValidSerialNumberProvided;

                    this.tbxSaturn5SMSerialNumber.EmptyInputValueProvided -= this.OnEmptySerialNumberProvided;
                    this.tbxSaturn5SMSerialNumber.EmptyInputValueProvided += this.OnEmptySerialNumberProvided;

                    this.tbxSaturn5SMSerialNumber.InvalidInputValueProvided -= this.OnInvalidSerialNumberProvided;
                    this.tbxSaturn5SMSerialNumber.InvalidInputValueProvided += this.OnInvalidSerialNumberProvided;
                }
            }

            private void ChangeCancelationMethodForSerialNumberInput(Saturn5SerialNumberEventArgs e)
            {
                if (this.tbxSaturn5SMSerialNumber.InvokeRequired)
                {
                    Action<Saturn5SerialNumberEventArgs> d = new Action<Saturn5SerialNumberEventArgs>(ChangeCancelationMethodForSerialNumberInput);
                    if (!this.tbxSaturn5SMSerialNumber.IsDisposed)
                        this.tbxSaturn5SMSerialNumber.Invoke(d, e);
                }
                else if (!this.tbxSaturn5SMSerialNumber.IsDisposed)
                {
                    this._serialNumberValidationCancelationArgs = e;

                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    //this.AwaitingSerialNumberCanceled += this.OnAwaitingSerialNumberCanceled;

                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.ValidatingSerialNumberCanceled += this.OnProvidedSerialNumberValidationCanceled;

                    this.tbxSaturn5SMSerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxSaturn5SMSerialNumber.InputValidationCanceled += this.OnProvidedSerialNumberValidationCanceled;
                }
            }

            private void StopListenForSerialNumberInput()
            {
                if (this.tbxSaturn5SMSerialNumber.InvokeRequired)
                {
                    Action d = new Action(StopListenForSerialNumberInput);
                    if (!tbxSaturn5SMSerialNumber.IsDisposed)
                        tbxSaturn5SMSerialNumber.Invoke(d);
                }
                else if (!this.tbxSaturn5SMSerialNumber.IsDisposed)
                {
                    this.AwaitingSerialNumberCanceled -= this.OnAwaitingSerialNumberCanceled;
                    this.ValidatingSerialNumberCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxSaturn5SMSerialNumber.InputValidationBegan -= this.OnProvidedSerialNumberValidationBegan;
                    this.tbxSaturn5SMSerialNumber.InputValidationFailed -= this.OnProvidedSerialNumberValidationFailed;
                    this.tbxSaturn5SMSerialNumber.InputValidationCanceled -= this.OnProvidedSerialNumberValidationCanceled;
                    this.tbxSaturn5SMSerialNumber.ValidInputValueProvided -= this.OnValidSerialNumberProvided;
                    this.tbxSaturn5SMSerialNumber.EmptyInputValueProvided -= this.OnEmptySerialNumberProvided;
                    this.tbxSaturn5SMSerialNumber.InvalidInputValueProvided -= this.OnInvalidSerialNumberProvided;

                    this._serialNumberValidationCancelationArgs = null;
                }
            }
            #endregion
            #endregion
        }
    }
}
