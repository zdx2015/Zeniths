// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.WorkFlow.Entity
{
    /// <summary>
    /// 流程任务
    /// </summary>
    [Table(Caption = "流程任务")]
    [PrimaryKey("Id")]
    public class FlowTask
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public string Id { get; set; }

        /// <summary>
        /// 上一任务Id
        /// </summary>
        [Column(Caption = "上一任务Id")]
        public string PrevId { get; set; }

        /// <summary>
        /// 上一步骤Id
        /// </summary>
        [Column(Caption = "上一步骤Id")]
        public string PrevStepId { get; set; }

        /// <summary>
        /// 流程实例Id
        /// </summary>
        [Column(Caption = "流程实例Id")]
        public string FlowInstanceId { get; set; }

        /// <summary>
        /// 流程Id
        /// </summary>
        [Column(Caption = "流程Id")]
        public string FlowId { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [Column(Caption = "流程名称")]
        public string FlowName { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        [Column(Caption = "步骤Id")]
        public string StepId { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [Column(Caption = "步骤名称")]
        public string StepName { get; set; }

        /// <summary>
        /// 业务Id
        /// </summary>
        [Column(Caption = "业务Id")]
        public string BusinessId { get; set; }

        /// <summary>
        /// 任务类型0正常1指派2委托
        /// </summary>
        [Column(Caption = "任务类型0正常1指派2委托")]
        public int Category { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [Column(Caption = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 发送人Id
        /// </summary>
        [Column(Caption = "发送人Id")]
        public string SenderId { get; set; }

        /// <summary>
        /// 发送人姓名
        /// </summary>
        [Column(Caption = "发送人姓名")]
        public string SenderName { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [Column(Caption = "发送时间")]
        public DateTime SenderDateTime { get; set; }

        /// <summary>
        /// 接收人Id
        /// </summary>
        [Column(Caption = "接收人Id")]
        public string ReceiveId { get; set; }

        /// <summary>
        /// 接收人姓名
        /// </summary>
        [Column(Caption = "接收人姓名")]
        public string ReceiveName { get; set; }

        /// <summary>
        /// 读取时间
        /// </summary>
        [Column(Caption = "读取时间")]
        public DateTime? ReadDateTime { get; set; }

        /// <summary>
        /// 计划完成时间
        /// </summary>
        [Column(Caption = "计划完成时间")]
        public DateTime? PlanFinishDateTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        [Column(Caption = "实际完成时间")]
        public DateTime? ActualFinishDateTime { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        [Column(Caption = "意见")]
        public string Opinion { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Column(Caption = "审核状态")]
        public bool? IsAudit { get; set; }

        /// <summary>
        /// 状态0待处理1打开2完成3退回4他人已处理5他人已退回
        /// </summary>
        [Column(Caption = "状态0待处理1打开2完成3退回4他人已处理5他人已退回")]
        public int Status { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Column(Caption = "序号")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public FlowTask Clone()
        {
            return (FlowTask)this.MemberwiseClone();
        }
    }
}
