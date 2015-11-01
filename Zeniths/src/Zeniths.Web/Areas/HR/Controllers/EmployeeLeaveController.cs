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
        /// 主视图(查询所有记录)
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult IndexAll()
        {
            return View();
        }

        /// <summary>
        /// 主视图(编辑自己请休假信息)
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
        public ActionResult GridAll(string employeeName,string deparment, string startDatetime, string startEndDatetime, string applyDateTime, int? status)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageListAll(pageIndex, pageSize, orderName, orderDir, employeeName, deparment, startDatetime, startEndDatetime, applyDateTime, status);
            return View(list);
        }

        /// <summary>
        /// 表格视图
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <returns>视图模板</returns>
        public ActionResult Grid(string startDatetime, string startEndDatetime, string applyDateTime, int? status)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var currentUser = OrganizeHelper.GetCurrentUser();
            var list = service.GetPageListMyself(pageIndex, pageSize, orderName, orderDir, currentUser.Id, startDatetime, startEndDatetime, applyDateTime, status);
            return View(list);
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            string businessId = Request.QueryString["businessId"] == null ? "" : Request.QueryString["businessId"];
            if (businessId != "")
            {
               return Edit(businessId);
            }
            //EmployeeLeave leaveEntity = new EmployeeLeave();
            //if (id != null)
            //{
            //    leaveEntity = service.Get(id);
            //}

            EmployeeLeave leaveEntity = new EmployeeLeave();
            var currentUser = OrganizeHelper.GetCurrentUser();
            var currentEmployee = employeeService.GetEmployeeByUser(currentUser.Id);    //根据当前登录用户的账号查找员工Id信息
            leaveEntity.EmployeeId = currentEmployee.Id;
            leaveEntity.EmployeeName = currentEmployee.Name;
            leaveEntity.DeparmentId = currentEmployee.DepartmentId;
            leaveEntity.Deparment = currentEmployee.Department;
            leaveEntity.Post = currentEmployee.Post;
            ViewData["flowId"] = Request.QueryString["flowId"];
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
        /// 数据编辑视图
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        private ActionResult EditCore(EmployeeLeave entity)
        {
            return View("Edit", entity);
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
        /// 保存方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Save(EmployeeLeave entity)
        {
            var service = new EmployeeLeaveService();
            if (entity.Id == 0)
            {
                var currentUser = OrganizeHelper.GetCurrentUser();
                entity.ApplyDateTime = DateTime.Now;
                entity.CreateUserId = currentUser.Id;
                entity.CreateUserName = currentUser.Name;
                entity.CreateDepartmentId = currentUser.DepartmentId;
                entity.CreateDepartmentName = currentUser.DepartmentName;
                entity.CreateDateTime = DateTime.Now;
                entity.IsFinish = false;
                entity.Status = 1;
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
        /// 更新销假信息视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult CancelLeaveEdit(string businessId)
        {
            var entity = service.Get(businessId.ToInt());
            return View(entity);
        }
        
        /// <summary>
        /// 请休假审批
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult ApproveOpinionEdit(string businessId)
        {
            var entity = service.Get(businessId.ToInt());
            return View(entity);
        }

    }
}
