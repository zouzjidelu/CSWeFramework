using System;
using System.Configuration;
using CSWeFramework.Core.Config;
using CSWeFramework.Core.Infrastucture;
using CSWeFramework.Web.Core.Infrastucture;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace CSWeFramework.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        //private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        //{
        //    var container = new UnityContainer();
        //    RegisterTypes(container);
        //    return container;
        //});

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            RegisterTypes(ServiceContainer.Current);
            return ServiceContainer.Current;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            
            //ע��unity����
            //IUnityContainer->UnityContainer
            container.RegisterInstance(container);
            //ͨ��������������bin��ʵ����IDependencyRegister�ӿڵ�����
            ITypeFinder typeFinder = new WebTypeFinder();
            //��ȡ���ý���Ϣ
            var config=ConfigurationManager.GetSection("applicationConfig") as ApplicationConfig;
            //ע��ʵ��
            container.RegisterInstance(config);
            
            var registerTypes = typeFinder.FindClassesOfType<IDependencyRegister>();
            //����ʵ����IDependencyRegister�ӿ�����
            foreach (var registerType in registerTypes)
            {
                //�����������
               var register= (IDependencyRegister)Activator.CreateInstance(registerType);
                register.RegisterType(container);

            }

        }
    }
}
