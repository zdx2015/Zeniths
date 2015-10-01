// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System.Collections.Generic;

namespace Zeniths.Data.Expressions.Compiler
{
    public class SqlExpressionCompilerSelectorResult
    {
        public SqlExpressionCompilerSelectorResult()
        {
            this.Select = new List<string>();
            this.Parameters = new Dictionary<string, object>();
        }

        public IList<string> Select { get; set; }
        public IDictionary<string, object> Parameters { get; set; }
    }
}
