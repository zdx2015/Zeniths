using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    public class SystemDocService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly AuthRepository<SystemDoc> repos = new AuthRepository<SystemDoc>();

        /// <summary>
        /// 检测是否存在指定系统文档
        /// </summary>
        /// <param name="entity">系统文档实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(SystemDoc entity)
        {
            var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            return has ? new BoolMessage(false, "指定的系统文档已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加系统文档
        /// </summary>
        /// <param name="entity">系统文档实体</param>
        public BoolMessage Insert(SystemDoc entity)
        {
            try
            {
                entity.CreateDateTime = entity.ModifyDateTime = DateTime.Now;
                repos.Insert(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新系统文档
        /// </summary>
        /// <param name="entity">系统文档实体</param>
        public BoolMessage Update(SystemDoc entity)
        {
            try
            {
                entity.ModifyDateTime = DateTime.Now;
                repos.Update(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除系统文档
        /// </summary>
        /// <param name="ids">系统文档主键数组</param>
        public BoolMessage Delete(int[] ids)
        {
            try
            {
                if (ids.Length == 1)
                {
                    repos.Delete(ids[0]);
                }
                else
                {
                    repos.Delete(ids);
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取系统文档对象
        /// </summary>
        /// <param name="id">系统文档主键</param>
        public SystemDoc Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取系统文档列表
        /// </summary>
        public List<SystemDoc> GetList()
        {
            var query = repos.NewQuery.OrderByDescending(p => p.ModifyDateTime);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取系统文档分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">文档标题</param>
        public PageList<SystemDoc> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemDoc.ModifyDateTime) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name) || p.Tag.Contains(name));
            }
            return repos.Page(query);
        }
    }
}