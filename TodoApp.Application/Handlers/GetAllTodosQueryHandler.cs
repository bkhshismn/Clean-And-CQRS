using MediatR;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Queries;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Handlers;

public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoItem>>
{
    private readonly ITodoRepository _repository;

    public GetAllTodosQueryHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TodoItem>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}