// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 上传文件表
    /// </summary>
    [Table(Caption = "上传文件表")]
    [PrimaryKey("Id")]
    public class SystemFile
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        [Column(Caption = "文件名称")]
        public string Name { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [Column(Caption = "文件路径")]
        public string Url { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        [Column(Caption = "文件大小")]
        public long Size { get; set; }

        /// <summary>
        /// 资源主键
        /// </summary>
        [Column(Caption = "资源主键")]
        public string ResourceId { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        [Column(Caption = "资源名称")]
        public string ResourceName { get; set; }

        /// <summary>
        /// 操作用户主键
        /// </summary>
        [Column(Caption = "操作用户主键")]
        public string CreateUserId { get; set; }

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
        public SystemFile Clone()
        {
            return (SystemFile)this.MemberwiseClone();
        }
    }
}
