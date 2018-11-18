using CSWeFramework.Core.Domain;
using CSWeFramework.Service.Cars;
using CSWeFramework.Web.Models.Car;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CSWeFramework.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
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

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CarViewModel car)
        {
            if (!ModelState.IsValid)
            {
                var keys=ModelState.Keys;
                var values=ModelState.Values;
            }

            return RedirectToAction("Index");
        }
    }
}