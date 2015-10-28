// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 工作日志分享人
    /// </summary>
    [Table(Caption = "工作日志分享人")]
    [PrimaryKey("Id", true)]
    public class OAWorkLogShare
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 分享日志主键
        /// </summary>
		[Column(Caption = "分享日志主键")]
        public int WorkLogId { get; set; }

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
        /// 分享人部门主键
        /// </summary>
		[Column(Caption = "分享人部门主键")]
        public int ShareDepartmentId { get; set; }

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

        /// <summary>
        /// 复制对象
        /// </summary>
        public OAWorkLogShare Clone()
        {
            return (OAWorkLogShare)this.MemberwiseClone();
        }
    }
}