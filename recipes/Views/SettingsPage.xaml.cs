using recipes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace recipes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            this.BindingContext = new SettingsViewModel();
        }
    }
}