// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.WorkFlow.Entity
{
    /// <summary>
    /// 流程按钮
    /// </summary>
    [Table(Caption = "流程按钮")]
    [PrimaryKey("Id", true)]
    public class FlowButton
    {
        /// <summary>
        /// 按钮主键
        /// </summary>
        //[Required(ErrorMessage = "请输入按钮主键")]
        //[StringLength(4, ErrorMessage = "按钮主键长度不能超过{1}")]
        [Column(Caption = "按钮主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>
        //[Required(ErrorMessage = "请输入按钮名称")]
        //[StringLength(100, ErrorMessage = "按钮名称长度不能超过{1}")]
        [Column(Caption = "按钮名称")]
        public string Name { get; set; }

        /// <summary>
        /// 图标样式
        /// </summary>
        //[StringLength(100, ErrorMessage = "图标样式长度不能超过{1}")]
        [Column(Caption = "图标样式")]
        public string IconClass { get; set; }

        /// <summary>
        /// 图标颜色
        /// </summary>
        [Column(Caption = "图标颜色")]
        public string IconColor { get; set; } = "#000000";

        /// <summary>
        /// 执行脚本
        /// </summary>
        //[Required(ErrorMessage = "请输入执行脚本")]
        //[StringLength(500, ErrorMessage = "执行脚本长度不能超过{1}")]
        [Column(Caption = "执行脚本")]
        public string Script { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        //[Required(ErrorMessage = "请输入启用状态")]
        //[StringLength(1, ErrorMessage = "启用状态长度不能超过{1}")]
        [Column(Caption = "启用状态")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 序号
        /// </summary>
        //[Required(ErrorMessage = "请输入序号")]
        //[StringLength(4, ErrorMessage = "序号长度不能超过{1}")]
        [Column(Caption = "序号")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        //[StringLength(1000, ErrorMessage = "备注长度不能超过{1}")]
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public FlowButton Clone() => this.MemberwiseClone() as FlowButton;
    }
}
