using CinemaApp.Models;
using CinemaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookingFood : ContentPage
    {
        private int _price;

        string listSeatString = null;

        private Showtime _showtime;
        ObservableCollection<Food> listFood = new ObservableCollection<Food>();

        List<Seat> _seatBooking = new List<Seat>();

        List<Food> _foodBooking = new List<Food>();
        public BookingFood(Showtime showtime, int price, List<Seat> seatBooking)
        {
            InitializeComponent();
            listFood = Food.InitializeFood();
            lstFood.ItemsSource = listFood;
            Title = "Mua bỏng nước";
            gridMovie.BindingContext = showtime;
            _showtime = showtime;
            txtPrice.BindingContext = new PriceViewModel(price);
            _price = price;
            _seatBooking = seatBooking;

            if (seatBooking != null && seatBooking.Count > 0)
            {
                listSeatString = seatBooking.Count().ToString() + " ghế";
            }
            txtSeat.BindingContext = new PriceViewModel(price, listSeatString);
        }

        private List<Food> GetListFood(ObservableCollection<Food> cltFood)
        {
            List<Food> foodBooking = new List<Food>();
            foreach (Food food in cltFood)
            {
                if (food.quantity > 0)
                {
                    foodBooking.Add(food);
                }
            }
            return foodBooking;
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

        private void RefreshCollectionView()
        {
            lstFood.ItemsSource = null;
            lstFood.ItemsSource = listFood;
        }

        private void btnBooking_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Payment(_showtime, _price, _seatBooking, _foodBooking));
        }

        private void btnMinus_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Food food = button.CommandParameter as Food;
            if (food.quantity>0)
            {
                food.quantity -= 1;
                _price = _price - food.price;
                txtPrice.BindingContext = new PriceViewModel(_price);

                listFood.Where<Food>(f => f.foodID == food.foodID).First().quantity = food.quantity;
                _foodBooking = null;
                _foodBooking = GetListFood(listFood);

                string listFoodString = null;

                if (_foodBooking != null && _foodBooking.Count > 0)
                {
                    listFoodString = ", " + GetQuantity(_foodBooking).ToString() + " combo";
                }
                txtFood.BindingContext = new PriceViewModel(_price, listSeatString, listFoodString);


                RefreshCollectionView();
            }
            
        }

        private void btnPlus_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Food food = button.CommandParameter as Food;
            if (food.quantity < 9)
            {
                food.quantity += 1;
                _price = _price + food.price;
                txtPrice.BindingContext = new PriceViewModel(_price);
                listFood.Where<Food>(f => f.foodID == food.foodID).First().quantity = food.quantity;
                _foodBooking = null;
                _foodBooking = GetListFood(listFood);

                string listFoodString = null;

                if (_foodBooking != null && _foodBooking.Count > 0)
                {
                    listFoodString = ", " + GetQuantity(_foodBooking).ToString() + " combo";
                }
                txtFood.BindingContext = new PriceViewModel(_price, listSeatString, listFoodString);


                RefreshCollectionView();
            }
        }
    }
}