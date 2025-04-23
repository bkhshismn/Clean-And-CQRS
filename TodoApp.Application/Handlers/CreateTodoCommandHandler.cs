using MediatR;
using TodoApp.Application.Commands;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Handlers;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
{
    private readonly ITodoRepository _repository;

    public CreateTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = new TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            IsCompleted = request.IsCompleted
        };
        await _repository.AddAsync(todo);
        return todo.Id;
    }
}