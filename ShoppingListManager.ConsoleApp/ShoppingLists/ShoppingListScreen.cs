using ShoppingListManager.ConsoleApp.Core;

namespace ShoppingListManager.ConsoleApp.ShoppingLists;

public class ShoppingListScreen : DefaultScreen<ShoppingList>, IScreenOptions, IScreenCrud
{

  private readonly ScreenUtils screen = new("Gestão de Lista de Compras");

  public ShoppingListScreen(
    ShoppingListRepository repository
  ) : base("Lista de Compras", repository)
  {
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

  }

  public void RemoveItem()
  {

  }

  public void ShowItens()
  {

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
}