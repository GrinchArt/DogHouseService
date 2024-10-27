using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using DogHouseService.Infrastructure.MiddleWare;
using DogHouseService.Infrastructure.RateLimiting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;

namespace DogHouseService.Tests.Middlewares
{
    public class TokenBucketRateLimitingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_ShouldLogWarning_WhenTooManyRequests()
        {
            // Arrange
            var context = new DefaultHttpContext();
            context.Connection.RemoteIpAddress = IPAddress.Parse("127.0.0.1");

            var next = new Mock<RequestDelegate>();
            next.Setup(n => n(context)).Returns(Task.CompletedTask);

            var options = Options.Create(new TokenBucketRateLimitingOptions
            {
                BucketCapacity = 2,
                TokensPerInterval = 1,
                Interval = TimeSpan.FromMinutes(1)
            });

            var loggerMock = new Mock<ILogger<TokenBucketRateLimitingMiddleware>>();

            var middleware = new TokenBucketRateLimitingMiddleware(next.Object, options, loggerMock.Object);

            for (int i = 0; i < 3; i++)
            {
                await middleware.InvokeAsync(context);
            }

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            loggerMock.Verify(l => l.Log(
                LogLevel.Warning,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Too many requests")),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.AtLeastOnce);
        }

    }
}

