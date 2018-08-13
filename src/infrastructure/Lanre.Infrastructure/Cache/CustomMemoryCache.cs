namespace Lanre.Infrastructure.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;

    // Todo: refactor this shit
    public class CustomMemoryCache : MemoryCache, ICustomMemoryCache
    {
        public static IList<string> _cacheKeys = new List<string>();

        public CustomMemoryCache(IOptions<MemoryCacheOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        public void AddKey(string key)
        {
            if (!_cacheKeys.Contains(key))
            {
                _cacheKeys.Add(key);
            }
        }

        public void RemoveKey(string key)
        {
            if (_cacheKeys.Contains(key))
            {
                _cacheKeys.Remove(key);
            }
        }

        public void RemoveStartingKeys(string startingKey)
        {
            var keys = this.GetKeysStarting(startingKey);
            foreach (var key in keys.ToList())
            {
                _cacheKeys.Remove(key);
            }
        }
        public void RemoveKeys(IEnumerable<string> keys)
        {
            foreach (var key in keys.ToList())
            {
                _cacheKeys.Remove(key);
            }
        }

        public IEnumerable<string> GetKeysStarting(string startingKey)
        {
            var keys = _cacheKeys.Where(x => x.StartsWith(startingKey, StringComparison.InvariantCultureIgnoreCase));
            return keys;
        }

        public void RemoveStartingBy(string startingKey)
        {
            var keys = this.GetKeysStarting(startingKey);

            if (keys == null) return;

            foreach (var key in keys.ToList())
            {
                this.Remove(key);
            }

            this.RemoveKeys(keys);
        }

    }
}
