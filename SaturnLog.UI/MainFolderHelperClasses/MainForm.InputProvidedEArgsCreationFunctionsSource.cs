using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
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
        public class InputProvidedEArgsFunctionsSource
        {
            #region Private Fields
            private MainForm _form;
            private App _app;

            private TextBox tbxPreBriefUserUsername { get { return this._form.tbxPreBriefUserUsername; } }
            private TextBox tbxDeBriefUserUsername { get { return this._form.tbxDeBriefUserUsername; } }
            private TextBox tbxOptionsUserUsername { get { return this._form.tbxOptionsUserUsername; } }
            #endregion

            #region Constructor
            public InputProvidedEArgsFunctionsSource(MainForm form)
            {
                this._form = form;
                this._app = this._form._app;
            }
            #endregion

            #region Methods - Creations Functions
            public Func<string, UserUsernameEventArgs> GetUserUsernameEventArgsCreationFunc()
            {
                return (username) => { return new UserUsernameEventArgs(username); };
            }

            public Func<string, Saturn5SerialNumberEventArgs> GetSaturn5SerialNumberEventArgsCreationFunc()
            {
                return (serialNumber) => { return new Saturn5SerialNumberEventArgs(serialNumber); };
            }

            public Func<string, Saturn5ShortIdEventArgs> GetSaturn5ShortIdEventArgsCreationFunc()
            {
                return (shortId) => { return new Saturn5ShortIdEventArgs(shortId); };
            }

            public Func<User, string, UserWithSaturn5SerialNumberEventArgs> GetUserWithSaturn5SerialNumberEventArgsCreationFunc()
            {
                return (user, serialNumber) => { return new UserWithSaturn5SerialNumberEventArgs(user, serialNumber); };
            }

            public Func<User, string, UserWithSaturn5ShortIdEventArgs> GetUserWithSaturn5ShortIdEventArgsCreationFunc()
            {
                return (user, shortId) => { return new UserWithSaturn5ShortIdEventArgs(user, shortId); };
            }
            #endregion

            #region Methods - Other Creations Functions Parameters Retrieval Functions
            public Func<User> GetUserFromUsernameTextBoxFunc(TextBox usernameSourceTextBox)
            {
                return new Func<User>(() =>
                {
                    string username = usernameSourceTextBox.Text;
                    return this._app.UserServices.Get(username);
                });
            }
            #endregion
        }
    }
}
