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
using Zeniths.Auth.Utility;

namespace Zeniths.Web.Areas.Hr.Controllers
{
    /// <summary>
    /// 流程按钮控制器
    /// </summary>
    [Authorize]
    public class HrBudgetController : HrBaseController
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private readonly HrBudgetService service = new HrBudgetService();

        /// <summary>
        /// 主视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Index(string type)
        {
            ViewData["type"] = type;
            return View();
        }
        /// <summary>
        /// 查询预算明细
        /// </summary>
        /// <param name="BudgetMonth">预算月份</param>
        /// <param name="DepartmentName">预算部门</param>
        /// <param name="status">预算状态</param>
        /// <param name="type">业务类型 1 部门负责人 2 经理审批 3 财务查看 </param>
        /// <returns></returns>
        public ActionResult Grid(string BudgetMonth,string DepartmentName,string status,string type)
        {
            ViewData["type"] = type;
            var currentUser = OrganizeHelper.GetCurrentUser();
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();            
            var orderDir = GetOrderDir();
            orderName = orderName == "" ? "id" : orderName;
            orderDir = orderDir == "" ? "asc" : orderDir;
            var list = service.GetPageListView(currentUser.DepartmentId,DepartmentName,type,BudgetMonth,status, pageIndex, pageSize, orderName, orderDir);
            return View(list);
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            ViewData["Title"] = "新增部门预算";
            return EditCore(new HrBudget());
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Edit(string id)
        {
            ViewData["Title"] = "编辑部门预算";
            var entity = service.Get(id.ToInt());
            return EditCore(entity);
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
        public ActionResult DetailsList(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }
        /// <summary>
        /// 数据编辑视图
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        private ActionResult EditCore(HrBudget entity)
        {
            return View("Edit", entity);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Save(HrBudget entity)
        {
            //判斷是否有明細信息
            var resul = service.ExistsCount(entity);
            if (!resul.Success)
            {
                return resul.Message;
            }
            entity.Status = 1;

            entity.CreateDateTime = DateTime.Now;
            entity.IsFinish = false;
            OrganizeHelper.SetCurrentUserCreateInfo(entity);
            //entity.BudgetDepartmentId = entity.CreateDepartmentId;
            entity.BudgetDepartmentName = entity.CreateDepartmentName;
            entity.Title = entity.CreateDepartmentName + " " + entity.BudgetMonth.ToString("yyyy年MM月") + "预算申请";
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return hasResult.Message;
            }

            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return result.Message;
        }
        /// <summary>
        /// 在添加明細信息時判斷是否添加了預算主體信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string BudgetSave(HrBudget entity)
        {
            entity.CreateDateTime = DateTime.Now;
            entity.IsFinish = false;
            OrganizeHelper.SetCurrentUserCreateInfo(entity);
           //entity.BudgetDepartmentId = entity.CreateDepartmentId;
            entity.BudgetDepartmentName = entity.CreateDepartmentName;
            entity.Status = 1;
            entity.Title = entity.CreateDepartmentName + " " + entity.BudgetMonth.ToString("yyyy年MM月") + "预算申请";
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return hasResult.Message;
            }

            var result = service.Insert(entity);
            return result.Message;
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
