using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ToDoListApp;

namespace ToDoListApp.Tests;

[TestClass]
public class UnitTests
{
    private Mock<ITaskRepository> _mockRepo;
    private ToDoManager _manager;

    [TestInitialize]
    public void Setup()
    {
        // Setup: Mock repository as test double (mock) to isolate manager logic.
        // Why: Per Martin Fowler, mocks verify interactions; used here to stub data without real storage.
        _mockRepo = new Mock<ITaskRepository>();
        _manager = new ToDoManager(_mockRepo.Object);
    }

    [TestMethod]
    public void AddTask_ShouldCallRepositoryAdd()
    {
        // Tests: What is being tested? AddTask method.
        // Should do: Add new task via repo.
        // Actual/Expected: Verify call; no return value.
        // Reproduce: Call with params.
        // Answers question 1: What are you testing? (AddTask isolation).
        _manager.AddTask("Test", "Cat", null);
        _mockRepo.Verify(r => r.Add(It.IsAny<Task>()), Times.Once);
    }

    [TestMethod]
    public void UpdateTask_ShouldUpdateDescriptionIfExists()
    {
        // Setup stub: Return task for update.
        var task = new Task { Id = 1, Description = "Old" };
        _mockRepo.Setup(r => r.GetById(1)).Returns(task);
        // Tests: What should it do? Change description.
        // Actual: Updated task passed to Update.
        // Expected: New desc.
        // Reproduce: With existing ID.
        // Answers question 2: What should it do? (Core functionality).
        _manager.UpdateTask(1, "New");
        Assert.AreEqual("New", task.Description);
        _mockRepo.Verify(r => r.Update(task), Times.Once);
    }

    [TestMethod]
    public void DeleteTask_ShouldCallRepositoryDelete()
    {
        // Tests: Actual output? No return, but interaction.
        // Expected: Delete called.
        // Reproduce: Any ID.
        // Answers question 3: What is the actual output? (Interaction verification).
        _manager.DeleteTask(1);
        _mockRepo.Verify(r => r.Delete(1), Times.Once);
    }

    [TestMethod]
    public void MarkCompleted_ShouldSetCompletedIfExists()
    {
        // Setup stub for edge: Existing task.
        var task = new Task { Id = 1, IsCompleted = false };
        _mockRepo.Setup(r => r.GetById(1)).Returns(task);
        // Tests: Expected output? Completed true.
        // Reproduce: With ID.
        // Answers question 4: What is the expected output? (State change).
        _manager.MarkCompleted(1);
        Assert.IsTrue(task.IsCompleted);
    }

    [TestMethod]
    public void GetTasksByCategory_ShouldReturnFromRepo()
    {
        // Setup stub: Return list.
        var tasks = new List<Task> { new Task { Category = "Cat" } };
        _mockRepo.Setup(r => r.GetAllByCategory("Cat")).Returns(tasks);
        // Tests: How reproduce? Call Get.
        // Answers question 5: How can the test be reproduced? (Query isolation).
        var result = _manager.GetTasksByCategory("Cat");
        CollectionAssert.AreEqual(tasks, result.ToList());
    }

    // Specification-based test: Based on req "categorize tasks".
    [TestMethod]
    public void AddTask_WithCategory_ShouldSetCategory()
    {
        // Spec: Tasks must have category.
        var capturedTask = new Task();
        _mockRepo.Setup(r => r.Add(It.IsAny<Task>())).Callback<Task>(t => capturedTask = t);
        _manager.AddTask("Test", "Work", null);
        Assert.AreEqual("Work", capturedTask.Category);
    }
}