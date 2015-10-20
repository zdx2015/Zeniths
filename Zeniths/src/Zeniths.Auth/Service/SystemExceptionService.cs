using System;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Utility;
using SystemException = Zeniths.Auth.Entity.SystemException;

namespace Zeniths.Auth.Service
{
    public class SystemExceptionService
    {
        private readonly AuthRepository<SystemException> repos = new AuthRepository<SystemException>();

        /// <summary>
        /// 保存异常信息
        /// </summary>
        /// <param name="entity">异常实体</param>
        public BoolMessage Save(SystemException entity)
        {
            try
            {
                repos.Insert(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取异常对象
        /// </summary>
        /// <param name="id">异常主键</param>
        public SystemException Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取异常分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="ip">IP地址</param>
        /// <param name="message">异常消息关键字</param>
        public PageList<SystemException> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string startDate, string endDate, string ip, string message)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemException.CreateDateTime) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (startDate.IsNotEmpty())
            {
                var startDateDT = startDate.ToDateTime();
                query.Where(p => p.CreateDateTime >= startDateDT);
            }

            if (endDate.IsNotEmpty())
            {
                var endDateDT = endDate.ToDateTime().AddDays(1);
                query.Where(p => p.CreateDateTime <= endDateDT);
            }

            if (ip.IsNotEmpty())
            {
                ip = ip.Trim();
                query.Where(p => p.IPAddress.Contains(ip));
            }

            if (message.IsNotEmpty())
            {
                message = message.Trim();
                query.Where(p => p.Message.Contains(message));
            }
            return repos.Page(query);
        }
    }
}