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
    public class EmployeeLeaveController : HrBaseController
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private readonly EmployeeLeaveService service = new EmployeeLeaveService();
        private readonly EmployeeService employeeService = new EmployeeService();

        /// <summary>
        /// 主视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 表格视图
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <returns>视图模板</returns>
        public ActionResult Grid(string employeeName)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var currentUser = OrganizeHelper.GetCurrentUser();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir, currentUser.Id);
            return View(list);
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            EmployeeLeave leaveEntity = new EmployeeLeave();
            var currentUser = OrganizeHelper.GetCurrentUser();
            var currentEmployee = employeeService.GetEmployeeByUser(currentUser.Id);    //根据当前登录用户的账号查找员工Id信息
            leaveEntity.EmployeeId = currentEmployee.Id;
            leaveEntity.EmployeeName = currentEmployee.Name;
            leaveEntity.DeparmentId = currentEmployee.DepartmentId;
            leaveEntity.Deparment = currentEmployee.Department;
            leaveEntity.Post = currentEmployee.Post;
            return EditCore(leaveEntity);
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Edit(string id)
        {
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

        /// <summary>
        /// 查看视图(请休假基本信息)
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult DetailsBaseLeave(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        /// <summary>
        /// 查看视图(处理意见信息)
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult DetailsOpinionContent(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        /// <summary>
        /// 查看视图(流程图)
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult DetailsWorkFlowChart(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        /// <summary>
        /// 数据编辑视图
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        private ActionResult EditCore(EmployeeLeave entity)
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
        public ActionResult Save(EmployeeLeave entity)
        {
            if (entity.Id == 0)
            {
                var currentUser = OrganizeHelper.GetCurrentUser();
                entity.ApplyDateTime = DateTime.Now;

                entity.CreateUserId = currentUser.Id;
                entity.CreateUserName = currentUser.Name;
                entity.CreateDepartmentId = currentUser.DepartmentId;
                entity.CreateDepartmentName = currentUser.DepartmentName;
                entity.CreateDateTime = DateTime.Now;

                entity.Title = "";
                entity.FlowId = "";
                entity.FlowName = "";
                entity.FlowInstanceId = "";
                entity.StepId = "";
                entity.StepName = "";
                entity.IsFinish = false;
            }
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return Json(result);
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

        /// <summary>
        /// 更新销假信息视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult CancelLeaveEdit(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        /// <summary>
        /// 更新工作代理人审批信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult UpdateJobAgentApproval(EmployeeLeave entity)
        {
            var result = service.UpdateJobAgentApproval(entity);
            return Json(result);
        }

        /// <summary>
        /// 更新请休假部门负责人审批信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult UpdateDepartmentManagerApproval(EmployeeLeave entity)
        {
            var result = service.UpdateDepartmentManagerApproval(entity);
            return Json(result);
        }

        /// <summary>
        /// 更新请休假总经理审批信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult UpdateGeneralManagerApproval(EmployeeLeave entity)
        {
            var result = service.UpdateGeneralManagerApproval(entity);
            return Json(result);
        }

        /// <summary>
        /// 更新销假信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult UpdateCancelLeaveInfo(EmployeeLeave entity)
        {
            var result = service.UpdateCancelLeaveInfo(entity);
            return Json(result);
        }

        /// <summary>
        /// 更新请休假销假部门负责人审批信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult UpdateCancelLeaveDepartmentManagerApproval(EmployeeLeave entity)
        {
            var result = service.UpdateCancelLeaveDepartmentManagerApproval(entity);
            return Json(result);
        }

        /// <summary>
        /// 更新请休假销假行政人力资源部审批信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult UpdateCancelLeaveHRManagerApproval(EmployeeLeave entity)
        {
            var result = service.UpdateCancelLeaveHRManagerApproval(entity);
            return Json(result);
        }
    }
}
