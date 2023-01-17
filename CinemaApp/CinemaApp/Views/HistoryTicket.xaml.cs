using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft;
using System.Net.Http;
using Newtonsoft.Json;
using CinemaApp.Models;
using Xamarin.Essentials;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryTicket : ContentPage
    {
        List<TicketExp> listTickets = new List<TicketExp>();
        public HistoryTicket()
        {
            InitializeComponent();

            String userID = Preferences.Get("userID", "default_value");

            if (userID != "default_value")
            {
                GetListTickets(userID);
            }
            else
            {
                txt_active.IsVisible = true;
                active.IsRunning = false;
                cltTickets.IsVisible = false;
            }

        }

        async void GetListTickets(String userID)
        {
            HttpClient httpClient = new HttpClient();

            //HttpResponseMessage result = await httpClient.GetAsync("http://192.168.1.70:3500/ticket/user/6395f9a7976a300647dc7625");

            HttpResponseMessage result = await httpClient.GetAsync("http://192.168.1.70:3500/ticket/user/" + userID);

            if (result.IsSuccessStatusCode)
            {
                var tickets = await result.Content.ReadAsStringAsync();
                List<TicketExp> listTicketConverted = JsonConvert.DeserializeObject<List<TicketExp>>(tickets);

                if (listTicketConverted.Count> 0)
                {
                    foreach (TicketExp ticket in listTicketConverted)
                    {
                        if (ticket.movieName.Length >= 22)
                        {
                            ticket.movieName = ticket.movieName.Substring(0, 22) + "...";
                        }
                    }
                    listTickets = listTicketConverted;
                    active.IsRunning = false;
                    cltTickets.ItemsSource = listTickets;
                }
                else
                {
                    txt_active.IsVisible = true;
                    active.IsRunning = false;
                    cltTickets.IsVisible = false;
                }

            }
            else
            {
                txt_active.IsVisible = true;
                active.IsRunning = false;
                cltTickets.IsVisible = false;
            }
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

        protected override void OnAppearing()
        {
            //Write the code of your page here
            base.OnAppearing();
        }

        private void cltTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cltTickets.SelectedItem != null)
            {
                TicketExp ticket = (TicketExp)cltTickets.SelectedItem;
                Navigation.PushAsync(new Ticket(ticket._id, 0));
            }
        }
    }
}