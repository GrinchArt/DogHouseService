using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Infrastructure.MiddleWare
{
    public class TokenBucketRateLimitingOptions
    {
        public int BucketCapacity { get; set; }
        public int TokensPerInterval { get; set; }
        public TimeSpan Interval { get; set; }
    }
}
