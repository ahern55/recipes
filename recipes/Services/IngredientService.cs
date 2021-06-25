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
            if (db == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");

                db = new SQLiteAsyncConnection(databasePath);

                await db.CreateTableAsync<Ingredient>();
            }
        }

        public static async Task AddIngredient(Ingredient ingredient)
        {
            await Init();

            await db.InsertAsync(ingredient);
        }

        public static async Task UpdateIngredient(Ingredient ingredient)
        {
            await Init();

            await db.UpdateAsync(ingredient);
        }

        public static async Task DeleteIngredient(int id)
        {
            await Init();

            await db.DeleteAsync<Ingredient>(id);
        }

        public static async Task DeleteIngredients(int recipeId)
        {
            await Init();

            await db.ExecuteAsync("DELETE FROM Ingredient WHERE RecipeId = ?", recipeId);
        }

        public static async Task<IEnumerable<Ingredient>> GetIngredients(int recipeId)
        {
            await Init();

            return await db.Table<Ingredient>().Where(ing => ing.RecipeId == recipeId).ToListAsync();
        }
    }
}