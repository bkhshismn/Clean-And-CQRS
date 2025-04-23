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

        if (todo is null)
            throw new KeyNotFoundException($"Todo with ID {request.Id} was not found.");

        return todo;
    }
}
