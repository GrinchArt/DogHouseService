namespace DogHouseService.Infrastructure.MiddleWare
{
    public class TokenBucketRateLimitingOptions
    {
        public int BucketCapacity { get; set; }
        public int TokensPerInterval { get; set; }
        public TimeSpan Interval { get; set; }
    }
}
