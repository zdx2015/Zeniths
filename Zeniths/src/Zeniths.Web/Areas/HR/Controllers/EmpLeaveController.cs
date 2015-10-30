// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;
using Zeniths.Hr.Utility;
using Zeniths.Web.Areas.HR.Models;

namespace Zeniths.Web.Areas.Hr.Controllers
{
    /// <summary>
    /// 流程按钮控制器
    /// </summary>
    [Authorize]
    public class EmpLeaveController : HrBaseController
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private readonly EmpLeaveService service = new EmpLeaveService();

        /// <summary>
        /// 主视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Storage()
        {
            return View();
        }

        /// <summary>
        /// 表格视图
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <returns>视图模板</returns>
        public ActionResult Grid(string name)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir, name);
            return View(list);
        }
        
        /// <summary>
        /// 请假登记
        /// </summary>
        /// <param name="businessId">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Register(string businessId)
        {
            EmpLeaveModel model = GetModel(businessId);
            return View(model);
        }

        public ActionResult Approve(string businessId)
        {
            EmpLeaveModel model = GetModel(businessId);
            return View(model);
        }

        public ActionResult Complete(string businessId)
        {
            EmpLeaveModel model = GetModel(businessId);
            return View(model);
        }

        private EmpLeaveModel GetModel(string businessId)
        {
            EmpLeaveModel model = new EmpLeaveModel();
            if (businessId.IsNotEmpty())
            {
                ObjectHelper.CopyProperty(service.Get(businessId.ToInt()), model);
            }
            return model;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmpLeaveModel entity)
        {
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return Json(hasResult);
            }

            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return Json(result);
        }


        /// <summary>
        /// 查看视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Details(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult Delete(string id)
        {
            var result = service.Delete(StringHelper.ConvertToArrayInt(id));
            return Json(result);
        }
        
        /// <summary>
        /// 数据导出
        /// </summary>
        /// <returns>返回文件下载流</returns>
        public ActionResult Export()
        {
            return Export(service.GetList());
        }
    }
}
