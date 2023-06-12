using System;
using OfflineSyncMobileApp.AppBase.Objects;
using OfflineSyncMobileApp.AppBase.Storage;

namespace OfflineSyncMobileApp.Models
{
    public class SaleSyncChange :ObservableObject
    {

        public int Id { get; set; }

        private DateTime date;

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        private int? storeId;

        public int? StoreId
        {
            get => storeId;
            set => SetProperty(ref storeId, value);
        }

        private int? productId;

        public int? ProductId
        {
            get => productId;
            set => SetProperty(ref productId, value);
        }

        private int? saleAmount;

        public int? SaleAmount
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

        private string operationType;

        public string OperationType
        {
            get => operationType;
            set => SetProperty(ref operationType, value);
        }

    }
}

