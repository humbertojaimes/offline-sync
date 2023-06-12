using System;
using OfflineSyncMobileApp.Implementations;
using OfflineSyncMobileApp.Models;

namespace OfflineSyncMobileApp.AppBase.Services
{
    public class ProductVersionRestService : GenericRestService<ProductVersion, ProductVersion>
    {
        public ProductVersionRestService()
            : base($"{Constants.ApiConstants.BASE_URI}{Constants.ApiConstants.PRODUCTS_VERSION}")
        {
        }
    }
}

