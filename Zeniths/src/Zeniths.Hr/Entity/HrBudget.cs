// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 部门预算
    /// </summary>
    [Table(Caption = "部门预算")]
    public class HrBudget
    {
		/// <summary>
        /// 主键
        /// </summary>
		[Column(Caption = "主键")]
        public int Id { get; set; }

		/// <summary>
        /// 任务标题
        /// </summary>
		[Column(Caption = "任务标题")]
        public string Title { get; set; }

		/// <summary>
        /// 预算月份
        /// </summary>
		[Column(Caption = "预算月份")]
        public DateTime BudgetMonth { get; set; }

		/// <summary>
        /// 预算部门主键
        /// </summary>
		[Column(Caption = "预算部门主键")]
        public int BudgetDepartmentId { get; set; }

		/// <summary>
        /// 预算部门名称
        /// </summary>
		[Column(Caption = "预算部门名称")]
        public string BudgetDepartmentName { get; set; }

		/// <summary>
        /// 流程主键
        /// </summary>
		[Column(Caption = "流程主键")]
        public int? FlowId { get; set; }

		/// <summary>
        /// 流程名称
        /// </summary>
		[Column(Caption = "流程名称")]
        public string FlowName { get; set; }

		/// <summary>
        /// 流程实例主键
        /// </summary>
		[Column(Caption = "流程实例主键")]
        public int? FlowInstanceId { get; set; }

		/// <summary>
        /// 当前步骤主键
        /// </summary>
		[Column(Caption = "当前步骤主键")]
        public int? StepId { get; set; }

		/// <summary>
        /// 当前步骤名称
        /// </summary>
		[Column(Caption = "当前步骤名称")]
        public string StepName { get; set; }

		/// <summary>
        /// 总经理主键
        /// </summary>
		[Column(Caption = "总经理主键")]
        public int? GeneralManagerId { get; set; }

		/// <summary>
        /// 总经理名称
        /// </summary>
		[Column(Caption = "总经理名称")]
        public string GeneralManagerName { get; set; }

		/// <summary>
        /// 总经理审批状态
        /// </summary>
		[Column(Caption = "总经理审批状态")]
        public bool? GeneralManagerStatus { get; set; }

		/// <summary>
        /// 总经理审批意见
        /// </summary>
		[Column(Caption = "总经理审批意见")]
        public string GeneralManagerNote { get; set; }

		/// <summary>
        /// 总经理审批时间
        /// </summary>
		[Column(Caption = "总经理审批时间")]
        public DateTime? GeneralManagerDate { get; set; }

		/// <summary>
        /// 处理状态
        /// </summary>
		[Column(Caption = "处理状态")]
        public int? Status { get; set; }

		/// <summary>
        /// 流程是否完成
        /// </summary>
		[Column(Caption = "流程是否完成")]
        public string IsFinish { get; set; }

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
        /// 创建用户部门主键
        /// </summary>
		[Column(Caption = "创建用户部门主键")]
        public int CreateDepartmentid { get; set; }

		/// <summary>
        /// 创建用户部门名称
        /// </summary>
		[Column(Caption = "创建用户部门名称")]
        public string CreateDepartmentName { get; set; }

		/// <summary>
        /// 创建时间
        /// </summary>
		[Column(Caption = "创建时间")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public HrBudget Clone()
        {
            return (HrBudget)this.MemberwiseClone();
        }
    }
}