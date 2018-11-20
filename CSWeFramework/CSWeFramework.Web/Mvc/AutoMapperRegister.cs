using AutoMapper;
using CSWeFramework.Core.Infrastucture;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSWeFramework.Web.Mvc
{
    /// <summary>
    /// 映射实体注册到容器中
    /// </summary>
    public class AutoMapperRegister : IDependencyRegister
    {
        public void RegisterType(IUnityContainer container)
        {
            //1.反射找到这个类型下的程序集的所有类型。条件是找到所有继承了Profile的类
            var profileTypes = this.GetType().Assembly.GetTypes().Where(t => typeof(Profile).IsAssignableFrom(t));
            //2.通过反射的方式通过类型，创建对象，得到一个实例
            var profileInstances = profileTypes.Select(t => (Profile)Activator.CreateInstance(t));
            //3.mapper配置。将创建的实例循环添加到配置中
            var config = new MapperConfiguration(cfg => { profileInstances.ToList().ForEach(i => cfg.AddProfile(i)); });
            //4.将映射配置注册到容器中
            container.RegisterInstance<MapperConfiguration>(config);
            //5.通过配置创建一个IMapper
            container.RegisterInstance<IMapper>(config.CreateMapper());
        }
    }
}