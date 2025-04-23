using MediatR;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Queries;

public class GetTodoByIdQuery : IRequest<TodoItem>
{
    public int Id { get; set; }
}