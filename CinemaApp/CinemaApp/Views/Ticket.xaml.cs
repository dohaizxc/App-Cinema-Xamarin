using CinemaApp.Models;
using CinemaApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ticket : ContentPage
    {

        public Ticket()
        {
            InitializeComponent();
        }

        public Ticket(String id, int type)
        {
            InitializeComponent();
            if (type != 1)
            {
                active.IsVisible = false;
            }
            GetTicket(id, type);
        }


        private string SeatsConverter(string seats)
        {
            string[] listSeats = seats.Split(',');

            List<int> seatsConverter = new List<int>();

            String _seats = null;

            foreach (String seat in listSeats)
            {
                seatsConverter.Add(int.Parse(seat));
            }

            foreach (int seat in seatsConverter)
            {
                _seats += ((char)(seat / 10 + 65)).ToString() + (seat % 10).ToString() + ", ";
            }

            return _seats.Remove(_seats.Length - 2); ;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void GetTicket(string ticketID, int price)
        {
            HttpClient httpClient = new HttpClient();

            var ticketz = await httpClient.GetStringAsync("http://192.168.1.70:3500/ticket/"
                + ticketID);

            TicketExp ticketConverted = JsonConvert.DeserializeObject<TicketExp>(ticketz);

            if (ticketConverted.foods.Length < 1 )
            {
                haveFood.IsVisible = false;
            }

            ticketConverted.seat = SeatsConverter(ticketConverted.seat);

            gridMovie.BindingContext = ticketConverted;

            
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new MainPage();
        }
    }
}