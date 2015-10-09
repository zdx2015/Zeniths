// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Zeniths.Collections;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;
using Zeniths.Data.Expressions;
using Zeniths.Data.Expressions.Compiler;
using Zeniths.Data.Utilities;

namespace Zeniths.Data
{
    /// <summary>
    /// 数据存储器实现
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class Repository<T> : IRepository<T> where T : class,new()
    {
        #region 字段

        private readonly Database _database;
        private readonly EntityMetadata _metadata;
        private string _tableName;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造数据存储器,创建默认数据库
        /// </summary>
        public Repository()
            : this(new Database())
        {
        }

        /// <summary>
        /// 构造数据存储器,使用指定数据库
        /// </summary>
        /// <param name="database">数据库对象</param>
        public Repository(Database database)
        {
            this._database = database;
            this._metadata = EntityMetadata.ForType(typeof(T));
        }

        #endregion

        #region 属性

        /// <summary>
        /// 数据库对象
        /// </summary>
        public Database Database
        {
            get { return _database; }
        }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        public SQLQuery<T> NewQuery
        {
            get { return new SQLQuery<T>(); }
        }

        /// <summary>
        /// 表名
        /// </summary>
        private string TableName
        {
            get { return _tableName ?? (_tableName = _metadata.TableInfo.TableName); }
        }

        /// <summary>
        /// 实体数据
        /// </summary>
        public EntityMetadata Metadata
        {
            get { return _metadata; }
        }

        #endregion

        #region Exists

        /// <summary>
        /// 是否存在指定条件的记录(参数对象)
        /// </summary>
        /// <param name="whereCondition">查询条件字符串(不带where,例如: name=@name and id=@id)</param>
        /// <param name="args">条件参数对象</param>
        /// <returns>找到符合条件的记录返回true,否则返回false</returns>
        public bool Exists(string whereCondition, object args = null)
        {
            if (string.IsNullOrEmpty(whereCondition))
                throw new ArgumentException("Where查询条件不允许为空");
            var sql = string.Format(Database.Provider.GetExistsStatement(), TableName, whereCondition);
            return Database.ExecuteScalar<int>(sql, args) != 0;
        }

        /// <summary>
        /// 是否存在指定主键值的记录(主键值)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="primaryKey">主键值</param>
        /// <returns>存在返回true,否则返回false</returns>
        public bool Exists(object primaryKey)
        {
            var whereCondition = string.Format("{0}=@{0}", Metadata.TableInfo.PrimaryKey);
            return Exists(whereCondition, new object[] { primaryKey });
        }

        /// <summary>
        /// 是否存在指定条件的记录(查询表达式)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="expression">查询表达式</param>
        /// <returns>存在返回true,否则返回false</returns>
        public bool Exists(Expression<Func<T, bool>> expression)
        {
            var result = SqlExpressionCompiler.Compile(0, new[] { expression });
            return Exists(result.SQL, result.Parameters);
        }

        #endregion

        #region Insert

        /// <summary>
        /// 插入数据(需要写入的列和值对象,支持字典类型和对象类型,不检查主键)
        /// </summary>
        /// <param name="args">参数对象(需要写入的列和值对象,支持字典类型和对象类型,不检查主键字段)</param>
        /// <returns>返回受影响的行数</returns>
        public int Insert(object args)
        {
            var names = new List<string>();
            var values = new List<string>();
            var paramDic = new Dictionary<string, object>();
            var objType = args.GetType();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(args))
            {
                string name = propertyDescriptor.Name;
                names.Add(name);
                values.Add(string.Format("@{0}", name));
                paramDic.Add(name, objType.GetProperty(name).GetValue(args, null));
            }

            var sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                TableName, string.Join(",", names.ToArray()), string.Join(",", values.ToArray()));
            return Database.Execute(sql, paramDic);
        }

        /// <summary>
        /// 插入数据(实体对象,检查主键)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>返回新增后记录的主键值</returns>
        public object Insert(T entity)
        {
            var pd = PocoData.ForType(typeof(T));
            var names = new List<string>();
            var values = new List<string>();
            var autoIncrement = pd.TableInfo.AutoIncrement;
            var primaryKeyName = pd.TableInfo.PrimaryKey;
            var paramDic = new Dictionary<string, object>();
            foreach (var i in pd.Columns)
            {
                if (i.Value.ColumnInfo.ResultColumn)
                    continue;

                if (autoIncrement && !string.IsNullOrEmpty(primaryKeyName)
                    && String.Compare(i.Key, primaryKeyName, StringComparison.OrdinalIgnoreCase) == 0)
                    continue;

                names.Add(i.Key);
                values.Add(string.Format("@{0}", i.Key));
                paramDic.Add(i.Key, i.Value.GetValue(entity));
            }

            var sql = string.Format("INSERT INTO {0} ({1}) VALUES ({2})",
                    pd.TableInfo.TableName, string.Join(",", names.ToArray()),
                    string.Join(",", values.ToArray()));
            object id;
            var cmd = Database.CreateCommand(sql, paramDic);
            Database.Provider.PreExecuteInsert(pd.TableInfo, cmd);
            if (autoIncrement)
            {
                id = Database.ExecuteScalar<object>(cmd);
                pd.SetPrimaryKeyValue(entity, id);
            }
            else
            {
                Database.Execute(cmd);
                id = pd.GetPrimaryKeyValue(entity);
            }
            return id;
        }

        #endregion

        #region Update

        /// <summary>
        /// 更新数据(需要写入的列和值对象,支持字典类型和对象类型)
        /// </summary>
        /// <param name="valueArgs">更新数据参数(需要更新的列和值对象,支持字典类型和对象类型,不检查主键字段)</param>
        /// <param name="whereCondition">查询条件字符串(不带where,例如: name=@name and id=@id)</param>
        /// <param name="args">条件参数对象</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(object valueArgs, string whereCondition, object args = null)
        {
            var paramDic = new Dictionary<string, object>();
            StringBuilder values = new StringBuilder();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(valueArgs))
            {
                string name = propertyDescriptor.Name;
                values.AppendFormat("{0}=@{0},", name);
                paramDic.Add(name, propertyDescriptor.GetValue(valueArgs));
            }
            string sql = string.Format("UPDATE {0} SET {1} ", TableName, values.ToString().TrimEnd(','));

            var args_dest = new List<KeyValuePair<string, object>>();
            whereCondition = ParameterHelper.ParserParameter(whereCondition, args, args_dest);
            foreach (var pair in args_dest)
            {
                paramDic.Add(pair.Key, pair.Value);
            }
            if (!string.IsNullOrEmpty(whereCondition))
            {
                sql += string.Format("WHERE {0}", whereCondition);
            }
            return Database.Execute(sql, paramDic);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="columns">指定需要更新的列名数组,如果为空则更新所有的列</param>
        /// <param name="whereCondition">查询条件字符串(不带where,例如: name=@name and id=@id)</param>
        /// <param name="args">条件参数对象</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(T entity, string[] columns, string whereCondition, object args = null)
        {
            var paramDic = new Dictionary<string, object>();
            StringBuilder values = new StringBuilder();
            Dictionary<string, EntityColumn> updateColumns;
            if (columns == null || columns.Length == 0)
            {
                updateColumns = Metadata.Columns;
            }
            else
            {
                updateColumns = new Dictionary<string, EntityColumn>();
                foreach (var col in columns)
                {
                    var entityCol = Metadata.Columns.Get(col);
                    if (entityCol != null)
                    {
                        updateColumns.Add(col, entityCol);
                    }
                }
            }

            foreach (var item in updateColumns)
            {
                string name = item.Key;
                if (String.Compare(name, Metadata.TableInfo.PrimaryKey, StringComparison.OrdinalIgnoreCase) == 0)
                    continue;

                if (item.Value.ColumnInfo.ResultColumn)
                    continue;

                values.AppendFormat("{0}=@{0},", name);
                paramDic.Add(name, item.Value.GetValue(entity));
            }
            string sql = string.Format("UPDATE {0} SET {1} ", TableName, values.ToString().TrimEnd(','));

            var args_dest = new List<KeyValuePair<string, object>>();
            whereCondition = ParameterHelper.ParserParameter(whereCondition, args, args_dest);
            foreach (var pair in args_dest)
            {
                paramDic.Add(pair.Key, pair.Value);
            }
            if (!string.IsNullOrEmpty(whereCondition))
            {
                sql += string.Format("WHERE {0}", whereCondition);
            }
            return Database.Execute(sql, paramDic);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="columns">指定需要更新的列名数组,如果为空则更新所有的列</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(T entity, string[] columns)
        {
            return Update(entity, columns, string.Format("{0} = @{0}", Metadata.TableInfo.PrimaryKey),
                new object[] { Metadata.GetPrimaryKeyValue(entity) });
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="columns">指定需要更新的列名数组,如果为空则更新所有的列</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(T entity, params Expression<Func<T, object>>[] columns)
        {
            var cols = ExpressionHelper.GetPropertyNameArray(columns);
            return Update(entity, cols);
        }

        /// <summary>
        /// 更新实体数据(查询表达式)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">查询表达式</param>
        /// <param name="columns">指定需要更新的列名数组,如果为空则更新所有的列</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(T entity, Expression<Func<T, bool>> expression, string[] columns)
        {
            var result = SqlExpressionCompiler.Compile(0, new[] { expression });
            return Update(entity, columns, result.SQL, result.Parameters);
        }
        
        /// <summary>
        /// 更新实体数据(查询表达式)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="expression">查询表达式</param>
        /// <param name="columns">指定需要更新的列名数组,如果为空则更新所有的列</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(T entity, Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] columns)
        {
            var cols = ExpressionHelper.GetPropertyNameArray(columns);
            return Update(entity, expression, cols);
        }
        
        /// <summary>
        /// 批量更新(建议外部开启事务)
        /// </summary>
        /// <param name="changedData">改变的数据</param>
        public int BatchUpdate(IEnumerable<PrimaryKeyValue> changedData)
        {
            var affectCount = 0;
            foreach (var item in changedData)
            {
                string sql = string.Format("UPDATE {0} SET {1}=@{1} Where {2}=@{2}",
                    TableName, item.Key, Metadata.TableInfo.PrimaryKey);
                Database.Execute(sql, new object[] { item.Value, item.Id });
            }
            return affectCount;
        }

        #endregion

        #region Delete

        /// <summary>
        /// 删除数据(参数对象)
        /// </summary>
        /// <param name="whereCondition">查询条件字符串(不带where,例如: name=@name and id=@id)</param>
        /// <param name="args">参数对象</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(string whereCondition, object args)
        {
            if (string.IsNullOrEmpty(whereCondition))
                throw new ArgumentException("Where查询条件不允许为空");
            var sql = string.Format("DELETE FROM {0} WHERE {1}", TableName, whereCondition);
            return Database.Execute(sql, args);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(Expression<Func<T, bool>> expression)
        {
            var result = SqlExpressionCompiler.Compile(0, new[] { expression });
            return Delete(result.SQL, result.Parameters);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(Array ids)
        {
            var whereCondition = string.Format("{0} in (@{0})", Metadata.TableInfo.PrimaryKey);
            return Delete(whereCondition, new object[] { ids });
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">整形主键</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(int id)
        {
            var whereCondition = string.Format("{0}=@{0}", Metadata.TableInfo.PrimaryKey);
            return Delete(whereCondition, new object[] { id });
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id">字符串主键</param>
        /// <returns>返回受影响的行数</returns>
        public int Delete(string id)
        {
            var whereCondition = string.Format("{0}=@{0}", Metadata.TableInfo.PrimaryKey);
            return Delete(whereCondition, new object[] { id });
        }

        /// <summary>
        /// 删除所有数据
        /// </summary>
        /// <returns>返回受影响的行数</returns>
        public int DeleteAll()
        {
            var sql = string.Format("DELETE FROM {0}", TableName);
            return Database.Execute(sql);
        }

        #endregion

        #region Get

        /// <summary>
        /// 获取实体对象(主键值)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">主键值</param>
        /// <param name="columns">查询列数组,如果为null查询所有列</param>
        /// <returns>返回实体对象</returns>
        public T Get(object id, string[] columns)
        {
            var pd = PocoData.ForType(typeof(T));
            string columnString;
            if (columns == null || columns.Length == 0)
            {
                columnString = string.Join(",", pd.QueryColumns);
            }
            else
            {
                columnString = string.Join(",", columns);
            }
            string sql = String.Format("SELECT {0} FROM {1} WHERE {2}=@{2}",
                columnString, pd.TableInfo.TableName, pd.TableInfo.PrimaryKey);
            return Database.Query<T>(sql, new object[] { id }).FirstOrDefault();
        }

        /// <summary>
        /// 获取实体对象(主键值)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="id">主键值</param>
        /// <param name="columns">查询列数组,如果为null查询所有列</param>
        /// <returns>返回实体对象</returns>
        public T Get(object id, params Expression<Func<T, object>>[] columns)
        {
            return Get(id, ExpressionHelper.GetPropertyNameArray(columns));
        }

        /// <summary>
        /// 获取实体对象(查询表达式)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="expression">查询表达式</param>
        /// <param name="columns">查询列数组,如果为null查询所有列</param>
        /// <returns>返回实体对象</returns>
        public T Get(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] columns)
        {
            var cols = ExpressionHelper.GetPropertyNameArray(columns);
            return Get(SQLQuery<T>.Instance.Where(expression).Select(cols));
        }

        /// <summary>
        /// 获取实体对象(查询表达式)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="linq">查询表达式</param>
        /// <returns>返回实体对象</returns>
        public T Get(SQLQuery<T> linq)
        {
            var result = linq.ToResult();
            return Database.Query<T>(result.ToSQL(), result.Parameters).FirstOrDefault();
        }

        #endregion

        #region Query

        /// <summary>
        /// 查询数据(默认排序规则)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>返回记录集合</returns>
        public IEnumerable<T> Query()
        {
            return Query(SQLQuery<T>.Instance);
        }

        /// <summary>
        /// 查询数据(查询表达式)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="linq">查询表达式</param>
        /// <returns>返回记录集合</returns>
        public IEnumerable<T> Query(SQLQuery<T> linq)
        {
            var result = linq.ToResult();
            return Database.Query<T>(result.ToSQL(), result.Parameters);
        }
        
        /// <summary>
        /// 查询数据(默认排序规则)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>返回记录集合</returns>
        public IEnumerable<T> Query(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] columns)
        {
            var cols = ExpressionHelper.GetPropertyNameArray(columns);
            var linq = SQLQuery<T>.Instance.Where(expression).Select(cols);
            return Query(linq);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>返回记录集合</returns>
        public List<T> Fetch()
        {
            return Query().ToList();
        }

        /// <summary>
        /// 查询数据(查询表达式)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="linq">查询表达式</param>
        /// <returns>返回记录集合</returns>
        public List<T> Fetch(SQLQuery<T> linq)
        {
            return Query(linq).ToList();
        }

        /// <summary>
        /// 查询数据(默认排序规则)
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="columns">查询列</param>
        /// <returns>返回记录集合</returns>
        public List<T> Fetch(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] columns)
        {
            return Query(expression, columns).ToList();
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>返回记录集合</returns>
        public DataTable GetTable()
        {
            return GetTable(SQLQuery<T>.Instance);
        }

        /// <summary>
        /// 查询数据(查询表达式)
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="linq">查询表达式</param>
        /// <returns>返回记录集合</returns>
        public DataTable GetTable(SQLQuery<T> linq)
        {
            var result = linq.ToResult();
            return Database.ExecuteDataTable(result.ToSQL(), result.Parameters);
        }

        /// <summary>
        /// 查询数据(默认排序规则)
        /// </summary>
        /// <param name="expression">查询表达式</param>
        /// <param name="columns">查询列</param>
        /// <returns>返回记录集合</returns>
        public DataTable GetTable(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] columns)
        {
            var cols = ExpressionHelper.GetPropertyNameArray(columns);
            var linq = SQLQuery<T>.Instance.Where(expression).Select(cols);
            var result = linq.ToResult();
            return Database.ExecuteDataTable(result.ToSQL(), result.Parameters);
        }

        #endregion

        #region Page

        /// <summary>
        /// 查询分页数据
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns>返回分页列表</returns>
        public PageList<T> Page(SQLQuery<T> linq)
        {
            if (linq == null) throw new ArgumentNullException("linq", "查询表达式不允许为空");

            var result = (SQLinqSelectResult)linq.ToResult();
            if (result.Skip == null) throw new ArgumentNullException("linq", "请指定Skip属性");
            if (result.Take == null) throw new ArgumentNullException("linq", "请指定Take属性");

            int startIndex = result.Skip.Value + 1;
            int endIndex = result.Skip.Value + (result.Take.Value);
            var dataSql = new StringBuilder();
            var countSql = new StringBuilder();

            var orderby = SQLinqSelectResult.ConcatFieldArray(result.OrderBy);
            var groupby = SQLinqSelectResult.ConcatFieldArray(result.GroupBy);
            var columnsString = SQLinqSelectResult.ConcatFieldArray(result.Select);

            dataSql.AppendFormat("select {0} from {1}", columnsString, TableName);
            countSql.AppendFormat("select count(1) from {0}", TableName);

            if (!string.IsNullOrEmpty(result.Where))
            {
                dataSql.AppendFormat(" WHERE {0}", result.Where);
                countSql.AppendFormat(" WHERE {0}", result.Where);
            }

            if (!string.IsNullOrEmpty(groupby))
            {
                dataSql.AppendFormat(" GROUP BY {0}", groupby);
                countSql.AppendFormat(" GROUP BY {0}", groupby);
            }

            if (!string.IsNullOrEmpty(result.Having))
            {
                dataSql.AppendFormat(" HAVING {0}", result.Having);
                countSql.AppendFormat(" HAVING {0}", result.Having);
            }

            var orderByString = string.Empty;
            if (orderby.Length > 0)
            {
                orderByString = " ORDER BY " + orderby;
            }

            string sql = Database.Provider.GetPageStatement(dataSql.ToString(), orderByString, startIndex, endIndex);
            var data = Database.Query<T>(sql, result.Parameters);
            int totalCount = Database.ExecuteScalar<int>(countSql.ToString(), result.Parameters);
            return new PageList<T>(result.PageIndex, result.Take.Value, totalCount, data);
        }

        #endregion
    }
}