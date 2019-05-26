using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SaturnLog.UI.Services;
using SaturnLog.UI.Pages;

namespace SaturnLog.UI
{
    public partial class App : Application
    {

        public App()
        {
            this.InitializeComponent();

            DependencyService.Register<MockDataStore>();
            this.MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
