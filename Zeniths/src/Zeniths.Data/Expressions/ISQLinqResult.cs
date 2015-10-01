// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System.Collections.Generic;

namespace Zeniths.Data.Expressions
{
    /// <summary>
    /// 语句结果
    /// </summary>
    public interface ISQLinqResult
    {
        /// <summary>
        /// 参数字典
        /// </summary>
        IDictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// 转为SQL语句
        /// </summary>
        /// <returns>返回SQL语句</returns>
        string ToSQL();
    }
}
