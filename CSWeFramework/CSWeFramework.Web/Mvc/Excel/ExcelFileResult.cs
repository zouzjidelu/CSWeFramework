using CSWeFramework.Web.Models.Car;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
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
                //获取用户自定义的工作表的属性设置【行高、列宽、名字】
                ExcelSheetAttribute excelSheetAttribute = typeof(TModel).GetCustomAttribute<ExcelSheetAttribute>() ?? new ExcelSheetAttribute(); ;
                //将新工作表添加到空工作簿中、指定工作表的名称
                ExcelWorksheet sheet = excel.Workbook.Worksheets.Add(excelSheetAttribute.Name);
                //sheet.Row(excelSheetAttribute.RowHeight);
                //sheet.Column(excelSheetAttribute.ColumnWeight);

                //添加页眉
                this.GenerateExcelHandler(sheet, propertys, excelSheetAttribute);
                //添加body 
                this.GenerateExcelBody(sheet, propertys, excelSheetAttribute);

                return excel.GetAsByteArray();
            }

        }

        /// <summary>
        /// 添加页眉
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="propertyInfos"></param>
        private void GenerateExcelHandler(ExcelWorksheet sheet, PropertyInfo[] propertyInfos, ExcelSheetAttribute excelSheetAttribute)
        {
            //设置行高、列宽
            sheet.Row(1).Height = excelSheetAttribute.RowHeight;
            sheet.Column(1).Width = excelSheetAttribute.ColumnWeight;
            sheet.TabColor = Color.Yellow;
            //单元格自适应，设置此项，则不能设置单元格的宽高。否则跑出异常
            sheet.Cells.Style.ShrinkToFit = true;

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
                ExcelRange currentCell = sheet.Cells[1, col];
                currentCell.Value = displayName;
                //标头加粗
                currentCell.Style.Font.Bold = true;
                //标头水平方向居中
                currentCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //垂直方向居中
                currentCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                //设置背景颜色
                currentCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                currentCell.Style.Fill.BackgroundColor.SetColor(Color.Red);
            }
        }


        /// <summary>
        /// 添加body
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="propertyInfos"></param>
        private void GenerateExcelBody(ExcelWorksheet sheet, PropertyInfo[] propertyInfos, ExcelSheetAttribute excelSheetAttribute)
        {
            TModel[] modelArray = model.ToArray();
            //第一行。是标头,故，从第二行开始
            for (int row = 2; row <= modelArray.Length + 1; row++)
            {
                TModel model = modelArray[row - 2];
                for (int i = 1; i <= propertyInfos.Length; i++)
                {
                    ExcelRange excelCell = sheet.Cells[row, i];
                    PropertyInfo currentPropertyInfo = propertyInfos[i - 1];

                    DisplayFormatAttribute displayFormatAttribute = currentPropertyInfo.GetCustomAttribute<DisplayFormatAttribute>();
                    //如果属性上有此标签，并且当前属性类型，能不能被格式化
                    if (displayFormatAttribute != null && typeof(IFormattable).IsAssignableFrom(currentPropertyInfo.PropertyType))
                    {
                        excelCell.Value = ((IFormattable)currentPropertyInfo.GetValue(model)).ToString(displayFormatAttribute.DataFormatString, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        excelCell.Value = currentPropertyInfo.GetValue(model);
                    }

                    ExcelColumnAttribute excelColumnAttribute = currentPropertyInfo.GetCustomAttribute<ExcelColumnAttribute>() ?? new ExcelColumnAttribute();
                    excelCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    excelCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    if (!string.IsNullOrEmpty(excelColumnAttribute.Format))
                    {
                        excelCell.Style.Numberformat.Format = excelColumnAttribute.Format;
                    }

                    //设置行高、列宽[优先使用列设置的宽高，否则使用标头设置的宽高]
                     sheet.Column(i).Width = excelColumnAttribute.Width > 0 ? excelColumnAttribute.Width : excelSheetAttribute.ColumnWeight;
                }
                sheet.Row(row).Height = excelSheetAttribute.RowHeight;
            }
        }
    }
}
