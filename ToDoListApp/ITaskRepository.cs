namespace ToDoListApp;

/// <summary>
/// Interface for task persistence, allowing test doubles.
/// </summary>
public interface ITaskRepository
{
    void Add(Task task);
    void Update(Task task);
    void Delete(int id);
    Task? GetById(int id);
    IEnumerable<Task> GetAllByCategory(string category);
}