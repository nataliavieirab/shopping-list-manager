namespace ShoppingListManager.ConsoleApp.Core;

public interface IScreenCrud
{
  void Create();
  void Edit();
  void Delete();
  void ShowAll(bool showHeader);
}
