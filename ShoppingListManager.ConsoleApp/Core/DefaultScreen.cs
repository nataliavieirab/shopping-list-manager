namespace ShoppingListManager.ConsoleApp.Core;

public abstract class DefaultScreen<T> : IScreen where T : DefaultEntity<T>
{
  private readonly ScreenUtils screen;
  public string entityName = string.Empty;
  private DefaultRepository<T> repository;

  public DefaultScreen(string entityName, DefaultRepository<T> repository)
  {
    this.screen = new ScreenUtils($"Gestão de {entityName}");
    this.entityName = entityName;
    this.repository = repository;
  }

  public string GetMenuOption()
  {

    screen.MainHeader();
    Console.WriteLine($"\n[1] Cadastrar {entityName}");
    Console.WriteLine($"[2] Editar {entityName}");
    Console.WriteLine($"[3] Excluir {entityName}");
    Console.WriteLine($"[4] Visualizar {entityName}s");
    Console.WriteLine($"[S] Voltar para o início");
    Console.Write("\n> ");
    string? input = Console.ReadLine()?.ToUpper();

    return string.IsNullOrWhiteSpace(input)
        ? string.Empty
        : input.ToUpper();
  }

  public void HandleOption(string option)
  {
    if (option == "1")
      Register();

    else if (option == "2")
      Edit();

    else if (option == "3")
      Delete();

    else if (option == "4")
      ShowAll(true);
  }

  public void Register()
  {

    screen.OperationHeader($"Cadastro de {entityName}");

    T newEntity = GetRegistrationData();

    string[] errors = newEntity.Validate();

    if (errors.Length > 0)
    {
      screen.ShowError(errors);

      Register();
      return;
    }

    repository?.Create(newEntity);

    screen.ShowMessage($"✅ O registro \"{newEntity.Id}\" foi cadastrado com sucesso!");
  }

  public void Edit()
  {

    screen.OperationHeader($"Edição de {entityName}");

    ShowAll(showHeader: false);

    string? selectedId = screen.GetEntityID(entityName);

    Console.WriteLine();
    screen.ShowUISimpleLine();

    T newEntity = GetRegistrationData();

    string[] errors = newEntity.Validate();

    if (errors.Length > 0)
    {
      screen.ShowError(errors);

      Edit();
      return;
    }

    bool success = repository.Update(selectedId, newEntity);

    if (!success)
    {
      screen.ShowMessage("❌ Não foi possível encontrar o registro requisitado.");
      return;
    }

    screen.ShowMessage($"✅ O registro \"{selectedId}\" foi editado com sucesso.");
  }

  public void Delete()
  {

    screen.OperationHeader($"Exclusão de {entityName}");

    ShowAll(showHeader: false);

    string? selectedId = screen.GetEntityID(entityName);

    bool success = repository.Delete(selectedId);

    if (!success)
    {
      screen.ShowMessage("❌ Não foi possível encontrar o registro requisitado.");
      return;
    }

    screen.ShowMessage($"✅ O registro \"{selectedId}\" foi excluído com sucesso.");
  }

  public abstract void ShowAll(bool showHeader);

  protected abstract T GetRegistrationData();
}