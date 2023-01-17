using CinemaApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetails : ContentPage
    {
        Movie _movie;
        public MovieDetails()
        {
            InitializeComponent();
        }

        public MovieDetails(Movie movie)
        {
            InitializeComponent();
            GetMovieDetails(movie._id);
        }

        async void GetMovieDetails(string movieID)
        {
            HttpClient httpClient = new HttpClient();

            var movieDetails = await httpClient.GetStringAsync("http://192.168.1.70:3500/movie/"
                + movieID.ToString());
            Movie movieDetailsConverted = JsonConvert.DeserializeObject<Movie>(movieDetails);

            foreach(string text in movieDetailsConverted.genre)
            {
                if (text != movieDetailsConverted.genre[movieDetailsConverted.genre.Count()-1])
                movieDetailsConverted.genreConverter += text + ", ";
                else movieDetailsConverted.genreConverter += text;
            }

            movieDetailsConverted.dateConverter = DateTime.ParseExact(movieDetailsConverted.releaseDate.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            movieDetailsConverted.releaseDate = movieDetailsConverted.dateConverter.ToString("dd/MM/yyyy");

            DateTime now = DateTime.Now;

            if (DateTime.Compare(movieDetailsConverted.dateConverter, now) > 0)
            {
                btnBooking.IsVisible = false;
            }

            movieDetailsBinding.BindingContext = movieDetailsConverted;
            _movie = movieDetailsConverted;
        }

        private void btnBooking_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowTimeCinema(_movie, 2));
        }
    }
}