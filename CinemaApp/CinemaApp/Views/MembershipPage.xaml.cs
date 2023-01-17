using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MembershipPage : ContentPage
	{
		public MembershipPage (int total)
		{
			InitializeComponent ();
            slider.Value = total;
            totalPayment.BindingContext = total;

            if (total >= 10000000)
            {
                rankMember.Text = "KIM CƯƠNG";
                sliver.BackgroundColor = Color.AliceBlue;
                golden.BackgroundColor = Color.AliceBlue;
                diamond.BackgroundColor = Color.SkyBlue;
                txt_sliver.IsVisible = false;
                txt_golden.IsVisible = false;
                txt_diamond.IsVisible = true;
            }
            else if (total >= 5000000)
            {
                rankMember.Text = "VÀNG";
                sliver.BackgroundColor = Color.AliceBlue;
                golden.BackgroundColor = Color.SkyBlue;
                diamond.BackgroundColor = Color.AliceBlue;
                txt_sliver.IsVisible = false;
                txt_golden.IsVisible = true;
                txt_diamond.IsVisible = false;
            }
            else
            {
                rankMember.Text = "BẠC";
            }

        }

        private void sliver_Clicked(object sender, EventArgs e)
        {
            sliver.BackgroundColor = Color.SkyBlue;
            golden.BackgroundColor = Color.AliceBlue;
            diamond.BackgroundColor = Color.AliceBlue;
            txt_sliver.IsVisible = true;
            txt_golden.IsVisible = false;
            txt_diamond.IsVisible = false;
        }

        private void golden_Clicked(object sender, EventArgs e)
        {
            sliver.BackgroundColor = Color.AliceBlue;
            golden.BackgroundColor = Color.SkyBlue;
            diamond.BackgroundColor = Color.AliceBlue;
            txt_sliver.IsVisible = false;
            txt_golden.IsVisible = true;
            txt_diamond.IsVisible = false;
        }

        private void diamond_Clicked(object sender, EventArgs e)
        {
            sliver.BackgroundColor = Color.AliceBlue;
            golden.BackgroundColor = Color.AliceBlue;
            diamond.BackgroundColor = Color.SkyBlue;
            txt_sliver.IsVisible = false;
            txt_golden.IsVisible = false;
            txt_diamond.IsVisible = true;
        }
    }
}