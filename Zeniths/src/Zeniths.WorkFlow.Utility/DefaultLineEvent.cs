using Zeniths.Utility;

namespace Zeniths.WorkFlow.Utility
{
    public class DefaultLineEvent: ILineEvent
    {
        #region Implementation of ILineEvent

        /// <summary>
        /// 线验证事件
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>验证成功返回BoolMessage.True</returns>
        public virtual BoolMessage OnValid(FlowLineEventArgs args)
        {
            return BoolMessage.False;
        }

        #endregion
    }
}