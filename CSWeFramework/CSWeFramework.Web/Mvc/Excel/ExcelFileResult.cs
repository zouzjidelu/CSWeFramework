using CSWeFramework.Web.Models.Car;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace CSWeFramework.Web.Mvc.Excel
{
    public class ExcelFileResult<TModel> : FileResult where TModel : class
    {
        private IEnumerable<TModel> model { get; }
        public ExcelFileResult(IEnumerable<TModel> model, string fileName = null) : base("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(TModel));
            }
            this.model = model;
            //如果文件名为空，则随机生成文件名
            fileName = string.IsNullOrEmpty(fileName) ? Path.GetRandomFileName() : fileName;
            //文件后缀可能不是xlsx，所以，统一更改后缀。
            base.FileDownloadName = Path.ChangeExtension(fileName, "xlsx");
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            //生成数据节
            byte[] buffer = this.GenerateExcel();
            //输出流
            response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// 生成数据节
        /// </summary>
        /// <returns></returns>
        private byte[] GenerateExcel()
        {
            //排除在属性上打了ExcelIgnoreAttribute标签的属性，ExcelIgnoreAttribute：不在excel中生成列名，和行数据
            PropertyInfo[] propertys = typeof(TModel).GetProperties().Where(p => !p.IsDefined(typeof(ExcelIgnoreAttribute))).ToArray();
            //创建excel包对象
            using (ExcelPackage excel = new ExcelPackage())
            {
                //将新工作表添加到空工作簿中
                ExcelWorksheet sheet = excel.Workbook.Worksheets.Add("sheet1");
                //添加页眉
                this.GenerateExcelHandler(sheet, propertys);
                //添加body 
                this.GenerateExcelBody(sheet, propertys);

                return excel.GetAsByteArray();
            }

        }

        /// <summary>
        /// 添加页眉
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="propertyInfos"></param>
        private void GenerateExcelHandler(ExcelWorksheet sheet, PropertyInfo[] propertyInfos)
        {
            for (int col = 1; col <= propertyInfos.Length; col++)
            {
                //获得当前属性
                PropertyInfo currentPropertyInfo = propertyInfos[col - 1];
                //找到属性上的标签
                DisplayNameAttribute displayNameAttribute = currentPropertyInfo.GetCustomAttribute<DisplayNameAttribute>();
                DisplayAttribute displayAttribute = currentPropertyInfo.GetCustomAttribute<DisplayAttribute>();
                //判断标签是存在
                string displayName = displayNameAttribute?.DisplayName ?? displayAttribute?.GetName() ?? currentPropertyInfo.Name;
                //给第一行的当前列，赋值
                ExcelRange currentExcelRange = sheet.Cells[1, col];
                currentExcelRange.Value = displayName;
                //标头加粗
                currentExcelRange.Style.Font.Bold = true;
            }
        }


        /// <summary>
        /// 添加body
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="propertyInfos"></param>
        private void GenerateExcelBody(ExcelWorksheet sheet, PropertyInfo[] propertyInfos)
        {
            TModel[] modelArray = model.ToArray();
            //第一行。是标头,故，从第二行开始
            for (int row = 2; row <= modelArray.Length + 1; row++)
            {
                TModel model = modelArray[row - 2];
                for (int i = 1; i <= propertyInfos.Length; i++)
                {
                    ExcelRange excelRange = sheet.Cells[row, i];
                    PropertyInfo currentPropertyInfo = propertyInfos[i - 1];
                   
                    DisplayFormatAttribute displayFormatAttribute = currentPropertyInfo.GetCustomAttribute<DisplayFormatAttribute>();
                    //如果属性上有此标签，并且当前属性类型，能不能被格式化
                    if (displayFormatAttribute != null && typeof(IFormattable).IsAssignableFrom(currentPropertyInfo.PropertyType))
                    {
                        excelRange.Value = ((IFormattable)currentPropertyInfo.GetValue(model)).ToString(displayFormatAttribute.DataFormatString, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        excelRange.Value = currentPropertyInfo.GetValue(model);
                    }                    
                }
            }
        }
    }
}
