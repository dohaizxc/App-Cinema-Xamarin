using CinemaApp.Models;
using CinemaApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ZXing.QrCode.Internal.Mode;

namespace CinemaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowTimeCinema : ContentPage
    {
        DateTime calendarDate = DateTime.Now;

        CalendarModel calendarModel = new CalendarModel();

        String nameTitle;

        int _type;

        List<ShowtimeCinemaViewModel> calendar = new List<ShowtimeCinemaViewModel>();


        String ID;

        public ShowTimeCinema(Cinema cinema, int type)
        {
            Title = cinema.cinemaName;
            nameTitle = cinema.cinemaName;
            _type = type;
            InitializeComponent();
            String getDate = calendarDate.ToString("MM/dd/yyyy").Replace('/', '-');
            lblMonthYear.Text = DayOfWeekVietNam((int)calendarDate.DayOfWeek) + ", ngày " + calendarDate.ToString("dd")
                                + " tháng " + calendarDate.ToString("MM") +" năm " + calendarDate.ToString("yyyy");
            //lblMonthYear.Text = calendarDate.ToString("dd") + " " + calendarDate.ToString("MMM") + " " + calendarDate.ToString("yyyy");
            calendarModel.GetDates();
            calendar = calendarModel.GetDates();
            cvShowtimeCinema.ItemsSource = calendar;
            ID = cinema.cinemaID;
            getListShowtimes(calendarModel, type, getDate, ID);
            //active.IsRunning = false;
        }

        public ShowTimeCinema(Movie movie, int type)
        {
            Title = movie.name;
            nameTitle = movie.name;
            _type = type;
            InitializeComponent();
            lblMonthYear.Text = DayOfWeekVietNam((int)calendarDate.DayOfWeek) + ", ngày " + calendarDate.ToString("dd")
                    + " tháng " + calendarDate.ToString("MM") + " năm " + calendarDate.ToString("yyyy");
            calendar = calendarModel.GetDates();
            cvShowtimeCinema.ItemsSource = calendar;
            String getDate = calendarDate.ToString("MM/dd/yyyy").Replace('/', '-');
            ID = movie._id;
            getListShowtimes(calendarModel, type, getDate, ID);
            //active.IsRunning = false;
        }

        private string DayOfWeekVietNam(int date)
        {

            switch (date)
            {
                case 0:
                    return "Chủ Nhật";
                case 1:
                    return "Thứ Hai";
                case 2:
                    return "Thứ Ba";
                case 3:
                    return "Thứ Tư";
                case 4:
                    return "Thứ Năm";
                case 5:
                    return "Thứ Sáu";
                case 6:
                    return "Thứ Bảy";
                default:
                    return "Err";
            }
        }


        async void getListShowtimes(CalendarModel listShowtimes, int type, string date, string ID)
        {
            HttpClient httpClient = new HttpClient();

            if (type == 1)
            {
                var listCinema = await httpClient.GetStringAsync("http://192.168.1.70:3500/showtime/cinema2/" + ID + "/" + date);
                List<Showtime> listMovieConverted = JsonConvert.DeserializeObject<List<Showtime>>(listCinema);

                foreach (Showtime showtime in listMovieConverted)
                {
                    DateTime dateConverter = DateTime.ParseExact(showtime.datetime.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    showtime.datetime = dateConverter.ToString("dd/MM/yyyy");
                }
                listShowtimes.allshowtime.Clear();
                listShowtimes._cinemas.Clear();
                listShowtimes.allshowtime.AddRange(listMovieConverted.Where(f => f.cinemaID == nameTitle));
                var groupData = listShowtimes.allshowtime.GroupBy(f => f.movie.name).Select(f => new CinemaGroup(f.Key, f.ToList()));
                listShowtimes._cinemas.AddRange(groupData);
            }
            else
            {
                var listCinema = await httpClient.GetStringAsync("http://192.168.1.70:3500/showtime/movie2/" + ID + "/" + date);
                List<Showtime> listMovieConverted = JsonConvert.DeserializeObject<List<Showtime>>(listCinema);
                foreach (Showtime showtime in listMovieConverted)
                {
                    DateTime dateConverter = DateTime.ParseExact(showtime.datetime.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    showtime.datetime = dateConverter.ToString("dd/MM/yyyy");
                }
                listShowtimes.allshowtime.Clear();
                listShowtimes._cinemas.Clear();
                listShowtimes.allshowtime.AddRange(listMovieConverted.Where(f => f.movie.name == nameTitle));
                var groupData = listShowtimes.allshowtime.GroupBy(f => f.cinemaID).Select(f => new CinemaGroup(f.Key, f.ToList()));
                listShowtimes._cinemas.AddRange(groupData);
            }

            BindingContext = listShowtimes;
            active.IsRunning = false;
            if (listShowtimes._cinemas.Count < 1)
            {
                txt_active.IsVisible = true;
            }
        }

        private async void cvCinemas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if (cvCinemas.SelectedItem != null)
                {
                    Showtime showtime = (Showtime)cvCinemas.SelectedItem;

                foreach (Showtime _showtime in calendarModel.allshowtime)
                {
                    _showtime.Color = "White";
                }

                showtime.Color = "LightBlue";

                BindingContext = null;
                BindingContext = calendarModel;


                String userID = Preferences.Get("userID", "default_value");

                if (userID != "default_value")
                {
                    await Navigation.PushAsync(new SeatsShowtime(showtime.showtimeID));
                    cvCinemas.SelectedItem = null;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Thông báo", "Vui lòng đăng nhập để tiếp tục!", "OK");
                    await Navigation.PushAsync(new LogInPage());
                    cvCinemas.SelectedItem = null;
                }

                }
            }

        private void cvShowtimeCinema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txt_active.IsVisible = false;
            active.IsRunning = true;
            if (cvShowtimeCinema.SelectedItem != null) {
                ShowtimeCinemaViewModel date = (ShowtimeCinemaViewModel)cvShowtimeCinema.SelectedItem;

                foreach (ShowtimeCinemaViewModel _calendar in calendar)
                {
                    _calendar.Color = "White";
                }

                date.Color = "DeepSkyBlue";

                cvShowtimeCinema.ItemsSource = null;
                cvShowtimeCinema.ItemsSource = calendar;


                String getDate = date.Dtime.ToString("MM/dd/yyyy").Replace('/', '-');
                BindingContext = null;
                getListShowtimes(calendarModel, _type, getDate, ID);
                //getListShowtimes(calendarModel, _type, "12-11-2022");

            }
        }
    }
    }