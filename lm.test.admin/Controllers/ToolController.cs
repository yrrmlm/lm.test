using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using lm.model.Enum;
using lm.model.Interface;
using lm.tool;
using Microsoft.AspNetCore.Mvc;

namespace lm.test.admin.Controllers
{
    public class ToolController : Controller
    {
        public JsonResult CreatePdf()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"test.pdf");
            PdfHelper.Instance.Create(filePath);
            return Json(new BaseResponse
            {
                code = RspCode.Success.GetHashCode()
            });
        }

        public JsonResult SendEmail()
        {
            var emailHelper = new EmailHelper("smtp.163.com", "1096122384@qq.com", "yrrmlm@163.com", "test", "test", "yrrmlm@163.com", "1096122384", "25", false, false, null);
            emailHelper.Send();
            return Json(new BaseResponse
            {
                code = RspCode.Success.GetHashCode()
            });
        }
    }
}