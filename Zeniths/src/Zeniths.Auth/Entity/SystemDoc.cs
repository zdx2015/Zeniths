// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统文档
    /// </summary>
    [Table(Caption = "系统文档")]
    [PrimaryKey("Id", true)]
    public class SystemDoc
    {
        /// <summary>
        /// 主键
        /// </summary>
        //[Required(ErrorMessage = "请输入主键")]
        //[StringLength(4, ErrorMessage = "主键长度不能超过{1}")]
        [Column(Caption = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        //[Required(ErrorMessage = "请输入标题")]
        //[StringLength(50, ErrorMessage = "标题长度不能超过{1}")]
        [Column(Caption = "标题")]
        public string Name { get; set; }

        /// <summary>
        /// 分类标签
        /// </summary>
        //[StringLength(50, ErrorMessage = "分类长度不能超过{1}")]
        [Column(Caption = "分类标签")]
        public string Tag { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        //[StringLength(-1, ErrorMessage = "内容长度不能超过{1}")]
        [Column(Caption = "内容")]
        public string Contents { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        //[Required(ErrorMessage = "请输入创建日期")]
        //[StringLength(8, ErrorMessage = "创建日期长度不能超过{1}")]
        [Column(Caption = "创建日期")]
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        //[Required(ErrorMessage = "请输入更新日期")]
        //[StringLength(8, ErrorMessage = "更新日期长度不能超过{1}")]
        [Column(Caption = "更新日期")]
        public DateTime ModifyDateTime { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemDoc Clone()
        {
            return (SystemDoc)this.MemberwiseClone();
        }
    }
}
