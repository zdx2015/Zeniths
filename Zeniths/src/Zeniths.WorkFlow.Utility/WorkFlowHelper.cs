using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Zeniths.Auth.Service;
using Zeniths.Auth.Utility;
using Zeniths.Configuration;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;
using Zeniths.WorkFlow.Service;

namespace Zeniths.WorkFlow.Utility
{
    public static class WorkFlowHelper
    {
        /// <summary>
        /// 流程设计对象缓存
        /// </summary>
        private static Cache<string, WorkFlowDesign> WorkFlowDesignCache = new Cache<string, WorkFlowDesign>();

        /// <summary>
        /// 获取流程设计对象
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <returns></returns>
        public static WorkFlowDesign GetWorkFlowDesign(string flowId)
        {
            return WorkFlowDesignCache.Get(flowId, () => GetWorkFlowDesignCore(flowId));
        }

        /// <summary>
        /// 刷新流程设计对象缓存
        /// </summary>
        /// <param name="flowId">流程主键</param>
        public static void RefreshWorkFlowDesignCache(string flowId)
        {
            WorkFlowDesignCache.Set(flowId, GetWorkFlowDesignCore(flowId));
        }

        private static WorkFlowDesign GetWorkFlowDesignCore(string flowId)
        {
            flowId = "0556f6ca-c0cb-4e83-8572-d2ac53d62ed1";
            var service = new FlowService();
            var json = service.GetFlowJson(flowId);
            if (json.IsEmpty())
            {
                throw new ApplicationException("请先配置流程");
            }
            return JsonHelper.Deserialize<WorkFlowDesign>(json);
        }

        //private static void CheckWorkFlowDesign(WorkFlowDesign design)
        //{
        //    if (design == null)
        //    {
        //        throw new ApplicationException("无法获取流程设计对象");
        //    }
        //}

