using recipes.Services;
using Xamarin.Forms;

namespace recipes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<RecipeService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}