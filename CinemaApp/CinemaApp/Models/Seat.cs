using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Models
{
    public class Seat
    {
        public string row { get; set; }
        public int number { get; set; }
        public int id { get; set; }
        public string code => $"{row}{number}";
        public string Available { get; set; }
        public int price { get; set; }
        public bool sweetBoxIsEmpty { get; set; } = false;
        public bool flag { get; set; } = false;
    }
}
