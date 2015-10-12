using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Auth.Service
{
    /// <summary>
    /// 系统数据字典服务
    /// </summary>
    public class SystemDictionaryService
    {
        /// <summary>
        /// 字典存储器
        /// </summary>
        private readonly AuthRepository<SystemDictionary> dicRepos = new AuthRepository<SystemDictionary>();

        /// <summary>
        /// 字典明细项存储器
        /// </summary>
        private readonly AuthRepository<SystemDictionaryDetails> detilsRepos = new AuthRepository<SystemDictionaryDetails>();

        /// <summary>
        /// 检测是否存在指定系统数据字典
        /// </summary>
        /// <param name="entity">系统数据字典实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage ExistsDictionary(SystemDictionary entity)
        {
            var has = dicRepos.Exists(p => p.Code == entity.Code && p.Id != entity.Id);
            return has ? new BoolMessage(false, "指定的数据字典编码已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加系统数据字典
        /// </summary>
        /// <param name="entity">系统数据字典实体</param>
        public BoolMessage InsertDictionary(SystemDictionary entity)
        {
            try
            {
                dicRepos.Insert(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新系统数据字典
        /// </summary>
        /// <param name="entity">系统数据字典实体</param>
        public BoolMessage UpdateDictionary(SystemDictionary entity)
        {
            try
            {
                dicRepos.Update(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除系统数据字典
        /// </summary>
        /// <param name="ids">系统数据字典主键数组</param>
        public BoolMessage DeleteDictionary(int[] ids)
        {
            try
            {
                if (ids.Length == 1)
                {
                    dicRepos.Delete(ids[0]);
                }
                else
                {
                    dicRepos.Delete(ids);
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }


        /// <summary>
        /// 保存系统数据字典父节点
        /// </summary>
        /// <param name="id">系统数据字典主键</param>
        /// <param name="oldParentId">原父节点Id</param>
        /// <param name="newParentId">新父节点Id</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage SaveParentDictionary(int id, int oldParentId, int newParentId)
        {
            try
            {
                dicRepos.Update(new SystemDictionary { ParentId = newParentId }, p => p.Id == id, p => p.ParentId);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新系统数据字典排序路径
        /// </summary>
        /// <param name="sortPathData">数据列表</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage UpdateSortPathDictionary(IEnumerable<PrimaryKeyValue> sortPathData)
        {
            try
            {
                dicRepos.BatchUpdate(sortPathData);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取系统数据字典对象
        /// </summary>
        /// <param name="id">系统数据字典主键</param>
        /// <returns>系统数据字典对象</returns>
        public SystemDictionary GetDictionary(int id)
        {
            return dicRepos.Get(id);
        }

        /// <summary>
        /// 获取系统数据字典列表
        /// </summary>
        public List<SystemDictionary> GetDictionaryList()
        {
            var query = dicRepos.NewQuery.OrderBy(p => p.SortPath);
            return dicRepos.Query(query).ToList();
        }

        /// <summary>
        /// 获取系统数据字典明细列表
        /// </summary>
        public List<SystemDictionaryDetails> GetEnabledDetailsList()
        {
            var query = detilsRepos.NewQuery.OrderBy(p => p.SortPath);
            return detilsRepos.Query(query).ToList();
        }

        /// <summary>
        /// 获取系统数据字典明细分页列表(包括禁用记录)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <returns>数据字典明细分页列表</returns>
        public PageList<SystemDictionaryDetails> GetPageDetailsList(int pageIndex, int pageSize, string orderName,string orderDir)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemDictionary.Id) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = detilsRepos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            return detilsRepos.Page(query);
        }
    }
}