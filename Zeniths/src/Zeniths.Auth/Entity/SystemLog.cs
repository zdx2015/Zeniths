// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统日志
    /// </summary>
    [Table(Caption = "系统日志")]
    [PrimaryKey("Id", true)]
    public class SystemLog
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Column(Caption = "级别")]
        public string LogLevel { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [Column(Caption = "类别")]
        public string Tag { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Column(Caption = "内容")]
        public string Message { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Column(Caption = "IP地址")]
        public string IPAddress { get; set; }

        /// <summary>
        /// 操作用户主键
        /// </summary>
        [Column(Caption = "操作用户主键")]
        public int? CreateUserId { get; set; }

        /// <summary>
        /// 操作用户姓名
        /// </summary>
        [Column(Caption = "操作用户姓名")]
        public string CreateUserName { get; set; }
        
        /// <summary>
        /// 操作时间
        /// </summary>
        [Column(Caption = "操作时间")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemLog Clone()
        {
            return (SystemLog)this.MemberwiseClone();
        }
    }
}
