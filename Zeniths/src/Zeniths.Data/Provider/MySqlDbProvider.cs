﻿// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System.Data.Common;
using Zeniths.Entity;

namespace Zeniths.Data.Provider
{
    /// <summary>
    /// MySql数据库引擎
    /// </summary>
    public class MySqlDbProvider : DbProvider, IDbProvider
    {
        /// <summary>
        /// MySql默认实例对象
        /// </summary>
        public static readonly MySqlDbProvider Instance = new MySqlDbProvider();

        /// <summary>
        /// 获取Exists语句模板
        /// </summary>
        /// <returns>获取Exists语句模板</returns>
        public override string GetExistsStatement()
        {
            return "SELECT EXISTS (SELECT 1 FROM {0} WHERE {1})";
        }

        /// <summary>
        /// 获取Insert语句模板
        /// </summary>
        /// <param name="tableInfo">表信息</param>
        /// <param name="cmd">Insert命令对象</param>
        public override void PreExecuteInsert(TableInfo tableInfo, DbCommand cmd)
        {
            if (!tableInfo.AutoIncrement) return;
            cmd.CommandText += ";\nSELECT last_insert_id();";
        }

        /// <summary>
        /// 获取分页语句模板
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="orderBy"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns>获取分页语句模板</returns>
        public override string GetPageStatement(string sql, string orderBy, int startIndex, int endIndex)
        {
            return string.Format("{0} {1} LIMIT {2},{3}", sql, orderBy,startIndex, endIndex - startIndex);
        }

        /// <summary>
        /// 获取数据库工厂对象
        /// </summary>
        /// <returns>返回工厂对象</returns>
        protected override DbProviderFactory GetFactory()
        {
            return GetFactory("MySql.Data.MySqlClient.MySqlClientFactory,MySql.Data");
        }
    }
}