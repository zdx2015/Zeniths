using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Configuration;
using Zeniths.Entity;
using Zeniths.Extensions;
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
            var service = new FlowService();
            var json = service.GetFlowJson(flowId);
            if (json.IsEmpty())
            {
                throw new ApplicationException("请先配置流程");
            }
            return JsonHelper.Deserialize<WorkFlowDesign>(json);
        }

        private static void CheckWorkFlowDesign(WorkFlowDesign design)
        {
            if (design == null)
            {
                throw new ApplicationException("无法获取流程设计对象");
            }
        }

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

        //private static FlowStepSetting GetStepSetting(string flowId, string stepId)
        //{
        //    var design = GetWorkFlowDesign(flowId);
        //    var step = design.Flow.Steps[stepId];
        //    var uid = step.Uid;
        //    design.Step.w
        //}

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

        ///// <summary>
        ///// 获取指定步骤的前面所有步骤集合
        ///// </summary>
        ///// <param name="flowId">流程主键</param>
        ///// <param name="stepId">步骤主键</param>
        ///// <returns></returns>
        //public static List<FlowStepSetting> GetAllPrevSteps(string flowId, string stepId)
        //{

        //}

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
    }
}