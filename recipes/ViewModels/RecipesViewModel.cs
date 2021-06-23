using recipes.Models;
using recipes.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        private Recipe _selectedRecipe;

        public ObservableCollection<Recipe> Recipes { get; }
        public Command LoadRecipesCommand { get; }
        public Command AddRecipeCommand { get; }
        public Command<Recipe> RecipeTapped { get; }

        public RecipesViewModel()
        {
            Title = "Browse Recipes";
            Recipes = new ObservableCollection<Recipe>();
            LoadRecipesCommand = new Command(async () => await ExecuteLoadRecipesCommand());

            RecipeTapped = new Command<Recipe>(OnRecipeSelected);

            AddRecipeCommand = new Command(OnAddRecipe);
        }

        private async Task ExecuteLoadRecipesCommand()
        {
            IsBusy = true;

            try
            {
                Recipes.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Recipes.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedRecipe = null;
        }

        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                SetProperty(ref _selectedRecipe, value);
                OnRecipeSelected(value);
            }
        }

        private async void OnAddRecipe(object obj)
        {
            await Shell.Current.GoToAsync(nameof(EditRecipePage));
        }

        private async void OnRecipeSelected(Recipe recipe)
        {
            if (recipe == null)
                return;

            // This will push the RecipeDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(RecipeDetailPage)}?{nameof(RecipeDetailViewModel.RecipeId)}={recipe.Id}");
        }
    }
}