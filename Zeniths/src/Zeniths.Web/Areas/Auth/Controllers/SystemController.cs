using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Zeniths.Auth.Service;
using Zeniths.Auth.Utility;
using Zeniths.Helper;
using Zeniths.Utility;

namespace Zeniths.Web.Areas.Auth.Controllers
{
    [Authorize]
    public class SystemController : AuthBaseController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string account, string password)
        {
            var userService = new SystemUserService();
            var result = userService.Login(account, password);
            if (result.Success) //登陆成功
            {
                FormsAuthentication.SetAuthCookie(account, false);
                return Json(new { success = true, message = "登陆成功", url = "/" });
            }
            return Json(result);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            OrganizeHelper.Logout();
            return RedirectToAction("Login");
        }

        //[ZenithsAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult GetMenuTree()
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
            return Json(nodes);
        }

        public ActionResult GetCurrentUser()
        {
            return Json(OrganizeHelper.GetCurrentUser());
        }

        public ActionResult WebIcons()
        {
            return View();
        }
    }
}