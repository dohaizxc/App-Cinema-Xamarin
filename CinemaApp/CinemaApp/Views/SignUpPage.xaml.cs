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
    public partial class SignUpPage : ContentPage
    {
        String _name = null;
        String _email = null;
        String _password = null;
        String _password2 = null;
        String _phone = null;
        String _dayOfBirth = null;
        String _gender = null;

        public SignUpPage()
        {
            InitializeComponent();
        }


        async void FetchAPISignUp()
        {
            var client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new { email = _email, password = _password, name = _name,
                phoneNumber = _phone, gender = _gender, dayOfBirth = _dayOfBirth });
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await client.PostAsync("http://192.168.1.70:3500/auth/register", content);
            if (result.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Đăng ký thành công", "OK");
                Shell.Current.CurrentItem = Shell.Current.Items[0].Items[5];
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Email hoặc số điện thoại không khả dụng!", "OK");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Shell.Current.CurrentItem = Shell.Current.Items[0].Items[5];
        }

        private async void btnSignUp_Clicked(object sender, EventArgs e)
        {
            var phonePattern = @"^[0-9]{10}$";
            var emailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
            _name = entryName.Text;
            _email = entryEmail.Text;
            _phone = entryPhone.Text;
            _password = entryPassword.Text;
            _password2 = entryPassword2.Text;
            _dayOfBirth = getDate.Date.ToString("yyyy-MM-dd");
            if (picker.SelectedIndex != -1)
            {
                _gender = picker.Items[picker.SelectedIndex];
            }
            if (_email == null || _password == null || _phone == null || _password2 == null || _name == null
                || _gender == null)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin!", "OK");
            }
            else if (!Regex.IsMatch(_email, emailPattern))
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Định dạng email không đúng, vui lòng nhập lại!", "OK");
            }
            else if (!Regex.IsMatch(_phone, phonePattern))
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Định dạng số điện thoại không đúng, vui lòng nhập lại!", "OK");
            }
            else if (_password != _password2)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu không trùng nhau, vui lòng nhập lại!", "OK");
            }
            else if (checkbox.IsChecked == false)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng đồng ý với điều khoản dịch vụ!", "OK");
            }
            else
            {
                FetchAPISignUp();
            }
        }
    }
}