using System;
using System.Collections.Generic;
using Zeniths.WorkFlow.Entity;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 任务处理结果
    /// </summary>
    [Serializable]
    public class Result
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Messages { get; set; }

        /// <summary>
        /// 调试信息
        /// </summary>
        public string DebugMessages { get; set; }

        /// <summary>
        /// 其它信息
        /// </summary>
        public object[] Other { get; set; }

        /// <summary>
        /// 后续任务
        /// </summary>
        public IEnumerable<FlowTask> NextTasks { get; set; }
    }
}