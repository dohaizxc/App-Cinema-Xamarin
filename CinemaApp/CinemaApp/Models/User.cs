using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaApp.Models
{
    public class User
    {
        public String _id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public DateTime dayOfBirth { get; set; }
        public string gender { get; set; }
        public string name { get; set; }
        public int total { get; set; }
        public string accessToken { get; set; }
    }
}
