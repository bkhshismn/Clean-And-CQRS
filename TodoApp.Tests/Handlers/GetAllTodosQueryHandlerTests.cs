using Moq;
using TodoApp.Application.Queries;
using TodoApp.Application.Handlers;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using Xunit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Tests.Handlers
{
    public class GetAllTodosQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsAllTodos()
        {
            // Arrange
            var mockRepo = new Mock<ITodoRepository>();
            var handler = new GetAllTodosQueryHandler(mockRepo.Object);

            var todos = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Todo 1", Description = "Description 1", IsCompleted = false },
                new TodoItem { Id = 2, Title = "Todo 2", Description = "Description 2", IsCompleted = true }
            };

            mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                    .ReturnsAsync(todos); 

            // Act
            var result = await handler.Handle(new GetAllTodosQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result); 
            Assert.Equal(2, result.Count); 
            Assert.Equal("Todo 1", result[0].Title); 
            Assert.Equal("Todo 2", result[1].Title);
            mockRepo.Verify(r => r.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
