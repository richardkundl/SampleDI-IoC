using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace SampleDI.Cache
{
    public class SystemRuntimeCacheStorage : ICacheStorage
    {
        public void Remove(string key)
        {
            var cache = MemoryCache.Default;
            cache.Remove(key);
        }

        public void Set(string key, object value)
        {
            var cache = MemoryCache.Default;
            var policy = new CacheItemPolicy
	    {
		AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(3)
	    };

	    cache.Set(key, value, policy);
        }

        public void Set(string key, object data, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            var cache = MemoryCache.Default;
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = slidingExpiration,
            };

            cache.Set(key, data, policy);
        }

        public T Get<T>(string key)
        {
            var cache = MemoryCache.Default;

            return cache.Contains(key) ? (T)cache[key] : default(T);
        }
    }
}
