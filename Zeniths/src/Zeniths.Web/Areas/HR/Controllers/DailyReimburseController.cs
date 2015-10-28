﻿// ===============================================================================
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
        /// 明细表格视图
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailGrid()
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = (List<DailyReimburseDetails>)Session["DailyReimburseDetails"];
            if(list == null)
            {
                list = new List<DailyReimburseDetails>();
            }
            PageList<DailyReimburseDetails> pList = new PageList<DailyReimburseDetails>(pageIndex, pageSize, list.Count,list);
            return View(pList);
        }
        /// <summary>
        /// 查看明细表格视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewGrid()
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = (List<DailyReimburseDetails>)Session["DailyReimburseDetails"];
            if (list == null)
            {
                list = new List<DailyReimburseDetails>();
            }
            PageList<DailyReimburseDetails> pList = new PageList<DailyReimburseDetails>(pageIndex, pageSize, list.Count, list);
            return View(pList);
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            ViewBag.Action = "Create";
            var entity = new DailyReimburse();
            //entity.ReimburseDepartmentId = CurrentUser.DepartmentId;
            entity.ReimburseDepartmentName = CurrentUser.DepartmentName;
            //entity.ApplicantId = CurrentUser.Id;
            entity.ApplicantName = CurrentUser.Name;
            entity.ApplicationDate = DateTime.Now.Date;
            //entity.CreateUserId = CurrentUser.Id;
            //entity.CreateUserName = CurrentUser.Name;
            //entity.CreateDepartmentId = CurrentUser.DepartmentId;
            //entity.CreateDepartmentName = CurrentUser.DepartmentName;
            //entity.CreateDateTime = DateTime.Now;
            Session["DailyReimburseDetails"] = null;
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
            ViewBag.Action = "Edit";
            var entity = service.Get(id.ToInt());
            var list = detailsService.GetList(id.ToInt());
            Session["DailyReimburseDetails"] = list;
            ViewBag.Title = "编辑日常费用报销";
            return EditCore(entity);
        }


        /// <summary>
        /// 查看视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Details(string id)
        {
            ViewBag.Action = "View";
            var entity = service.Get(id.ToInt());
            var list = detailsService.GetList(id.ToInt());
            Session["DailyReimburseDetails"] = list;
            ViewBag.Title = "编辑日常费用报销";
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
            if (entity.Id == 0)
            {
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
                var list = (List<DailyReimburseDetails>)Session["DailyReimburseDetails"];
                foreach (var item in list)
                {
                    entity.ProjectSumMoney = entity.ProjectSumMoney + item.Amount;
                }
                result = service.Insert(entity, list);
            }
            else
            {
                var list = (List<DailyReimburseDetails>)Session["DailyReimburseDetails"];
                result = service.Update(entity, list);
            }

            //var result = entity.Id == 0 ? service.Insert(entity, list) : service.Update(entity);
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
    }
}
