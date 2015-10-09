// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using Newtonsoft.Json;
using Zeniths.Entity;
using Zeniths.Utility;

namespace Zeniths.Auth.Entity
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [Table(Caption = "系统菜单")]
    [PrimaryKey("Id", true)]
    [ParentKey("ParentId")]
    [TextKey("Name")]
    [SortPath("SortPath")]
    public class SystemMenu
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Column(Caption = "主键")]
        public int Id { get; set; }

        /// <summary>
        /// 父级主键
        /// </summary>
        [Column(Caption = "父级主键")]
        [JsonProperty("_parentId", NullValueHandling = NullValueHandling.Ignore)]
        public int ParentId { get; set; }

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
        /// 简拼
        /// </summary>
        [Column(Caption = "简拼")]
        public string NameSpell { get; set; }

        /// <summary>
        /// 排序路径
        /// </summary>
        [Column(Caption = "排序路径")]
        public string SortPath { get; set; }

        /// <summary>
        /// Web导航地址
        /// </summary>
        [Column(Caption = "Web导航地址")]
        public string WebUrl { get; set; }

        /// <summary>
        /// Web大图标样式
        /// </summary>
        [Column(Caption = "Web大图标样式")]
        [JsonProperty("iconCls", NullValueHandling = NullValueHandling.Ignore)]
        public string WebCls { get; set; }

        /// <summary>
        /// Web打开方式
        /// </summary>
        [Column(Caption = "Web打开方式")]
        public string WebOpenMode { get; set; }

        /// <summary>
        /// Win窗口类名
        /// </summary>
        [Column(Caption = "Win窗口类名")]
        public string WinFormProvider { get; set; }

        /// <summary>
        /// Win对话框显示
        /// </summary>
        [Column(Caption = "Win对话框显示")]
        public bool WinShowDialog { get; set; }

        /// <summary>
        /// Win大图标名称
        /// </summary>
        [Column(Caption = "Win大图标名称")]
        public string WinLargeIconName { get; set; }

        /// <summary>
        /// Win小图标名称
        /// </summary>
        [Column(Caption = "Win小图标名称")]
        public string WinSmallIconName { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        [Column(Caption = "展开")]
        public bool IsExpand { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        [Column(Caption = "公开")]
        public bool IsPublic { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column(Caption = "启用")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
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
