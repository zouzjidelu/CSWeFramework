using CSWeFramework.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Service.Cars
{
    /// <summary>
    /// car服务接口类
    /// </summary>
    public interface ICarService
    {
        void CreateCar(Car car);

        void DeleteCar(Car car);

        void UpdateCar(Car car);

        List<Car> GetCars();
    }
}
