using System.Security.Cryptography;
using ShoppingListManager.ConsoleApp.Products;

namespace ShoppingListManager.ConsoleApp.ShoppingLists;

public class ListItem
{
  public string Id { get; private set; }
  public Product Product { get; private set; }
  public int Quantity { get; private set; }
  public decimal Price
  {
    get
    {
      return Product.EstimatedPrice * Quantity;
    }
  }

  public ListItem(Product product, int quantity)
  {

    Id = Convert
            .ToHexString(RandomNumberGenerator.GetBytes(4))
            .ToLower()
            .Substring(0, 7);

    Product = product;
    Quantity = quantity;
  }
}