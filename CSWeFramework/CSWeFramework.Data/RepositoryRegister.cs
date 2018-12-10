using CSWeFramework.Core.Data;
using CSWeFramework.Core.Infrastucture;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Data
{
    /// <summary>
    /// 容器注册
    /// </summary>
    public class RepositoryRegister : IDependencyRegister
    {
        public void RegisterType(IUnityContainer container)
        {
            //数据库上下文注册
            //container.RegisterType<IDbContext, CarDbContext>();//通过配置文件的方式注册，并注册为每次请求一个实例
            //仓储注册
            container.RegisterType(typeof(IRepository<>), typeof(EfRepository<>));

        }
    }
}
