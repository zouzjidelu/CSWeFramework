using CSWeFramework.Core.Cache;
using CSWeFramework.Core.Infrastucture;
using CSWeFramework.Core.Logs;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Service
{
    /// <summary>
    /// 基础设施注册
    /// </summary>
    public class InfrastuctureRegister : IDependencyRegister
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<ILogger, NullLogger>();
            container.RegisterType<ICacheManager, MemoryCacheManager>();
        }
    }
}
