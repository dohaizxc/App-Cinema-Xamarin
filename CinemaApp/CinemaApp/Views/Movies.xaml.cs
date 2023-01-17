using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Globalization;

namespace CinemaApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Movies : ContentPage
    {
        List<Movie> listMovies = new List<Movie>();
        List<Movie> carouselMovieNowShowing = new List<Movie>();
        List<Movie> carouselMovieCommingSoon = new List<Movie>();

        public Movies() : this("NowShowing") { }
        public Movies(string type)
        {
            InitializeComponent();
            getListMovies(type);
            
        }
        async void getListMovies(string type)
        {
            HttpClient httpClient = new HttpClient();

            var listMovie = await httpClient.GetStringAsync("http://192.168.1.70:3500/movie");
            List<Movie> listMovieConverted = JsonConvert.DeserializeObject<List<Movie>>(listMovie);

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
            }




            if (type == "NowShowing")
            {
                listMovies = null;
                listMovies = carouselMovieNowShowing;
            }
            else
            {
                listMovies = null;
                listMovies = carouselMovieCommingSoon;
            }

            lstMovies.ItemsSource = listMovies;
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

        private void lstMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstMovies.SelectedItem != null)
            {
                Movie movie = (Movie)lstMovies.SelectedItem;
                Navigation.PushAsync(new MovieDetails(movie));
            }
        }
    }
}