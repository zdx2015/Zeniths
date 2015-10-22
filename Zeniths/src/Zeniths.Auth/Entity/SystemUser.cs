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
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        [Column(Caption = "帐号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column(Caption = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Column(Caption = "姓名")]
        public string Name { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        [Column(Caption = "简拼")]
        public string NameSpell { get; set; }

        /// <summary>
        /// 部门主键
        /// </summary>
        [Column(Caption = "部门主键")]
        public int DepartmentId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column(Caption = "部门名称")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// 允许开始时间
        /// </summary>
        [Column(Caption = "允许开始时间")]
        public DateTime? AllowStartDateTime { get; set; }

        /// <summary>
        /// 允许结束时间
        /// </summary>
        [Column(Caption = "允许结束时间")]
        public DateTime? AllowEndDateTime { get; set; }

        /// <summary>
        /// 第一次登录时间
        /// </summary>
        [Column(Caption = "第一次登录时间")]
        public DateTime? FirstVisitDateTime { get; set; }

        /// <summary>
        /// 最后一次登录时间
        /// </summary>
        [Column(Caption = "最后一次登录时间")]
        public DateTime? LastVisitDateTime { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        [Column(Caption = "登录次数")]
        public int LoginCount { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [Column(Caption = "是否管理员")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column(Caption = "是否启用")]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 是否审核
        /// </summary>
        [Column(Caption = "是否审核")]
        public bool IsAudit { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        [Column(Caption = "电子邮件")]
        public string Email { get; set; }

        /// <summary>
        /// 密码提示问题
        /// </summary>
        [Column(Caption = "密码提示问题")]
        public string HintQuestion { get; set; }

        /// <summary>
        /// 密码提示答案
        /// </summary>
        [Column(Caption = "密码提示答案")]
        public string HintAnswer { get; set; }

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
        public SystemUser Clone()
        {
            return (SystemUser)this.MemberwiseClone();
        }
    }
}
