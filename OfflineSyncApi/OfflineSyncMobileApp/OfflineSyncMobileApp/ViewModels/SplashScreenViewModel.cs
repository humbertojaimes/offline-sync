using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OfflineSyncMobileApp.AppBase.Objects;
using OfflineSyncMobileApp.AppBase.Services;
using OfflineSyncMobileApp.AppBase.Settings;
using OfflineSyncMobileApp.AppBase.Storage;
using OfflineSyncMobileApp.Models;
using OfflineSyncMobileApp.Views;
using Xamarin.Forms;

namespace OfflineSyncMobileApp.ViewModels
{
    public class SplashScreenViewModel : BaseViewModel
    {
        public SplashScreenViewModel()
        {
            Title = "Carga Inicial";

            LoadData();
        }

        async Task LoadData()
        {
            try
            {
                
                var sales = await SQLiteAsyncClient.Instance.GetAllValuesAsync<Sale>();
                var products = await SQLiteAsyncClient.Instance.GetAllValuesAsync<Product>();
                var stores = await SQLiteAsyncClient.Instance.GetAllValuesAsync<Store>();

                if (!stores.Any())
                {
                    StoresRestService storesRestService = new();
                    var restStores = await storesRestService.GetAll();
                    await SQLiteAsyncClient.Instance.SaveCatalogAsync
                        (restStores.Response);
                }

                if (!products.Any())
                {
                    ProductVersionRestService productVersionRestService
                        = new();

                    var version = await productVersionRestService.Get();

                    ProductsRestService productsRestService
                        = new();

                    var restProducts = await productsRestService.GetAll();
                    await SQLiteAsyncClient.Instance.SaveCatalogAsync
                        (restProducts.Response);
                    UserSettings.ProductVersion = version.Response.Version;

                }
                else
                {
                    ProductVersionRestService productVersionRestService
                        = new();

                    var version = await productVersionRestService.Get();

                    if (UserSettings.ProductVersion != version.Response.Version)
                    {
                        ProductsRestService productsRestService
                        = new();

                        var restProducts = await productsRestService.GetAll();
                        await SQLiteAsyncClient.Instance.SaveCatalogAsync
                            (restProducts.Response);
                        UserSettings.ProductVersion = version.Response.Version;

                    }

                }


                if (!sales.Any())
                {
                    SalesRestService salesRestService = new();
                    var restSales = await salesRestService.GetAll();
                    await SQLiteAsyncClient.Instance.SaveAllValuesAsync
                        (restSales.Response);
                }

                

                

                App.Current.MainPage =
                    new NavigationPage(new MainMenuPage());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }


    }
}