        private static string GetStepId(string flowId, string stepUId)
        {
            var design = GetWorkFlowDesign(flowId);
            var stepDic = design?.Flow.Steps;
            if (stepDic != null)
            {
                foreach (var item in stepDic)
                {
                    if (item.Value.Uid.Equals(stepUId))
                    {
                        return item.Key;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取步骤设置
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <returns></returns>
        public static FlowStepSetting GetStepSetting(string flowId, string stepId)
        {
            var design = GetWorkFlowDesign(flowId);
            var _stepId = GetStepId(flowId, stepId);
            return design.Step[_stepId];
        }

        /// <summary>
        /// 获取连线设置
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="fromStepId">开始步骤主键</param>
        /// <param name="toStepId">结束步骤主键</param>
        /// <returns></returns>
        public static FlowLineSetting GetLineSetting(string flowId, string fromStepId, string toStepId)
        {
            var design = GetWorkFlowDesign(flowId);
            var _fromStepId = GetStepId(flowId, fromStepId);
            var _toStepId = GetStepId(flowId, toStepId);
            var lineId = string.Empty;
            foreach (var item in design.Flow.Lines)
            {
                if (item.Value.From.Equals(_fromStepId) && item.Value.To.Equals(_toStepId))
                {
                    lineId = item.Key;
                    break;
                }
            }
            if (design.Line.ContainsKey(lineId))
            {
                return design.Line[lineId];
            }
            return new FlowLineSetting();
        }

        /// <summary>
        /// 获取流程名称
        /// </summary>
        /// <param name="flowId">流程主键</param>
        public static string GetFlowName(string flowId)
        {
            var design = GetWorkFlowDesign(flowId);
            return design != null ? design.Property.Name : string.Empty;
        }

        /// <summary>
        /// 获取步骤名称
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <returns></returns>
        public static string GetStepName(string flowId, string stepId)
        {
            var design = GetWorkFlowDesign(flowId);
            var _stepId = GetStepId(flowId, stepId);
            return design?.Flow.Steps[_stepId]?.Name;
        }

        /// <summary>
        /// 获取指定步骤的前面所有步骤集合
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <returns></returns>
        public static List<FlowStepSetting> GetAllPrevSteps(string flowId, string stepId)
        {
            List<FlowStepSetting> list = new List<FlowStepSetting>();
            var design = GetWorkFlowDesign(flowId);
            if (design == null)
            {
                return list;
            }
            List<string> nodeList = new List<string>();
            var startNodeId = GetStepId(flowId, stepId);
            AddPrevNode(design, startNodeId, nodeList);
            foreach (var item in nodeList)
            {
                if (design.Step.ContainsKey(item))
                {
                    list.Add(design.Step[item]);
                }
            }
            return list;
        }

        private static void AddPrevNode(WorkFlowDesign design, string toNodeId, List<string> list)
        {
            var fromNodeId = GetFromNodeId(design, toNodeId);//返回到起始节点
            if (fromNodeId.Count == 0) return;
            foreach (var item in fromNodeId) //把当前层的步骤一次性填写
            {
                if (!list.Any(p => p.Equals(item)))
                {
                    list.Add(item);
                }
            }
            foreach (var item in fromNodeId) //找上级
            {
                AddPrevNode(design, item, list);
            }
        }

        /// <summary>
        /// 获取流程第一步步骤主键
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <returns></returns>
        public static string GetFirstStepId(string flowId)
        {
            var design = GetWorkFlowDesign(flowId);
            var startNodeId = GetStartNodeId(design);
            var toList = GetToNodeId(design, startNodeId);
            if (toList.Count > 0)
            {
                return design.Flow.Steps[toList[0]].Uid;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取流程最后一步步骤主键
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <returns></returns>
        public static string GetLastStepId(string flowId)
        {
            var design = GetWorkFlowDesign(flowId);
            var endNodeId = GetEndNodeId(design);
            var fromList = GetFromNodeId(design, endNodeId);
            if (fromList.Count > 0)
            {
                return design.Flow.Steps[fromList[0]].Uid;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取所有步骤列表
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <returns></returns>
        public static List<FlowStepSetting> GetAllSteps(string flowId)
        {
            List<FlowStepSetting> list = new List<FlowStepSetting>();
            var design = GetWorkFlowDesign(flowId);
            if (design == null)
            {
                return list;
            }
            List<string> nodeList = new List<string>();
            var startNodeId = GetStartNodeId(design);
            AddNode(design, startNodeId, nodeList);
            foreach (var item in nodeList)
            {
                if (design.Step.ContainsKey(item))
                {
                    list.Add(design.Step[item]);
                }
            }
            return list;
        }

        private static void AddNode(WorkFlowDesign design, string fromNodeId, List<string> list)
        {
            var toNodeIdList = GetToNodeId(design, fromNodeId);//返回到达节点
            if (toNodeIdList.Count == 0) return;
            foreach (var item in toNodeIdList) //把当前层的步骤一次性填写
            {
                if (!list.Any(p => p.Equals(item)))
                {
                    list.Add(item);
                }
            }
            foreach (var item in toNodeIdList) //找下级
            {
                AddNode(design, item, list);
            }
        }

        private static string GetStartNodeId(WorkFlowDesign design)
        {
            foreach (var item in design.Flow.Steps)
            {
                if (item.Value.Type.Equals("startround"))
                {
                    return item.Key;
                }
            }
            return string.Empty;
        }

        private static string GetEndNodeId(WorkFlowDesign design)
        {
            foreach (var item in design.Flow.Steps)
            {
                if (item.Value.Type.Equals("endround"))
                {
                    return item.Key;
                }
            }
            return string.Empty;
        }

        private static List<string> GetFromNodeId(WorkFlowDesign design, string toNodeId)
        {
            var nodes = new List<string>();
            foreach (var item in design.Flow.Lines)
            {
                if (item.Value.To.Equals(toNodeId)) //终节点
                {
                    nodes.Add(item.Value.From);//添加起始的节点
                }
            }
            return nodes;
        }

        private static List<string> GetToNodeId(WorkFlowDesign design, string fromNodeId)
        {
            var nodes = new List<string>();
            foreach (var item in design.Flow.Lines)
            {
                if (item.Value.From.Equals(fromNodeId)) //起始节点
                {
                    nodes.Add(item.Value.To);//添加到达的节点
                }
            }
            return nodes;
        }

        /// <summary>
        /// 获取指定步骤的前面步骤集合
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <returns></returns>
        public static List<FlowStepSetting> GetPrevSteps(string flowId, string stepId)
        {
            List<FlowStepSetting> steps = new List<FlowStepSetting>();
            var design = GetWorkFlowDesign(flowId);
            if (design == null)
            {
                return steps;
            }
            var _stepId = GetStepId(flowId, stepId);
            var lines = design.Flow.Lines.Where(p => p.Value.To.Equals(_stepId));
            steps.AddRange(lines.Select(line => design.Step[line.Value.From]));
            return steps;
        }

        /// <summary>
        /// 获取指定步骤的后续步骤集合
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <returns></returns>
        public static List<FlowStepSetting> GetNextSteps(string flowId, string stepId)
        {
            List<FlowStepSetting> steps = new List<FlowStepSetting>();
            var design = GetWorkFlowDesign(flowId);
            if (design == null)
            {
                return steps;
            }
            var _stepId = GetStepId(flowId, stepId);
            var lines = design.Flow.Lines.Where(p => p.Value.From.Equals(_stepId));
            steps.AddRange(lines.Select(line => design.Step[line.Value.To]));
            return steps;
        }

        /// <summary>
        /// 获取指定步骤的流程执行的后续步骤集合
        /// </summary>
        /// <param name="flowId">流程主键</param>
        /// <param name="stepId">步骤主键</param>
        /// <param name="queryString">表单QueryString数据</param>
        /// <param name="form">表单Form数据</param>
        /// <returns></returns>
        public static List<FlowStepSetting> GetExecuteNextSteps(string flowId, string stepId,
            NameValueCollection queryString, NameValueCollection form)
        {
            var currentStep = WorkFlowHelper.GetStepSetting(flowId, stepId);
            var nextSteps = GetNextSteps(flowId, stepId);
            if (currentStep.FlowCategory.ToInt() != 0)
            {
                return nextSteps;
            }
            var userService = new SystemUserService();
            var taskService = new FlowTaskService();
            var firstStepId = WorkFlowHelper.GetFirstStepId(flowId);
            List<string> removeIds = new List<string>();
            var eventArgs = new FlowLineEventArgs();
            eventArgs.FlowId = flowId;
            eventArgs.StepId = stepId;
            eventArgs.FlowInstanceId = form["FlowInstanceId"];
            eventArgs.TaskId = form["TaskId"];
            eventArgs.BusinessId = form["BusinessId"];

            eventArgs.QueryString = queryString;
            eventArgs.Form = form;

            foreach (var step in nextSteps)
            {
                var line = GetLineSetting(flowId, stepId, step.Uid);
                eventArgs.LineSetting = line;
                eventArgs.StepSetting = step;
                ILineEvent lineEvent = ReflectionHelper.CreateInstance<ILineEvent>(line.ValidProvider);
                if (lineEvent != null)
                {
                    var result = lineEvent.OnValid(eventArgs);
                    if (result.Failure)
                    {
                        removeIds.Add(step.Uid);
                    }
                }

                #region 组织机构关系判断
                var senderId = OrganizeHelper.GetCurrentUser().Id;
                if (line.OrganizeSenderchargeleader && !userService.IsChargeLeader(senderId))
                {
                    removeIds.Add(step.Uid);
                }
                if (!line.OrganizeSenderin.IsNullOrEmpty() && !OrganizeHelper.IsContainsUser(senderId, line.OrganizeSenderin))
                {
                    removeIds.Add(step.Uid);
                }
                if (line.OrganizeSenderleader && !userService.IsMainLeader(senderId))
                {
                    removeIds.Add(step.Uid);
                }
                if (!line.OrganizeSendernotin.IsNullOrEmpty() && OrganizeHelper.IsContainsUser(senderId, line.OrganizeSendernotin))
                {
                    removeIds.Add(step.Uid);
                }
                //发起者Id
                int sponserID = currentStep.Uid == firstStepId ? //如果是第一步则发起者就是发送者
                    senderId :
                    taskService.GetFirstSenderId(eventArgs.FlowId, eventArgs.FlowInstanceId).ToInt();

                if (line.OrganizeSponsorchargeleader && !userService.IsChargeLeader(sponserID))
                {
                    removeIds.Add(step.Uid);
                }
                if (!line.OrganizeSponsorin.IsNullOrEmpty() && !OrganizeHelper.IsContainsUser(sponserID, line.OrganizeSponsorin))
                {
                    removeIds.Add(step.Uid);
                }
                if (line.OrganizeSponsorleader && !userService.IsMainLeader(sponserID))
                {
                    removeIds.Add(step.Uid);
                }
                if (!line.OrganizeSponsornotin.IsNullOrEmpty() && OrganizeHelper.IsContainsUser(sponserID, line.OrganizeSponsornotin))
                {
                    removeIds.Add(step.Uid);
                }
                if (line.OrganizeNotsenderchargeleader && userService.IsChargeLeader(senderId))
                {
                    removeIds.Add(step.Uid);
                }
                if (line.OrganizeNotsenderleader && userService.IsMainLeader(senderId))
                {
                    removeIds.Add(step.Uid);
                }
                if (line.OrganizeNotsponsorchargeleader && userService.IsChargeLeader(sponserID))
                {
                    removeIds.Add(step.Uid);
                }
                if (line.OrganizeNotsponsorleader && userService.IsMainLeader(sponserID))
                {
                    removeIds.Add(step.Uid);
                }
                #endregion

            }
            foreach (string rid in removeIds)
            {
                nextSteps.RemoveAll(p => p.Uid == rid);
            }
            return nextSteps;
        }

        /// <summary>
        /// 获取任务状态名称
        /// </summary>
        /// <param name="status">任务状态</param>
        /// <returns></returns>
        public static string GetTaskStatusName(int status)
        {
            string title = string.Empty;
            switch (status)
            {
                case 0:
                    title = "待处理";
                    break;
                case 1:
                    title = "已打开";
                    break;
                case 2:
                    title = "已完成";
                    break;
                case 3:
                    title = "已退回";
                    break;
                case 4:
                    title = "他人已处理";
                    break;
                case 5:
                    title = "他人已退回";
                    break;
                default:
                    title = "其它";
                    break;
            }

            return title;
        }

        /// <summary>
        /// 得到一个任务可以退回的步骤字典(key:步骤id;value:步骤名称)
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetBackSteps(string taskId)
        {
            var taskService = new FlowTaskService();
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var task = taskService.Get(taskId);
            var step = GetStepSetting(task.FlowId, task.StepId);
            if (step == null)
            {
                return dict;
            }
            int backType = step.BackCategory.ToInt();//退回类型
            switch (backType)
            {
                case 0://退回前一步

                    if (step.CountersignPolicy.ToInt() != 0)//如果是会签步骤，则要退回到前面所有步骤
                    {
                        var backSteps = GetPrevSteps(task.FlowId, task.StepId);
                        foreach (var backStep in backSteps)
                        {
                            dict.Add(backStep.Uid, backStep.Name);
                        }
                    }
                    else
                    {
                        dict.Add(task.PrevStepId, GetStepName(task.FlowId, task.PrevStepId));
                    }

                    break;
                case 1://退回第一步
                    var firstStepId = GetFirstStepId(task.FlowId);
                    dict.Add(firstStepId, GetStepName(task.FlowId, firstStepId));
                    break;
                case 2://退回某一步
                    if (backType == 2 && step.BackStep.IsNotEmpty())
                    {
                        dict.Add(step.BackStep, GetStepName(task.FlowId, step.BackStep));
                    }
                    else
                    {
                        var taskList = taskService.GetTaskList(task.FlowId, task.FlowInstanceId).Where(p => p.Status.InArray(2, 3, 4)).OrderBy(p => p.SortIndex);
                        foreach (var task1 in taskList)
                        {
                            if (!dict.Keys.Contains(task1.StepId) && task1.StepId != task.StepId)
                            {
                                dict.Add(task1.StepId, GetStepName(task.FlowId, task.StepId));
                            }
                        }
                    }
                    break;
            }
            return dict;
        }

        /// <summary>
        /// 设置对象的当前流程步骤信息
        /// </summary>
        /// <param name="entity">待修改的对象</param>
        /// <param name="args">流程事件参数</param>
        public static void SetCurrentFlowInfo(dynamic entity, FlowEventArgs args)
        {
            var type = entity?.GetType();

            type?.GetProperty("Title")?.SetValue(entity, args.ExecuteData.Title);
            type?.GetProperty("FlowId")?.SetValue(entity, args.FlowId);
            type?.GetProperty("FlowName")?.SetValue(entity, GetFlowName(args.FlowId));
            type?.GetProperty("FlowInstanceId")?.SetValue(entity, args.FlowInstanceId);
            type?.GetProperty("StepId")?.SetValue(entity, args.StepId);
            type?.GetProperty("StepName")?.SetValue(entity, args.StepSetting.Name);
        }

        /// <summary>
        /// 获取步骤事件描述属性对象列表
        /// </summary>
        /// <returns></returns>
        public static List<WorkFlowEventCaptionAttribute> GetStepEventCaptionList()
        {
            return GetEventCaptionList<IStepEvent>();
        }

        /// <summary>
        /// 获取线事件描述属性对象列表
        /// </summary>
        /// <returns></returns>
        public static List<WorkFlowEventCaptionAttribute> GetLineEventCaptionList()
        {
            return GetEventCaptionList<ILineEvent>();
        }

        /// <summary>
        /// 获取事件描述属性对象列表
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <returns></returns>
        private static List<WorkFlowEventCaptionAttribute> GetEventCaptionList<T>()
        {
            var types = AssemblyHelper.GetInterfaceSubClass(TypeHelper.GetApplicateTypes(), false, typeof(T));
            var atts = new List<WorkFlowEventCaptionAttribute>();
            foreach (var item in types)
            {
                var att = AssemblyHelper.GetAttribute<WorkFlowEventCaptionAttribute>(item) ??
                          new WorkFlowEventCaptionAttribute(item.Name);
                att.InstanceType = item;
                atts.Add(att);
            }
            return atts;
        }
    }
}