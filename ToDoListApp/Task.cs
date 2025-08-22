namespace ToDoListApp;

/// <summary>
/// Represents a task in the to-do list.
/// </summary>
public class Task
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime? Deadline { get; set; }
    public string Category { get; set; } = "Default";
}