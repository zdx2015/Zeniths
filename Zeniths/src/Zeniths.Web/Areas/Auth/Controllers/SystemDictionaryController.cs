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
        /// 获取系统数据字典列表
        /// </summary>
        public ActionResult GetDictionaryList()
        {
            var dics = service.GetDictionaryList();
            dics.Insert(0, new SystemDictionary
            {
                Id = -1,
                ParentId = 0,
                Code = "_root",
                Name = "数据字典"
            });
            var nodes = TreeHelper.Build(dics, p => p.ParentId == 0, (node, instace) =>
            {
                if (instace.Id == -1)
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
            if (parentId > 0)
            {
                ViewBag.ParentEntity = service.GetDictionary(parentId);
            }
            return EditDictionaryCore(new SystemDictionary
            {
                ParentId = parentId,
                SortPath = sortPath
            });
        }

        public ActionResult EditDictionary(int id)
        {
            var entity = service.GetDictionary(id);
            if (entity.ParentId > 0)
            {
                ViewBag.ParentEntity = service.GetDictionary(entity.ParentId);
            }
            return EditDictionaryCore(entity);
        }

        private ActionResult EditDictionaryCore(SystemDictionary entity)
        {
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


        public ActionResult DetailsGrid()
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetPageDetailsList(pageIndex, pageSize, orderName, orderDir);
            return View(list);
        }
    }
}