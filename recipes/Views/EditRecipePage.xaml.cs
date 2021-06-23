using recipes.Models;
using recipes.ViewModels;
using Xamarin.Forms;

namespace recipes.Views
{
    public partial class EditRecipePage : ContentPage
    {
        public Recipe Recipe { get; set; }

        public EditRecipePage()
        {
            InitializeComponent();
            BindingContext = new EditRecipeViewModel();
        }
    }
}