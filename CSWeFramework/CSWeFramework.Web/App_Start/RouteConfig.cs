﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CSWeFramework.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{lg}/{controller}/{action}/{id}",
            //    defaults: new { lg="zh-cn", controller = "Car", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
              name: "Default",
              url: "{controller}/{action}/{id}",
              defaults: new {  controller = "Car", action = "Index", id = UrlParameter.Optional }
          );
        }
    }
}
