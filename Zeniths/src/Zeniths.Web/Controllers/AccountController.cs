using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.MvcUtility;

namespace Zeniths.Web.Controllers
{
    public class AccountController : JsonController
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userName, string password)
        {
            if (userName.Equals("admin"))
            {
                return JsonNet(new JsonMessage(true));
            }
            return JsonNet(new JsonMessage(false, "账号或者密码错误!"));
        }
    }
}