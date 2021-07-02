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

        public static int ColorTheme
        {
            get => Preferences.Get(nameof(ColorTheme), colorTheme);
            set => Preferences.Set(nameof(ColorTheme), value);
        }
    }
}