// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 外出登记表
    /// </summary>
    [Table(Caption = "外出登记表")]
    [PrimaryKey("Id", true)]
    public class EmployeeGoOutRegistration
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 员工主键
        /// </summary>
		[Column(Caption = "员工主键")]
        public int EmployeeId { get; set; }

		/// <summary>
        /// 员工姓名
        /// </summary>
		[Column(Caption = "员工姓名")]
        public string EmployeeName { get; set; }

		/// <summary>
        /// 外出事由
        /// </summary>
		[Column(Caption = "外出事由")]
        public string GoOutReason { get; set; }

		/// <summary>
        /// 外出时间
        /// </summary>
		[Column(Caption = "外出时间")]
        public DateTime GoOutDateTime { get; set; }

		/// <summary>
        /// 申请时间
        /// </summary>
		[Column(Caption = "申请时间")]
        public DateTime ApplyDateTime { get; set; }

        /// <summary>
        /// 预计返回时间
        /// </summary>
        [Column(Caption = "预计返回时间")]
        public DateTime PlanBackDateTime { get; set; }

        /// <summary>
        /// 实际外出时间
        /// </summary>
        [Column(Caption = "实际外出时间")]
        public DateTime? RealGoOutDateTime { get; set; }
        
        /// <summary>
        /// 实际返回时间
        /// </summary>
        [Column(Caption = "实际返回时间")]
        public DateTime? RealBackDateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
		[Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 主管领导主键
        /// </summary>
        [Column(Caption = "主管领导主键")]
        public int? GeneralManagerId { get; set; }

        /// <summary>
        /// 主管领导是否同意
        /// </summary>
        [Column(Caption = "主管领导是否同意")]
        public bool? GeneralManagerIsAudit { get; set; }

        /// <summary>
        /// 主管领导意见
        /// </summary>
        [Column(Caption = "主管领导意见")]
        public string GeneralManagerOpinion { get; set; }

        /// <summary>
        /// 主管领导签字
        /// </summary>
        [Column(Caption = "主管领导签字")]
        public string GeneralManagerSign { get; set; }

        /// <summary>
        /// 主管领导签字日期
        /// </summary>
        [Column(Caption = "主管领导签字日期")]
        public DateTime? GeneralManagerSignDate { get; set; }

		/// <summary>
        /// 创建用户主键
        /// </summary>
		[Column(Caption = "创建用户主键")]
        public int CreateUserId { get; set; }

		/// <summary>
        /// 创建用户姓名
        /// </summary>
		[Column(Caption = "创建用户姓名")]
        public string CreateUserName { get; set; }

		/// <summary>
        /// 创建人部门主键
        /// </summary>
		[Column(Caption = "创建人部门主键")]
        public int CreateDepartmentId { get; set; }

		/// <summary>
        /// 创建人部门姓名
        /// </summary>
		[Column(Caption = "创建人部门姓名")]
        public string CreateDepartmentName { get; set; }

		/// <summary>
        /// 创建时间
        /// </summary>
		[Column(Caption = "创建时间")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public EmployeeGoOutRegistration Clone()
        {
            return (EmployeeGoOutRegistration)this.MemberwiseClone();
        }
    }
}
