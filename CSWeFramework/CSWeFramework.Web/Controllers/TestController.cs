using CSWeFramework.Service.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSWeFramework.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly ITestService testService;
        private readonly IGenericTestService<int> genericTestService;

        public TestController(ITestService testService,IGenericTestService<int> genericTestService)
        {
            this.testService = testService;
            this.genericTestService = genericTestService;
        }

        // GET: Test
        public ActionResult Index()
        {
            object result = this.testService.Test();
            return View(result);
        }

        public ActionResult IndexGeneric()
        {
            object result = this.genericTestService.Test(123);
            return View(result);
        }


    }
}