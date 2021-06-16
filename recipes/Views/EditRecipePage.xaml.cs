using recipes.Models;
using recipes.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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