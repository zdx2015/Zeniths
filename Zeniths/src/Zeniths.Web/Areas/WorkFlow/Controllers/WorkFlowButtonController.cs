using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Helper;
using Zeniths.WorkFlow.Entity;
using Zeniths.WorkFlow.Service;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Web.Areas.WorkFlow.Controllers
{
    public class WorkFlowButtonController : WorkFlowBaseController
    {
        private readonly WorkFlowButtonService service = new WorkFlowButtonService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Grid(string name)
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
            return EditCore(new WorkFlowButton());
        }

        public ActionResult Edit(int id)
        {
            var entity = service.Get(id);
            return EditCore(entity);
        }

        private ActionResult EditCore(WorkFlowButton entity)
        {
            return View("Edit", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(WorkFlowButton entity)
        {
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return JsonNet(hasResult);
            }

            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return JsonNet(result);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var result = service.Delete(StringHelper.ConvertToArrayInt(id));
            return JsonNet(result);
        }

        public ActionResult Export()
        {
            return Export(service.GetEnabledList());
        }
    }
}