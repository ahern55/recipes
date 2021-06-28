using recipes.Views;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public SettingsViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}