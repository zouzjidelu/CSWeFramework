using CSWeFramework.Web.Mvc.Excel;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSWeFramework.Web.Models.Car
{
    /// <summary>
    /// 导出excel实体类
    /// </summary>
    [ExcelSheetAttribute(ColumnWeight = 40, Name = "汽车", RowHeight = 30)]
    public class ExcelCarViewModel
    {
        [DisplayName("ID")]
        [ExcelIgnore]
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]

        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [Display(Name = "价格")]
        //DisplayFormatAttribute设置数据字段的格式,在属性、字段打此标签，可以自定义自己的数据格式
        //[DisplayFormat(DataFormatString = "F2")]
        [ExcelColumn(Format ="0.00")]//excel表格中的价格的 数据格式化器
        public decimal Price { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd HH:mm:ss")]
        [ExcelColumnAttribute(Sort = 1, Width = 30)]
        public DateTime CreateTime { get; set; }

        [Display(Name = "邮箱")]
        public string Email { get; set; }

        
    }
}