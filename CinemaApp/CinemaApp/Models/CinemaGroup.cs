using CinemaApp.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CinemaApp.Models
{
    public class CinemaGroup: ObservableRangeCollection<Showtime>
    {
        public string movieID { get; set; }
        public string cinemaName { get; set; }
        public string provinceID { get; set; }
        public string address { get; set; }
        public string ratedImage { get; set; } = "icon_p.png";

        public CinemaGroup(string movieID, List<Showtime> showtimes) : base(showtimes)
        {
            this.movieID = movieID;
        }

    }
}