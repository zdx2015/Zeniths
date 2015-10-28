using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 执行参数
    /// </summary>
    public class ExecuteParam
    {
        /// <summary>
        /// 流程主键
        /// </summary>
        [JsonProperty("flowId")]
        public string FlowId { get; set; }

        /// <summary>
        /// 步骤主键
        /// </summary>
        [JsonProperty("stepId")]
        public string StepId { get; set; }

        /// <summary>
        /// 任务主键
        /// </summary>
        [JsonProperty("taskId")]
        public string TaskId { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        [JsonProperty("businessId")]
        public string BusinessId { get; set; }

        /// <summary>
        /// 流程实例主键
        /// </summary>
        [JsonProperty("flowInstanceId")]
        public string FlowInstanceId { get; set; }

        /// <summary>
        /// 任务标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        [JsonProperty("opinion")]
        public string Opinion { get; set; }

        /// <summary>
        /// 是否审核通过
        /// </summary>
        [JsonProperty("isAudit")]
        public bool? IsAudit { get; set; }

        /// <summary>
        /// 执行类型
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 提交步骤
        /// </summary>
        [JsonProperty("steps")]
        public List<ExecuteParamStep> Steps { get; set; } = new List<ExecuteParamStep>();
    }

    /// <summary>
    /// 执行参数步骤
    /// </summary>
    public class ExecuteParamStep
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 成员字符串,逗号隔开
        /// </summary>
        [JsonProperty("member")]
        public string Member { get; set; }
    }
}