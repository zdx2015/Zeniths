using System.Web.Mvc;
using Zeniths.Auth.Service;
using Zeniths.Helper;

namespace Zeniths.Hr.Utility
{
    /// <summary>
    /// 工作流下拉选项类
    /// </summary>
    public static class HtmlExtensions
    {
        ///// <summary>
        ///// 获取流程表单下拉选项
        ///// </summary>
        ///// <param name="helper"></param>
        ///// <param name="selected">选中的值</param>
        ///// <returns></returns>
        //public static MvcHtmlString WorkFlowFormCategoryOptions(this HtmlHelper helper, string selected = null)
        //{
        //    var options = AuthHelper.DictionaryOptions("WorkFlow", selected);
        //    return MvcHtmlString.Create(options);
        //}

        /// <summary>
        /// 获取用户下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的表单值</param>
        /// <returns></returns>
        public static MvcHtmlString ShareUserOptions(this HtmlHelper helper, string selected = null)
        {
            var service = new SystemUserService();
            var formList = service.GetEnabledList();
            var result = WebHelper.GetSelectOptions(formList, selectedValue: selected);
            return MvcHtmlString.Create(result);
        }
    }
}