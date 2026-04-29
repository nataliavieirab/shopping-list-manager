using System.Security.Cryptography;

namespace ShoppingListManager.ConsoleApp.Core;

public abstract class DefaultEntity<T> where T : DefaultEntity<T>
{
  public string Id { get; set; } = string.Empty;

  public DefaultEntity()
  {

    Id = Convert
        .ToHexString(RandomNumberGenerator.GetBytes(20))
        .ToLower()
        .Substring(0, 7);
  }

  public abstract string[] Validate();
  public abstract void UpdateRegister(T updatedEntity);
}