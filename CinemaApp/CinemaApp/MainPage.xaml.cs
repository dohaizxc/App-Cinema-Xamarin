using CinemaApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CinemaApp
{
    public partial class MainPage : Shell
    {
        public ICommand TapCommand => new Command<string>(async (url) => await Launcher.OpenAsync(url));

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            var menuItem = logOut;

            String userID = Preferences.Get("userID", "default_value");

            if (userID != "default_value")
            {
                userInfo.IsVisible = true;
                historyBooking.IsVisible = true;
                logIn.IsVisible = false;
                signUp.IsVisible = false;
                //CurrentShell.Items.Add(menuItem);
            }
            else
            {
                foreach (ShellItem item in CurrentShell.Items)
                {
                    if (item.Title == menuItem.Text)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }
                }
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //refresh the data
        }

        private async void logOut_Clicked(object sender, EventArgs e)
        {
            bool answer = await App.Current.MainPage.DisplayAlert("Thông báo", "Bạn có muốn đăng xuất?", "Có", "Không");

            if (answer)
            {
                Preferences.Set("userID", "default_value");
                Application.Current.MainPage = new MainPage();
            }
        }
    }
}
