using ShoppingListManager.ConsoleApp.Categories;
using ShoppingListManager.ConsoleApp.Core;

namespace ShoppingListManager.ConsoleApp.Products;

public class Product : DefaultEntity<Product>
{
  public string Name { get; private set; }
  public UnitOfMeasure UnitOfMeasure { get; private set; }
  public decimal EstimatedPrice { get; private set; }
  public Category Category { get; private set; }

  public Product(string name, UnitOfMeasure unitOfMeasure, decimal estimatedPrice, Category category)
  {

    Name = name;
    UnitOfMeasure = unitOfMeasure;
    EstimatedPrice = estimatedPrice;
    Category = category;
  }

  public override void UpdateData(Product updatedEntity)
  {

    Name = updatedEntity.Name;
    UnitOfMeasure = updatedEntity.UnitOfMeasure;
    EstimatedPrice = updatedEntity.EstimatedPrice;
    Category = updatedEntity.Category;
  }

  public override string[] Validate()
  {

    string errors = string.Empty;

    if (string.IsNullOrWhiteSpace(Name))
      errors += "O campo \"Nome\" deve ser preenchido;";

    else if (Name.Length < 2 || Name.Length > 100)
      errors += "O campo \"Nome\" deve conter entre 2 e 100 caracteres;";

    if (!Enum.GetValues<UnitOfMeasure>().Contains(UnitOfMeasure))
      errors += "O campo \"Unidade de Medida\" deve conter uma seleção permitida (Kg, Unidade, Litro, Caixa);";

    // if (EstimatedPrice <= 0)
    //   errors += "O campo \"Unidade de Medida\" deve ser preenchido com um valor positivo;";

    return errors.Split(';', StringSplitOptions.RemoveEmptyEntries);
  }
}
