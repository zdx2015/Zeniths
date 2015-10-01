// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Reflection;

namespace Zeniths.Entity
{
	public class EntityColumn
	{
	    private ColumnInfo _columnInfo;

	    public ColumnInfo ColumnInfo
	    {
	        get { return _columnInfo ?? (_columnInfo = ColumnInfo.FromProperty(PropertyInfo)); }
	    }

        public PropertyInfo PropertyInfo { get; set; }
	    public string ColumnName { get { return ColumnInfo.ColumnName; } }

	    public virtual void SetValue(object target, object val) { PropertyInfo.SetValue(target, val, null); }
		public virtual object GetValue(object target) { return PropertyInfo.GetValue(target, null); }
		public virtual object ChangeType(object val) { return Convert.ChangeType(val, PropertyInfo.PropertyType); }
	}
}
