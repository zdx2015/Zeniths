﻿// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Data.Expressions;
using Zeniths.Data.Expressions.Compiler;

namespace Zeniths.Data
{
    /// <summary>
    /// 构建SQL查询对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class SQLQuery<T> : ISQLinq
    {
        /// <summary>
        /// 构造SQL查询对象
        /// </summary>
        public SQLQuery()
        {
            this.Expressions = new List<Expression>();
            this.OrderByExpressions = new List<OrderByExpression>();
            this.Metadata = EntityMetadata.ForType(typeof(T));
        }

        /// <summary>
        /// 创建SQL查询对象
        /// </summary>
        public static SQLQuery<T> Instance
        {
            get { return new SQLQuery<T>(); }
        }

        /// <summary>
        /// 查询表达式
        /// </summary>
        public List<Expression> Expressions { get; private set; }

        /// <summary>
        /// 查询列
        /// </summary>
        public Expression<Func<T, object>> Selects { get; private set; }

        /// <summary>
        /// 分组列
        /// </summary>
        public Expression<Func<T, object>> GroupBys { get; private set; }

        /// <summary>
        /// 提取记录
        /// </summary>
        public int? TakeRecords { get; private set; }

        /// <summary>
        /// 跳过记录
        /// </summary>
        public int? SkipRecords { get; private set; }

        /// <summary>
        /// 页码索引,从1开始
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 排序表达式
        /// </summary>
        public List<OrderByExpression> OrderByExpressions { get; private set; }

        /// <summary>
        /// 是否去重
        /// </summary>
        private bool? DistinctValue { get; set; }

        private IList<string> SelectColumns { get; set; }
        private EntityMetadata Metadata { get; set; }

        /// <summary>
        /// 转为查询语句结果对象
        /// </summary>
        /// <param name="existingParameterCount">已经存在的参数个数</param>
        /// <returns>返回查询语句结果对象</returns>
        public ISQLinqResult ToResult(int existingParameterCount = 0)
        {
            int _parameterNumber = existingParameterCount;
            var parameters = new Dictionary<string, object>();

            // Get Table / View Name
            var tableName = this.GetTableName();

            //// SELECT
            var selectResult = this.ToSQL_Select(_parameterNumber,parameters);
            _parameterNumber = existingParameterCount + parameters.Count;

            // WHERE
            var whereResult = this.ToSQL_Where(_parameterNumber, parameters);
            _parameterNumber = existingParameterCount + parameters.Count;

            // ORDER BY
            var orderbyResult = this.ToSQL_OrderBy(_parameterNumber, parameters);
            _parameterNumber = existingParameterCount + parameters.Count;

            //Group By 
            var groupbyResult = this.ToSQL_GroupBy(_parameterNumber, parameters);
            //_parameterNumber = existingParameterCount + parameters.Count;
            
            return new SQLinqSelectResult
            {
                Select = selectResult.Select.ToArray(),
                Distinct = this.DistinctValue,
                Take = this.TakeRecords,
                Skip = this.SkipRecords,
                PageIndex = this.PageIndex,
                Table = tableName,
                //Join = join.ToArray(),
                GroupBy = groupbyResult.Select.ToArray(),
                Where = whereResult == null ? null : whereResult.SQL,
                OrderBy = orderbyResult.Select.ToArray(),
                Parameters = parameters
            };
        }
        
        /// <summary>
        /// 去重
        /// </summary>
        /// <param name="distinct">是否去重</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> Distinct(bool distinct = true)
        {
            this.DistinctValue = distinct;
            return this;
        }

        /// <summary>
        /// 提取记录
        /// </summary>
        /// <param name="take">提取记录数</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> Take(int take)
        {
            this.TakeRecords = take;
            return this;
        }

        /// <summary>
        /// 页码索引
        /// </summary>
        /// <param name="pageIndex">页码索引,从1开始</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> Page(int pageIndex)
        {
            //if (this.OrderByExpressions.Count == 0)
            //{
            //    throw new NotSupportedException("请指定排序子句");
            //}
            if (this.TakeRecords == null)
            {
                throw new NotSupportedException("请指定提取记录TakeRecords");
            }
            this.PageIndex = pageIndex;
            this.SkipRecords = (pageIndex - 1) * (TakeRecords.Value);
            return this;
        }

        ///// <summary>
        ///// 跳过记录
        ///// </summary>
        ///// <param name="skip">跳过记录数</param>
        ///// <returns>返回查询对象</returns>
        //public SQLinq<T> Skip(int skip)
        //{
        //    if (this.OrderByExpressions.Count == 0)
        //    {
        //        throw new NotSupportedException("SQLinq: Skip can only be performed if OrderBy is specified.");
        //    }
        //    this.SkipRecords = skip;
        //    return this;
        //}

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <returns>返回查询对象</returns>
        public SQLinqCount<T> Count()
        {
            return new SQLinqCount<T>(this);
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> Where(Expression<Func<T, bool>> expression)
        {
            this.Expressions.Add(expression);
            return this;
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> Where(Expression expression)
        {
            this.Expressions.Add(expression);
            return this;
        }

        /// <summary>
        /// 指定查询列
        /// </summary>
        /// <param name="selector">查询列表达式</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> Select(Expression<Func<T, object>> selector)
        {
            this.Selects = selector;
            return this;
        }

        /// <summary>
        /// 添加查询列
        /// </summary>
        /// <param name="columns">查询列数组</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> Select(params string[] columns)
        {
            if (columns == null || columns.Length == 0) return this;
            if (SelectColumns == null)
            {
                SelectColumns = new List<string>();
            }
            Array.ForEach(columns, p => SelectColumns.Add(p));
            return this;
        }

        /// <summary>
        /// 指定分组列
        /// </summary>
        /// <param name="groupBy">分组列表达式</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> GroupBy(Expression<Func<T, object>> groupBy)
        {
            this.GroupBys = groupBy;
            return this;
        }

        /// <summary>
        /// 添加排序子句
        /// </summary>
        /// <param name="orderName">字段名</param>
        /// <param name="ascending">是否正序</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> OrderBy(string orderName, bool ascending = true)
        {
            return OrderBy(ExpressionHelper.CreatePropertyExpression<T>(orderName), ascending);
        }

        /// <summary>
        /// 添加倒序子句
        /// </summary>
        /// <param name="orderName">字段名</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> OrderByDescending(string orderName)
        {
            return OrderBy(orderName, false);
        }

        /// <summary>
        /// 添加排序子句
        /// </summary>
        /// <param name="keySelector">排序列</param>
        /// <param name="ascending">是否正序</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> OrderBy(Expression<Func<T, object>> keySelector, bool ascending = true)
        {
            this.OrderByExpressions.Add(new OrderByExpression { Expression = keySelector, Ascending = ascending });
            return this;
        }
        
        /// <summary>
        /// 添加倒序子句
        /// </summary>
        /// <param name="keySelector">排序列</param>
        /// <returns>返回查询对象</returns>
        public SQLQuery<T> OrderByDescending(Expression<Func<T, object>> keySelector)
        {
            return OrderBy(keySelector,false);
        }

        private string GetTableName()
        {
            return Metadata.TableInfo.TableName;
        }

        private SqlExpressionCompilerResult ToSQL_Where(int parameterNumber, IDictionary<string, object> parameters)
        {
            SqlExpressionCompilerResult whereResult = null;
            if (this.Expressions.Count > 0)
            {
                whereResult = SqlExpressionCompiler.Compile(parameterNumber,this.Expressions);
                foreach (var item in whereResult.Parameters)
                {
                    parameters.Add(item.Key, item.Value);
                }
            }
            return whereResult;
        }

        private SqlExpressionCompilerSelectorResult ToSQL_Select(int parameterNumber, IDictionary<string, object> parameters)
        {
            var selectResult = SqlExpressionCompiler.CompileSelector(parameterNumber,this.Selects);
            foreach (var item in selectResult.Parameters)
            {
                parameters.Add(item.Key, item.Value);
            }
            if (SelectColumns != null && SelectColumns.Count > 0)
            {
                SelectColumns.ForEach(p => selectResult.Select.Add(p));
            }
            if (selectResult.Select.Count == 0)
            {
                Array.ForEach(Metadata.QueryColumns, p => selectResult.Select.Add(p));
            }
            return selectResult;
        }

        private SqlExpressionCompilerSelectorResult ToSQL_GroupBy(int parameterNumber, IDictionary<string, object> parameters)
        {
            var selectResult = SqlExpressionCompiler.CompileSelector(parameterNumber, this.GroupBys);
            foreach (var item in selectResult.Parameters)
            {
                parameters.Add(item.Key, item.Value);
            }
            return selectResult;
        }

        private SqlExpressionCompilerSelectorResult ToSQL_OrderBy(int parameterNumber, IDictionary<string, object> parameters)
        {
            var orderbyResult = new SqlExpressionCompilerSelectorResult();

            for (var i = 0; i < this.OrderByExpressions.Count; i++)
            {
                var r = SqlExpressionCompiler.CompileSelector(parameterNumber,this.OrderByExpressions[i].Expression);
                foreach (var s in r.Select)
                {
                    orderbyResult.Select.Add(s);
                }
                foreach (var p in r.Parameters)
                {
                    orderbyResult.Parameters.Add(p.Key, p.Value);
                }
            }
            foreach (var item in orderbyResult.Parameters)
            {
                parameters.Add(item.Key, item.Value);
            }
            for (var i = 0; i < this.OrderByExpressions.Count; i++)
            {
                if (!this.OrderByExpressions[i].Ascending)
                {
                    orderbyResult.Select[i] = orderbyResult.Select[i] + " DESC";
                }
            }

            return orderbyResult;
        }

        public class OrderByExpression
        {
            public Expression<Func<T, object>> Expression { get; set; }
            public bool Ascending { get; set; }
        }
    }
}
