using CinemaApp.Models;
using CinemaApp.ViewModels;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections;
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
    public partial class SeatsShowtime : ContentPage
    {
        public int _price = 0;
        public Showtime _showtime;

        List<Seat> _seatBooking = new List<Seat>();

        List<Seat> seats = new List<Seat>();

        bool valid = false;

        public SeatsShowtime()
        {
            InitializeComponent();
        }

        public SeatsShowtime(string showtimeID)
        {
            InitializeComponent();
            GetShowtime(showtimeID);

        }

        public void InIt(int[] seatUnavailable, int dateOfWeek)
        {

            int priceSingle = 100000;
            int priceSweetbox = 120000;

            if (dateOfWeek == 0 || dateOfWeek == 6) {
                priceSingle = 120000;
                priceSweetbox = 140000;
            }


            for (int i = 0; i < 90; i++)
            {
                seats.Add(new Seat
                {
                    id = i,
                    row = ((char)(i/10+65)).ToString(),
                    number= i%10,
                    Available = checkAvailable(seatUnavailable, i),
                    price = priceSingle
                });
            }

            for (int i = 90; i < 100; i++)
            {
                seats.Add(new Seat
                {
                    id = i,
                    row = ((char)(i / 10 + 65)).ToString(),
                    number = i % 10,
                    Available = checkAvailable(seatUnavailable, i),
                    price = priceSweetbox
                });
            }
        }


        public string checkAvailable(int[] listSeat, int seatID)
        {
            String color = "lightblue";

            if (seatID >= 90)
            {
                color = "pink";
            }

            if(Array.Exists(listSeat, x => x == seatID))
            {
                color = "gray";
            }
            return color;
        }

        //public String GetColor(int seatColor)
        //{
        //    switch (seatColor)
        //    {
        //        case 1:
        //        default:
        //            return "lightblue";
        //        case 0:
        //            return "gray";
        //        case 2:
        //            return "pink";
        //    }
        //}

        public int GetPrice(List<Seat> listseats)
        {
            int totalPrice = 0;

            if (listseats == null) return 0;

            foreach (Seat seat in listseats)
            {
                totalPrice += seat.price;
            }

            return totalPrice;
        }


        async void GetShowtime(string showtimeID)
        {
            HttpClient httpClient = new HttpClient();

            var showtime = await httpClient.GetStringAsync("http://192.168.1.70:3500/showtime/" + showtimeID);
            Showtime showtimeConverted = JsonConvert.DeserializeObject<Showtime>(showtime);

            DateTime date = DateTime.ParseExact(showtimeConverted.datetime.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            showtimeConverted.datetime = date.ToString("dd/MM/yyyy");

            if (showtimeConverted.movie.name.Length >= 24)
            {
                showtimeConverted.movie.name = showtimeConverted.movie.name.Substring(0, 24) + "...";
            }

            _showtime = showtimeConverted;
            infoSeats.BindingContext = _showtime;


            InIt(showtimeConverted.seats, (int)date.DayOfWeek);
            lstSeats.ItemsSource = seats;
            Title = showtimeConverted.cinemaID;
            txtPrice.BindingContext = new PriceViewModel(0);



        }

        private async void lstSeats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Seat previous = e.PreviousSelection.FirstOrDefault() as Seat;
            //Seat current = e.CurrentSelection.FirstOrDefault() as Seat;

            Seat previous = null;
            Seat current = null;

            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                current = e.CurrentSelection[e.CurrentSelection.Count - 1] as Seat;
            }

            if (e.PreviousSelection != null && e.PreviousSelection.Count > 0)
            {
                previous = e.PreviousSelection[e.PreviousSelection.Count - 1] as Seat;
            }

            if (current?.Available == "gray")
            {
                lstSeats.SelectedItems.Remove(lstSeats.SelectedItems[lstSeats.SelectedItems.Count - 1]);
                return;
            }

            if (lstSeats.SelectedItems.Count > 8)
            {
                lstSeats.SelectedItems.Remove(lstSeats.SelectedItems[lstSeats.SelectedItems.Count - 1]);
                await DisplayAlert("Thông báo", "Bạn chỉ được đặt tối đa 8 ghế", "OK");
                return;
            }

            if (lstSeats.SelectedItems.Count > 7 && current?.Available == "pink")
            {
                if (previous?.Available != "pink")
                {
                    lstSeats.SelectedItems.Remove(lstSeats.SelectedItems[lstSeats.SelectedItems.Count - 1]);
                    await DisplayAlert("Thông báo", "Bạn chỉ được đặt tối đa 8 ghế", "OK");
                    return;
                }
            }

            if (current?.Available == "pink")
            {
                int count;
                if (current.number % 2 == 0)
                    count = current.number + 1;
                else count = current.number - 1;

                var selectedItems = seats.Where(f => f.Available == "pink");

                if (current.sweetBoxIsEmpty == false)
                {

                    current.sweetBoxIsEmpty = true;

                    if (selectedItems != null)
                    {
                        foreach (Seat item in selectedItems)
                        {
                            if (item.number == count && item.sweetBoxIsEmpty == false)
                            {
                                item.sweetBoxIsEmpty = true;
                                lstSeats.SelectedItems.Add(item);
                            }
                        }
                    }
                }
            }

                var selected = lstSeats.SelectedItems;

                int totalPrice = 0;

                string listSeatString = null;

                List<Seat> seatBooking = new List<Seat>();

                if (selected != null && selected.Count > 0)
                {
                    foreach (Seat seat in selected)
                    {
                        seatBooking.Add(seat);
                        totalPrice += seat.price;
                        //if (seat == selected[0])
                        //{
                        //    listSeatString = seat.code;
                        //}
                        //else listSeatString = listSeatString + ", " + seat.code;
                    }
                    valid = true;

                    listSeatString = selected.Count().ToString() + " ghế";
                }
                else valid = false;

                _price = totalPrice;
                _seatBooking = seatBooking;

                txtSeat.BindingContext = new PriceViewModel(totalPrice, listSeatString);
                txtPrice.BindingContext = new PriceViewModel(totalPrice, listSeatString);
        }

        private async void btnBooking_Clicked(object sender, EventArgs e)
        {
            if (valid == false) {
                await DisplayAlert("Thông báo", "Vui lòng chọn ghế để tiếp tục", "OK");
            }
            else await Navigation.PushAsync(new BookingFood(_showtime, _price, _seatBooking));
        }
    }
}