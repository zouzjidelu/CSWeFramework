using CSWeFramework.Core.Config;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Text;

namespace CSWeFramework.Core.Cache
{
    /// <summary>
    /// Redis缓存管理
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {
        private readonly string redisConnectionString;

        /// <summary>
        /// 与redis服务器相互关联的组件
        /// </summary>
        private volatile ConnectionMultiplexer redisConnection;

        private readonly object redisConnectionLock = new object();

        /// <summary>
        /// 将redis配置信息读取出来
        /// </summary>
        /// <param name="config"></param>
        public RedisCacheManager(ApplicationConfig config)
        {
            if (string.IsNullOrEmpty(config.RedisCacheConfig.ConnectionString))
            {
                throw new ArgumentException("Redis connection string is Empty");
            }
            //得到redis连接字符串
            this.redisConnectionString = config.RedisCacheConfig.ConnectionString;
            //获得redis连接
            this.redisConnection = this.GetRedisConnection();
        }

        /// <summary>
        /// 获得redis连接
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetRedisConnection()
        {
            //redis连接不为null并且是连接状态，则直接返回当前连接
            if (this.redisConnection != null && this.redisConnection.IsConnected)
            {
                return this.redisConnection;
            }

            lock (redisConnectionLock)
            {
                //如果连接不是null，正在尝试连接，则直接释放
                if (this.redisConnection != null)
                {
                    this.redisConnection.Dispose();
                }
                this.redisConnection = ConnectionMultiplexer.Connect(this.redisConnectionString);
            }

            return this.redisConnection;
        }

        public void Clear()
        {
            foreach (var endPoint in this.redisConnection.GetEndPoints())
            {
                var server = this.redisConnection.GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    this.Remove(key);
                }
            }
        }

        public bool Contains(string key)
        {
            return this.redisConnection.GetDatabase().KeyExists(key);
        }

        public T Get<T>(string key)
        {
            var value = this.redisConnection.GetDatabase().StringGet(key);
            if (value.HasValue)
            {
                return this.Deserialize<T>(value);
            }
            return default(T);
        }

        public void Remove(string key)
        {
            this.redisConnection.GetDatabase().KeyDelete(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                byte[] bytes = this.Serialize(value);
                this.redisConnection.GetDatabase().StringSet(key, bytes, cacheTime);
            }
        }


        protected T Deserialize<T>(byte[] bytes)
        {
            string value = Encoding.UTF8.GetString(bytes);
            return JsonConvert.DeserializeObject<T>(value);
        }

        protected byte[] Serialize(object value)
        {
            string strValue = JsonConvert.SerializeObject(value);
            return Encoding.UTF8.GetBytes(strValue);
        }
    }
}
