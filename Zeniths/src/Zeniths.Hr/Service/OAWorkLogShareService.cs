// ===============================================================================
// 正得信股份 版权所有
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
using Zeniths.Data.Extensions;
using Zeniths.Auth.Utility;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 工作日志分享人服务
    /// </summary>
    public class OAWorkLogShareService
    {
        /// <summary>
        /// 工作日志分享人存储器
        /// </summary>
        private readonly Repository<OAWorkLogShare> repos = new Repository<OAWorkLogShare>();
        /// <summary>
        /// 工作日志分享扩展存储器
        /// </summary>
        private readonly Repository<OAWorkLogExtend> reposExtend = new Repository<OAWorkLogExtend>();

        /// <summary>
        /// 检测是否存在指定工作日志分享人
        /// </summary>
        /// <param name="entity">工作日志分享人实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(OAWorkLogShare entity)
        {
            return BoolMessage.True;
            //var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增工作日志分享人
        /// </summary>
        /// <param name="entity">工作日志分享人实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(OAWorkLogShare entity)
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
        /// 更新工作日志分享人
        /// </summary>
        /// <param name="entity">工作日志分享人实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(OAWorkLogShare entity)
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
        /// 删除工作日志分享人
        /// </summary>
        /// <param name="ids">工作日志分享人主键数组</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
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
        /// 获取工作日志分享人对象
        /// </summary>
        /// <param name="id">工作日志分享人主键</param>
        /// <returns>返回工作日志分享人对象</returns>
        public OAWorkLogShare Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取工作日志分享人列表
        /// </summary>
        /// <returns>返回工作日志分享人列表</returns>
        public List<OAWorkLogShare> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }

        #region 私有方法

        /// <summary>
        /// 获取分享给我的工作日志列表
        /// </summary>
        /// <returns>返回分享给我的工作日志列表</returns>
        public PageList<OAWorkLogExtend> GetShared(int pageIndex, int pageSize,
            string orderName, string orderDir,
            DateTime? logDateFirst, DateTime? logDateLast)
        {
            orderName = orderName.IsEmpty() ? nameof(OAWorkLogExtend.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var currentUser = OrganizeHelper.GetCurrentUser();
            var userId = currentUser.Id;
            string query = @"select WorkLogId, LogDate, ShareUserId, CreateUserName, ShareDepartmentName, IsFeedback, FeedbackInfomation, FeedbackDateTime
            from OAWorkLog, OAWorkLogShare
            where OAWorkLog.Id = OAWorkLogShare.WorkLogId and ShareUserId = "+ userId + "  Order by LogDate";
            return reposExtend.Page(pageIndex, pageSize, query);
        }

        /// <summary>
        /// 获取我分享的工作日志列表
        /// </summary>
        /// <returns>返回我分享的工作日志列表</returns>
        public PageList<OAWorkLogExtend> GetMyShareList(int pageIndex, int pageSize, 
            string orderName, string orderDir, 
            DateTime? logDateFirst, DateTime? logDateLast)
        {
            orderName = orderName.IsEmpty() ? nameof(OAWorkLogExtend.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序

            if(logDateFirst.IsEmpty() && logDateLast.IsEmpty())
            {
                var query = @"select LogDate, ShareUserId, ShareUserName, ShareDepartmentName, IsFeedback, FeedbackInfomation, FeedbackDateTime
                from OAWorkLog, OAWorkLogShare
                where OAWorkLog.Id = OAWorkLogShare.WorkLogId and WorkLogId = WorkLogId
                Order by LogDate Desc";
                return reposExtend.Page(pageIndex, pageSize, query);
            }
            else if (logDateFirst.IsNotEmpty() && logDateLast.IsNotEmpty())
            {
                var query = @"select select LogDate, ShareUserId, ShareUserName, ShareDepartmentName, IsFeedback, FeedbackInfomation, FeedbackDateTime
                from OAWorkLog, OAWorkLogShare
                where OAWorkLog.Id = OAWorkLogShare.WorkLogId and (logDateFirst < LogDate and LogDate < logDateLast)
                Order by LogDate";
                return reposExtend.Page(pageIndex, pageSize, query);
            }
            else if (logDateFirst.IsNotEmpty())
            {
                var query = @"select LogDate, ShareUserId, ShareUserName, ShareDepartmentName, IsFeedback, FeedbackInfomation, FeedbackDateTime
                from OAWorkLog, OAWorkLogShare
                where OAWorkLog.Id = OAWorkLogShare.WorkLogId and logDateFirst = LogDate";
                return reposExtend.Page(pageIndex, pageSize, query);
            }
            else if (logDateLast.IsNotEmpty())
            {
                var query = @"select select LogDate, ShareUserId, ShareUserName, ShareDepartmentName, IsFeedback, FeedbackInfomation, FeedbackDateTime
                from OAWorkExtend
                where LogDate = logDateLast";
                return reposExtend.Page(pageIndex, pageSize, query);
            }
            return null;
        }
    }

        #endregion   
}