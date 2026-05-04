namespace ShoppingListManager.ConsoleApp.Core;

public interface IScreenOptions
{
  string GetMenuOption();
  void HandleOption(string option);
}