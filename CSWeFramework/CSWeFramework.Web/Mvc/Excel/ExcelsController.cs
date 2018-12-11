using CSWeFramework.Web.Core.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSWeFramework.Web.Mvc.Excel
{
    public class ExcelsController : BaseController
    {
        public ExcelFileResult<TModel> Excel<TModel>(IEnumerable<TModel> model) where TModel : class
        {
            return new ExcelFileResult<TModel>(model);
        }

        public ExcelFileResult<TModel> Excel<TModel>(IEnumerable<TModel> model, string fileName) where TModel : class
        {
            return new ExcelFileResult<TModel>(model, fileName);
        }

    }
}