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

    [TestMethod]
    public void UpdateNonExistentTask_ShouldNotThrow()
    {
        // Test: Updating non-existent task should be safe
        // Ensures: Null check works in real scenario
        _manager.UpdateTask(999, "NonExistent");
        // Should complete without throwing
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void MarkCompletedNonExistentTask_ShouldNotThrow()
    {
        // Test: Marking non-existent task as completed should be safe
        // Ensures: Null check works in real scenario
        _manager.MarkCompleted(999);
        // Should complete without throwing
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void DeleteTask_ShouldRemoveFromRepository()
    {
        // Test: Delete removes task completely
        // Ensures: Delete works end-to-end
        _manager.AddTask("ToDelete", "Test", null);
        var task = _manager.GetTasksByCategory("Test").First();
        int taskId = task.Id;
        
        _manager.DeleteTask(taskId);
        
        var tasks = _manager.GetTasksByCategory("Test");
        Assert.AreEqual(0, tasks.Count());
    }

    [TestMethod]
    public void TaskWithDeadline_Integration()
    {
        // Test: Tasks with deadlines work correctly
        var deadline = DateTime.Now.AddDays(7);
        _manager.AddTask("DeadlineTask", "Work", deadline);
        
        var task = _manager.GetTasksByCategory("Work").First();
        Assert.AreEqual(deadline, task.Deadline);
    }

    [TestMethod]
    public void MultipleCategories_ShouldBeIndependent()
    {
        // Test: Different categories are isolated
        _manager.AddTask("Work1", "Work", null);
        _manager.AddTask("Personal1", "Personal", null);
        _manager.AddTask("Work2", "Work", null);
        
        var workTasks = _manager.GetTasksByCategory("Work");
        var personalTasks = _manager.GetTasksByCategory("Personal");
        
        Assert.AreEqual(2, workTasks.Count());
        Assert.AreEqual(1, personalTasks.Count());
    }

    [TestMethod]
    public void TaskDefaultProperties_ShouldBeSet()
    {
        // Test: Default properties are set correctly during creation
        _manager.AddTask("TestTask", "TestCat", null);
        var task = _manager.GetTasksByCategory("TestCat").First();
        
        Assert.IsFalse(task.IsCompleted);
        Assert.IsNull(task.Deadline);
        Assert.AreEqual("TestTask", task.Description);
        Assert.AreEqual("TestCat", task.Category);
        Assert.IsTrue(task.Id > 0);
    }
}