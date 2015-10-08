using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Utility;

namespace Zeniths.Helper
{
    /// <summary>
    /// 树型结构操作帮助类
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// 构建树形节点
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">数据列表</param>
        /// <param name="rootHandle">获取根节点回调函数</param>
        public static List<TreeNode> BuildNodes<T>(List<T> data, Func<T, bool> rootHandle) where T : ITreeNode
        {
            var nodes = new List<TreeNode>();
            var datas = rootHandle == null ? data : data.Where(rootHandle);
            foreach (T item in datas)
            {
                var node = item.CreateTreeNode();
                AddChildNode(data,node, item);
                nodes.Add(node);
            }
            return nodes;
        }

        private static void AddChildNode<T>(List<T> data, TreeNode currentNode,T currentData) where T : ITreeNode
        {
            var childs = data.Where(p => currentData.GetTreeNodeId().Equals(p.GetTreeNodeParentId()));
            foreach (T item in childs)
            {
                var childNode = item.CreateTreeNode();
                AddChildNode(data, childNode, item);
                if (currentNode.Children == null)
                {
                    currentNode.Children = new List<TreeNode>();
                }
                currentNode.Children.Add(childNode);
            }
        }
    }
}