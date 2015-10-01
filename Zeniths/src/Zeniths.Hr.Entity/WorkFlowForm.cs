// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 流程表单
    /// </summary>
    [Table(Caption = "流程表单")]
    [PrimaryKey("WorkFlowFormId", true)]
    public class WorkFlowForm
    {
        /// <summary>
        /// 表单主键
        /// </summary>
        //[Required(ErrorMessage = "请输入表单主键")]
        //[StringLength(4, ErrorMessage = "表单主键长度不能超过{1}")]
        [Column(Caption = "表单主键", Exported = false)]
        public int WorkFlowFormId { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        //[Required(ErrorMessage = "请输入表单名称")]
        //[StringLength(100, ErrorMessage = "表单名称长度不能超过{1}")]
        [Column(Caption = "表单名称")]
        public string WorkFlowFormName { get; set; }

        /// <summary>
        /// 表单分类
        /// </summary>
        //[Required(ErrorMessage = "请输入表单分类")]
        //[StringLength(100, ErrorMessage = "表单分类长度不能超过{1}")]
        [Column(Caption = "表单分类")]
        public string WorkFlowFormCategory { get; set; }

        /// <summary>
        /// 表单地址
        /// </summary>
        //[Required(ErrorMessage = "请输入表单地址")]
        //[StringLength(500, ErrorMessage = "表单地址长度不能超过{1}")]
        [Column(Caption = "表单地址")]
        public string Url { get; set; }

        /// <summary>
        /// 启用状态
        /// </summary>
        //[Required(ErrorMessage = "请输入启用状态")]
        //[StringLength(1, ErrorMessage = "启用状态长度不能超过{1}")]
        [Column(Caption = "启用状态")]
        public bool IsEnabled { get; set; }

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
        public WorkFlowForm Clone()
        {
            return (WorkFlowForm)this.MemberwiseClone();
        }
    }
}
