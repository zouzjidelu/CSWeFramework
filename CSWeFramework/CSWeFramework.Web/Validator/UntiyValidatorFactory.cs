using FluentValidation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSWeFramework.Web.Validator
{
    /// <summary>
    /// untiy验证工厂类
    /// </summary>
    public class UntiyValidatorFactory : ValidatorFactoryBase
    {
        private readonly IUnityContainer unityContainer;
        /// <summary>
        /// 将unity容器注入
        /// </summary>
        /// <param name="unityContainer"></param>
        public UntiyValidatorFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        /// <summary>
        /// 实例化验证程序
        /// </summary>
        /// <param name="validatorType">
        /// 这里传入的类似于CarViewModelType == [Validator(typeof(CarValidator))]
        /// </param>
        /// <returns></returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator = null;
            try
            {
                // 用容器中的给定名称(validatorType.GetGenericArguments().First().FullName，举例：找到的是CarViewModel)解析请求类型的实例。
                validator = unityContainer.Resolve(validatorType, validatorType.GetGenericArguments().First().FullName) as IValidator;
            }
            catch
            {
                validator = null;
            }

            return validator;
        }
    }

}