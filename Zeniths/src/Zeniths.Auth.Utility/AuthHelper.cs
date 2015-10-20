using System;
using System.Text;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Extensions;
using Zeniths.Helper;

namespace Zeniths.Auth.Utility
{
    public static class AuthHelper
    {
        /// <summary>
        /// 生成数据字典下拉选项
        /// </summary>
        /// <param name="dicCode">数据字典编码</param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static string BuildDicOptions(string dicCode, string selected = null)
        {
            if (string.IsNullOrEmpty(dicCode))
            {
                throw new ArgumentNullException(nameof(dicCode), "数据字典编码不允许为空");
            }
            var service = new SystemDictionaryService();
            var detailsList = service.GetEnabledDetailsListByDicCode(dicCode);
            var options = new StringBuilder();
            foreach (SystemDictionaryDetails item in detailsList)
            {
                string name = item.Name;
                string value = item.Value.ToStringOrEmpty();
                string display = $"{StringHelper.GetFirstAlpha(name).ToUpper()}:{name}";
                options.AppendFormat("<option value=\"{0}\"{1}>{2}</option>", value,
                    value.Equals(selected.ToStringOrEmpty()) ? " selected" : string.Empty, display);
            }
            return options.ToString();
        }

        /// <summary>
        /// 获取部门节点图标Cls
        /// </summary>
        /// <param name="department">部门实体</param>
        public static string GetDepartmentIconCls(SystemDepartment department)
        {
            return department.Id == 0 ? "icon-chart_organisation" : "icon-users";
        }

        /// <summary>
        /// 生成异常对象
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static Zeniths.Auth.Entity.SystemException BuildExceptionEntity(Exception ex)
        {
            return new Zeniths.Auth.Entity.SystemException
            {
                Message = ex.Message,
                Details = StringHelper.BuildExceptionDetails(ex),
                IPAddress = WebHelper.GetClientIP(),
                CreateDateTime = DateTime.Now
            };
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
    }
}