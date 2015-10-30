// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 预算明细信息
    /// </summary>
    [Table(Caption = "预算明细信息")]
    [PrimaryKey("Id", true)]
    public class HrBudgetDetails
    {
		/// <summary>
        /// 主键
        /// </summary>
		[Column(Caption = "主键")]
        public int Id { get; set; }

		/// <summary>
        /// 预算主键
        /// </summary>
		[Column(Caption = "预算主键")]
        public int BudgetId { get; set; }

		/// <summary>
        /// 预算类别主键
        /// </summary>
		[Column(Caption = "预算类别主键")]
        public int BudgetCategoryId { get; set; }

		/// <summary>
        /// 预算类别名称
        /// </summary>
		[Column(Caption = "预算类别名称")]
        public string BudgetCategoryName { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Column(Caption = "项目名称")]
        public string BudgetItemName { get; set; }
        /// <summary>
        /// 项目主键
        /// </summary>
        [Column(Caption = "项目主键")]
        public int BudgetItemId { get; set; }
        
        /// <summary>
        /// 预算金额
        /// </summary>
        [Column(Caption = "预算金额")]
        public decimal BudgetMoney { get; set; }

		/// <summary>
        /// 备注
        /// </summary>
		[Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public HrBudgetDetails Clone()
        {
            return (HrBudgetDetails)this.MemberwiseClone();
        }
    }
}