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
    public class RecipeService : IDataStore<Recipe>
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

        public async Task AddItemAsync(Recipe recipe)
        {
            await Init();

            await db.InsertAsync(recipe);
        }

        public async Task UpdateItemAsync(Recipe recipe)
        {
            await Init();

            await db.UpdateAsync(recipe);
        }

        public async Task DeleteItemAsync(string id)
        {
            await Init();

            await db.DeleteAsync<Recipe>(id);
        }

        public async Task<Recipe> GetItemAsync(string id)
        {
            await Init();

            return await db.GetAsync<Recipe>(id);
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            await Init();

            return await db.Table<Recipe>().ToListAsync();
        }
    }
}