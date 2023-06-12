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
    public class ProductsViewModel : BaseViewModel
    {
        public ProductsViewModel()
        {
            Title = "Ventas";
            Load();
        }

        async Task Load()
        {
            IsBusy = true;
            var products = (await SQLiteAsyncClient.Instance.GetAllValuesAsync<Product>()).OrderBy
                   (s => s.Id);
            Products = new(products);
            Total = products.Count();
            IsBusy = false;
        }

        

        public Command SyncCommand { get; set; }

        private ObservableCollection<Product> products;

        public ObservableCollection<Product> Products
        {
            get => products;
            set => SetProperty(ref products, value);
        }

        private int total;

        public int Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }
    }
}

