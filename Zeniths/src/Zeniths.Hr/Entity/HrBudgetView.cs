// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 费用预算表
    /// </summary>
    [Table(Caption = "费用预算表")]
    public class HrBudgetView
    {
        /// <summary>
        /// 预算主键
        /// </summary>
        [Column(Caption = "预算主键")]
        public int BudgetId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column(Caption = "部门名称")]
        public string BudgetDepartmentName { get; set; }

        /// <summary>
        /// 预算月份
        /// </summary>
        [Column(Caption = "预算月份")]
        public string BudgetMonth { get; set; }

        /// <summary>
        /// 办公类
        /// </summary>
        [Column(Caption = "办公类")]
        public decimal? 办公类 { get; set; }

        /// <summary>
        /// 材料类
        /// </summary>
        [Column(Caption = "材料类")]
        public decimal? 材料类 { get; set; }

        /// <summary>
        /// 差旅类
        /// </summary>
        [Column(Caption = "差旅类")]
        public decimal? 差旅类 { get; set; }

        /// <summary>
        /// 车辆类
        /// </summary>
        [Column(Caption = "车辆类")]
        public decimal? 车辆类 { get; set; }

        /// <summary>
        /// 福利类
        /// </summary>
        [Column(Caption = "福利类")]
        public decimal? 福利类 { get; set; }

        /// <summary>
        /// 工程类
        /// </summary>
        [Column(Caption = "工程类")]
        public decimal? 工程类 { get; set; }

        /// <summary>
        /// 工资薪金
        /// </summary>
        [Column(Caption = "工资薪金")]
        public decimal? 工资薪金 { get; set; }

        /// <summary>
        /// 会议费
        /// </summary>
        [Column(Caption = "会议费")]
        public decimal? 会议费 { get; set; }

        /// <summary>
        /// 交通类
        /// </summary>
        [Column(Caption = "交通类")]
        public decimal? 交通类 { get; set; }

        /// <summary>
        /// 培训类
        /// </summary>
        [Column(Caption = "培训类")]
        public decimal? 培训类 { get; set; }

        /// <summary>
        /// 其他类
        /// </summary>
        [Column(Caption = "其他类")]
        public decimal? 其他类 { get; set; }

        /// <summary>
        /// 业务宣传费
        /// </summary>
        [Column(Caption = "业务宣传费")]
        public decimal? 业务宣传费 { get; set; }

        /// <summary>
        /// 招待类
        /// </summary>
        [Column(Caption = "招待类")]
        public decimal? 招待类 { get; set; }

        /// <summary>
        /// 资产类
        /// </summary>
        [Column(Caption = "资产类")]
        public decimal? 资产类 { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        [Column(Caption = "处理状态")]
        public string Status { get; set; }

        /// <summary>
        /// sumMoney
        /// </summary>
        [Column(Caption = "sumMoney")]
        public decimal? sumMoney { get; set; }
        
        /// <summary>
        /// 流程主键
        /// </summary>
		[Column(Caption = "流程主键")]
        public string FlowId { get; set; }
        /// <summary>
        /// 复制对象
        /// </summary>
        public HrBudgetView Clone()
        {
            return (HrBudgetView)this.MemberwiseClone();
        }
    }
}