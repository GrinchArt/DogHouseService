using Microsoft.AspNetCore.Http;

namespace DogHouseService.Application.Interfaces
{
    public interface IRateLimitingMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}
