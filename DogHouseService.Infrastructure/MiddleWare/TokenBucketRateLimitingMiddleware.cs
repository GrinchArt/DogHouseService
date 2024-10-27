using DogHouseService.Application.Interfaces;
using DogHouseService.Infrastructure.MiddleWare;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace DogHouseService.Infrastructure.RateLimiting
{
    public class TokenBucketRateLimitingMiddleware : IRateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConcurrentDictionary<string, TokenBucket> _buckets = new();
        private readonly int _bucketCapacity;
        private readonly int _tokensPerInterval;
        private readonly TimeSpan _interval;

        private readonly ILogger<TokenBucketRateLimitingMiddleware> _logger;

        public TokenBucketRateLimitingMiddleware(RequestDelegate next, IOptions<TokenBucketRateLimitingOptions> options, ILogger<TokenBucketRateLimitingMiddleware> logger)
        {
            _next = next;
            var opts = options.Value;
            _bucketCapacity = opts.BucketCapacity;
            _tokensPerInterval = opts.TokensPerInterval;
            _interval = opts.Interval;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (ipAddress == null)
            {
                _logger.LogWarning("Request received without a valid IP address.");
                await _next(context);
                return;
            }

            var bucket = _buckets.GetOrAdd(ipAddress, _ => new TokenBucket(_bucketCapacity, _tokensPerInterval, _interval));

            if (!bucket.TryConsume())
            {
                _logger.LogWarning("Too many requests from IP: {IpAddress}. Limit exceeded.", ipAddress);
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            _logger.LogInformation("Request from IP: {IpAddress} processed successfully.", ipAddress);
            await _next(context);
        }
    }
}
