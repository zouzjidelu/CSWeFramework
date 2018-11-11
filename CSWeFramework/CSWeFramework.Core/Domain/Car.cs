using CSWeFramework.Core.Domain;
using System;

namespace CSWeFramework.Core.Domain
{
    /// <summary>
    /// 汽车实体类
    /// </summary>
    public class Car : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
