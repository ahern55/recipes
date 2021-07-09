using recipes.Views;
using System;
using Xamarin.Forms;
using recipes.Helpers;

namespace recipes
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RecipeDetailPage), typeof(RecipeDetailPage));
            Routing.RegisterRoute(nameof(EditRecipePage), typeof(EditRecipePage));

            switch (Settings.HomePage)
            {
                case 0:
                    CurrentItem = home;
                    break;

                case 1:
                    CurrentItem = recipes;
                    break;
            }
        }

        private async void OnMenuRecipeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}