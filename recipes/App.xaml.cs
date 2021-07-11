using recipes.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using recipes.Helpers;

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
            AppCenter.Start($"android={Secrets.AppCenterSecrets.AppCenterSecret_Android};" +
                  $"ios={Secrets.AppCenterSecrets.AppCenterSecret_iOS}",
                  typeof(Analytics), typeof(Crashes));

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