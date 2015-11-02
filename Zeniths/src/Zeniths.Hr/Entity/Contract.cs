// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 
    /// </summary>
    [Table(Caption = "合同台帐")]
    [PrimaryKey("Id", true)]
    public class Contract
    {
        public Contract()
        {
            StepDateTime = DateTime.Now;//最后更新时间默认
            CreateDateTime = DateTime.Now;//创建时间默认
            Title = "";
            FlowId = "";
            FlowName = "";
            FlowInstanceId = "";
            StepId = "";
            StepName = "未处理";
            IsFinish = false;
            UndertakeDepartmentID = CreateDepartmentId;//承办部门默认与起草人登陆用户所在部门相同
            UndertakeDepartmentName = CreateDepartmentName;
            SenderID = CreateUserId;//送审人默认与创建人相同
            SenderName = CreateUserName;
            StateId = "Modifying";
            StateName = "未提交";

        }
        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "合同名称")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "合同分类")]
        public string TypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "级别ID")]
        public int? LevelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "级别")]
        public string LevelName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "甲方")]
        public string FirstParty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "乙方")]
        public string SecondParty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "合同编号")]
        public string ContractNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "是否变更")]
        public bool IsChange { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "变更编号")]
        public string ChangeNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "合同金额")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "合同内容")]
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "有效期")]
        public DateTime? ValidDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "签章日期")]
        public DateTime? SignDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "原件返回日期")]
        public DateTime? ReturnFileDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "合同附件")]
        public string Attachment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "会议附件")]
        public string MeetingAttachment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "送审人ID")]
        public int? SenderID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "送审人")]
        public string SenderName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "送审时间")]
        public DateTime? SendDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "承办部门ID")]
        public int? UndertakeDepartmentID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "承办部门")]
        public string UndertakeDepartmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "法务部负责人(设定级别)主建")]
        public int? LegalDepartmentManagerSetID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "法务部负责人(设定级别)签字日期")]
        public DateTime? LegalDepartmentManagerSetSignDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "承办部门负责人主键")]
        public int? UndertakeDepartmentManagerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "承办部门负责人是否同意")]
        public bool? UndertakeDepartmentManagerIsAudit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "承办部门负责人意见")]
        public string UndertakeDepartmentManagerOpinion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "承办部门负责人签字")]
        public string UndertakeDepartmentManagerSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "承办部门负责人签字日期")]
        public DateTime? UndertakeDepartmentManagerSignDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "法务部负责人主键")]
        public int? LegalDepartmentManagerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "法务部负责人是否同意")]
        public bool? LegalDepartmentManagerIsAudit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "法务部门负责人意见")]
        public string LegalDepartmentManagerOpinion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "法务部门负责人签字")]
        public string LegalDepartmentManagerSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "法务部门负责人签字日期")]
        public DateTime? LegalDepartmentManagerSignDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "财务部负责人主键")]
        public int? FinancialDepartmentManagerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "财务部负责人是否同意")]
        public bool? FinancialDepartmentManagerIsAudit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "财务部负责人意见")]
        public string FinancialDepartmentManagerOpinion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "财务部负责人签字")]
        public string FinancialDepartmentManagerSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "财务部负责人签字日期")]
        public DateTime? FinancialDepartmentManagerSignDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "审计巡查部负责人主键")]
        public int? AuditDepartmentManagerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "审计巡查部负责人是否同意")]
        public bool? AuditDepartmentManagerIsAudit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "审计巡查部负责人 意见")]
        public string AuditDepartmentManagerOpinion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "审计巡查部负责人签字")]
        public string AuditDepartmentManagerSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "审计巡查部负责人签字日期")]
        public DateTime? AuditDepartmentManagerSignDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "总经理主键")]
        public int? GeneralManagerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "总经理是否同意")]
        public bool? GeneralManagerIsAudit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "总经理意见")]
        public string GeneralManagerOpinion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "总经理签字")]
        public string GeneralManagerSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "总经理签字日期")]
        public DateTime? GeneralManagerSignDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事长主键")]
        public int? ChairmanId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事长是否同意")]
        public bool? ChairmanIsAudit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事长意见")]
        public string ChairmanOpinion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事长签字")]
        public string ChairmanSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事长签字日期")]
        public DateTime? ChairmanSignDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事会负责人主键")]
        public int? BoardManagerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事会负责人是否同意")]
        public bool? BoardManagerIsAudit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事会负责人意见")]
        public string BoardManagerOpinion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事会负责人签字")]
        public string BoardManagerSign { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "董事会负责人签字日期")]
        public DateTime? BoardManagerSignDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "任务名称")]
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "流程主键")]
        public string FlowId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "流程名称")]
        public string FlowName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "流程实例主键")]
        public string FlowInstanceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "步骤主键")]
        public string StepId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "步骤名称")]
        public string StepName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "最后处理时间")]
        public DateTime StepDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "流程是否完成")]
        public bool IsFinish { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "创建用户主键")]
        public int CreateUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "创建用户姓名")]
        public string CreateUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "创建人部门主键")]
        public int CreateDepartmentId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "创建人部门姓名")]
        public string CreateDepartmentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "创建时间")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "状态Id")]
        public string StateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "状态名称")]
        public string StateName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "数量")]
        public decimal? Count { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "单位")]
        public string Unit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Column(Caption = "单价")]
        public decimal? Price { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public Contract Clone()
        {
            return (Contract)this.MemberwiseClone();
        }
    }
}