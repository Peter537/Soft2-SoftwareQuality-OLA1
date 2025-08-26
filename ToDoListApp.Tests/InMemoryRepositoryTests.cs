using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoListApp;

namespace ToDoListApp.Tests;

[TestClass]
public class InMemoryRepositoryTests
{
    private InMemoryTaskRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        _repository = new InMemoryTaskRepository();
    }

    [TestMethod]
    public void Add_ShouldIncrementId()
    {
        // Tests: Verify ID increments correctly, not decrements
        // Targets surviving mutant: _nextId++ to _nextId--
        var task1 = new Task { Description = "First" };
        var task2 = new Task { Description = "Second" };
        
        _repository.Add(task1);
        _repository.Add(task2);
        
        Assert.AreEqual(1, task1.Id);
        Assert.AreEqual(2, task2.Id);
        Assert.AreNotEqual(0, task1.Id); // Would fail with _nextId--
        Assert.AreNotEqual(-1, task2.Id); // Would fail with _nextId--
    }

    [TestMethod]
    public void Update_ShouldUpdateExistingTask()
    {
        // Tests: Verify update only works when task exists
        // Targets surviving mutant: ContainsKey to !ContainsKey
        var task = new Task { Description = "Original" };
        _repository.Add(task);
        
        task.Description = "Updated";
        _repository.Update(task);
        
        var retrieved = _repository.GetById(task.Id);
        Assert.AreEqual("Updated", retrieved?.Description);
    }

    [TestMethod]
    public void Update_ShouldNotUpdateNonExistentTask()
    {
        // Tests: Verify update doesn't work for non-existent tasks
        // Targets surviving mutant: ContainsKey to !ContainsKey
        var task = new Task { Id = 999, Description = "NonExistent" };
        
        _repository.Update(task); // Should do nothing
        
        var retrieved = _repository.GetById(999);
        Assert.IsNull(retrieved);
    }

    [TestMethod]
    public void GetById_ShouldReturnNullForNonExistentTask()
    {
        // Tests: Verify GetById returns null when task doesn't exist
        // Targets surviving mutant: entire method body to ;
        var result = _repository.GetById(999);
        Assert.IsNull(result);
    }

    [TestMethod]
    public void GetById_ShouldReturnTaskWhenExists()
    {
        // Tests: Verify GetById returns correct task when it exists
        // Targets surviving mutant: entire method body to ;
        var task = new Task { Description = "Test" };
        _repository.Add(task);
        
        var result = _repository.GetById(task.Id);
        Assert.IsNotNull(result);
        Assert.AreEqual("Test", result.Description);
        Assert.AreEqual(task.Id, result.Id);
    }

    [TestMethod]
    public void GetById_ShouldReturnCorrectTask()
    {
        // Tests: Verify TryGetValue works correctly
        // Targets surviving mutant: method body replacement
        var task1 = new Task { Description = "First" };
        var task2 = new Task { Description = "Second" };
        
        _repository.Add(task1);
        _repository.Add(task2);
        
        var result1 = _repository.GetById(task1.Id);
        var result2 = _repository.GetById(task2.Id);
        
        Assert.AreEqual("First", result1?.Description);
        Assert.AreEqual("Second", result2?.Description);
        Assert.AreNotEqual(result1?.Id, result2?.Id);
    }

    [TestMethod]
    public void Delete_ShouldRemoveTask()
    {
        // Tests: Verify delete actually removes task
        var task = new Task { Description = "ToDelete" };
        _repository.Add(task);
        
        Assert.IsNotNull(_repository.GetById(task.Id));
        
        _repository.Delete(task.Id);
        
        Assert.IsNull(_repository.GetById(task.Id));
    }

    [TestMethod]
    public void GetAllByCategory_ShouldFilterCorrectly()
    {
        // Tests: Verify category filtering works
        var task1 = new Task { Description = "Work1", Category = "Work" };
        var task2 = new Task { Description = "Personal1", Category = "Personal" };
        var task3 = new Task { Description = "Work2", Category = "Work" };
        
        _repository.Add(task1);
        _repository.Add(task2);
        _repository.Add(task3);
        
        var workTasks = _repository.GetAllByCategory("Work").ToList();
        var personalTasks = _repository.GetAllByCategory("Personal").ToList();
        
        Assert.AreEqual(2, workTasks.Count);
        Assert.AreEqual(1, personalTasks.Count);
        Assert.IsTrue(workTasks.All(t => t.Category == "Work"));
        Assert.IsTrue(personalTasks.All(t => t.Category == "Personal"));
    }
}