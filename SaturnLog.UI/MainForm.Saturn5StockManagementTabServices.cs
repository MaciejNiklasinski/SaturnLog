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
        public class Saturn5StockManagementTabServices
        {
            #region Private Fields
            private MainForm _form;
            private ValidationFunctionsSource _validationFunctionsSource { get { return this._form._validationFunctionsSource; } }
            private InputProvidedEArgsFunctionsSource _inputProvidedEArgsFunctionsSource { get { return this._form._inputProvidedEArgsFunctionsSource; } }
            
            private Saturn5SerialNumberValidatingTextBox tbxSaturn5SMSaturn5SerialNumber { get { return this._form.tbxSaturn5SMSerialNumber; } }
            private TextBox tbxSaturn5SMSaturn5Barcode { get { return this._form.tbxSaturn5SMBarcode; } }

            private RichTextBox rtbSaturn5SMInfo { get { return this._form.rtbSaturn5SMInfo; } }
            #endregion

            #region Constructor
            public Saturn5StockManagementTabServices(MainForm form)
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
                    this.tbxSaturn5SMSaturn5SerialNumber.ValidationFunc = this._validationFunctionsSource.GetSerialNumberValidationFunc();                    
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
                    this.tbxSaturn5SMSaturn5SerialNumber.InputProvidedEArgsCreationFunc = this._inputProvidedEArgsFunctionsSource.GetSaturn5SerialNumberEventArgsCreationFunc();
                }
            }
            #endregion
        }
    }
}
