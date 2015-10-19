// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统参数
    /// </summary>
    [Table(Caption = "系统参数")]
    [PrimaryKey("Id", true)]
    public class SystemParam
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键", Exported = false)]
        public int Id { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Column(Caption = "编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(Caption = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [Column(Caption = "参数值")]
        public string Value { get; set; }

        /// <summary>
        /// 参数默认值
        /// </summary>
        [Column(Caption = "参数默认值")]
        public string DefaultValue { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        [Column(Caption = "分类")]
        public string Category { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemParam Clone()
        {
            return (SystemParam)this.MemberwiseClone();
        }
    }
}
