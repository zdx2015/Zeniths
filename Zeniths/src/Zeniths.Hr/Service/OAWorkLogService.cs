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
    /// 工作日志服务
    /// </summary>
    public class OAWorkLogService
    {
        /// <summary>
        /// 工作日志存储器
        /// </summary>
        private readonly Repository<OAWorkLog> repos = new Repository<OAWorkLog>();

        /// <summary>
        /// 检测是否存在指定工作日志
        /// </summary>
        /// <param name="entity">工作日志实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(OAWorkLog entity)
        {
            return BoolMessage.True;
            //var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增工作日志
        /// </summary>
        /// <param name="entity">工作日志实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(OAWorkLog entity)
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
        /// 更新工作日志
        /// </summary>
        /// <param name="entity">工作日志实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(OAWorkLog entity)
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
        /// 删除工作日志
        /// </summary>
        /// <param name="ids">工作日志主键数组</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Delete(int[] ids)
        {
            try
            {
                if (ids.Length==1)
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
        
        /*
        /// <summary>
        /// 获取启用的工作日志列表
        /// </summary>
        /// <returns>返回启用的工作日志列表</returns>
        public List<OAWorkLog> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /// <summary>
        /// 获取工作日志DataTable
        /// </summary>
        /// <returns>返回工作日志DataTable</returns>
        public DataTable GetTable()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.GetTable(query);
        }
        */
        
        /// <summary>
        /// 获取工作日志分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回工作日志分页列表</returns>
        public PageList<OAWorkLog> GetPageList(int pageIndex, int pageSize, string orderName,string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(OAWorkLog.Id) : orderName;//默认使用主键排序
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


        #endregion
    }
}