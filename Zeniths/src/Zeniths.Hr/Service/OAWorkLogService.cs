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
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Helper;
using System.Text;
using Zeniths.Data.Extensions;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 工作日志服务
    /// </summary>
    public class OAWorkLogService
    {
        /// <summary>
        /// 工作日志存储器
        /// </summary>
        private readonly Repository<OAWorkLog> repos = new Repository<OAWorkLog>();
        /// <summary>
        /// 工作日志分享人存储器
        /// </summary>
        private readonly Repository<OAWorkLogShare> reposWorkShare = new Repository<OAWorkLogShare>();

        /// <summary>
        /// 检测是否存在工作日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>存在工作日志返回true</returns>
        public BoolMessage WorkLogExists(OAWorkLog entity)
        {
            var logDate = entity.LogDate.Date;
            var has = repos.Exists(p => p.LogDate == logDate && p.CreateUserId == entity.CreateUserId);
            return has ? new BoolMessage(false, "编写的工作日志已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 保存工作日志
        /// </summary>
        /// <param name="entity">工作日志实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Save(OAWorkLog entity)
        {
            var logDate = entity.LogDate.Date;
            bool has = repos.Exists(p => p.LogDate == logDate);
            bool exists = repos.Exists(p => p.Id == entity.Id);
            StringBuilder message = new StringBuilder();
            try
            {
                if(has && !exists)
                {
                    message.Append($"{DateTimeHelper.FormatDate(entity.LogDate)}日志已存在，无法保存");
                    return new BoolMessage(false, message.ToString());
                }
                if (WorkLogExists(entity).Failure)
                {
                    
                    repos.Update(entity, p => p.Id == entity.Id, p => p.WorkSummary, p => p.TomorrowPlan, p => p.OtheInstructions);
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
        /// 删除工作日志
        /// </summary>
        /// <param name="id">工作日志主键</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Delete(string id)
        {
            try
            {
                StringBuilder message = new StringBuilder();
                var idArray = StringHelper.ConvertStringToArray(id);
                for (int i = 0; i < idArray.Length; i++)
                {
                    var logId = idArray[i].ToInt();
                    var entity = repos.Get(logId, new string[] { nameof(OAWorkLog.IsShare), nameof(OAWorkLog.LogDate) });
                    if (entity.IsShare)
                    {
                        message.Append($"日志已经分享，无法删除{DateTimeHelper.FormatDate(entity.LogDate)}的日志");
                    }
                    else
                    {
                        repos.Delete(logId);
                    }
                }

                if(message.Length>0)
                {
                    return new BoolMessage(false, message.ToString());
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        public BoolMessage Exists(OAWorkLog entity)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 分享工作日志
        /// </summary>
        /// <param name="id">工作日志主键</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage ShareWorkLog(int logId, string shareUserId)
        {
            try
            {
                StringBuilder message = new StringBuilder();
                var entity = repos.Get(logId, new string[] { nameof(OAWorkLog.IsShare) });
                SystemUserService userService = new SystemUserService();
                var userArray = StringHelper.ConvertStringToArray(shareUserId);
                for (int i = 0; i < userArray.Length; i++)
                {
                    var userId = userArray[i].ToInt();
                    if (reposWorkShare.Exists(p => p.WorkLogId == logId && p.ShareUserId == userId))
                    {
                        message.Append($"日志已经分享给{userService.GetName(userId)}了!!");
                    }

                    entity.IsShare = true;
                    repos.Update(entity, p => p.Id == logId, p => p.IsShare);

                    OAWorkLogShare entitys = new OAWorkLogShare();
                    var user = userService.Get(userId);
                    entitys.ShareUserId = userId;
                    entitys.WorkLogId = logId;
                    entitys.ShareUserName = user.Name;
                    entitys.ShareDepartmentId = user.DepartmentId;
                    entitys.ShareDepartmentName = user.DepartmentName;
                    reposWorkShare.Insert(entitys);

                }

                return new BoolMessage(true, message.ToString());
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 反馈分享工作日志意见
        /// </summary>
        /// <param name="id">工作日志主键</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage FeedbackWorkLogShare(int id)
        {
            try
            {
                var entity = reposWorkShare.Get(id, new string[] { nameof(OAWorkLogShare.IsFeedback) });
                if (entity.IsFeedback)
                {
                    return new BoolMessage(false, "日志意见已反馈");
                }

                reposWorkShare.Update(entity);

                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除分享工作日志反馈的意见
        /// </summary>
        /// <param name="id">工作日志主键</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage DeleteWorkLogShare(int id)
        {
            try
            {
                var entity = reposWorkShare.Get(id, new string[] { nameof(OAWorkLogShare.FeedbackInfomation) });
                reposWorkShare.Update(entity);

                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 获取工作日志对象
        /// </summary>
        /// <param name="id">工作日志主键</param>
        /// <returns>返回工作日志对象</returns>
        public OAWorkLog Get(int id)
        {
            return repos.Get(id);
        }


        /// <summary>
        /// 获取工作日志列表
        /// </summary>
        /// <returns>返回工作日志列表</returns>
        public List<OAWorkLog> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }



        /// <summary>
        /// 获取工作日志分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回工作日志分页列表</returns>
        public PageList<OAWorkLog> GetPageList(int pageIndex, int pageSize, string orderName, string orderDir, DateTime? logDateFirst, DateTime? logDateLast)
        {
            orderName = orderName.IsEmpty() ? nameof(OAWorkLog.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());

            if (logDateFirst.IsNotEmpty() && logDateLast.IsNotEmpty())
            {
                if(logDateFirst < logDateLast)
                {
                    query.Where(p => p.LogDate.Between(logDateFirst.ToDateTime(), logDateLast.ToDateTime().AddDays(1).AddSeconds(-1)));
                }
                else
                {
                    query.Where(p => p.LogDate.Between(logDateLast.ToDateTime(), logDateFirst.ToDateTime().AddDays(1).AddSeconds(-1)));
                }
                
            }
            else if (logDateFirst.IsNotEmpty())
            {
                //DateTime logDatetime = logDate.ToDateTime();
                query.Where(p => p.LogDate == logDateFirst);
            }
            else if(logDateLast.IsNotEmpty())
            {
                //DateTime createDatetime = createDateTime.ToDateTime();
                query.Where(p => p.LogDate == logDateLast);
            }


            return repos.Page(query);
        }

        #region 私有方法

        /// <summary>
        /// 获取所有用户信息分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns>返回工作日志分页列表</returns>
        //public List<SystemUser> GetUserList(int pageIndex, int pageSize)
        //{
        //    SystemUserService userService = new SystemUserService();
        //    return userService.GetEnabledList();
        //}

        #endregion
    }
}