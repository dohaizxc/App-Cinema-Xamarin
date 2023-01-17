using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;

namespace CinemaApp.ViewModels
{
    class SelectSeatViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<Seat> Seats { get; }

        List<Seat> selectedSeats;
        public List<Seat> SelectedSeats
        {
            get { return selectedSeats; }
            set
            {
                if (selectedSeats != value)
                {
                    selectedSeats = value;
                }
            }
        }

        public SelectSeatViewModel(List<Seat> listSeats)
        {
            Seats = new List<Seat>();
            Seats = listSeats;
            SelectedSeats = new List<Seat>();
        }
    }
}
