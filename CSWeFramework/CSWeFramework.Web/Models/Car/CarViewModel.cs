using CSWeFramework.Web.Validator;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSWeFramework.Web.Models.Car
{
    //[Validator(typeof(CarValidator))]
    public class CarViewModel
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [DisplayName("汽车售价")]
        public decimal Price { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}