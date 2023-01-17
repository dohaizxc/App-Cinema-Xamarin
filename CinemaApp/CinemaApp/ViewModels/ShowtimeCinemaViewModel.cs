using CinemaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CinemaApp.ViewModels
{
    public class ShowtimeCinemaViewModel
    {
        public string WeekName { get; set; }
        public string Date { get; set; }
        public DateTime Dtime { get; set; }
        public String Color { get; set; } = "White";
    }

    public class CalendarModel
    {
        public List<Showtime> allshowtime = new List<Showtime>();

        public List<CinemaGroup> _cinemas { get; set; } = new List<CinemaGroup>();
        public List<ShowtimeCinemaViewModel> GetDates()
        {
            List<ShowtimeCinemaViewModel> list = new List<ShowtimeCinemaViewModel>();
            DateTime startDate = DateTime.Now.AddDays(0);
            //DateTime startDate = new DateTime(2022, 12, 10);
            DateTime endDate = DateTime.Now.AddDays(10);
            //DateTime endDate = startDate.AddDays(10);

            list.Add(new ShowtimeCinemaViewModel { Date = startDate.ToString("dd"), Color = "DeepSkyBlue", Dtime = startDate, WeekName = DayOfWeekVietNam((int)startDate.DayOfWeek) });
            for (DateTime date = startDate.AddDays(1); date <= endDate; date = date.AddDays(1))
            {
                list.Add(new ShowtimeCinemaViewModel { Date = date.ToString("dd"), Dtime = date, WeekName = DayOfWeekVietNam((int)date.DayOfWeek) });

            }
            return list;
        }

        private string DayOfWeekVietNam(int date)
        {

            switch (date)
            {
                case 0:
                    return "CN";
                case 1:
                    return "Th2";
                case 2:
                    return "Th3";
                case 3:
                    return "Th4";
                case 4:
                    return "Th5";
                case 5:
                    return "Th6";
                case 6:
                    return "Th7";
                default:
                    return "Err";
            }
        }
    }


}
