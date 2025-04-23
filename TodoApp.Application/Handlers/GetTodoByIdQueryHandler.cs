using MediatR;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Queries;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Handlers;

public class GetTodoByIdQueryHandler : IRequestHandler<GetTodoByIdQuery, TodoItem>
{
    private readonly ITodoRepository _repository;

    public GetTodoByIdQueryHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoItem> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id);
        if (todo == null)
        {
            throw new Exception($"Todo with ID {request.Id} not found.");
        }
        return todo;
    }
}