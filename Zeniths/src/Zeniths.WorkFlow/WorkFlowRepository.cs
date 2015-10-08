using Zeniths.Data;

namespace Zeniths.WorkFlow
{
    public class WorkFlowRepository<T> : Repository<T> where T : class, new()
    {
        #region 构造函数

        /// <summary>
        /// 构造数据存储器
        /// </summary>
        public WorkFlowRepository()
            : base(new WorkFlowDatabase())
        {
        }

        /// <summary>
        /// 构造数据存储器
        /// </summary>
        /// <param name="database">数据库对象</param>
        public WorkFlowRepository(Database database)
            : base(database)
        {
        }

        #endregion
    }
}