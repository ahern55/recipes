using System;

namespace recipes.Models
{
    public class Ingredient 
    {
        public string RecipeId { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
        public int Index { get; set; }
    }
}