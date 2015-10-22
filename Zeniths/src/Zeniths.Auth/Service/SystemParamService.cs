using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    public class SystemParamService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly AuthRepository<SystemParam> repos = new AuthRepository<SystemParam>();

        /// <summary>
        /// 检测是否存在指定参数
        /// </summary>
        /// <param name="entity">参数实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(SystemParam entity)
        {
            var has = repos.Exists(p => p.Code == entity.Code && p.Id != entity.Id);
            return has ? new BoolMessage(false, "输入流程参数编码已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="entity">参数实体</param>
        public BoolMessage Insert(SystemParam entity)
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
        /// 更新参数
        /// </summary>
        /// <param name="entity">参数实体</param>
        public BoolMessage Update(SystemParam entity)
        {
            try
            {
                repos.Update(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="ids">参数主键数组</param>
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
        /// 获取参数
        /// </summary>
        /// <param name="id">参数主键</param>
        /// <returns>参数对象</returns>
        public SystemParam Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取启用的参数列表
        /// </summary>
        /// <returns>返回启用的参数列表</returns>
        public List<SystemParam> GetList()
        {
            var query = repos.NewQuery;
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取参数分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">参数名称</param>
        /// <param name="category">参数分类</param>
        /// <returns>参数分页列表</returns>
        public PageList<SystemParam> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string name, string category)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemParam.Id) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name) || p.Value.Contains(name));
            }
            if (category.IsNotEmpty())
            {
                category = category.Trim();
                query.Where(p => p.Category == category);
            }
            return repos.Page(query);
        }
    }
}