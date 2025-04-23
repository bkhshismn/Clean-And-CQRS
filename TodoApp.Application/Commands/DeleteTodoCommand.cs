using MediatR;

namespace TodoApp.Application.Commands;

public class DeleteTodoCommand : IRequest<Unit>
{
    public int Id { get; set; }
}