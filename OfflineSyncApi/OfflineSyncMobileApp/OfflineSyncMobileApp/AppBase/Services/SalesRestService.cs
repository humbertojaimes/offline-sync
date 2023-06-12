using System;
using OfflineSyncMobileApp.Implementations;
using OfflineSyncMobileApp.Models;

namespace OfflineSyncMobileApp.AppBase.Services
{
    public class SalesRestService : GenericRestService<Sale, Sale>
    {
        public SalesRestService()
            : base($"{Constants.ApiConstants.BASE_URI}{Constants.ApiConstants.SALES}")
        {
        }
    }
}

