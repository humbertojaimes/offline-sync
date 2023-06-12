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
    public class SalesViewModel : BaseViewModel
    {
        public SalesViewModel()
        {
            Title = "Ventas";

            Load();
            SyncCommand = new(async () => await Sync());
        }

        async Task Load()
        {
            var sales = (await SQLiteAsyncClient.Instance.GetAllValuesAsync<Sale>()).OrderBy
                   (s => s.Id);
            Sales = new(sales);
            Total = sales.Count();
        }

        async Task Sync()
        {
            try
            {
                IsBusy = true;
                Stopwatch sw = new ();
                sw.Start();
                var sales = (await SQLiteAsyncClient.Instance.GetAllValuesAsync<Sale>()).OrderBy
                    (s => s.Id);
                Sales = new(sales);
                Total = sales.Count(); 


                List<SaleSync> syncs = new();
                foreach (var sale in sales)
                {
                    syncs.Add(new() { Id = sale.Id, HashId = sale.HashId });
                }
                SaleSyncRestService saleSyncRestService = new();

                var syncResponse = await saleSyncRestService.Post(syncs);


                List<Sale> inserts = new();
                List<Sale> updates = new();
                List<Sale> deletes = new();

                var insertsSync = syncResponse.Response.Where(s => s.OperationType.Equals("INSERT"));

                if(insertsSync.Any())
                   inserts =  insertsSync.Select(sync => new Sale
                (sync.Id, sync.Date, sync.HashId, sync.ProductId.Value, sync.SaleAmount.Value, sync.StoreId.Value)).ToList();

                var deletesSync = syncResponse.Response.Where(s => s.OperationType.Equals("DELETE"));

                if(deletesSync.Any())
                  deletes =  deletesSync.Select(sync => new Sale
                (sync.Id, default,default, default,default,default)).ToList();

                var updatesSync = syncResponse.Response.Where(s => s.OperationType.Equals("UPDATE"));

                if (updatesSync.Any())
                   updates = updatesSync.Select(sync => new Sale
                ( sync.Id,sync.Date,sync.HashId,sync.ProductId.Value, sync.SaleAmount.Value,sync.StoreId.Value)).ToList();


                Inserts = inserts.Count();
                Deletes = deletes.Count();
                Updates = updates.Count();

                if (inserts.Any())
                    await SQLiteAsyncClient.Instance.SaveAllValuesAsync(inserts);
                if(deletes.Any())
                    await SQLiteAsyncClient.Instance.DeleteAllValuesAsync(deletes);
                if(updates.Any())
                    await SQLiteAsyncClient.Instance.UpdateAllValuesAsync(updates);

                sw.Stop();
                Time = sw.Elapsed.ToString();

                sales = (await SQLiteAsyncClient.Instance.GetAllValuesAsync<Sale>()).OrderBy
                    (s => s.Id);
                Sales = new(sales);
                Total = sales.Count();
                
                IsBusy = false;
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public Command SyncCommand { get; set; }

        private ObservableCollection<Sale> sales;

        public ObservableCollection<Sale> Sales
        {
            get => sales;
            set => SetProperty(ref sales, value);
        }

        private int inserts;

        public int Inserts
        {
            get => inserts;
            set => SetProperty(ref inserts, value);
        }

        private int deletes;

        public int Deletes
        {
            get => deletes;
            set => SetProperty(ref deletes, value);
        }


        private int updates;

        public int Updates
        {
            get => updates;
            set => SetProperty(ref updates, value);
        }

        private int total;

        public int Total
        {
            get => total;
            set => SetProperty(ref total, value);
        }

        private string time;

        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

    }
}

