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
    [Authorize]
    public class SystemDepartmentController : AuthBaseController
    {
        private readonly SystemDepartmentService service = new SystemDepartmentService();

        #region private

        /// <summary>
        /// 设置父节点实体对象
        /// </summary>
        /// <param name="parentId"></param>
        private void SetParentEntity(int parentId)
        {
            if (parentId > 0)
            {
                ViewBag.ParentEntity = service.Get(parentId);
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
            var dics = service.GetList();
            dics.Insert(0, new SystemDepartment
            {
                ParentId = -1,
                Name = "组织机构"
            });
            var nodes = TreeHelper.Build(dics, p => p.ParentId == -1, (node, instace) =>
            {
                node.IconCls = OrganizeHelper.GetDepartmentIconCls(instace);
            });
            return Json(nodes);
        }

        public ActionResult Grid(string departmentId, string name)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir,
                departmentId.ToInt(), name);
            return View(list);
        }

        public ActionResult Create()
        {
            var parentId = Request.QueryString["parentId"].ToInt();
            var sortPath = Request.QueryString["sortPath"];
            return EditCore(new SystemDepartment
            {
                ParentId = parentId,
                SortPath = sortPath
            });
        }

        public ActionResult Edit(string id)
        {
            var entity = service.Get(id.ToInt());
            return EditCore(entity);
        }

        private ActionResult EditCore(SystemDepartment entity)
        {
            SetParentEntity(entity.ParentId);
            return View("Edit", entity);
        }

        public ActionResult Details(string id)
        {
            var entity = service.Get(id.ToInt());
            SetParentEntity(entity.ParentId);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SystemDepartment entity)
        {
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

        /// <summary>
        /// 保存树父节点
        /// </summary>
        public ActionResult SaveParent()
        {
            int id = Request.Form["id"].ToInt();
            int newParentId = Request.Form["newParentId"].ToInt();
            var result = service.SaveParent(id, newParentId);
            return Json(result);
        }

        /// <summary>
        /// 树排序路径
        /// </summary>
        public ActionResult SaveSort()
        {
            var list = Request.Form.AllKeys.Select(key => new PrimaryKeyValue(key, nameof(SystemDepartment.SortPath), Request.Form[key]));
            var result = service.SaveSort(list);
            return Json(result);
        }

        public ActionResult Export()
        {
            return Export(service.GetEnabledList());
        }
    }
}