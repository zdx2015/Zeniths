﻿// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System.Data;
using System.Data.Common;
using Zeniths.Entity;

namespace Zeniths.Data
{
    /// <summary>
    /// 数据库引擎接口
    /// </summary>
    public interface IDbProvider
    {
        /// <summary>
        /// 数据对象创建工厂
        /// </summary>
        DbProviderFactory ProviderFactory { get; }

        /// <summary>
        /// 参数前缀
        /// </summary>
        string ParameterPrefix { get; }

        /// <summary>
        /// 获取Exists语句模板
        /// </summary>
        /// <returns>获取Exists语句模板</returns>
        string GetExistsStatement();

        /// <summary>
        /// 获取Insert语句模板
        /// </summary>
        /// <param name="tableInfo">表信息</param>
        /// <param name="cmd">Insert命令对象</param>
        void PreExecuteInsert(TableInfo tableInfo, DbCommand cmd);

        /// <summary>
        /// 获取分页语句模板
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="orderBy"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns>获取分页语句模板</returns>
        string GetPageStatement(string sql, string orderBy, int startIndex, int endIndex);

        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <param name="value">参数值</param>
        /// <returns>返回数据库对应的数据类型</returns>
        object ConvertParameterValue(object value);

        /// <summary>
        /// 执行命令之前调用,可以修改DbCommand对象
        /// </summary>
        /// <param name="cmd">命令对象</param>
        void PreExecute(IDbCommand cmd);

        /// <summary>
        /// 添加游标参数（针对Oracle数据库）
        /// </summary>
        /// <param name="param">参数对象</param>
        void SetCursorParam(DbParameter param);
    }
}