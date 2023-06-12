using System;
using OfflineSyncMobileApp.AppBase.Objects;
using OfflineSyncMobileApp.Views;
using Xamarin.Forms;

namespace OfflineSyncMobileApp.ViewModels
{
    public class MainMenuViewModel :BaseViewModel
    {
        public MainMenuViewModel()
        {
            Title = "Menú Principal";
            ProductsCommand = new( async ()
            => await Application.Current.MainPage.Navigation
            .PushAsync(new ProductsPage()));

            SalesCommand = new(async ()
           => await Application.Current.MainPage.Navigation
           .PushAsync(new SalesPage()));

            StoresCommand = new(async ()
           => await Application.Current.MainPage.Navigation
           .PushAsync(new StoresPage()));

        }



        public Command ProductsCommand { get; set; }

        public Command SalesCommand { get; set; }

        public Command StoresCommand { get; set; }
    }
}

