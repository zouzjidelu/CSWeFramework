using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Core.Infrastucture
{
    /// <summary>
    /// 依赖寄存器，实现该接口的容器，都会被注册到unity容器中
    /// </summary>
    public interface IDependencyRegister
    {
        void RegisterType(IUnityContainer container);
    }
}
