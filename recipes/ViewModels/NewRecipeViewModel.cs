using recipes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    public class NewRecipeViewModel : BaseViewModel
    {
        //these are the private fields for the properties.
        private string name;
        private string description;
        private string prepareTime = null;
        private string cookTime = null;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public NewRecipeViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            int dummy;
            return !string.IsNullOrWhiteSpace(name)
                && Int32.TryParse(prepareTime, out dummy)
                && dummy > 0
                && Int32.TryParse(cookTime, out dummy)
                && dummy > 0;
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

        private async void OnSave()
        {
            //validate save
            if (!ValidateSave())
            {
                return;
            }

            Recipe newRecipe = new Recipe()
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                Description = Description,
                PrepareTime = Int32.Parse(PrepareTime),
                CookTime = Int32.Parse(CookTime)
            };

            await DataStore.AddItemAsync(newRecipe);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
