using recipes.ViewModels;
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

        private void ExpandIngredients_Tapped(object sender, System.EventArgs e)
        {
            if (IngredientsListView.IsVisible)
            {
                IngredientsCaret.RotateTo(180, 200);
                IngredientsListView.IsVisible = false;
            }
            else
            {
                IngredientsCaret.RotateTo(0, 200);
                IngredientsListView.IsVisible = true;
            }
        }

        private void ExpandInstructions_Tapped(object sender, System.EventArgs e)
        {
            if (InstructionsListView.IsVisible)
            {
                InstructionsCaret.RotateTo(180, 200);
                InstructionsListView.IsVisible = false;
            }
            else
            {
                InstructionsCaret.RotateTo(0, 200);
                InstructionsListView.IsVisible = true;
            }
        }
    }
}