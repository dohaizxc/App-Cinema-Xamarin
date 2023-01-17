using CinemaApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        int _total = 0;
        public UserPage()
        {
            InitializeComponent();
            slider.IsEnabled = false;

            gridSlider.WidthRequest = DeviceDisplay.MainDisplayInfo.Width;

            User userDefault = new User
            {
                name = "default",
                phoneNumber = "default",
                email = "default",
            };
            String userID = Preferences.Get("userID", "default_value");

            String avatar = Preferences.Get("avatar", "avatar_user.png");
            imgAvatar.Source = avatar;

            GetUserInfo(userID);

                

        }

        async void GetUserInfo(string userID)
        {
            HttpClient httpClient = new HttpClient();

            var userInfo = await httpClient.GetStringAsync("http://192.168.1.70:3500/user/"
                + userID);
            User userInfoConverted = JsonConvert.DeserializeObject<User>(userInfo);

            stlUser.BindingContext = userInfoConverted;
            slider.Value = userInfoConverted.total;
            _total = userInfoConverted.total;
        }

        private void btnHistoryTicket_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryTicket());
        }

        protected override void OnAppearing()
        {
            //Write the code of your page here
            base.OnAppearing();
        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditInfoUser());
        }

        private void btnChangePass_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePasswordPage());
        }

        private void btnMembership_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MembershipPage(_total));
        }
    }
}