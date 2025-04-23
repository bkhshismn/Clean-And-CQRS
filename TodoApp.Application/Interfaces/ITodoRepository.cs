using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces;

public interface ITodoRepository
{
    Task<int> AddAsync(TodoItem todo, CancellationToken cancellationToken = default);
    Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(TodoItem todo, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<List<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default);
}
