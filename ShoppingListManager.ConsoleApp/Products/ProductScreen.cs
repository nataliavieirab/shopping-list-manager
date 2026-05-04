using ShoppingListManager.ConsoleApp.Categories;
using ShoppingListManager.ConsoleApp.Core;

namespace ShoppingListManager.ConsoleApp.Products;

public class ProductScreen : DefaultScreen<Product>, IScreenOptions, IScreenCrud
{

  private readonly ScreenUtils screen = new("Gestão de Produtos");
  private readonly CategoryRepository categoryRepository;

  public ProductScreen(ProductRepository repository, CategoryRepository categoryRepository) : base("Produto", repository)
  {
    this.repository = repository;
    this.categoryRepository = categoryRepository;
  }

  public override void ShowAll(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Produtos");

    string line = screen.GetUIDoubleLine();

    List<Product> products = repository.FindAll();

    if (products.Count == 0)
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      screen.ShowWarningMessage("Não existe nenhum registro de produto.");
      return;
    }

    Console.Write($"\n{line}");
    Console.WriteLine(
        "{0, -7} | {1, -20} | {2, -10} | {3, -10} | {4, -10}",
        "ID", "Nome", "Categoria", "Unidade", "Preço Apx."
    );

    foreach (Product p in products)
    {

      Console.Write("{0, -7} | ", p.Id);
      Console.Write("{0, -20} | ", p.Name);

      Colors categoryColor = p.Category.Color;

      if (categoryColor == Colors.Red)
        Console.ForegroundColor = ConsoleColor.Red;

      else if (categoryColor == Colors.Green)
        Console.ForegroundColor = ConsoleColor.Green;

      else if (categoryColor == Colors.Blue)
        Console.ForegroundColor = ConsoleColor.Blue;

      Console.Write("{0, -10} | ", p.Category.Name);
      Console.ResetColor();

      Console.Write("{0, -10} | ", p.UnitOfMeasure);
      Console.Write("{0, -10} | ", p.EstimatedPrice);
    }

    if (showHeader) screen.ShowEnterMessage();
  }

  protected override Product GetRegistrationData()
  {
    Console.WriteLine("\nDigite o nome do produto");
    Console.Write("> ");
    string name = Console.ReadLine() ?? string.Empty;

    Console.WriteLine("\nDigite o valor aproximado do produto");
    Console.Write("> ");
    decimal estimatedPrice = Convert.ToDecimal(Console.ReadLine());

    string categoryId = SelectCategory();

    Category? selectedCategory = categoryRepository.FindById(categoryId);

    if (selectedCategory == null)
      throw new NullReferenceException("Não foi possível obter o registro selecionado {Categoria}.");

    UnitOfMeasure unitOfMeasure = GetMeasurementUnit();

    return new Product(name, unitOfMeasure, estimatedPrice, selectedCategory);
  }

  private string SelectCategory()
  {
    screen.InternOperationHeader("Vincular Categoria");

    List<Category> categories = categoryRepository.FindAll();

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -20} | {2, -10}",
        "Id", "Nome", "Cor"
    );

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


      Console.WriteLine(
          "{0, -7} | {1, -20} | {2, -10}",
          c.Id, c.Name, color
      );
    }

    Console.ResetColor();
    Console.Write($"{line}");

    string? selectedId;

    do
    {
      Console.WriteLine("\nDigite o ID da categoria do produto");
      Console.Write("> ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7)
        break;
    } while (true);

    return selectedId;
  }

  private UnitOfMeasure GetMeasurementUnit()
  {

    screen.InternOperationHeader("Selecionar Unidade de Medida");

    Console.WriteLine("\nSelecione uma unidade de medida válida para o produto");

    Console.WriteLine("[1] - Kg");
    Console.WriteLine("[2] - Unidade");
    Console.WriteLine("[3] - Litros");
    Console.WriteLine("[4] - Caixa");

    Console.Write("\n> ");
    string selectedColor = Console.ReadLine() ?? string.Empty;

    UnitOfMeasure unit;

    if (selectedColor == "1") unit = UnitOfMeasure.Kilogram;

    else if (selectedColor == "2") unit = UnitOfMeasure.Unit;

    else if (selectedColor == "3") unit = UnitOfMeasure.Liter;

    else unit = UnitOfMeasure.Box;

    return unit;
  }
}