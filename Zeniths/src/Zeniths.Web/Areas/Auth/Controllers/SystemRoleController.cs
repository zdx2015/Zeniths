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

namespace Zeniths.Web.Areas.Auth.Controllers
{
    public class SystemRoleController : AuthBaseController
    {
        private readonly SystemRoleService service = new SystemRoleService();

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
            return EditCore(new SystemRole());
        }

        public ActionResult Edit(string id)
        {
            var entity = service.Get(id.ToInt());
            return EditCore(entity);
        }

        private ActionResult EditCore(SystemRole entity)
        {
            return View("Edit", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SystemRole entity)
        {
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return JsonNet(hasResult);
            }
            entity.NameSpell = SpellHelper.ConvertSpell(entity.Name);
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return JsonNet(result);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var result = service.Delete(StringHelper.ConvertToArrayInt(id));
            return JsonNet(result);
        }

        public ActionResult Details(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        public ActionResult Export()
        {
            return Export(service.GetEnabledList());
        }
    }
}