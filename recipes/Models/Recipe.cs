using SQLite;
using System.Collections.Generic;

namespace recipes.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        //public string Description { get; set; }

        public int PrepareTime { get; set; }

        public int CookTime { get; set; }

        public int TotalTime => PrepareTime + CookTime;
    }
}