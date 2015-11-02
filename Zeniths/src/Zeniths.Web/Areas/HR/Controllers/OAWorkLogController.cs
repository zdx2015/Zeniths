﻿// ===============================================================================
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
    public class OAWorkLogController : HrBaseController
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private readonly OAWorkLogService service = new OAWorkLogService();

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
        public ActionResult Grid(DateTime? logDateFirst, DateTime? logDateLast)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageList(pageIndex, pageSize, orderName, orderDir, logDateFirst, logDateLast);
            return View(list);
        }

        /// <summary>
        /// 分享视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Share(string id)
        {
            //var pageIndex = GetPageIndex();
            //var pageSize = GetPageSize();

            //var list = service.GetUserList(pageIndex, pageSize);

            //ViewBag.selectlist = "";
            //for (int i = 0; i < list.Count; i++)
            //{
            //    ViewBag.selectlist += "<option value='" + i + "'>" + list[i].Account + ", " +
            //        list[i].Name + ", " + list[i].DepartmentName + "</option>";
            //}

            var entity = service.Get(id.ToInt());
            return View(entity);

        }

        /// <summary>
        /// 保存用户数据
        /// </summary>
        /// <param name="shareUser">下拉框id</param>
        /// <returns>返回所选择的用户</returns>
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult shareUser(OAWorkLog entity)
        {
            OAWorkLogShare shareUser = new OAWorkLogShare();

            shareUser.WorkLogId = entity.Id;
            var userIds = Request.Form["ShareUsers"];

            var result = service.ShareWorkLog(entity.Id, userIds);
            //if(result.Success)
            //{
            //    result = new BoolMessage(true, "分享日志成功");
            //}
            
            return Json(result);
        }

        /// <summary>
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            return EditCore(new OAWorkLog() {  LogDate=DateTime.Now.Date,});
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
        /// 数据编辑视图
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>视图模板</returns>
        private ActionResult EditCore(OAWorkLog entity)
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
        public ActionResult Save(OAWorkLog entity)
        {
            OrganizeHelper.SetCurrentUserCreateInfo(entity);
            var result = service.Save(entity);
            return Json(result);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回JsonMessage</returns>
        public ActionResult Delete(string id)
        {
            var result = service.Delete(id);
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
