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
using Zeniths.Utility;

namespace Zeniths.Web.Areas.Auth.Controllers
{
    //[Authorize]
    public class SystemUserController : AuthBaseController
    {
        private readonly SystemUserService service = new SystemUserService();

        #region private

        /// <summary>
        /// 设置部门实体对象
        /// </summary>
        /// <param name="departmentId">部门主键</param>
        private void SetDepartmentEntity(int departmentId)
        {
            if (departmentId > 0)
            {
                var deptService = new SystemDepartmentService();
                var dept = deptService.Get(departmentId);
                ViewBag.DepartmentEntity = dept;
            }
        }


        #endregion

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取系统部门树节点
        /// </summary>
        public ActionResult GetTree()
        {
            var departmentService = new SystemDepartmentService();
            var dics = departmentService.GetEnabledList();
            var nodes = TreeHelper.Build(dics, p => p.ParentId == 0, (node, instace) =>
            {
                node.IconCls = AuthHelper.GetDepartmentIconCls(instace);
            });
            return Json(nodes);
        }

        public ActionResult Grid(string name, string departmentIds)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir, name, StringHelper.ConvertToArrayInt(departmentIds));
            return View(list);
        }

        public ActionResult Create()
        {
            var departmentId = Request.QueryString["departmentId"].ToInt();
            return EditCore(new SystemUser
            {
                DepartmentId = departmentId
            });
        }

        public ActionResult Edit(string id)
        {
            var entity = service.Get(id.ToInt());
            return EditCore(entity);
        }

        private ActionResult EditCore(SystemUser entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "请指定实体对象");
            SetDepartmentEntity(entity.DepartmentId);
            return View("Edit", entity);
        }

        public ActionResult Details(string id)
        {
            var entity = service.Get(id.ToInt());
            SetDepartmentEntity(entity.DepartmentId);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SystemUser entity)
        {
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return Json(hasResult);
            }
            entity.NameSpell = SpellHelper.ConvertSpell(entity.Name);
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            var result = service.Delete(StringHelper.ConvertToArrayInt(id));
            return Json(result);
        }

        public ActionResult Export()
        {
            return Export(service.GetEnabledList());
        }
    }
}