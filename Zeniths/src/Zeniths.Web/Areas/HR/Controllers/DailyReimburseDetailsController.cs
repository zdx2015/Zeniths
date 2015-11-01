// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;
using Zeniths.Hr.Utility;
using Zeniths.Auth.Service;

namespace Zeniths.Web.Areas.Hr.Controllers
{
    /// <summary>
    /// 流程按钮控制器
    /// </summary>
    [Authorize]
    public class DailyReimburseDetailsController : HrBaseController
    {
        /// <summary>
        /// 服务对象
        /// </summary>
        private readonly DailyReimburseDetailsService service = new DailyReimburseDetailsService();
        private readonly SystemDictionaryService  dicSevice = new SystemDictionaryService();

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
        /// 新增视图
        /// </summary>
        /// <returns>视图模板</returns>
        public ActionResult Create()
        {
            return EditCore(new DailyReimburseDetails());
        }

        /// <summary>
        /// 编辑视图
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>视图模板</returns>
        public ActionResult Edit(string id)
        {
            DailyReimburseDetails entity = new DailyReimburseDetails(); //service.Get(id.ToInt());
            var list = (List<DailyReimburseDetails>)SessionData;
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (id.ToInt() > 0)
                    {
                        if (item.Id == id.ToInt())
                        {
                            entity = item;
                        }
                    }
                   
                }
            }

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
        private ActionResult EditCore(DailyReimburseDetails entity)
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
        public ActionResult Save(DailyReimburseDetails entity)
        {
            var hasResult = service.Exists(entity);
            if (hasResult.Failure)
            {
                return Json(hasResult);
            }


            var result = BoolMessage.True;
            try
            {
                List<DailyReimburseDetails> list = new List<DailyReimburseDetails>();

                if (SessionData == null)
                {
                    SessionData = list;
                }
                else
                {
                    list = (List<DailyReimburseDetails>)SessionData;
                }
                var dicDetailEntity = dicSevice.GetDetails(entity.ItemId);
                entity.CategoryId = dicDetailEntity.DictionaryId;
                var dicEntity = dicSevice.GetDictionary(entity.CategoryId);
                entity.CategoryName = dicEntity.Name;
                if (entity.Id == 0)
                {
                    //var isCorrect = IsCorrectSelect("DailyReimburseCategory", entity.CategoryName, entity.ItemId);
                    //if (!isCorrect.Success)
                    //{
                    //    return Json(isCorrect);
                    //}

                    if (list.Count > 0)
                    {
                        entity.Id = list[list.Count - 1].Id + 1;
                    }
                    else
                    {
                        entity.Id = 1;
                    }
                    list.Add(entity);
                    SessionData = list;
                  
                }
                else
                {
                    foreach (var item in list)
                    {
                        if (item.Id == entity.Id)
                        {
                            item.CategoryId = entity.CategoryId;
                            item.CategoryName = entity.CategoryName;
                            item.ItemName = entity.ItemName;
                            item.Amount = entity.Amount;
                        }
                    }
                    SessionData = list;

                }
            }
            catch (Exception e)
            {

                result = new BoolMessage(false,e.Message);
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
            var result = BoolMessage.True;
            try
            {
                var list = (List<DailyReimburseDetails>)SessionData;

                var ids = StringHelper.ConvertToArrayInt(id);
                for (int i = 0; i < ids.Length; i++)
                {
                    foreach (var item in list)
                    {
                        if (item.Id == ids[i])
                        {
                            list.Remove(item);
                            break;
                        }
                    }
                }
                SessionData = list;
            }
            catch (Exception e)
            {

                result= new BoolMessage(false,e.Message);
            }
            
            
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
        /// 判断所选的大类和项目名称是否匹配
        /// </summary>
        /// <param name="dicCode"></param>
        /// <param name="dicName"></param>
        /// <param name="dicDetaiId"></param>
        /// <returns></returns>
        private BoolMessage IsCorrectSelect(string dicCode, string dicName, int dicDetaiId)
        {
            if (!string.IsNullOrEmpty(dicName))
            {
                dicName = dicName.Substring(dicName.IndexOf(":") + 1);
            }
            var dicId = dicSevice.GetIDByCodeAndName(dicCode, dicName);
            var dicDetailEntity = dicSevice.GetDetails(dicDetaiId);

            if (dicId == dicDetailEntity.DictionaryId)
            {
                return  BoolMessage.True;
            }
            else
            {
                
                return new BoolMessage(false,"项目名称 和所选 费用类别 不匹配");
            }

        }
    }
}
