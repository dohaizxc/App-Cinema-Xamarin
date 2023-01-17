using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CinemaApp.Models
{
    public class Movie
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string shortName { get; set; }
        public string image { get; set; }
        public string director { get; set; }
        public string actors { get; set; }
        public string[] genre { get; set; }
        public string genreConverter { get; set; }
        public string releaseDate { get; set; }
        public DateTime dateConverter { get; set; }
        public string duration { get; set; }
        public string language { get; set; }
        public string description { get; set; }
        public string rated { get; set; }
        public string trailer_url { get; set; }
        public string ratedImage { get; set; }
        public int stt { get; set; }
    }
}
