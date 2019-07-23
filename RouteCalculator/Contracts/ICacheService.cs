using System;

namespace RouteCalculator.Contracts
{
    public interface ICacheService
    {
        T GetEntity<T>(string key, Func<T> getEntityFromSource) where T : class;
    }
}
