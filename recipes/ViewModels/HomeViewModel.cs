using recipes.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Title = "About";
            BrowseRecipesCommand = new Command(async () => await Shell.Current.GoToAsync($"//{nameof(RecipesPage)}"));
        }

        public ICommand BrowseRecipesCommand { get; }
    }
}