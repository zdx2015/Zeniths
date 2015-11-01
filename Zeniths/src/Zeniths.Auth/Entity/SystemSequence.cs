// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统序列
    /// </summary>
    [Table(Caption = "系统序列")]
    [PrimaryKey("Id")]
    public class SystemSequence
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 名称
        /// </summary>
		[Column(Caption = "名称")]
        public string Name { get; set; }

		/// <summary>
        /// 分类
        /// </summary>
		[Column(Caption = "分类")]
        public string Category { get; set; }

		/// <summary>
        /// 当前值
        /// </summary>
		[Column(Caption = "当前值")]
        public int Value { get; set; }

		/// <summary>
        /// 步长
        /// </summary>
		[Column(Caption = "步长")]
        public int Step { get; set; }

		/// <summary>
        /// 序号长度
        /// </summary>
		[Column(Caption = "序号长度")]
        public int Length { get; set; }

		/// <summary>
        /// 占位符
        /// </summary>
		[Column(Caption = "占位符")]
        public string Placeholder { get; set; }

		/// <summary>
        /// 模板
        /// </summary>
		[Column(Caption = "模板")]
        public string Template { get; set; }

		/// <summary>
        /// 备注
        /// </summary>
		[Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemSequence Clone()
        {
            return (SystemSequence)this.MemberwiseClone();
        }
    }
}
