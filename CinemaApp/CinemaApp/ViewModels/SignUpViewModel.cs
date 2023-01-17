using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CinemaApp.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
        private string userName;
        private string password;
        private string password2;
        private string email;
        private string phoneNumber;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string PasswordAgain
        {
            get => password2;
            set => SetProperty(ref password2, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string PhoneNumber
        {
            get => phoneNumber;
            set => SetProperty(ref phoneNumber, value);
        }
        public Command SignUpCommand { get; }

        public SignUpViewModel()
        {
            SignUpCommand = new Command(OnSignUpClicked);
        }

        private async void OnSignUpClicked()
        {
            var emailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";
            IsBusy = true;
            try

            {
                if (userName == null || password == null || email == null || phoneNumber == null)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ thông tin", "OK");
                }
                else if (password != password2)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu không trùng nhau, vui lòng nhập lại! ", "OK");
                }
                else if (!Regex.IsMatch(Email, emailPattern))
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Định dạng email không đúng, vui lòng nhập lại! ", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("MVVM", ex.Message, "OK");
            }
            IsBusy = false;
        }
    }
}
