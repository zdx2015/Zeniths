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
    [TextKey("Name")]
    [SortPath("SortPath")]
    public class SystemDepartment
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
        /// 名称
        /// </summary>
        [Column(Caption = "名称")]
        public string Name { get; set; }
        
		/// <summary>
        /// 部门领导主键
        /// </summary>
        [Column(Caption = "部门领导主键")]
        public int DepartmentLeaderId { get; set; }

		/// <summary>
        /// 部门领导名称
        /// </summary>
        [Column(Caption = "部门领导名称")]
        public string DepartmentLeaderName { get; set; }

		/// <summary>
        /// 分管领导主键
        /// </summary>
        [Column(Caption = "分管领导主键")]
        public int ChargeLeaderId { get; set; }

		/// <summary>
        /// 分管领导名称
        /// </summary>
        [Column(Caption = "分管领导名称")]
        public string ChargeLeaderName { get; set; }

		/// <summary>
        /// 主管领导主键
        /// </summary>
        [Column(Caption = "主管领导主键")]
        public int MainLeaderId { get; set; }

		/// <summary>
        /// 主管领导名称
        /// </summary>
        [Column(Caption = "主管领导名称")]
        public string MainLeaderName { get; set; }

		/// <summary>
        /// 排序路径
        /// </summary>
        [Column(Caption = "排序路径")]
        public string SortPath { get; set; }

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
        public SystemDepartment Clone()
        {
            return (SystemDepartment)this.MemberwiseClone();
        }
    }
}
