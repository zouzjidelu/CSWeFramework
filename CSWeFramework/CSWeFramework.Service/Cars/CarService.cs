using CSWeFramework.Core.Cache;
using CSWeFramework.Core.Data;
using CSWeFramework.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Service.Cars
{
    /// <summary>
    /// car业务实现类
    /// </summary>
    public class CarService : ICarService
    {
        //使用仓储，于ef解耦。，依赖抽象
        private readonly IRepository<Car> carRepository;
        //不依赖具体的缓存管理，依赖抽象
        private readonly ICacheManager cacheManager;

        private const string CarCacheKey = nameof(CarService) + nameof(Car);

        public CarService(IRepository<Car> carRepository, ICacheManager cacheManager)
        {
            this.carRepository = carRepository;
            this.cacheManager = cacheManager;
        }

        public void CreateCar(Car car)
        {
            this.carRepository.Insert(car);
            //新增清除缓存
            this.cacheManager.Clear();
        }

        public void DeleteCar(Car car)
        {
            this.carRepository.Delete(car);
        }

        public List<Car> GetCars()
        {
            List<Car> cars = null;
            if (this.cacheManager.Contains(CarCacheKey))
            {
                cars = this.cacheManager.Get<List<Car>>(CarCacheKey);
            }
            else
            {
                cars = this.carRepository.Table.ToList();
                this.cacheManager.Set(CarCacheKey, cars, TimeSpan.FromHours(1));
            }

            return cars;
        }

        public void UpdateCar(Car car)
        {
            this.carRepository.Update(car);
        }
    }
}
