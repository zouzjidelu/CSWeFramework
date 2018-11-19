using CSWeFramework.Web.Models.Car;
using FluentValidation;

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
            RuleFor(car => car.Name).NotNull().Length(5, 10);
            RuleFor(car => car.Price).NotNull();
            RuleFor(car => car.Email).NotNull().EmailAddress();
        }
    }
}