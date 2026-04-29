using ShoppingListManager.ConsoleApp.Core;

namespace ShoppingListManager.ConsoleApp;

class Program
{
  static void Main(string[] args)
  {

    MainScreen mainScreen = new();

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