using MediatR;
using TodoApp.Application.Commands;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Handlers;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, Unit>
{
    private readonly ITodoRepository _repository;

    public DeleteTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var todo = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (todo is null)
        {
            throw new KeyNotFoundException($"Todo with ID {request.Id} not found.");
        }

        await _repository.DeleteAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}
