using recipes.ViewModels;
using Xamarin.Forms;

namespace recipes.Views
{
    public partial class RecipesPage : ContentPage
    {
        private RecipesViewModel _viewModel;

        public RecipesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RecipesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}