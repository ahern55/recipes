using recipes.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            BrowseRecipesCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(RecipesPage)}"));
        }

        public ICommand BrowseRecipesCommand { get; }
    }
}