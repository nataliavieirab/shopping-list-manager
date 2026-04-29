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

  public override string[] Validate()
  {

    string errors = string.Empty;

    if (Name.Length == 0 || Name.Length > 50)
      errors += "O campo \"Nome\" deve conter entre 0 e 50 caracteres;";

    //else if (!Enum.IsDefined(typeof(Colors), Color))
    else if (!Enum.GetValues<Colors>().Contains(Color))
      errors += "O campo \"Cor\" deve conter uma seleção permitida (Vermelho, Azul, Verde, Branco);";

    return errors.Split(';', StringSplitOptions.RemoveEmptyEntries);
  }
}