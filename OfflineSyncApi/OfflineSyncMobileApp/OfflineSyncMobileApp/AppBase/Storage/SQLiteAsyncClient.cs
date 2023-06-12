using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OfflineSyncMobileApp.AppBase.Storage;
using OfflineSyncMobileApp.Models;
using SQLite;

namespace OfflineSyncMobileApp.AppBase.Storage
{
    public class SQLiteAsyncClient
    {

        private static SQLiteAsyncClient instance;

        public static SQLiteAsyncClient Instance
        {
            get
            {
                if (instance is null)
                    instance = new();
                return instance;
            }

        }


        private SQLiteAsyncConnection connection;

        public SQLiteAsyncClient()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"db.db3");

            connection = new(path);

            connection.CreateTableAsync<Product>().Wait();
            connection.CreateTableAsync<Sale>().Wait();
            connection.CreateTableAsync<Store>().Wait();
        }

        public async Task SaveValueAsync<T>(T value, bool overrideIfExists = true)
            where T : IKeyObject, new()
        {
            var item = await connection.FindAsync<T>(value.Id);

            if (item is null)
                await connection.InsertAsync(value);
            else
                if (overrideIfExists)
                await connection.InsertOrReplaceAsync(value);
            else
                throw new Exception($"Element with ID {value.Id} already exists in table");


        }


        public async Task UpdateValueAsync<T>(T value)
            where T : IKeyObject, new() => await connection.UpdateAsync(value);

        public async Task DeleteValueAsync<T>(T value)
            where T : IKeyObject, new() => await connection.DeleteAsync(value);

        public async Task DeleteValueAsync<T>(object id)
            where T : IKeyObject, new() => await connection.DeleteAsync<T>(id);


        public async Task<T> FindValueAsync<T>(object id)
            where T : IKeyObject, new() => await connection.FindAsync<T>(id);

        public async Task<List<T>> GetAllValuesAsync<T>()
            where T : IKeyObject, new() => await connection.Table<T>().ToListAsync();

        public async Task SaveCatalogAsync<T>(IEnumerable<T> values)
            where T : IKeyObject, new()
        {
            await connection.DropTableAsync<T>();
            await connection.CreateTableAsync<T>();
            await connection.InsertAllAsync(values);
        }

        public async Task SaveAllValuesAsync<T>(IEnumerable<T> values)
            where T : IKeyObject, new()
            => await connection.InsertAllAsync(values);

        public async Task UpdateAllValuesAsync<T>(IEnumerable<T> values)
            where T : IKeyObject, new()
            => await connection.UpdateAllAsync(values);

        public async Task DeleteAllValuesAsync<T>(IEnumerable<T> values)
            where T : IKeyObject, new()
        {
            await connection.RunInTransactionAsync((c) =>
            {
                foreach (var item in values)
                {
                    c.Delete(item);
                }
            });
        }
    
    }
}
