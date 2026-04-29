using ShoppingListManager.ConsoleApp.Categories;
using ShoppingListManager.ConsoleApp.Core;
namespace ShoppingListManager.ConsoleApp;

class MainScreen
{
  private readonly ScreenUtils screen = new("Lista de Compras");
  private CategoryRepository categoryRepository;

  public MainScreen(CategoryRepository categoryRepository)
  {

    this.categoryRepository = categoryRepository;

    Category category = new("Frutas", Colors.Blue);
    categoryRepository.Create(category);
  }

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

    if (menuOption == "1")
      return new CategoryScreen(categoryRepository);

    return null;
  }
}