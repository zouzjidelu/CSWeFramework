using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CSWeFramework.Web.Mvc
{
    /// <summary>
    /// 语言过滤器，在FilterConfig中注入
    /// </summary>
    public class LanuageActionFilter : IActionFilter
    {
        /// <summary>
        /// 在action方法之后调用
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        /// <summary>
        /// 在action方法调用之前调用
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var values = filterContext.RouteData.Values["lg"];
            if (values != null)
            {
                //当程序运行时，CurrentUICulture属性用来决定加载什么样的资源，而CurrentCulture属性用来决定诸如货币、数字和日期如何格式化。
                //1.CurrentCulture属性的默认值是操作系统的User Locale，我们可以在控制面板里设置。
                //2.CurrentUICulture属性的默认值是操作系统用户界面语言。

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(values.ToString());
                Thread.CurrentThread.CurrentCulture = new CultureInfo(values.ToString());
            }
        }
    }
}