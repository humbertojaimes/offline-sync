using System;
using OfflineSyncMobileApp.AppBase.Objects;

namespace OfflineSyncMobileApp.Models
{
    public class ProductVersion : ObservableObject
    {

        private DateTime version;

        public DateTime Version
        {
            get => version;
            set => SetProperty(ref version, value);
        }
    }
}

