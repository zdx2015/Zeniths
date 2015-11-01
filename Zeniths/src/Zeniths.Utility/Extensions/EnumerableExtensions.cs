// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Zeniths.Helper;

namespace Zeniths.Extensions
{
    /// <summary>
    /// Enumerable扩展操作
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// 复制实体列表(创建新列表,并且新创建每个元素)
        /// </summary>
        /// <param name="list">列表对象</param>
        /// <param name="filterPredicate">过滤的操作,如果为空则复制全部</param>
        /// <exception cref="System.ArgumentNullException">参数action 为null</exception>
        public static List<TSource> Copy<TSource>(this IEnumerable<TSource> list, Func<TSource, bool> filterPredicate) where TSource : class, new()
        {
            List<TSource> newList = new List<TSource>();
            foreach (TSource item in list)
            {
                if (filterPredicate != null && !filterPredicate(item))
                {
                    continue;
                }
                var newItem = new TSource();
                ObjectHelper.CopyProperty(item, newItem);
                newList.Add(newItem);
            }
            return newList;
        }

        /// <summary>
        /// 对列表的每个元素执行指定操作
        /// </summary>
        /// <param name="list">列表对象</param>
        /// <param name="action">执行的操作</param>
        /// <exception cref="System.ArgumentNullException">参数action 为null</exception>
        public static void ForEach<TSource>(this IEnumerable<TSource> list, Action<TSource> action)
        {
            if (action == null)
            {
                throw new System.ArgumentNullException("action", "参数action不能为null");
            }
            foreach (TSource item in list)
            {
                action(item);
            }
        }

        #region 公共方法

        /// <summary>
        /// 确定某元素是否在列表中。
        /// </summary>
        /// <param name="list">列表对象</param>
        /// <param name="predicate">查找条件。</param>
        /// <exception cref="System.ArgumentNullException">predicate 为null。</exception>
        /// <returns>如果在列表中找到项，则为true，否则为false。</returns>
        public static bool Contains<TSource>(this IEnumerable<TSource> list, Func<TSource, bool> predicate)
        {
            CheckPredicate(predicate);

            foreach (var item in list)
            {
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 用指定条件搜索整个列表，并返回整个列表中第一个匹配项的从零开始的索引。
        /// </summary>
        /// <param name="list">列表对象</param>
        /// <param name="predicate">查找条件。</param>
        /// <exception cref="System.ArgumentNullException">predicate 为null。</exception>
        /// <returns>如果在整个列表中找到元素的第一个匹配项，则为该项的从零开始的索引；否则为-1。</returns>
        public static int IndexOf<TSource>(this IEnumerable<TSource> list, Func<TSource, bool> predicate)
        {
            CheckPredicate(predicate);

            int i = 0;
            foreach (TSource item in list)
            {
                if (predicate(item))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        /// <summary>
        /// 从列表中删除指定查询条件的匹配项。
        /// </summary>
        /// <param name="list">列表对象</param>
        /// <param name="predicate">查找条件。</param>
        /// <exception cref="System.ArgumentNullException">predicate 为null。</exception>
        /// <returns>返回移除的数量。</returns>
        public static int Delete<TSource>(this IList<TSource> list, Func<TSource, bool> predicate)
        {
            CheckPredicate(predicate);

            int removeCount = 0;
            var newlist = new List<TSource>();
            foreach (var item in list)
            {
                if (predicate(item))
                {
                    newlist.Add(item);
                }
            }

            foreach (TSource item in newlist)
            {
                list.Remove(item);
                removeCount++;
            }
            return removeCount;
        }

        /// <summary>
        /// 转为DataTable
        /// </summary>
        /// <typeparam name="TSource">实体类型</typeparam>
        /// <param name="list">实体列表</param>
        public static DataTable ToDataTable<TSource>(this List<TSource> list) where TSource : class, new()
        {
            return DataTableHelper.ConvertToDataTable(list);
        }

        public static string ToSplitString(this IList<string> strList, string split = ",")
        {
            StringBuilder sb = new StringBuilder(strList.Count * 10);
            for (int i = 0; i < strList.Count; i++)
            {
                sb.Append(strList[i]);
                if (i < strList.Count - 1)
                {
                    sb.Append(split);
                }
            }
            return sb.ToString();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 检查查找条件，为null抛出异常。
        /// </summary>
        /// <param name="predicate">查找条件。</param>
        private static void CheckPredicate<TSource>(Func<TSource, bool> predicate)
        {
            if (predicate == null)
            {
                throw new System.ArgumentNullException("predicate", "参数predicate不能为null。");
            }
        }

        #endregion
    }
}