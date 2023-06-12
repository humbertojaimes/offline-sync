using System;
using OfflineSyncMobileApp.AppBase.Storage;

namespace OfflineSyncMobileApp.Models
{
    public class Product : SQLiteObject
    {

        private string ean;

        public string Ean
        {
            get => ean;
            set => SetProperty(ref ean, value);
        }

        private string description;

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

    }
}

