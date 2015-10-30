using System;
using Zeniths.Entity;
using Zeniths.Hr.Entity;

namespace Zeniths.Web.Areas.HR.Models
{
    public class EmpLeaveModel: EmpLeave
    {
        /// <summary>
        /// 开始日期
        /// </summary>
        [Column(Caption = "开始日期")]
        public new DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [Column(Caption = "结束日期")]
        public new DateTime? EndDate { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        [Column(Caption = "天数")]
        public new int? Days { get; set; }
    }
}