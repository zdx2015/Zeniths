using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Hr.Utility;
using Zeniths.WorkFlow.Entity;
using Zeniths.WorkFlow.Service;

namespace Zeniths.Hr.Web.Controllers
{
    public class WorkFlowFormController : HrBaseController
    {
        private readonly WorkFlowFormService service = new WorkFlowFormService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _Grid(string formName, string formCategory)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir,
                formName, formCategory);
            return View(list);
        }

        public ActionResult Create()
        {
            return EditCore(new WorkFlowForm());
        }

        public ActionResult Edit(int id)
        {
            var entity = service.Get(id);
            return EditCore(entity);
        }

        private ActionResult EditCore(WorkFlowForm entity)
        {
            return View("Edit",entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(WorkFlowForm entity)
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