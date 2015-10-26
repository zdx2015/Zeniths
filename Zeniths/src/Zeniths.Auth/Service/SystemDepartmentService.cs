﻿using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    /// <summary>
    /// 系统部门服务
    /// </summary>
    public class SystemDepartmentService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly AuthRepository<SystemDepartment> repos = new AuthRepository<SystemDepartment>();

        /// <summary>
        /// 检测是否存在指定系统部门
        /// </summary>
        /// <param name="entity">系统部门实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(SystemDepartment entity)
        {
            var has = repos.Exists(p => p.Name == entity.Name && p.ParentId == entity.ParentId && p.Id != entity.Id);
            return has ? new BoolMessage(false, "指定的部门名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加系统部门
        /// </summary>
        /// <param name="entity">系统部门实体</param>
        public BoolMessage Insert(SystemDepartment entity)
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
        /// 更新系统部门
        /// </summary>
        /// <param name="entity">系统部门实体</param>
        public BoolMessage Update(SystemDepartment entity)
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
        /// 删除系统部门
        /// </summary>
        /// <param name="ids">系统部门主键数组</param>
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
        /// 保存父节点
        /// </summary>
        /// <param name="id">模块主键</param>
        /// <param name="newParentId">新父节点Id</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage SaveParent(int id, int newParentId)
        {
            try
            {
                repos.Update(new SystemDepartment { ParentId = newParentId }, p => p.Id == id, p => p.ParentId);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新排序路径
        /// </summary>
        /// <param name="sortData">数据列表</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage SaveSort(IEnumerable<PrimaryKeyValue> sortData)
        {
            try
            {
                repos.BatchUpdate(sortData);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取系统部门对象
        /// </summary>
        /// <param name="id">系统部门主键</param>
        /// <returns>系统部门对象</returns>
        public SystemDepartment Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取部门名称
        /// </summary>
        /// <param name="id">系统部门主键</param>
        /// <returns>返回指定主键的部门名称</returns>
        public string GetName(int id)
        {
            return repos.Get(id, p => p.Name)?.Name;
        }


        /// <summary>
        /// 获取系统部门列表
        /// </summary>
        /// <returns>系统部门列表</returns>
        public List<SystemDepartment> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.SortPath);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取启用部门列表
        /// </summary>
        public List<SystemDepartment> GetEnabledList()
        {
            var query = repos.NewQuery.OrderBy(p => p.SortPath)
                .Where(p => p.IsEnabled == true);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取系统部门列表(包括禁用记录)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="departmentId">部门主键</param>
        /// <param name="name">部门名称</param>
        /// <returns>返回系统部门分页列表</returns>
        public PageList<SystemDepartment> GetPageList(int pageIndex, int pageSize,
            string orderName, string orderDir, int departmentId, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemDepartment.SortPath) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Asc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                Where(p => p.ParentId == departmentId).
                OrderBy(orderName, orderDir.IsAsc());
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name));
            }
            return repos.Page(query);
        }

        /// <summary>
        /// 获取指定部门主键的所有上级部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回指定部门主键的所有上级部门列表</returns>
        public List<SystemDepartment> GetAllParents(int departmentId)
        {
            var cols = EntityMetadata.ForType(typeof(SystemDepartment)).QueryColumns;
            var sql = $"SELECT {StringHelper.ConvertArrayToString(cols)} FROM fnGetAllParentsDepartment(@departmentId) ORDER BY SortPath ASC";
            return repos.Database.Query<SystemDepartment>(sql, new object[] {departmentId}).ToList();
        }

        /// <summary>
        /// 获取指定部门主键的所有上级部门主键数组
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回指定部门主键的所有上级部门主键数组</returns>
        public int[] GetAllParentsIdArray(int departmentId)
        {
            var sql = $"SELECT Id FROM fnGetAllParentsDepartment(@departmentId) ORDER BY SortPath ASC";
            return repos.Database.Query<int>(sql, new object[] { departmentId }).ToArray();
        }

        /// <summary>
        /// 获取指定部门主键的所有下级部门
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回指定部门主键的所有下级部门列表</returns>
        public List<SystemDepartment> GetAllChilds(int departmentId)
        {
            var cols = EntityMetadata.ForType(typeof(SystemDepartment)).QueryColumns;
            var sql = $"SELECT {StringHelper.ConvertArrayToString(cols)} FROM fnGetAllChildsDepartment(@departmentId) ORDER BY SortPath ASC";
            return repos.Database.Query<SystemDepartment>(sql, new object[] { departmentId }).ToList();
        }

        /// <summary>
        /// 获取指定部门主键的所有下级部门主键数组
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>返回指定部门主键的所有下级部门主键数组</returns>
        public int[] GetAllChildsIdArray(int departmentId)
        {
            var cols = EntityMetadata.ForType(typeof(SystemDepartment)).QueryColumns;
            var sql = $"SELECT {StringHelper.ConvertArrayToString(cols)} FROM fnGetAllChildsDepartment(@departmentId) ORDER BY SortPath ASC";
            return repos.Database.Query<int>(sql, new object[] { departmentId }).ToArray();
        }

        /// <summary>
        /// 查询所有有效的部门主键和名称列表(Key为Id,Value为名称)
        /// </summary>
        public List<KeyValuePair<int, string>> GetDepartmentIdNameList()
        {
            var query = repos.NewQuery
                .Select(nameof(SystemDepartment.Id), nameof(SystemDepartment.Name))
                .Where(p => p.IsEnabled == true)
                .OrderBy(p => p.SortPath);
            var list = repos.Query(query).ToList();
            return list.Select(item => new KeyValuePair<int, string>(item.Id, item.Name)).ToList();
        }

        /// <summary>
        /// 查询所有有效的部门主键和名称字典
        /// </summary>
        public Dictionary<int, string> GetDepartmentIdNameDic()
        {
            var list = GetDepartmentIdNameList();
            var dic = new Dictionary<int, string>();
            foreach (var item in list)
            {
                dic[item.Key] = item.Value;
            }
            return dic;
        }
    }
}