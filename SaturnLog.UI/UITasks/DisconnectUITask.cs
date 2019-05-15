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
        // Disconnects application into the database and downloads initial data set from it.
        public class DisconnectUITask : IUITask<EventArgs>
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
            #endregion

            #region Constructor
            // Default Constructor
            public DisconnectUITask(MainForm form)
            { 
                this._form = form;
            }
            #endregion

            #region Methods
            // Triggers database connect and initial data fetch process.
            public void Trigger(object sender, EventArgs e)
            {
                this.OnRequested(sender, e);
            }
            
            // Cancels database connect and/or initial data fetch process.
            public void Cancel(object sender, EventArgs e)
            {

            }
            #endregion

            #region Private Helpers
            #region Disonnect Operations EventHandlers - Operation Logic
            // Occurs whenever user pressed disconnect button.
            private void OnRequested(object sender, System.EventArgs e)
            {
                // Disables connect button.
                this._controlsEnabler.OnDisconnect_Requested(sender, e);

                // Logs awaiting for database to become fully operational.
                this._consolesServices.OnDisconnect_Requested(sender, e);

                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Attempt to disconnect from database...
                Task disconnectTask = this._app.DisconnectAsync();
                disconnectTask.ContinueWith((t) =>
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // If successfully disconnected from database. 
                            this.OnSuccessfullyDisconnected(sender, e);
                            break;
                        default:
                            // If fail to disconnected with database.
                            this.OnFailedToDisconnnect(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }

            // Occurs whenever database has been successfully disconnected.
            private void OnSuccessfullyDisconnected(object sender, System.EventArgs e)
            {
                // Enable control appropriate when application is disconnected from database, and user is logged out.
                this._controlsEnabler.OnDisconnect_Succeed(sender, e);

                // Set all the consoles according is disconnected.
                this._consolesServices.OnDisconnect_Succeed(sender, e);
            }

            // Occurs whenever database fail to disconnect and become operational.
            private void OnFailedToDisconnnect(object sender, System.EventArgs e)
            {
                this._consolesServices.OnDisconnect_Failed(sender, e);

                // Ask user what to do in case of failure
                DialogResult result = MessageBox.Show($"Failed to disconnect from database.", "Failed to disconnect with database.", MessageBoxButtons.OK);
                
                // Otherwise close the application. 
                this._form.Close();
            }
            #endregion
            #endregion
        }
    }
}
