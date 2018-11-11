using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Core.Infrastucture
{
    /// <summary>
    /// 应用程序域查找器
    /// </summary>
    public class AppDomainTypeFinder : ITypeFinder
    {
        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            throw new NotImplementedException();
        }

        public IList<Assembly> GetAssemblies()
        {
            throw new NotImplementedException();
        }
    }
}
