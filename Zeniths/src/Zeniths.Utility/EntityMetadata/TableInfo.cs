// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;

namespace Zeniths.Entity
{
    /// <summary>
    /// 表信息
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 主键字段名
        /// </summary>
        public string PrimaryKey { get; set; }

        /// <summary>
        /// 父级字段名
        /// </summary>
        public string ParentKey { get; set; }

        /// <summary>
        /// 排序路径
        /// </summary>
        public string SortPath { get; set; }

        /// <summary>
        /// 主键是否自增
        /// </summary>
        public bool AutoIncrement { get; set; }

        /// <summary>
        /// 序列名称
        /// </summary>
        public string SequenceName { get; set; }
        
        /// <summary>
        /// 读取特性标签获取表信息
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <returns>返回新创建的表信息</returns>
        public static TableInfo FromPoco(Type t)
        {
            TableInfo ti = new TableInfo();

            #region 表信息

            var a = t.GetCustomAttributes(typeof(TableAttribute), true);
            if (a.Length > 0)
            {
                var tableAttribute = a[0] as TableAttribute;
                // ReSharper disable once PossibleNullReferenceException
                string name = string.IsNullOrEmpty(tableAttribute.Name) ? t.Name : tableAttribute.Name;
                string schema = tableAttribute.Schema;
                string tableName = string.IsNullOrEmpty(schema) ? name : string.Format("{0}.{1}", schema, name);
                ti.TableName = tableName;
                ti.Caption = tableAttribute.Caption;
            }
            else
            {
                ti.TableName = t.Name;
            }

            #endregion

            #region 主键信息

            a = t.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
            if (a.Length > 0)
            {
                var primaryKeyAttribute = a[0] as PrimaryKeyAttribute;

                // ReSharper disable once PossibleNullReferenceException
                ti.PrimaryKey = primaryKeyAttribute.Name;
                ti.SequenceName = primaryKeyAttribute.SequenceName;
                ti.AutoIncrement = primaryKeyAttribute.AutoIncrement;

            }

            #endregion

            #region 父级信息

            a = t.GetCustomAttributes(typeof(ParentKeyAttribute), true);
            if (a.Length > 0)
            {
                var parentKeyAttribute = a[0] as ParentKeyAttribute;

                // ReSharper disable once PossibleNullReferenceException
                ti.ParentKey = parentKeyAttribute.Name;
            }

            #endregion

            #region 排序路径

            a = t.GetCustomAttributes(typeof(SortPathAttribute), true);
            if (a.Length > 0)
            {
                var attribute = a[0] as SortPathAttribute;

                // ReSharper disable once PossibleNullReferenceException
                ti.SortPath = attribute.Name;
            }

            #endregion

            return ti;
        }

    }

}
