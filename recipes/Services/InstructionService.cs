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
    public class InstructionService
    {
        private static SQLiteAsyncConnection db;

        private static async Task Init()
        {
            if (db == null)
            {
                var databasePath = Path.Combine(FileSystem.AppDataDirectory, "recipes.db");

                db = new SQLiteAsyncConnection(databasePath);

                await db.CreateTableAsync<Instruction>();
            }
        }

        public static async Task AddInstruction(Instruction instruction)
        {
            await Init();

            await db.InsertAsync(instruction);
        }

        public static async Task UpdateInstruction(Instruction instruction)
        {
            await Init();

            await db.UpdateAsync(instruction);
        }

        public static async Task DeleteInstruction(int id)
        {
            await Init();

            await db.DeleteAsync<Instruction>(id);
        }

        public static async Task DeleteInstructions(int recipeId)
        {
            await Init();

            await db.ExecuteAsync("DELETE FROM Instruction WHERE RecipeId = ?", recipeId);
        }

        public static async Task<IEnumerable<Instruction>> GetInstructions(int recipeId)
        {
            await Init();

            return await db.Table<Instruction>().Where(ing => ing.RecipeId == recipeId).ToListAsync();
        }
    }
}