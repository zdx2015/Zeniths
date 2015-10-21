using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Auth.Entity;
using Zeniths.MvcUtility;

namespace Zeniths.Auth.Utility
{
    public class AuthBaseController : JsonController
    {
        /// <summary>
        /// 当前登录用户主键
        /// </summary>
        public static int CurrentUserId
        {
            get { return CurrentUser.Id; }
        }

        /// <summary>
        /// 当前登录用户账号
        /// </summary>
        public static string CurrentUserAccount
        {
            get { return CurrentUser.Account; }
        }

        /// <summary>
        /// 当前登录用户姓名
        /// </summary>
        public static string CurrentUserName
        {
            get { return CurrentUser.Name; }
        }

        /// <summary>
        /// 当前登录用户对象
        /// </summary>
        public static SystemUser CurrentUser
        {
            get { return AuthHelper.GetLoginUser(); }
        }

        /// <summary>
        /// 当前登录用户部门主键
        /// </summary>
        public static int CurrentDepartmentId
        {
            get { return CurrentDepartment.Id; }
        }

        /// <summary>
        /// 当前登录用户部门名称
        /// </summary>
        public static string CurrentDepartmentName
        {
            get { return CurrentDepartment.Name; }
        }

        /// <summary>
        /// 当前登录用户部门对象
        /// </summary>
        public static SystemDepartment CurrentDepartment
        {
            get { return AuthHelper.GetLoginDepartment(); }
        }
    }
}