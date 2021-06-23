using recipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipes.Services
{
    public class RecipeService : IDataStore<Recipe>
    {
        private readonly List<Recipe> recipes;
        private readonly Ingredient meatballs;
        private readonly Ingredient corn;
        private readonly Instruction one;
        private readonly Instruction two;

        public RecipeService()
        {
            meatballs = new Ingredient
            {
                RecipeId = "0",
                Name = "Meatballs",
                Amount = "2",
                Unit = "cups",
                Index = 1
            };

            corn = new Ingredient
            {
                RecipeId = "0",
                Name = "Corn",
                Amount = "10",
                Unit = "tbsp",
                Index = 2
            };

            one = new Instruction
            {
                Contents = "cut meatballs",
                RecipeId = "0"
            };

            two = new Instruction
            {
                Contents = "boil corn",
                RecipeId = "0"
            };

            recipes = new List<Recipe>()
            {
                new Recipe { Id = Guid.NewGuid().ToString(), Name = "First recipe", Description="This is an recipe description.", PrepareTime = 5, CookTime = 6,
                IngredientsList = new List<Ingredient> {meatballs, corn},
                InstructionList = new List<Instruction> {one, two}},
                new Recipe { Id = Guid.NewGuid().ToString(), Name = "Second recipe", Description="This is an recipe description.", PrepareTime = 5, CookTime = 6 },
                new Recipe { Id = Guid.NewGuid().ToString(), Name = "Third recipe", Description="This is an recipe description.", PrepareTime = 5, CookTime = 6 },
                new Recipe { Id = Guid.NewGuid().ToString(), Name = "Fourth recipe", Description="This is an recipe description.", PrepareTime = 5, CookTime = 6 },
                new Recipe { Id = Guid.NewGuid().ToString(), Name = "Fifth recipe", Description="This is an recipe description.", PrepareTime = 5, CookTime = 6 },
                new Recipe { Id = Guid.NewGuid().ToString(), Name = "Sixth recipe", Description="This is an recipe description.", PrepareTime = 5, CookTime = 6 }
            };
        }

        public async Task<bool> AddItemAsync(Recipe recipe)
        {
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Recipe recipe)
        {
            var oldRecipe = recipes.Where((Recipe arg) => arg.Id == recipe.Id).FirstOrDefault();
            recipes.Remove(oldRecipe);
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldRecipe = recipes.Where((Recipe arg) => arg.Id == id).FirstOrDefault();
            recipes.Remove(oldRecipe);

            return await Task.FromResult(true);
        }

        public async Task<Recipe> GetItemAsync(string id)
        {
            return await Task.FromResult(recipes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(recipes);
        }
    }
}