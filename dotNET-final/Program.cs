using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClassLibrary;

namespace dotNET_final
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                try
                {
                    //Создание каталога рецептов
                    RecipeCatalog catalog = new RecipeCatalog();

                    while (true)
                    {
                        Console.WriteLine("\nМеню:");
                        Console.WriteLine("1. Добавить новый рецепт");
                        Console.WriteLine("2. Просмотреть все рецепты");
                        Console.WriteLine("3. Поиск рецепта");
                        Console.WriteLine("4. Удалить рецепт");
                        Console.WriteLine("5. Сортировать рецепты по имени");
                        Console.WriteLine("6. Сортировать рецепты по дате добавления");
                        Console.WriteLine("7. Сохранить рецепты в файл");
                        Console.WriteLine("8. Загрузить рецепты из файла");
                        Console.WriteLine("9. Поиск рецептов по категории");
                        Console.WriteLine("10. Оценить рецепт");
                        Console.WriteLine("11. Выход");

                        Console.Write("\nВыберите действие: \n");
                        int choice = Convert.ToInt32(Console.ReadLine());

                        switch (choice)
                        {
                            case 1:
                                AddNewRecipe(catalog);
                                break;
                            case 2:
                                catalog.DisplayRecipes();
                                break;
                            case 3:
                                SearchRecipe(catalog);
                                break;
                            case 4:
                                RemoveRecipe(catalog);
                                break;
                            case 5:
                                catalog.SortByName();
                                break;
                            case 6:
                                catalog.SortByDateAdded();
                                break;
                            case 7:
                                SaveRecipes(catalog);
                                break;
                            case 8:
                                LoadRecipes(catalog);
                                break;
                            case 9:
                                SearchByCategory(catalog);
                                break;
                            case 10:
                                RateRecipe(catalog);
                                break;
                            case 11:
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("Неверный ввод. Пожалуйста, выберите действие от 1 до 11.");
                                break;
                        }
                    }

                }

                //<=============Запись Exception в лог =============>

                catch (Exception ex)
                {
                    LogException(ex);
                }
            }

            
        }

        //<=============Добавление нового рецепта НАЧАЛО=============>

        static void AddNewRecipe(RecipeCatalog catalog)
        {
            Console.Write("\nВведите название рецепта: ");
            string name = Console.ReadLine();

            Console.Write("Введите ингредиенты (через запятую): ");
            string[] ingredients = Console.ReadLine().Split(',');

            Console.WriteLine("Введите инструкции (каждый шаг на новой строке, введите 'готово', чтобы закончить): ");
            List<string> instructions = new List<string>();
            string instruction;
            while ((instruction = Console.ReadLine()) != "готово")
            {
                instructions.Add(instruction);
            }

            Console.Write("Введите категории (через запятую): ");
            string[] categories = Console.ReadLine().Split(',');

            // Добавление нового рецепта в каталог
            catalog.AddRecipe(new Recipe(name, ingredients.ToList(), instructions, categories.ToList()));
        }

        //<=============Добавление нового рецепта КОНЕЦ=============>



        //<=============Поиск рецепта/рецептов НАЧАЛО=============>

        static void SearchRecipe(RecipeCatalog catalog)
        {
            Console.Write("\nВведите название рецепта для поиска: ");
            string searchName = Console.ReadLine();

            Recipe foundRecipe = catalog.FindRecipeByName(searchName);

            if (foundRecipe != null)
            {
                Console.WriteLine($"Название: {foundRecipe.Name}");
                Console.WriteLine("Ингредиенты:");
                foreach (var ingredient in foundRecipe.Ingredients)
                {
                    Console.WriteLine(ingredient);
                }
                Console.WriteLine($"Рейтинг: {foundRecipe.GetAverageRating()}");

                Console.WriteLine("Инструкции к рецепту:");
                foreach (var instruction in foundRecipe.Instructions)
                {
                    Console.WriteLine(instruction);
                }
                Console.WriteLine("----------------------------");
            }
            else
            {
                Console.WriteLine("Рецепт не найден.");
            }
        }

        //<=============Поиск рецепта/рецептов КОНЕЦ=============>



        //<=============Удаление рецепта НАЧАЛО=============>

        static void RemoveRecipe(RecipeCatalog catalog)
        {
            Console.Write("\nВведите название рецепта для удаления: ");
            string removeName = Console.ReadLine();

            Recipe recipeToRemove = catalog.FindRecipeByName(removeName);

            if (recipeToRemove != null)
            {
                catalog.RemoveRecipe(recipeToRemove);
                Console.WriteLine("Рецепт удален.");
            }
            else
            {
                Console.WriteLine("Рецепт не найден.");
            }
        }

        //<=============Удаление рецепта КОНЕЦ=============>



        //<=============Сохранение рецептов в файл НАЧАЛО=============>

        static void SaveRecipes(RecipeCatalog catalog)
        {
            Console.Write("\nВведите имя файла для сохранения: ");
            string fileName = Console.ReadLine();
            catalog.SaveRecipesToFile(fileName);
            Console.WriteLine("Рецепты сохранены в файл.");
        }

        //<=============Сохранение рецептов в файл КОНЕЦ=============>



        //<=============Загрузка рецептов из файла НАЧАЛО=============>

        static void LoadRecipes(RecipeCatalog catalog)
        {
            Console.Write("\nВведите имя файла для загрузки: ");
            string fileName = Console.ReadLine();
            catalog.LoadRecipesFromFile(fileName);
            Console.WriteLine("Рецепты загружены из файла.");
        }

        //<=============Загрузка рецептов из файла КОНЕЦ=============>



        //<=============Поиск рецептов по категории НАЧАЛО=============>

        static void SearchByCategory(RecipeCatalog catalog)
        {
            Console.Write("\nВведите категорию для поиска рецептов: ");
            string category = Console.ReadLine();

            List<Recipe> recipesInCategory = catalog.FindRecipesByCategory(category);

            if (recipesInCategory.Count > 0)
            {
                Console.WriteLine($"Рецепты в категории '{category}':");
                foreach (Recipe recipe in recipesInCategory)
                {
                    Console.WriteLine(recipe.Name + "(Рейтинг: " + recipe.GetAverageRating() + ")");
                }

            }
            else
            {
                Console.WriteLine("Рецепты в указанной категории не найдены.");
            }
        }

        //<=============Поиск рецептов по категории КОНЕЦ=============>



        //<=============Оценка рецептов НАЧАЛО=============>

        static void RateRecipe(RecipeCatalog catalog)
        {
            Console.Write("\nВведите название рецепта для оценки: ");
            string recipeName = Console.ReadLine();

            Recipe recipeToRate = catalog.FindRecipeByName(recipeName);

            if (recipeToRate != null)
            {
                Console.Write("Введите оценку (от 1 до 5): ");
                int rating = Convert.ToInt32(Console.ReadLine());

                if (rating >= 1 && rating <= 5)
                {
                    catalog.RateRecipe(recipeToRate, rating);
                    Console.WriteLine("Рецепт оценен.");
                }
                else
                {
                    Console.WriteLine("Оценка должна быть от 1 до 5.");
                }
            }
            else
            {
                Console.WriteLine("Рецепт не найден.");
            }
        }

        //<=============Оценка рецептов КОНЕЦ=============>



        //<=============Логирование исключений НАЧАЛО=============>

        static void LogException(Exception ex)
        {
            string logFilePath = "error_log.txt"; // Указываем путь и имя файла для лога

            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"[{DateTime.Now}] Ошибка: {ex.Message}");
                    writer.WriteLine(); // Пустая строка для разделения записей об исключениях
                }
            }
            catch (Exception)
            {
                // Если возникло исключение при записи в лог, выводим сообщение в консоль
                Console.WriteLine("Ошибка при записи в лог файл.");
            }
        }

        //<=============Логирование исключений КОНЕЦ=============>

    }
}
