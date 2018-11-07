using System;
using System.Runtime.Caching;

namespace CSWeFramework.Core.Cache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        public void Clear()
        {
            foreach (var cache in MemoryCache.Default)
            {
                this.Remove(cache.Key);
            }
        }

        public bool Contains(string key)
        {
            return MemoryCache.Default.Contains(key);
        }

        public T Get<T>(string key)
        {
            if (this.Contains(key))
            {
                return (T)MemoryCache.Default.Get(key);
            }
            return default(T);
        }

        public void Remove(string key)
        {
            MemoryCache.Default.Remove(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                MemoryCache.Default.Set(key, value, new CacheItemPolicy() { SlidingExpiration = cacheTime });
            }
        }
    }
}
