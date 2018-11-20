using CSWeFramework.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSWeFramework.Web.Mvc
{
   /// <summary>
   /// 自定义ViewPage，继承WebViewPage
   /// </summary>
   /// <typeparam name="TModel">展示视图中的模型</typeparam>
    public abstract class CustomViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        /// <summary>
        /// 通过key找到资源模板中的值
        /// </summary>
        /// <param name="key">资源模板中的key</param>
        /// <returns>资源模板对应的值</returns>
        public string T(string key)
        {
            return Resources.ResourceManager.GetString(key);
        }
    }
}