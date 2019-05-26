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
        public class OptionsTabServices
        {
            #region Private Fields
            private MainForm _form;
            private ValidationFunctionsSource _validationFunctionsSource { get { return this._form._validationFunctionsSource; } }
            private InputProvidedEArgsFunctionsSource _inputProvidedEArgsFunctionsSource { get { return this._form._inputProvidedEArgsFunctionsSource; } }

            private UserUsernameValidatingTextBox tbxOptionsUserUsername { get { return this._form.tbxOptionsUserUsername; } }
            private TextBox tbxOptionsUserType { get { return this._form.tbxOptionsUserType; } }
            private TextBox tbxOptionsUserFirstName { get { return this._form.tbxOptionsUserFirstName; } }
            private TextBox tbxOptionsUserSurname { get { return this._form.tbxOptionsUserSurname; } }

            private TextBox tbxOptionsSaturn5SerialNumber { get { return this._form.tbxOptionsSaturn5SerialNumber; } }
            private TextBox tbxOptionsSaturn5Barcode { get { return this._form.tbxOptionsSaturn5Barcode; } }

            private RichTextBox rtbOptionsInfo { get { return this._form.rtbOptionsInfo; } }
            #endregion

            #region Constructor
            public OptionsTabServices(MainForm form)
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
                    this.tbxOptionsUserUsername.ValidationFunc = this._validationFunctionsSource.GetUsernameValidationFunc();
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
                    this.tbxOptionsUserUsername.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetUserUsernameEventArgsCreationFunc();
                }
            }
            #endregion
        }
    }
}
