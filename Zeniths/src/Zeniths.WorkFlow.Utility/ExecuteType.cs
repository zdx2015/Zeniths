namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 流程处理类型
    /// </summary>
    public enum ExecuteType
    {
        /// <summary>
        /// 提交
        /// </summary>
        Submit,

        /// <summary>
        /// 保存
        /// </summary>
        Save,

        /// <summary>
        /// 退回
        /// </summary>
        Back,

        /// <summary>
        /// 完成
        /// </summary>
        Completed,

        /// <summary>
        /// 转交
        /// </summary>
        Redirect
    }
}