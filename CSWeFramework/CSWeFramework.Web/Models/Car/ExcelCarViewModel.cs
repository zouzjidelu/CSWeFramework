using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CSWeFramework.Web.Models.Car
{
    public class ExcelCarViewModel
    {
        [DisplayName("ID")]
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
        public decimal Price { get; set; }

        [Display(Name = "邮箱")]
        public string Email { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}