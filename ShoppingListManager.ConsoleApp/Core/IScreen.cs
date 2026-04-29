namespace ShoppingListManager.ConsoleApp.Core;

public interface IScreen
{
  string GetMenuOption();
  void HandleOption(string option);
}