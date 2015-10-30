using System;
using System.Linq;
using System.Web.Mvc;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.WorkFlow.Service;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Web.Areas.WorkFlow.Controllers
{
    public class FlowRunController : WorkFlowBaseController
    {
        public ActionResult Start(string flowId)
        {
            if (string.IsNullOrEmpty(flowId))
            {
                ViewBag.ErrorMessage = "无效的流程标识,请检查流程启动Url!";
                return View("Error");
            }
            var design = WorkFlowHelper.GetWorkFlowDesign(flowId);
            if (design == null)
            {
                ViewBag.ErrorMessage = "未找到流程!";
                return View("Error");
            }
            var firstStepId = WorkFlowHelper.GetFirstStepId(flowId);
            var currentStep = WorkFlowHelper.GetStepSetting(flowId, firstStepId);
            var formId = currentStep.FormName;//目前只显示一个表单
            var formService = new FlowFormService();
            string src = formService.GetUrl(formId.ToInt());
            if (src.IsEmpty())
            {
                ViewBag.ErrorMessage = "表单的地址无效";
                return View("Error");
            }
            string query = $"flowId={flowId}&stepId={firstStepId}";
            var fix = WebHelper.GetUrlJoinSymbol(src);
            return Redirect(src + fix + query);
        }

        public ActionResult Process(string taskId)
        {
            if (string.IsNullOrEmpty(taskId))
            {
                ViewBag.ErrorMessage = "无效的流程任务标识,请检查流程处理Url!";
                return View("Error");
            }
            var taskService = new FlowTaskService();
            var task = taskService.Get(taskId);
            if (task == null)
            {
                ViewBag.ErrorMessage = "无效的流程任务!";
                return View("Error");
            }
            if (task.Status.InArray(2, 3, 4, 5))
            {
                ViewBag.ErrorMessage = "该任务已处理,请刷新您的待办列表!";
                return View("Error");
            }
            if (task.ReceiveId != CurrentUserId.ToString())
            {
                ViewBag.ErrorMessage = "您没有权限处理当前任务!";
                return View("Error");
            }

            taskService.UpdateReadDateTime(taskId, DateTime.Now);

            var currentStep = WorkFlowHelper.GetStepSetting(task.FlowId, task.StepId);
            var formId = currentStep.FormName;
            var formService = new FlowFormService();
            string src = formService.GetUrl(formId.ToInt());
            if (src.IsEmpty())
            {
                ViewBag.ErrorMessage = "表单的地址无效";
                return View("Error");
            }
            string query = $"flowId={task.FlowId}&stepId={task.StepId}&taskId={task.Id}&flowInstanceId={task.FlowInstanceId}&businessId={task.BusinessId}";
            var fix = WebHelper.GetUrlJoinSymbol(src);
            return Redirect(src + fix + query);
        }

        public ActionResult Send(string flowId, string stepId, string taskId, string flowInstanceId, string businessId)
        {
            ViewBag.FlowId = flowId;
            ViewBag.FlowInstanceId = flowInstanceId;
            ViewBag.CurrentStep = WorkFlowHelper.GetStepSetting(flowId, stepId);
            ViewBag.NextSteps = WorkFlowHelper.GetExecuteNextSteps(flowId, stepId, Request.QueryString, Request.Form);
            ViewBag.FlowDesign = WorkFlowHelper.GetWorkFlowDesign(flowId);
            return View();
        }

        public ActionResult Back(string flowId, string stepId, string taskId, string flowInstanceId, string businessId)
        {
            ViewBag.FlowId = flowId;
            ViewBag.FlowInstanceId = flowInstanceId;
            ViewBag.CurrentStep = WorkFlowHelper.GetStepSetting(flowId, stepId);
            ViewBag.PrevSteps = WorkFlowHelper.GetBackSteps(taskId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Execute()
        {
            string workflowParams = Request.Form["_workflow_execute_params"];
            WorkFlowEngine engine = new WorkFlowEngine();
            var executeData = engine.GetExecuteData(workflowParams,CurrentUser,Request.QueryString,Request.Form);
            var result = engine.Process(executeData);
            if (result.Success)
            {
                #region Result

                var currentUserIdString = CurrentUserId.ToString();
                var url = string.Empty;
                var nextTasks = result.NextTasks?.Where(p => p.Status.InArray(0, 1) && p.ReceiveId == currentUserIdString);
                var nextTask = nextTasks?.FirstOrDefault();
                if (nextTask != null)
                {
                    url = Url.Action("Process", new {taskId = nextTask.Id});
                }

                return Json(new
                {
                    success = true,
                    message = result.Message,
                    debugMessage = result.DebugMessage,
                    url = url
                });

                #endregion
            }
            else
            {
                return Json(new { success = false, message = result.Message, debugMessage = result.DebugMessage });
            }
        }

    }
}