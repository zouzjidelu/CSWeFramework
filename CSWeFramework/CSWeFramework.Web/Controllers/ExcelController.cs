using OfficeOpenXml;
using System.Web.Mvc;
namespace CSWeFramework.Web.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Excel
        public ActionResult Index()
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                //将新工作表添加到空工作簿中
                ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("sheet1");
                //worksheet.Row(5).Style.Font.Bold;
                //worksheet.Cells[]

            }
            return View();
        }
    }
}