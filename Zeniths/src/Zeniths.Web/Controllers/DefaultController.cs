using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Helper;
using Zeniths.MvcUtility;
using Zeniths.Utility;

namespace Zeniths.Web.Controllers
{
    public class DefaultController : JsonController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var service = new SystemMenuService();
            var menus = service.GetEnabledList();
            var nodes = TreeHelper.Build(menus, p => p.ParentId == 0, (node, instace) =>
            {
                node.IconCls = instace.WebCls;
                node.Url = instace.WebUrl;
                if (node.Children != null)
                {
                    node.State = instace.IsExpand ? TreeNodeState.Open : TreeNodeState.Closed;
                }
            });
            return JsonNet(nodes);
        }
    }
}