using Moq;
using TodoApp.Application.Commands;
using TodoApp.Application.Handlers;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using System;

namespace TodoApp.Tests.Handlers
{
    public class UpdateTodoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_UpdatesTodo()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var handler = new UpdateTodoCommandHandler(mockRepo.Object);

            var command = new UpdateTodoCommand
            {
                Id = 1,
                Title = "Updated Title",
                Description = "Updated Description",
                IsCompleted = true
            };

            var todo = new TodoItem
            {
                Id = 1,
                Title = "Old Title",
                Description = "Old Description",
                IsCompleted = false
            };

            mockRepo.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(todo); 

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask); 

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Once); 
            Assert.Equal("Updated Title", todo.Title);
            Assert.Equal("Updated Description", todo.Description);
            Assert.True(todo.IsCompleted);
        }

        [Fact]
        public async Task Handle_TodoNotFound_ThrowsException()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var handler = new UpdateTodoCommandHandler(mockRepo.Object);

            var command = new UpdateTodoCommand { Id = 99 }; 

            mockRepo.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TodoItem)null); 

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("Todo with ID 99 not found.", exception.Message);
        }
    }
}
