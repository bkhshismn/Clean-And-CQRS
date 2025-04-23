using Moq;
using TodoApp.Application.Queries;
using TodoApp.Application.Handlers;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Tests.Handlers
{
    public class GetTodoByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_TodoExists_ReturnsTodoItem()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var handler = new GetTodoByIdQueryHandler(mockRepo.Object);

            var todo = new TodoItem { Id = 1, Title = "Test Todo", Description = "Test Description", IsCompleted = false };

            mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(todo); 

            // Act
            var result = await handler.Handle(new GetTodoByIdQuery { Id = 1 }, CancellationToken.None);

            // Assert
            Assert.NotNull(result); 
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Todo", result.Title);
            mockRepo.Verify(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()), Times.Once); 
        }

        [Fact]
        public async Task Handle_TodoNotFound_ThrowsException()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var handler = new GetTodoByIdQueryHandler(mockRepo.Object);

            mockRepo.Setup(r => r.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                    .ReturnsAsync((TodoItem)null); 

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(new GetTodoByIdQuery { Id = 1 }, CancellationToken.None));
        }
    }
}
