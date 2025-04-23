namespace TodoApp.Infrastructure.Repositories;
using TodoApp.Domain.Entities;
using TodoApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using TodoApp.Infrastructure.Data;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TodoItem todo)
    {
        await _context.TodoItems.AddAsync(todo);
        await _context.SaveChangesAsync();
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        return await _context.TodoItems.FindAsync(id);
    }

    public async Task UpdateAsync(TodoItem todo)
    {
        _context.TodoItems.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var todo = await _context.TodoItems.FindAsync(id);
        if (todo != null)
        {
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await _context.TodoItems.ToListAsync();
    }
}