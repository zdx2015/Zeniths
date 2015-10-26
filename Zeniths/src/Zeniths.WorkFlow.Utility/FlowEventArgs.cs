using System.Collections.Specialized;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 流程事件参数
    /// </summary>
    public class FlowEventArgs
    {
        /// <summary>
        /// 流程主键
        /// </summary>
        public string FlowId { get; set; }

        /// <summary>
        /// 步骤主键
        /// </summary>
        public string StepId { get; set; }

        /// <summary>
        /// 流程实例主键
        /// </summary>
        public string FlowInstanceId { get; set; }

        /// <summary>
        /// 任务主键
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 业务主键
        /// </summary>
        public string BusinessId { get; set; }

        /// <summary>
        /// 当前步骤配置信息
        /// </summary>
        public FlowStepSetting StepSetting { get; set; }

        /// <summary>
        /// 表单QueryString数据
        /// </summary>
        public NameValueCollection QueryString { get; set; }

        /// <summary>
        /// 表单Form数据
        /// </summary>
        public NameValueCollection Form { get; set; }
    }
}