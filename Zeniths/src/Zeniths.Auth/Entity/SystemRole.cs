// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统角色
    /// </summary>
    [Table(Caption = "系统角色")]
    [PrimaryKey("Id", true)]
    public class SystemRole
    {
		/// <summary>
        /// 主键
        /// </summary>
        //[Required(ErrorMessage = "请输入主键")]
        //[StringLength(4, ErrorMessage = "主键长度不能超过{1}")]
        [Column(Caption = "主键")]
        public int Id { get; set; }

		/// <summary>
        /// 名称
        /// </summary>
        //[Required(ErrorMessage = "请输入名称")]
        //[StringLength(100, ErrorMessage = "名称长度不能超过{1}")]
        [Column(Caption = "名称")]
        public string Name { get; set; }

		/// <summary>
        /// 简拼
        /// </summary>
        //[StringLength(100, ErrorMessage = "简拼长度不能超过{1}")]
        [Column(Caption = "简拼")]
        public string NameSpell { get; set; }

		/// <summary>
        /// 分类
        /// </summary>
        //[StringLength(100, ErrorMessage = "分类长度不能超过{1}")]
        [Column(Caption = "分类")]
        public string Category { get; set; }

		/// <summary>
        /// 是否启用
        /// </summary>
        //[Required(ErrorMessage = "请输入是否启用")]
        //[StringLength(1, ErrorMessage = "是否启用长度不能超过{1}")]
        [Column(Caption = "是否启用")]
        public bool IsEnabled { get; set; }

		/// <summary>
        /// 备注
        /// </summary>
        //[StringLength(1000, ErrorMessage = "备注长度不能超过{1}")]
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemRole Clone()
        {
            return (SystemRole)this.MemberwiseClone();
        }
    }
}
