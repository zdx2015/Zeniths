// ===============================================================================
// 正得信股份 版权所有
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Zeniths.Data.Extensions;
using Zeniths.Hr.Entity;
using Zeniths.Collections;
using Zeniths.Data;
using Zeniths.Extensions;
using Zeniths.Utility;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 日常费用报销服务
    /// </summary>
    public class DailyReimburseService
    {
        /// <summary>
        /// 日常费用报销存储器
        /// </summary>
        private readonly Repository<DailyReimburse> repos = new Repository<DailyReimburse>();
        private readonly Repository<DailyReimburseDetails> detailRepos = new Repository<DailyReimburseDetails>();
      

        /// <summary>
        /// 检测是否存在指定日常费用报销
        /// </summary>
        /// <param name="entity">日常费用报销实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(DailyReimburse entity)
        {
            return BoolMessage.True;
            //var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增日常费用报销
        /// </summary>
        /// <param name="entity">日常费用报销实体</param>
        /// <param name="detailList">报销明细实体list</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(DailyReimburse entity,List<DailyReimburseDetails> detailList)
        {
            try
            {
                var recordId =repos.Insert(entity).ToInt();
                bool result = false;
                int[] ids = new int[detailList.Count];
                if (recordId>0)
                {
                    int count = 0;
                   
                    foreach(var item in detailList)
                    {
                        item.ReimburseId = recordId;
                        int detailId = detailRepos.Insert(item).ToInt();

                        if (detailId > 0)
                        {
                            ids[count] = detailId;
                            count = count + 1;
                        }
                    }
                   
                    
                    if(count==detailList.Count)
                    {
                        result = true;
                    }
                }

                //根据成功Insert明细表的数量和页面传入list数量比较，相等则返回成功。否则 删除已插入的数据并返回失败
                if (result)
                {
                    return BoolMessage.True;
                }else
                {
                    repos.Delete(recordId);
                    detailRepos.Delete(ids);
                    return BoolMessage.False;
                }
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 提交发送前 更新日常费用报销
        /// </summary>
        /// <param name="entity">日常费用报销实体</param>  
        /// <param name="detailList">报销明细实体list</param>      
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(DailyReimburse entity, List<DailyReimburseDetails> detailList)
        {
            try
            {
                var count = repos.Update(entity);

                if (count > 0)
                {
                    detailRepos.Delete(p => p.ReimburseId == entity.Id);

                    foreach (var item in detailList)
                    {
                        detailRepos.Insert(item);
                    }
                    return BoolMessage.True;
                }
                else
                {
                   
                    return BoolMessage.False;
                }
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }


        /// <summary>
        /// 提交发送后 更新日常费用报销数据
        /// </summary>
        /// <param name="entity">日常费用报销实体</param>
        /// <param name="actionFlag">提交数据的当前步骤标识--1:部门负责人，2：会计审核，3：财务负责人审核，4：总经理签字，5：董事长签字，6：出纳付款，7：(预算外)部门负责人，8：(预算外)总经理出示意见</param>
        /// <returns></returns>
        public BoolMessage Update(DailyReimburse entity,int actionFlag)
        {
            DailyReimburse oldEntity = Get(entity.Id);//取出修改前的数据
            switch (actionFlag)
            {
                case 1:
                    try
                    {
                        var count = repos.Update(entity, p => p.Id == entity.Id, p => p.DepartmentManagerId, p => p.DepartmentManagerIsAudit, p => p.DepartmentManagerOpinion, p => p.DepartmentManagerSign, p => p.DepartmentManagerSignDate);
                        if (count > 0)
                        {
                            return BoolMessage.True;
                        }
                        else
                        {
                            repos.Update(oldEntity, p => p.Id == oldEntity.Id, p => p.DepartmentManagerId, p => p.DepartmentManagerIsAudit, p => p.DepartmentManagerOpinion, p => p.DepartmentManagerSign, p => p.DepartmentManagerSignDate);
                            return BoolMessage.False;
                        }
                    }
                    catch(Exception e)
                    {
                        return new BoolMessage(false, e.Message);
                    }
                    
                case 2:
                    try
                    {
                        var count = repos.Update(entity, p => p.Id == entity.Id, p => p.AccountantId, p => p.AccountantIsAudit, p => p.AccountantOpinion, p => p.AccountantSign, p => p.AccountantSignDate);
                        if (count > 0)
                        {
                            return BoolMessage.True;
                        }
                        else
                        {
                            repos.Update(oldEntity, p => p.Id == oldEntity.Id, p => p.AccountantId, p => p.AccountantIsAudit, p => p.AccountantOpinion, p => p.AccountantSign, p => p.AccountantSignDate);
                            return BoolMessage.False;
                        }
                    }
                    catch (Exception e)
                    {
                        return new BoolMessage(false, e.Message);
                    }
                 
                case 3:
                    try
                    {
                        var count = repos.Update(entity, p => p.Id == entity.Id, p => p.FinancialManagerId, p => p.FinancialManagerIsAudit, p => p.FinancialManagerOpinion, p => p.FinancialManagerSign, p => p.FinancialManagerSignDate);
                        if (count > 0)
                        {
                            return BoolMessage.True;
                        }
                        else
                        {
                            repos.Update(oldEntity, p => p.Id == oldEntity.Id, p => p.FinancialManagerId, p => p.FinancialManagerIsAudit, p => p.FinancialManagerOpinion, p => p.FinancialManagerSign, p => p.FinancialManagerSignDate);
                            return BoolMessage.False;
                        }
                    }
                    catch (Exception e)
                    {
                        return new BoolMessage(false, e.Message);
                    }
                  
                case 4:
                    try
                    {
                        var count = repos.Update(entity, p => p.Id == entity.Id, p => p.GeneralManagerId, p => p.GeneralManagerIsAudit, p => p.GeneralManagerOpinion, p => p.GeneralManagerSign, p => p.GeneralManagerSignDate);
                        if (count > 0)
                        {
                            return BoolMessage.True;
                        }
                        else
                        {
                            repos.Update(oldEntity, p => p.Id == oldEntity.Id, p => p.GeneralManagerId, p => p.GeneralManagerIsAudit, p => p.GeneralManagerOpinion, p => p.GeneralManagerSign, p => p.GeneralManagerSignDate);
                            return BoolMessage.False;
                        }
                    }
                    catch (Exception e)
                    {
                        return new BoolMessage(false, e.Message);
                    }
                   
                case 5:
                    try
                    {
                        var count = repos.Update(entity, p => p.Id == entity.Id, p => p.ChairmanId, p => p.ChairmanIsAudit, p => p.ChairmanOpinion, p => p.ChairmanSign, p => p.ChairmanSignDate);
                        if (count > 0)
                        {
                            return BoolMessage.True;
                        }
                        else
                        {
                            repos.Update(oldEntity, p => p.Id == oldEntity.Id, p => p.ChairmanId, p => p.ChairmanIsAudit, p => p.ChairmanOpinion, p => p.ChairmanSign, p => p.ChairmanSignDate);
                            return BoolMessage.False;
                        }
                    }
                    catch (Exception e)
                    {
                        return new BoolMessage(false, e.Message);
                    }
                   
                case 6:
                    try
                    {
                        var count = repos.Update(entity, p => p.Id == entity.Id, p => p.CashierId, p => p.CashierName, p => p.CashierUpdateDateTime);
                        if (count > 0)
                        {
                            return BoolMessage.True;
                        }
                        else
                        {
                            repos.Update(oldEntity, p => p.Id == oldEntity.Id, p => p.CashierId, p => p.CashierName, p => p.CashierUpdateDateTime);
                            return BoolMessage.False;
                        }
                    }
                    catch (Exception e)
                    {
                        return new BoolMessage(false, e.Message);
                    }
                  
                case 7:
                    try
                    {
                        var count = repos.Update(entity, p => p.Id == entity.Id, p => p.AddDepartmentManagerId, p => p.AddDepartmentManagerIsAudit, p => p.AddDepartmentManagerOpinion, p => p.AddDepartmentManagerSign, p => p.AddDepartmentManagerSignDate);
                        if (count > 0)
                        {
                            return BoolMessage.True;
                        }
                        else
                        {
                            repos.Update(oldEntity, p => p.Id == oldEntity.Id, p => p.AddDepartmentManagerId, p => p.AddDepartmentManagerIsAudit, p => p.AddDepartmentManagerOpinion, p => p.AddDepartmentManagerSign, p => p.AddDepartmentManagerSignDate);
                            return BoolMessage.False;
                        }
                    }
                    catch (Exception e)
                    {
                        return new BoolMessage(false, e.Message);
                    }
                   
                case 8:
                    try
                    {
                        var count = repos.Update(entity, p => p.Id == entity.Id, p => p.AddGeneralManagerId, p => p.AddGeneralManagerIsAudit, p => p.AddGeneralManagerOpinion, p => p.AddGeneralManagerSign, p => p.AddGeneralManagerSignDate);
                        if (count > 0)
                        {
                            return BoolMessage.True;
                        }
                        else
                        {
                            repos.Update(oldEntity, p => p.Id == oldEntity.Id, p => p.AddGeneralManagerId, p => p.AddGeneralManagerIsAudit, p => p.AddGeneralManagerOpinion, p => p.AddGeneralManagerSign, p => p.AddGeneralManagerSignDate);
                            return BoolMessage.False;
                        }
                    }
                    catch (Exception e)
                    {
                        return new BoolMessage(false, e.Message);
                    }
                  
            }

            return BoolMessage.False;
        }


        /// <summary>
        /// 删除日常费用报销
        /// </summary>
        /// <param name="ids">日常费用报销主键数组</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Delete(int[] ids)
        {
            try
            {
                if (ids.Length==1)
                {
                    repos.Delete(ids[0]);
                    int m = ids[0];
                    detailRepos.Delete(p => p.ReimburseId == m);

                }
                else
                {
                    repos.Delete(ids);
                    detailRepos.Delete(p => p.ReimburseId.In(ids));
                }
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        
        /// <summary>
        /// 获取日常费用报销对象
        /// </summary>
        /// <param name="id">日常费用报销主键</param>
        /// <returns>返回日常费用报销对象</returns>
        public DailyReimburse Get(int id)
        {
            return repos.Get(id);
        }

        /// <summary>
        /// 获取费用报销单顺序号(主体部分)
        /// </summary>
        /// <returns></returns>
        public string GetYearAndMonthString()
        {
            var temDate = DateTime.Now;
            var sYear = temDate.Year.ToString().Substring(2, 2);
            var sMonth = temDate.Month.ToString();
            if (sMonth.Length == 1)
            {
                sMonth = sMonth.PadLeft(1, '0');
            }
            return sYear + sMonth;
        }
                
        /// <summary>
        /// 获取日常费用报销列表
        /// </summary>
        /// <returns>返回日常费用报销列表</returns>
        public List<DailyReimburse> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /*
        /// <summary>
        /// 获取启用的日常费用报销列表
        /// </summary>
        /// <returns>返回启用的日常费用报销列表</returns>
        public List<DailyReimburse> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /// <summary>
        /// 获取日常费用报销DataTable
        /// </summary>
        /// <returns>返回日常费用报销DataTable</returns>
        public DataTable GetTable()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.GetTable(query);
        }
        */
        
        /// <summary>
        /// 获取日常费用报销分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回日常费用报销分页列表</returns>
        public PageList<DailyReimburse> GetPageList(int pageIndex, int pageSize, string orderName,string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(DailyReimburse.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.ReimburseDepartmentName.Contains(name)||p.ApplyOpinion.Contains(name));
            }
            
            return repos.Page(query);
        }

        #region 私有方法


        #endregion
    }
}