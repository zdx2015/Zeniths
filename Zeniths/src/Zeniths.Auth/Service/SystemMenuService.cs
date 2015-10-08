using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    public class SystemMenuService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly AuthRepository<SystemMenu> repos = new AuthRepository<SystemMenu>();

        /// <summary>
        /// 检测是否存在指定系统菜单
        /// </summary>
        /// <param name="entity">系统菜单实体</param>
        /// <returns>存在返回true</returns>
        public bool Exists(SystemMenu entity)
        {
            return repos.Exists(p => p.Code == entity.Code && p.Id != entity.Id);
        }

        /// <summary>
        /// 添加系统菜单
        /// </summary>
        /// <param name="entity">系统菜单实体</param>
        public BoolMessage Insert(SystemMenu entity)
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
        /// 更新系统菜单
        /// </summary>
        /// <param name="entity">系统菜单实体</param>
        public BoolMessage Update(SystemMenu entity)
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
        /// 删除系统菜单
        /// </summary>
        /// <param name="ids">系统菜单主键数组</param>
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
        /// 获取系统菜单对象
        /// </summary>
        /// <param name="id">系统菜单主键</param>
        /// <returns>系统菜单对象</returns>
        public SystemMenu Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取系统菜单列表
        /// </summary>
        /// <returns>系统菜单列表</returns>
        public List<SystemMenu> GetList()
        {
            var query = repos.NewQuery.OrderBy(p=>p.SortPath);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取启用模块列表
        /// </summary>
        public List<SystemMenu> GetEnabledList()
        {
            var query = repos.NewQuery.OrderBy(p => p.SortPath)
                .Where(p=>p.IsEnabled == true);
            return repos.Query(query).ToList();
        }
    }
}