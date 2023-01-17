using CinemaApp.Models;
using CinemaApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Payment : ContentPage
    {
        private int _price;
        private Showtime _showtime;

        String _userID;
        List<Seat> _seatBooking;
        List<Food> _foodBooking;
        String _paymentMethod;

        public Payment()
        {
            InitializeComponent();
        }

        async void FetchAPIBookingTicket(Showtime showtime, String userID, List<Seat> seatBooking, List<Food> foodBooking)
        {
            var client = new HttpClient();
            string jsonData = JsonConvert.SerializeObject(new
            {
                showtime = showtime.showtimeID,
                user = userID,
                seat = seatBooking,
                foods = foodBooking,
                paymentMethod = _paymentMethod
            });
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await client.PostAsync("http://192.168.1.70:3500/ticket", content);

            if (result.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Đặt vé thành công", "OK");
                string json_response = await result.Content.ReadAsStringAsync();

                TicketExp ticketConverted = JsonConvert.DeserializeObject<TicketExp>(json_response);

                await Navigation.PushAsync(new Ticket(ticketConverted._id, 1));

                //await Shell.Current.GoToAsync("//ticket");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Ghế bạn chọn hiện không khả dụng vui lòng chọn lại!", "OK");
                await Navigation.PushAsync(new SeatsShowtime(showtime.showtimeID));
            }
        }


        public Payment(Showtime showtime, int price,List<Seat> seatBooking, List<Food> foodBooking)
        {
            InitializeComponent();


            _userID = Preferences.Get("userID", "default_value");
            _seatBooking = seatBooking;
            _foodBooking = foodBooking;

            Title = showtime.movie.name;
            gridMovie.BindingContext = showtime;
            txtPrice.BindingContext = new PriceViewModel(price);
            _price = price;
            _showtime = showtime;
            lstSeat.HeightRequest = seatBooking.Count*42;

            int totalSeat = 0;
            int totalFood = 0;
            int total = 0;

            foreach (Seat seat in seatBooking)
            {
                totalSeat += seat.price;
            }

            if (foodBooking.Count>0)
            {
                active_foodName.IsVisible = true;
                active_food.IsVisible = true;
                lstFood.HeightRequest = foodBooking.Count * 42;

                foreach (Food food in foodBooking)
                {
                    totalFood += food.price;
                }
                txt_totalFood_active.IsVisible = true;
                txt_totalSeat_active.IsVisible = true;
            }
            total = totalSeat + totalFood;


            txt_totalSeat.BindingContext = totalSeat;
            txt_totalFood.BindingContext = totalFood;
            txt_total.BindingContext = total;

            lstSeat.ItemsSource= seatBooking;
            lstFood.ItemsSource = foodBooking;

            String listSeatString = null;

            if (seatBooking != null && seatBooking.Count > 0)
            {
                listSeatString = seatBooking.Count().ToString() + " ghế";
            }
            txtSeat.BindingContext = new PriceViewModel(price, listSeatString);

            string listFoodString = null;

            if (foodBooking != null && foodBooking.Count > 0)
            {
                listFoodString = ", " + GetQuantity(foodBooking).ToString() + " combo";
            }
            txtFood.BindingContext = new PriceViewModel(_price, listSeatString, listFoodString);

        }

        private int GetQuantity(List<Food> foodBooking)
        {
            int total = 0;
            foreach (Food food in foodBooking)
            {
                if (food.quantity > 0)
                {
                    total += food.quantity;
                }
            }
            return total;
        }

        private async void btnBooking_Clicked(object sender, EventArgs e)
        {

            if (!radioButton1.IsChecked && !radioButton2.IsChecked)
            {
                await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng chọn phương thức thanh toán!", "OK");
            }
            else if (radioButton1.IsChecked)
            {
                _paymentMethod = radioButton1.Content.ToString();
                FetchAPIBookingTicket(_showtime, _userID, _seatBooking, _foodBooking);
            }  
            else if (radioButton2.IsChecked)
            {
                _paymentMethod = radioButton2.Content.ToString();
                FetchAPIBookingTicket(_showtime, _userID, _seatBooking, _foodBooking);
            }
        }
    }
}