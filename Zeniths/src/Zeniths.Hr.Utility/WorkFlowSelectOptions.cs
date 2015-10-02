using System.Web.Mvc;
using Zeniths.Helper;

namespace Zeniths.Hr.Utility
{
    /// <summary>
    /// 工作流下拉选项类
    /// </summary>
    public static class WorkFlowSelectOptions
    {
        /// <summary>
        /// 获取流程表单下拉选项
        /// </summary>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString WorkFlowFormCategory(string selected = null)
        {
            var sz = new[] { "案件分流", "案件类", "办公类", "采购类", "审理类", "信访类", "业务类", "自定义表单" };
            return MvcHtmlString.Create(WebHelper.GetSelectOptions(sz, selected));
        }
    }
}