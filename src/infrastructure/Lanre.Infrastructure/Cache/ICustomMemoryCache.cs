namespace Lanre.Infrastructure.Cache
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    public interface ICustomMemoryCache : IMemoryCache
    {
        void AddKey(string key);

        void RemoveKey(string key);

        void RemoveStartingKeys(string startingKey);

        void RemoveKeys(IEnumerable<string> keys);

        IEnumerable<string> GetKeysStarting(string startingKey);


        void RemoveStartingBy(string startingKey);

    }
}
