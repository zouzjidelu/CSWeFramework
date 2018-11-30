using CSWeFramework.Core.Config;
using CSWeFramework.Core.Infrastucture;
using CSWeFramework.Web.Core.Infrastucture;
using Microsoft.Practices.Unity;
using System;
using System.Configuration;
using System.Web.Http;
using Unity.WebApi;

namespace CSWeFramework.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            //var container = new UnityContainer();
            var container = ServiceContainer.Current;
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            RegisterTypes(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

        }
        public static void RegisterTypes(IUnityContainer container)
        {
            //注册unity容器
            //IUnityContainer->UnityContainer
            container.RegisterInstance(container);
            //通过查找器，查找bin中实现了IDependencyRegister接口的类型
            ITypeFinder typeFinder = new WebTypeFinder();
            //获取配置节信息
            var config = ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;
            //注册实例
            container.RegisterInstance(config);

            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            //遍历实现了IDependencyRegister接口类型
            foreach (var registerType in registerTypes)
            {
                //创建这个对象
                var register = (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterType(container);
            }

        }

    }
}