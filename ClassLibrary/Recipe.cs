using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Recipe
    {

        //<=============Переменные класса Recipe НАЧАЛО=============>

        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
        public DateTime DateAdded { get; set; }
        public List<string> Categories { get; set; }
        public List<int> Ratings { get; set; }

        //<=============Переменные класса Recipe КОНЕЦ=============>





        public Recipe(string name, List<string> ingredients, List<string> instructions, List<string> categories)
        {
            Name = name;
            Ingredients = ingredients;
            Instructions = instructions;
            DateAdded = DateTime.Now;
            Categories = categories;
            Ratings = new List<int>();
        }





        //<=============Получение средней оценки рецепта НАЧАЛО=============>

        public double GetAverageRating()
        {
            if (Ratings.Count == 0)
            {
                return 0;
            }
            return Ratings.Average();
        }

        //<=============Получение средней оценки рецепта КОНЕЦ=============>
    }
}
