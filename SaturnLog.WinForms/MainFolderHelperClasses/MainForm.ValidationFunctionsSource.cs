using SaturnLog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.UI
{
    public partial class MainForm
    {
        public class ValidationFunctionsSource
        {
            #region Private Fields
            private MainForm _form;
            private App _app { get { return this._form._app; } }
            #endregion

            #region Constructor
            public ValidationFunctionsSource(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public Func<string, bool> GetUsernameValidationFunc()
            {
                return (username) => { return this._app.UserServices.HasUsername(username); };
            }

            public Func<string, bool> GetSerialNumberValidationFunc()
            {
                return (serialNumber) => { return this._app.Saturn5Services.HasSerialNumber(serialNumber); };

            }

            public Func<string, bool> GetShortIdValidationFunc()
            {
                return (shortId) => { return this._app.Saturn5Services.HasShortId(shortId); };
            }

            public Func<string, bool> GetUserTypeValidationFunc()
            {
                return (userTypeString) =>
                {
                    try
                    {
                        UserType userType = UserTypeProvider.GetFromString(userTypeString);

                        return true;
                    }
                    catch (ArgumentException ex) when (ex.ParamName == nameof(userTypeString))
                    { return false; }
                };
            }

            public Func<string, bool> GetFirstNameValidationFunc()
            {
                return (firstName) =>
                {
                    if (firstName is null || firstName.Trim() == "")
                        return false;
                    else
                        return true;
                };
            }

            public Func<string, bool> GetSurnameValidationFunc()
            {
                return (surname) =>
                {
                    if (surname is null || surname.Trim() == "")
                        return false;
                    else
                        return true;
                };
            }
            #endregion
        }
    }
}
