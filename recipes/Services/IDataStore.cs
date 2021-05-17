using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace recipes.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddRecipeAsync(T recipe);
        Task<bool> UpdateRecipeAsync(T recipe);
        Task<bool> DeleteRecipeAsync(string id);
        Task<T> GetRecipeAsync(string id);
        Task<IEnumerable<T>> GetRecipesAsync(bool forceRefresh = false);
    }
}
