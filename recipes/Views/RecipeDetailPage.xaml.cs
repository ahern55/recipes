using recipes.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace recipes.Views
{
    public partial class RecipeDetailPage : ContentPage
    {
        public RecipeDetailPage()
        {
            InitializeComponent();
            BindingContext = new RecipeDetailViewModel();
        }
    }
}