using Moq;
using TodoApp.Application.Commands;
using TodoApp.Application.Handlers;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Tests.Handlers
{
    public class CreateTodoCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsNewTodoId()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var handler = new CreateTodoCommandHandler(mockRepo.Object);

            var command = new CreateTodoCommand
            {
                Title = "Test Title",
                Description = "Test Description",
                IsCompleted = false
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(1); // Mock: برگشتن آیدی

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
            mockRepo.Verify(r => r.AddAsync(It.IsAny<TodoItem>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
