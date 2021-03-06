﻿// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
namespace Zeniths.Data.Extensions
{
    /// <summary>
    /// 查询语句扩展
    /// </summary>
    public static class SQLinqExtensions
    {
        /// <summary>
        /// Between查询
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="start">开始值</param>
        /// <param name="end">结束值</param>
        public static bool Between(this object column, object start, object end)
        {
            return true;
        }

        /// <summary>
        /// In查询
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="values">列值</param>
        public static bool In(this object column, params string[] values)
        {
            return true;
        }

        /// <summary>
        /// In查询
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="values">列值</param>
        public static bool In(this object column, params int[] values)
        {
            return true;
        }

        /// <summary>
        /// NotIn查询
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="values">列值</param>
        public static bool NotIn(this object column, params string[] values)
        {
            return true;
        }

        /// <summary>
        /// NotIn查询
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="values">列值</param>
        public static bool NotIn(this object column, params int[] values)
        {
            return true;
        }
    }
}