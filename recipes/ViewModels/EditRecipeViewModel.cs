﻿using recipes.Models;
using recipes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private int recipeId;
        private bool isNewRecipe = true;

        private Recipe editedRecipe;

        public ObservableCollection<Ingredient> Ingredients { get; }
        public ObservableCollection<Ingredient> DeletedIngredients { get; }

        public ObservableCollection<Instruction> Instructions { get; }
        public ObservableCollection<Instruction> DeletedInstructions { get; }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public Command AddIngredientCommand { get; }
        public Command<Ingredient> DeleteIngredientCommand { get; }

        public Command AddInstructionCommand { get; }
        public Command<Instruction> DeleteInstructionCommand { get; }

        public EditRecipeViewModel()
        {
            editedRecipe = new Recipe();

            Ingredients = new ObservableCollection<Ingredient>();
            DeletedIngredients = new ObservableCollection<Ingredient>();

            Instructions = new ObservableCollection<Instruction>();
            DeletedInstructions = new ObservableCollection<Instruction>();

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

        public async void LoadRecipeId(int recipeId)
        {
            try
            {
                isNewRecipe = false;

                editedRecipe = await RecipeService.GetRecipe(recipeId);
                Name = editedRecipe.Name;
                PrepareTime = editedRecipe.PrepareTime.ToString();
                CookTime = editedRecipe.CookTime.ToString();

                Ingredients.Clear();
                var ingredientsList = await IngredientService.GetIngredients(RecipeId);
                foreach (var ingredient in ingredientsList)
                {
                    Ingredients.Add(ingredient);
                }

                Instructions.Clear();
                var instructionsList = await InstructionService.GetInstructions(RecipeId);
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

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private void OnAddIngredient()
        {
            Ingredients.Add(new Ingredient()
            {
                Name = "",
                Unit = "",
                isNewIngredient = true
            });
        }

        private void OnAddInstruction()
        {
            Instructions.Add(new Instruction()
            {
                Contents = "",
                isNewInstruction = true
            });
        }

        private void OnDeleteIngredient(Ingredient ingredient)
        {
            if (Ingredients.Contains(ingredient))
            {
                Ingredients.Remove(ingredient);
                if (!ingredient.isNewIngredient)
                {
                    DeletedIngredients.Add(ingredient);
                }
            }
        }

        private void OnDeleteInstruction(Instruction instruction)
        {
            if (Instructions.Contains(instruction))
            {
                Instructions.Remove(instruction);
                if (!instruction.isNewInstruction)
                {
                    DeletedInstructions.Add(instruction);
                }
            }
        }

        private async void OnSave()
        {
            //validate save
            if (!ValidateSave())
            {
                return;
            }
            else
            {
                editedRecipe.Name = Name;
                editedRecipe.PrepareTime = Int32.Parse(PrepareTime);
                editedRecipe.CookTime = Int32.Parse(CookTime);

                if (isNewRecipe)
                {
                    await RecipeService.AddRecipe(editedRecipe);
                }
                else
                {
                    await RecipeService.UpdateRecipe(editedRecipe);
                }
            }

            foreach (var ingredient in Ingredients)
            {
                ingredient.RecipeId = editedRecipe.Id;

                if (ingredient.isNewIngredient)
                {
                    ingredient.isNewIngredient = false;
                    await IngredientService.AddIngredient(ingredient);
                }
                else
                {
                    await IngredientService.UpdateIngredient(ingredient);
                }
            }

            foreach (var deletedIngredient in DeletedIngredients)
            {
                await IngredientService.DeleteIngredient(deletedIngredient.Id);
            }

            foreach (var instruction in Instructions)
            {
                instruction.RecipeId = editedRecipe.Id;

                if (instruction.isNewInstruction)
                {
                    instruction.isNewInstruction = false;
                    await InstructionService.AddInstruction(instruction);
                }
                else
                {
                    await InstructionService.UpdateInstruction(instruction);
                }
            }

            foreach (var deletedInstruction in DeletedInstructions)
            {
                await InstructionService.DeleteInstruction(deletedInstruction.Id);
            }

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}