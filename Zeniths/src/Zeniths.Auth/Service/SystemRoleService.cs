using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    public class SystemRoleService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly AuthRepository<SystemRole> repos = new AuthRepository<SystemRole>();

        /// <summary>
        /// 检测是否存在指定角色
        /// </summary>
        /// <param name="entity">角色实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(SystemRole entity)
        {
            var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            return has ? new BoolMessage(false, "输入角色名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="entity">角色实体</param>
        public BoolMessage Insert(SystemRole entity)
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
        /// 更新角色
        /// </summary>
        /// <param name="entity">角色实体</param>
        public BoolMessage Update(SystemRole entity)
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
        /// 删除角色
        /// </summary>
        /// <param name="ids">角色主键数组</param>
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
        /// 获取角色
        /// </summary>
        /// <param name="id">角色主键</param>
        /// <returns>角色对象</returns>
        public SystemRole Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取启用的角色列表
        /// </summary>
        /// <returns>返回启用的角色列表</returns>
        public List<SystemRole> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.SortIndex);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取角色列表(包括禁用记录)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">角色名称</param>
        /// <param name="category">角色分类</param>
        /// <returns>角色分页列表</returns>
        public PageList<SystemRole> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string name, string category)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemRole.SortIndex) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Asc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name) || p.NameSpell.Contains(name));
            }
            if (category.IsNotEmpty())
            {
                category = category.Trim();
                query.Where(p => p.Category == category);
            }
            return repos.Page(query);
        }

        /// <summary>
        /// 查询所有有效的角色主键和名称列表(Key为Id,Value为名称)
        /// </summary>
        public List<KeyValuePair<int, string>> GetRoleIdNameList()
        {
            var query = repos.NewQuery
                .Select(nameof(SystemRole.Id), nameof(SystemRole.Name))
                .Where(p => p.IsEnabled == true)
                .OrderBy(p => p.SortIndex);
            var list = repos.Query(query).ToList();
            return list.Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
        }

        /// <summary>
        /// 查询所有有效的角色主键和名称字典
        /// </summary>
        public Dictionary<int, string> GetRoleIdNameDic()
        {
            var list = GetRoleIdNameList();
            var dic = new Dictionary<int, string>();
            foreach (var item in list)
            {
                dic[item.Key] = item.Value;
            }
            return dic;
        }
    }
}