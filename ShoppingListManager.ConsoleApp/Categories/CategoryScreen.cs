using ShoppingListManager.ConsoleApp.Core;
namespace ShoppingListManager.ConsoleApp.Categories;

public class CategoryScreen : DefaultScreen<Category>
{
  private readonly ScreenUtils screen = new("Gestão de Categorias");
  private readonly CategoryRepository repository;

  public CategoryScreen(CategoryRepository repository) : base("Categoria", repository)
  {

    this.repository = repository;
  }

  public override void ShowAll(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Categorias");

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
      "\n{0, -7} | {1, -20} | {2, -10}",
        "Id", "Nome", "Cor"
    );

    List<Category> categories = repository.FindAll();

    if (categories.Count == 0)
    {
      screen.ShowWarningMessage("Não existe nenhum registro.");
      return;
    }

    foreach (Category c in categories)
    {
      string? color = string.Empty;

      if (c.Color == Colors.Red)
      {
        color = "Vermelho";
        Console.ForegroundColor = ConsoleColor.Red;
      }

      else if (c.Color == Colors.Green)
      {
        color = "Verde";
        Console.ForegroundColor = ConsoleColor.Green;
      }

      else if (c.Color == Colors.Blue)
      {
        color = "Azul";
        Console.ForegroundColor = ConsoleColor.Blue;
      }

      Console.WriteLine("{0, -7} | {1, -20} | {2, -10}",
          c.Id, c.Name, color);

      Console.ResetColor();
    }

    Console.WriteLine(line);

    if (showHeader)
    {
      Console.Write("\nDigite ENTER para continuar... ");
      Console.ReadLine();
    }
  }

  protected override Category GetRegistrationData()
  {

    string name;

    do
    {

      Console.WriteLine("\nInforme o nome da categoria");
      Console.Write("> ");
      name = Console.ReadLine() ?? string.Empty; ;

      if (isNameValid(name)) break;

      screen.ShowErrorMessage("Já existe um registro de categoria com este nome.");
    } while (true);

    Colors color = GetColorOption();

    return new Category(name, color);
  }

  private bool isNameValid(string name)
  {
    Category[] categories = [.. repository.FindAll()];

    for (int i = 0; i < categories.Length; i++)
    {
      if (categories[i].Name == name)
        return false;
    }

    return true;
  }

  private Colors GetColorOption()
  {

    Console.WriteLine("\nSelecione uma cor válida para a categoria");
    Console.WriteLine();

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("[1] Vermelho");

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("[2] Azul");

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("[3] Verde");

    Console.ResetColor();
    Console.WriteLine("[4] Branco (Padrão)");

    Console.Write("\n> ");
    string selectedColor = Console.ReadLine() ?? string.Empty;

    Colors color;

    if (selectedColor == "1") color = Colors.Red;

    else if (selectedColor == "2") color = Colors.Blue;

    else if (selectedColor == "3") color = Colors.Green;

    else color = Colors.White;

    return color;
  }
}