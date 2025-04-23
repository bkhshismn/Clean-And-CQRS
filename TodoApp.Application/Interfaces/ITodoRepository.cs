using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces;
public interface ITodoRepository
{
    Task AddAsync(TodoItem todo);
    Task<TodoItem> GetByIdAsync(int id);
    Task UpdateAsync(TodoItem todo);
    Task DeleteAsync(int id);
    Task<List<TodoItem>> GetAllAsync();
}