﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Models
{
    public class TicketExp
    {
        public string _id { get; set; }
        public string movieImage { get; set; }
        public string ratedImage { get; set; } = "icon_p.png";
        public string movieName { get; set; }
        public string cinemaName { get; set; }
        public string seat { get; set; }
        public string foods { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public int totalTicket { get; set; }
        public int totalFood { get; set; }
        public int total { get; set; }
        public int room { get; set; }
        public string id { get; set; }
        public string paymentMethod { get; set; }
    }
}
