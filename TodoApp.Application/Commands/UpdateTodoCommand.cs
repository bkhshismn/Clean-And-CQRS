using MediatR;
using MediatR.Registration;


namespace TodoApp.Application.Commands;

public class UpdateTodoCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}