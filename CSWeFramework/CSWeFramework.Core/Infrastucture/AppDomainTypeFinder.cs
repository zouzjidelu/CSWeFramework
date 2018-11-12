using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CSWeFramework.Core.Infrastucture
{
    /// <summary>
    /// 应用程序域查找器
    /// </summary>
    public class AppDomainTypeFinder : ITypeFinder
    {
        #region Fields

        /// <summary>
        /// 忽略的模式
        /// </summary>
        private readonly string ignorePattern = "^System|^mscorlib|^Microsoft|^AjaxControlToolkit|^Antlr3|^Autofac|^AutoMapper";

        /// <summary>
        /// 过滤模式
        /// </summary>
        private readonly string filterPattern = ".*";

        #endregion

        #region Method

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return this.FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            List<Type> types = new List<Type>();

            foreach (Assembly assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    //指定类型的实例可以分配给当前的实例||(当前Type可以用来构造其他泛型类型的泛型类型&&类型实现通用)
                    if (type.IsAssignableFrom(assignTypeFrom) || (assignTypeFrom.IsGenericTypeDefinition && IsAssignableGeneric(type, assignTypeFrom)))
                    {
                        //如果当前类型不是一个接口
                        if (!type.IsInterface)
                        {
                            if (onlyConcreteClasses)
                            {
                                if (type.IsClass && !type.IsAbstract)
                                {
                                    types.Add(type);
                                }
                            }
                            else
                            {
                                types.Add(type);
                            }
                        }
                    }

                }
            }

            return types;

        }

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return this.FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return this.FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);
        }

        /// <summary>
        /// 获取与当前实现相关的程序集
        /// </summary>
        /// <returns>一个应由NOP工厂加载的程序集列表</returns>
        public virtual IList<Assembly> GetAssemblies()
        {
            return this.GetCurrentDomainAssemblies();
        }

        public IList<Assembly> GetCurrentDomainAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();
            //获得已加载到此应用程序上下文中的程序集
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                //检查当前程序集名称是否是不需要检查的dll之一
                if (this.Matches(assembly.FullName))
                {
                    if (!assemblies.Any(a => a.FullName.Contains(assembly.FullName)))
                    {
                        assemblies.Add(assembly);
                    }
                }
            }

            return assemblies;
        }


        /// <summary>
        /// 确保提供的文件夹中的匹配程序集加载到应用程序域中
        /// </summary>
        /// <param name="directory">到包含应用程序域中加载的DLL的目录的物理路径</param>
        protected virtual void LoadMatchingAssembles(string directory)
        {
            //加载当前程序域下的所有程序集名称
            var loadAssemblyeNames = this.GetCurrentDomainAssemblies().Select(a => a.FullName);
            //是否有此磁盘目录
            if (Directory.Exists(directory))
            {
                //获取磁盘目录下的文件
                foreach (var dllFilePath in Directory.GetFiles(directory, "*.dll"))
                {
                    try
                    {
                        //获取当前文件路径的文件
                        var assemblyName = AssemblyName.GetAssemblyName(dllFilePath);
                        //当前文件不需要检查&&不是此应用程序上下文中的程序集
                        if (this.Matches(assemblyName.FullName) && !loadAssemblyeNames.Contains(assemblyName.FullName))
                        {
                            AppDomain.CurrentDomain.Load(assemblyName);
                        }
                    }
                    catch (BadImageFormatException ex)
                    {
                        Trace.TraceError(ex.ToString());
                    }
                }
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 检查DLL是否是我们知道不需要调查的DLL之一
        /// </summary>
        /// <param name="assemblyFullName">要检查的程序集的名称</param>
        /// <returns>如果程序集应加载到NOP中，则为true。</returns>
        public virtual bool Matches(string assemblyFullName)
        {
            bool isIgnore = Regex.IsMatch(assemblyFullName, ignorePattern, RegexOptions.IgnoreCase);
            bool isFilter = Regex.IsMatch(assemblyFullName, filterPattern, RegexOptions.IgnoreCase);

            return !isIgnore && isFilter;
        }

        /// <summary>
        /// 类型实现通用
        /// </summary>
        /// <param name="type"></param>
        /// <param name="genericType"></param>
        /// <returns></returns>
        protected virtual bool IsAssignableGeneric(Type type, Type genericType)
        {
            Type genericTypeDefinition = genericType.GetGenericTypeDefinition();

            foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
            {
                if (implementedInterface.IsGenericType)
                {
                    return genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                }
            }

            return false;
        }

        #endregion

    }
}
