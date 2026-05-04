namespace ShoppingListManager.ConsoleApp.Core;

public class ScreenUtils
{
  public string? title;

  public ScreenUtils(string _title)
  {
    title = _title;
  }

  public void MainHeader()
  {
    string line = GetUIDoubleLine();

    Console.Clear();
    Console.WriteLine(line);
    Console.WriteLine($"------------------------------------------ {title} ----------------------------------------------");
    Console.WriteLine(line);
  }

  public void OperationHeader(string operation)
  {
    MainHeader();

    string centeredText = new string(' ', 42) + operation;
    Console.WriteLine($"\n{centeredText.ToUpper()}");
  }

  public void InternOperationHeader(string operation)
  {

    string centeredText = new string(' ', 42) + operation;
    Console.WriteLine($"\n{centeredText}");
  }

  public void ShowMessage(string message)
  {
    Console.WriteLine();

    ShowUISimpleLine();
    Console.WriteLine(message);
    ShowUISimpleLine();

    ShowEnterMessage();
  }

  public void ShowWarningMessage(string message)
  {

    ShowMessage($"⚠️  {message}");
  }

  public void ShowSuccessMessage(string message)
  {

    ShowMessage($"✅ {message}");
  }

  public void ShowErrorMessage(string message)
  {

    ShowMessage($"❌ {message}");
  }

  public void ShowEnterMessage()
  {
    Console.WriteLine("\nDigite ENTER para continuar...");
    Console.ReadLine();
  }

  public void ShowError(string[] errors)
  {
    for (int i = 0; i < errors.Length; i++)
    {

      string error = errors[i];

      Console.ForegroundColor = ConsoleColor.Red;
      ShowErrorMessage(error);
      Console.ResetColor();
    }

    ShowEnterMessage();
  }

  public string GetEntityID(string entityName)
  {
    string? selectedId;

    do
    {
      Console.WriteLine($"\nDigite o ID do/a {entityName}");
      Console.Write("> ");
      selectedId = Console.ReadLine();

      if (!string.IsNullOrWhiteSpace(selectedId) && selectedId.Length == 7) break;
    } while (true);

    return selectedId;
  }

  public string GetUIDoubleLine()
  {
    return "==========================================================================================================";
  }

  public void ShowUISimpleLine()
  {
    Console.WriteLine("----------------------------------------------------------------------------------------------------------");
  }
}