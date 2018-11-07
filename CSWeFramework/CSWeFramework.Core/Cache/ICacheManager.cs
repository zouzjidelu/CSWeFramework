using System;

namespace CSWeFramework.Core.Cache
{
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 根据key获取缓存
        /// </summary>
        /// <typeparam name="T">缓存数据类型</typeparam>
        /// <param name="key">键</param>
        /// <returns>缓存数据</returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存信息
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">缓存数据</param>
        /// <param name="cacheTime">缓存时间</param>
        void Set(string key, object value, TimeSpan cacheTime);

        /// <summary>
        /// 根据key缓存是否存在
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>true：存在，false:不存在</returns>
        bool Contains(string key);

        /// <summary>
        /// 根据key删除一个缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// Clear All Cache Data
        /// </summary>
        void Clear();
    }
}
