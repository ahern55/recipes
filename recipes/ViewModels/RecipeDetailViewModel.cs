using recipes.Models;
using recipes.Services;
using recipes.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]
    public class RecipeDetailViewModel : BaseViewModel
    {
        private int recipeId;
        private string name;
        private string description;
        private int prepareTime;
        private int cookTime;
        public int Id { get; set; }

        public ObservableCollection<Ingredient> Ingredients { get; }
        public ObservableCollection<Instruction> Instructions { get; }
        public Command EditRecipeCommand { get; }
        public Command DeleteRecipeCommand { get; }

        public RecipeDetailViewModel()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            Instructions = new ObservableCollection<Instruction>();
            EditRecipeCommand = new Command(OnEditRecipe);
            DeleteRecipeCommand = new Command(OnDeleteRecipe);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public int PrepareTime
        {
            get => prepareTime;
            set => SetProperty(ref prepareTime, value);
        }

        public int CookTime
        {
            get => cookTime;
            set => SetProperty(ref cookTime, value);
        }

        public int TotalTime => PrepareTime + CookTime;

        public int RecipeId
        {
            get
            {
                return recipeId;
            }
            set
            {
                recipeId = value;
                LoadRecipeId(value);
            }
        }

        public async void LoadRecipeId(int recipeId)
        {
            try
            {
                Recipe recipe = await RecipeService.GetRecipe(recipeId);
                Id = recipe.Id;
                Name = recipe.Name;
                PrepareTime = recipe.PrepareTime;
                CookTime = recipe.CookTime;

                Ingredients.Clear();
                var ingredientsList = await IngredientService.GetIngredients(recipeId);
                foreach (var ingredient in ingredientsList)
                {
                    Ingredients.Add(ingredient);
                }

                Instructions.Clear();
                var instructionsList = await InstructionService.GetInstructions(recipeId);
                foreach (var instruction in instructionsList)
                {
                    Instructions.Add(instruction);
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Recipe");
            }
        }

        private async void OnEditRecipe(object obj)
        {
            //go to the edit recipe page, passing the recipe ID since this recipe already exists
            await Shell.Current.GoToAsync($"{nameof(EditRecipePage)}?{nameof(EditRecipeViewModel.RecipeId)}={Id}");
        }

        private async void OnDeleteRecipe(object obj)
        {
            if (await Application.Current.MainPage.DisplayAlert("Confirm Deletion", $"Are you sure you'd like to delete {name}?", "Delete", "Cancel"))
            {
                await RecipeService.DeleteRecipe(RecipeId);
                await IngredientService.DeleteIngredients(RecipeId);
                await InstructionService.DeleteInstructions(RecipeId);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}