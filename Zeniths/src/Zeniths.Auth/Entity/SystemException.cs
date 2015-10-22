// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统异常
    /// </summary>
    [Table(Caption = "系统异常")]
    [PrimaryKey("Id", true)]
    public class SystemException
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [Column(Caption = "消息")]
        public string Message { get; set; }

        /// <summary>
        /// 详细信息
        /// </summary>
        [Column(Caption = "详细信息")]
        public string Details { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Column(Caption = "IP地址")]
        public string IPAddress { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column(Caption = "创建时间")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemException Clone()
        {
            return (SystemException)this.MemberwiseClone();
        }
    }
}
