using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
using SaturnLog.Core.Exceptions;
using SaturnLog.UI.UITasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI
{
    public partial class MainForm
    {
        // Connect application into the database and downloads initial data set from it.
        public class ConnectUITask : IUITask<EventArgs>
        {
            #region Private Fields
            // Main form
            private MainForm _form;

            // Domain layer facade
            private App _app { get { return this._form._app; } }

            // Helper class responsible for printing to 'Consoles' text boxes.
            private ConsolesServices _consolesServices { get { return this._form._consolesServices; } }

            // Helper class responsible for displaying specific user/saturn5 etc related data.
            private DataDisplayServices _dataDisplayServices { get { return this._form._dataDisplayServices; } }

            // Helper class responsible for enabling and disabling tabs/buttons/text boxes
            private ControlsEnabler _controlsEnabler { get { return this._form._controlsEnabler; } }

            private bool _forceDBConnectionTakeover = false;

            private CancellationTokenSource _cancellationTokenSource = null;
            private CancellationTokenSource _watchDatabaseConnectingTokenSource = null;
            #endregion

            #region Constructor
            // Default Constructor
            public ConnectUITask(MainForm form)
            { 
                this._form = form;
            }
            #endregion

            #region Methods
            // Triggers database connect and initial data fetch process.
            public void Trigger(object sender, EventArgs e)
            {
                this._forceDBConnectionTakeover = false;
                this.OnRequested(sender, e);
            }
            
            // Cancels database connect and/or initial data fetch process.
            public void Cancel(object sender, EventArgs e)
            {
                this._watchDatabaseConnectingTokenSource?.Cancel();
                this._cancellationTokenSource.Cancel();
            }
            #endregion

            #region Private Helpers
            #region Connect Operations EventHandlers - Operation Logic
            // Occurs whenever user pressed connect button.
            private void OnRequested(object sender, System.EventArgs e)
            {
                // Disables connect button.
                this._controlsEnabler.OnConnect_Requested(sender, e);

                // Logs awaiting for database to become fully operational.
                this._consolesServices.OnConnect_Requested(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);
                
                //
                this._watchDatabaseConnectingTokenSource = new CancellationTokenSource();
                this._cancellationTokenSource = new CancellationTokenSource();
                this._cancellationTokenSource.Token.Register(() =>
                {
                    this._watchDatabaseConnectingTokenSource?.Cancel();
                });
                
                //
                Task watchDatabaseConnectingTask = Task.Run(async () => 
                {                   
                    // Declare nullable integer variable to compare changes ,
                    int? lastCountdownValue = null;

                    // Loop through till canceled
                    while (true)
                    {
                        // Await 100ms
                        await Task.Delay(100, this._watchDatabaseConnectingTokenSource.Token);

                        // If app is currently connecting into the database..
                        if (this._app.DBConnected == null  
                        // .. and current availability countdown is not equal previously stored value.
                        && this._app.DBConnectionAvailabilityCountdown != lastCountdownValue)
                        {
                            // Store the value of the database availability countdown in a local variable lastCountdownValue for later comparison.
                            lastCountdownValue = this._app.DBConnectionAvailabilityCountdown;
                            
                            // Display appropriate database status.
                            this._consolesServices.OnConnect_DBConnectionAvailabilityCountdown(sender, e);
                        }
                    }

                }, _watchDatabaseConnectingTokenSource.Token);
                                
                // Attempt to connect with database...
                Task<Task> connectTask = this._app.ConnectAsync(this._cancellationTokenSource, this._forceDBConnectionTakeover);
                connectTask.ContinueWith((t) =>
                {
                    // Cancel watching database connection task
                    _watchDatabaseConnectingTokenSource.Cancel();

                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // If successfully connected with database.
                            this.OnOperational(sender, new TaskEventArgs(t.Result));
                            break;

                        case TaskStatus.Faulted:
                            // Get flatten collection of exceptions which caused task to fail.
                            IList<Exception> connectTaskExceptions = t.Exception.Flatten().InnerExceptions;

                            // If task failed because other application instance is currently using the database
                            // or application failed recently without unlocking the database ..
                            if (connectTaskExceptions.Any((ex) => { return (ex.GetType() == typeof(DBInUseByOtherApplicationInstanceException) 
                                || ex.GetType() == typeof(DBIsAttemptingToConnectToOtherApplicationInstanceException)
                                || ex.GetType() == typeof(DBForcedToBeTakenOverException)); }))
                                this.OnDatabaseUnavailable(sender, e);
                            // .. otherwise if fail to connect with database because any other reason.
                            else
                                this.OnFailedToConnect(sender, new ExceptionsEventArgs(connectTaskExceptions));
                            break;

                        case TaskStatus.Canceled:
                            // If cancel to connect with database.
                            this.OnCanceledToConnect(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());


            }

            // Occurs whenever database is unavailable because is in use by other application instance.
            private void OnDatabaseUnavailable(object sender, System.EventArgs e)
            {
                this._consolesServices.OnConnect_Failed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Database is currently in use by somebody else or an application has been closed incorrectly/crashed recently. Would you like to force database connection anyway, and disconnect any other active database connection?", "Database is currently in use.", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                // If user pressed 'Yes' button, reattempt to connect with database. 
                {
                    this._forceDBConnectionTakeover = true;
                    this.OnRequested(sender, e);
                }
                // .. otherwise ..
                else
                    // .. cancel attempt to connect to database
                    this.OnCanceledToConnect(sender, e);
            }

            // Occurs whenever database attempt to connect and become operational has been canceled.
            private void OnCanceledToConnect(object sender, System.EventArgs e)
            {
                // Enable control appropriate when application is disconnected from database, and user is logged out.
                this._controlsEnabler.OnConnect_Canceled(sender, e);
                
                // Set all the consoles according is disconnected.
                this._consolesServices.OnConnect_Canceled(sender, e);
            }
            
            // Occurs whenever database fail to connect and become operational.
            private void OnFailedToConnect(object sender, ExceptionsEventArgs e)
            {
                this._consolesServices.OnConnect_Failed(sender, e);

                foreach (Exception exception in e.Exceptions)
                    MessageBox.Show($"Database connection failed. Exception message:  {exception.Message} Inner exception message: {exception.InnerException?.Message}", "Database connection failed.", MessageBoxButtons.OK);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Failed to connect with database.", "Failed to connect with database.", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    // If user pressed 'Retry' button, reattempt to connect with database. 
                    this.OnRequested(sender, e);
                    return;
                }

                // Otherwise close the application. 
                this._form.Close();
            }

            // Occurs whenever database has been successfully connected - become operational.
            private void OnOperational(object sender, TaskEventArgs e)
            {
                // Enable control appropriate when application is connected with database, and user is logged out.
                this._controlsEnabler.OnConnect_Operational(sender, e);

                // Set all the consoles according database becoming operational.
                this._consolesServices.OnConnect_Operational(sender, e);

                // Continue on any connection fault.
                e.Task.ContinueWith((t) =>
                {
                    // Get flatten collection of exceptions which caused task to fail.
                    IList<Exception> connectTaskExceptions = t.Exception.Flatten().InnerExceptions;
                    

                    // If established connection failed..
                    if (connectTaskExceptions.Any((ex) => { return (ex.GetType() == typeof(DBConnectionFailureException)); }))
                    {
                        DBConnectionFailureException connectionFailureEx = connectTaskExceptions.First((ex) => { return (ex.GetType() == typeof(DBConnectionFailureException)); }) as DBConnectionFailureException;
                        this.OnConnectionFailed(sender, new ExceptionEventArgs<DBConnectionFailureException>(connectionFailureEx));
                    }

                    // If forced to log out and disconnect..
                    else if (this._app.LoggedIn && connectTaskExceptions.Any((ex) => { return (ex.GetType() == typeof(DBForcedToBeTakenOverException)); }))
                    {
                        this.OnForcedToLogOut(sender, e);
                        this.OnForcedToDisconnect(sender, e);
                    }

                    // If forced to disconnect..
                    else if (connectTaskExceptions.Any((ex) => { return (ex.GetType() == typeof(DBForcedToBeTakenOverException)); }))
                        this.OnForcedToDisconnect(sender, e);

                    // If disconnected willingly..
                    else if (connectTaskExceptions.Any((ex) => { return (ex.GetType() == typeof(DBDisconnectedException)); }))
                        ; // Do Nothing - should not occure anyway // this.OnConnectionFailed(sender, e);

                    // Any other exception...
                    else this.OnConnectionFailed(sender, new ExceptionsEventArgs(connectTaskExceptions));

                }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
                
                // Attempt to fetch all data from database...
                Task fetchDataTask = this._app.FetchDataAsync(new CancellationTokenSource());
                fetchDataTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // If successfully fetch data from database. 
                            this.OnDataFetchCompleted(sender, e);
                            break;
                        case TaskStatus.Faulted:
                            // If failed to fetch data from database. 
                            this.OnDataFetchFailed(sender, e);
                            break;
                        case TaskStatus.Canceled:
                            // If canceled to fetch data from database. 
                            this.OnDataFetchCanceled(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever database attempt to connect and become operational has been canceled.
            private void OnConnectionFailed(object sender, ExceptionEventArgs<DBConnectionFailureException> e)
            {
                this._consolesServices.OnConnect_ConnectionFailed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Database connection failed. Exception message: {e.Exception.Message} Inner exception message: {e.Exception.InnerException?.Message}", "Database connection failed.", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    // If user pressed 'Retry' button, reattempt to connect with database. 
                    this.OnRequested(sender, e);
                    return;
                }

                // Otherwise close the application. 
                this._form.Close();
            }

            // Occurs whenever database attempt to connect and become operational has been canceled.
            private void OnConnectionFailed(object sender, ExceptionsEventArgs e)
            {
                this._consolesServices.OnConnect_ConnectionFailed(sender, e);

                foreach (Exception exception in e.Exceptions)
                    MessageBox.Show($"Database connection failed. Exception message:  {exception.Message} Inner exception message: {exception.InnerException?.Message}", "Database connection failed.", MessageBoxButtons.OK);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Database connection failed.", "Database connection failed.", MessageBoxButtons.RetryCancel);
                if (result == DialogResult.Retry)
                {
                    // If user pressed 'Retry' button, reattempt to connect with database. 
                    this.OnRequested(sender, e);
                    return;
                }

                // Otherwise close the application. 
                this._form.Close();
            }
            
            // Occurs whenever application has been forced to log out.
            private void OnForcedToLogOut(object sender, System.EventArgs e)
            {
                // Enable control appropriate when application is disconnected from database, and user is logged out.
                this._controlsEnabler.OnConnect_ForcedToLogOut(sender, e);

                // Set all the consoles according is disconnected.
                this._consolesServices.OnConnect_ForcedToLogOut(sender, e);
            }
            // Occurs whenever database has been forced to disconnect.
            private void OnForcedToDisconnect(object sender, System.EventArgs e)
            {
                // Enable control appropriate when application is disconnected from database, and user is logged out.
                this._controlsEnabler.OnConnect_ForcedToDisconnect(sender, e);

                // Set all the consoles according is disconnected.
                this._consolesServices.OnConnect_ForcedToDisconnect(sender, e);
            }

            // Occurs whenever application completed initial data fetch from database.
            private void OnDataFetchCompleted(object sender, System.EventArgs e)
            {
                // Informs user about completion of initial database data fetch.
                this._consolesServices.OnConnect_DataFetchCompleted(sender, e);
            }

            // Occurs whenever application failed initial data fetch from database.
            private void OnDataFetchCanceled(object sender, System.EventArgs e)
            {
                // Informs user about failure to complete initial database data fetch.
                this._consolesServices.OnConnect_DataFetchCanceled(sender, e);
                //MessageBox.Show("Application canceled fetching data from the database. Application will stay fully operational, however its performance will be lower.", "Canceled to fetch data from database.", MessageBoxButtons.OK);
            }

            // Occurs whenever application failed initial data fetch from database.
            private void OnDataFetchFailed(object sender, System.EventArgs e)
            {
                // Informs user about failure to complete initial database data fetch.
                this._consolesServices.OnConnect_DataFetchFailed(sender, e);
                MessageBox.Show("Application failed fetching data from the database. It might be cause by temporary lost of Internet and/or database connection. It is advisable to reboot connection with database as soon as possible.", "Failed to fetch data from database.", MessageBoxButtons.OK);
            }
            #endregion
            #endregion
        }
    }
}
