using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Utility;
using Zeniths.Configuration;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.WorkFlow.Entity;
using Zeniths.WorkFlow.Service;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 工作流引擎
    /// </summary>
    public class WorkFlowEngine
    {
        private ExecuteResult result;
        private List<FlowTask> nextTasks;
        private WorkFlowDesign design;
        private readonly FlowTaskService taskService = new FlowTaskService();
        private readonly object lockObject = new object();

        /// <summary>
        /// 开始处理流程
        /// </summary>
        /// <param name="execute">执行数据</param>
        /// <returns>返回执行结果</returns>
        public ExecuteResult Process(ExecuteData execute)
        {
            var eventArgs = new FlowEventArgs();
            eventArgs.FlowId = execute.FlowId;
            eventArgs.StepId = execute.StepId;
            eventArgs.FlowInstanceId = execute.FlowInstanceId;
            eventArgs.TaskId = execute.TaskId;
            eventArgs.BusinessId = execute.BusinessId;
            eventArgs.StepSetting = WorkFlowHelper.GetStepSetting(execute.FlowId, execute.StepId);
            eventArgs.QueryString = execute.QueryString;
            eventArgs.Form = execute.Form;
            eventArgs.CurrentUser = execute.SenderUser;
            eventArgs.ExecuteData = execute;

            var currentStepSetting = WorkFlowHelper.GetStepSetting(execute.FlowId, execute.StepId);
            IStepEvent stepEvent = ReflectionHelper.CreateInstance<IStepEvent>(currentStepSetting.ControlProvider);


            //步骤保存前事件
            if (stepEvent != null && (execute.ExecuteType == ExecuteType.Save))
            {
                var eventResult = stepEvent.OnBeforeSave(eventArgs);
                Debug.WriteLine($"执行步骤保存前事件：返回值：{eventResult.Success};{eventResult.Message}");
                if (eventResult.Failure)
                {
                    return new ExecuteResult(false, eventResult.Message);
                }
            }


            //保存业务数据
            #region 保存业务数据

            if (stepEvent != null)
            {
                switch (execute.ExecuteType)
                {
                    case ExecuteType.Save:
                    case ExecuteType.Submit:
                    case ExecuteType.Completed:
                        var saveResult = stepEvent.OnSaveData(eventArgs);
                        if (saveResult.Success)
                        {
                            execute.BusinessId = eventArgs.BusinessId;
                        }
                        else
                        {
                            return new ExecuteResult(false, saveResult.Message);
                        }
                        break;
                }
            }

            #endregion

            //步骤提交前事件
            if (stepEvent != null && (execute.ExecuteType == ExecuteType.Submit || execute.ExecuteType == ExecuteType.Completed))
            {
                var eventResult = stepEvent.OnBeforeSubmit(eventArgs);
                Debug.WriteLine($"执行步骤提交前事件：返回值：{eventResult.Success};{eventResult.Message}");
                if (eventResult.Failure)
                {
                    return new ExecuteResult(false, eventResult.Message);
                }
            }
            //步骤退回前事件
            if (stepEvent != null && execute.ExecuteType == ExecuteType.Back)
            {
                var eventResult = stepEvent.OnBeforeBack(eventArgs);
                Debug.WriteLine($"执行步骤退回前事件：返回值：{eventResult.Success};{eventResult.Message}");
                if (eventResult.Failure)
                {
                    return new ExecuteResult(false, eventResult.Message);
                }
            }


            //处理委托
            //foreach (var executeStep in execute.Steps)
            //{
            //    for (int i = 0; i < executeStep.Value.Count; i++)
            //    {
            //        Guid newUserID = bworkFlowDelegation.GetFlowDelegationByUserID(execute.FlowID, executeStep.Value[i].ID);
            //        if (newUserID != Guid.Empty && newUserID != executeStep.Value[i].ID)
            //        {
            //            executeStep.Value[i] = busers.Get(newUserID);
            //        }
            //    }
            //}

            var reslut = Execute(execute);
            Debug.WriteLine($"处理流程步骤结果：{(reslut.Success ? "成功" : "失败")}\r\n");
            Debug.WriteLine($"调试信息：{reslut.DebugMessage}");
            Debug.WriteLine($"返回信息：{reslut.Message}");
            //string msg = reslut.Messages;
            //string logContent = $"处理参数：{execute.ExecuteParam}<br/>处理结果：{(reslut.IsSuccess ? "成功" : "失败")}<br/>调试信息：{reslut.DebugMessages}<br/>返回信息：{reslut.Messages}";


            //日志
            //RoadFlow.Platform.Log.Add(string.Format("处理了流程({0})", wfInstalled.Name), logContent,
            //    RoadFlow.Platform.Log.Types.流程相关);


            eventArgs.NextTasks = reslut.NextTasks;
            eventArgs.FlowInstanceId = execute.FlowInstanceId;
            eventArgs.TaskId = execute.TaskId;

            //步骤保存后事件
            if (stepEvent != null && (execute.ExecuteType == ExecuteType.Save))
            {
                var eventResult = stepEvent.OnAfterSave(eventArgs);
                Debug.WriteLine($"执行步骤保存前事件：返回值：{eventResult.Success};{eventResult.Message}");
                if (eventResult.Failure)
                {
                    return new ExecuteResult(false, eventResult.Message);
                }
            }

            //步骤提交后事件
            if (stepEvent != null && (execute.ExecuteType == ExecuteType.Submit || execute.ExecuteType == ExecuteType.Completed))
            {
                var eventResult = stepEvent.OnAfterSubmit(eventArgs);
                Debug.WriteLine($"执行步骤提交后事件：返回值：{eventResult.Success};{eventResult.Message}");
                if (eventResult.Failure)
                {
                    return new ExecuteResult(false, eventResult.Message);
                }
            }
            //步骤退回后事件
            if (stepEvent != null && execute.ExecuteType == ExecuteType.Back)
            {
                var eventResult = stepEvent.OnAfterBack(eventArgs);
                Debug.WriteLine($"执行步骤退回后事件：返回值：{eventResult.Success};{eventResult.Message}");
                if (eventResult.Failure)
                {
                    return new ExecuteResult(false, eventResult.Message);
                }
            }

            return reslut;
        }

        /// <summary>
        /// 处理流程
        /// </summary>
        /// <param name="executeModel">处理实体</param>
        /// <returns></returns>
        private ExecuteResult Execute(ExecuteData executeModel)
        {
            result = new ExecuteResult();
            nextTasks = new List<FlowTask>();
            if (string.IsNullOrEmpty(executeModel.FlowId))
            {
                result.DebugMessage = "流程Id错误";
                result.Success = false;
                result.Message = "执行参数错误";
                return result;
            }

            design = WorkFlowHelper.GetWorkFlowDesign(executeModel.FlowId);
            if (design == null)
            {
                result.DebugMessage = "未找到流程运行时实体";
                result.Success = false;
                result.Message = "流程运行时为空";
                return result;
            }

            lock (lockObject)
            {
                switch (executeModel.ExecuteType)
                {
                    case ExecuteType.Back:
                        executeBack(executeModel);
                        break;
                    case ExecuteType.Save:
                        executeSave(executeModel);
                        break;
                    case ExecuteType.Submit:
                    case ExecuteType.Completed:
                        executeSubmit(executeModel);
                        break;
                    case ExecuteType.Redirect:
                        executeRedirect(executeModel);
                        break;
                    default:
                        result.DebugMessage = "流程处理类型为空";
                        result.Success = false;
                        result.Message = "流程处理类型为空";
                        return result;
                }

                result.NextTasks = nextTasks;
                return result;
            }
        }

        /// <summary>
        /// 获取流程执行数据
        /// </summary>
        /// <param name="executeParam"></param>
        /// <param name="currentUser"></param>
        /// <param name="queryString"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public ExecuteData GetExecuteData(string executeParam, SystemUser currentUser,
            NameValueCollection queryString, NameValueCollection form)
        {
            if (string.IsNullOrEmpty(executeParam))
            {
                throw new ArgumentException("流程执行参数不允许为空", nameof(executeParam));
            }
            var executeParamObject = JsonHelper.Deserialize<ExecuteParam>(executeParam);
            string flowId = executeParamObject.FlowId;
            string opinion = executeParamObject.Opinion;
            string stepId = executeParamObject.StepId;
            string taskId = executeParamObject.TaskId;
            string title = executeParamObject.Title;
            string businessId = executeParamObject.BusinessId;
            string flowInstanceId = executeParamObject.FlowInstanceId;
            bool? isAudit = executeParamObject.IsAudit;

            var workFlowDesign = WorkFlowHelper.GetWorkFlowDesign(flowId);
            if (workFlowDesign == null)
            {
                throw new ArgumentException("未找到流程设计对象,请确认流程是否已发布", nameof(flowId));
            }

            string opation = executeParamObject.Type.ToLower();

            var execute = new ExecuteData();
            execute.Opinion = string.IsNullOrEmpty(opinion) ? string.Empty : opinion.Trim();
            execute.FlowId = flowId;
            execute.FlowInstanceId = flowInstanceId;
            execute.BusinessId = businessId;
            execute.IsAudit = isAudit;
            execute.Note = string.Empty;
            execute.SenderUser = currentUser;
            execute.StepId = string.IsNullOrEmpty(stepId) ? WorkFlowHelper.GetFirstStepId(flowId) : stepId;
            execute.TaskId = taskId;
            execute.Title = string.IsNullOrEmpty(title) ? string.Empty : title;
            execute.QueryString = queryString;
            execute.Form = form;
            execute.ExecuteParam = executeParam;

            #region ExecuteType

            switch (opation)
            {
                case "submit":
                    execute.ExecuteType = ExecuteType.Submit;
                    break;
                case "save":
                    execute.ExecuteType = ExecuteType.Save;
                    break;
                case "back":
                    execute.ExecuteType = ExecuteType.Back;
                    break;
                case "completed":
                    execute.ExecuteType = ExecuteType.Completed;
                    break;
                case "redirect":
                    execute.ExecuteType = ExecuteType.Redirect;
                    break;
            }

            #endregion

            #region execute.Steps

            foreach (var step in executeParamObject.Steps)
            {
                if (!string.IsNullOrEmpty(step.Id))
                {
                    switch (execute.ExecuteType)
                    {
                        case ExecuteType.Submit:
                            execute.Steps.Add(step.Id, OrganizeHelper.GetAllUsers(step.Member));
                            break;
                        case ExecuteType.Back:
                            execute.Steps.Add(step.Id, new List<SystemUser>());
                            break;
                        case ExecuteType.Save:
                            break;
                        case ExecuteType.Completed:
                            break;
                        case ExecuteType.Redirect:
                            break;
                    }
                }
                if (execute.ExecuteType == ExecuteType.Redirect)
                {
                    execute.Steps.Add(string.Empty, OrganizeHelper.GetAllUsers(step.Member));
                }
            }

            #endregion


            Debug.WriteLine("执行参数：" + executeParam + "\r\n");

            return execute;
        }

        private void executeSubmit(ExecuteData executeModel)
        {
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                #region 验证

                //如果是第一步提交并且没有实例则先创建实例
                FlowTask currentTask = GetExecuteTask(executeModel);
                if (currentTask == null)
                {
                    result.DebugMessage = "未能创建或找到当前任务";
                    result.Success = false;
                    result.Message = "未能创建或找到当前任务";
                    return;
                }
                if (currentTask.Status.InArray(2, 3, 4))
                {
                    result.DebugMessage = "当前任务已处理";
                    result.Success = false;
                    result.Message = "当前任务已处理";
                    return;
                }

                var flowId = executeModel.FlowId;
                var currentStep = WorkFlowHelper.GetStepSetting(flowId, executeModel.StepId);
                if (currentStep == null)
                {
                    result.DebugMessage = "未找到当前步骤";
                    result.Success = false;
                    result.Message = "未找到当前步骤";
                    return;
                }

                #endregion

                int status = 0; //状态位 如果为-1则不提交到下一步

                #region 处理策略判断  完成本人当前任务
                if (currentTask.StepId != WorkFlowHelper.GetFirstStepId(flowId))//第一步不判断策略
                {
                    // 0:一人同意即可
                    // 1:所有人必须同意
                    // 2:独立处理
                    // 3:依据比例
                    switch (currentStep.ProcessPolicy.ToInt())
                    {
                        #region 一人同意即可

                        case 0://一人同意即可
                            var taskList1 = taskService.GetSiblingTaskList(currentTask.Id);//取出当前步骤中,所有的任务
                            foreach (var task in taskList1)
                            {
                                if (task.Id != currentTask.Id) //如果是其他人的任务
                                {
                                    if (task.Status.InArray(0, 1)) //如果其他人没有完成,那么自动完成那些任务,因为只要一个人处理,其他人就可以不用处理
                                    {
                                        taskService.Completed(task.Id, string.Empty, null, 4);//完成任务 状态为4他人已处理
                                    }
                                }
                                else
                                {
                                    taskService.Completed(task.Id, executeModel.Opinion, executeModel.IsAudit, 2);//完成任务 状态为2完成
                                }
                            }
                            break;

                        #endregion

                        #region 所有人必须处理
                        case 1://所有人必须处理
                            var taskList = taskService.GetSiblingTaskList(currentTask.Id);// 取出当前步骤中,所有的任务
                            if (taskList.Count > 1)
                            {
                                var noCompleted = taskList.Where(p => p.Status != 2);//取出没有完成的集合
                                if (noCompleted.Count() - 1 > 0) //减1是要把自己的这条任务减掉
                                {
                                    status = -1; //标识状态为-1,在下面的判断中 表示 不能提交到下个步骤
                                }
                            }
                            taskService.Completed(currentTask.Id, executeModel.Opinion, executeModel.IsAudit, 2);//只完成自己的这条任务,状态-1不能提交到下一步
                            break;
                        #endregion

                        #region 独立处理
                        case 2://独立处理
                            taskService.Completed(currentTask.Id, executeModel.Opinion, executeModel.IsAudit, 2);
                            break;
                        #endregion

                        #region 依据人数比例
                        case 3://依据人数比例
                            var taskList2 = taskService.GetSiblingTaskList(currentTask.Id);// 取出当前步骤中,所有的任务
                            if (taskList2.Count > 1)
                            {
                                decimal percentage = currentStep.ProcessPercentage.ToDecimal() <= 0 ? 100 : currentStep.ProcessPercentage.ToDecimal();//比例
                                if ((((decimal)(taskList2.Count(p => p.Status == 2) + 1) / (decimal)taskList2.Count) * 100).Round() < percentage)//没有达到比例
                                {
                                    status = -1;//不提交到下一步
                                }
                                else //如果达到比例,把其他没有完成的任务 自动设为他人已处理
                                {
                                    foreach (var task in taskList2)
                                    {
                                        if (task.Id != currentTask.Id && task.Status.InArray(0, 1))
                                        {
                                            taskService.Completed(task.Id, string.Empty, null, 4);//自动设为他人已处理
                                        }
                                    }
                                }
                            }
                            taskService.Completed(currentTask.Id, executeModel.Opinion, executeModel.IsAudit, 2);//完成自己的任务
                            break;
                            #endregion
                    }
                }
                else //完成第一步
                {
                    taskService.Completed(currentTask.Id, executeModel.Opinion, executeModel.IsAudit, 2);
                }

                #endregion

                #region 返回 

                //如果条件不满足则不创建后续任务，直到最后一个条件满足时才创建后续任务。等待中的任务 状态为：5 已不用
                if (status == -1)
                {
                    result.DebugMessage += "已发送,其他人未处理,不创建后续任务";
                    result.Success = true;
                    result.Message += "已发送,等待他人处理!";
                    result.NextTasks = nextTasks;
                    scope.Complete();
                    return;
                }

                //如果是完成,根本不需要创建后续步骤,所以在这里就结束了
                if (executeModel.ExecuteType == ExecuteType.Completed || executeModel.Steps == null || executeModel.Steps.Count == 0)
                {
                    result.DebugMessage += "流程完成";
                    result.Success = true;
                    result.Message += "流程完成";
                    scope.Complete();
                    return;
                }

                #endregion

                #region 发送到后续步骤

                foreach (var step in executeModel.Steps)
                {
                    foreach (var user in step.Value) //每一个步骤,发送个多个人或者1个
                    {
                        //如果发送的人已经一个相同的任务,但没有完成,说明已经发送过了无需发送
                        if (taskService.GetUserHasNoCompletedTasks(executeModel.FlowId, step.Key, currentTask.FlowInstanceId, user.Id.ToString()))
                        {
                            continue;
                        }

                        //待发送的步骤信息
                        var nextStep = WorkFlowHelper.GetStepSetting(executeModel.FlowId, step.Key);
                        if (nextStep == null)
                        {
                            continue;
                        }

                        // 0:不会签
                        // 1:一个步骤同意即可
                        // 2:所有步骤同意
                        // 3:依据比例
                        int countersignPolicy = nextStep.CountersignPolicy.ToInt();
                        bool isPassing = 0 == countersignPolicy; //是否通过

                        #region 如果下一步骤为会签，则要检查当前步骤的平级步骤是否已处理
                        if (0 != countersignPolicy)
                        {
                            var prevSteps = WorkFlowHelper.GetPrevSteps(executeModel.FlowId, nextStep.Uid);
                            switch (countersignPolicy)
                            {
                                #region 一个步骤同意即可

                                case 1://一个步骤同意即可
                                    isPassing = false;
                                    foreach (var prevStep in prevSteps)
                                    {
                                        if (IsPassing(prevStep, executeModel.FlowId, executeModel.FlowInstanceId, currentTask.PrevId))
                                        {
                                            isPassing = true;
                                            break;
                                        }
                                    }
                                    break;

                                #endregion

                                #region 所有步骤同意
                                case 2://所有步骤同意
                                    isPassing = true;
                                    foreach (var prevStep in prevSteps)
                                    {
                                        if (!IsPassing(prevStep, executeModel.FlowId, executeModel.FlowInstanceId, currentTask.PrevId))
                                        {
                                            isPassing = false;
                                            break;
                                        }
                                    }
                                    break;

                                #endregion

                                #region 依据比例
                                case 3://依据比例
                                    int passCount = 0;
                                    foreach (var prevStep in prevSteps)
                                    {
                                        if (IsPassing(prevStep, executeModel.FlowId, executeModel.FlowInstanceId, currentTask.PrevId))
                                        {
                                            passCount++;
                                        }
                                    }
                                    isPassing = (((decimal)passCount / (decimal)prevSteps.Count) * 100).Round() >= (nextStep.CountersignPercentage.ToDecimal() <= 0 ? 100 : nextStep.CountersignPercentage.ToDecimal());
                                    break;

                                    #endregion
                            }
                            if (isPassing)
                            {
                                var tjTasks = taskService.GetSiblingTaskList(currentTask.Id, false);//获取与当前步骤同层级的全部步骤
                                foreach (var tjTask in tjTasks)
                                {
                                    if (tjTask.Id == currentTask.Id || tjTask.Status.InArray(2, 3, 4, 5))
                                    {
                                        continue;
                                    }
                                    taskService.Completed(tjTask.Id, string.Empty, false, 4);//完成掉剩余那些会签的步骤
                                }
                            }
                        }
                        #endregion

                        if (isPassing) //可以创建后续步骤
                        {
                            var task = CreateNextTask(executeModel, currentTask, user, step.Key);
                            nextTasks.Add(task);
                        }
                    }
                }

                #endregion

                scope.Complete();

                #region 后续任务提示

                if (nextTasks.Count > 0)
                {
                    List<string> nextStepName = new List<string>();
                    foreach (var nstep in nextTasks)
                    {
                        nextStepName.Add(nstep.StepName);
                    }
                    var sendUsers = nextStepName.Distinct().ToList().ToSplitString();
                    result.DebugMessage += $"已发送到：{sendUsers}";
                    result.Success = true;
                    result.Message += $"已发送到：{sendUsers}";
                    result.NextTasks = nextTasks;
                }
                else
                {
                    result.DebugMessage += "已发送,等待其它步骤处理";
                    result.Success = true;
                    result.Message += "已发送,等待其它步骤处理";
                    result.NextTasks = nextTasks;
                }

                #endregion
            }
        }

        private void executeBack(ExecuteData executeModel)
        {
            var currentTask = taskService.Get(executeModel.TaskId);
            if (currentTask == null)
            {
                result.DebugMessage = "未能找到当前任务";
                result.Success = false;
                result.Message = "未能找到当前任务";
                return;
            }
            if (currentTask.Status.InArray(2, 3, 4))
            {
                result.DebugMessage = "当前任务已处理";
                result.Success = false;
                result.Message = "当前任务已处理";
                return;
            }

            var flowId = executeModel.FlowId;
            var currentStep = WorkFlowHelper.GetStepSetting(flowId, currentTask.StepId);
            if (currentStep == null)
            {
                result.DebugMessage = "未能找到当前步骤";
                result.Success = false;
                result.Message = "未能找到当前步骤";
                return;
            }
            if (currentTask.StepId == WorkFlowHelper.GetFirstStepId(flowId))
            {
                result.DebugMessage = "当前任务是流程第一步,不能退回";
                result.Success = false;
                result.Message = "当前任务是流程第一步,不能退回";
                return;
            }
            if (executeModel.Steps.Count == 0)
            {
                result.DebugMessage = "没有选择要退回的步骤";
                result.Success = false;
                result.Message = "没有选择要退回的步骤";
                return;
            }
            using (System.Transactions.TransactionScope scope = new System.Transactions.TransactionScope())
            {
                List<FlowTask> backTasks = new List<FlowTask>();
                int status = 0;
                switch (currentStep.BackPolicy.ToInt())
                {
                    case 1://不能退回
                        result.DebugMessage = "当前步骤设置为不能退回";
                        result.Success = false;
                        result.Message = "当前步骤设置为不能退回";
                        return;
                    #region 根据策略退回
                    case 0:
                        switch (currentStep.ProcessPolicy.ToInt())
                        {
                            case 0://所有人必须同意,如果一人不同意则全部退回
                                var taskList1 = taskService.GetSiblingTaskList(currentTask.Id);
                                foreach (var task in taskList1)
                                {
                                    if (task.Id != currentTask.Id)
                                    {
                                        if (task.Status.InArray(0, 1))
                                        {
                                            taskService.Completed(task.Id, string.Empty, null, 5);
                                        }
                                    }
                                    else
                                    {
                                        taskService.Completed(task.Id, executeModel.Opinion, executeModel.IsAudit, 3);
                                    }
                                }
                                break;
                            case 1://一人同意即可
                                var taskList = taskService.GetSiblingTaskList(currentTask.Id);
                                if (taskList.Count > 1)
                                {
                                    var noCompleted = taskList.Where(p => p.Status != 3);
                                    if (noCompleted.Count() - 1 > 0)
                                    {
                                        status = -1;
                                    }
                                }
                                taskService.Completed(currentTask.Id, executeModel.Opinion, executeModel.IsAudit, 3);
                                break;
                            case 2://依据人数比例
                                var taskList2 = taskService.GetSiblingTaskList(currentTask.Id);
                                if (taskList2.Count > 1)
                                {
                                    decimal percentage = currentStep.ProcessPercentage.ToDecimal() <= 0 ? 100 : currentStep.ProcessPercentage.ToDecimal();//比例
                                    if ((((decimal)(taskList2.Where(p => p.Status == 3).Count() + 1) / (decimal)taskList2.Count) * 100).Round() < percentage)
                                    {
                                        status = -1;
                                    }
                                    else
                                    {
                                        foreach (var task in taskList2)
                                        {
                                            if (task.Id != currentTask.Id && task.Status.InArray(0, 1))
                                            {
                                                taskService.Completed(task.Id, string.Empty, null, 5);
                                            }
                                        }
                                    }
                                }
                                taskService.Completed(currentTask.Id, executeModel.Opinion, executeModel.IsAudit, 3);
                                break;
                            case 3://独立处理
                                taskService.Completed(currentTask.Id, executeModel.Opinion, executeModel.IsAudit, 3);
                                break;
                        }
                        backTasks.Add(currentTask);
                        break;
                        #endregion
                }

                if (status == -1)
                {
                    result.DebugMessage += "已退回,等待他人处理";
                    result.Success = true;
                    result.Message += "已退回,等待他人处理!";
                    result.NextTasks = nextTasks;
                    scope.Complete();
                    return;
                }

                foreach (var backTask in backTasks)
                {
                    if (backTask.Status.InArray(2, 3))//已完成的任务不能退回
                    {
                        continue;
                    }
                    if (backTask.Id == currentTask.Id)
                    {
                        taskService.Completed(backTask.Id, executeModel.Opinion, executeModel.IsAudit, 3);
                    }
                    else
                    {
                        taskService.Completed(backTask.Id, string.Empty, null, 3, "他人已退回");
                    }
                }

                List<FlowTask> tasks = new List<FlowTask>();
                foreach (var step in executeModel.Steps)
                {
                    tasks.AddRange(taskService.GetTaskList(executeModel.FlowId, step.Key, executeModel.FlowInstanceId));
                }

                #region 处理会签形式的退回 
                //当前步骤是否是会签步骤
                var countersignatureStep = WorkFlowHelper.GetNextSteps(executeModel.FlowId, executeModel.StepId)
                    .FirstOrDefault(p => p.CountersignPolicy.ToInt() != 0);
                bool isCountersignature = countersignatureStep != null;
                bool isBack = true;
                if (isCountersignature)
                {
                    var steps = WorkFlowHelper.GetPrevSteps(executeModel.FlowId, countersignatureStep.Uid);
                    // 0:不会签
                    // 1:一个步骤同意即可
                    // 2:所有步骤同意
                    // 3:依据比例
                    switch (countersignatureStep.CountersignPolicy.ToInt())
                    {
                        case 1://一个步骤退回,如果有一个步骤同意，则不退回
                            isBack = true;
                            foreach (var step in steps)
                            {
                                if (!IsBack(step, executeModel.FlowId, currentTask.FlowInstanceId, currentTask.PrevId))
                                {
                                    isBack = false;
                                    break;
                                }
                            }
                            break;
                        case 2://所有步骤处理，如果一个步骤退回则退回
                            isBack = false;
                            foreach (var step in steps)
                            {
                                if (IsBack(step, executeModel.FlowId, currentTask.FlowInstanceId, currentTask.PrevId))
                                {
                                    isBack = true;
                                    break;
                                }
                            }
                            break;
                        case 3://依据比例退回
                            int backCount = 0;
                            foreach (var step in steps)
                            {
                                if (IsBack(step, executeModel.FlowId, currentTask.FlowInstanceId, currentTask.PrevId))
                                {
                                    backCount++;
                                }
                            }
                            isBack = (((decimal)backCount / (decimal)steps.Count) * 100).Round() >= (countersignatureStep.CountersignPercentage.ToDecimal() <= 0 ? 100 : countersignatureStep.CountersignPercentage.ToDecimal());
                            break;
                    }

                    if (isBack)
                    {
                        var tjTasks = taskService.GetSiblingTaskList(currentTask.Id, false);
                        foreach (var tjTask in tjTasks)
                        {
                            if (tjTask.Id == currentTask.Id || tjTask.Status.InArray(2, 3, 4, 5))
                            {
                                continue;
                            }
                            taskService.Completed(tjTask.Id, string.Empty, null, 5);
                        }
                    }
                }
                #endregion


                if (isBack)
                {
                    foreach (var task in tasks.Distinct(new FlowTaskReceiveIdComparer()))
                    {
                        if (task != null)
                        {
                            FlowTask newTask = task;

                            newTask.Id = StringHelper.GetGuid();
                            newTask.PrevId = currentTask.Id;
                            newTask.Note = "退回任务";
                            newTask.SenderId = currentTask.ReceiveId;
                            newTask.SenderName = currentTask.ReceiveName;
                            newTask.SenderDateTime = DateTime.Now;
                            newTask.SortIndex = currentTask.SortIndex + 1;
                            newTask.Status = 0;
                            newTask.Opinion = string.Empty;
                            newTask.ReadDateTime = null;
                            //newTask.PrevStepID = currentTask.StepID;

                            var processHour = currentStep.ProcessHour.ToDouble();
                            if (processHour > 0)
                            {
                                task.PlanFinishDateTime = DateTime.Now.AddHours(processHour);
                            }
                            else
                            {
                                task.PlanFinishDateTime = null;
                            }

                            newTask.ActualFinishDateTime = null;
                            taskService.Insert(newTask);
                            nextTasks.Add(newTask);


                            executeModel.FlowInstanceId = newTask.FlowInstanceId;
                            executeModel.TaskId = newTask.Id;
                        }
                    }
                }

                scope.Complete();
            }

            if (nextTasks.Count > 0)
            {
                List<string> nextStepName = new List<string>();
                foreach (var nstep in nextTasks)
                {
                    nextStepName.Add(nstep.StepName);
                }
                string msg = $"已退回到：{nextStepName.Distinct().ToList().ToSplitString()}";
                result.DebugMessage += msg;
                result.Success = true;
                result.Message += msg;
                result.NextTasks = nextTasks;
            }
            else
            {
                result.DebugMessage += "已退回,等待其它步骤处理";
                result.Success = true;
                result.Message += "已退回,等待其它步骤处理";
                result.NextTasks = nextTasks;
            }
        }

        private void executeSave(ExecuteData executeModel)
        {
            bool isFirst = IsFirstTask(executeModel);
            FlowTask currentTask = GetExecuteTask(executeModel);
            if (currentTask == null)
            {
                result.DebugMessage = "未能创建或找到当前任务";
                result.Success = false;
                result.Message = "未能创建或找到当前任务";
                return;
            }
            if (currentTask.Status.InArray(2, 3, 4))
            {
                result.DebugMessage = "当前任务已处理";
                result.Success = false;
                result.Message = "当前任务已处理";
                return;
            }

            currentTask.BusinessId = executeModel.BusinessId;
            nextTasks.Add(currentTask);
            if (!isFirst && !executeModel.Title.IsNullOrEmpty()) //已经存在记录且标题不为空
            {
                taskService.UpdateTaskTitle(executeModel.TaskId, executeModel.Title);
            }


            result.DebugMessage = "保存成功";
            result.Success = true;
            result.Message = "保存成功";
        }

        //private void executeComplete(ExecuteData executeModel, bool isCompleteTask = true)
        //{
        //    if (executeModel.TaskId.IsEmpty() || executeModel.FlowId.IsEmpty())
        //    {
        //        result.DebugMessage = "完成流程参数错误";
        //        result.Success = false;
        //        result.Message = "完成流程参数错误";
        //        return;
        //    }
        //    var task = taskService.Get(executeModel.TaskId);
        //    if (task == null)
        //    {
        //        result.DebugMessage = "未找到当前任务";
        //        result.Success = false;
        //        result.Message = "未找到当前任务";
        //        return;
        //    }
        //    if (isCompleteTask && task.Status.InArray(2, 3, 4))
        //    {
        //        result.DebugMessage = "当前任务已处理";
        //        result.Success = false;
        //        result.Message = "当前任务已处理";
        //        return;
        //    }
        //    if (isCompleteTask)
        //    {
        //        Completed(task.Id, executeModel.Comment, executeModel.IsSign);
        //    }

        //    result.DebugMessage += "已完成";
        //    result.Success = true;
        //    result.Message += "已完成";
        //}

        private void executeRedirect(ExecuteData executeModel)
        {
            FlowTask currentTask = GetExecuteTask(executeModel);
            if (currentTask == null)
            {
                result.DebugMessage = "未能创建或找到当前任务";
                result.Success = false;
                result.Message = "未能创建或找到当前任务";
                return;
            }
            if (currentTask.Status.InArray(2, 3, 4))
            {
                result.DebugMessage = "当前任务已处理";
                result.Success = false;
                result.Message = "当前任务已处理";
                return;
            }
            if (currentTask.Status == 5)
            {
                result.DebugMessage = "当前任务正在等待他人处理";
                result.Success = false;
                result.Message = "当前任务正在等待他人处理";
                return;
            }

            if (executeModel.Steps.First().Value.Count == 0)
            {
                result.DebugMessage = "未设置转交人员";
                result.Success = false;
                result.Message = "未设置转交人员";
                return;
            }
            if (executeModel.Steps.First().Value.Count > 1)
            {
                result.DebugMessage = "当前任务只能转交给一个人员";
                result.Success = false;
                result.Message = "当前任务只能转交给一个人员";
                return;
            }
            string receiveName = currentTask.ReceiveName;
            currentTask.ReceiveId = executeModel.Steps.First().Value.First().Id.ToString();
            currentTask.ReceiveName = executeModel.Steps.First().Value.First().Name;
            currentTask.ReadDateTime = null;
            currentTask.Status = 0;
            currentTask.Note = $"该任务由{receiveName}转交";
            taskService.Update(currentTask);
            nextTasks.Add(currentTask);
            result.DebugMessage = "转交成功";
            result.Success = true;
            result.Message = string.Concat("已转交给：", currentTask.ReceiveName);
        }

        /// <summary>
        /// 创建第一个任务
        /// </summary>
        /// <param name="executeModel"></param>
        /// <returns></returns>
        private FlowTask CreateFirstTask(ExecuteData executeModel)
        {
            var flowId = executeModel.FlowId;
            var firstStepId = WorkFlowHelper.GetFirstStepId(flowId);
            var firstStepName = WorkFlowHelper.GetStepName(flowId, firstStepId);
            var currentStepSetting = WorkFlowHelper.GetStepSetting(flowId, firstStepId);
            if (currentStepSetting == null)
            {
                return null;
            }
            FlowTask task = new FlowTask();
            task.Id = StringHelper.GetGuid();
            task.PrevId = null;
            task.PrevStepId = null;
            task.FlowInstanceId = StringHelper.GetGuid();
            task.FlowId = flowId;
            task.FlowName = WorkFlowHelper.GetFlowName(flowId);
            task.StepId = firstStepId;
            task.StepName = firstStepName;
            task.BusinessId = executeModel.BusinessId;
            task.Category = 0;
            task.Title = executeModel.Title.IsNullOrEmpty() ? "未命名任务" : executeModel.Title;
            task.SenderId = executeModel.SenderUser.Id.ToString();
            task.SenderName = executeModel.SenderUser.Name;
            task.SenderDateTime = DateTime.Now;
            task.ReceiveId = executeModel.SenderUser.Id.ToString();
            task.ReceiveName = executeModel.SenderUser.Name;
            task.Status = 0;
            task.SortIndex = 1;
            task.Note = executeModel.Note;
            var processHour = currentStepSetting.ProcessHour.ToDouble();
            if (processHour > 0)
            {
                task.PlanFinishDateTime = DateTime.Now.AddHours(processHour);
            }

            var taskAddResult = taskService.Insert(task);
            if (taskAddResult.Failure)
            {
                throw new ApplicationException(taskAddResult.Message);
            }

            executeModel.FlowInstanceId = task.FlowInstanceId;
            executeModel.TaskId = task.Id;


            var flowUser = new FlowUser();
            flowUser.FlowInstanceId = task.FlowInstanceId;
            flowUser.FlowId = task.FlowId;
            flowUser.BusinessId = task.BusinessId;
            flowUser.UserId = task.SenderId;
            flowUser.CreateDateTime = DateTime.Now;
            var _result = new FlowUserService().Save(flowUser);
            if (_result.Failure)
            {
                throw new ApplicationException("保存流程用户失败");
            }

            return task;
        }

        private FlowTask CreateNextTask(ExecuteData executeModel, FlowTask currentTask, SystemUser user, string nextStepId)
        {
            FlowTask task = new FlowTask();
            task.Id = StringHelper.GetGuid();
            task.PrevId = currentTask.Id;
            task.PrevStepId = currentTask.StepId;
            task.FlowInstanceId = currentTask.FlowInstanceId;
            task.FlowId = executeModel.FlowId;
            task.FlowName = WorkFlowHelper.GetFlowName(executeModel.FlowId);
            task.StepId = nextStepId;
            task.StepName = WorkFlowHelper.GetStepName(executeModel.FlowId, nextStepId);
            task.BusinessId = executeModel.BusinessId;
            task.Category = 0;
            task.Title = executeModel.Title.IsNullOrEmpty() ? currentTask.Title : executeModel.Title;
            task.SenderId = executeModel.SenderUser.Id.ToString();
            task.SenderName = executeModel.SenderUser.Name;
            task.SenderDateTime = DateTime.Now;
            task.ReceiveId = user.Id.ToString();
            task.ReceiveName = user.Name;
            task.Status = 0;
            task.SortIndex = currentTask.SortIndex + 1;
            task.Note = executeModel.Note;
            var nextStepSetting = WorkFlowHelper.GetStepSetting(executeModel.FlowId, nextStepId);
            var processHour = nextStepSetting.ProcessHour.ToDouble();
            if (processHour > 0)
            {
                task.PlanFinishDateTime = DateTime.Now.AddHours(processHour);
            }
            var taskAddResult = taskService.Insert(task);
            if (taskAddResult.Failure)
            {
                throw new ApplicationException(taskAddResult.Message);
            }

            taskService.Insert(task);

            executeModel.FlowInstanceId = task.FlowInstanceId;
            executeModel.TaskId = task.Id;


            var flowUser = new FlowUser();
            flowUser.FlowInstanceId = task.FlowInstanceId;
            flowUser.FlowId = task.FlowId;
            flowUser.BusinessId = task.BusinessId;
            flowUser.UserId = task.ReceiveId;
            flowUser.CreateDateTime = DateTime.Now;
            var _result = new FlowUserService().Save(flowUser);
            if(_result.Failure)
            {
                throw new ApplicationException("保存流程用户失败");
            }

            return task;
        }

        private bool IsFirstTask(ExecuteData executeModel)
        {
            bool isFirst = executeModel.StepId == WorkFlowHelper.GetFirstStepId(executeModel.FlowId)
                && string.IsNullOrEmpty(executeModel.TaskId)
                && string.IsNullOrEmpty(executeModel.FlowInstanceId);
            return isFirst;
        }

        private FlowTask GetExecuteTask(ExecuteData executeModel)
        {
            //如果是第一步提交并且没有实例则先创建实例
            bool isFirst = IsFirstTask(executeModel);

            FlowTask currentTask = isFirst ? CreateFirstTask(executeModel) : taskService.Get(executeModel.TaskId);
            return currentTask;
        }

        /// <summary>
        /// 判断一个步骤是否通过
        /// </summary>
        private bool IsPassing(FlowStepSetting step, string flowId, string flowInstanceId, string taskId)
        {
            var tasks = taskService.GetTaskList(flowId, step.Uid, flowInstanceId).FindAll(p => p.PrevId == taskId);
            if (tasks.Count == 0)
            {
                return false;
            }
            bool isPassing = true;
            // 0:一人同意即可
            // 1:所有人必须同意
            // 2:独立处理
            // 3:依据比例
            switch (step.ProcessPolicy.ToInt())
            {
                case 1://所有人必须处理
                case 2://独立处理
                    isPassing = tasks.Where(p => p.Status != 2).Count() == 0;
                    break;
                case 0://一人同意即可
                    isPassing = tasks.Where(p => p.Status == 2).Count() > 0;
                    break;
                case 3://依据人数比例
                    isPassing = (((decimal)(tasks.Where(p => p.Status == 2).Count() + 1) / (decimal)tasks.Count) * 100).Round() >= (step.ProcessPercentage.ToDecimal() <= 0 ? 100 : step.ProcessPercentage.ToDecimal());
                    break;
            }
            return isPassing;
        }

        /// <summary>
        /// 判断一个步骤是否退回
        /// </summary>
        private bool IsBack(FlowStepSetting step, string flowId, string flowInstanceId, string taskId)
        {
            var tasks = taskService.GetTaskList(flowId, step.Uid, flowInstanceId).FindAll(p => p.PrevId == taskId);
            if (tasks.Count == 0)
            {
                return false;
            }
            bool isBack = true;
            // 0:一人同意即可
            // 1:所有人必须同意
            // 2:独立处理
            // 3:依据比例
            switch (step.ProcessPolicy.ToInt())
            {
                case 1://所有人必须处理
                case 2://独立处理
                    isBack = tasks.Where(p => p.Status.InArray(3, 5)).Count() > 0;
                    break;
                case 0://一人同意即可
                    isBack = tasks.Where(p => p.Status.InArray(2, 4)).Count() == 0;
                    break;
                case 3://依据人数比例
                    isBack = (((decimal)(tasks.Where(p => p.Status.InArray(3, 5)).Count() + 1) / (decimal)tasks.Count) * 100).Round() >= (step.ProcessPercentage.ToDecimal() <= 0 ? 100 : step.ProcessPercentage.ToDecimal());
                    break;
            }
            return isBack;
        }

    }
}