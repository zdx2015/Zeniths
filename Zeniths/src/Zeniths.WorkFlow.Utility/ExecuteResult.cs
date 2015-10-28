using System;
using System.Collections.Generic;
using Zeniths.WorkFlow.Entity;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 任务处理结果
    /// </summary>
    [Serializable]
    public class ExecuteResult
    {
        public ExecuteResult()
        {
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        public ExecuteResult(bool success)
        {
            Success = success;
        }

        /// <summary>
        /// 初始化 <see cref="T:System.Object"/> 类的新实例。
        /// </summary>
        public ExecuteResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 调试信息
        /// </summary>
        public string DebugMessage { get; set; }

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