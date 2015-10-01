// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;

namespace Zeniths.Utility
{
    /// <summary>
    /// 枚举项描述信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class EnumCaptionAttribute : Attribute
    {
        private readonly string _caption;

        /// <summary>
        /// 枚举项名称
        /// </summary>
        public string Name { get; internal set; }
        
        /// <summary>
        /// 枚举项描述
        /// </summary>
        public string Caption
        {
            get { return _caption; }
        }

        /// <summary>
        /// 指定描述信息初始化枚举项描述信息
        /// </summary>
        /// <param name="caption">枚举项描述</param>
        public EnumCaptionAttribute(string caption)
        {
            this._caption = caption;
        }
    }
}