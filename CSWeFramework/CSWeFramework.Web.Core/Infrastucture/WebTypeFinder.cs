using CSWeFramework.Core.Infrastucture;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CSWeFramework.Web.Core.Infrastucture
{
    /// <summary>
    /// web类型查找器
    /// 查找当前应用程序域下的bin目录的dll或者iis下，发布的dll
    /// 提供当前Web应用程序中有关类型的信息。
    ///可选地，这个类可以查看bin文件夹中的所有程序集。
    /// </summary>
    public class WebTypeFinder : AppDomainTypeFinder
    {
        /// <summary>
        /// bin文件夹程序集是否加载
        /// </summary>
        private  bool binFolderAssemblesLoaded = false;

        /// <summary>
        /// 获取\bin目录的物理磁盘路径 物理路径。
        /// </summary>
        /// <returns>例如：C:\InPub\www.根目录</returns>
        public virtual string GetBinDirectroy()
        {
            //是否托管于iis，是，则获取iis的bin目录的物理路径
            if (System.Web.Hosting.HostingEnvironment.IsHosted)
            {
                return System.Web.HttpRuntime.BinDirectory;
            }

            //不托管的。例如，在单元测试中运行
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (!this.binFolderAssemblesLoaded)
            {
                this.binFolderAssemblesLoaded = true;
                base.LoadMatchingAssembles(this.GetBinDirectroy());
            }

            return base.GetAssemblies();
        }
    }
}
