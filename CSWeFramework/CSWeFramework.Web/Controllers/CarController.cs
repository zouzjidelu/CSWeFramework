using CSWeFramework.Core.Domain;
using CSWeFramework.Service.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSWeFramework.Web.Controllers
{
    /// <summary>
    /// car 控制器
    /// </summary>
    public class CarController : Controller
    {
        public readonly ICarService carService;
        /// <summary>
        /// 注入carservice
        /// </summary>
        /// <param name="carService"></param>
        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        // GET: Car
        public ActionResult Index()
        {
            List<Car> cars = carService.GetCars();
            return View(cars);
        }
    }
}