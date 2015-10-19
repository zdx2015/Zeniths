using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Auth.Utility;

namespace Zeniths.Web.Areas.Auth.Controllers
{
    public class SystemDepartmentController : AuthBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}