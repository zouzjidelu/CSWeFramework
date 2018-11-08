using System.Configuration;

namespace CSWeFramework.Core.Config
{
    /// <summary>
    /// 配置管理类
    /// <ApplicationConfig> <rediceCache enable="" connectionString=""></rediceCache></ApplicationConfig>
    /// </summary>
    public class ApplicationConfig : ConfigurationSection
    {
        private const string RedisCachePropertyName = "redisCache";
        public RedisCacheElement RedisCacheConfig
        {
            get { return (RedisCacheElement)base[RedisCachePropertyName]; }
            set { base[RedisCachePropertyName] = value; }
        }
    }
}
