using ShoppingListManager.ConsoleApp.Core;
using ShoppingListManager.ConsoleApp.Products;

namespace ShoppingListManager.ConsoleApp.ShoppingLists;

public class ShoppingList : DefaultEntity<ShoppingList>
{

  public string Name { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public ShoppingListStatus Status { get; private set; }
  public List<ListItem> Items { get; private set; } = [];
  public decimal TotalSpent
  {
    get
    {
      decimal totalSpent = 0;

      foreach (ListItem item in Items)
        totalSpent += item.Price;

      return totalSpent;
    }
  }

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

  public void AddItem(Product product, int quantity)
  {
    ListItem item = new(product, quantity);

    Items.Add(item);
  }

  public bool RemoveItem(string itemId)
  {
    foreach (ListItem item in Items)
    {
      if (item.Id == itemId)
      {
        Items.Remove(item);
        return true;
      }
    }

    return false;
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
