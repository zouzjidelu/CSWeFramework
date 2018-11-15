using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Mvc;

namespace CSWeFramework.Web.Core.Mvc
{
    public class JsonNetResult : JsonResult
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            JsonSerializerSettings jsonSerializerSettings = this.JsonSerializerSettings ?? new JsonSerializerSettings();
            //忽略循环引用，不要序列化。如果设置 为Error，则遇到循环引用的时候报错
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//json中属性开头字母小写的驼峰命名
            jsonSerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            var json = JsonConvert.SerializeObject(Data, Formatting.None, jsonSerializerSettings);
            response.Write(json);
        }
    }
}
