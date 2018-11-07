using System.Configuration;

namespace CSWeFramework.Core.Config
{
    /// <summary>
    /// Reids缓存配置类
    /// <ApplicationConfig> <rediceCache enable="" connectionString=""></rediceCache></ApplicationConfig>
    /// </summary>
    public class RedisCacheElement : ConfigurationElement
    {
        private const string EnabledPropertyName = "enabled";

        private const string ConnectionStringNamePropertyName = "connectionString";
        /// <summary>
        /// 是否禁用
        /// </summary>
        [ConfigurationProperty(EnabledPropertyName, IsRequired = true)]/*当前属性(enabled)是否为必填项*/
        public bool Enabled
        {
            get { return (bool)base[EnabledPropertyName]; }
            set { base[EnabledPropertyName] = value; }
        }

        /// <summary>
        /// redis连接字符串
        /// </summary>
        [ConfigurationProperty(ConnectionStringNamePropertyName, IsRequired = true)]/*当前属性(connectionString)是否为必填项*/
        public string ConnectionString
        {
            get { return (string)base[ConnectionStringNamePropertyName]; }
            set { base[ConnectionStringNamePropertyName] = value; }
        }
    }
}
