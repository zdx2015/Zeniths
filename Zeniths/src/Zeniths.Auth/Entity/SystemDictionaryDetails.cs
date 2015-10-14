// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统数据字典明细
    /// </summary>
    [Table(Caption = "系统数据字典明细")]
    [PrimaryKey("Id", true)]
    [ParentKey("ParentId")]
    [SortPath("SortPath")]
    public class SystemDictionaryDetails
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }
         
        /// <summary>
        /// 字典主键
        /// </summary>
        [Column(Caption = "字典主键")]
        public int DictionaryId { get; set; }

        /// <summary>
        /// 明细项名称
        /// </summary>
        [Column(Caption = "明细项名称")]
        public string Name { get; set; }

        /// <summary>
        /// 明细项简拼
        /// </summary>
        [Column(Caption = "明细项简拼")]
        public string NameSpell { get; set; }

        /// <summary>
        /// 明细项值
        /// </summary>
        [Column(Caption = "明细项值")]
        public string Value { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [Column(Caption = "序号")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column(Caption = "是否启用")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 备注
        /// </summary>
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemDictionaryDetails Clone()
        {
            return (SystemDictionaryDetails)this.MemberwiseClone();
        }
    }
}
