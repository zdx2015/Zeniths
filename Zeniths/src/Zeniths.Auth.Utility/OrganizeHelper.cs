using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Extensions;

namespace Zeniths.Auth.Utility
{
    /// <summary>
    /// 组织机构帮助类
    /// </summary>
    public static class OrganizeHelper
    {

        public const string DEPTPREFIX = "dept_";
        public const string USERPREFIX = "user_";

        /// <summary>
        /// 获取部门节点图标Cls
        /// </summary>
        /// <param name="department">部门实体</param>
        public static string GetDepartmentIconCls(SystemDepartment department)
        {
            return department.Id == 0 ? "icon-chart_organisation" : "icon-users";
        }

        /// <summary>
        /// 获取用户节点图标Cls
        /// </summary>
        /// <param name="user">用户实体</param>
        public static string GetUserIconCls(SystemUser user)
        {
            return "icon-user_business_boss";
        }

        /// <summary>
        /// 部门Id添加标识前缀(dept_)
        /// </summary>
        /// <param name="id">原始部门Id</param>
        /// <returns>返回带标识符的部门Id</returns>
        public static string AddDepartmentPrefix(int id)
        {
            return DEPTPREFIX + id;
        }

        /// <summary>
        /// 清除部门Id中标识符
        /// </summary>
        /// <param name="deptId">标识符部门Id</param>
        /// <returns>返回原始部门Id</returns>
        public static int ClearDepartmentPrefix(string deptId)
        {
            return string.IsNullOrEmpty(deptId) ? 0 : deptId.Replace(DEPTPREFIX, string.Empty).ToInt();
        }

        /// <summary>
        /// 指定部门Id标示符是否是部门
        /// </summary>
        /// <param name="deptId">标识符部门Id</param>
        /// <returns>如果是部门Id标示符返回true</returns>
        public static bool IsDepartment(string deptId)
        {
            return deptId.StartsWith(DEPTPREFIX);
        }

        /// <summary>
        /// 用户Id添加标识前缀(user_)
        /// </summary>
        /// <param name="id">原始用户Id</param>
        /// <returns>返回带标识符的用户Id</returns>
        public static string AddUserPrefix(int id)
        {
            return USERPREFIX + id;
        }

        /// <summary>
        /// 清除用户Id中标识符
        /// </summary>
        /// <param name="userId">标识符用户Id</param>
        /// <returns>返回原始用户Id</returns>
        public static int ClearUserPrefix(string userId)
        {
            return string.IsNullOrEmpty(userId) ? 0 : userId.Replace(USERPREFIX, string.Empty).ToInt();
        }

        /// <summary>
        /// 指定用户Id标示符是否是用户
        /// </summary>
        /// <param name="userId">标识符用户Id</param>
        /// <returns>如果是用户Id标示符返回true</returns>
        public static bool IsUser(string userId)
        {
            return userId.StartsWith(USERPREFIX);
        }

        /// <summary>
        /// 用户是否过期
        /// </summary>
        /// <param name="entity">用户对象</param>
        /// <returns></returns>
        public static bool IsUserExpire(SystemUser entity)
        {
            return new SystemUserService().IsUserExpire(entity);
        }

        /// <summary>
        /// 获取当前登录用户对象
        /// </summary>
        public static SystemUser GetLoginUser()
        {
            var key = "_Zeniths.User";
            if (HttpContext.Current.Session[key] == null)
            {
                var userService = new SystemUserService();
                var account = HttpContext.Current.User.Identity.Name;
                HttpContext.Current.Session[key] = userService.GetByAccount(account);
            }
            return HttpContext.Current.Session[key] as SystemUser;
        }

        /// <summary>
        /// 获取当前登录用户部门对象
        /// </summary>
        public static SystemDepartment GetLoginDepartment()
        {
            var key = "_Zeniths.Department";
            if (HttpContext.Current.Session[key] == null)
            {
                var deptService = new SystemDepartmentService();
                var currentUser = GetLoginUser();
                if (currentUser.DepartmentId == 0)
                {
                    throw new ApplicationException($"请指定当前用户 {currentUser.Account} 的部门主键");
                }
                HttpContext.Current.Session[key] = deptService.Get(currentUser.DepartmentId);
            }
            return HttpContext.Current.Session[key] as SystemDepartment;
        }


        /// <summary>
        /// 获取指定部门Id的所有用户
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回此部门下的所有用户列表</returns>
        public static List<SystemUser> GetUsersByDepartment(int departmentId)
        {
            var userService = new SystemUserService();
            var childs = GetAllChildsIdArray(departmentId);
            List<int> ids = new List<int>();
            ids.Add(departmentId);
            ids.AddRange(childs);
            return userService.GetEnabledList(ids.ToArray());
        }

        /// <summary>
        /// 获取指定组织机构字符串的所有用户
        /// </summary>
        /// <param name="ids">组织机构Id字符串</param>
        /// <returns>返回此字符串对应的所有用户列表</returns>
        public static List<SystemUser> GetAllUsers(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return new List<SystemUser>();
            }
            string[] idArray = ids.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var userList = new List<SystemUser>();
            var userService = new SystemUserService();
            foreach (string id in idArray)
            {
                if (IsUser(id))//人员
                {
                    userList.Add(userService.Get(ClearUserPrefix(id)));
                }
                else if (IsDepartment(id))//部门
                {
                    userList.AddRange(GetUsersByDepartment(ClearDepartmentPrefix(id)));
                }
            }
            userList.RemoveAll(p => p == null);
            return userList.Distinct(new UsersEqualityComparer()).ToList();
        }


