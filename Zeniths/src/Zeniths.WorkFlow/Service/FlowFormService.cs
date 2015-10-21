using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Utility;
using Zeniths.WorkFlow.Entity;

namespace Zeniths.WorkFlow.Service
{
    public class FlowFormService
    {
        private readonly WorkFlowRepository<FlowForm> repos = new WorkFlowRepository<FlowForm>();

        /// <summary>
        /// 检测是否存在指定流程表单
        /// </summary>
        /// <param name="entity">流程表单实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(FlowForm entity)
        {
            var has = repos.Exists(p => p.Name == entity.Name && p.Category == entity.Category && p.Id != entity.Id);
            return has ? new BoolMessage(false, "输入流程表单名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加流程表单
        /// </summary>
        /// <param name="entity">流程表单实体</param>
        public BoolMessage Insert(FlowForm entity)
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
        /// 更新流程表单
        /// </summary>
        /// <param name="entity">流程表单实体</param>
        public BoolMessage Update(FlowForm entity)
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
        /// 删除流程表单
        /// </summary>
        /// <param name="ids">流程表单主键数组</param>
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
        /// 获取流程表单对象
        /// </summary>
        /// <param name="id">流程表单主键</param>
        /// <returns>流程表单对象</returns>
        public FlowForm Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取启用的表单列表
        /// </summary>
        /// <returns>返回启用的表单列表</returns>
        public List<FlowForm> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.SortIndex);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取流程表单列表(包括禁用记录)
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">表单名称</param>
        /// <param name="category">表单分类</param>
        /// <returns>流程表单分页列表</returns>
        public PageList<FlowForm> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string name, string category)
        {
            orderName = orderName.IsEmpty() ? nameof(FlowForm.Id) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
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

        #region 私有方法


        #endregion
    }
}