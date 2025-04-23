using MediatR;

namespace TodoApp.Application.Commands;

public class UpdateTodoCommand : IRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
}