using System;
using OfflineSyncMobileApp.AppBase.Objects;
using SQLite;

namespace OfflineSyncMobileApp.AppBase.Storage
{
    public class SQLiteObject : ObservableObject, IKeyObject
    {
        [PrimaryKey]
        public int Id { get; set; }

    }
}
