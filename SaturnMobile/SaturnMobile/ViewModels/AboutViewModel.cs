using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace SaturnMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://github.com/MaciejNiklasinski/SaturnLog")));
        }

        public ICommand OpenWebCommand { get; }
    }
}