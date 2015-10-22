using System.Web.Mvc;
using Zeniths.Auth.Utility;
using Zeniths.Helper;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 工作流下拉选项类
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// 获取流程表单分类下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString FlowFormCategoryOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("FormCategory", selected));
        }

        /// <summary>
        /// 获取流程分类下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString FlowCategoryOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("FlowCategory", selected));
        }
    }
}