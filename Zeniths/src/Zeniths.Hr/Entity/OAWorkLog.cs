// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 工作日志
    /// </summary>
    [Table(Caption = "工作日志")]
    [PrimaryKey("Id", true)]
    public class OAWorkLog
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
        /// 本日工作总结
        /// </summary>
		[Column(Caption = "本日工作总结")]
        public string WorkSummary { get; set; }

		/// <summary>
        /// 明日工作计划
        /// </summary>
		[Column(Caption = "明日工作计划")]
        public string TomorrowPlan { get; set; }

		/// <summary>
        /// 其他说明
        /// </summary>
		[Column(Caption = "其他说明")]
        public string OtheInstructions { get; set; }

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
        public OAWorkLog Clone()
        {
            return (OAWorkLog)this.MemberwiseClone();
        }
    }
}