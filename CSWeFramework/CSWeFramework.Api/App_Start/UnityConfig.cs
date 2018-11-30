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
            //ע��unity����
            //IUnityContainer->UnityContainer
            container.RegisterInstance(container);
            //ͨ��������������bin��ʵ����IDependencyRegister�ӿڵ�����
            ITypeFinder typeFinder = new WebTypeFinder();
            //��ȡ���ý���Ϣ
            var config = ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;
            //ע��ʵ��
            container.RegisterInstance(config);

            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            //����ʵ����IDependencyRegister�ӿ�����
            foreach (var registerType in registerTypes)
            {
                //�����������
                var register = (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterType(container);
            }

        }

    }
}