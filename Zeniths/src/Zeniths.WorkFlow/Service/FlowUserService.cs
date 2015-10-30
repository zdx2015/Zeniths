// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.WorkFlow.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.WorkFlow.Service
{
    /// <summary>
    /// 流程用户服务
    /// </summary>
    public class FlowUserService
    {
        /// <summary>
        /// 流程用户存储器
        /// </summary>
        private readonly WorkFlowRepository<FlowUser> repos = new WorkFlowRepository<FlowUser>();

        /// <summary>
        /// 检测是否存在指定流程用户(每个用户每个流程实例1条数据)
        /// </summary>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(string userId, string flowInstanceId)
        {
            var has = repos.Exists(p => p.FlowInstanceId == flowInstanceId && p.UserId == userId);
            return has ? BoolMessage.False : BoolMessage.True;
        }

        /// <summary>
        /// 新增流程用户
        /// </summary>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Save(FlowUser entity)
        {
            try
            {
                string userId = entity.UserId;
                string flowInstanceId = entity.FlowInstanceId;
                if (Exists(userId, flowInstanceId).Success)
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
        /// 获取流程用户对象
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="flowId">流程主键</param>
        /// <returns>返回流程用户对象</returns>
        public int[] GetBusinessIdArray(string userId, string flowId = null)
        {
            var query = repos.NewQuery
                .Where(p => p.UserId == userId)
                .Select(p => p.BusinessId)
                .OrderByDescending(p => p.CreateDateTime);
            if (!string.IsNullOrEmpty(flowId))
            {
                query.Where(p => p.FlowId == flowId);
            }
            var list = repos.Query(query).ToList();
            return list.Select(item => item.BusinessId.ToInt()).ToArray();
        }

        /// <summary>
        /// 获取流程用户列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="flowId">流程主键</param>
        /// <returns>返回流程用户列表</returns>
        public List<FlowUser> GetList(string userId, string flowId = null)
        {
            var query = repos.NewQuery.Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreateDateTime);
            if (!string.IsNullOrEmpty(flowId))
            {
                query.Where(p => p.FlowId == flowId);
            }

            return repos.Query(query).ToList();
        }
    }
}