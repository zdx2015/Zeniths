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
using System.Text;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 预算明细信息服务
    /// </summary>
    public class HrBudgetDetailsService
    {
        /// <summary>
        /// 预算明细信息存储器
        /// </summary>
        private readonly Repository<HrBudgetDetails> repos = new Repository<HrBudgetDetails>();

        /// <summary>
        /// 检测是否存在指定预算明细信息
        /// </summary>
        /// <param name="entity">预算明细信息实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(HrBudgetDetails entity)
        {
            //return BoolMessage.True; 
            var has = repos.Exists(p => p.BudgetItemId == entity.BudgetItemId && p.BudgetId == entity.BudgetId && p.Id!=entity.Id);
            return has ? new BoolMessage(false, "输入项目名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增预算明细信息
        /// </summary>
        /// <param name="entity">预算明细信息实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(HrBudgetDetails entity)
        {
            try
            {
                BoolMessage exm = Exists(entity);
                if (!exm.Success)
                {
                    return exm;
                }
                repos.Insert(entity);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新预算明细信息
        /// </summary>
        /// <param name="entity">预算明细信息实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(HrBudgetDetails entity)
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
        /// 删除预算明细信息
        /// </summary>
        /// <param name="ids">预算明细信息主键数组</param>
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
        /// 获取预算明细信息对象
        /// </summary>
        /// <param name="id">预算明细信息主键</param>
        /// <returns>返回预算明细信息对象</returns>
        public HrBudgetDetails Get(int id)
        {
            return repos.Get(id);
        }
                
        /// <summary>
        /// 获取预算明细信息列表
        /// </summary>
        /// <returns>返回预算明细信息列表</returns>
        public List<HrBudgetDetails> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }

        /*
        /// <summary>
        /// 获取启用的预算明细信息列表
        /// </summary>
        /// <returns>返回启用的预算明细信息列表</returns>
        public List<HrBudgetDetails> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /// <summary>
        /// 获取预算明细信息DataTable
        /// </summary>
        /// <returns>返回预算明细信息DataTable</returns>
        public DataTable GetTable()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.GetTable(query);
        }
        */

        /// <summary>
        /// 获取预算明细信息分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回预算明细信息分页列表</returns>
        public PageList<HrBudgetDetails> GetPageList(int pageIndex, int pageSize, string orderName, string orderDir, int BudgetId)
        {
            orderName = orderName.IsEmpty() ? nameof(HrBudgetDetails.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            query.Where(p => p.BudgetId ==  BudgetId);
            return repos.Page(query);
        }
        #region 私有方法
        /// <summary>
        /// 加载费用类别
        /// </summary>
        /// <param name="ParentId">分类类别 1 基类 2 子类</param>
        /// <param name="id">类别主键</param>
        /// <param name="Selected">选中id</param>
        /// <returns></returns>
        public string GetBudgetCategory(string ParentId,string id,string Selected)
        {
            DataTable dt = repos.Database.ExecuteDataTable("exec proc_GetBudgetCategory '"+ParentId+"','"+id+"','"+Selected+"'");
            string json = DataTableJson(dt);
            return json;
        }
        public static string DataTableJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            return jsonBuilder.ToString();
        }

        #endregion
    }
}