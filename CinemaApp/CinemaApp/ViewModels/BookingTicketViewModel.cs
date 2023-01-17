using CinemaApp.Models;
using CinemaApp.Views;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.ViewModels
{
    public class BookingTicketViewModel : BaseViewModel
    {
        private string movieName;
        private string image;
        private string time;
        private string money;
        private string duration;
        public string MovieName
        {
            get => movieName;
            set => SetProperty(ref movieName, value);
        }

        public string Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public string Money
        {
            get => money;
            set => SetProperty(ref money, value);
        }

        public string Duration
        {
            get => duration;
            set => SetProperty(ref duration, value);
        }
        public Command SignUpCommand { get; }

        public BookingTicketViewModel()
        {
            SignUpCommand = new Command(OnBookingTicketClicked);
        }

        private async void OnBookingTicketClicked()
        {

        }
    }
}
