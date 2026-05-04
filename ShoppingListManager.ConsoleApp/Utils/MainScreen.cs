using ShoppingListManager.ConsoleApp.Categories;
using ShoppingListManager.ConsoleApp.Core;
using ShoppingListManager.ConsoleApp.Products;
using ShoppingListManager.ConsoleApp.ShoppingLists;
namespace ShoppingListManager.ConsoleApp;

class MainScreen
{
  private readonly ScreenUtils screen = new("Lista de Compras");
  private CategoryRepository categoryRepository;
  private ProductRepository productRepository;
  private ShoppingListRepository shoppingListRepository;

  public MainScreen(CategoryRepository categoryRepository, ProductRepository productRepository, ShoppingListRepository shoppingListRepository)
  {

    this.categoryRepository = categoryRepository;
    this.productRepository = productRepository;
    this.shoppingListRepository = shoppingListRepository;

    Category category = new("Mercearia", Colors.Blue);
    categoryRepository.Create(category);

    Product product = new("Café", UnitOfMeasure.Kilogram, 24, category);
    productRepository.Create(product);

    ShoppingList shoppingList = new("Mercado");
    shoppingListRepository.Create(shoppingList);
  }

  public IScreenOptions? GetMainMenuOption()
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

    else if (menuOption == "2")
      return new ProductScreen(productRepository, categoryRepository);

    else if (menuOption == "3")
      return new ShoppingListScreen(shoppingListRepository);

    return null;
  }
}