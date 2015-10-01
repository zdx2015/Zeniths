﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Hr.Entity;
using Zeniths.Utility;

namespace Zeniths.Hr.Service
{
    public class WorkFlowFormService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly Repository<WorkFlowForm> repos = new Repository<WorkFlowForm>();

        /// <summary>
        /// 检测是否存在指定流程表单
        /// </summary>
        /// <param name="entity">流程表单实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(WorkFlowForm entity)
        {
            var has = repos.Exists(p => p.WorkFlowFormName == entity.WorkFlowFormName
            && p.WorkFlowFormCategory == entity.WorkFlowFormCategory
            && p.WorkFlowFormId != entity.WorkFlowFormId);
            return has ? new BoolMessage(false, "输入流程表单名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加流程表单
        /// </summary>
        /// <param name="entity">流程表单实体</param>
        public BoolMessage Insert(WorkFlowForm entity)
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
        public BoolMessage Update(WorkFlowForm entity)
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
        public WorkFlowForm Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取启用的表单列表
        /// </summary>
        /// <returns>返回启用的表单列表</returns>
        public List<WorkFlowForm> GetEnabledList()
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
        /// <param name="workFlowFormName">表单名称</param>
        /// <param name="workFlowFormCategory">表单分类</param>
        /// <returns>流程表单分页列表</returns>
        public PageList<WorkFlowForm> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string workFlowFormName, string workFlowFormCategory)
        {
            orderName = orderName.IsEmpty() ? nameof(WorkFlowForm.WorkFlowFormId) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (workFlowFormName.IsNotEmpty())
            {
                workFlowFormName = workFlowFormName.Trim();
                query.Where(p => p.WorkFlowFormName.Contains(workFlowFormName));
            }
            if (workFlowFormCategory.IsNotEmpty())
            {
                workFlowFormCategory = workFlowFormCategory.Trim();
                query.Where(p => p.WorkFlowFormCategory == workFlowFormCategory);
            }
            return repos.Page(query);
        }

        #region 私有方法


        #endregion
    }
}