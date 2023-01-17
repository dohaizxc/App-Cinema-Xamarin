using CinemaApp.Models;
using CinemaApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Forms.Internals.Profile;

namespace CinemaApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {

        Movie _movie;
        List<Movie> carouselMovieNowShowing = new List<Movie>();
        List<Movie> carouselMovieCommingSoon = new List<Movie>();

        public HomePage()
        {
            InitializeComponent();
            GetListMovies();


            List<News> listNews = new List<News>()
            {
                new News()
                {
                    imageNews = "https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/2/0/2023_u22_n_o_240x201.png",
                },
                new News()
                {
                    imageNews = "https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/b/i/birthday_popcorn_box_240x201.png",
                },
                new News()
                {
                    imageNews = "https://www.cgv.vn/media/banner/cache/1/b58515f018eb873dafa430b6f9ae0c1e/2/0/2023_happy_wed-02.png",
                }
            };

            carousel.ItemsSource = listNews;

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

                if (carousel.Position == 2)
                {
                    carousel.Position = 0;
                    return;
                }
                carousel.Position += 1;
            });
        }

        private string RatedImageConverter(string rated)
        {
            if (rated.Contains("13"))
            {
                return "icon_13.png";
            }
            if (rated.Contains("15"))
            {
                return "icon_13.png";
            }
            if (rated.Contains("18"))
            {
                return "icon_13.png";
            }
            return "icon_P.png";
        }

        async void GetListMovies()
        {
            HttpClient httpClient = new HttpClient();

            var listMovie = await httpClient.GetStringAsync("http://192.168.1.70:3500/movie");
            var listMovieConverted = JsonConvert.DeserializeObject<List<Movie>>(listMovie);

            DateTime now = DateTime.Now;

            foreach (Movie movie in listMovieConverted)
            {

                movie.dateConverter = DateTime.ParseExact(movie.releaseDate.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                if (DateTime.Compare(movie.dateConverter, now) <= 0)
                {
                    carouselMovieNowShowing.Add(movie);
                }
                else
                {
                    carouselMovieCommingSoon.Add(movie);
                }

                movie.releaseDate = movie.dateConverter.ToString("dd/MM/yyyy");
                movie.ratedImage = RatedImageConverter(movie.rated);


                if (movie.name.Length >= 19)
                {
                    movie.shortName = movie.name.Substring(0, 19) + "...";
                }
                else
                {
                    movie.shortName = movie.name;
                }
            }


            int temp = 0;
            foreach (Movie movie in carouselMovieNowShowing)
            {
                temp++;
                movie.stt = temp;
            }

            lstMoviesNowShowing.ItemsSource = carouselMovieNowShowing;
            lstMoviesCommingSoon.ItemsSource = carouselMovieCommingSoon;
        }

        private void btnBooking_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Movie movie = button.CommandParameter as Movie;
            Navigation.PushAsync(new ShowTimeCinema(movie, 2));
        }

        private void lstMovies_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            Movie movie = e.CurrentItem as Movie;
            _movie = movie;
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MovieDetails(_movie));
        }

        private void nowShowing_Clicked(object sender, EventArgs e)
        {
            nowShowing.BackgroundColor = Color.SkyBlue;
            commingSoon.BackgroundColor = Color.AliceBlue;
            lstMoviesNowShowing.IsVisible = true;
            lstMoviesCommingSoon.IsVisible = false;
        }

        private void commingSoon_Clicked(object sender, EventArgs e)
        {
            nowShowing.BackgroundColor = Color.AliceBlue;
            commingSoon.BackgroundColor = Color.SkyBlue;
            lstMoviesNowShowing.IsVisible = false;
            lstMoviesCommingSoon.IsVisible = true;
        }
    }
}