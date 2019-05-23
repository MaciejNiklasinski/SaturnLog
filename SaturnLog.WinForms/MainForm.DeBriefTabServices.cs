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
        public class DeBriefTabServices
        {
            #region Private Fields
            private MainForm _form;
            private ValidationFunctionsSource _validationFunctionsSource { get { return this._form._validationFunctionsSource; } }
            private InputProvidedEArgsFunctionsSource _inputProvidedEArgsFunctionsSource { get { return this._form._inputProvidedEArgsFunctionsSource; } }

            private TextBox tbxDeBriefUserUsername { get { return this._form.tbxDeBriefUserUsername; } }
            private TextBox tbxDeBriefUserType { get { return this._form.tbxDeBriefUserType; } }
            private TextBox tbxDeBriefUserFirstName { get { return this._form.tbxDeBriefUserFirstName; } }
            private TextBox tbxDeBriefUserSurname { get { return this._form.tbxDeBriefUserSurname; } }

            private Saturn5SerialNumberValidatingTextBox tbxDeBriefSaturn5SerialNumber { get { return this._form.tbxDeBriefSaturn5SerialNumber; } }
            private Saturn5ShortIdValidatingTextBox tbxDeBriefSaturn5Barcode { get { return this._form.tbxDeBriefSaturn5Barcode; } }

            private RichTextBox rtbDeBriefInfo { get { return this._form.rtbDeBriefInfo; } }
            #endregion

            #region Constructor
            public DeBriefTabServices(MainForm form)
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
                    //this.tbxDeBriefUserUsername.ValidationFunc = this._validationFunctionsSource.GetUsernameValidationFunc();

                    this.tbxDeBriefSaturn5SerialNumber.ValidationFunc = this._validationFunctionsSource.GetSerialNumberValidationFunc();

                    this.tbxDeBriefSaturn5Barcode.ValidationFunc = this._validationFunctionsSource.GetShortIdValidationFunc();
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
                    //this.tbxDeBriefUserUsername.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetUserUsernameEventArgsCreationFunc();

                    this.tbxDeBriefSaturn5SerialNumber.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetSaturn5SerialNumberEventArgsCreationFunc();
                    //this.tbxDeBriefSaturn5SerialNumber.GetOtherEArgsCreationParamFunc = this._inputProvidedEArgsFunctionsSource.GetUserFromUsernameTextBoxFunc(this.tbxDeBriefUserUsername);

                    this.tbxDeBriefSaturn5Barcode.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetSaturn5ShortIdEventArgsCreationFunc();
                    //this.tbxDeBriefSaturn5Barcode.GetOtherEArgsCreationParamFunc = this._inputProvidedEArgsFunctionsSource.GetUserFromUsernameTextBoxFunc(this.tbxDeBriefUserUsername);
                }
            }
            #endregion
        }
    }


}
