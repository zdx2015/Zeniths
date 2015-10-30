// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 日常费用报销明细
    /// </summary>
    [Table(Caption = "日常费用报销明细")]
    [PrimaryKey("Id",true)]
    public class DailyReimburseDetails
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 报销主表主键
        /// </summary>
		[Column(Caption = "报销主表主键")]
        public int ReimburseId { get; set; }

        /// <summary>
        /// 项目名称Id
        /// </summary>
        [Column(Caption = "项目名称Id")]
        public int ItemId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Column(Caption = "项目名称")]
        public string ItemName { get; set; }

		/// <summary>
        /// 费用类别主键
        /// </summary>
		[Column(Caption = "费用类别主键")]
        public int CategoryId { get; set; }

		/// <summary>
        /// 费用类别名称
        /// </summary>
		[Column(Caption = "费用类别名称")]
        public string CategoryName { get; set; }

		/// <summary>
        /// 费用金额
        /// </summary>
		[Column(Caption = "费用金额")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 临时虚拟序号
        /// </summary>
        [Ignore]
        public int TempSortNum { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public DailyReimburseDetails Clone()
        {
            return (DailyReimburseDetails)this.MemberwiseClone();
        }
    }
}