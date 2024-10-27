using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Infrastructure.MiddleWare
{
    public class TokenBucket
    {
        private readonly int _capacity;
        private readonly int _tokensPerInterval;
        private readonly TimeSpan _interval;
        private int _availableTokens;
        private DateTime _lastRefill;

        public TokenBucket(int capacity, int tokensPerInterval, TimeSpan interval)
        {
            _capacity = capacity;
            _tokensPerInterval = tokensPerInterval;
            _interval = interval;
            _availableTokens = capacity;
            _lastRefill = DateTime.UtcNow;
        }

        private void Refill()
        {
            var now = DateTime.UtcNow;
            var tokensToAdd = (int)((now - _lastRefill).TotalMilliseconds / _interval.TotalMilliseconds) * _tokensPerInterval;
            if (tokensToAdd > 0)
            {
                _availableTokens = Math.Min(_capacity, _availableTokens + tokensToAdd);
                _lastRefill = now;
            }
        }

        public bool TryConsume()
        {
            lock (this)
            {
                Refill();
                if (_availableTokens > 0)
                {
                    _availableTokens--;
                    return true;
                }
                return false;
            }
        }
    }
}
