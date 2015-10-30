// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 日常费用报销
    /// </summary>
    [Table(Caption = "日常费用报销")]
    [PrimaryKey("Id", true)]
    public class DailyReimburse
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 预算主键
        /// </summary>
		[Column(Caption = "预算主键")]
        public int BudgetId { get; set; }

		/// <summary>
        /// 报销部门主键
        /// </summary>
		[Column(Caption = "报销部门主键")]
        public int ReimburseDepartmentId { get; set; }

		/// <summary>
        /// 报销部门名称
        /// </summary>
		[Column(Caption = "报销部门名称")]
        public string ReimburseDepartmentName { get; set; }

		/// <summary>
        /// 经办人主键
        /// </summary>
		[Column(Caption = "经办人主键")]
        public int ApplicantId { get; set; }

		/// <summary>
        /// 经办人姓名
        /// </summary>
		[Column(Caption = "经办人姓名")]
        public string ApplicantName { get; set; }

		/// <summary>
        /// 申请情况说明
        /// </summary>
		[Column(Caption = "申请情况说明")]
        public string ApplyOpinion { get; set; }

		/// <summary>
        /// 填表日期
        /// </summary>
		[Column(Caption = "填表日期")]
        public DateTime ApplicationDate { get; set; }

		/// <summary>
        /// 报销单顺序号
        /// </summary>
		[Column(Caption = "报销单顺序号")]
        public string ApplySortNumber { get; set; }

		/// <summary>
        /// 附件张数
        /// </summary>
		[Column(Caption = "附件张数")]
        public int? AttachmentCount { get; set; }

		/// <summary>
        /// 报销金额合计
        /// </summary>
		[Column(Caption = "报销金额合计")]
        public decimal? ProjectSumMoney { get; set; }

		/// <summary>
        /// 借款数
        /// </summary>
		[Column(Caption = "借款数")]
        public decimal? BorrowMoney { get; set; }

		/// <summary>
        /// 应退金额
        /// </summary>
		[Column(Caption = "应退金额")]
        public decimal? RefundMoney { get; set; }

		/// <summary>
        /// 应补金额
        /// </summary>
		[Column(Caption = "应补金额")]
        public decimal? AfterBackMoney { get; set; }

		/// <summary>
        /// 备注
        /// </summary>
		[Column(Caption = "备注")]
        public string Note { get; set; }

		/// <summary>
        /// 部门经理主键(预算外)
        /// </summary>
		[Column(Caption = "部门经理主键(预算外)")]
        public int? AddDepartmentManagerId { get; set; }

		/// <summary>
        /// 部门经理姓名(预算外)
        /// </summary>
		[Column(Caption = "部门经理姓名(预算外)")]
        public string AddDepartmentManagerSign { get; set; }

		/// <summary>
        /// 部门经理审批状态(预算外)
        /// </summary>
		[Column(Caption = "部门经理审批状态(预算外)")]
        public bool? AddDepartmentManagerIsAudit { get; set; }

		/// <summary>
        /// 部门经理意见(预算外)
        /// </summary>
		[Column(Caption = "部门经理意见(预算外)")]
        public string AddDepartmentManagerOpinion { get; set; }

		/// <summary>
        /// 部门经理审批时间(预算外)
        /// </summary>
		[Column(Caption = "部门经理审批时间(预算外)")]
        public DateTime? AddDepartmentManagerSignDate { get; set; }

		/// <summary>
        /// 部门经理主键
        /// </summary>
		[Column(Caption = "部门经理主键")]
        public int? DepartmentManagerId { get; set; }

		/// <summary>
        /// 部门经理姓名
        /// </summary>
		[Column(Caption = "部门经理姓名")]
        public string DepartmentManagerSign { get; set; }

		/// <summary>
        /// 部门经理意见
        /// </summary>
		[Column(Caption = "部门经理意见")]
        public string DepartmentManagerOpinion { get; set; }

		/// <summary>
        /// 部门经理审批状态
        /// </summary>
		[Column(Caption = "部门经理审批状态")]
        public bool? DepartmentManagerIsAudit { get; set; }

		/// <summary>
        /// 部门经理审批时间
        /// </summary>
		[Column(Caption = "部门经理审批时间")]
        public DateTime? DepartmentManagerSignDate { get; set; }

		/// <summary>
        /// 财务经理主键
        /// </summary>
		[Column(Caption = "财务经理主键")]
        public int? FinancialManagerId { get; set; }

		/// <summary>
        /// 财务经理姓名
        /// </summary>
		[Column(Caption = "财务经理姓名")]
        public string FinancialManagerSign { get; set; }

		/// <summary>
        /// 财务经理意见
        /// </summary>
		[Column(Caption = "财务经理意见")]
        public string FinancialManagerOpinion { get; set; }

		/// <summary>
        /// 财务经理审批状态
        /// </summary>
		[Column(Caption = "财务经理审批状态")]
        public bool? FinancialManagerIsAudit { get; set; }

		/// <summary>
        /// 财务经理审批时间
        /// </summary>
		[Column(Caption = "财务经理审批时间")]
        public DateTime? FinancialManagerSignDate { get; set; }

		/// <summary>
        /// 会计主键
        /// </summary>
		[Column(Caption = "会计主键")]
        public int? AccountantId { get; set; }

		/// <summary>
        /// 会计姓名
        /// </summary>
		[Column(Caption = "会计姓名")]
        public string AccountantSign { get; set; }

		/// <summary>
        /// 会计审核状态
        /// </summary>
		[Column(Caption = "会计审核状态")]
        public bool? AccountantIsAudit { get; set; }

		/// <summary>
        /// 会计审核意见
        /// </summary>
		[Column(Caption = "会计审核意见")]
        public string AccountantOpinion { get; set; }

		/// <summary>
        /// 会计审核时间
        /// </summary>
		[Column(Caption = "会计审核时间")]
        public DateTime? AccountantSignDate { get; set; }

		/// <summary>
        /// 总经理主键(预算外)
        /// </summary>
		[Column(Caption = "总经理主键(预算外)")]
        public int? AddGeneralManagerId { get; set; }

		/// <summary>
        /// 总经理姓名(预算外)
        /// </summary>
		[Column(Caption = "总经理姓名(预算外)")]
        public string AddGeneralManagerSign { get; set; }

		/// <summary>
        /// 总经理审批状态(预算外)
        /// </summary>
		[Column(Caption = "总经理审批状态(预算外)")]
        public bool? AddGeneralManagerIsAudit { get; set; }

		/// <summary>
        /// 总经理审批意见(预算外)
        /// </summary>
		[Column(Caption = "总经理审批意见(预算外)")]
        public string AddGeneralManagerOpinion { get; set; }

		/// <summary>
        /// 总经理审批时间(预算外)
        /// </summary>
		[Column(Caption = "总经理审批时间(预算外)")]
        public DateTime? AddGeneralManagerSignDate { get; set; }

		/// <summary>
        /// 总经理主键
        /// </summary>
		[Column(Caption = "总经理主键")]
        public int? GeneralManagerId { get; set; }

		/// <summary>
        /// 总经理姓名
        /// </summary>
		[Column(Caption = "总经理姓名")]
        public string GeneralManagerSign { get; set; }

		/// <summary>
        /// 总经理审批状态
        /// </summary>
		[Column(Caption = "总经理审批状态")]
        public bool? GeneralManagerIsAudit { get; set; }

		/// <summary>
        /// 总经理审批意见
        /// </summary>
		[Column(Caption = "总经理审批意见")]
        public string GeneralManagerOpinion { get; set; }

		/// <summary>
        /// 总经理审批时间
        /// </summary>
		[Column(Caption = "总经理审批时间")]
        public DateTime? GeneralManagerSignDate { get; set; }

		/// <summary>
        /// 董事长主键
        /// </summary>
		[Column(Caption = "董事长主键")]
        public int? ChairmanId { get; set; }

		/// <summary>
        /// 董事长姓名
        /// </summary>
		[Column(Caption = "董事长姓名")]
        public string ChairmanSign { get; set; }

		/// <summary>
        /// 董事长审批状态
        /// </summary>
		[Column(Caption = "董事长审批状态")]
        public bool? ChairmanIsAudit { get; set; }

		/// <summary>
        /// 董事长审批意见
        /// </summary>
		[Column(Caption = "董事长审批意见")]
        public string ChairmanOpinion { get; set; }

		/// <summary>
        /// 董事长审批时间
        /// </summary>
		[Column(Caption = "董事长审批时间")]
        public DateTime? ChairmanSignDate { get; set; }

		/// <summary>
        /// 出纳主键
        /// </summary>
		[Column(Caption = "出纳主键")]
        public int? CashierId { get; set; }

		/// <summary>
        /// 出纳姓名
        /// </summary>
		[Column(Caption = "出纳姓名")]
        public string CashierName { get; set; }

		/// <summary>
        /// 出纳付款时间
        /// </summary>
		[Column(Caption = "出纳付款时间")]
        public DateTime? CashierUpdateDateTime { get; set; }

		/// <summary>
        /// 领款金额
        /// </summary>
		[Column(Caption = "领款金额")]
        public decimal? DrawMoney { get; set; }

		/// <summary>
        /// 领款人主键
        /// </summary>
		[Column(Caption = "领款人主键")]
        public int? DrawMoneyId { get; set; }

		/// <summary>
        /// 领款人姓名
        /// </summary>
		[Column(Caption = "领款人姓名")]
        public string DrawMoneyName { get; set; }

		/// <summary>
        /// 领款时间
        /// </summary>
		[Column(Caption = "领款时间")]
        public DateTime? DrawMoneyDateTime { get; set; }

		/// <summary>
        /// 流程标题
        /// </summary>
		[Column(Caption = "流程标题")]
        public string Title { get; set; }

		/// <summary>
        /// 工作流主键
        /// </summary>
		[Column(Caption = "工作流主键")]
        public string FlowId { get; set; }

		/// <summary>
        /// 工作流名称
        /// </summary>
		[Column(Caption = "工作流名称")]
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
        /// 步骤状态
        /// </summary>
        [Column(Caption = "步骤状态")]
        public bool? StepStatus { get; set; }

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
        /// 创建时间
        /// </summary>
		[Column(Caption = "创建时间")]
        public DateTime CreateDateTime { get; set; }

		/// <summary>
        /// 创建人部门主键
        /// </summary>
		[Column(Caption = "创建人部门主键")]
        public int CreateDepartmentId { get; set; }

		/// <summary>
        /// 创建人部门名称
        /// </summary>
		[Column(Caption = "创建人部门名称")]
        public string CreateDepartmentName { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public DailyReimburse Clone()
        {
            return (DailyReimburse)this.MemberwiseClone();
        }
    }
}