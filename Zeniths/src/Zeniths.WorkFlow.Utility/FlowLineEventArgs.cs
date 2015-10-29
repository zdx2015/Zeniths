namespace Zeniths.WorkFlow.Utility
{
    public class FlowLineEventArgs: FlowEventArgs
    {
        /// <summary>
        /// 当前Line配置信息
        /// </summary>
        public FlowLineSetting LineSetting { get; set; }
    }
}