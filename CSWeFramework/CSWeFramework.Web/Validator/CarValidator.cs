using CSWeFramework.Web.Models.Car;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSWeFramework.Web.Validator
{
    /// <summary>
    /// Car验证器类
    /// 继承第三方框架自带的验证器类，其传入要验证的实体类
    /// </summary>
    public class CarValidator : AbstractValidator<CarViewModel>
    {
        public CarValidator()
        {
            //定义指定属性的验证规则
            RuleFor(car => car.Name).NotNull().WithMessage("不能为控").Length(5, 10).WithMessage("长度范围为20-50字节");
           
            RuleFor(car => car.Price).NotNull();
        }
    }
}