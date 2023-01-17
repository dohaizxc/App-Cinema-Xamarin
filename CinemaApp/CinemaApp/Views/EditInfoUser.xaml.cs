using CinemaApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditInfoUser : ContentPage
    {
        String _avatar = Preferences.Get("avatar", "avatar_user.png");

        String _name = null;
        String _phone = null;
        String _dayOfBirth;
        String _gender = null;

        public EditInfoUser()
        {

            List<News> listNews = new List<News>()
            {
                new News()
                {
                    imageNews = "avatar_user.png",
                },
                new News()
                {
                    imageNews = "avatar_man.png",
                },
                new News()
                {
                    imageNews = "avatar_woman.png",
                },
                new News()
                {
                    imageNews = "avatar_man1.png",
                },
                new News()
                {
                    imageNews = "avatar_woman1.png",
                },
                new News()
                {
                    imageNews = "avatar_man2.png",
                },
                new News()
                {
                    imageNews = "avatar_woman2.png",
                },
                new News()
                {
                    imageNews = "avatar_man3.png",
                },
                new News()
                {
                    imageNews = "avatar_woman3.png",
                },
                new News()
                {
                    imageNews = "avatar_cat.png",
                },
                new News()
                {
                    imageNews = "avatar_dog.png",
                },
                new News()
                {
                    imageNews = "avatar_fox.png",
                },
                new News()
                {
                    imageNews = "avatar_koala.png",
                },
                new News()
                {
                    imageNews = "avatar_rabbit.png",
                },
                new News()
                {
                    imageNews = "avatar_weasel.png",
                },
            };
            InitializeComponent();
            String userID = Preferences.Get("userID", "default_value");
            String avatar = Preferences.Get("avatar", "avatar_user.png");
            carousel.ItemsSource = listNews;
            imgAvatar.Source = avatar;
            GetUserInfo(userID);
        }

        async void FetchAPIUpdateUser(String userID)
        {
            var client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new
            {
                name = _name,
                phoneNumber = _phone,
                gender = _gender,
                dayOfBirth = _dayOfBirth
            });
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await client.PostAsync("http://192.168.1.70:3500/user/" + userID, content);

            if (result.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Sửa thông tin thành công!", "OK");
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Số điện thoại không khả dụng!", "OK");
            }
        }

        async void GetUserInfo(string userID)
        {
            HttpClient httpClient = new HttpClient();

            var userInfo = await httpClient.GetStringAsync("http://192.168.1.70:3500/user/"
                + userID);
            User userInfoConverted = JsonConvert.DeserializeObject<User>(userInfo);
            infoUser.BindingContext = userInfoConverted;
            getDate.Date = userInfoConverted.dayOfBirth;
            if (userInfoConverted.gender == "Nam")
            {
                picker.SelectedIndex = 0;
            }
            else picker.SelectedIndex = 1;

        }

        private void carousel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (carousel.SelectedItem != null)
            {
                News news = (News)carousel.SelectedItem;
                _avatar = news.imageNews;
                imgAvatar.Source = news.imageNews;
            }
        }

        private async void btnEdit_Clicked(object sender, EventArgs e)
        {

            _name = entryName.Text;
            _phone = entryPhone.Text;

            _dayOfBirth = getDate.Date.ToString();


            if (picker.SelectedIndex != -1)
            {
                _gender = picker.Items[picker.SelectedIndex];
            }


            var phonePattern = @"^[0-9]{10}$";
            if (!Regex.IsMatch(_phone, phonePattern))
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Định dạng số điện thoại không đúng, vui lòng nhập lại!", "OK");
            }
            Preferences.Set("avatar", _avatar);
            String userID = Preferences.Get("userID", "default_value");
            FetchAPIUpdateUser(userID);
        }
    }
}