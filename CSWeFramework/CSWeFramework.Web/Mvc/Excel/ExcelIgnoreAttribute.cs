using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSWeFramework.Web.Models.Car
{
    /// <summary>
    /// 打上此标签，代表，属性不输出到excel中
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Parameter | System.AttributeTargets.Property | System.AttributeTargets.ReturnValue)]
    public class ExcelIgnoreAttribute:Attribute
    {
    }
}