
using TodoApp.Application.Commands;
using TodoApp.Application.Interfaces;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTodoCommand).Assembly));
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateTodoCommand>());
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();