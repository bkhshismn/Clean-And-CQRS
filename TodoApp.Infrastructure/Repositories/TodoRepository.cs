using TodoApp.Domain.Entities;
using TodoApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using TodoApp.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApp.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(TodoItem todo, CancellationToken cancellationToken = default)
        {
            await _context.TodoItems.AddAsync(todo, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return todo.Id;
        }

        public async Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.TodoItems.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task UpdateAsync(TodoItem todo, CancellationToken cancellationToken = default)
        {
            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var todo = await _context.TodoItems.FindAsync(new object[] { id }, cancellationToken);
            if (todo != null)
            {
                _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<List<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.TodoItems.ToListAsync(cancellationToken);
        }
    }
}
