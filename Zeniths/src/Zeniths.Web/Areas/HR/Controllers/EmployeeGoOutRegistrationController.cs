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
    public class EmployeeGoOutRegistrationController : HrBaseController
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private readonly EmployeeGoOutRegistrationService service = new EmployeeGoOutRegistrationService();
        private readonly EmployeeService employeeService = new EmployeeService();

        /// <summary>
        /// 主视图(所有记录)
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 主视图(本部门记录)
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult IndexDepartment()
        {
            return View();
        }

        /// <summary>
        /// 主视图(自己的记录)
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult IndexMyself()
        {
            return View();
        }

        /// <summary>
        /// 主视图(待我办理记录)
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult GeneralManagerOpinionList()
        {
            return View();
        }

        /// <summary>
        /// 表格视图
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <returns>视图模板</returns>
        public ActionResult Grid(string employeeName, string goOutDateTime, string goOutEndDateTime)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetAllPageList(pageIndex, pageSize, orderName, orderDir, employeeName, goOutDateTime, goOutEndDateTime);
            return View(list);
        }

        /// <summary>
        /// 表格视图(本部门记录)
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <returns>视图模板</returns>
        public ActionResult GridDepartment(string employeeName, string goOutDateTime, string goOutEndDateTime)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var currentUser = OrganizeHelper.GetCurrentUser();
            var list = service.GetDepartmentPageList(pageIndex, pageSize, orderName, orderDir, employeeName, goOutDateTime, goOutEndDateTime,currentUser.DepartmentId);
            return View(list);
        }

        /// <summary>
        /// 表格视图(自己的记录)
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <returns>视图模板</returns>
        public ActionResult GridMyself(string goOutDateTime,string goOutEndDateTime)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var currentUser = OrganizeHelper.GetCurrentUser();
            var list = service.GetOneselfPageList(pageIndex, pageSize, orderName, orderDir, goOutDateTime, goOutEndDateTime, currentUser.Id);
            return View(list);
        }

        /// <summary>
        /// 表格视图(待审批记录)
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <returns>视图模板</returns>
        public ActionResult GridGeneralManagerOpinion(string employeeName, string goOutDateTime, string goOutEndDateTime)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            //var currentUser = OrganizeHelper.GetCurrentUser();
            //var list = service.GetGeneralManagerOpinionPageList(pageIndex, pageSize, orderName, orderDir, goOutDateTime, goOutEndDateTime, currentUser.Id);
            var list = service.GetGeneralManagerOpinionPageList(pageIndex, pageSize, orderName, orderDir, employeeName, goOutDateTime, goOutEndDateTime);
            return View(list);
        }

        /// <summary>
        /// 表格列头视图
        /// </summary>
        /// <returns></returns>
        public ActionResult _ColumnCommon()
        {
            return View();
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            ViewBag.flag = 0;
            EmployeeGoOutRegistration employeeEntity = new EmployeeGoOutRegistration();
            var currentUser = OrganizeHelper.GetCurrentUser();
            var currentEmployee = employeeService.GetEmployeeByUser(currentUser.Id);    //根据当前登录用户的账号查找员工Id信息
            employeeEntity.EmployeeId = currentEmployee.Id;
            employeeEntity.EmployeeName = currentEmployee.Name;
            return EditCore(employeeEntity, 0);
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Edit(string id)
        {
            var entity = service.Get(id.ToInt());
            return EditCore(entity, 1);
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
        /// 数据编辑视图
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        private ActionResult EditCore(EmployeeGoOutRegistration entity,int flag)
        {
            ViewBag.flag = flag;
            return View("Edit", entity);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EmployeeGoOutRegistration entity)
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
            }
            else
            {
                EmployeeGoOutRegistration entitytemp = service.Get(entity.Id);
                if (entitytemp.GeneralManagerIsAudit.HasValue)
                {
                    entity.GeneralManagerIsAudit = entitytemp.GeneralManagerIsAudit;
                }
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
        /// 部门领导意见视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult GeneralManagerOpinion(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        /// <summary>
        /// 修改返回时间视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult GoBackInfo(string id)
        {
            var entity = service.Get(id.ToInt());
            return View(entity);
        }

        /// <summary>
        /// 修改部门领导意见
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult UpdateGeneralManagerOpinion(EmployeeGoOutRegistration entity)
        {
            //var currentUser = OrganizeHelper.GetCurrentUser();
            //EmployeeGoOutRegistration employeeEntity = service.Get(entity.Id);
            //employeeEntity.GeneralManagerId = currentUser.Id;
            //employeeEntity.GeneralManagerSign = currentUser.Name;
            //employeeEntity.GeneralManagerSignDate = DateTime.Now;
            //employeeEntity.GeneralManagerIsAudit = entity.GeneralManagerIsAudit;
            //employeeEntity.GeneralManagerOpinion = entity.GeneralManagerOpinion;

            var currentUser = OrganizeHelper.GetCurrentUser();
            entity.GeneralManagerId = currentUser.Id;
            entity.GeneralManagerSign = currentUser.Name;
            entity.GeneralManagerSignDate = DateTime.Now;
            var result = service.UpdateGeneralManagerOpinion(entity);
            return Json(result);
        }

        /// <summary>
        /// 修改返回时间
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        [HttpPost]
        public ActionResult UpdateGoBackInfo(EmployeeGoOutRegistration entity)
        {
            var result = service.UpdateGoBackInfo(entity);
            return Json(result);
        }
    }
}
