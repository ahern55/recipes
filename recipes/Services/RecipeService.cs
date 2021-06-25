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
    public class RecipeService
    {
        private static SQLiteAsyncConnection db;

        private static async Task Init()
        {
            if (db == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");

                db = new SQLiteAsyncConnection(databasePath);

                await db.CreateTableAsync<Recipe>();
            }
        }

        public static async Task AddRecipe(Recipe recipe)
        {
            await Init();

            await db.InsertAsync(recipe);
        }

        public static async Task UpdateRecipe(Recipe recipe)
        {
            await Init();

            await db.UpdateAsync(recipe);
        }

        public static async Task DeleteRecipe(int id)
        {
            await Init();

            await db.DeleteAsync<Recipe>(id);
        }

        public static async Task<Recipe> GetRecipe(int id)
        {
            await Init();

            return await db.GetAsync<Recipe>(id);
        }

        public static async Task<IEnumerable<Recipe>> GetRecipes(bool forceRefresh = false)
        {
            await Init();

            return await db.Table<Recipe>().ToListAsync();
        }
    }
}