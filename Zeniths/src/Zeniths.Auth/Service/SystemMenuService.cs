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
    /// <summary>
    /// 系统菜单服务
    /// </summary>
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
        public BoolMessage Exists(SystemMenu entity)
        {
            var has = repos.Exists(p => p.Code == entity.Code && p.Id != entity.Id);
            return has ? new BoolMessage(false, "指定的菜单编码已经存在") : BoolMessage.True;
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
        /// 保存模块父节点
        /// </summary>
        /// <param name="id">模块主键</param>
        /// <param name="oldParentId">原父节点Id</param>
        /// <param name="newParentId">新父节点Id</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage SaveParent(int id, int oldParentId, int newParentId)
        {
            try
            {
                repos.Update(new SystemMenu { ParentId = newParentId }, p => p.Id == id, p => p.ParentId);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新模块排序路径
        /// </summary>
        /// <param name="sortPathData">数据列表</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage UpdateSortPath(IEnumerable<PrimaryKeyValue> sortPathData)
        {
            try
            {
                repos.BatchUpdate(sortPathData);
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
            var query = repos.NewQuery.OrderBy(p => p.SortPath);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取启用模块列表
        /// </summary>
        public List<SystemMenu> GetEnabledList()
        {
            var query = repos.NewQuery.OrderBy(p => p.SortPath)
                .Where(p => p.IsEnabled == true);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 查询所有有效的菜单主键和名称列表(Key为Id,Value为名称)
        /// </summary>
        public List<KeyValuePair<int, string>> GetMenuIdNameList()
        {
            var query = repos.NewQuery
                .Select(nameof(SystemMenu.Id), nameof(SystemMenu.Name))
                .Where(p => p.IsEnabled == true)
                .OrderBy(p => p.SortPath);
            var list = repos.Query(query).ToList();
            return list.Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
        }

        /// <summary>
        /// 查询所有有效的菜单主键和名称字典
        /// </summary>
        public Dictionary<int, string> GetMenuIdNameDic()
        {
            var list = GetMenuIdNameList();
            var dic = new Dictionary<int, string>();
            foreach (var item in list)
            {
                dic[item.Key] = item.Value;
            }
            return dic;
        }
    }
}