// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using Zeniths.Data;

namespace Zeniths.Auth.Service
{
    public class AuthRepository<T> : Repository<T> where T : class,new()
    {
        #region 构造函数

        /// <summary>
        /// 构造数据存储器
        /// </summary>
        public AuthRepository()
            : base(new AuthDatabase())
        {
        }

        /// <summary>
        /// 构造数据存储器
        /// </summary>
        /// <param name="database">数据库对象</param>
        public AuthRepository(Database database)
            : base(database)
        {
        }

        #endregion
    }
}