using CinemaApp.Models;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Input;

namespace CinemaApp.ViewModels
{
    public class ProvinceViewModel
    {
        public List<Cinema> _allCinemas = new List<Cinema>();
        public List<Province> provinces { get; set; } = new List<Province>();

        public ICommand HideOrShow => new Command<Province>((item) =>
        {
            if (item.IsVisible == "icon_up.png")
            {
                item.Clear();
                item.IsVisible = "icon_down.png";
            }
            else
            {
                var add = _allCinemas.Where(f => item.provinceID == f.provinceID).ToList();
                item.AddRange(add);
                item.IsVisible = "icon_up.png";
            }
        });
    }
}
