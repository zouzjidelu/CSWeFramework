using CSWeFramework.Web.Mvc;
using System.Web;
using System.Web.Mvc;

namespace CSWeFramework.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //引入语言过滤器
            filters.Add(new LanuageActionFilter());
        }
    }
}
