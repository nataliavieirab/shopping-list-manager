using ShoppingListManager.ConsoleApp.Core;

namespace ShoppingListManager.ConsoleApp.ShoppingLists;

public class ShoppingList : DefaultEntity<ShoppingList>
{

  public string Name { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public ShoppingListStatus Status { get; private set; }

  public ShoppingList(string name)
  {

    Name = name;
    CreatedAt = DateTime.Now;

    Open();
  }

  public void Open()
  {
    Status = ShoppingListStatus.Open;
  }

  public void Conclude()
  {
    Status = ShoppingListStatus.Completed;
  }

  public override void UpdateData(ShoppingList updatedEntity)
  {

    Name = updatedEntity.Name;
  }

  public override List<string> Validate()
  {
    List<string> errors = new List<string>();

    if (Name.Length < 3 || Name.Length > 100)
      errors.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

    return errors;
  }
}
