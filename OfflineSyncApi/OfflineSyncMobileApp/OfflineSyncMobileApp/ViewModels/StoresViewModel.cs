using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OfflineSyncMobileApp.AppBase.Objects;
using OfflineSyncMobileApp.AppBase.Services;
using OfflineSyncMobileApp.AppBase.Storage;
using OfflineSyncMobileApp.Models;
using Xamarin.Forms;

namespace OfflineSyncMobileApp.ViewModels
{
    public class StoresViewModel : BaseViewModel
    {
        public StoresViewModel()
        {
            Title = "Tiendas";

            Sync();
            SyncCommand = new(async () => await Sync());
        }

        async Task Sync()
        {

                var stores = (await SQLiteAsyncClient.Instance.GetAllValuesAsync<Store>());
                Stores = new(stores);
        }

        public Command SyncCommand { get; set; }

        private ObservableCollection<Store> stores;

        public ObservableCollection<Store> Stores
        {
            get => stores;
            set => SetProperty(ref stores, value);
        }

    }
}

