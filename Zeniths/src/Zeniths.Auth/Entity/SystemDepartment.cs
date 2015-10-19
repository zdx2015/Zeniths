// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统部门
    /// </summary>
    [Table(Caption = "系统部门")]
    [PrimaryKey("Id", true)]
    [ParentKey("ParentId")]
    [SortPath("SortPath")]
    public class SystemDepartment
    {
		/// <summary>
        /// 主键
        /// </summary>
        //[Required(ErrorMessage = "请输入主键")]
        //[StringLength(4, ErrorMessage = "主键长度不能超过{1}")]
        [Column(Caption = "主键")]
        public int Id { get; set; }

		/// <summary>
        /// 父级主键
        /// </summary>
        //[Required(ErrorMessage = "请输入父级主键")]
        //[StringLength(4, ErrorMessage = "父级主键长度不能超过{1}")]
        [Column(Caption = "父级主键")]
        public int ParentId { get; set; }

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
        /// 部门领导主键
        /// </summary>
        //[StringLength(4, ErrorMessage = "部门领导主键长度不能超过{1}")]
        [Column(Caption = "部门领导主键")]
        public int? DepartmentLeaderId { get; set; }

		/// <summary>
        /// 部门领导名称
        /// </summary>
        //[StringLength(100, ErrorMessage = "部门领导名称长度不能超过{1}")]
        [Column(Caption = "部门领导名称")]
        public string DepartmentLeaderName { get; set; }

		/// <summary>
        /// 分管领导主键
        /// </summary>
        //[StringLength(4, ErrorMessage = "分管领导主键长度不能超过{1}")]
        [Column(Caption = "分管领导主键")]
        public int? ChargeLeaderId { get; set; }

		/// <summary>
        /// 分管领导名称
        /// </summary>
        //[StringLength(100, ErrorMessage = "分管领导名称长度不能超过{1}")]
        [Column(Caption = "分管领导名称")]
        public string ChargeLeaderName { get; set; }

		/// <summary>
        /// 主管领导主键
        /// </summary>
        //[StringLength(4, ErrorMessage = "主管领导主键长度不能超过{1}")]
        [Column(Caption = "主管领导主键")]
        public int? MainLeaderId { get; set; }

		/// <summary>
        /// 主管领导名称
        /// </summary>
        //[StringLength(100, ErrorMessage = "主管领导名称长度不能超过{1}")]
        [Column(Caption = "主管领导名称")]
        public string MainLeaderName { get; set; }

		/// <summary>
        /// 排序路径
        /// </summary>
        //[Required(ErrorMessage = "请输入排序路径")]
        //[StringLength(1000, ErrorMessage = "排序路径长度不能超过{1}")]
        [Column(Caption = "排序路径")]
        public string SortPath { get; set; }

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
        public SystemDepartment Clone()
        {
            return (SystemDepartment)this.MemberwiseClone();
        }
    }
}