        /// <summary>
        /// 获取指定组织机构字符串的所有用户Id
        /// </summary>
        /// <param name="ids">组织机构Id字符串</param>
        /// <returns>返回此字符串对应的所有用户Id数组</returns>
        public static int[] GetAllUserIds(string ids)
        {
            var users = GetAllUsers(ids);
            return users.Select(user => user.Id).ToArray();
        }

        /// <summary>
        /// 获取指定组织机构字符串的名称
        /// </summary>
        /// <param name="ids">组织机构Id字符串</param>
        /// <returns>返回指定组织机构字符串的名称(逗号隔开)</returns>
        public static string GetIdStringNames(string ids)
        {
            if (string.IsNullOrEmpty(ids)) return string.Empty;
            string[] idArray = ids.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder sb = new StringBuilder(idArray.Length * 50);
            int i = 0;
            foreach (var id in idArray)
            {
                if (string.IsNullOrEmpty(id)) continue;

                sb.Append(GetIdStringName(id));
                if (i++ < idArray.Length - 1)
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取指定组织机构Id的名称
        /// </summary>
        /// <param name="idString">组织机构Id</param>
        /// <returns></returns>
        public static string GetIdStringName(string idString)
        {
            var userService = new SystemUserService();
            var deptService = new SystemDepartmentService();
            if (IsUser(idString))//用户
            {
                return userService.GetName(ClearUserPrefix(idString));
            }
            if (IsDepartment(idString))//机构
            {
                return deptService.GetName(ClearDepartmentPrefix(idString));
            }
            return string.Empty;
        }


        /// <summary>
        /// 判断指定用户主键是否在一个组织机构字符串里
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="ids">组织机构Id字符串</param>
        /// <returns>如果在,返回true</returns>
        public static bool IsContainsUser(int userId, string ids)
        {
            if (string.IsNullOrEmpty(ids)) return false;
            var user = GetAllUsers(ids).Find(p => p.Id == userId);
            return user != null;
        }

        /// <summary>
        /// 获取用户的部门领导主键
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>返回指定用户部门领导主键</returns>
        public static int GetDepartmentLeaderId(int userId)
        {
            var userService = new SystemUserService();
            return userService.GetDepartmentLeaderId(userId);
        }

        /// <summary>
        /// 获取用户的部门领导对象
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>返回指定用户部门领导对象</returns>
        public static SystemUser GetDepartmentLeader(int userId)
        {
            var userService = new SystemUserService();
            return userService.GetDepartmentLeader(userId);
        }

        /// <summary>
        /// 获取用户的分管领导主键
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>返回指定用户分管领导主键</returns>
        public static int GetChargeLeaderId(int userId)
        {
            var userService = new SystemUserService();
            return userService.GetChargeLeaderId(userId);
        }

        /// <summary>
        /// 获取用户的分管领导对象
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>返回指定用户分管领导对象</returns>
        public static SystemUser GetChargeLeader(int userId)
        {
            var userService = new SystemUserService();
            return userService.GetChargeLeader(userId);
        }

        /// <summary>
        /// 获取用户的主管领导主键
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>返回指定用户主管领导主键</returns>
        public static int GetMainLeaderId(int userId)
        {
            var userService = new SystemUserService();
            return userService.GetMainLeaderId(userId);
        }

        /// <summary>
        /// 获取用户的主管领导对象
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>返回指定用户主管领导对象</returns>
        public static SystemUser GetMainLeader(int userId)
        {
            var userService = new SystemUserService();
            return userService.GetMainLeader(userId);
        }

        /// <summary>
        /// 指定用户是否是部门领导
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>如果是部门领导返回true</returns>
        public static bool IsDepartmentLeader(int userId)
        {
            var userService = new SystemUserService();
            return userService.IsDepartmentLeader(userId);
        }

        /// <summary>
        /// 指定用户是否是分管领导
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>如果是分管领导返回true</returns>
        public static bool IsChargeLeader(int userId)
        {
            var userService = new SystemUserService();
            return userService.IsChargeLeader(userId);
        }

        /// <summary>
        /// 指定用户是否是主管领导
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <returns>如果是主管领导返回true</returns>
        public static bool IsMainLeader(int userId)
        {
            var userService = new SystemUserService();
            return userService.IsMainLeader(userId);
        }


        /// <summary>
        /// 获取指定部门主键的所有上级部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回指定部门主键的所有上级部门列表</returns>
        public static List<SystemDepartment> GetAllParents(int departmentId)
        {
            var deptService = new SystemDepartmentService();
            return deptService.GetAllParents(departmentId);
        }

        /// <summary>
        /// 获取指定部门主键的所有上级部门主键数组
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回指定部门主键的所有上级部门主键数组</returns>
        public static int[] GetAllParentsIdArray(int departmentId)
        {
            var deptService = new SystemDepartmentService();
            return deptService.GetAllParentsIdArray(departmentId);
        }

        /// <summary>
        /// 获取指定部门主键的所有下级部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回指定部门主键的所有下级部门列表</returns>
        public static List<SystemDepartment> GetAllChilds(int departmentId)
        {
            var deptService = new SystemDepartmentService();
            return deptService.GetAllChilds(departmentId);
        }

        /// <summary>
        /// 获取指定部门主键的所有下级部门主键数组
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回指定部门主键的所有下级部门主键数组</returns>
        public static int[] GetAllChildsIdArray(int departmentId)
        {
            var deptService = new SystemDepartmentService();
            return deptService.GetAllChildsIdArray(departmentId);
        }

    }
}