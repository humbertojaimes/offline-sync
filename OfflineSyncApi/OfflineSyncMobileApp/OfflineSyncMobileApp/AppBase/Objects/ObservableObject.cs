using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OfflineSyncMobileApp.AppBase.Objects
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        protected bool SetProperty<T>(ref T backing, T value,
            [CallerMemberName] string propertyName = "")
        {

            if (EqualityComparer<T>.Default.Equals(backing, value)) return false;

            backing = value;
            OnPropertyChanged(propertyName);

            return true;
        }


    }
}
