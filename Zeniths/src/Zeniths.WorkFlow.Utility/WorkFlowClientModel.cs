using Newtonsoft.Json;

namespace Zeniths.WorkFlow.Utility
{
    public class WorkFlowClientModel
    {
        /// <summary>
        /// 是否是第一步
        /// </summary>
        [JsonProperty("isFirstStep")]
        public bool IsFirstStep { get; set; }

        /// <summary>
        /// 是否是最后一步
        /// </summary>
        [JsonProperty("isLastStep")]
        public bool IsLastStep { get; set; }

        /// <summary>
        /// 是否需要意见
        /// </summary>
        [JsonProperty("isNeedOpinion")]
        public bool IsNeedOpinion { get; set; }

        /// <summary>
        /// 是否需要签章
        /// </summary>
        [JsonProperty("isNeedSignature")]
        public bool IsNeedSignature { get; set; }

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
        /// 流程实例主键
        /// </summary>
        [JsonProperty("flowInstanceId")]
        public string FlowInstanceId { get; set; }

        /// <summary>
        /// 业务记录主键
        /// </summary>
        [JsonProperty("businessId")]
        public string BusinessId { get; set; }
        
    }
}