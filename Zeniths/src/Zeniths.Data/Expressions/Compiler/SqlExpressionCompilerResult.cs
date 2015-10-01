// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System.Collections.Generic;

namespace Zeniths.Data.Expressions.Compiler
{
    public class SqlExpressionCompilerResult
    {
        public SqlExpressionCompilerResult()
        {
            this.Parameters = new Dictionary<string, object>();
        }

        public SqlExpressionCompilerResult(string sql, IDictionary<string, object> parameters)
        {
            this.SQL = sql;
            this.Parameters = parameters;
        }

        public string SQL { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
    }
}
