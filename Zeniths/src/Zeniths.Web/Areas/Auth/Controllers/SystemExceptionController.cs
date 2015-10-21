using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Auth.Service;
using Zeniths.Auth.Utility;
using Zeniths.Extensions;

namespace Zeniths.Web.Areas.Auth.Controllers
{
    [Authorize]
    public class SystemExceptionController : AuthBaseController
    {
        private readonly SystemExceptionService service = new SystemExceptionService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid(string startDate, string endDate, string ip, string message)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize,
                orderName, orderDir, startDate, endDate, ip, message);
            return View(list);
        }

        public ActionResult Details(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }
    }
}