using ShoppingListManager.ConsoleApp.Categories;
using ShoppingListManager.ConsoleApp.Core;
using ShoppingListManager.ConsoleApp.Products;
namespace ShoppingListManager.ConsoleApp;

class Program
{
  static void Main(string[] args)
  {

    CategoryRepository categoryRepository = new();
    ProductRepository productRepository = new();

    MainScreen mainScreen = new(categoryRepository, productRepository);

    while (true)
    {

      IScreenOptions? selectedScreen = mainScreen.GetMainMenuOption();

      if (selectedScreen == null)
      {
        Console.Clear();
        break;
      }

      while (true)
      {

        string internalMenuOption = selectedScreen.GetMenuOption();

        if (internalMenuOption == "S") break;

        selectedScreen.HandleOption(internalMenuOption);
      }
    }
  }
}