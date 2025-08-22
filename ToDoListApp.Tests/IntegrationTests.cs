using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListApp;

namespace ToDoListApp.Tests;

[TestClass]
public class IntegrationTests
{
    private ToDoManager _manager;

    [TestInitialize]
    public void Setup()
    {
        // Real integration: Use actual InMemoryRepo, no doubles.
        // Why: Test component interaction (manager + repo).
        var repo = new InMemoryTaskRepository();
        _manager = new ToDoManager(repo);
    }

    [TestMethod]
    public void AddAndGetTasks_Integration()
    {
        // Test: Add then retrieve by category.
        // Ensures: Manager and repo interact correctly.
        _manager.AddTask("Task1", "Cat1", null);
        _manager.AddTask("Task2", "Cat1", null);
        var tasks = _manager.GetTasksByCategory("Cat1");
        Assert.AreEqual(2, tasks.Count());
    }

    [TestMethod]
    public void UpdateAndMarkCompleted_Integration()
    {
        // Test: Add, update, mark, verify state.
        // Ensures: Full flow across components.
        _manager.AddTask("Task", "Cat", null);
        var task = _manager.GetTasksByCategory("Cat").First();
        _manager.UpdateTask(task.Id, "Updated");
        _manager.MarkCompleted(task.Id);
        var updated = _manager.GetTasksByCategory("Cat").First();
        Assert.AreEqual("Updated", updated.Description);
        Assert.IsTrue(updated.IsCompleted);
    }
}