using Newtonsoft.Json;

namespace CSWeFramework.Core.Serialize
{
    /// <summary>
    /// 序列化，反序列化帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// json字符串转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T StringToObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// 对象转json字符串
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ObjectToString<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
    }
}
