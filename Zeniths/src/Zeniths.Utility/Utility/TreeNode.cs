using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zeniths.Utility
{
    /// <summary>
    /// 数树节点
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// 绑定节点的标识值
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 显示的节点文本
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// 显示的节点图标CSS类ID
        /// </summary>
        [JsonProperty("iconCls")]
        public string IconCls { get; set; }

        /// <summary>
        /// 该节点是否被选中
        /// </summary>
        [JsonProperty("checked")]
        public bool Checked { get; set; }

        /// <summary>
        /// 节点状态，'open' 或 'closed'
        /// </summary>
        [JsonProperty("state")]
        public TreeNodeState State { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        [JsonProperty("children")]
        public List<TreeNode> Children { get; set; }

        /// <summary>
        /// 绑定该节点的自定义属性
        /// </summary>
        [JsonProperty("attributes")]
        public Dictionary<string, string> Attributes { get; set; }
    }

    /// <summary>
    /// 节点展开状态
    /// </summary>
    public enum TreeNodeState
    {
        /// <summary>
        /// 展开
        /// </summary>
        [JsonProperty("open")]
        Open,

        /// <summary>
        /// 不展开
        /// </summary>
        [JsonProperty("closed")]
        Closed
    }
}