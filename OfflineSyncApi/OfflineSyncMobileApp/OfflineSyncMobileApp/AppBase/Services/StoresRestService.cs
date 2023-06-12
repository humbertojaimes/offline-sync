using System;
using OfflineSyncMobileApp.Implementations;
using OfflineSyncMobileApp.Models;

namespace OfflineSyncMobileApp.AppBase.Services
{
    public class StoresRestService : GenericRestService<Store, Store>
    {
        public StoresRestService()
            : base($"{Constants.ApiConstants.BASE_URI}{Constants.ApiConstants.STORES}")
        {
        }
    }
}

