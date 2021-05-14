using recipes.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace recipes.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}