using App1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App1.Services
{
    public interface IDataStore<T>
    {        
        Task<int> UpdateItemAsync(T item);
        Task<T> GetItemAsync(int id);        
        Task<T> GetItemAsync(string id);        
        Task<List<T>> GetItemsAsync(bool forceRefresh = false);        
        Task<int> DeleteItemAsync(int id);
        Task<int> AddItemAsync(T item);
        Task<List<T>> GetItemByIPAsync(string id);



    }
}
