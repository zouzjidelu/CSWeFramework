using AutoMapper;
using CSWeFramework.Core.Domain;
using CSWeFramework.Service.Cars;
using CSWeFramework.Web.Models.Car;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CSWeFramework.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService carService;
        private readonly IMapper mapper;
        private readonly MapperConfiguration mapperConfiguration;
        public CarController(ICarService carService, IMapper mapper, MapperConfiguration mapperConfiguration)
        {
            this.carService = carService;
            this.mapper = mapper;
            this.mapperConfiguration = mapperConfiguration;
        }

        // GET: Car
        public ActionResult Index()
        {
            List<Car> cars = carService.GetCars();
            List<CarViewModel> carList = mapper.Map<List<Car>, List<CarViewModel>>(cars);
            return View(carList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CarViewModel car)
        {
            if (ModelState.IsValid)
            {
                Car model = mapper.Map<CarViewModel, Car>(car);
                model.CreateTime = DateTime.Now;
                this.carService.CreateCar(model);
            }

            return RedirectToAction("Index");
        }
    }
}