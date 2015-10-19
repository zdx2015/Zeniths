// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统用户
    /// </summary>
    [Table(Caption = "系统用户")]
    [PrimaryKey("Id", true)]
    public class SystemUser
    {
		/// <summary>
        /// 主键
        /// </summary>
        //[Required(ErrorMessage = "请输入主键")]
        //[StringLength(4, ErrorMessage = "主键长度不能超过{1}")]
        [Column(Caption = "主键")]
        public int Id { get; set; }

		/// <summary>
        /// 帐号
        /// </summary>
        //[Required(ErrorMessage = "请输入帐号")]
        //[StringLength(100, ErrorMessage = "帐号长度不能超过{1}")]
        [Column(Caption = "帐号")]
        public string Account { get; set; }

		/// <summary>
        /// 密码
        /// </summary>
        //[Required(ErrorMessage = "请输入密码")]
        //[StringLength(500, ErrorMessage = "密码长度不能超过{1}")]
        [Column(Caption = "密码")]
        public string Password { get; set; }

		/// <summary>
        /// 姓名
        /// </summary>
        //[Required(ErrorMessage = "请输入姓名")]
        //[StringLength(100, ErrorMessage = "姓名长度不能超过{1}")]
        [Column(Caption = "姓名")]
        public string Name { get; set; }

		/// <summary>
        /// 简拼
        /// </summary>
        //[StringLength(100, ErrorMessage = "简拼长度不能超过{1}")]
        [Column(Caption = "简拼")]
        public string NameSpell { get; set; }

		/// <summary>
        /// 部门主键
        /// </summary>
        //[Required(ErrorMessage = "请输入部门主键")]
        //[StringLength(4, ErrorMessage = "部门主键长度不能超过{1}")]
        [Column(Caption = "部门主键")]
        public int DepartmentId { get; set; }

		/// <summary>
        /// 部门名称
        /// </summary>
        //[StringLength(100, ErrorMessage = "部门名称长度不能超过{1}")]
        [Column(Caption = "部门名称")]
        public string DepartmentName { get; set; }

		/// <summary>
        /// 允许开始时间
        /// </summary>
        //[StringLength(8, ErrorMessage = "允许开始时间长度不能超过{1}")]
        [Column(Caption = "允许开始时间")]
        public DateTime? AllowStartDateTime { get; set; }

		/// <summary>
        /// 允许结束时间
        /// </summary>
        //[StringLength(8, ErrorMessage = "允许结束时间长度不能超过{1}")]
        [Column(Caption = "允许结束时间")]
        public DateTime? AllowEndDateTime { get; set; }

		/// <summary>
        /// 第一次登录时间
        /// </summary>
        //[StringLength(8, ErrorMessage = "第一次登录时间长度不能超过{1}")]
        [Column(Caption = "第一次登录时间")]
        public DateTime? FirstVisitDateTime { get; set; }

		/// <summary>
        /// 最后一次登录时间
        /// </summary>
        //[StringLength(8, ErrorMessage = "最后一次登录时间长度不能超过{1}")]
        [Column(Caption = "最后一次登录时间")]
        public DateTime? LastVisitDateTime { get; set; }

		/// <summary>
        /// 登录次数
        /// </summary>
        //[Required(ErrorMessage = "请输入登录次数")]
        //[StringLength(4, ErrorMessage = "登录次数长度不能超过{1}")]
        [Column(Caption = "登录次数")]
        public int LoginCount { get; set; }

		/// <summary>
        /// 是否管理员
        /// </summary>
        //[Required(ErrorMessage = "请输入是否管理员")]
        //[StringLength(1, ErrorMessage = "是否管理员长度不能超过{1}")]
        [Column(Caption = "是否管理员")]
        public bool IsAdmin { get; set; }

		/// <summary>
        /// 是否启用
        /// </summary>
        //[Required(ErrorMessage = "请输入是否启用")]
        //[StringLength(1, ErrorMessage = "是否启用长度不能超过{1}")]
        [Column(Caption = "是否启用")]
        public bool IsEnabled { get; set; }

		/// <summary>
        /// 是否审核
        /// </summary>
        //[Required(ErrorMessage = "请输入是否审核")]
        //[StringLength(1, ErrorMessage = "是否审核长度不能超过{1}")]
        [Column(Caption = "是否审核")]
        public bool IsAudit { get; set; }

		/// <summary>
        /// 密码提示问题
        /// </summary>
        //[StringLength(100, ErrorMessage = "密码提示问题长度不能超过{1}")]
        [Column(Caption = "密码提示问题")]
        public string HintQuestion { get; set; }

		/// <summary>
        /// 密码提示答案
        /// </summary>
        //[StringLength(100, ErrorMessage = "密码提示答案长度不能超过{1}")]
        [Column(Caption = "密码提示答案")]
        public string HintAnswer { get; set; }

		/// <summary>
        /// 备注
        /// </summary>
        //[StringLength(1000, ErrorMessage = "备注长度不能超过{1}")]
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemUser Clone()
        {
            return (SystemUser)this.MemberwiseClone();
        }
    }
}
