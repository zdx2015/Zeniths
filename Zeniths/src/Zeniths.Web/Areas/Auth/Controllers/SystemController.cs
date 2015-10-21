using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Auth.Utility;

namespace Zeniths.Web.Areas.Auth.Controllers
{
    [Authorize]
    public class SystemController : AuthBaseController
    {
        public ActionResult WebIcons()
        {
            return View();
        }
    }
}