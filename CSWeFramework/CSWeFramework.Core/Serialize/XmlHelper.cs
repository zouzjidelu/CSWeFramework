using System.IO;
using System.Xml.Serialization;

namespace CSWeFramework.Core.Serialize
{
    public class XmlHelper
    {
        /// <summary>
        /// xml转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlStr"></param>
        /// <returns></returns>
        public static T XmlToObject<T>(string xmlStr) where T : new()
        {
            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlStr)))
            {
                XmlSerializer serialize = new XmlSerializer(typeof(T));
                return (T)serialize.Deserialize(stream);
            }
        }

        /// <summary>
        /// 对象转xml
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToXml<T>(T obj) where T : new()
        {
            XmlSerializer serialize = new XmlSerializer(typeof(T));
            Stream stream = new MemoryStream();
            serialize.Serialize(stream, obj);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
