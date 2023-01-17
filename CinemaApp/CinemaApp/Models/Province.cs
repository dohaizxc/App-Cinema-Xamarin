using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace CinemaApp.Models
{
    public class Province: ObservableRangeCollection<Cinema>, INotifyPropertyChanged
    {
        public string provinceID { get; set; }
        public string provinceName { get; set; }

        private string _isVisible = "icon_down.png";

        public string IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public Province(string provinceID, List<Cinema> cinemas): base(cinemas)
        {
            this.provinceID = provinceID;
        }

        protected virtual bool SetProperty<T>(ref T backingStore,
            T value, [CallerMemberName] string propertyName = null, Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;
            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #region INotifyPropertyChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if(changed == null)
                return;
            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
