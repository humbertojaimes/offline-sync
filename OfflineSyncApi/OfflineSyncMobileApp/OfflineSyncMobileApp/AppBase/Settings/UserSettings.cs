using System;
using Xamarin.Essentials;

namespace OfflineSyncMobileApp.AppBase.Settings
{
    public static class UserSettings
    {

        const string productsVersion = "ProductsVersion";

        public static DateTime ProductVersion
        {
            get => Preferences.Get(productsVersion, default(DateTime));
            set => Preferences.Set(productsVersion, value);
        }

    }
}
