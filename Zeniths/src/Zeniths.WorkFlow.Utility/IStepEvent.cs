using Zeniths.Utility;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 步骤事件接口
    /// </summary>
    public interface IStepEvent
    {
        /// <summary>
        /// 保存业务数据事件,数据保存成功后,需要把新建的业务记录主键赋值给args.BusinessId
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        BoolMessage OnSaveData(FlowEventArgs args);

        /// <summary>
        /// 流程保存之前
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        BoolMessage OnBeforeSave(FlowEventArgs args);

        /// <summary>
        /// 流程提交之前
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        BoolMessage OnBeforeSubmit(FlowEventArgs args);

        /// <summary>
        /// 流程退回之前
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        BoolMessage OnBeforeBack(FlowEventArgs args);

        /// <summary>
        /// 流程保存之后
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        BoolMessage OnAfterSave(FlowEventArgs args);

        /// <summary>
        /// 流程提交之后
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        BoolMessage OnAfterSubmit(FlowEventArgs args);

        /// <summary>
        /// 流程退回之后
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        BoolMessage OnAfterBack(FlowEventArgs args);
    }
}