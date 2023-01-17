using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogIn : ContentPage
    {
        public LogIn()
        {
            InitializeComponent();
            BindingContext = new ViewModels.LoginViewModel();
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("my_key", "my_value");
        }
    }
}