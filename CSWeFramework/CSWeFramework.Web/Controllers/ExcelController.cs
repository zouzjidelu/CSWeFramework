using CSWeFramework.Web.Models.Car;
using CSWeFramework.Web.Mvc.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace CSWeFramework.Web.Controllers
{
    public class ExcelController : ExcelsController
    {
        // GET: Excel
        public ActionResult Index()
        {
            //using (ExcelPackage excel = new ExcelPackage())
            //{
            //    //将新工作表添加到空工作簿中
            //    ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("sheet1");
            //    //worksheet.Row(5).Style.Font.Bold;
            //    //worksheet.Cells[]

            //}
            //return View();
            List<ExcelCarViewModel> viewModelList = new List<ExcelCarViewModel>();
            Random rdm = new Random(System.Environment.TickCount);
            for (int i = 0; i < 5; i++)
            {
                viewModelList.Add(new ExcelCarViewModel()
                {
                    ID = i,
                    CreateTime = DateTime.Now,
                    Email = i + "@.qq.com",
                    Name = Guid.NewGuid().ToString(),
                    Price = rdm.Next(10, 500),
                });
            }

            return Excel(viewModelList);
        }
    }
}