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
    /// 日常费用报销明细服务
    /// </summary>
    public class DailyReimburseDetailsService
    {
        /// <summary>
        /// 日常费用报销明细存储器
        /// </summary>
        private readonly Repository<DailyReimburseDetails> repos = new Repository<DailyReimburseDetails>();

        /// <summary>
        /// 检测是否存在指定日常费用报销明细
        /// </summary>
        /// <param name="entity">日常费用报销明细实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(DailyReimburseDetails entity)
        {
            return BoolMessage.True;
            //var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增日常费用报销明细
        /// </summary>
        /// <param name="entity">日常费用报销明细实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(DailyReimburseDetails entity)
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
        /// 提交发送前 更新日常费用报销明细
        /// </summary>
        /// <param name="entity">日常费用报销明细实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(DailyReimburseDetails entity)
        {
            try
            {
                DailyReimburseDetails oldEntity = Get(entity.Id);
                int count = repos.Update(entity);
                if (count > 0)
                {
                    return BoolMessage.True;
                }
                else
                {
                    repos.Update(oldEntity);
                    return BoolMessage.False;
                }
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除日常费用报销明细
        /// </summary>
        /// <param name="ids">日常费用报销明细主键数组</param>
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
        /// 获取日常费用报销明细对象
        /// </summary>
        /// <param name="id">日常费用报销明细主键</param>
        /// <returns>返回日常费用报销明细对象</returns>
        public DailyReimburseDetails Get(int id)
        {
            return repos.Get(id);
        }
                
        /// <summary>
        /// 获取日常费用报销明细列表
        /// </summary>
        /// <returns>返回日常费用报销明细列表</returns>
        public List<DailyReimburseDetails> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        /// <summary>
        /// 获取日常费用报销明细列表
        /// </summary>
        /// <param name="businessId">主业务表(预算主表)id</param>
        /// <returns></returns>
        public List<DailyReimburseDetails> GetList(int businessId)
        {
            var query = repos.NewQuery.Where(p => p.ReimburseId == businessId);
            query.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }

        /*
        /// <summary>
        /// 获取启用的日常费用报销明细列表
        /// </summary>
        /// <returns>返回启用的日常费用报销明细列表</returns>
        public List<DailyReimburseDetails> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /// <summary>
        /// 获取日常费用报销明细DataTable
        /// </summary>
        /// <returns>返回日常费用报销明细DataTable</returns>
        public DataTable GetTable()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.GetTable(query);
        }
        */

        /// <summary>
        /// 获取日常费用报销明细分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回日常费用报销明细分页列表</returns>
        public PageList<DailyReimburseDetails> GetPageList(int pageIndex, int pageSize, string orderName,string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(DailyReimburseDetails.Id) : orderName;//默认使用主键排序
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


        /// <summary>
        /// 获取预算表Id
        /// </summary>budGetRepos
        /// <param name="dpartId"></param>
        /// <returns></returns>
        public List<HrBudgetDetails> GetBudgetDetailList(int dpartId)
        {
            var curMonth = DateTime.Now.Month - 1;
            var sql = @"
                       SELECT Id,BudgetId,BudgetCategoryId,BudgetCategoryName,BudgetItemId,BudgetItemName,BudgetMoney,Note
                       FROM HrBudgetDetails WHERE BudgetId = (
                       SELECT Id FROM HrBudget WHERE BudgetDepartmentId = @dpartId AND Month(BudgetMonth) = @curMonth ) 
                    ";
            return repos.Database.Query<HrBudgetDetails>(sql, new { dpartId = dpartId, curMonth = curMonth }).ToList();
        }

        /// <summary>
        /// 判断是否在预算内
        /// </summary>
        /// <param name="businessId">主业务表(预算主表)id </param>
        /// <param name="curUserDepartId">当前登录用户部门id</param>
        /// <returns>true:预算内，flase:预算外</returns>
        public BoolMessage IsInnerBudget(int businessId,int curUserDepartId)
        {
           
            var list = GetList(businessId);
            var budList = GetBudgetDetailList(curUserDepartId);
            List<bool> results = new List<bool>();
            bool finalResult = false;
            if (list != null && budList != null)
            {
                if (list.Count > budList.Count)
                {
                    return new BoolMessage(finalResult);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    foreach (var item in budList)
                    {
                        if (list[i].CategoryId == item.BudgetCategoryId)
                        {
                            if (list[i].ItemId == item.BudgetItemId)
                            {
                                if (list[i].Amount == item.BudgetMoney)
                                {
                                    results.Add(true);
                                }
                            }
                        }
                    }

                }

                if (results.Count == list.Count)
                {
                    finalResult = true;
                }

            }
            return new BoolMessage(finalResult);
        }


        #region 私有方法


        #endregion
    }
}