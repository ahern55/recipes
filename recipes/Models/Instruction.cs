using SQLite;

namespace recipes.Models
{
    public class Instruction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int RecipeId { get; set; }

        public string Contents { get; set; }
    }
}