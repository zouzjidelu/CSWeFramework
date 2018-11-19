using CSWeFramework.Core.Infrastucture;
using CSWeFramework.Web.Properties;
using FluentValidation;
using FluentValidation.Mvc;
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

            //3。将验证资源模板引入，提示信息会使用模板中的。
            //如果在验证类加了验证提示，优先执行验证类的提示信息
            FluentValidationModelValidatorProvider.Configure();
            ValidatorOptions.ResourceProviderType = typeof(Resources);

            //4.在3.的基础上加了提示信息模板后
            //但是表单验证提示信息的属性和资源模板中的元数据配置名称的不一样。应该和元数据名称一致。
            //比如：
             //1.车名--值，如果没有值，则提示车名不能为空。。
             //2.CarName--值，如果没有值，则提示CarName no is null。。

            ValidatorOptions.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
              {
                  //生成key
                  string key = type.Name + memberInfo.Name + "DisplayName";
                  //通过key找到displayname
                  string displayName = Resources.ResourceManager.GetString(key);

                  return displayName;
              };

        }
    }
}