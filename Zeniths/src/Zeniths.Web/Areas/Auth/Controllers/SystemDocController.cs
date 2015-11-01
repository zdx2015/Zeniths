using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Auth.Utility;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.WorkFlow.Entity;
using Zeniths.WorkFlow.Service;

namespace Zeniths.Web.Areas.Auth.Controllers
{
    [Authorize]
    public class SystemDocController : AuthBaseController
    {
        private readonly SystemDocService service = new SystemDocService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid(string name)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir, name);
            return View(list);
        }

        public ActionResult Create()
        {
            return EditCore(new SystemDoc());
        }

        public ActionResult Edit(string id)
        {
            var entity = service.Get(id.ToInt());
            return EditCore(entity);
        }

        private ActionResult EditCore(SystemDoc entity)
        {
            return View("Edit", entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SystemDoc entity)
        {
            foreach (string key in Request.Files.Keys)
            {
                var file = Request.Files[key];
                file.SaveAs(Server.MapPath("~/UploadFiles/"+file.FileName));
            }
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return Json(hasResult);
            }

            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var result = service.Delete(StringHelper.ConvertToArrayInt(id));
            return Json(result);
        }

        public ActionResult Details(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        public ActionResult Export()
        {
            return Export(service.GetList());
        }
    }
}