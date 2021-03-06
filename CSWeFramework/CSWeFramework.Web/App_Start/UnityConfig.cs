using System;
using System.Configuration;
using System.IO;
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
            //RegisterTypes(ServiceContainer.Current);
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

            //注册unity容器
            //IUnityContainer->UnityContainer
            container.RegisterInstance(container);

            //配置先后顺序，先读取配置文件的，在读取IDependencyRegister接口的
            Configuration(container);

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

        /// <summary>
        /// 读取untiry配种的依赖数据
        /// </summary>
        /// <param name="container"></param>
        public static void Configuration(IUnityContainer container)
        {
            // 找到配置文件的path
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Unity.config");
            //配置文件映射
            var configMap = new ExeConfigurationFileMap { ExeConfigFilename = path };
            //打开配置文件映射
            var config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            //找到untiy节点
            UnityConfigurationSection section =(UnityConfigurationSection) config.GetSection("unity");
            //将默认容器元素中的配置应用于给定容器。 
            section.Configure(container);
        }
    }
}
