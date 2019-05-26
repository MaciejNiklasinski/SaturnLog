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
        // Logs user out.
        public class LogOutUITask : IUITask<EventArgs>
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
            // Default constructor. 
            public LogOutUITask(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public void Trigger(object sender, EventArgs e)
            {
                this.OnRequested(sender, e);
            }
            
            public void Cancel(object sender, EventArgs e)
            {

            }
            #endregion

            #region Private Helpers
            #region LogOut Operations EventHandlers - Operation Logic
            // Occurs whenever user will press log out button.
            private void OnRequested(object sender, System.EventArgs e)
            {
                // Clears the content of all of the main form text boxes displaying User/Satur5 etc. data.
                this._dataDisplayServices.ClearAllDataDisplayTextBoxes(sender, e);

                // Clears info boxes
                this._dataDisplayServices.ClearInfoBoxes(sender, e);

                // Attempt to log out...
                Task logOutTask = this._app.LogOutAsync();
                logOutTask.ContinueWith((t) => 
                {
                    switch (t.Status)
                    {
                        case TaskStatus.RanToCompletion:
                            // ... execute if logged out successfully.
                            this.OnSuccessfully(sender, e);
                            break;
                        case TaskStatus.Faulted:
                            // ... execute if failed to logged out.
                            this.OnFailed(sender, e);
                            break;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            
            // Occurs whenever application will successfully log out a user.
            private void OnSuccessfully(object sender, System.EventArgs e)
            {
                // Informs user about successful log out.
                this._consolesServices.OnLogOut_Successfully(sender, e);

                // Enables/Disables appropriate controls.
                this._controlsEnabler.OnLogOut_Successfully(sender, e);
            }
            
            // Occurs whenever application will fail to log out a user.
            private void OnFailed(object sender, System.EventArgs e)
            {
                // Informs user about application failed to log out and as such being unable to continue.
                MessageBox.Show("Application failed to log out and must be closed.", "Failed to log out.", MessageBoxButtons.OK);

                // Close the application.
                this._form.Close();
            }
            #endregion
            #endregion
        }
    }
}
