using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Auth.Utility;
using Zeniths.Helper;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Web.Areas.WorkFlow.Controllers
{
    public class SelectMemberController : WorkFlowBaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrganize()
        {
            SystemDepartmentService deptService = new SystemDepartmentService();
            SystemUserService userService = new SystemUserService();

            var depts = deptService.GetEnabledList();
            var users = userService.GetEnabledList();
            var nodes = TreeHelper.Build(depts, p => p.ParentId == 0, (node, instace) =>
            {
                node.Id = OrganizeHelper.AddDepartmentPrefix(instace.Id);
                node.IconCls = OrganizeHelper.GetDepartmentIconCls(instace);
                var ulist = users.Where(p => p.DepartmentId == instace.Id);
                if (node.Children == null)
                {
                    node.Children = new List<TreeNode>();
                }
                foreach (var user in ulist)
                {
                    var userNode = new TreeNode();
                    userNode.Id = OrganizeHelper.AddUserPrefix(user.Id);
                    userNode.ParentId = node.Id;
                    userNode.Text = user.Name;
                    userNode.IconCls = OrganizeHelper.GetUserIconCls(user);
                    node.Children.Add(userNode);
                }
            });
            return Json(nodes);
        }

        public ActionResult GetNames(string ids)
        {
            var text = OrganizeHelper.GetIdStringNames(ids);
            return Content(text);
        }
    }
}