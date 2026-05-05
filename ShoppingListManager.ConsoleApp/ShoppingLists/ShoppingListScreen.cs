using ShoppingListManager.ConsoleApp.Categories;
using ShoppingListManager.ConsoleApp.Core;
using ShoppingListManager.ConsoleApp.Products;

namespace ShoppingListManager.ConsoleApp.ShoppingLists;

public class ShoppingListScreen : DefaultScreen<ShoppingList>, IScreenOptions, IScreenCrud
{
  private readonly ScreenUtils screen = new("Gestão de Lista de Compras");
  private readonly ProductRepository productRepository;

  public ShoppingListScreen(
    ShoppingListRepository repository,
    ProductRepository productRepository
  ) : base("Lista de Compras", repository)
  {
    this.productRepository = productRepository;
  }

  public override string? GetMenuOption()
  {
    screen.MainHeader();
    Console.WriteLine("\n[1] Cadastrar Lista de Compras");
    Console.WriteLine("[2] Editar  Lista de Compras");
    Console.WriteLine("[3] Excluir Lista de Compras");
    Console.WriteLine("[4] Visualizar Listas de Compras");
    Console.WriteLine("[5] Adicionar Item à Lista de Compras");
    Console.WriteLine("[6] Remover Item da Lista de Compras");
    Console.WriteLine("[7] Visualizar Itens de Listas de Compras");
    Console.WriteLine("[S] Voltar para o início");
    Console.Write("\n> ");
    string? opcaoMenu = Console.ReadLine()?.ToUpper();

    return opcaoMenu;
  }

  public void AddItem()
  {

    screen.OperationHeader("Adição de Item de Listas de Compras");

    ShowAll(false);

    Console.WriteLine("\nDigite o ID da lista que deseja gerenciar (ou S para sair)");
    Console.Write("> ");
    string selectedId = Console.ReadLine() ?? string.Empty;

    if (selectedId.ToUpper() == "S") return;

    ShoppingList? selectedList = repository.FindById(selectedId);

    if (selectedList == null)
    {
      screen.ShowWarningMessage("Não foi possível encontrar a lista de compras selecionada.");
      return;
    }

    ShowItems(selectedList);

    screen.InternOperationHeader("Selecione um produto abaixo");

    ShowProducts();

    Console.Write("\nDigite o ID do produto que deseja adicionar (ou S para sair): ");
    Console.Write("> ");
    string selectedProductId = Console.ReadLine() ?? string.Empty;

    if (selectedProductId.ToUpper() == "S") return;

    Product? selectedProduct = productRepository.FindById(selectedProductId);

    if (selectedProduct == null)
    {
      screen.ShowErrorMessage("Não foi possível encontrar o produto selecionado.");
      return;
    }

    Console.Write("\nDigite a quantidade do produto que deseja adicionar: ");
    int itemsQuantity = Convert.ToInt32(Console.ReadLine());

    selectedList.AddItem(selectedProduct, itemsQuantity);

    screen.ShowSuccessMessage($"O item \"{selectedProduct.Name}\" foi adicionado à lista com sucesso!");
  }

  public void RemoveItem()
  {

  }

  public void ShowItems(ShoppingList? selectedList = null)
  {
    if (selectedList == null)
    {

      screen.OperationHeader("Visualização de Item de Listas de Compras");

      ShowAll(false);

      Console.Write("\nDigite o ID da lista que deseja gerenciar (ou S para sair): ");
      string selectedId = Console.ReadLine() ?? string.Empty;

      if (selectedId.ToUpper() == "S") return;

      selectedList = repository.FindById(selectedId);

      if (selectedList == null)
      {
        screen.ShowWarningMessage("Não foi possível encontrar a lista de compras selecionada.");
        return;
      }
    }

    List<ListItem> items = selectedList.Items;

    if (items.Count == 0)
    {
      screen.ShowErrorMessage("Nenhum item registrado.");
      return;
    }
    else
    {
      screen.InternOperationHeader($"Itens atuais da lista \"{selectedList.Name}\"");

      string line = screen.GetUIDoubleLine();

      Console.Write($"\n{line}");
      Console.WriteLine(
          "\n{0, -7} | {1, -30} | {2, -15} | {3, -15}",
          "Id", "Nome do Produto", "Quantidade", "Preço (R$)"
      );

      Console.ForegroundColor = ConsoleColor.Yellow;

      foreach (ListItem i in items)
      {
        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15} | {3, -15}",
            i.Id, i.Product.Name, i.Quantity, i.Price.ToString("C2")
        );
      }

      Console.ResetColor();
    }

    screen.ShowEnterMessage();
  }

  public override void ShowAll(bool showHeader)
  {
    if (showHeader) screen.OperationHeader("Visualização de Listas de Compras");

    string line = screen.GetUIDoubleLine();

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -20}",
        "Id", "Nome", "Criação", "Qtd. Itens", "Total Gasto (R$)"
    );

    List<ShoppingList> listas = repository.FindAll();

    foreach (ShoppingList s in listas)
    {
      Console.WriteLine(
          "{0, -7} | {1, -30} | {2, -15} | {3, -20} | {4, -20}",
          s.Id, s.Name, s.CreatedAt.ToShortDateString(), 0, 0.0m.ToString("C2")
      );
    }

    Console.WriteLine(line);

    if (showHeader) screen.ShowEnterMessage();
  }

  protected override ShoppingList GetRegistrationData()
  {
    Console.WriteLine("\nDigite o nome da lista");
    Console.Write("> ");
    string nome = Console.ReadLine() ?? string.Empty;

    return new ShoppingList(nome);
  }

  private void ShowProducts()
  {

    string line = screen.GetUIDoubleLine();

    List<Product> products = productRepository.FindAll();

    if (products.Count == 0)
    {
      screen.ShowWarningMessage("Nenhum produto registrado.");
      return;
    }

    Console.Write($"\n{line}");
    Console.WriteLine(
        "\n{0, -7} | {1, -20} | {2, -10} | {3, -10} | {4, -10}",
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
      Console.Write("{0, -10} | ", p.EstimatedPrice.ToString("C2"));
    }
  }
}