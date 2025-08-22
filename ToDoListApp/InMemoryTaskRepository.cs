namespace ToDoListApp;

/// <summary>
/// In-memory repository for tasks, simulating persistence.
/// </summary>
public class InMemoryTaskRepository : ITaskRepository
{
    private readonly Dictionary<int, Task> _tasks = new();
    private int _nextId = 1;

    public void Add(Task task)
    {
        task.Id = _nextId++;
        _tasks.Add(task.Id, task);
    }

    public void Update(Task task)
    {
        if (_tasks.ContainsKey(task.Id))
            _tasks[task.Id] = task;
    }

    public void Delete(int id)
    {
        _tasks.Remove(id);
    }

    public Task? GetById(int id)
    {
        _tasks.TryGetValue(id, out var task);
        return task;
    }

    public IEnumerable<Task> GetAllByCategory(string category)
    {
        return _tasks.Values.Where(t => t.Category == category);
    }
}