// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 分享工作日志
    /// </summary>
    [Table(Caption = "分享工作日志")]
    [PrimaryKey("Id", true)]
    public class OAWorkLogExtend
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 日志日期
        /// </summary>
        [Column(Caption = "日志日期")]
        public DateTime LogDate { get; set; }

        /// <summary>
        /// 分享人主键
        /// </summary>
        [Column(Caption = "分享人主键")]
        public int ShareUserId { get; set; }

        /// <summary>
        /// 分享人姓名
        /// </summary>
        [Column(Caption = "分享人姓名")]
        public string ShareUserName { get; set; }

        /// <summary>
        /// 分享人部门名称
        /// </summary>
        [Column(Caption = "分享人部门名称")]
        public string ShareDepartmentName { get; set; }

        /// <summary>
        /// 是否反馈
        /// </summary>
        [Column(Caption = "是否反馈")]
        public bool IsFeedback { get; set; }

        /// <summary>
        /// 反馈信息
        /// </summary>
        [Column(Caption = "反馈信息")]
        public string FeedbackInfomation { get; set; }

        /// <summary>
        /// 反馈时间
        /// </summary>
        [Column(Caption = "反馈时间")]
        public DateTime? FeedbackDateTime { get; set; }

    }
}
