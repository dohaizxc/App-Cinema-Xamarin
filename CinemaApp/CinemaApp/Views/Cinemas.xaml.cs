using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using CinemaApp.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cinemas : ContentPage
    {
        ProvinceViewModel listCinemas = new ProvinceViewModel();

        public Cinemas()
        {
            InitializeComponent();
            getListCinemas(listCinemas);
        }

        async void getListCinemas(ProvinceViewModel listCinemas)
        {
            HttpClient httpClient = new HttpClient();

            var listCinema = await httpClient.GetStringAsync("http://192.168.1.70:3500/cinema");
            List<Cinema> listMovieConverted = JsonConvert.DeserializeObject<List<Cinema>>(listCinema);
            listCinemas._allCinemas.AddRange(listMovieConverted);
            var groupData = listCinemas._allCinemas.GroupBy(f => f.provinceID).Select(f => new Province(f.Key, f.ToList()));
            listCinemas.provinces.AddRange(groupData);

            foreach (Province _province in listCinemas.provinces)
            {
                _province.Clear();
            }
            BindingContext = listCinemas;
        }

        private void lstProvinces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCinemas.SelectedItem != null)
            {
                Cinema cinema = (Cinema)lstCinemas.SelectedItem;

                foreach (Cinema _cinema in listCinemas._allCinemas)
                {
                    _cinema.Color = "White";
                }

                cinema.Color = "LightBlue";

                BindingContext = null;
                BindingContext = listCinemas;
                Navigation.PushAsync(new CinemaDetails(cinema));
                lstCinemas.SelectedItem = null;
            }
        }
    }
}