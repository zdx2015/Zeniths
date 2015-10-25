// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.WorkFlow.Entity
{
    /// <summary>
    /// 流程
    /// </summary>
    [Table(Caption = "流程")]
    [PrimaryKey("Id")]
    public class Flow
    {
        /// <summary>
        /// 流程主键
        /// </summary>
        [Column(Caption = "流程主键", Exported = true)]
        public string Id { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [Column(Caption = "流程名称")]
        public string Name { get; set; }

        /// <summary>
        /// 流程分类
        /// </summary>
        [Column(Caption = "流程分类")]
        public string Category { get; set; }

        /// <summary>
        /// 流程信息
        /// </summary>
        [Column(Caption = "流程信息")]
        public string Json { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column(Caption = "是否启用")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否启用调试
        /// </summary>
        [Column(Caption = "是否启用调试")]
        public bool IsDebug { get; set; }

        /// <summary>
        /// 调试用户
        /// </summary>
        [Column(Caption = "调试用户")]
        public string DebugUserIds { get; set; }

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
        public Flow Clone()
        {
            return (Flow)this.MemberwiseClone();
        }
    }
}
