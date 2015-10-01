// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Data;

namespace Zeniths.Extensions
{
    /// <summary>
    /// DataTable扩展操作
    /// </summary>
    public static class DataTableExtensions
    {
        /// <summary>
        /// 对数据表的每行执行指定操作
        /// </summary>
        /// <param name="table">DataTable对象</param>
        /// <param name="action">执行的操作</param>
        /// <exception cref="System.ArgumentNullException">参数action 为null</exception>
        public static void ForEach(this DataTable table, Action<DataRow> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "参数action不能为null");
            }
            foreach (DataRow row in table.Rows)
            {
                action(row);
            }
        }

        /// <summary>
        /// 对数据表的每行执行指定操作
        /// </summary>
        /// <param name="table">DataTable对象</param>
        /// <param name="action">执行的操作</param>
        /// <exception cref="System.ArgumentNullException">参数action 为null</exception>
        public static void ForEach(this DataTable table, Action<int,DataRow> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "参数action不能为null");
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                action(i, row);
            }
        }
    }
}