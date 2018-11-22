using CSWeFramework.Core.Infrastucture;
using Microsoft.Practices.Unity;

namespace CSWeFramework.Web.Mvc
{
    public class ActionFilterRegister : IDependencyRegister
    {
        public void RegisterType(IUnityContainer container)
        {
            container.RegisterType<CustomHandleErrorAttribute>();
        }
    }
}