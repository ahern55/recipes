using recipes.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace recipes.ViewModels
{
    [QueryProperty(nameof(RecipeId), nameof(RecipeId))]
    public class RecipeDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string RecipeId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadRecipeId(value);
            }
        }

        public async void LoadRecipeId(string itemId)
        {
            try
            {
                var item = await DataStore.GetRecipeAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Recipe");
            }
        }
    }
}
