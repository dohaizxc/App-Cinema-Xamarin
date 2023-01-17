using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class CinemaDetails : ContentPage
    {
        Cinema _cinema;

        public CinemaDetails()
        {
            InitializeComponent();
        }

        public CinemaDetails(Cinema cinema)
        {
            InitializeComponent();

            List<News> listNews = new List<News>()
            {
                new News()
                {
                    imageNews = "https://www.cgv.vn/media/site/cache/1/980x415/b58515f018eb873dafa430b6f9ae0c1e/c/g/cgv-landmark-1.png",
                },
                new News()
                {
                    imageNews = "https://www.cgv.vn/media/site/cache/1/980x415/b58515f018eb873dafa430b6f9ae0c1e/c/g/cgv-landmark-4.png",
                },
                new News()
                {
                    imageNews = "https://www.cgv.vn/media/site/cache/1/980x415/b58515f018eb873dafa430b6f9ae0c1e/c/g/cgv-landmark-5.png",
                },
                new News()
                {
                    imageNews = "https://www.cgv.vn/media/site/cache/1/980x415/b58515f018eb873dafa430b6f9ae0c1e/c/g/cgv-landmark-2.png",
                },
                new News()
                {
                    imageNews = "https://www.cgv.vn/media/site/cache/1/980x415/b58515f018eb873dafa430b6f9ae0c1e/c/g/cgv-landmark-3.png",
                }
            };
            _cinema = cinema;
            Title = cinema.cinemaName;
            carousel.ItemsSource = listNews;
            name.Text = cinema.cinemaName;
            address.Text = cinema.address;
            add.CommandParameter = cinema.address_url;
        }

        private Timer timer;

        protected override void OnAppearing()
        {
            timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true, Enabled = true };
            timer.Elapsed += Timer_Elapsed;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            timer?.Dispose();
            base.OnDisappearing();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                if (carousel.Position == 4)
                {
                    carousel.Position = 0;
                    return;
                }
                carousel.Position += 1;
            });
        }

        private void btnBooking_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowTimeCinema(_cinema, 1));
        }

        private void btnLocation_Clicked(object sender, EventArgs e)
        {

        }
    }
}