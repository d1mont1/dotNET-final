using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace ClassLibrary
{
    public class RecipeCatalog
    {
        private List<Recipe> recipes;

        public RecipeCatalog()
        {
            recipes = new List<Recipe>();
        }

        //<=============Добавление рецептов НАЧАЛО=============>

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }

        //<=============Добавление рецептов КОНЕЦ=============>



        //<=============Вывод рецептов НАЧАЛО=============>

        public void DisplayRecipes()
        {
            foreach (Recipe recipe in recipes)
            {
                Console.WriteLine(recipe.Name + "(Рейтинг: " + recipe.GetAverageRating() + ")");
            }
        }

        //<=============Вывод рецептов КОНЕЦ=============>



        //<=============Поиск рецептов по наименованию НАЧАЛО=============>

        public Recipe FindRecipeByName(string name)
        {
            return recipes.Find(recipe => recipe.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        //<=============Поиск рецептов по наименованию КОНЕЦ=============>



        //<=============Удаление рецептов НАЧАЛО=============>

        public void RemoveRecipe(Recipe recipe)
        {
            recipes.Remove(recipe);
        }

        //<=============Удаление рецептов КОНЕЦ=============>



        //<=============Сортировка рецептов по имени НАЧАЛО=============>

        public void SortByName()
        {
            recipes.Sort((r1, r2) => string.Compare(r1.Name, r2.Name, StringComparison.Ordinal));
            DisplayRecipes();
        }

        //<=============Сортировка рецептов по имени КОНЕЦ=============>



        //<=============Сортировка рецептов по дате добавления НАЧАЛО=============>

        public void SortByDateAdded()
        {
            recipes.Sort((r1, r2) => DateTime.Compare(r1.DateAdded, r2.DateAdded));
            DisplayRecipes();
        }

        //<=============Сортировка рецептов по дате добавления КОНЕЦ=============>



        //<=============Сохранение рецептов в файл НАЧАЛО=============>

        public void SaveRecipesToFile(string filePath)
        {
            string jsonData = JsonSerializer.Serialize(recipes);
            File.WriteAllText(filePath, jsonData);
        }

        //<=============Сохранение рецептов в файл КОНЕЦ=============>



        //<=============Загрузка рецептов из файла НАЧАЛО=============>

        public void LoadRecipesFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                recipes = JsonSerializer.Deserialize<List<Recipe>>(jsonData);
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }

        //<=============Загрузка рецептов из файла КОНЕЦ=============>



        //<=============Поиск рецептов по категории НАЧАЛО=============>

        public List<Recipe> FindRecipesByCategory(string category)
        {
            return recipes.FindAll(recipe => recipe.Categories.Contains(category));
        }

        //<=============Поиск рецептов по категории КОНЕЦ=============>



        //<=============Оценка рецептов НАЧАЛО=============>

        public void RateRecipe(Recipe recipe, int rating)
        {
            recipe.Ratings.Add(rating);
        }

        //<=============Оценка рецептов КОНЕЦ=============>

    }
}
