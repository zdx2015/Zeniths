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


        ///// <summary>
        ///// 获取启用的工作日志分享人列表
        ///// </summary>
        ///// <returns>返回启用的工作日志分享人列表</returns>
        //public List<OAWorkLogShare> GetEnabledList()
        //{
        //    OAWorkLog entity = new OAWorkLog();
        //    var query = repos.NewQuery.Where(p => p.ShareUserName == entity.CreateDepartmentName).OrderBy(p => p.Id);
        //    return repos.Query(query).ToList();
        //}

        ///// <summary>
        ///// 获取工作日志分享人DataTable
        ///// </summary>
        ///// <returns>返回工作日志分享人DataTable</returns>
        //public DataTable GetTable()
        //{
        //    var query = repos.NewQuery.OrderBy(p => p.Id);
        //    return repos.GetTable(query);
        //}


        /// <summary>
        /// 获取工作日志分享人分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回工作日志分享人分页列表</returns>
        public PageList<OAWorkLogShare> GetPageList(int pageIndex, int pageSize, string orderName, string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(OAWorkLogShare.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            /*
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name));
            }
            */
            return repos.Page(query);
        }

        #region 私有方法
      
        /// <summary>
        /// 链表查询获得分享给我的工作日志DataTable
        ///</summary>
        /// <returns>返回分享给我的工作日志DataTable</returns>
        public DataTable GetShared()
        {
            string sqlstr = @"select LogDate, ShareUserId, ShareUserName, ShareDepartmentName, IsFeedback, FeedbackInfomation, FeedbackDateTime
            from [Zeniths.HR].[dbo].[OAWorkLog], OAWorkLogShare
            where [Zeniths.HR].[dbo].[OAWorkLog].Id = OAWorkLogShare.WorkLogId and ShareUserName = CreateUserName
            Order by LogDate Desc";
            var db = repos.Database;
            DataTable dt = db.ExecuteDataTable(sqlstr);
            return dt;
        }

        /// <summary>
        /// 获取我分享的工作日志列表
        /// </summary>
        /// <returns>返回我分享的工作日志列表</returns>
        public PageList<OAWorkLogExtend> GetMyShareList(int pageIndex, int pageSize, string orderName, string orderDir)
        {
            orderName = orderName.IsEmpty() ? nameof(OAWorkLogExtend.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序

            var querySql = @"select LogDate, ShareUserId, ShareUserName, ShareDepartmentName, IsFeedback, FeedbackInfomation, FeedbackDateTime
            from[Zeniths.HR].[dbo].[OAWorkLog], [Zeniths.HR].[dbo].[OAWorkLogShare]
            where[Zeniths.HR].[dbo].[OAWorkLog].Id = [Zeniths.HR].[dbo].[OAWorkLogShare].WorkLogId and year(LogDate) / month(LogDate) = year(getdate()) / month(getdate())
            Order by LogDate Desc";

            var list = reposExtend.Database.Query<OAWorkLogExtend>(querySql).ToList();
            var query = reposExtend.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());

            return reposExtend.Page(query);
        }
    }

        #endregion   
}