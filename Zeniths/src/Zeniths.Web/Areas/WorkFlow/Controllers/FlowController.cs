using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.WorkFlow.Entity;
using Zeniths.WorkFlow.Service;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Web.Areas.WorkFlow.Controllers
{
    [Authorize]
    public class FlowController : WorkFlowBaseController
    {
        private readonly FlowService service = new FlowService();


        public ActionResult GetTestData()
        {
            var path =  Server.MapPath("~/Areas/WorkFlow/Assets/js/data.json");
            var content = System.IO.File.ReadAllText(path);
            return Content(content, "text/html", Encoding.UTF8);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grid(string name, string category)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize,
                orderName, orderDir, name, category);
            return View(list);
        }

        public ActionResult Create()
        {
            return EditCore(new Flow());
        }

        public ActionResult Edit(string id)
        {
            var entity = service.Get(id);
            return EditCore(entity);
        }

        private ActionResult EditCore(Flow entity)
        {
            return View("Edit", entity);
        }

        public ActionResult Design()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Flow entity)
        {
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return Json(hasResult);
            }

            var result = string.IsNullOrEmpty(entity.Id) ? service.Insert(entity) : service.Update(entity);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var result = service.Delete(StringHelper.ConvertStringToArray(id));
            return Json(result);
        }

        public ActionResult Details(string id)
        {
            var entity = service.Get(id);
            return View(entity);
        }

        public ActionResult Export()
        {
            return Export(service.GetEnabledList());
        }
    }
}