using Zeniths.Utility;

namespace Zeniths.WorkFlow.Utility
{
    public class DefaultStepEvent : IStepEvent
    {
        #region Implementation of IStepEvent

        /// <summary>
        /// 保存业务数据事件,数据保存成功后,需要把新建的业务记录主键赋值给args.BusinessId
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public virtual BoolMessage OnSaveData(FlowEventArgs args)
        {
            return BoolMessage.True;
        }

        /// <summary>
        /// 流程保存之前
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public virtual BoolMessage OnBeforeSave(FlowEventArgs args)
        {
            return BoolMessage.True;
        }

        /// <summary>
        /// 流程提交之前
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public virtual BoolMessage OnBeforeSubmit(FlowEventArgs args)
        {
            return BoolMessage.True;
        }

        /// <summary>
        /// 流程退回之前
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public virtual BoolMessage OnBeforeBack(FlowEventArgs args)
        {
            return BoolMessage.True;
        }

        /// <summary>
        /// 流程保存之后
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public virtual BoolMessage OnAfterSave(FlowEventArgs args)
        {
            return BoolMessage.True;
        }

        /// <summary>
        /// 流程提交之后
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public virtual BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            return BoolMessage.True;
        }

        /// <summary>
        /// 流程退回之后
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public virtual BoolMessage OnAfterBack(FlowEventArgs args)
        {
            return BoolMessage.True;
        }

        #endregion
    }
}