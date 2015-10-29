using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.Auth.Entity;
using Zeniths.Collections;
using Zeniths.Data.Extensions;
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
        private readonly AuthRepository<SystemDictionaryDetails> detailsRepos = new AuthRepository<SystemDictionaryDetails>();

        private readonly AuthRepository<SystemDictionaryDetailExtend>  detailExRepos = new AuthRepository<SystemDictionaryDetailExtend>();

        /// <summary>
        /// 检测是否存在指定系统数据字典
        /// </summary>
        /// <param name="code">数据字典编码</param>
        /// <param name="id">数据字典主键</param>
        /// <returns>存在返回true</returns>
        public BoolMessage ExistsDictionary(string code, int id)
        {
            var has = dicRepos.Exists(p => p.Code == code && p.Id != id);
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
                    var id = ids[0];
                    dicRepos.Delete(id);
                    detailsRepos.Delete(p => p.DictionaryId == id);
                }
                else
                {
                    dicRepos.Delete(ids);
                    detailsRepos.Delete(p => p.DictionaryId.In(ids));
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
        /// <param name="newParentId">新父节点Id</param>
        /// <returns>操作成功返回True</returns>
        public BoolMessage SaveParentDictionary(int id, int newParentId)
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
            var list = dicRepos.Query(query).ToList();
            return list;
        }


        /// <summary>
        /// 检测是否存在指定系统数据字典明细项
        /// </summary>
        /// <param name="name">数据字典明细项名称</param>
        /// <param name="dictionaryId">数据字典主键</param>
        /// <param name="id">字典明细项主键</param>
        /// <returns>存在返回true</returns>
        public BoolMessage ExistsDetails(string name, int dictionaryId, int id)
        {
            var has = detailsRepos.Exists(p => p.Name == name && p.DictionaryId == dictionaryId && p.Id != id);
            return has ? new BoolMessage(false, "指定的字典明细项名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加系统数据字典明细项
        /// </summary>
        /// <param name="entity">系统数据字典明细项实体</param>
        public BoolMessage InsertDetails(SystemDictionaryDetails entity)
        {
            try
            {
                detailsRepos.Insert(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新系统数据字典明细项
        /// </summary>
        /// <param name="entity">系统数据字典明细项实体</param>
        public BoolMessage UpdateDetails(SystemDictionaryDetails entity)
        {
            try
            {
                detailsRepos.Update(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除系统数据字典明细项
        /// </summary>
        /// <param name="ids">系统数据字典明细项主键数组</param>
        public BoolMessage DeleteDetails(int[] ids)
        {
            try
            {
                if (ids.Length == 1)
                {
                    detailsRepos.Delete(ids[0]);
                }
                else
                {
                    detailsRepos.Delete(ids);
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取系统数据字典明细对象
        /// </summary>
        /// <param name="id">系统数据字典明细主键</param>
        /// <returns>系统数据字典明细对象</returns>
        public SystemDictionaryDetails GetDetails(int id)
        {
            return detailsRepos.Get(id);
        }

        /// <summary>
        /// 获取系统数据字典明细列表
        /// </summary>
        /// <param name="dictionaryId">字典主键</param>
        /// <returns></returns>
        public List<SystemDictionaryDetails> GetEnabledDetailsListByDicId(int dictionaryId)
        {
            var query = detailsRepos.NewQuery.
                Where(p => p.DictionaryId == dictionaryId).
                OrderBy(p => p.SortIndex);
            return detailsRepos.Query(query).ToList();
        }

        /// <summary>
        /// 获取系统数据字典明细列表
        /// </summary>
        /// <param name="dicCode">字典编码</param>
        /// <returns></returns>
        public List<SystemDictionaryDetails> GetEnabledDetailsListByDicCode(string dicCode)
        {
            var sql = @"
SELECT details.Id ,details.DictionaryId ,details.Name ,details.NameSpell ,details.Value ,details.IsEnabled ,
details.SortIndex ,details.Note FROM SystemDictionary dic JOIN SystemDictionaryDetails details
ON dic.Id = details.DictionaryId AND dic.Code=@code ORDER BY details.SortIndex";
            return detailsRepos.Database.Query<SystemDictionaryDetails>(sql, new { code = dicCode }).ToList();
        }


        /// <summary>
        /// 获取系统数据字典明细列表
        /// </summary>
        /// <param name="dicCode">字典编码</param>
        /// <returns></returns>
        public List<SystemDictionaryDetailExtend> GetEnabledDicListByDicCode(string dicCode)
        {
            var sql = @"
                    SELECT a.Id,a.DictionaryId,a.Name,b.Name AS Category ,a.NameSpell,a.Value,a.IsEnabled,a.SortIndex,a.Note
                    FROM dbo.SystemDictionaryDetails a JOIN (
                    SELECT Id,Name FROM  dbo.SystemDictionary WHERE ParentId in (
                    SELECT Id FROM dbo.SystemDictionary WHERE Code=@code )
                    ) b
                    ON a.DictionaryId = b.Id ";
             
            return detailExRepos.Database.Query<SystemDictionaryDetailExtend>(sql, new { code = dicCode }).ToList();
        }




        /// <summary>
        /// 获取系统数据字典明细分页列表(包括禁用记录)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="dictionaryId">字典主键</param>
        /// <param name="name">明细项名称或者简拼</param>
        /// <returns>数据字典明细分页列表</returns>
        public PageList<SystemDictionaryDetails> GetPageDetailsList(int pageIndex, int pageSize,
            string orderName, string orderDir, int dictionaryId, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(SystemDictionaryDetails.SortIndex) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Asc) : orderDir;
            var query = detailsRepos.NewQuery.Take(pageSize).Page(pageIndex).
                Where(p => p.DictionaryId == dictionaryId).
                OrderBy(orderName, orderDir.IsAsc());
            if (!string.IsNullOrEmpty(name))
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name) || p.NameSpell.Contains(name));
            }
            return detailsRepos.Page(query);
        }

    }
}