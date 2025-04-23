using MediatR;
using TodoApp.Application.Commands;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Handlers;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, Unit>
{
    private readonly ITodoRepository _repository;

    public UpdateTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id);
        if (todo == null)
        {
            throw new Exception($"Todo with ID {request.Id} not found.");
        }

        todo.Title = request.Title;
        todo.Description = request.Description;
        todo.IsCompleted = request.IsCompleted;

        await _repository.UpdateAsync(todo);
        return Unit.Value;
    }
}