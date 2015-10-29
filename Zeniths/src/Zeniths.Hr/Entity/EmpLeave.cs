// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 员工请假
    /// </summary>
    [Table(Caption = "员工请假")]
    [PrimaryKey("Id", true)]
    public class EmpLeave
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 开始日期
        /// </summary>
		[Column(Caption = "开始日期")]
        public DateTime StartDate { get; set; }

		/// <summary>
        /// 结束日期
        /// </summary>
		[Column(Caption = "结束日期")]
        public DateTime EndDate { get; set; }

		/// <summary>
        /// 天数
        /// </summary>
		[Column(Caption = "天数")]
        public int Days { get; set; }

		/// <summary>
        /// 类型
        /// </summary>
		[Column(Caption = "类型")]
        public string Category { get; set; }

		/// <summary>
        /// 原因
        /// </summary>
		[Column(Caption = "原因")]
        public string Reason { get; set; }

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
        public EmpLeave Clone()
        {
            return (EmpLeave)this.MemberwiseClone();
        }
    }
}
