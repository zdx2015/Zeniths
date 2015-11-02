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

        /// <summary>
        /// 获取步骤事件实例下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的步骤事件实例值</param>
        /// <returns></returns>
        public static MvcHtmlString FlowStepEventOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(WebHelper.GetSelectOptions(WorkFlowHelper.GetStepEventCaptionList(),
                nameof(WorkFlowEventCaptionAttribute.Cation),nameof(WorkFlowEventCaptionAttribute.Provider),selected));
        }

        /// <summary>
        /// 获取线事件实例下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的步骤事件实例值</param>
        /// <returns></returns>
        public static MvcHtmlString FlowLineEventOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(WebHelper.GetSelectOptions(WorkFlowHelper.GetLineEventCaptionList(),
                nameof(WorkFlowEventCaptionAttribute.Cation), nameof(WorkFlowEventCaptionAttribute.Provider), selected));
        }

        public static MvcHtmlString BudgetCategoryOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("DailyReimburseCategory", selected));
        }

        /// <summary>
        /// 获取请假状态下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的流程分类值</param>
        /// <returns></returns>
        public static MvcHtmlString EmployeeLeaveOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("EmployeeLeaveCategory", selected));
        }

        /// <summary>
        /// 获取加班状态下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的流程分类值</param>
        /// <returns></returns>
        public static MvcHtmlString EmployeeOvertimeOptions(this HtmlHelper helper, string selected = null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("EmployeeOvertimeCategory", selected));
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

        /// <summary>
        /// 获取当前流程模型
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString WorkFlowClientModel(this HtmlHelper helper)
        {
            var flowId = WebHelper.GetQueryString("FlowId");
            var stepId = WebHelper.GetQueryString("StepId");
            WorkFlowClientModel model = new WorkFlowClientModel();
            if (!string.IsNullOrEmpty(stepId))
            {
                var step = WorkFlowHelper.GetStepSetting(flowId, stepId);
                model.IsFirstStep = WorkFlowHelper.GetFirstStepId(flowId).Equals(stepId);
                model.IsLastStep = WorkFlowHelper.GetLastStepId(flowId).Equals(stepId);
                model.IsNeedOpinion = step.SignatureType.Equals("1") || step.SignatureType.Equals("2");
                model.IsNeedSignature = step.SignatureType.Equals("2");
                model.FlowId = flowId;
                model.StepId = stepId;
                model.TaskId = WebHelper.GetQueryString("TaskId");
                model.FlowInstanceId = WebHelper.GetQueryString("FlowInstanceId");
                model.BusinessId = WebHelper.GetQueryString("BusinessId");
            }
            return MvcHtmlString.Create(JsonHelper.Serialize(model));
        }

        /// <summary>
        /// 获取当前流程设计对象
        /// </summary>
        /// <returns></returns>
        public static WorkFlowDesign GetWorkFlowDesign()
        {
            var flowId = WebHelper.GetQueryString("FlowId");
            if (!string.IsNullOrEmpty(flowId))
            {
                return WorkFlowHelper.GetWorkFlowDesign(flowId);
            }
            return null;
        }

        /// <summary>
        /// 获取当前步骤名称
        /// </summary>
        /// <returns></returns>
        public static FlowStepSetting GetCurrentStep(this HtmlHelper helper)
        {
            var flowId = WebHelper.GetQueryString("FlowId");
            var stepId = WebHelper.GetQueryString("StepId");
            if (!string.IsNullOrEmpty(stepId))
            {
                return WorkFlowHelper.GetStepSetting(flowId, stepId);
            }
            return null;
        }

        /// <summary>
        /// 获取当前步骤名称
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentStepName(this HtmlHelper helper)
        {
            var flowId = WebHelper.GetQueryString("FlowId");
            var stepId = WebHelper.GetQueryString("StepId");
            if (!string.IsNullOrEmpty(stepId))
            {
                return WorkFlowHelper.GetStepName(flowId, stepId);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取流程启动Url
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="flowId">流程主键</param>
        /// <param name="businessId">业务主键</param>
        /// <returns></returns>
        public static MvcHtmlString GetFlowStartUrl(this HtmlHelper helper, string flowId, string businessId = null)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            // ReSharper disable once Mvc.AreaNotResolved
            return MvcHtmlString.Create(url.Action("Start", "FlowRun", new { area = "WorkFlow", flowId, businessId }));
        }

        /// <summary>
        /// 获取任务处理Url
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        public static MvcHtmlString GetFlowProcessUrl(this HtmlHelper helper, string taskId)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            // ReSharper disable once Mvc.AreaNotResolved
            return MvcHtmlString.Create(url.Action("Process", "FlowRun", new { area = "WorkFlow", taskId }));
        }

        /// <summary>
        /// 获取任务执行Url
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        public static MvcHtmlString GetFlowExecuteUrl(this HtmlHelper helper)
        {
            UrlHelper url = new UrlHelper(helper.ViewContext.RequestContext);
            // ReSharper disable once Mvc.AreaNotResolved
            return MvcHtmlString.Create(url.Action("Execute", "FlowRun", new { area = "WorkFlow" }));
        }
    }
}