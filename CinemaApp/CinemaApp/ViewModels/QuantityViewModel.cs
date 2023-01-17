using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CinemaApp.ViewModels
{
    public class QuantityViewModel : INotifyPropertyChanged
    {
        private int Quantity = 0;
        public int quantity
        {
            get
            {
                return Quantity;
            }
            set
            {
                Quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public QuantityViewModel()
        {
            // some initialization code here ...
            Quantity = 0;
        }

        public QuantityViewModel(int _quantity)
        {
            // some initialization code here ...
            Quantity = _quantity;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
