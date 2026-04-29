using ShoppingListManager.ConsoleApp.Categories;
using ShoppingListManager.ConsoleApp.Core;
namespace ShoppingListManager.ConsoleApp;

class Program
{
  static void Main(string[] args)
  {

    CategoryRepository categoryRepository = new();

    MainScreen mainScreen = new(categoryRepository);

    while (true)
    {

      IScreen? selectedScreen = mainScreen.GetMainMenuOption();

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