// ===============================================================================
//  Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.Hr.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 流程按钮服务
    /// </summary>
    public class WorkFlowButtonService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly Repository<WorkFlowButton> repos = new Repository<WorkFlowButton>();

        /// <summary>
        /// 检测是否存在指定流程按钮
        /// </summary>
        /// <param name="entity">流程按钮实体</param>
        /// <returns>存在返回true</returns>
        public BoolMessage Exists(WorkFlowButton entity)
        {
            var has = repos.Exists(p => p.WorkFlowButtonName == entity.WorkFlowButtonName
            && p.WorkFlowButtonId != entity.WorkFlowButtonId);
            return has ? new BoolMessage(false, "输入流程按钮名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 添加流程按钮
        /// </summary>
        /// <param name="entity">流程按钮实体</param>
        public BoolMessage Insert(WorkFlowButton entity)
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
        /// 更新流程按钮
        /// </summary>
        /// <param name="entity">流程按钮实体</param>
        public BoolMessage Update(WorkFlowButton entity)
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
        /// 删除流程按钮
        /// </summary>
        /// <param name="ids">流程按钮主键数组</param>
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
        /// 获取流程按钮对象
        /// </summary>
        /// <param name="id">流程表单主键</param>
        /// <returns>流程表单对象</returns>
        public WorkFlowButton Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取启用的表单列表
        /// </summary>
        /// <returns>返回启用的表单列表</returns>
        public List<WorkFlowButton> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.SortIndex);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取流程按钮列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="workFlowButtonName">按钮名称</param>
        /// <returns></returns>
        public PageList<WorkFlowButton> GetPageList(int pageIndex, int pageSize, string orderName,
            string orderDir, string workFlowButtonName)
        {
            orderName = orderName.IsEmpty() ? nameof(WorkFlowButton.WorkFlowButtonId) : orderName;
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).
                OrderBy(orderName, orderDir.IsAsc());
            if (workFlowButtonName.IsNotEmpty())
            {
                workFlowButtonName = workFlowButtonName.Trim();
                query.Where(p => p.WorkFlowButtonName.Contains(workFlowButtonName));
            }
            return repos.Page(query);
        }

        #region 私有方法


        #endregion
    }
}