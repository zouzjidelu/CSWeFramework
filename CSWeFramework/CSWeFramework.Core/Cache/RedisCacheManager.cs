using System;

namespace CSWeFramework.Core.Cache
{
    /// <summary>
    /// Redis缓存管理
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            throw new NotImplementedException();
        }
    }
}
