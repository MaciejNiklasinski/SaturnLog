using SaturnLog.Core;
using SaturnLog.Core.EventArgs;
using SaturnLog.UI.ControlsExtensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaturnLog.UI
{
    public partial class MainForm
    {
        private class DataDisplayServices
        {
            #region Private Fields
            private MainForm _form;
            
            #region Pre-Brief Tab
            private TextBox tbxPreBriefUserUsername { get { return this._form.tbxPreBriefUserUsername; } }
            private TextBox tbxPreBriefUserType { get { return this._form.tbxPreBriefUserType; } }
            private TextBox tbxPreBriefUserFirstName { get { return this._form.tbxPreBriefUserFirstName; } }
            private TextBox tbxPreBriefUserSurname { get { return this._form.tbxPreBriefUserSurname; } }

            private TextBox tbxPreBriefSaturn5SerialNumber { get { return this._form.tbxPreBriefSaturn5SerialNumber; } }
            private TextBox tbxPreBriefSaturn5Barcode { get { return this._form.tbxPreBriefSaturn5Barcode; } }

            private RichTextBox rtbPreBriefInfo { get { return this._form.rtbPreBriefInfo; } }
            #endregion

            #region De-Brief Tab
            private TextBox tbxDeBriefUserUsername { get { return this._form.tbxDeBriefUserUsername; } }
            private TextBox tbxDeBriefUserType { get { return this._form.tbxDeBriefUserType; } }
            private TextBox tbxDeBriefUserFirstName { get { return this._form.tbxDeBriefUserFirstName; } }
            private TextBox tbxDeBriefUserSurname { get { return this._form.tbxDeBriefUserSurname; } }

            private TextBox tbxDeBriefSaturn5SerialNumber { get { return this._form.tbxDeBriefSaturn5SerialNumber; } }
            private TextBox tbxDeBriefSaturn5Barcode { get { return this._form.tbxDeBriefSaturn5Barcode; } }

            private RichTextBox rtbDeBriefInfo { get { return this._form.rtbDeBriefInfo; } }
            #endregion

            #region Options Tab
            private TextBox tbxOptionsUserUsername { get { return this._form.tbxOptionsUserUsername; } }
            private TextBox tbxOptionsUserType { get { return this._form.tbxOptionsUserType; } }
            private TextBox tbxOptionsUserFirstName { get { return this._form.tbxOptionsUserFirstName; } }
            private TextBox tbxOptionsUserSurname { get { return this._form.tbxOptionsUserSurname; } }

            private TextBox tbxOptionsSaturn5SerialNumber { get { return this._form.tbxOptionsSaturn5SerialNumber; } }
            private TextBox tbxOptionsSaturn5Barcode { get { return this._form.tbxOptionsSaturn5Barcode; } }

            private RichTextBox rtbOptionsInfo { get { return this._form.rtbOptionsInfo; } }
            #endregion

            #region Saturn5 Stock Management Tab
            private TextBox tbxSaturn5SMSerialNumber { get { return this._form.tbxSaturn5SMSerialNumber; } }
            private TextBox tbxSaturn5SMBarcode { get { return this._form.tbxSaturn5SMBarcode; } }
            
            private RichTextBox rtbSaturn5SMInfo { get { return this._form.rtbSaturn5SMInfo; } }
            #endregion

            #region Options Taba
            private TextBox tbxAdminUserUsername { get { return this._form.tbxAdminUserUsername; } }
            private TextBox tbxAdminUserType { get { return this._form.tbxAdminUserType; } }
            private TextBox tbxAdminUserFirstName { get { return this._form.tbxAdminUserFirstName; } }
            private TextBox tbxAdminUserSurname { get { return this._form.tbxAdminUserSurname; } }
            
            private RichTextBox rtbAdminInfo { get { return this._form.rtbAdminInfo; } }
            #endregion
            #endregion

            #region Constructor
            public DataDisplayServices(MainForm form)
            {
                this._form = form;
            }
            #endregion

            #region Methods
            public void ClearInfoBoxes(object sender, EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(ClearUserRelatedDataDisplayTextBoxes);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    rtbPreBriefInfoServices.Clear(this.rtbPreBriefInfo);
                    rtbDeBriefInfoServices.Clear(this.rtbDeBriefInfo);
                    rtbOptionsInfoServices.Clear(this.rtbOptionsInfo);
                    //rtbSaturn5SMInfoServices.Clear(this.rtbSaturn5SMInfo);
                    //rtbAdminInfoServices.Clear(this.rtbAdminInfo);
                }
            }

            public void ClearUserRelatedDataDisplayTextBoxes(object sender, System.EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(ClearUserRelatedDataDisplayTextBoxes);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Pre-Brief tab
                    this.tbxPreBriefUserUsername.Clear();
                    this.tbxPreBriefUserType.Clear();
                    this.tbxPreBriefUserFirstName.Clear();
                    this.tbxPreBriefUserSurname.Clear();

                    // De-Brief tab
                    this.tbxDeBriefUserUsername.Clear();
                    this.tbxDeBriefUserType.Clear();
                    this.tbxDeBriefUserFirstName.Clear();
                    this.tbxDeBriefUserSurname.Clear();

                    // Options tab
                    this.tbxOptionsUserUsername.Clear();
                    this.tbxOptionsUserType.Clear();
                    this.tbxOptionsUserFirstName.Clear();
                    this.tbxOptionsUserSurname.Clear();

                    // Admin tab
                    this.tbxAdminUserUsername.Clear();
                    this.tbxAdminUserType.Clear();
                    this.tbxAdminUserFirstName.Clear();
                    this.tbxAdminUserSurname.Clear();
                }
            }

            public void ClearSaturn5RelatedDisplayTextBoxes(object sender, System.EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(ClearSaturn5RelatedDisplayTextBoxes);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Pre-Brief tab
                    this.tbxPreBriefSaturn5SerialNumber.Clear();
                    this.tbxPreBriefSaturn5Barcode.Clear();

                    // De-Brief tab
                    this.tbxDeBriefSaturn5SerialNumber.Clear();
                    this.tbxDeBriefSaturn5Barcode.Clear();

                    // Options tab
                    this.tbxOptionsSaturn5SerialNumber.Clear();
                    this.tbxOptionsSaturn5Barcode.Clear();

                    // Saturn5 Stock Management tab
                    this.tbxSaturn5SMSerialNumber.Clear();
                    this.tbxSaturn5SMBarcode.Clear();
                }
            }

            public void ClearAllDataDisplayTextBoxes(object sender, System.EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, EventArgs> d = new Action<object, EventArgs>(ClearAllDataDisplayTextBoxes);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Pre-Brief tab
                    this.tbxPreBriefUserUsername.Clear();
                    this.tbxPreBriefUserType.Clear();
                    this.tbxPreBriefUserFirstName.Clear();
                    this.tbxPreBriefUserSurname.Clear();

                    this.tbxPreBriefSaturn5SerialNumber.Clear();
                    this.tbxPreBriefSaturn5Barcode.Clear();

                    // De-Brief tab
                    this.tbxDeBriefUserUsername.Clear();
                    this.tbxDeBriefUserType.Clear();
                    this.tbxDeBriefUserFirstName.Clear();
                    this.tbxDeBriefUserSurname.Clear();

                    this.tbxDeBriefSaturn5SerialNumber.Clear();
                    this.tbxDeBriefSaturn5Barcode.Clear();

                    // Options tab
                    this.tbxOptionsUserUsername.Clear();
                    this.tbxOptionsUserType.Clear();
                    this.tbxOptionsUserFirstName.Clear();
                    this.tbxOptionsUserSurname.Clear();

                    this.tbxOptionsSaturn5SerialNumber.Clear();
                    this.tbxOptionsSaturn5Barcode.Clear();

                    // Saturn5 Stock Management tab
                    this.tbxSaturn5SMSerialNumber.Clear();
                    this.tbxSaturn5SMBarcode.Clear();

                    // Admin tab
                    this.tbxAdminUserUsername.Clear();
                    this.tbxAdminUserType.Clear();
                    this.tbxAdminUserFirstName.Clear();
                    this.tbxAdminUserSurname.Clear();
                }
            }
            
            #region Retrieving Data
            #region Retrieving User Data
            public void OnRetrievingUserDataBegan(object sender, UserUsernameEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserUsernameEventArgs> d = new Action<object, UserUsernameEventArgs>(OnRetrievingUserDataBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Pre-Brief tab
                    this.tbxPreBriefUserUsername.Text = e.Username;
                    this.tbxPreBriefUserType.Clear();
                    this.tbxPreBriefUserFirstName.Clear();
                    this.tbxPreBriefUserSurname.Clear();

                    // De-Brief tab
                    this.tbxDeBriefUserUsername.Text = e.Username;
                    this.tbxDeBriefUserType.Clear();
                    this.tbxDeBriefUserFirstName.Clear();
                    this.tbxDeBriefUserSurname.Clear();

                    // Options tab
                    this.tbxOptionsUserUsername.Text = e.Username;
                    this.tbxOptionsUserType.Clear();
                    this.tbxOptionsUserFirstName.Clear();
                    this.tbxOptionsUserSurname.Clear();

                    // Admin tab
                    this.tbxAdminUserUsername.Text = e.Username;
                    this.tbxAdminUserType.Clear();
                    this.tbxAdminUserFirstName.Clear();
                    this.tbxAdminUserSurname.Clear();
                }
            }

            public void OnRetrievingUserDataCompleted(object sender, UserEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, UserEventArgs> d = new Action<object, UserEventArgs>(OnRetrievingUserDataCompleted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Pre-Brief tab
                    this.tbxPreBriefUserUsername.Text = e.User.Username;
                    this.tbxPreBriefUserType.Text = e.User.Type.GetDisplayableString();
                    this.tbxPreBriefUserFirstName.Text = e.User.FirstName;
                    this.tbxPreBriefUserSurname.Text = e.User.Surname;

                    // De-Brief tab
                    this.tbxDeBriefUserUsername.Text = e.User.Username;
                    this.tbxDeBriefUserType.Text = e.User.Type.GetDisplayableString();
                    this.tbxDeBriefUserFirstName.Text = e.User.FirstName;
                    this.tbxDeBriefUserSurname.Text = e.User.Surname;

                    // Options tab
                    this.tbxOptionsUserUsername.Text = e.User.Username;
                    this.tbxOptionsUserType.Text = e.User.Type.GetDisplayableString();
                    this.tbxOptionsUserFirstName.Text = e.User.FirstName;
                    this.tbxOptionsUserSurname.Text = e.User.Surname;

                    // Admin tab
                    this.tbxAdminUserUsername.Text = e.User.Username;
                    this.tbxAdminUserType.Text = e.User.Type.GetDisplayableString();
                    this.tbxAdminUserFirstName.Text = e.User.FirstName;
                    this.tbxAdminUserSurname.Text = e.User.Surname;
                }
            }
            #endregion

            #region Retrieving Saturn5 Data
            public void OnRetrievingSaturn5DataBySerialNumberBegan(object sender, Saturn5SerialNumberEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5SerialNumberEventArgs> d = new Action<object, Saturn5SerialNumberEventArgs>(OnRetrievingSaturn5DataBySerialNumberBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Pre-Brief tab
                    this.tbxPreBriefSaturn5SerialNumber.Text = e.SerialNumber;
                    this.tbxPreBriefSaturn5Barcode.Clear();

                    // De-Brief tab
                    this.tbxDeBriefSaturn5SerialNumber.Text = e.SerialNumber;
                    this.tbxDeBriefSaturn5Barcode.Clear();

                    // Options tab
                    this.tbxOptionsSaturn5SerialNumber.Text = e.SerialNumber;
                    this.tbxOptionsSaturn5Barcode.Clear();

                    // Saturn 5 Stock Management tab
                    this.tbxSaturn5SMSerialNumber.Text = e.SerialNumber;
                    this.tbxSaturn5SMBarcode.Clear();
                }
            }
            
            public void OnRetrievingSaturn5DataByShortIdBegan(object sender, Saturn5ShortIdEventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5ShortIdEventArgs> d = new Action<object, Saturn5ShortIdEventArgs>(OnRetrievingSaturn5DataByShortIdBegan);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Pre-Brief tab
                    this.tbxPreBriefSaturn5SerialNumber.Clear();
                    this.tbxPreBriefSaturn5Barcode.Text = e.ShortId;

                    // De-Brief tab
                    this.tbxDeBriefSaturn5SerialNumber.Clear();
                    this.tbxDeBriefSaturn5Barcode.Text = e.ShortId;

                    // Options tab
                    this.tbxOptionsSaturn5SerialNumber.Clear();
                    this.tbxOptionsSaturn5Barcode.Text = e.ShortId;

                    // Saturn 5 Stock Management tab
                    this.tbxSaturn5SMSerialNumber.Clear();
                    this.tbxSaturn5SMBarcode.Text = e.ShortId;
                }
            }
            
            public void OnRetrievingSaturn5DataCompleted(object sender, Saturn5EventArgs e)
            {
                if (this._form.InvokeRequired)
                {
                    Action<object, Saturn5EventArgs> d = new Action<object, Saturn5EventArgs>(OnRetrievingSaturn5DataCompleted);
                    if (!this._form.IsDisposed)
                        this._form.Invoke(d, sender, e);
                }
                else if (!this._form.IsDisposed)
                {
                    // Pre-Brief tab
                    this.tbxPreBriefSaturn5SerialNumber.Text = e.Saturn5.SerialNumber;
                    this.tbxPreBriefSaturn5Barcode.Text = e.Saturn5.ShortId;

                    // De-Brief tab
                    this.tbxDeBriefSaturn5SerialNumber.Text = e.Saturn5.SerialNumber;
                    this.tbxDeBriefSaturn5Barcode.Text = e.Saturn5.ShortId;

                    // Options tab
                    this.tbxOptionsSaturn5SerialNumber.Text = e.Saturn5.SerialNumber;
                    this.tbxOptionsSaturn5Barcode.Text = e.Saturn5.ShortId;

                    // Saturn 5 Stock Management tab
                    this.tbxSaturn5SMSerialNumber.Text = e.Saturn5.SerialNumber;
                    this.tbxSaturn5SMBarcode.Text = e.Saturn5.ShortId;
                }
            }
            #endregion

            public void OnRetrievingUserAndSaturn5DataCompleted(object sender, UserWithSaturn5EventArgs e)
            {
                this.OnRetrievingUserDataCompleted(sender, new UserEventArgs(e.User));

                this.OnRetrievingSaturn5DataCompleted(sender, new Saturn5EventArgs(e.Saturn5));
            }

            public void OnRetrievingSaturn5WithInvalidLastUserDataCompleted(object sender, Saturn5EventArgs e)
            {
                this.OnRetrievingUserDataBegan(sender, new UserUsernameEventArgs(e.Saturn5.LastSeenUsername));

                this.OnRetrievingSaturn5DataCompleted(sender, new Saturn5EventArgs(e.Saturn5));
            }
            #endregion
            #endregion
        }
    }
}
