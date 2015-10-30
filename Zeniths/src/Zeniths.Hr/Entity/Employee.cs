// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Hr.Entity
{
    /// <summary>
    /// 员工表
    /// </summary>
    [Table(Caption = "员工表")]
    [PrimaryKey("Id", true)]
    public class Employee
    {
		/// <summary>
        /// Id
        /// </summary>
        [Column(Caption = "Id", Exported = false)]
        public int Id { get; set; }

		/// <summary>
        /// 工号
        /// </summary>
		[Column(Caption = "工号")]
        public string WorkNumber { get; set; }

		/// <summary>
        /// 考勤编号
        /// </summary>
		[Column(Caption = "考勤编号")]
        public string CardNumber { get; set; }

		/// <summary>
        /// 用户主键
        /// </summary>
		[Column(Caption = "用户主键")]
        public int UserId { get; set; }

		/// <summary>
        /// 姓名
        /// </summary>
		[Column(Caption = "姓名")]
        public string Name { get; set; }

		/// <summary>
        /// 职级
        /// </summary>
		[Column(Caption = "职级")]
        public string Rank { get; set; }

		/// <summary>
        /// 职系
        /// </summary>
		[Column(Caption = "职系")]
        public string Grade { get; set; }

		/// <summary>
        /// 照片
        /// </summary>
		[Column(Caption = "照片")]
        public string Picture { get; set; }

		/// <summary>
        /// 部门主键
        /// </summary>
		[Column(Caption = "部门主键")]
        public int DepartmentId { get; set; }

		/// <summary>
        /// 部门名称
        /// </summary>
		[Column(Caption = "部门名称")]
        public string Department { get; set; }

		/// <summary>
        /// 岗位
        /// </summary>
		[Column(Caption = "岗位")]
        public string Post { get; set; }

		/// <summary>
        /// 入职时间
        /// </summary>
		[Column(Caption = "入职时间")]
        public DateTime? EntryDateTime { get; set; }

		/// <summary>
        /// 转正时间
        /// </summary>
		[Column(Caption = "转正时间")]
        public DateTime? PositiveDateTime { get; set; }

		/// <summary>
        /// 性别
        /// </summary>
		[Column(Caption = "性别")]
        public string Gender { get; set; }

		/// <summary>
        /// 出生日期
        /// </summary>
		[Column(Caption = "出生日期")]
        public DateTime? BirthDate { get; set; }

		/// <summary>
        /// 学历
        /// </summary>
		[Column(Caption = "学历")]
        public string Education { get; set; }

		/// <summary>
        /// 工龄
        /// </summary>
		[Column(Caption = "工龄")]
        public int? WorkAge { get; set; }

		/// <summary>
        /// 执/职业资格
        /// </summary>
		[Column(Caption = "执/职业资格")]
        public string ProfessionalQualification { get; set; }

		/// <summary>
        /// 职称
        /// </summary>
		[Column(Caption = "职称")]
        public string JobTitle { get; set; }

		/// <summary>
        /// 婚否
        /// </summary>
		[Column(Caption = "婚否")]
        public string Marriage { get; set; }

		/// <summary>
        /// 政治面貌
        /// </summary>
		[Column(Caption = "政治面貌")]
        public string PoliticsStatus { get; set; }

		/// <summary>
        /// 籍贯
        /// </summary>
		[Column(Caption = "籍贯")]
        public string NativePlace { get; set; }

		/// <summary>
        /// 户口所在地
        /// </summary>
		[Column(Caption = "户口所在地")]
        public string DomicilePlace { get; set; }

		/// <summary>
        /// 户口类型
        /// </summary>
		[Column(Caption = "户口类型")]
        public string DomicileType { get; set; }

		/// <summary>
        /// 毕业院校
        /// </summary>
		[Column(Caption = "毕业院校")]
        public string GraduateSchool { get; set; }

		/// <summary>
        /// 专业
        /// </summary>
		[Column(Caption = "专业")]
        public string Major { get; set; }

		/// <summary>
        /// 毕业时间
        /// </summary>
		[Column(Caption = "毕业时间")]
        public DateTime? GraduateTime { get; set; }

		/// <summary>
        /// 手机
        /// </summary>
		[Column(Caption = "手机")]
        public string Tel { get; set; }

		/// <summary>
        /// QQ
        /// </summary>
		[Column(Caption = "QQ")]
        public string QQ { get; set; }

		/// <summary>
        /// 公司邮箱
        /// </summary>
		[Column(Caption = "公司邮箱")]
        public string CompanyMail { get; set; }

		/// <summary>
        /// 身份证号
        /// </summary>
		[Column(Caption = "身份证号")]
        public string IdNumber { get; set; }

		/// <summary>
        /// 家庭详细地址
        /// </summary>
		[Column(Caption = "家庭详细地址")]
        public string Address { get; set; }

		/// <summary>
        /// 紧急联系人
        /// </summary>
		[Column(Caption = "紧急联系人")]
        public string EmergencyContact { get; set; }

		/// <summary>
        /// 与紧急联系人关系
        /// </summary>
		[Column(Caption = "与紧急联系人关系")]
        public string EmergercyRelation { get; set; }

		/// <summary>
        /// 联系方式
        /// </summary>
		[Column(Caption = "联系方式")]
        public string Contact { get; set; }

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
        public Employee Clone()
        {
            return (Employee)this.MemberwiseClone();
        }
    }
}
