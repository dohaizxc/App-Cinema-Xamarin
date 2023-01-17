using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace CinemaApp.Models
{
    public class Cinema
    {
        public string cinemaID { get; set; }
        public string cinemaName { get; set; }
        public string provinceID { get; set; }
        public string address { get; set; }
        public string address_url { get; set; }
        public string Color { get; set; } = "White";
    }
}