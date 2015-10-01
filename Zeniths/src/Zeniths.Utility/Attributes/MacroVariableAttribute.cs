// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;

namespace Zeniths.Utility
{
    /// <summary>
    /// 宏变量
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MacroVariableAttribute: Attribute
    {
        /// <summary>
        /// 名称 格式$({属性名称}))
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get;set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 宏变量
        /// </summary>
        public MacroVariableAttribute()
        {

        }

        /// <summary>
        /// 宏变量
        /// </summary>
        public MacroVariableAttribute(string remark)
        {
            this.Remark = remark;
        }
    }
}