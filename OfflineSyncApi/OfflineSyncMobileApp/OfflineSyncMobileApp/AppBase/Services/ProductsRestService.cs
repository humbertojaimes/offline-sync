using System;
using OfflineSyncMobileApp.Implementations;
using OfflineSyncMobileApp.Models;

namespace OfflineSyncMobileApp.AppBase.Services
{
    public class ProductsRestService : GenericRestService<Product, Product>
    {
        public ProductsRestService()
            : base($"{Constants.ApiConstants.BASE_URI}{Constants.ApiConstants.PRODUCTS}")
        {
        }
    }
}

