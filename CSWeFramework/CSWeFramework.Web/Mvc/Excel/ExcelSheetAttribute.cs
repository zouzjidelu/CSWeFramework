using System;

namespace CSWeFramework.Web.Mvc.Excel
{
    /// <summary>
    /// 自定义Excel工作表的行高、列宽及sheet名字，需要其他的扩展，都可在这里做
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExcelSheetAttribute : Attribute
    {
        /// <summary>
        /// 工作表的名称，默认Sheet1
        /// </summary>
        public string Name { get; set; } = "Sheet1";

        /// <summary>
        /// 行高，默认25
        /// </summary>
        public int RowHeight { get; set; } = 25;

        /// <summary>
        /// 列宽,默认30
        /// </summary>
        public int ColumnWeight { get; set; } = 30;


    }
}