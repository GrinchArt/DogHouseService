using DogHouseService.Infrastructure.MiddleWare;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;

namespace DogHouseService.Infrastructure.RateLimiting
{
    public class TokenBucketRateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ConcurrentDictionary<string, TokenBucket> _buckets = new();
        private readonly int _bucketCapacity;
        private readonly int _tokensPerInterval;
        private readonly TimeSpan _interval;

        public TokenBucketRateLimitingMiddleware(RequestDelegate next, IOptions<TokenBucketRateLimitingOptions> options)
        {
            _next = next;
            var opts = options.Value;
            _bucketCapacity = opts.BucketCapacity;
            _tokensPerInterval = opts.TokensPerInterval;
            _interval = opts.Interval;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            if (ipAddress == null)
            {
                await _next(context);
                return;
            }

            var bucket = _buckets.GetOrAdd(ipAddress, _ => new TokenBucket(_bucketCapacity, _tokensPerInterval, _interval));

            if (!bucket.TryConsume())
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
                return;
            }

            await _next(context);
        }
    }
}
