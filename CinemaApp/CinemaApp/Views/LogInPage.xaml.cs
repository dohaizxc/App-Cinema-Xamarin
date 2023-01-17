using CinemaApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
	public partial class LogInPage : ContentPage
	{
        String email = null;
        String password = null;

        public LogInPage ()
		{
			InitializeComponent ();
            String _email = Preferences.Get("email", "default_value");

            if (_email != "default_value")
            {
                entryAccount.Text = _email;
                checkbox.IsChecked = true;
            }
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            var emailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
            email = entryAccount.Text;
            password = entryPassword.Text;

            if (email == null || password == null)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ tài khoản và mật khẩu", "OK");
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Định dạng email không đúng, vui lòng nhập lại! ", "OK");
            }
            else
            {
                if (checkbox.IsChecked == true)
                {
                    Preferences.Set("email", email);
                }

                FetchAPILogIn(email, password);
            }
        }

        async void FetchAPILogIn(String _email, String _password)
        {
            
            var client = new HttpClient();

            string jsonData = JsonConvert.SerializeObject(new { email = _email, password = _password });
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await client.PostAsync("http://192.168.1.70:3500/auth/userlogin", content);

            if (result.IsSuccessStatusCode)
            {
                var user = await result.Content.ReadAsStringAsync();
                User userConverted = JsonConvert.DeserializeObject<User>(user);
                Preferences.Set("accessToken", userConverted.accessToken);
                Preferences.Set("userID", userConverted._id);
                Preferences.Set("avatar", "avatar_man.png");
                await App.Current.MainPage.DisplayAlert("Thông báo", "Đăng nhập thành công", "OK");
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                Preferences.Set("userID", "default_value");
                await App.Current.MainPage.DisplayAlert("Thông báo", "Tên tài khoản hoặc mật khẩu không đúng!", "OK");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.CurrentItem = Shell.Current.Items[0].Items[6];
        }

        private void checkbox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

            if (checkbox.IsChecked == false)
            {
                Preferences.Set("email", "default_value");
            }
        }

        private void forgetPassword_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgetPassword());
        }
    }
}