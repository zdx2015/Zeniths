// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 请休假申请
    /// </summary>
    [Table(Caption = "请休假申请")]
    [PrimaryKey("Id", true)]
    public class EmployeeLeave
    {
		/// <summary>
        /// Id
        /// </summary>
        [Column(Caption = "Id", Exported = false)]
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
        /// 岗位
        /// </summary>
		[Column(Caption = "岗位")]
        public string Post { get; set; }

		/// <summary>
        /// 请假类别
        /// </summary>
		[Column(Caption = "请假类别")]
        public string LeaveCategory { get; set; }

		/// <summary>
        /// 预计请假开始日期
        /// </summary>
		[Column(Caption = "预计开始请假时间")]
        public DateTime StartDatetime { get; set; }

        /// <summary>
        /// 预计请假结束日期
        /// </summary>
        [Column(Caption = "预计结束请假时间")]
        public DateTime EndDatetime { get; set; }

		/// <summary>
        /// 请假天数
        /// </summary>
		[Column(Caption = "请假天数")]
        public decimal? Days { get; set; }

		/// <summary>
        /// 请假事由
        /// </summary>
		[Column(Caption = "请假事由")]
        public string Reason { get; set; }

		/// <summary>
        /// 申请时间
        /// </summary>
		[Column(Caption = "申请时间")]
        public DateTime ApplyDateTime { get; set; }

		/// <summary>
        /// 工作代理人主键
        /// </summary>
		[Column(Caption = "工作代理人主键")]
        public int? JobAgentId { get; set; }

		/// <summary>
        /// 工作代理人是否同意
        /// </summary>
		[Column(Caption = "工作代理人是否同意")]
        public bool? JobAgentIsAudit { get; set; }

		/// <summary>
        /// 工作代理人意见
        /// </summary>
		[Column(Caption = "工作代理人意见")]
        public string JobAgentOpinion { get; set; }

		/// <summary>
        /// 工作代理人签字
        /// </summary>
		[Column(Caption = "工作代理人签字")]
        public string JobAgentSign { get; set; }

		/// <summary>
        /// 工作代理人签字日期
        /// </summary>
		[Column(Caption = "工作代理人签字日期")]
        public DateTime? JobAgentSignDate { get; set; }

		/// <summary>
        /// 部门负责人主键
        /// </summary>
		[Column(Caption = "部门负责人主键")]
        public int? DepartmentManagerId { get; set; }

		/// <summary>
        /// 部门负责人是否同意
        /// </summary>
		[Column(Caption = "部门负责人是否同意")]
        public bool? DepartmentManagerIsAudit { get; set; }

		/// <summary>
        /// 部门负责人意见
        /// </summary>
		[Column(Caption = "部门负责人意见")]
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
        /// 实际请假开始日期
        /// </summary>
		[Column(Caption = "实际请假开始日期")]
        public DateTime? RealStartDatetime { get; set; }

        /// <summary>
        /// 实际请假结束日期
        /// </summary>
        [Column(Caption = "实际请假结束日期")]
        public DateTime? RealEndDatetime { get; set; }
        
		/// <summary>
        /// 实休天数
        /// </summary>
		[Column(Caption = "实休天数")]
        public double? ActualDays { get; set; }

        /// <summary>
        /// 销假时间
        /// </summary>
        [Column(Caption = "销假时间")]
        public DateTime? CancelLeaveDateTime { get; set; }

        /// <summary>
        /// 销假人主键
        /// </summary>
        [Column(Caption = "销假人主键")]
        public int? CancelLeavePersonId { get; set; }

		/// <summary>
        /// 销假人姓名
        /// </summary>
		[Column(Caption = "销假人姓名")]
        public string CancelLeavePersonName { get; set; }

		/// <summary>
        /// 部门负责人(销假)主键
        /// </summary>
		[Column(Caption = "部门负责人(销假)主键")]
        public int? DepartmentManagerCancelId { get; set; }

		/// <summary>
        /// 部门负责人(销假]是否同意
        /// </summary>
		[Column(Caption = "部门负责人(销假]是否同意")]
        public bool? DepartmentManagerCancelIsAudit { get; set; }

		/// <summary>
        /// 部门负责人(销假]意见
        /// </summary>
		[Column(Caption = "部门负责人(销假]意见")]
        public string DepartmentManagerCancelOpinion { get; set; }

		/// <summary>
        /// 部门负责人(销假]签字
        /// </summary>
		[Column(Caption = "部门负责人(销假]签字")]
        public string DepartmentManagerCancelSign { get; set; }

		/// <summary>
        /// 部门负责人(销假]签字日期
        /// </summary>
		[Column(Caption = "部门负责人(销假]签字日期")]
        public DateTime? DepartmentManagerCancelSignDate { get; set; }

		/// <summary>
        /// 行政人力资源部主键
        /// </summary>
		[Column(Caption = "行政人力资源部主键")]
        public int? HRManagerId { get; set; }

		/// <summary>
        /// 行政人力资源部是否同意
        /// </summary>
		[Column(Caption = "行政人力资源部是否同意")]
        public bool? HRManagerIsAudit { get; set; }

		/// <summary>
        /// 行政人力资源部意见
        /// </summary>
		[Column(Caption = "行政人力资源部意见")]
        public string HRManagerOpinion { get; set; }

		/// <summary>
        /// 行政人力资源部签字
        /// </summary>
		[Column(Caption = "行政人力资源部签字")]
        public string HRManagerSign { get; set; }

		/// <summary>
        /// 行政人力资源部签字日期
        /// </summary>
		[Column(Caption = "行政人力资源部签字日期")]
        public DateTime? HRManagerSignDate { get; set; }

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
        /// 状态
        /// </summary>
		[Column(Caption = "状态")]
        public int Status { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        /// <returns></returns>
        public string StatusName()
        {
            if (Status == 1)
            {
                return "待提交";
            }
            else if (Status == 2)
            {
                return "审批中";
            }
            else
            {
                return "完成";
            }
        }

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
        public EmployeeLeave Clone()
        {
            return (EmployeeLeave)this.MemberwiseClone();
        }
    }
}
