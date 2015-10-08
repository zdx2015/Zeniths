namespace Zeniths.Utility
{
    /// <summary>
    /// 树节点接口
    /// </summary>
    public interface ITreeNode
    {
        /// <summary>
        /// 获取节点Id
        /// </summary>
        string GetTreeNodeId();

        /// <summary>
        /// 获取父节点Id
        /// </summary>
        string GetTreeNodeParentId();

        /// <summary>
        /// 获取节点文本
        /// </summary>
        string GetTreeNodeText();
        
        /// <summary>
        /// 创建节点
        /// </summary>
        /// <returns>返回新创建的节点</returns>
        TreeNode CreateTreeNode();
    }
}