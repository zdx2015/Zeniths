using Zeniths.Utility;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 步骤流转事件
    /// </summary>
    public interface ILineEvent
    {
        /// <summary>
        /// 线验证事件
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>验证成功返回BoolMessage.True</returns>
        BoolMessage OnValid(FlowEventArgs args);
    }
}