using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Auth.Utility;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;

namespace Zeniths.Web.Areas.Auth.Controllers
{
    public class SystemDictionaryController : AuthBaseController
    {
        private readonly SystemDictionaryService service = new SystemDictionaryService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取系统数据字典树节点
        /// </summary>
        public ActionResult GetDictionaryTree()
        {
            var dics = service.GetDictionaryList();
            dics.Insert(0, new SystemDictionary
            {
                ParentId = -1,
                Name = "数据字典"
            });
            var nodes = TreeHelper.Build(dics, p => p.ParentId == -1, (node, instace) =>
            {
                if (instace.Id == 0)
                {
                    node.IconCls = "icon-house";
                }
                else if (node.Children != null)
                {
                    node.IconCls = string.Empty;
                }
                else
                {
                    node.IconCls = "icon-plugin";
                }
            });
            return JsonNet(nodes);
        }

        public ActionResult CreateDictionary()
        {
            var parentId = Request.QueryString["parentId"].ToInt();
            var sortPath = Request.QueryString["sortPath"];
            return EditDictionaryCore(new SystemDictionary
            {
                ParentId = parentId,
                SortPath = sortPath
            });
        }

        public ActionResult EditDictionary(string id)
        {
            var entity = service.GetDictionary(id.ToInt());
            return EditDictionaryCore(entity);
        }

        private ActionResult EditDictionaryCore(SystemDictionary entity)
        {
            if (entity.ParentId > 0)
            {
                ViewBag.ParentEntity = service.GetDictionary(entity.ParentId);
            }
            return View("DictionaryEdit", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDictionary(SystemDictionary entity)
        {
            var hasResult = service.ExistsDictionary(entity.Code, entity.Id);
            if (hasResult.Failure)
            {
                return JsonNet(hasResult);
            }
            entity.NameSpell = SpellHelper.ConvertSpell(entity.Name);
            var result = entity.Id == 0 ? service.InsertDictionary(entity) : service.UpdateDictionary(entity);
            return JsonNet(result);
        }

        [HttpPost]
        public ActionResult DeleteDictionary(string id)
        {
            var result = service.DeleteDictionary(StringHelper.ConvertToArrayInt(id));
            return JsonNet(result);
        }

        /// <summary>
        /// 保存树父节点
        /// </summary>
        public ActionResult SaveParent()
        {
            int id = Request.Form["id"].ToInt();
            int newParentId = Request.Form["newParentId"].ToInt();
            var result = service.SaveParentDictionary(id, newParentId);
            return JsonNet(result);
        }

        /// <summary>
        /// 树排序路径
        /// </summary>
        public ActionResult SaveSort()
        {
            var list = Request.Form.AllKeys.Select(key => new PrimaryKeyValue(key, nameof(SystemDictionary.SortPath), Request.Form[key]));
            var result = service.UpdateSortPathDictionary(list);
            return JsonNet(result);
        }


        public ActionResult DetailsGrid()
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var dictionaryId = Request.Form["dictionaryId"].ToInt();
            var detailsKey = Request.Form["detailsKey"];
            var list = service.GetPageDetailsList(pageIndex, pageSize, orderName, orderDir, dictionaryId, detailsKey);
            return View(list);
        }

        public ActionResult CreateDetails()
        {
            var dictionaryId = Request.QueryString["dictionaryId"].ToInt();
            return EditDetailsCore(new SystemDictionaryDetails
            {
                DictionaryId = dictionaryId
            });
        }

        public ActionResult EditDetails(string id)
        {
            var entity = service.GetDetails(id.ToInt());
            return EditDetailsCore(entity);
        }

        private ActionResult EditDetailsCore(SystemDictionaryDetails entity)
        {
            if (entity.DictionaryId > 0)
            {
                ViewBag.DictionaryEntity = service.GetDictionary(entity.DictionaryId);
            }
            return View("DetailsEdit", entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDetails(SystemDictionaryDetails entity)
        {
            var hasResult = service.ExistsDetails(entity.Name, entity.DictionaryId, entity.Id);
            if (hasResult.Failure)
            {
                return JsonNet(hasResult);
            }
            entity.NameSpell = SpellHelper.ConvertSpell(entity.Name);
            var result = entity.Id == 0 ? service.InsertDetails(entity) : service.UpdateDetails(entity);
            return JsonNet(result);
        }

        [HttpPost]
        public ActionResult DeleteDetails(string id)
        {
            var result = service.DeleteDetails(StringHelper.ConvertToArrayInt(id));
            return JsonNet(result);
        }

        public ActionResult DetailsView(string id)
        {
            var entity = service.GetDetails(id.ToInt());
            if (entity.DictionaryId > 0)
            {
                ViewBag.DictionaryEntity = service.GetDictionary(entity.DictionaryId);
            }
            return View(entity);
        }

        public ActionResult ExportDetails()
        {
            var dictionaryId = Request.QueryString["dictionaryId"].ToInt();
            return Export(service.GetEnabledDetailsListByDicId(dictionaryId));
        }

    }
}