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
    // Pre-Brief tab services
    public partial class MainForm
    {
        public class PreBriefTabServices
        {
            #region Private Fields
            private MainForm _form;
            private ValidationFunctionsSource _validationFunctionsSource { get { return this._form._validationFunctionsSource; } }
            private InputProvidedEArgsFunctionsSource _inputProvidedEArgsFunctionsSource { get { return this._form._inputProvidedEArgsFunctionsSource; } }

            private UserUsernameValidatingTextBox tbxPreBriefUserUsername { get { return this._form.tbxPreBriefUserUsername; } }
            private TextBox tbxPreBriefUserType { get { return this._form.tbxPreBriefUserType; } }
            private TextBox tbxPreBriefUserFirstName { get { return this._form.tbxPreBriefUserFirstName; } }
            private TextBox tbxPreBriefUserSurname { get { return this._form.tbxPreBriefUserSurname; } }
    
            private UserWithSaturn5SerialNumberValidatingTextBox tbxPreBriefSaturn5SerialNumber { get { return this._form.tbxPreBriefSaturn5SerialNumber; } }
            private UserWithSaturn5ShortIdValidatingTextBox tbxPreBriefSaturn5Barcode { get { return this._form.tbxPreBriefSaturn5Barcode; } }
    
            private RichTextBox rtbPreBriefInfo { get { return this._form.rtbPreBriefInfo; } }
            #endregion

            #region Constructor
            public PreBriefTabServices(MainForm form)
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
                    this.tbxPreBriefUserUsername.ValidationFunc = this._validationFunctionsSource.GetUsernameValidationFunc();

                    this.tbxPreBriefSaturn5SerialNumber.ValidationFunc = this._validationFunctionsSource.GetSerialNumberValidationFunc();

                    this.tbxPreBriefSaturn5Barcode.ValidationFunc = this._validationFunctionsSource.GetShortIdValidationFunc();
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
                    this.tbxPreBriefUserUsername.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetUserUsernameEventArgsCreationFunc();

                    this.tbxPreBriefSaturn5SerialNumber.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetUserWithSaturn5SerialNumberEventArgsCreationFunc();
                    this.tbxPreBriefSaturn5SerialNumber.GetOtherEArgsCreationParamFunc = this._inputProvidedEArgsFunctionsSource.GetUserFromUsernameTextBoxFunc(this.tbxPreBriefUserUsername);

                    this.tbxPreBriefSaturn5Barcode.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetUserWithSaturn5ShortIdEventArgsCreationFunc();
                    this.tbxPreBriefSaturn5Barcode.GetOtherEArgsCreationParamFunc = this._inputProvidedEArgsFunctionsSource.GetUserFromUsernameTextBoxFunc(this.tbxPreBriefUserUsername);
                }
            }
            #endregion
        }
    }
}
