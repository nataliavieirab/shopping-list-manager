using ShoppingListManager.ConsoleApp.Core;

namespace ShoppingListManager.ConsoleApp;

class MainScreen
{

  private readonly ScreenUtils screen = new("Clube da Leitura");

  public MainScreen() { }

  public IScreen? GetMainMenuOption()
  {

    screen.MainHeader();
    Console.WriteLine("\n[1] Gerenciar Categorias");
    Console.WriteLine("[2] Gerenciar Produtos");
    Console.WriteLine("[3] Gerenciar Listas");
    Console.WriteLine("[4] Gerenciar Itens de Listas de Compras");
    Console.WriteLine("[S] Sair");
    Console.Write("\n> ");
    string menuOption = Console.ReadLine()?.ToUpper()!;

    return null;
  }
}