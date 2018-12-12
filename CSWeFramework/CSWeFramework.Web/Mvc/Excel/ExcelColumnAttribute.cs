using System;

namespace CSWeFramework.Web.Mvc.Excel
{
    /// <summary>
    /// 针对Excel 工作表中的列进行格式化
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ExcelColumnAttribute : Attribute
    {
        /// <summary>
        /// 是否让列居中，
        /// </summary>
        public bool IsCenter { get; set; } = false;
        /// <summary>
        /// 列排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 格式化器【Excel自带的格式化】
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public int Width { get; set; } = 0;
        
    }
}