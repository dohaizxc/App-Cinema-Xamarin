using CinemaApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CinemaApp.ViewModels
{

    public class PriceViewModel : INotifyPropertyChanged
    {
        private int price;
        public int Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                OnPropertyChanged(nameof(price));
            }
        }

        private string listSeatString;
        public string ListSeatString
        {
            get
            {
                return listSeatString;
            }
            set
            {
                listSeatString = value;
                OnPropertyChanged(nameof(listSeatString));
            }
        }

        private String listFoodString;
        public String ListFoodString
        {
            get
            {
                return listFoodString;
            }
            set
            {
                listFoodString = value;
                OnPropertyChanged(nameof(listFoodString));
            }
        }

        public PriceViewModel()
        {
            price = 0;
            listSeatString = null;
        }

        public PriceViewModel(int _price, string _listSeatString)
        {
            price = _price;
            listSeatString = _listSeatString;

        }

        public PriceViewModel(int _price)
        {
            price = _price;
            listSeatString = null;
        }

        public PriceViewModel(int _price, string _listSeatString, string _listFoodString)
        {
            price = _price;
            listSeatString = _listSeatString;
            listFoodString = _listFoodString;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
