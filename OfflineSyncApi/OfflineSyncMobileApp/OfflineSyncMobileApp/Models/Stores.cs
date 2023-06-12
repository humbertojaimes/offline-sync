using System;
using OfflineSyncMobileApp.AppBase.Storage;

namespace OfflineSyncMobileApp.Models
{
    public class Store : SQLiteObject
    {

        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }


        private string address;

        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
    }
}

