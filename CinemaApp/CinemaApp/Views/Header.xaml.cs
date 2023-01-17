using CinemaApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Header : ContentView
    {
        public Header()
        {
            User userDefault= new User{
                name = "default",
                phoneNumber = "default",
                email= "default",   
            };

            InitializeComponent();
            String userID = Preferences.Get("userID", "default_value");

            if (userID != "default_value")
            {
                String avatar = Preferences.Get("avatar", "avatar_user.png");
                imgAvatar.Source = avatar;
                stlUser.IsVisible = true;
                headerLogOut.IsVisible = false;
                GetUserInfo(userID);
            }
        }

        async void GetUserInfo(string userID)
        {
            HttpClient httpClient = new HttpClient();

            var userInfo = await httpClient.GetStringAsync("http://192.168.1.70:3500/user/"
                + userID);
            User userInfoConverted = JsonConvert.DeserializeObject<User>(userInfo);

            stlUser.BindingContext = userInfoConverted;
        }
    }
}