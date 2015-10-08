using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.MvcUtility;

namespace Zeniths.Hr.Web.Controllers
{
    public class DemoController : JsonController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetUserList()
        {
            //var userServie = new UserService();
            //var pageIndex = WebHelper.GetFormString("page").ToInt(1);
            //var orderName = WebHelper.GetFormString("order", "Id");
            //var orderDir = WebHelper.GetFormString("dir", "asc");
            //var userName = WebHelper.GetFormString("userName");
            //var realName = WebHelper.GetFormString("realName");
            //var list = userServie.GetPageList(pageIndex, 10, orderName, orderDir, userName, realName);
            return View("Data/_UserTable");
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }
    }
}