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
    /// 未打卡记录表服务
    /// </summary>
    public class EmployeeNotClockService
    {
        /// <summary>
        /// 未打卡记录表存储器
        /// </summary>
        private readonly Repository<EmployeeNotClock> repos = new Repository<EmployeeNotClock>();

        /// <summary>
        /// 检测是否存在指定未打卡记录表
        /// </summary>
        /// <param name="entity">未打卡记录表实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(EmployeeNotClock entity)
        {
            return BoolMessage.True;
            //var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增未打卡记录表
        /// </summary>
        /// <param name="entity">未打卡记录表实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(EmployeeNotClock entity)
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
        /// 更新未打卡记录表
        /// </summary>
        /// <param name="entity">未打卡记录表实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(EmployeeNotClock entity)
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
        /// 删除未打卡记录表
        /// </summary>
        /// <param name="ids">未打卡记录表主键数组</param>
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
        /// 获取未打卡记录表对象
        /// </summary>
        /// <param name="id">未打卡记录表主键</param>
        /// <returns>返回未打卡记录表对象</returns>
        public EmployeeNotClock Get(int id)
        {
            return repos.Get(id);
        }
                
        /// <summary>
        /// 获取未打卡记录表列表
        /// </summary>
        /// <returns>返回未打卡记录表列表</returns>
        public List<EmployeeNotClock> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /// <summary>
        /// 获取未打卡记录表分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回未打卡记录表分页列表</returns>
        public PageList<EmployeeNotClock> GetPageList(int pageIndex, int pageSize, string orderName,string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(EmployeeNotClock.Id) : orderName;//默认使用主键排序
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