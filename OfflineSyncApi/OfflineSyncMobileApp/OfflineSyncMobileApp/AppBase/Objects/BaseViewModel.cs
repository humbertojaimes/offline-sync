using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace OfflineSyncMobileApp.AppBase.Objects
{
    public class BaseViewModel : ObservableObject
    {



        private string title;

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }


        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

    }
}
