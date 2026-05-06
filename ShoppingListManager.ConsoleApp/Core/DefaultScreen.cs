namespace ShoppingListManager.ConsoleApp.Core;

public abstract class DefaultScreen<T> where T : DefaultEntity<T>
{
  private readonly ScreenUtils screen;
  public string entityName = string.Empty;
  protected DefaultRepository<T> repository;

  public DefaultScreen(string entityName, DefaultRepository<T> repository)
  {
    this.screen = new ScreenUtils($"Gestão de {entityName}");
    this.entityName = entityName;
    this.repository = repository;
  }

  public virtual string? GetMenuOption()
  {

    screen.ShowTitle();
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

  public virtual void HandleOption(string option)
  {
    if (option == "1")
      Create();

    else if (option == "2")
      Edit();

    else if (option == "3")
      Delete();

    else if (option == "4")
      ShowAll(true);
  }

  public void Create()
  {

    screen.OperationHeader($"Cadastro de {entityName}");

    T newEntity = GetRegistrationData();

    List<string> errors = newEntity.Validate();

    if (errors.Count > 0)
    {
      screen.ShowError(errors);

      Create();
      return;
    }

    List<string> duplicationErros = ValidateDuplicateRecord(newEntity);

    if (duplicationErros.Count > 0)
    {
      screen.ShowError(duplicationErros);

      Create();
      return;
    }

    repository.Create(newEntity);

    screen.ShowSuccessMessage($"O registro \"{newEntity.Id}\" foi cadastrado com sucesso!");
  }

  public void Edit()
  {

    screen.OperationHeader($"Edição de {entityName}");

    ShowAll(showHeader: false);

    string? selectedId = screen.GetEntityID(entityName);

    Console.WriteLine();
    screen.ShowUISimpleLine();

    T newEntity = GetRegistrationData();

    List<string> errors = newEntity.Validate();

    if (errors.Count > 0)
    {
      screen.ShowError(errors);

      Edit();
      return;
    }

    bool success = repository.Update(selectedId, newEntity);

    if (!success)
    {
      screen.ShowErrorMessage("Não foi possível encontrar o registro requisitado.");
      return;
    }

    screen.ShowSuccessMessage($"O registro \"{selectedId}\" foi editado com sucesso.");
  }

  public void Delete()
  {

    screen.OperationHeader($"Exclusão de {entityName}");

    ShowAll(showHeader: false);

    string? selectedId = GetEntityID();

    if (selectedId == null) return;

    T? selectedRegister = repository.FindById(selectedId);

    if (selectedRegister == null)
    {
      screen.ShowErrorMessage("Não foi possível encontrar o registro requisitado.");

      Delete();
      return;
    }

    List<string> duplicationErros = ValidateRecordDeletion(selectedRegister);

    if (duplicationErros.Count > 0)
    {
      screen.ShowError(duplicationErros);
      return;
    }

    bool success = repository.Delete(selectedId);

    if (!success)
    {
      screen.ShowErrorMessage("Não foi possível excluir o registro requisitado.");
      return;
    }

    screen.ShowSuccessMessage($"O registro \"{selectedId}\" foi excluído com sucesso.");
  }

  public abstract void ShowAll(bool showHeader);

  protected virtual List<string> ValidateDuplicateRecord(T newEntity, string? ignoredId = null)
  {
    return new List<string>();
  }

  protected virtual List<string> ValidateRecordDeletion(T record)
  {
    return new List<string>();
  }

  protected abstract T GetRegistrationData();

  private string? GetEntityID()
  {
    string? selectedId;

    do
    {
      Console.WriteLine("\nDigite o ID do registro que deseja excluir (ou S para sair): ");
      Console.Write("> ");
      selectedId = Console.ReadLine() ?? string.Empty;

      if (selectedId.ToUpper() == "S") return null;

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;
    } while (true);

    return selectedId;
  }
}