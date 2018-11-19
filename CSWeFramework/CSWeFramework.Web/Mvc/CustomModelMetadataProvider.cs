using CSWeFramework.Web.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSWeFramework.Web.Mvc
{
    /// <summary>
    /// 元数据提供者类
    /// 继承微软元数据提供者类，重写创建元数据类，并替换掉
    /// </summary>
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            //1.保存微软生成的元数据
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            //2.判断元数据类型是否为空
            if (containerType != null)
            {
                //3.获取元数据key
                string key = containerType.Name.Replace(".", string.Empty) + propertyName + nameof(modelMetadata.DisplayName);
                //4.根据key在资源管理器中获取DisplayName
                string displayName = Resources.ResourceManager.GetString(key);
                if (!string.IsNullOrEmpty(displayName))
                {
                    //替换掉微软生成的元数据中的DisplayName
                    modelMetadata.DisplayName = displayName;
                }
            }
            
            return modelMetadata;
        }
    }
}