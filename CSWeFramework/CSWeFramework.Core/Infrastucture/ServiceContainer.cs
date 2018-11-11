using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Core.Infrastucture
{
    /// <summary>
    /// 注册服务容器
    /// 所有容器注册都从这里获取入口注册，比如仓储注册，业务层注册
    /// </summary>
    public class ServiceContainer
    {
        /// <summary>
        /// 延时加载，需要的时候才加载，
        /// 传入unity容器，也可传入其他容器，具体实现需要自己在参数委托中实现，这里用untiy容器
        /// </summary>
        static Lazy<IUnityContainer> Container = new Lazy<IUnityContainer>(() => { return new UnityContainer(); });

        /// <summary>
        /// 获取当前容器,类似于web中的当前的httpcontext
        /// </summary>
        public static IUnityContainer Current { get { return Container.Value; } }

        public static T Resolve<T>() where T : class
        {
            return Container.Value.Resolve<T>();
        }

        public static IEnumerable<T> ResolveAll<T>() where T : class
        {
            return Container.Value.ResolveAll<T>();
        }
    }
}
