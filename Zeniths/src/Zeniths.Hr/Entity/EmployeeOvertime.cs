// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 加班申请
    /// </summary>
    [Table(Caption = "加班申请")]
    [PrimaryKey("Id", true)]
    public class EmployeeOvertime
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
        /// 部门主键
        /// </summary>
		[Column(Caption = "部门主键")]
        public int DeparmentId { get; set; }

		/// <summary>
        /// 部门名称
        /// </summary>
		[Column(Caption = "部门名称")]
        public string Deparment { get; set; }

		/// <summary>
        /// 职务
        /// </summary>
		[Column(Caption = "职务")]
        public string Post { get; set; }

		/// <summary>
        /// 加班事由
        /// </summary>
		[Column(Caption = "加班事由")]
        public string Reasons { get; set; }

		/// <summary>
        /// 加班起始日期
        /// </summary>
		[Column(Caption = "加班起始日期")]
        public DateTime StartDatetime { get; set; }

		/// <summary>
        /// 加班结束日期
        /// </summary>
		[Column(Caption = "加班结束日期")]
        public DateTime EndDatetime { get; set; }

		/// <summary>
        /// 加班申请日期
        /// </summary>
		[Column(Caption = "加班申请日期")]
        public DateTime ApplyDateTime { get; set; }

		/// <summary>
        /// 加班天数
        /// </summary>
		[Column(Caption = "加班天数")]
        public decimal? Days { get; set; }

		/// <summary>
        /// 部门负责人签字
        /// </summary>
		[Column(Caption = "部门负责人签字")]
        public int? DepartmentManagerId { get; set; }

		/// <summary>
        /// 部门负责人是否同意
        /// </summary>
		[Column(Caption = "部门负责人是否同意")]
        public bool? DepartmentManagerIsAudit { get; set; }

		/// <summary>
        /// 部门负责人签字
        /// </summary>
		[Column(Caption = "部门负责人签字")]
        public string DepartmentManagerOpinion { get; set; }

		/// <summary>
        /// 部门负责人签字
        /// </summary>
		[Column(Caption = "部门负责人签字")]
        public string DepartmentManagerSign { get; set; }

		/// <summary>
        /// 部门负责人签字日期
        /// </summary>
		[Column(Caption = "部门负责人签字日期")]
        public DateTime? DepartmentManagerSignDate { get; set; }

		/// <summary>
        /// 总经理主键
        /// </summary>
		[Column(Caption = "总经理主键")]
        public int? GeneralManagerId { get; set; }

		/// <summary>
        /// 总经理是否同意
        /// </summary>
		[Column(Caption = "总经理是否同意")]
        public bool? GeneralManagerIsAudit { get; set; }

		/// <summary>
        /// 总经理意见
        /// </summary>
		[Column(Caption = "总经理意见")]
        public string GeneralManagerOpinion { get; set; }

		/// <summary>
        /// 总经理签字
        /// </summary>
		[Column(Caption = "总经理签字")]
        public string GeneralManagerSign { get; set; }

		/// <summary>
        /// 总经理签字日期
        /// </summary>
		[Column(Caption = "总经理签字日期")]
        public DateTime? GeneralManagerSignDate { get; set; }

		/// <summary>
        /// 状态
        /// </summary>
		[Column(Caption = "状态")]
        public int Status { get; set; }

		/// <summary>
        /// 任务名称
        /// </summary>
		[Column(Caption = "任务名称")]
        public string Title { get; set; }

		/// <summary>
        /// 流程主键
        /// </summary>
		[Column(Caption = "流程主键")]
        public string FlowId { get; set; }

		/// <summary>
        /// 流程名称
        /// </summary>
		[Column(Caption = "流程名称")]
        public string FlowName { get; set; }

		/// <summary>
        /// 流程实例主键
        /// </summary>
		[Column(Caption = "流程实例主键")]
        public string FlowInstanceId { get; set; }

		/// <summary>
        /// 步骤主键
        /// </summary>
		[Column(Caption = "步骤主键")]
        public string StepId { get; set; }

		/// <summary>
        /// 步骤名称
        /// </summary>
		[Column(Caption = "步骤名称")]
        public string StepName { get; set; }

		/// <summary>
        /// 流程是否完成
        /// </summary>
		[Column(Caption = "流程是否完成")]
        public bool IsFinish { get; set; }

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
        public EmployeeOvertime Clone()
        {
            return (EmployeeOvertime)this.MemberwiseClone();
        }
    }
}
