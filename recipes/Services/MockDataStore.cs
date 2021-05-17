using recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipes.Services
{
    public class MockDataStore : IDataStore<Recipe>
    {
        readonly List<Recipe> recipes;

        public MockDataStore()
        {
            recipes = new List<Recipe>()
            {
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "First recipe", Description="This is an recipe description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Second recipe", Description="This is an recipe description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Third recipe", Description="This is an recipe description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Fourth recipe", Description="This is an recipe description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Fifth recipe", Description="This is an recipe description." },
                new Recipe { Id = Guid.NewGuid().ToString(), Text = "Sixth recipe", Description="This is an recipe description." }
            };
        }

        public async Task<bool> AddRecipeAsync(Recipe recipe)
        {
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateRecipeAsync(Recipe recipe)
        {
            var oldRecipe = recipes.Where((Recipe arg) => arg.Id == recipe.Id).FirstOrDefault();
            recipes.Remove(oldRecipe);
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRecipeAsync(string id)
        {
            var oldRecipe = recipes.Where((Recipe arg) => arg.Id == id).FirstOrDefault();
            recipes.Remove(oldRecipe);

            return await Task.FromResult(true);
        }

        public async Task<Recipe> GetRecipeAsync(string id)
        {
            return await Task.FromResult(recipes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(recipes);
        }
    }
}