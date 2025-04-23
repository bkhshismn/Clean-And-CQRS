using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;
using Xunit;

namespace TodoApp.Tests.Repositories;

public class TodoRepositoryTests : IDisposable
{
    private readonly TodoDbContext _context;
    private readonly TodoRepository _repository;

    public TodoRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<TodoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new TodoDbContext(options);
        _repository = new TodoRepository(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task AddAsync_ShouldAddTodoItem()
    {
        // Arrange
        var todo = new TodoItem
        {
            Title = "Test Todo",
            Description = "Test Description",
            IsCompleted = false
        };

        // Act
        await _repository.AddAsync(todo);
        var result = await _context.TodoItems.FindAsync(todo.Id);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be("Test Todo");
        result.Description.Should().Be("Test Description");
        result.IsCompleted.Should().BeFalse();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTodoItem_WhenExists()
    {
        // Arrange
        var todo = new TodoItem
        {
            Title = "Test Todo",
            Description = "Test Description",
            IsCompleted = false
        };
        await _context.TodoItems.AddAsync(todo);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByIdAsync(todo.Id);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(todo.Id);
        result.Title.Should().Be("Test Todo");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenNotExists()
    {
        // Arrange

        // Act
        var result = await _repository.GetByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTodoItems()
    {
        // Arrange
        var todos = new List<TodoItem>
        {
            new() { Title = "Todo 1", Description = "Desc 1", IsCompleted = false },
            new() { Title = "Todo 2", Description = "Desc 2", IsCompleted = true }
        };
        await _context.TodoItems.AddRangeAsync(todos);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetAllAsync();

        // Assert
        result.Should().HaveCount(2);
        result.Should().Contain(t => t.Title == "Todo 1" && t.IsCompleted == false);
        result.Should().Contain(t => t.Title == "Todo 2" && t.IsCompleted == true);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTodoItem()
    {
        // Arrange
        var todo = new TodoItem
        {
            Title = "Original Todo",
            Description = "Original Description",
            IsCompleted = false
        };
        await _context.TodoItems.AddAsync(todo);
        await _context.SaveChangesAsync();

        // Act
        todo.Title = "Updated Todo";
        todo.Description = "Updated Description";
        todo.IsCompleted = true;
        await _repository.UpdateAsync(todo);
        var result = await _context.TodoItems.FindAsync(todo.Id);

        // Assert
        result.Title.Should().Be("Updated Todo");
        result.Description.Should().Be("Updated Description");
        result.IsCompleted.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveTodoItem_WhenExists()
    {
        // Arrange
        var todo = new TodoItem
        {
            Title = "Test Todo",
            Description = "Test Description",
            IsCompleted = false
        };
        await _context.TodoItems.AddAsync(todo);
        await _context.SaveChangesAsync();

        // Act
        await _repository.DeleteAsync(todo.Id);
        var result = await _context.TodoItems.FindAsync(todo.Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_ShouldDoNothing_WhenNotExists()
    {
        // Arrange
        // Act
        await _repository.DeleteAsync(999);

        // Assert
        var count = await _context.TodoItems.CountAsync();
        count.Should().Be(0);
    }
}