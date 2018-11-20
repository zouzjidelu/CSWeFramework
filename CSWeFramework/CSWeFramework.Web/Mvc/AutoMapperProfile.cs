using AutoMapper;
using CSWeFramework.Core.Domain;
using CSWeFramework.Web.Models.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSWeFramework.Web.Mvc
{
    /// <summary>
    /// 映射实体
    /// </summary>
    public class AutoMapperProfile:Profile
    {
        protected override void Configure()
        {
            this.CreateMap<Car, CarViewModel>();
            this.CreateMap<CarViewModel,Car>();
        }
    }
}