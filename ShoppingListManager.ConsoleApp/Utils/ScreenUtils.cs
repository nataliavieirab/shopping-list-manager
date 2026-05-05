namespace ShoppingListManager.ConsoleApp.Core;

public class ScreenUtils
{
  public string title;

  public ScreenUtils(string _title)
  {
    title = _title;
  }

  public void OperationHeader(string operation)
  {
    ShowTitle();

    string centeredText = CenterText(operation);

    Console.WriteLine($"\n{centeredText.ToUpper()}");
  }

  public void ShowTitle()
  {

    string line = GetUIDoubleLine();
    string centeredText = CenterText(title);

    Console.Clear();
    Console.WriteLine(line);
    Console.WriteLine(centeredText);
    Console.WriteLine(line);
  }

  public static string CenterText(string text)
  {
    int consoleWidth = Console.WindowWidth;
    int padding = (consoleWidth - text.Length) / 2;

    if (padding > 0)
      return new string(' ', padding) + text;
    else
      return text;
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

  public void ShowError(List<string> errors)
  {
    foreach (string error in errors)
    {

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
    return new string('=', Console.WindowWidth - 1);
  }

  public string GetTableLine()
  {
    return "==========================================================================================================";
  }

  public void ShowUISimpleLine()
  {
    string line = new string('-', Console.WindowWidth - 1);
    Console.WriteLine(line);
  }
}