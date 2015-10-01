using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Hr.Entity;

namespace Zeniths.Hr.Service
{
    public class UserService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly Repository<BaseUser> _repos = new Repository<BaseUser>();


        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>对象</returns>
        public BaseUser Get(int id)
        {
            return _repos.Get(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>列表</returns>
        public List<BaseUser> GetList()
        {
            var query = _repos.NewQuery;
            return _repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取数据表
        /// </summary>
        /// <returns>返回数据表</returns>
        public DataTable GetTable()
        {
            var query = _repos.NewQuery;
            return _repos.GetTable(query);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        public PageList<BaseUser> GetPageList(int pageIndex, int pageSize, 
            string orderName, string orderDir, string userName, string realName)
        {
            orderName = string.IsNullOrEmpty(orderName) ? "Id" : orderName;
            var query = _repos.NewQuery.Take(pageSize)
                .Page(pageIndex)
                .OrderBy(orderName, orderDir.ToLower().Equals("asc"));
            if (userName.IsNotEmpty())
            {
                userName = userName.Trim();
                query.Where(p => p.UserName.Contains(userName));
            }
            if (realName.IsNotEmpty())
            {
                realName = realName.Trim();
                query.Where(p => p.RealName.Contains(realName));
            }
            return _repos.Page(query);
        }

    }
}