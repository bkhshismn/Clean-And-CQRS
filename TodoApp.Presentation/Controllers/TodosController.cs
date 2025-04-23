using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Commands;
using TodoApp.Application.Queries;
using TodoApp.Domain.Entities;

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/todos
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TodoItem>>> GetAllTodos()
    {
        var todos = await _mediator.Send(new GetAllTodosQuery());
        return Ok(todos);
    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TodoItem>> GetTodoById(int id)
    {
        try
        {
            var todo = await _mediator.Send(new GetTodoByIdQuery { Id = id });
            return Ok(todo);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: api/todos
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItem>> CreateTodo([FromBody] CreateTodoCommand command)
    {
        var todoItem = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTodoById), new { id = todoItem }, todoItem);
    }

    // PUT: api/todos/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID mismatch.");
        }

        try
        {
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    // DELETE: api/todos/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        try
        {
            await _mediator.Send(new DeleteTodoCommand { Id = id });
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}
