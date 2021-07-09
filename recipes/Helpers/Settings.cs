using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace recipes.Helpers
{
    public static class Settings
    {
        // 0 for default, 1 for light, 2 for dark
        private const int colorTheme = 0;

        // 0 for Home, 1 for Recipes
        private const int homePage = 0;

        public static int ColorTheme
        {
            get => Preferences.Get(nameof(ColorTheme), colorTheme);
            set => Preferences.Set(nameof(ColorTheme), value);
        }

        public static int HomePage
        {
            get => Preferences.Get(nameof(HomePage), homePage);
            set => Preferences.Set(nameof(HomePage), value);
        }
    }
}