// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
namespace Zeniths.Data.Expressions
{
    /// <summary>
    /// Count语句
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class SQLinqCount<T> : ISQLinq
    {
        /// <summary>
        /// 构造Count语句
        /// </summary>
        /// <param name="query">查询对象</param>
        public SQLinqCount(SQLQuery<T> query)
        {
            this.Query = query;
        }

        /// <summary>
        /// 查询对象
        /// </summary>
        public SQLQuery<T> Query { get; private set; }

        /// <summary>
        /// 转为Count语句结果对象
        /// </summary>
        /// <param name="existingParameterCount">已经存在的参数个数</param>
        /// <returns>返回Count语句结果对象</returns>
        public ISQLinqResult ToResult(int existingParameterCount = 0)
        {
            var result = (SQLinqSelectResult)this.Query.ToResult(existingParameterCount);

            const string selectCount = "COUNT(1)";
            result.Select = new [] { selectCount };

            return result;
        }
    }
}
