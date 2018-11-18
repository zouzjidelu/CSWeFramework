using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSWeFramework.Web.Validator;
using FluentValidation;
using FluentValidation.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CSWeFramework.Web.App_Start.ExensionsActivator), "Start")]
namespace CSWeFramework.Web.App_Start
{
    /// <summary>
    /// 扩展激活器，将mvc自带的验证框架换成第三方FluentValidation的验证框架
    /// 并在Application_Start执行之前执行
    /// </summary>
    public class ExensionsActivator
    {
        /// <summary>
        /// Application_Start之前执行的此方法
        /// </summary>
        public static void Start()
        {
            //将mvc自带的验证器禁用
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            //将untiy验证工厂注入
            UntiyValidatorFactory untiyValidatorFactory = new UntiyValidatorFactory(UnityConfig.GetConfiguredContainer());
            //将第三方验证器加入
            ModelValidatorProviders.Providers.Add( new FluentValidationModelValidatorProvider(untiyValidatorFactory));
        }

    }
}