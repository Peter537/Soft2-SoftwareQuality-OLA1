using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDoListApp;

namespace ToDoListApp.Tests;

[TestClass]
public class EdgeCaseTests
{
    private Mock<ITaskRepository> _mockRepo;
    private ToDoManager _manager;

    [TestInitialize]
    public void Setup()
    {
        _mockRepo = new Mock<ITaskRepository>();
        _manager = new ToDoManager(_mockRepo.Object);
    }

    [TestMethod]
    public void UpdateTask_WithNullFromRepository_ShouldHandleGracefully()
    {
        // Tests: Specific null handling in UpdateTask
        // Targets: (task != null) mutant survival
        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((Task?)null);
        
        // Should not throw exception
        _manager.UpdateTask(1, "New Description");
        
        // Verify Update was never called
        _mockRepo.Verify(r => r.Update(It.IsAny<Task>()), Times.Never);
    }

    [TestMethod]
    public void MarkCompleted_WithNullFromRepository_ShouldHandleGracefully()
    {
        // Tests: Specific null handling in MarkCompleted  
        // Targets: (task != null) mutant survival
        _mockRepo.Setup(r => r.GetById(It.IsAny<int>())).Returns((Task?)null);
        
        // Should not throw exception
        _manager.MarkCompleted(1);
        
        // Verify Update was never called
        _mockRepo.Verify(r => r.Update(It.IsAny<Task>()), Times.Never);
    }

    [TestMethod]
    public void MarkCompleted_ShouldOnlySetTrueNotFalse()
    {
        // Tests: Ensure IsCompleted is set to true, not false
        // Targets: task.IsCompleted = true; to task.IsCompleted = false;
        var task = new Task { Id = 1, IsCompleted = false };
        _mockRepo.Setup(r => r.GetById(1)).Returns(task);
        
        _manager.MarkCompleted(1);
        
        // Verify it's true, and explicitly verify it's not false
        Assert.IsTrue(task.IsCompleted);
        Assert.IsFalse(task.IsCompleted == false);
    }

    [TestMethod]
    public void UpdateTask_ShouldCallUpdateOnRepository()
    {
        // Tests: Ensure Update is called on repository
        // Targets: _repository.Update(task); to ;
        var task = new Task { Id = 1, Description = "Old" };
        _mockRepo.Setup(r => r.GetById(1)).Returns(task);
        
        _manager.UpdateTask(1, "New");
        
        // Explicitly verify Update was called
        _mockRepo.Verify(r => r.Update(task), Times.Once);
        _mockRepo.Verify(r => r.Update(It.IsAny<Task>()), Times.Once);
    }

    [TestMethod]
    public void AddTask_ShouldCreateTaskWithCorrectProperties()
    {
        // Tests: Verify task creation with all properties
        Task? capturedTask = null;
        _mockRepo.Setup(r => r.Add(It.IsAny<Task>()))
                 .Callback<Task>(t => capturedTask = t);
        
        var deadline = DateTime.Now.AddDays(5);
        _manager.AddTask("Test Description", "Work", deadline);
        
        Assert.IsNotNull(capturedTask);
        Assert.AreEqual("Test Description", capturedTask.Description);
        Assert.AreEqual("Work", capturedTask.Category);
        Assert.AreEqual(deadline, capturedTask.Deadline);
        Assert.IsFalse(capturedTask.IsCompleted);
    }

    [TestMethod]
    public void GetTasksByCategory_ShouldReturnExactResult()
    {
        // Tests: Verify GetTasksByCategory returns exact repository result
        var expectedTasks = new List<Task> 
        { 
            new Task { Category = "Work", Description = "Task1" },
            new Task { Category = "Work", Description = "Task2" }
        };
        
        _mockRepo.Setup(r => r.GetAllByCategory("Work")).Returns(expectedTasks);
        
        var result = _manager.GetTasksByCategory("Work");
        
        Assert.AreSame(expectedTasks, result);
    }
}