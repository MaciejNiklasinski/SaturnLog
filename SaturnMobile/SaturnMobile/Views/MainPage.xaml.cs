using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaturnMobile.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Shell.Current.SendBackButtonPressed();
        }

    }
}