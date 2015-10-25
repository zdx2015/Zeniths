using System;
using System.Collections.Generic;
using System.Linq;
using Zeniths.Collections;
using Zeniths.Entity;
using Zeniths.Extensions;
using Zeniths.Utility;
using Zeniths.WorkFlow.Entity;

namespace Zeniths.WorkFlow.Service
{
    public class FlowService
    {
        private readonly WorkFlowRepository<Flow> repos = new WorkFlowRepository<Flow>();

        /// <summary>
        /// 检测是否存在指定流程
        /// </summary>
        /// <param name="entity">流程实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(Flow entity)
        {
            var has = repos.Exists(p => p.Name == entity.Name && p.Category == entity.Category && p.Id != entity.Id);
            return has ? new BoolMessage(false, "输入流程名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加流程
        /// </summary>
        /// <param name="entity">流程实体</param>
        public BoolMessage Save(Flow entity)
        {
            try
            {
                var has = repos.Exists(p => p.Id == entity.Id);
                if (has)
                {
                    repos.Update(entity);
                }
                else
                {
                    repos.Insert(entity);
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="ids">流程主键数组</param>
        public BoolMessage Delete(string[] ids)
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
        /// 获取流程对象
        /// </summary>
        /// <param name="id">流程主键</param>
        /// <returns>流程对象</returns>
        public Flow Get(string id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取流程Json
        /// </summary>
        /// <param name="id">流程主键</param>
        /// <returns>流程Json</returns>
        public string GetFlowJson(string id)
        {
            return repos.Get(id,p=>p.Json)?.Json;
        }

        /// <summary>
        /// 查询所有流程主键和名称字典
        /// </summary>
        public Dictionary<string, string> GetFlowDic()
        {
            var query = repos.NewQuery
                .Select(nameof(Flow.Id),nameof(Flow.Name))
                .Where(p => p.IsEnabled == true)
                .OrderBy(p => p.SortIndex);
            var list = repos.Query(query).ToList();
            var dic = new Dictionary<string, string>();
            foreach (var item in list)
            {
                dic[item.Id] = item.Name;
            }
            return dic;
        }

        /// <summary>
        /// 获取启用的流程列表
        /// </summary>
        /// <returns>返回启用的流程列表</returns>
        public List<Flow> GetEnabledList()
        {
            var cols = EntityMetadata.ForType(typeof(Flow)).QueryColumns.Except(new[] { "Json" }).ToArray();
            var query = repos.NewQuery
                .Select(cols)
                .Where(p => p.IsEnabled == true)
                .OrderBy(p => p.SortIndex);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取流程列表(包括禁用记录)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">流程名称</param>
        /// <param name="category">流程分类</param>
        /// <returns>流程分页列表</returns>
        public PageList<Flow> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string name, string category)
        {
            var cols = EntityMetadata.ForType(typeof(Flow)).QueryColumns.Except(new[] { "Json" }).ToArray();
            orderName = orderName.IsEmpty() ? nameof(Flow.SortIndex) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery
                .Select(cols)
                .Take(pageSize)
                .Page(pageIndex)
                .OrderBy(orderName, orderDir.IsAsc());
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name));
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