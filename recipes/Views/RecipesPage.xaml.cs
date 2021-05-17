using recipes.Models;
using recipes.ViewModels;
using recipes.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace recipes.Views
{
    public partial class RecipesPage : ContentPage
    {
        RecipesViewModel _viewModel;

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