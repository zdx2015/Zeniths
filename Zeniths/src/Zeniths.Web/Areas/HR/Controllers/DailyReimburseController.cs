// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Collections;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;
using Zeniths.Hr.Utility;

namespace Zeniths.Web.Areas.Hr.Controllers
{
    /// <summary>
    /// 流程按钮控制器
    /// </summary>
    [Authorize]
    public class DailyReimburseController : HrBaseController
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private readonly DailyReimburseService service = new DailyReimburseService();
        private readonly DailyReimburseDetailsService detailsService = new DailyReimburseDetailsService();

        private object SessionData
        {
            get { return Session["DailyReimburseDetails"]; }
            set { Session["DailyReimburseDetails"] = value; }

        }

        /// <summary>
        /// 主视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// 查看主视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult AllIndex()
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
        /// 表格视图
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult AllGrid(string name, string startDate, string endDate)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetAllPageList(pageIndex, pageSize, orderName, orderDir, name, startDate, endDate);
            return View(list);
        }

        /// <summary>
        /// 明细表格视图
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailGrid(string dailyId)
        {
            var list = detailsService.GetList(dailyId.ToInt()); 
            return View(list);
        }
        

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            var entity = new DailyReimburse();
            entity.ReimburseDepartmentName = CurrentUser.DepartmentName;
            entity.ApplicantName = CurrentUser.Name;
            SessionData = null;
            ViewBag.Title = "添加日常费用报销";
            return EditCore(entity);
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Edit(string id)
        {
          
            var entity = service.Get(id.ToInt());
            var list = detailsService.GetList(id.ToInt());
            SessionData = list;
            ViewBag.Title = "编辑日常费用报销";
            return EditCore(entity);
        }


        /// <summary>
        /// 查看视图
        /// </summary>
        /// <param name="businessId">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Details(string businessId)
        {
           
            var entity = service.Get(businessId.ToInt());
            var list = detailsService.GetList(businessId.ToInt());
            SessionData = list;
            ViewBag.Title = "查看日常费用报销";
            return View(entity);
        }
        
        /// <summary>
        /// 查看视图
        /// </summary>
        /// <param name="businessId">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult AllDetails(string businessId)
        {
            var entity = service.Get(businessId.ToInt());
            var list = detailsService.GetList(businessId.ToInt());
            SessionData = list;
            ViewBag.Title = "查看日常费用报销";
            return View(entity);
        }

        /// <summary>
        /// 数据编辑视图
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        private ActionResult EditCore(DailyReimburse entity)
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
        public ActionResult Save(DailyReimburse entity)
        {
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return Json(hasResult);
            }
            var result = new BoolMessage(false);
            
                entity.ReimburseDepartmentId = CurrentUser.DepartmentId;
                entity.ReimburseDepartmentName = CurrentUser.DepartmentName;
                entity.ApplicantId = CurrentUser.Id;
                entity.ApplicantName = CurrentUser.Name;
                entity.ApplicationDate = DateTime.Now.Date;
                entity.ApplySortNumber = service.GetYearAndMonthString() + "0000";
                entity.Title = "填写报销单";
                entity.IsFinish = false;
                entity.FlowId = "";
                entity.FlowInstanceId = "";
                entity.FlowName = "";
                entity.StepId = "";
                entity.StepName = "";
                entity.BudgetId = 0;
                entity.CreateUserId = CurrentUser.Id;
                entity.CreateUserName = CurrentUser.Name;
                entity.CreateDepartmentId = CurrentUser.DepartmentId;
                entity.CreateDepartmentName = CurrentUser.DepartmentName;
                entity.CreateDateTime = DateTime.Now;
                entity.ProjectSumMoney = 0;
                var curlist = (List<DailyReimburseDetails>)SessionData;
                foreach (var item in curlist)
                {
                    item.ReimburseId = entity.Id;
                    entity.ProjectSumMoney = entity.ProjectSumMoney + item.Amount;
                }
            if (entity.Id == 0)
            {
                result = service.Insert(entity, curlist);
            }
            else
            {
               
                result = service.Update(entity, curlist);
            }

           
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
        
    }
}
