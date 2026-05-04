using ShoppingListManager.ConsoleApp.Core;
namespace ShoppingListManager.ConsoleApp.Categories;

public class Category : DefaultEntity<Category>
{

  public string Name { get; private set; }
  public Colors Color { get; private set; }

  public Category(string name, Colors color)
  {
    Name = name;
    Color = color;
  }

  public override void UpdateData(Category updatedEntity)
  {
    Name = updatedEntity.Name;
    Color = updatedEntity.Color;
  }

  public override List<string> Validate()
  {

    List<string> errors = new List<string>();

    if (Name.Length == 0 || Name.Length > 50)
      errors.Add("O campo \"Nome\" deve conter entre 0 e 50 caracteres;");

    if (!Enum.GetValues<Colors>().Contains(Color))
      errors.Add("O campo \"Cor\" deve conter uma seleção permitida (Vermelho, Azul, Verde, Branco);");

    return errors;
  }
}