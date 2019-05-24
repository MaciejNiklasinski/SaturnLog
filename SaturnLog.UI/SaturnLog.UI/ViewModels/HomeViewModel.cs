using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SaturnLog.UI.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        

        private bool _disconnectButtonIsEnabled = false;
        public bool DisconnectButtonIsEnabled
        {
            get { return _disconnectButtonIsEnabled; }
            set => this.SetProperty(ref this._disconnectButtonIsEnabled, value);
        }

        private bool _connectButtonIsEnabled = true;
        public bool ConnectButtonIsEnabled
        {
            get { return _connectButtonIsEnabled; }
            set => this.SetProperty(ref this._connectButtonIsEnabled, value);
        }



        private bool _logOutButtonIsEnabled = false;
        public bool LogOutButtonIsEnabled
        {
            get { return _logOutButtonIsEnabled; }
            set => this.SetProperty(ref this._logOutButtonIsEnabled, value);
        }

        private bool _logInButtonIsEnabled = true;
        public bool LogInButtonIsEnabled
        {
            get { return _logInButtonIsEnabled; }
            set => this.SetProperty(ref this._logInButtonIsEnabled, value);
        }


        private string _databaseLogoText = "Database";
        public string DatabaseLogoText
        {
            get { return _databaseLogoText; }
            set => this.SetProperty(ref this._databaseLogoText, value);
        }

        private string _connectButtonText = "Connect";
        public string ConnectButtonText
        {
            get { return _connectButtonText; }
            set => this.SetProperty(ref this._connectButtonText, value);
        }

        private string _disonnectButtonText = "Disonnect";
        public string DisonnectButtonText
        {
            get { return _disonnectButtonText; }
            set => this.SetProperty(ref this._disonnectButtonText, value);
        }

        private string _accountLogoText = "Account";
        public string AccountLogoText
        {
            get { return _accountLogoText; }
            set => this.SetProperty(ref this._accountLogoText, value);
        }
        
        private string _logInButtonText = "LogIn";
        public string LogInButtonText
        {
            get { return _logInButtonText; }
            set => this.SetProperty(ref this._logInButtonText, value);
        }

        private string _logOutButtonText = "LogOut";
        public string LogOutButtonText
        {
            get { return _logOutButtonText; }
            set => this.SetProperty(ref this._logOutButtonText, value);
        }



        public ICommand DisconnectCommand { get; }
        public ICommand ConnectCommand { get; }
        public ICommand LogOutCommand { get; }
        public ICommand LogInCommand { get; }

        public HomeViewModel() 
        {
            Title = "Home";


            DisconnectCommand = new Command(() => { this.DatabaseLogoText = "Database"; this.DisconnectButtonIsEnabled = false; this.ConnectButtonIsEnabled = true; this.LogOutButtonIsEnabled = false; this.LogInButtonIsEnabled = true; });






            ConnectCommand = new Command(async () => 
            {
                this.DisconnectButtonIsEnabled = true; this.ConnectButtonIsEnabled = false; this.LogOutButtonIsEnabled = true; this.LogInButtonIsEnabled = false;

                int countdown = 60;

                do
                {
                    this.DatabaseLogoText = countdown.ToString() + " seconds left.";

                    await Task.Delay(1000);

                    countdown--;
                } while (this.DatabaseLogoText != "Database" && countdown > 0);

                this.DatabaseLogoText = "Database"; this.DisconnectButtonIsEnabled = false; this.ConnectButtonIsEnabled = true; this.LogOutButtonIsEnabled = false; this.LogInButtonIsEnabled = true;
            });






            LogOutCommand = new Command(() => { this.DisconnectButtonIsEnabled = false; this.ConnectButtonIsEnabled = true; this.LogOutButtonIsEnabled = false; this.LogInButtonIsEnabled = true; });
            LogInCommand = new Command(() => { this.DisconnectButtonIsEnabled = true; this.ConnectButtonIsEnabled = false; this.LogOutButtonIsEnabled = true; this.LogInButtonIsEnabled = false; });
        }
    }
}
