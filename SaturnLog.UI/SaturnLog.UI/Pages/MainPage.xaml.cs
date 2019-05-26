using SaturnLog.UI.Models;
using SaturnLog.UI.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SaturnLog.UI.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : MasterDetailPage
    {
        // Dictionary of Navigation pages contained by main menu
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        public MainPage()
        {
            this.InitializeComponent();

            this.MasterBehavior = MasterBehavior.Popover;
            
            // Set dafuult page here
            this.MenuPages.Add((int)MenuItemType.Home, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            // Add page here
            if (!this.MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Home:
                        this.MenuPages.Add(id, new NavigationPage(new HomePage()));
                        break;
                    case (int)MenuItemType.Browse:
                        this.MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.About:
                        this.MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            NavigationPage newPage = this.MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                this.Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                this.IsPresented = false;
            }
        }
    }
}
