using CSWeFramework.Core.Infrastucture;
using FluentValidation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSWeFramework.Web.Validator
{
    /// <summary>
    /// 验证器注册类
    /// </summary>
    public class ValidatorRegister : IDependencyRegister
    {
        public void RegisterType(IUnityContainer container)
        {
            //1.通过反射的方式，找到程序集类型下的接口只要有一个是泛型的
            //并且是泛型类型的定义，并且是IValidator,就查出来所有的类型
            var validatorTypes = this.GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)));
            //2.循环验证器类型，依次 注入到untiy容器中
            foreach (Type type in validatorTypes)
            {
                //第三参数解读：，找到实现IValidator该接口的实例名称，
                //第四个参数解读：将该类型注入到容器中是一个单例的形式
                container.RegisterType(typeof(IValidator<>), type, type.BaseType.GetGenericArguments().First().FullName, new ContainerControlledLifetimeManager());
            }
        }
    }
}