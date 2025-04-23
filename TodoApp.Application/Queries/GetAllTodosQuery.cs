using MediatR;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Queries;

public class GetAllTodosQuery : IRequest<List<TodoItem>>
{
}