namespace ShoppingListManager.ConsoleApp.Core;

public abstract class DefaultRepository<T> where T : DefaultEntity<T>
{
  protected readonly List<T> entities = [];

  public void Create(T newEntity)
  {

    entities.Add(newEntity);
  }

  public bool Update(string id, T newEntity)
  {

    T? entity = FindById(id);

    if (entity == null) return false;

    entity.UpdateRegister(newEntity);

    return true;
  }

  public bool Delete(string id)
  {
    T? entity = FindById(id);


    if (entity == null) return false;

    entities.Remove(entity);

    return true;
  }

  public List<T> FindAll()
  {

    return entities;
  }

  public T? FindById(string? id)
  {

    return entities.Find(e => e.Id == id);
  }
}