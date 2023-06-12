using System;
using OfflineSyncMobileApp.AppBase.Storage;

namespace OfflineSyncMobileApp.Models
{
    public class Sale : SQLiteObject
    {
        
        private DateTime date;

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        private int storeId;

        public int StoreId
        {
            get => storeId;
            set => SetProperty(ref storeId, value);
        }

        private int productId;

        public int ProductId
        {
            get => productId;
            set => SetProperty(ref productId, value);
        }

        private int saleAmount;

        public int SaleAmount
        {
            get => saleAmount;
            set => SetProperty(ref saleAmount, value);
        }

        private string hashId;

        public string HashId
        {
            get => hashId;
            set => SetProperty(ref hashId, value);
        }

        public Sale()
        {

        }

        public Sale(int id, DateTime date, string hashId,int productId, int saleAmount, int storeId)
        {
            Id = id;
            Date = date;
            HashId = hashId;
            ProductId = productId;
            SaleAmount = saleAmount;
            StoreId = storeId;
        }

    }
}

