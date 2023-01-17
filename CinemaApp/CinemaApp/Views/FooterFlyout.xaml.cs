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
    public partial class FooterFlyout : ContentView
    {
        public FooterFlyout()
        {
            InitializeComponent();

        }

        private void logOut_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("userID", "default_value");
            MessagingCenter.Send<object>(new Header(), "hi");
            //Shell.Current.CurrentItem = Shell.Current.Items[0].Items[0];
            Application.Current.MainPage = new MainPage();
        }
    }
}