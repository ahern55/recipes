using recipes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace recipes.Services
{
    public class IngredientService
    {
        private static SQLiteAsyncConnection db;

        private static async Task Init()
        {
            if (db != null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");

                db = new SQLiteAsyncConnection(databasePath);

                await db.CreateTableAsync<Recipe>();
            }
        }

        //public async Task AddItemAsync(Ingredient recipe)
        //{
        //    await Init();

        //    await db.InsertAsync(recipe);
        //}

        //public async Task UpdateItemAsync(Ingredient recipe)
        //{
        //    await Init();

        //    await db.UpdateAsync(recipe);
        //}

        //public async Task DeleteItemAsync(string id)
        //{
        //    await Init();

        //    await db.DeleteAsync<Recipe>(id);
        //}

        //public async Task<Ingredient> GetItemAsync(string id)
        //{
        //    await Init();

        //    return await db.GetAsync<Recipe>(id);
        //}

        //public async Task<IEnumerable<Ingredient>> GetItemsAsync(bool forceRefresh = false)
        //{
        //    await Init();

        //    return await db.Table<Recipe>().ToListAsync();
        //}
    }
}