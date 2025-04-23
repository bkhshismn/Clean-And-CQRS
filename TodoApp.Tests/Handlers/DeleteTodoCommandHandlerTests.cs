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
    public class DeleteTodoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidId_DeletesTodo()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var handler = new DeleteTodoCommandHandler(mockRepo.Object);

            var command = new DeleteTodoCommand { Id = 1 };
            var todo = new TodoItem { Id = 1, Title = "Test Todo", Description = "Test Description" };

            mockRepo.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(todo);

            mockRepo.Setup(r => r.DeleteAsync(command.Id, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask); 

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            mockRepo.Verify(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
            mockRepo.Verify(r => r.DeleteAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once); 
        }

        [Fact]
        public async Task Handle_TodoNotFound_ThrowsException()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var handler = new DeleteTodoCommandHandler(mockRepo.Object);

            var command = new DeleteTodoCommand { Id = 99 };

            mockRepo.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TodoItem)null); 

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
            Assert.Equal("Todo with ID 99 not found.", exception.Message);
        }
    }
}
