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

  public override List<string> Validate()
  {

    List<string> errors = new List<string>();

    if (string.IsNullOrWhiteSpace(Name))
      errors.Add("O campo \"Nome\" deve ser preenchido;");

    else if (Name.Length < 2 || Name.Length > 100)
      errors.Add("O campo \"Nome\" deve conter entre 2 e 100 caracteres;");

    if (!Enum.GetValues<UnitOfMeasure>().Contains(UnitOfMeasure))
      errors.Add("O campo \"Unidade de Medida\" deve conter uma seleção permitida (Kg, Unidade, Litro, Caixa);");

    if (EstimatedPrice == 0)
      errors.Add("O campo \"Preço Aproximado\" deve ser preenchido.");

    return errors;
  }
}