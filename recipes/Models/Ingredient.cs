using SQLite;

namespace recipes.Models
{
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int RecipeId { get; set; }

        public string Name { get; set; }

        public string Amount { get; set; }

        public string Unit { get; set; }

        public int Index { get; set; }
    }
}