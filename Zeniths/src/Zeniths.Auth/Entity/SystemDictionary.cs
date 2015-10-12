// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统数据字典
    /// </summary>
    [Table(Caption = "系统数据字典")]
    [PrimaryKey("Id", true)]
    [ParentKey("ParentId")]
    [TextKey("Name")]
    [SortPath("SortPath")]
    public class SystemDictionary
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        [Column(Caption = "父级主键")]
        public int ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Column(Caption = "编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(Caption = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [Column(Caption = "简拼")]
        public string NameSpell { get; set; }

        /// <summary>
        /// 排序路径
        /// </summary>
        [Column(Caption = "排序路径")]
        public string SortPath { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemDictionary Clone()
        {
            return (SystemDictionary)this.MemberwiseClone();
        }
    }
}
