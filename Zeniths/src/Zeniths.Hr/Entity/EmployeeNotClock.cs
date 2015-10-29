// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 未打卡记录表
    /// </summary>
    [Table(Caption = "未打卡记录表")]
    [PrimaryKey("Id", true)]
    public class EmployeeNotClock
    {
		/// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 员工主键
        /// </summary>
		[Column(Caption = "员工主键")]
        public int EmployeeId { get; set; }

		/// <summary>
        /// 员工姓名
        /// </summary>
		[Column(Caption = "员工姓名")]
        public string EmployeeName { get; set; }

		/// <summary>
        /// 未打卡日期
        /// </summary>
		[Column(Caption = "未打卡日期")]
        public DateTime? NotClockDate { get; set; }

		/// <summary>
        /// 未打卡班次1上午2下午
        /// </summary>
		[Column(Caption = "未打卡班次1上午2下午")]
        public int? NotClockClasses { get; set; }

		/// <summary>
        /// 事由
        /// </summary>
		[Column(Caption = "事由")]
        public string Reason { get; set; }

		/// <summary>
        /// 创建用户主键
        /// </summary>
		[Column(Caption = "创建用户主键")]
        public int? CreateUserId { get; set; }

		/// <summary>
        /// 创建用户姓名
        /// </summary>
		[Column(Caption = "创建用户姓名")]
        public string CreateUserName { get; set; }

		/// <summary>
        /// 创建人部门主键
        /// </summary>
		[Column(Caption = "创建人部门主键")]
        public int? CreateDepartmentId { get; set; }

		/// <summary>
        /// 创建人部门姓名
        /// </summary>
		[Column(Caption = "创建人部门姓名")]
        public string CreateDepartmentName { get; set; }

		/// <summary>
        /// 创建时间
        /// </summary>
		[Column(Caption = "创建时间")]
        public DateTime? CreateDateTime { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public EmployeeNotClock Clone()
        {
            return (EmployeeNotClock)this.MemberwiseClone();
        }
    }
}
