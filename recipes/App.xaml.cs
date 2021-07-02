using recipes.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace recipes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SettingsService.SetTheme();

            DependencyService.Register<RecipeService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            OnResume();
        }

        protected override void OnSleep()
        {
            SettingsService.SetTheme();
            //deregister event
            RequestedThemeChanged -= App_RequestThemeChanged;
        }

        protected override void OnResume()
        {
            SettingsService.SetTheme();
            //register event
            RequestedThemeChanged += App_RequestThemeChanged;
        }

        private void App_RequestThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() => SettingsService.SetTheme());
        }
    }
}