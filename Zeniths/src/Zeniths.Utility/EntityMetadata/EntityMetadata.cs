// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Utility;

namespace Zeniths.Entity
{
    /// <summary>
    /// 实体元数据
    /// </summary>
    public class EntityMetadata
    {
        private static Cache<Type, EntityMetadata> _datas = new Cache<Type, EntityMetadata>();

        /// <summary>
        /// 实体类型
        /// </summary>
        public Type EntityType { get; private set; }

        /// <summary>
        /// 查询列
        /// </summary>
        public string[] QueryColumns { get; private set; }

        /// <summary>
        /// 表信息
        /// </summary>
        public TableInfo TableInfo { get; private set; }

        /// <summary>
        /// 列信息
        /// </summary>
        public Dictionary<string, EntityColumn> Columns { get; private set; }

        /// <summary>
        /// 使用指定的实体类型获取实体元数据(从缓存)
        /// </summary>
        /// <param name="t">实体类型</param>
        /// <returns>返回实体元数据</returns>
        public static EntityMetadata ForType(Type t)
        {
            return _datas.Get(t, () => new EntityMetadata(t));
        }

        /// <summary>
        /// 初始化实体元数据
        /// </summary>
        public EntityMetadata()
        {
        }

        /// <summary>
        /// 使用类型初始化实体元数据
        /// </summary>
        /// <param name="t">实体类型</param>
        public EntityMetadata(Type t)
        {
            TableInfo = TableInfo.FromPoco(t);
            Columns = new Dictionary<string, EntityColumn>(StringComparer.OrdinalIgnoreCase);
            EntityType = t;
            foreach (var pi in t.GetProperties())
            {
                ColumnInfo ci = ColumnInfo.FromProperty(pi);
                if (ci == null) continue;

                var pc = new EntityColumn();
                pc.PropertyInfo = pi;
                Columns.Add(pc.ColumnName, pc);
            }
            QueryColumns = (from c in Columns select c.Key).ToArray();
        }
         
        /// <summary>
        /// 获取实体主键值
        /// </summary>
        /// <param name="poco">实体对象</param>
        /// <returns>返回实体主键值</returns>
        public object GetPrimaryKeyValue(object poco)
        {
            string primaryKeyName = TableInfo.PrimaryKey;
            if (!string.IsNullOrEmpty(primaryKeyName))
            {
                var pc = this.Columns[primaryKeyName];
                return pc.GetValue(poco);
            }
            return null;
        }

        /// <summary>
        /// 获取实体主键值
        /// </summary>
        /// <param name="poco">实体对象</param>
        /// <param name="primaryKeyValue">主键值</param>
        /// <returns>返回实体主键值</returns>
        public void SetPrimaryKeyValue(object poco, object primaryKeyValue)
        {
            string primaryKeyName = TableInfo.PrimaryKey;
            if (!string.IsNullOrEmpty(primaryKeyName))
            {
                var pc = this.Columns[primaryKeyName];
                pc.SetValue(poco, pc.ChangeType(primaryKeyValue));
            }
        }
    }
}