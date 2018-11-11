using CSWeFramework.Core.Infrastucture;
using CSWeFramework.Service.Cars;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Service
{
    /// <summary>
    /// 业务层容器注册。
    /// 如果所有的业务层需要的注册到容器的接口，就会导致这个方法很大。
    /// 所以可以分类注册
    /// 比如是car类的
    /// 就是是CarServiceRegister,UserServiceRegister
    /// </summary>
    public class CarServiceRegister : IDependencyRegister
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<ICarService, CarService>();
        }
    }
}
