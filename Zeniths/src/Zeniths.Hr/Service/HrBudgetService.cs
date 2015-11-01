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
using Zeniths.Data.Utilities;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 部门预算服务
    /// </summary>
    public class HrBudgetService
    {
        /// <summary>
        /// 部门预算存储器
        /// </summary>
        private readonly Repository<HrBudget> repos = new Repository<HrBudget>();

        /// <summary>
        /// 检测是否存在指定部门预算
        /// </summary>
        /// <param name="entity">部门预算实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        /// 判断部门名称 年月份
        public BoolMessage Exists(HrBudget entity)
        {
            //return BoolMessage.True;
            var has = repos.Exists(p => p.BudgetDepartmentId == entity.BudgetDepartmentId && p.BudgetMonth == entity.BudgetMonth && p.Id!=entity.Id);
            return has ? new BoolMessage(false, entity.BudgetDepartmentName+" "+entity.BudgetMonth.ToString("yyyy年MM月")+" 预算申请已经提交！") : BoolMessage.True;
        }

        /// <summary>
        /// 判斷是否有明細信息存在
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage ExistsCount(HrBudget entity)
        {
            int count= repos.Database.ExecuteDataTable("select count(1) from HrBudgetDetails where BudgetId='"+entity.Id+"'").Rows[0][0].ToInt();
            return count == 0 ? new BoolMessage(false,"没有可保存的信息") : BoolMessage.True;
        }

        /// <summary>
        /// 新增部门预算
        /// </summary>
        /// <param name="entity">部门预算实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(HrBudget entity)
        {
            try
            {
                string id = repos.Insert(entity).ToString();
                return new BoolMessage(true, id);
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新部门预算
        /// </summary>
        /// <param name="entity">部门预算实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(HrBudget entity)
        {
            try
            {
                repos.Update(entity);
                return new BoolMessage(true, entity.Id.ToString());
            }
            catch (Exception e) 
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除部门预算
        /// </summary>
        /// <param name="ids">部门预算主键数组</param>
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
        /// 获取部门预算对象
        /// </summary>
        /// <param name="id">部门预算主键</param>
        /// <returns>返回部门预算对象</returns>
        public HrBudget Get(int id)
        {
            return repos.Get(id);
        }
                
        /// <summary>
        /// 获取部门预算列表
        /// </summary>
        /// <returns>返回部门预算列表</returns>
        public List<HrBudget> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /*
        /// <summary>
        /// 获取启用的部门预算列表
        /// </summary>
        /// <returns>返回启用的部门预算列表</returns>
        public List<HrBudget> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        */
        /// <summary>
        /// 获取部门预算DataTable
        /// </summary>
        /// <returns>返回部门预算DataTable</returns>
        public DataTable GetTable()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.GetTable(query);
        }
        
        
        /// <summary>
        /// 获取部门预算分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回部门预算分页列表</returns>
        public PageList<HrBudget> GetPageList(int pageIndex, int pageSize, string orderName,string orderDir, string name)
        {
            orderName = orderName.IsEmpty() ? nameof(HrBudget.Id) : orderName;//默认使用主键排序
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
        /// 查询分页信息
        /// </summary>
        /// <param name="DepartmentId">当前用户部门主键</param>
        /// <param name="type">业务类型 1 部门负责人 2 经理审批 3 财务查看 </param>
        /// <param name="year">业务申请年份</param>
        /// <param name="status">单据类型</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <returns></returns>
        public PageList<HrBudgetView> GetPageListView(int DepartmentId,string DepartmentName, string type, string year, string status, int pageIndex, int pageSize, string orderName, string orderDir)
        {
            //查询列表数据 
            IEnumerable<HrBudgetView> view = repos.Database.Query<HrBudgetView>("exec proc_GetHrBudgetPageList 'list','"+ DepartmentId.ToString()+ "','" + DepartmentName + "','" + type+"','"+year+"','"+status+"',"+pageIndex.ToString()+","+pageSize.ToString()+",'"+orderName+"','"+orderDir+"'");
            //查询数据条数
            DataSet ds = repos.Database.ExecuteDataSet("exec proc_GetHrBudgetPageList 'count', '"+ DepartmentId.ToString()+ "','"+DepartmentName+"', '"+type+"', '"+year+"', '"+status+"', "+pageIndex.ToString()+", "+pageSize.ToString()+", '"+orderName+"', '"+orderDir+"'");
            PageList <HrBudgetView> page = new PageList<HrBudgetView>(pageIndex, pageSize, (int)ds.Tables[0].Rows[0][0], view);
            return page;
        }
        #region 私有方法
        /// <summary>
        /// 部门预算 总经理审批
        /// </summary>
        /// <param name="userId">总经理主键id</param>
        /// <param name="userName">总经理姓名</param>
        /// <param name="Note">审批意见</param>
        /// <param name="Status">审批状态</param>
        /// <returns></returns>
        public BoolMessage GeneralManagerApproval(string id, string userId,string userName, string Note, string Status)
        {
            try
            {
                HrBudget model = new HrBudget();
                repos.Update(model,id,new HrBudget{GeneralManagerId =userId.ToInt(), GeneralManagerName =userName,GeneralManagerNote=Note,GeneralManagerStatus=Status.ToBool()});
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        #endregion
    }
}