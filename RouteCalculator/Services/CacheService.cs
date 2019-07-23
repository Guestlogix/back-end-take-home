using System;
using Microsoft.Extensions.Caching.Memory;
using RouteCalculator.Contracts;

namespace RouteCalculator.Services
{
    public class CacheService : ICacheService
    {
        public int DefaultDurationInMinutes { get; set; }
        private IMemoryCache _cache;

        public CacheService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            DefaultDurationInMinutes = 60;
        }

        public T GetEntity<T>(string key, Func<T> getEntityFromSource) where T : class
        {
            if (!_cache.TryGetValue(key, out T entity))
            {
                entity = getEntityFromSource();
                _cache.Set(key, entity, DateTimeOffset.Now.AddMinutes(DefaultDurationInMinutes));
            }

            return entity;
        }
    }
}