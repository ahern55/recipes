using recipes.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command NameClickCommand { get; }

        public AboutViewModel()
        {
            NameClickCommand = new Command(OnLinkClicked);
        }

        private async void OnLinkClicked(object obj)
        {
            await Launcher.OpenAsync(new System.Uri("https://jasonahern.com"));
        }
    }
}