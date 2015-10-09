using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Helper
{
    /// <summary>
    /// 树结构操作帮助类
    /// </summary>
    public class TreeHelper
    {
        /// <summary>
        /// 构建树形节点
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">数据列表</param>
        /// <param name="getRootHandle">获取根节点函数</param>
        /// <param name="afterCreateNodeHandle">节点创建后函数</param>
        public static List<TreeNode> Build<T>(List<T> data,
            Func<T, bool> getRootHandle = null, 
            Action<TreeNode, T> afterCreateNodeHandle = null)
        {
            var nodes = new List<TreeNode>();
            var rootDataList = getRootHandle == null ? data : data.Where(getRootHandle).ToList();

            var meta = EntityMetadata.ForType(typeof(T));
            var idField = meta.TableInfo.PrimaryKey;
            var parentIdField = meta.TableInfo.ParentKey;
            var textField = meta.TableInfo.TextKey;
            Func<T, string> idHandle = p => ObjectHelper.GetObjectValue(p, idField).ToStringOrEmpty();
            Func<T, string> parentIdHandle = p => ObjectHelper.GetObjectValue(p, parentIdField).ToStringOrEmpty();
            Func<T, string> textHandle = p => ObjectHelper.GetObjectValue(p, textField).ToStringOrEmpty();

            foreach (T item in rootDataList)
            {
                var node = CreateNode(item, idHandle, parentIdHandle, textHandle);
                AddChildNode(data, node, item, idHandle, parentIdHandle, textHandle,afterCreateNodeHandle);
                nodes.Add(node);
            }
            return nodes;
        }

        /// <summary>
        /// 构建树形节点
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="data">数据列表</param>
        /// <param name="getRootHandle">获取根节点函数</param>
        /// <param name="afterCreateNodeHandle">节点创建后函数</param>
        public static List<TreeNode> AsyncBuild<T>(List<T> data,
            Func<T, bool> getRootHandle = null,
            Action<TreeNode, T> afterCreateNodeHandle = null)
        {
            var nodes = new List<TreeNode>();
            var rootDataList = getRootHandle == null ? data : data.Where(getRootHandle).ToList();

            var meta = EntityMetadata.ForType(typeof(T));
            var idField = meta.TableInfo.PrimaryKey;
            var parentIdField = meta.TableInfo.ParentKey;
            var textField = meta.TableInfo.TextKey;
            Func<T, string> idHandle = p => ObjectHelper.GetObjectValue(p, idField).ToStringOrEmpty();
            Func<T, string> parentIdHandle = p => ObjectHelper.GetObjectValue(p, parentIdField).ToStringOrEmpty();
            Func<T, string> textHandle = p => ObjectHelper.GetObjectValue(p, textField).ToStringOrEmpty();

            foreach (T item in rootDataList)
            {
                var node = CreateNode(item, idHandle, parentIdHandle, textHandle);
                afterCreateNodeHandle?.Invoke(node, item);
                nodes.Add(node);
            }
            return nodes;
        }

        private static void AddChildNode<T>(List<T> data, TreeNode currentNode,
            T currentInstance, Func<T, string> idHandle,
            Func<T, string> parentIdHandle, Func<T, string> textHandle,
            Action<TreeNode, T> afterCreateNodeHandle)
        {
            var childs = data.Where(p => idHandle(currentInstance).Equals(parentIdHandle(p)));
            foreach (T item in childs)
            {
                var childNode = CreateNode(item, idHandle, parentIdHandle, textHandle);
                AddChildNode(data, childNode, item, idHandle, parentIdHandle, textHandle, afterCreateNodeHandle);
                if (currentNode.Children == null)
                {
                    currentNode.Children = new List<TreeNode>();
                }
                currentNode.Children.Add(childNode);
            }
            afterCreateNodeHandle?.Invoke(currentNode, currentInstance);
        }

        private static TreeNode CreateNode<T>(T instance, Func<T, string> idHandle,
            Func<T, string> parentIdHandle, Func<T, string> textHandle)
        {
            var node = new TreeNode();
            node.Id = idHandle(instance);
            if (parentIdHandle != null)
            {
                node.ParentId = parentIdHandle(instance);
            }
            node.Text = textHandle(instance);
            return node;
        }
    }
}