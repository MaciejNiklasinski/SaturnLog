using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;

namespace SaturnMobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        string _info = string.Empty;
        public string Info
        {
            get { return _info; }
            set { SetProperty(ref _info, value); }
        }

        public ICommand OpenWebCommand { get; }

        public ICommand DoStuffCommand { get; }

        public AboutViewModel()
        {
            Title = "About";

            // Setting this property will trigger INotify and update the bound control on the xaml form
            Info = "Find things";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://github.com/MaciejNiklasinski/SaturnLog")));

            // This command can call async workloads
            DoStuffCommand = new Command(async () => await DoStuffAsync());
        }

        

        public async Task DoStuffAsync()
        {
            await Task.CompletedTask;
        }
    }
}