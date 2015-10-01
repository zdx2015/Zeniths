// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System.Collections.Generic;

namespace Zeniths.Collections
{
    /// <summary>
    /// 分页列表
    /// </summary>
    /// <typeparam name="T">元素类型</typeparam>
    public class PageList<T> : List<T>
    {
        private readonly PagingCollection pagingCollection;

        /// <summary>
        /// 页码索引
        /// </summary>
        public int PageIndex { get { return pagingCollection.PageIndex; } }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get { return pagingCollection.PageSize; } }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get { return pagingCollection.TotalCount; } }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get { return pagingCollection.TotalPages; } }

        /// <summary>
        /// 记录开始数
        /// </summary>
        public int RecordStartIndex { get { return pagingCollection.RecordStartIndex; } }

        /// <summary>
        /// 记录结束数
        /// </summary>
        public int RecordEndIndex { get { return pagingCollection.RecordEndIndex; } }

        /// <summary>
        /// 是否首页
        /// </summary>
        public bool IsFirst
        {
            get { return pagingCollection.IsFirst; }
        }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage
        {
            get { return pagingCollection.HasPreviousPage; }
        }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return pagingCollection.HasNextPage; }
        }

        /// <summary>
        /// 是否最后一页
        /// </summary>
        public bool IsLast
        {
            get { return pagingCollection.IsLast; }
        }

        /// <summary>
        /// 构造分页列表
        /// </summary>
        /// <param name="pageIndex">页码索引,从1开始</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="source">当前页的列表对象</param>
        public PageList(int pageIndex, int pageSize, int totalCount, IEnumerable<T> source)
        {
            pagingCollection = new PagingCollection(pageIndex,pageSize,totalCount);
            if (source != null)
            {
                AddRange(source);
            }
        }

        /// <summary>
        /// 构造分页列表
        /// </summary>
        /// <param name="page">分页集合对象</param>
        /// <param name="source">当前页的列表对象</param>
        public PageList(PagingCollection page, IEnumerable<T> source)
        {
            this.pagingCollection = page;
            if (source != null)
            {
                AddRange(source);
            }
        }
    }
}