using System;
using System.Collections.Generic;
using Zeniths.Auth.Entity;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 任务处理模型
    /// </summary>
    [Serializable]
    public class Execute
    { 
        /// <summary>
        /// 流程Id
        /// </summary>
        public string FlowId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public string StepId { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 业务Id
        /// </summary>
        public string BusinessId { get; set; }

        /// <summary>
        /// 流程实例Id
        /// </summary>
        public string FlowInstanceId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public ExecuteType ExecuteType { get; set; }

        /// <summary>
        /// 发送人员
        /// </summary>
        public SystemUser SenderUser { get; set; }

        /// <summary>
        /// 接收的步骤和人员
        /// </summary>
        public Dictionary<string, List<SystemUser>> Steps { get; set; } = new Dictionary<string, List<SystemUser>>();

        /// <summary>
        /// 处理意见
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}