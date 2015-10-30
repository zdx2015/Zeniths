// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.WorkFlow.Entity
{
    /// <summary>
    /// 流程用户
    /// </summary>
    [Table(Caption = "流程用户")]
    [PrimaryKey("Id", true)]
    public class FlowUser
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 流程实例主键
        /// </summary>
		[Column(Caption = "流程实例主键")]
        public string FlowInstanceId { get; set; }

		/// <summary>
        /// 流程主键
        /// </summary>
		[Column(Caption = "流程主键")]
        public string FlowId { get; set; }

		/// <summary>
        /// 业务主键
        /// </summary>
		[Column(Caption = "业务主键")]
        public string BusinessId { get; set; }

		/// <summary>
        /// 用户主键
        /// </summary>
		[Column(Caption = "用户主键")]
        public string UserId { get; set; }

		/// <summary>
        /// 创建时间
        /// </summary>
		[Column(Caption = "创建时间")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public FlowUser Clone()
        {
            return (FlowUser)this.MemberwiseClone();
        }
    }
}
