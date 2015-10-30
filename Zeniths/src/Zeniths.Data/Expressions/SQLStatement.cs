namespace Zeniths.Data.Expressions
{
    /// <summary>
    /// SQL原生语句
    /// </summary>
    public class SQLStatement
    {
        /// <summary>
        /// SQL语句
        /// </summary>
        public string Statement { get; set; }

        /// <summary>
        /// 初始化SQL原生语句。
        /// </summary>
        /// <param name="statement">SQL语句</param>
        public SQLStatement(string statement)
        {
            Statement = statement;
        }

    }
}