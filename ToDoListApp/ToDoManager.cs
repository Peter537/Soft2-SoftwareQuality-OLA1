namespace ToDoListApp;

/// <summary>
/// Manages to-do operations, depends on repository.
/// </summary>
public class ToDoManager
{
    private readonly ITaskRepository _repository;

    public ToDoManager(ITaskRepository repository)
    {
        _repository = repository;
    }

    public void AddTask(string description, string category, DateTime? deadline)
    {
        var task = new Task { Description = description, Category = category, Deadline = deadline };
        _repository.Add(task);
    }

    public void UpdateTask(int id, string newDescription)
    {
        var task = _repository.GetById(id);
        if (task != null)
        {
            task.Description = newDescription;
            _repository.Update(task);
        }
    }

    public void DeleteTask(int id)
    {
        _repository.Delete(id);
    }

    public void MarkCompleted(int id)
    {
        var task = _repository.GetById(id);
        if (task != null)
        {
            task.IsCompleted = true;
            _repository.Update(task);
        }
    }

    public IEnumerable<Task> GetTasksByCategory(string category)
    {
        return _repository.GetAllByCategory(category);
    }
}