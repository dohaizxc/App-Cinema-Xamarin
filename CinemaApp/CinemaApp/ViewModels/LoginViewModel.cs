using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string userName;
        private string password;
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

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked()
        {
            IsBusy = true;
            try
            {
                if (userName == null || password == null)
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng nhập đầy đủ tài khoản và mật khẩu", "OK");
                }
                else
                    if (userName == "123" && password == "123")
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Đăng nhập thành công", "OK");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Tên tài khoản hoặc mật khẩu không đúng!", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", ex.Message, "OK");
            }
            IsBusy = false;
        }

    }
}
