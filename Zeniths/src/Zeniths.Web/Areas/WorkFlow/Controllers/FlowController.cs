using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Zeniths.Configuration;
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
        
        public ActionResult Design(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult StepSetting()
        {
            return View();
        }

        public ActionResult LineSetting()
        {
            return View();
        }

        public ActionResult FlowSetting()
        {
            return View();
        }

        public ActionResult LoadSetting(string id)
        {
            var entity = service.Get(id);
            if (entity == null)
            {
                return Json(false, "无效的流程标识");
            }
            return Content(entity.Json);
        }

        public ActionResult SaveSetting()
        {
            string json = Request.Form["json"];
            var workFlowDesign = JsonHelper.Deserialize<WorkFlowDesign>(json);
            var entity = new Flow();
            ObjectHelper.CopyProperty(workFlowDesign.Property, entity);
            entity.Json = json;

            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return Json(hasResult);
            }

            var result = service.Save(entity);
            if (result.Success)
            {
                WorkFlowHelper.RefreshWorkFlowDesignCache(entity.Id);
            }
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
            ViewBag.Id = id;
            var entity = service.Get(id);
            return View(entity);
        }

        public ActionResult Export()
        {
            return Export(service.GetEnabledList());
        }
    }
}