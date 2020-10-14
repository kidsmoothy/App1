using App1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App1.Services
{
    public class CPAutomationsDatabase : IDataStore<ControllerInfoItem>
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public CPAutomationsDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }
        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ControllerInfoItem).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ControllerInfoItem)).ConfigureAwait(false);
                }

                initialized = true;
            }
        }

        //public Task<List<ControllerInfoItem>> GetItemsAsync()
        //{
        //    return Database.Table<ControllerInfoItem>().ToListAsync();
        //}

        public Task<ControllerInfoItem> GetItemAsync(int id)        
        {
            return Database.Table<ControllerInfoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<ControllerInfoItem> GetItemAsync(string id)
        {
            var intId = Int32.Parse(id);
            return Database.Table<ControllerInfoItem>().Where(i => i.Id == intId).FirstOrDefaultAsync();
        }     
               
        public Task<int> UpdateItemAsync(ControllerInfoItem item)
        {
            return Database.UpdateAsync(item);
        }

        public Task<int> AddItemAsync(ControllerInfoItem item)
        {
            return Database.InsertAsync(item);
        }
        
        public Task<int> DeleteItemAsync(int id)
        {
            return Database.Table<ControllerInfoItem>().DeleteAsync(x => x.Id == id);            
        }

        public Task<List<ControllerInfoItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return Database.Table<ControllerInfoItem>().ToListAsync();
        }

        public Task<List<ControllerInfoItem>> GetItemByIPAsync(string id)
        {
            return Database.Table<ControllerInfoItem>().Where(i => i.IPAddress == id).ToListAsync();
        }
    }
}
