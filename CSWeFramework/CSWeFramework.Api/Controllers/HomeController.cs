using CSWeFramework.Core.Domain;
using CSWeFramework.Web.Core.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CSWeFramework.Api.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public JsonResult GetJson()
        {
            List<Car> cars = new List<Car>()
            {
                new Car(){ID=1,Name="a",Price=83,CreateTime=DateTime.Now},
                new Car(){ID=2,Name="b",Price=83,CreateTime=DateTime.Now},
                new Car(){ID=3,Name="c",Price=83,CreateTime=DateTime.Now},
            };
            return Json(cars, JsonRequestBehavior.AllowGet);
        }
    }
}
