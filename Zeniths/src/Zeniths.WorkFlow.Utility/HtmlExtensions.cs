using System.Web.Mvc;
using Zeniths.Auth.Utility;
using Zeniths.Configuration;
using Zeniths.Helper;
using Zeniths.WorkFlow.Service;

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
        /// <param name="selected">选中的表单分类值</param>
        /// <returns></returns>
        public static MvcHtmlString FlowFormCategoryOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("FormCategory", selected));
        }

        /// <summary>
        /// 获取流程表单下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="isCategoryGroup">是否按表单分类分组</param>
        /// <param name="selected">选中的表单值</param>
        /// <returns></returns>
        public static MvcHtmlString FlowFormOptions(this HtmlHelper helper, bool isCategoryGroup = true, string selected = null)
        {
            var service = new FlowFormService();
            var formList = service.GetEnabledList();
            var result = isCategoryGroup
                ? WebHelper.GetSelectGroupOptions(formList, selectedValue: selected)
                : WebHelper.GetSelectOptions(formList, selectedValue: selected);
            return MvcHtmlString.Create(result);
        }

        /// <summary>
        /// 获取流程分类下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的流程分类值</param>
        /// <returns></returns>
        public static MvcHtmlString FlowCategoryOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("FlowCategory", selected));
        }
        public static MvcHtmlString BudgetCategoryOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("DailyReimburseCategory", selected));
        }
        /// <summary>
        /// 生成工作流参数
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString WorkflowExecuteParams(this HtmlHelper helper)
        {
            ExecuteParam param = new ExecuteParam();

            param.FlowId = WebHelper.GetQueryString("FlowId");
            param.StepId = WebHelper.GetQueryString("StepId");
            param.TaskId = WebHelper.GetQueryString("TaskId");
            param.BusinessId = WebHelper.GetQueryString("BusinessId");
            param.FlowInstanceId = WebHelper.GetQueryString("FlowInstanceId");

            return MvcHtmlString.Create(JsonHelper.Serialize(param));
        }
    }
}