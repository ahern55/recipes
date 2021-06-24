using System.Collections.Generic;
using System.Threading.Tasks;

namespace recipes.Services
{
    public interface IDataStore<T>
    {
        Task AddItemAsync(T item);

        Task UpdateItemAsync(T item);

        Task DeleteItemAsync(string id);

        Task<T> GetItemAsync(string id);

        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}