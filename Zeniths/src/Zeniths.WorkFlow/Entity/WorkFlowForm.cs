// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.WorkFlow.Entity
{
    /// <summary>
    /// 流程表单
    /// </summary>
    [Table(Caption = "流程表单")]
    [PrimaryKey("Id", true)]
    public class WorkFlowForm
    {
        /// <summary>
        /// 表单主键
        /// </summary>
        [Column(Caption = "表单主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        [Column(Caption = "表单名称")]
        public string Name { get; set; }

        /// <summary>
        /// 表单分类
        /// </summary>
        [Column(Caption = "表单分类")]
        public string Category { get; set; }

        /// <summary>
        /// 表单地址
        /// </summary>
        [Column(Caption = "表单地址")]
        public string Url { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        [Column(Caption = "启用状态")]
        public bool IsEnabled { get; set; } = true;

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
        public WorkFlowForm Clone() => MemberwiseClone() as WorkFlowForm;
    }
}
