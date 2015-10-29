using System.Collections.Generic;
using Zeniths.WorkFlow.Entity;

namespace Zeniths.WorkFlow.Utility
{
    public class FlowTaskReceiveIdComparer : IEqualityComparer<FlowTask>
    {
        /// <summary>
        /// 去除重复的接收人，在退回任务时去重，避免一个人收到多条任务。
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns></returns>
        public bool Equals(FlowTask task1, FlowTask task2)
        {
            return task1.ReceiveId == task2.ReceiveId;
        }

        public int GetHashCode(FlowTask task)
        {
            return task.ToString().GetHashCode();
        }
    }
}