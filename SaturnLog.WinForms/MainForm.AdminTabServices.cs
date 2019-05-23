using SaturnLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI
{
    // De-Brief tab services
    public partial class MainForm
    {
        public class AdminTabServices
        {
            #region Private Fields
            private MainForm _form;
            private ValidationFunctionsSource _validationFunctionsSource { get { return this._form._validationFunctionsSource; } }
            private InputProvidedEArgsFunctionsSource _inputProvidedEArgsFunctionsSource { get { return this._form._inputProvidedEArgsFunctionsSource; } }

            private UserUsernameValidatingTextBox tbxAdminUserUsername { get { return this._form.tbxAdminUserUsername; } }
            private TextBox tbxAdminUserType { get { return this._form.tbxAdminUserType; } }
            private TextBox tbxAdminUserFirstName { get { return this._form.tbxAdminUserFirstName; } }
            private TextBox tbxAdminUserSurname { get { return this._form.tbxAdminUserSurname; } }

            private RichTextBox rtbAdminInfo { get { return this._form.rtbAdminInfo; } }
            #endregion

            #region Constructor
            public AdminTabServices(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public void AssignTextBoxesValidationFunctions()
            {
                if (this._form.InvokeRequired)
                {
                    Action d = new Action(AssignTextBoxesValidationFunctions);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d);
                }
                else if (!this._form.IsDisposed)
                {
                    this.tbxAdminUserUsername.ValidationFunc = this._validationFunctionsSource.GetUsernameValidationFunc();
                }
            }

            public void AssignTextBoxesInputProvidedEArgsCreationFunctions()
            {
                if (this._form.InvokeRequired)
                {
                    Action d = new Action(AssignTextBoxesInputProvidedEArgsCreationFunctions);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d);
                }
                else if (!this._form.IsDisposed)
                {
                    this.tbxAdminUserUsername.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetUserUsernameEventArgsCreationFunc();
                }
            }
            #endregion
        }
    }
}
