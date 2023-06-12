using System;
using System.Collections.Generic;
using OfflineSyncMobileApp.Implementations;
using OfflineSyncMobileApp.Models;

namespace OfflineSyncMobileApp.AppBase.Services
{
    public class SaleSyncRestService : GenericRestService<IEnumerable<SaleSync>, IEnumerable<SaleSyncChange>>
    {
        public SaleSyncRestService()
            :base($"{Constants.ApiConstants.BASE_URI}{Constants.ApiConstants.SALES_SYNC}")
        {
        }
    }
}

