﻿using SaturnLog.Core;
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
        // Resolves Saturn 5 fault or damage in the depot.
        public class ResolveSaturn5IssueUITask : IUITask<EventArgs>
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
            public ResolveSaturn5IssueUITask(MainForm form)
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
            #region ResolveSaturn5Issue Operations EventHandlers - Operation Logic
            // Occurs whenever user pressed the button and UI task is expecting valid serial number to be provided into the appropriate text box.
            private void OnAwaitingSerialNumber(object sender, System.EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnResolveSaturn5Issue_AwaitingSerialNumber(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Start listening for valid serial number.
                this.StartListenForSerialNumberInput();

                // Enables saturn 5 serial number input text box
                this._controlsEnabler.OnResolveSaturn5Issue_AwaitingSerialNumber(sender, e);
            }

            private void OnAwaitingSerialNumberCanceled(object sender, System.EventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForSerialNumberInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnResolveSaturn5Issue_AwaitingSerialNumberCanceled(sender, e);

                // Displays appropriate logs informing user that application canceled obtaining user data.
                this._consolesServices.OnResolveSaturn5Issue_AwaitingSerialNumberCanceled(sender, e);
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
                this._consolesServices.OnResolveSaturn5Issue_ProvidedSerialNumberValidationBegan(sender, e);
            }

            // Occurs whenever validation of the value provided by the user failed, and the user didn't agree to retry.
            private void OnProvidedSerialNumberValidationFailed(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnResolveSaturn5Issue_ProvidedSerialNumberValidationFailed(sender, e);

                // Close the application. 
                this._form.Close();
            }

            // Occurs whenever validation of the value provided by the user has been canceled.
            private void OnProvidedSerialNumberValidationCanceled(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Stop listening for serial number text box input.
                this.StopListenForSerialNumberInput();

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnResolveSaturn5Issue_ProvidedSerialNumberValidationCanceled(sender, e);

                // Displays appropriate logs informing user about empty serial number input being provided.
                this._consolesServices.OnResolveSaturn5Issue_ProvidedSerialNumberValidationCanceled(sender, e);
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
                this._consolesServices.OnResolveSaturn5Issue_EmptySerialNumberProvided(sender, e);
            }

            // Occurs whenever user will provide invalid serial number.
            private void OnInvalidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Displays appropriate logs informing user about invalid serial number being provided.
                this._consolesServices.OnResolveSaturn5Issue_InvalidSerialNumberProvided(sender, e);
            }

            // Occurs whenever user provided valid serialNumber into the appropriate text box
            private void OnValidSerialNumberProvided(object sender, Saturn5SerialNumberEventArgs e)
            {
                // Stop listening for saturn 5 serial number
                this.StopListenForSerialNumberInput();

                // Displays appropriate logs informing user about valid serial number being provided.
                this._consolesServices.OnResolveSaturn5Issue_ValidSerialNumberProvided(sender, e);

                // Disables saturn 5 serial number input text box
                this._controlsEnabler.OnResolveSaturn5Issue_ValidSerialNumberProvided(sender, e);

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
                this._controlsEnabler.OnResolveSaturn5Issue_CancelToRetrieveSaturn5Data(sender, e);
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

            // Occurs whenever saturn 5 data of the unit designated for faults or damages to get resolved
            private void OnAwaitingReport(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnResolveSaturn5Issue_AwaitingReport(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnResolveSaturn5Issue_AwaitingReport(sender, e);

                // Creates and opens Saturn 5 resolve issue form
                ResolveSaturn5IssueReportCreationForm resolveSaturn5IssueReportCreationForm = new ResolveSaturn5IssueReportCreationForm(this._app, e.Saturn5);
                resolveSaturn5IssueReportCreationForm.FormClosed += ((s, args) =>
                {
                    // If ResolveSaturn5IssueReportCreationForm has been closed by user pressing save button...
                    if (resolveSaturn5IssueReportCreationForm.Commit)
                        // .. attempt to commit EditSaturn5 data.
                        this.OnReportDataUploadRequired(sender, new ResolveSaturn5IssueEventArgs(
                            resolveSaturn5IssueReportCreationForm.Issue,
                            resolveSaturn5IssueReportCreationForm.ResolvedBy,
                            resolveSaturn5IssueReportCreationForm.ResolvedHow,
                            resolveSaturn5IssueReportCreationForm.ResolvedHowDescription));
                    // otherwise ..
                    else
                        // Cancel UI task.
                        this.OnAwaitingReportCanceled(sender, e);
                });

                resolveSaturn5IssueReportCreationForm.ShowDialog(this._form);
            }

            // Occurs whenever user will press cancel button rather then save to close ResolveSaturn5IssueReportCreationForm
            private void OnAwaitingReportCanceled(object sender, Saturn5EventArgs e)
            {
                // Displays appropriate logs informing user that application canceled current task.
                this._consolesServices.OnResolveSaturn5Issue_AwaitingReportCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnResolveSaturn5Issue_AwaitingReportCanceled(sender, e);
            }

            // Occurs whenever saturn 5 issue resolved report has been provided and is required to be committed.
            private void OnReportDataUploadRequired(object sender, ResolveSaturn5IssueEventArgs e)
            {
                // Displays appropriate logs/application state/user instructions text to the user.
                this._consolesServices.OnResolveSaturn5Issue_UploadingReportDataBegan(sender, e);

                // Attempt to commit resolving saturn 5 fault or damage
                Task resolveSaturn5Issue = this._saturn5Services.ResolveIssueAsync(e.Issue.SerialNumber, e.Issue.Timestamp, e.ResolvedHow, e.ResolvedHowDescription);
                resolveSaturn5Issue.ContinueWith((t) =>
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

            // Occurs whenever saturn 5 resolve issue data get uploaded successfully
            private void OnReportDataUploadSucceed(object sender, ResolveSaturn5IssueEventArgs e)
            {
                // Displays appropriate logs informing user that application successfully committed the data.
                this._consolesServices.OnResolveSaturn5Issue_ReportDataUploadSucceed(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnResolveSaturn5Issue_ReportDataUploadSucceed(sender, e);
            }
            
            // Occurs whenever saturn 5 resolve issue data failed to get uploaded.
            private void OnReportDataUploadFailed(object sender, ResolveSaturn5IssueEventArgs e)
            {
                // Displays appropriate logs informing user about failed data upload.
                this._consolesServices.OnResolveSaturn5Issue_ReportDataUploadFailed(sender, e);

                // Informs user about application failed to resolve saturn 5 issue and as such is unable to continue.
                MessageBox.Show("Application failed to resolve saturn 5 issue and must be closed.", "Saturn 5 issue resolving failed.", MessageBoxButtons.OK);

                // Close the application.
                this._form.Close();
            }

            // Occurs whenever saturn 5 resolve issue data canceled to get uploaded.
            private void OnReportDataUploadCanceled(object sender, ResolveSaturn5IssueEventArgs e)
            {
                // Displays appropriate logs informing user that application canceled data upload
                this._consolesServices.OnResolveSaturn5Issue_ReportDataUploadCanceled(sender, e);
                this._consolesServices.OnBackToIdle(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnResolveSaturn5Issue_ReportDataUploadCanceled(sender, e);
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
