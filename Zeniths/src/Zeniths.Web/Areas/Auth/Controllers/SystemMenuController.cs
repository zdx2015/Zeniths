﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Auth.Utility;
using Zeniths.Helper;
using Zeniths.MvcUtility;
using Zeniths.Utility;

namespace Zeniths.Web.Areas.Auth.Controllers
{
    public class SystemMenuController : AuthBaseController
    {
        private readonly SystemMenuService service = new SystemMenuService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取全部模块数据列表
        /// </summary>
        public ActionResult GetList()
        {
            var list = service.GetList();
            return JsonNet(new EasyUIGrid(list.Count, list));
        }

        /// <summary>
        /// 获取指定主键的模块数据
        /// </summary>
        /// <param name="id">模块主键</param>
        public ActionResult Get(int id)
        {
            var entity = service.Get(id);
            return JsonNet(entity == null ? new EntityMessage(false, "没有找到相应的记录,Id=" + id) : new EntityMessage(entity));
        }

        /// <summary>
        /// 保存模块数据
        /// </summary>
        /// <param name="entity">模块实体</param>
        public ActionResult Save(SystemMenu entity)
        {
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            return JsonNetText(new JsonMessage(result));
        }

        /// <summary>
        /// 保存模块父节点
        /// </summary>
        /// <param name="id">模块主键</param>
        /// <param name="oldParentId">原父节点Id</param>
        /// <param name="newParentId">新父节点Id</param>
        public ActionResult SaveParent(int id, int oldParentId, int newParentId)
        {
            var result = service.SaveParent(id, oldParentId, newParentId);
            return JsonNet(new JsonMessage(result));
        }

        /// <summary>
        /// 更新模块排序路径
        /// </summary>
        public ActionResult UpdateSortPath()
        {
            var list = Request.Form.AllKeys.Select(key => new PrimaryKeyValue(key, nameof(SystemMenu.SortPath), Request.Form[key]));
            var result = service.UpdateSortPath(list);
            return JsonNet(new JsonMessage(result));
        }

        /// <summary>
        /// 删除模块数据
        /// </summary>
        /// <param name="ids">模块主键字符串(多个主键逗号隔开)</param>
        public ActionResult Delete(string ids)
        {
            var idsz = StringHelper.ConvertToArrayInt(ids);
            var result = service.Delete(idsz);
            return JsonNet(new JsonMessage(result));
        }

        /// <summary>
        /// 导出模块数据
        /// </summary>
        public ActionResult Export()
        {
            return Export(service.GetEnabledList());
        }
    }
}