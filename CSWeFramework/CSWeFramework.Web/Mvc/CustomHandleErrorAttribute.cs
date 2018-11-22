using CSWeFramework.Core.Infrastucture;
using CSWeFramework.Core.Logs;
using System.Web.Mvc;

namespace CSWeFramework.Web.Mvc
{
    /// <summary>
    /// 自定义异常处理handle
    /// </summary>
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //标识异常已被捕捉。不再往上抛
            filterContext.ExceptionHandled = true;
            ILogger logger = ServiceContainer.Resolve<ILogger>();
            logger.Error("发现未处理的异常", filterContext.Exception);

            filterContext.Result=new ViewResult() { ViewName = "Error",ViewData=new ViewDataDictionary(filterContext.Exception) }; 
        }
    }
}