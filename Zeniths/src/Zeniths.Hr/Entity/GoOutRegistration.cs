// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 外出登记
    /// </summary>
    [Table(Caption = "外出登记")]
    [PrimaryKey("Id", true)]
    public class GoOutRegistration
    {
        /// <summary>
        /// Id
        /// </summary>
        [Column(Caption = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// 员工Id
        /// </summary>
        [Column(Caption = "员工Id")]
        public int EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [Column(Caption = "员工姓名")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// 外出事由
        /// </summary>
        [Column(Caption = "外出事由")]
        public string GoOutReason { get; set; }

        /// <summary>
        /// 外出时间
        /// </summary>
        [Column(Caption = "外出时间")]
        public DateTime? GoOutTime { get; set; }

        /// <summary>
        /// 预计返回时间
        /// </summary>
        [Column(Caption = "预计返回时间")]
        public DateTime? PlanBackTime { get; set; }

        /// <summary>
        /// 实际返回时间
        /// </summary>
        [Column(Caption = "实际返回时间")]
        public DateTime? RealBackTime { get; set; }

        /// <summary>
        /// 主管领导意见
        /// </summary>
        [Column(Caption = "主管领导意见")]
        public string LeadershipOpinion { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        [Column(Caption = "申请日期")]
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 权限用户Id
        /// </summary>
        [Column(Caption = "权限用户Id")]
        public int AuthUserId { get; set; }

        /// <summary>
        /// 权限部门Id
        /// </summary>
        [Column(Caption = "权限部门Id")]
        public int AuthDepartmentId { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public GoOutRegistration Clone()
        {
            return (GoOutRegistration)this.MemberwiseClone();
        }
    }
}
