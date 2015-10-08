// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Zeniths.Entity;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [Table(Caption = "系统菜单")]
    [PrimaryKey("Id", true)]
    [ParentKey("ParentId")]
    [SortPath("SortPath")]
    public class SystemMenu
    {
        /// <summary>
        /// 主键
        /// </summary>
        //[Required(ErrorMessage = "请输入主键")]
        //[StringLength(4, ErrorMessage = "主键长度不能超过{1}")]
        [Column(Caption = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        //[Required(ErrorMessage = "请输入父级主键")]
        //[StringLength(4, ErrorMessage = "父级主键长度不能超过{1}")]
        [Column(Caption = "父级主键")]
        public int ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        //[Required(ErrorMessage = "请输入编码")]
        //[StringLength(100, ErrorMessage = "编码长度不能超过{1}")]
        [Column(Caption = "编码")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        //[Required(ErrorMessage = "请输入名称")]
        //[StringLength(100, ErrorMessage = "名称长度不能超过{1}")]
        [Column(Caption = "名称")]
        public string Name { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        //[StringLength(100, ErrorMessage = "简拼长度不能超过{1}")]
        [Column(Caption = "简拼")]
        public string NameSpell { get; set; }

        /// <summary>
        /// 排序路径
        /// </summary>
        //[Required(ErrorMessage = "请输入排序路径")]
        //[StringLength(1000, ErrorMessage = "排序路径长度不能超过{1}")]
        [Column(Caption = "排序路径")]
        public string SortPath { get; set; }

        /// <summary>
        /// Web导航地址
        /// </summary>
        //[StringLength(500, ErrorMessage = "Web导航地址长度不能超过{1}")]
        [Column(Caption = "Web导航地址")]
        public string WebUrl { get; set; }

        /// <summary>
        /// Web大图标样式
        /// </summary>
        //[StringLength(100, ErrorMessage = "Web大图标样式长度不能超过{1}")]
        [Column(Caption = "Web大图标样式")]
        public string WebCls { get; set; }

        /// <summary>
        /// Web打开方式
        /// </summary>
        //[StringLength(100, ErrorMessage = "Web打开方式长度不能超过{1}")]
        [Column(Caption = "Web打开方式")]
        public string WebOpenMode { get; set; }

        /// <summary>
        /// Win窗口类名
        /// </summary>
        //[StringLength(500, ErrorMessage = "Win窗口类名长度不能超过{1}")]
        [Column(Caption = "Win窗口类名")]
        public string WinFormProvider { get; set; }

        /// <summary>
        /// Win对话框显示
        /// </summary>
        //[Required(ErrorMessage = "请输入Win对话框显示")]
        //[StringLength(1, ErrorMessage = "Win对话框显示长度不能超过{1}")]
        [Column(Caption = "Win对话框显示")]
        public bool WinShowDialog { get; set; }

        /// <summary>
        /// Win大图标名称
        /// </summary>
        //[StringLength(100, ErrorMessage = "Win大图标名称长度不能超过{1}")]
        [Column(Caption = "Win大图标名称")]
        public string WinLargeIconName { get; set; }

        /// <summary>
        /// Win小图标名称
        /// </summary>
        //[StringLength(100, ErrorMessage = "Win小图标名称长度不能超过{1}")]
        [Column(Caption = "Win小图标名称")]
        public string WinSmallIconName { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        //[Required(ErrorMessage = "请输入是否展开")]
        //[StringLength(1, ErrorMessage = "是否展开长度不能超过{1}")]
        [Column(Caption = "是否展开")]
        public bool IsExpand { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        //[Required(ErrorMessage = "请输入是否公开")]
        //[StringLength(1, ErrorMessage = "是否公开长度不能超过{1}")]
        [Column(Caption = "是否公开")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        //[Required(ErrorMessage = "请输入是否启用")]
        //[StringLength(1, ErrorMessage = "是否启用长度不能超过{1}")]
        [Column(Caption = "是否启用")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        //[StringLength(1000, ErrorMessage = "备注长度不能超过{1}")]
        [Column(Caption = "备注")]
        public string Note { get; set; }

        /// <summary>
        /// 复制对象
        /// </summary>
        public SystemMenu Clone()
        {
            return (SystemMenu)this.MemberwiseClone();
        }
    }
}
