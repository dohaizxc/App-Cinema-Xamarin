using CinemaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Models
{
    public class Showtime
    {
        public String showtimeID { get; set; }
        public String time { get; set; }
        public String timeEnd { get; set; }
        public String cinemaID { get; set; }
        public String datetime { get; set; }
        public int room { get; set; }
        public int[] seats { get; set; }
        public Movie movie { get; set; }
        public String Color { get; set; } = "White";

    }
}
