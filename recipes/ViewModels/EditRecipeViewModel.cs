using recipes.Models;
using recipes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]
    public class EditRecipeViewModel : BaseViewModel
    {
        //these are the private fields for the properties.
        private string name;

        private string prepareTime;
        private string cookTime;
        private string recipeId;
        private Recipe editedRecipe;

        public ObservableCollection<Ingredient> Ingredients { get; }
        public ObservableCollection<Instruction> Instructions { get; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public Command AddIngredientCommand { get; }
        public Command<Ingredient> DeleteIngredientCommand { get; }

        public Command AddInstructionCommand { get; }
        public Command<Instruction> DeleteInstructionCommand { get; }

        public EditRecipeViewModel()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            Instructions = new ObservableCollection<Instruction>();

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            AddIngredientCommand = new Command(OnAddIngredient);
            DeleteIngredientCommand = new Command<Ingredient>(OnDeleteIngredient);

            AddInstructionCommand = new Command(OnAddInstruction);
            DeleteInstructionCommand = new Command<Instruction>(OnDeleteInstruction);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
            //Ingredients.CollectionChanged +=
            //    (_, __) => SaveCommand.ChangeCanExecute();
        }

        public string RecipeId
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

        public async void LoadRecipeId(string recipeId)
        {
            try
            {
                editedRecipe = await RecipeService.GetRecipe(recipeId);
                Name = editedRecipe.Name;
                PrepareTime = editedRecipe.PrepareTime.ToString();
                CookTime = editedRecipe.CookTime.ToString();

                Ingredients.Clear();

                //TODO get instructions and ingredients
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Recipe");
            }
        }

        private bool ValidateSave()
        {
            int dummy;
            bool validateIngredeints = true;
            foreach (var ingredient in Ingredients)
            {
                validateIngredeints =
                    !string.IsNullOrWhiteSpace(ingredient.Name);

                if (!validateIngredeints) return false;
            }

            foreach (var instruction in Instructions)
            {
                validateIngredeints =
                    !string.IsNullOrWhiteSpace(instruction.Contents);

                if (!validateIngredeints) return false;
            }

            return !string.IsNullOrWhiteSpace(name)
                && Int32.TryParse(prepareTime, out dummy)
                && dummy > 0
                && Int32.TryParse(cookTime, out dummy)
                && dummy > 0
                && validateIngredeints;
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string PrepareTime
        {
            get => prepareTime;
            set => SetProperty(ref prepareTime, value);
        }

        public string CookTime
        {
            get => cookTime;
            set => SetProperty(ref cookTime, value);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private void OnAddIngredient()
        {
            Ingredients.Add(
                new Ingredient()
                {
                    Name = "",
                    Unit = "",
                    RecipeId = recipeId
                });
        }

        private void OnAddInstruction()
        {
            Instructions.Add(
                new Instruction()
                {
                    Contents = "",
                    RecipeId = recipeId
                });
        }

        private void OnDeleteIngredient(Ingredient ingredient)
        {
            if (Ingredients.Contains(ingredient))
            {
                Ingredients.Remove(ingredient);
            }
        }

        private void OnDeleteInstruction(Instruction instruction)
        {
            if (Instructions.Contains(instruction))
            {
                Instructions.Remove(instruction);
            }
        }

        private async void OnSave()
        {
            //validate save
            if (!ValidateSave())
            {
                return;
            }
            else if (RecipeId != null)
            {
                editedRecipe.Name = Name;
                editedRecipe.PrepareTime = Int32.Parse(PrepareTime);
                editedRecipe.CookTime = Int32.Parse(CookTime);

                //this is awful; I need to find a way to not clear and remake the list every time!

                await RecipeService.UpdateRecipe(editedRecipe);
            }
            else
            {
                editedRecipe = new Recipe()
                {
                    Name = Name,
                    PrepareTime = Int32.Parse(PrepareTime),
                    CookTime = Int32.Parse(CookTime),
                };

                await RecipeService.AddRecipe(editedRecipe);
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}