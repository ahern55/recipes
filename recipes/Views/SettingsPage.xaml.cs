﻿using recipes.Helpers;
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

            switch (Settings.ColorTheme)
            {
                case 0:
                    RadioButtonSystem.IsChecked = true;
                    break;

                case 1:
                    RadioButtonLight.IsChecked = true;
                    break;

                case 2:
                    RadioButtonDark.IsChecked = true;
                    break;
            }
            this.BindingContext = new SettingsViewModel();
        }

        private void RadioButtonSystem_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!e.Value) return;

            var value = (sender as RadioButton)?.Value as string;
            if (string.IsNullOrWhiteSpace(value)) return;

            switch (value)
            {
                case "System":
                    Settings.ColorTheme = 0;
                    break;

                case "Light":
                    Settings.ColorTheme = 1;
                    break;

                case "Dark":
                    Settings.ColorTheme = 2;
                    break;
            }
        }
    }
}