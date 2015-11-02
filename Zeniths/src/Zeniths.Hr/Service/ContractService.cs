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
using Zeniths.Helper;
namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 服务
    /// </summary>
    public class ContractService
    {
        /// <summary>
        /// 存储器
        /// </summary>
        private readonly Repository<Contract> repos = new Repository<Contract>();

        /// <summary>
        /// 检测是否存在指定
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(Contract entity)
        {
            //return BoolMessage.True;

            var has = repos.Exists(p =>p.Id != entity.Id);
            return has ?  BoolMessage.False: BoolMessage.True;
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }
        public BoolMessage ExistsContractNo(Contract entity)
        {
            //return BoolMessage.True;

            var has = repos.Exists(p => p.ContractNo == entity.ChangeNo && p.StateId== "Compalted");
            return has ? BoolMessage.True : new BoolMessage(false,"非法的合同编号");
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(Contract entity)
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
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(Contract entity)
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
        /// 法务部确定合同类型和级别
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateContractLevel(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.TypeName, p => p.LevelName, p => p.StepDateTime, p => p.LegalDepartmentManagerSetID, p => p.LegalDepartmentManagerSetSignDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        /// <summary>
        /// 承办部门负责人审批合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateUndertake(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p =>p.UndertakeDepartmentManagerID,p=>p.UndertakeDepartmentManagerSign,p=>p.UndertakeDepartmentManagerIsAudit,p=>p.UndertakeDepartmentManagerOpinion,p=>p.UndertakeDepartmentManagerSignDateTime, p => p.StepDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        /// <summary>
        /// 法务部负责人审批合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateLegal(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.LegalDepartmentManagerID, p => p.LegalDepartmentManagerSign, p => p.LegalDepartmentManagerIsAudit, p => p.LegalDepartmentManagerOpinion, p => p.LegalDepartmentManagerSignDateTime, p => p.StepDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        /// <summary>
        /// 财务部审批合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateFinancial(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.FinancialDepartmentManagerID, p => p.FinancialDepartmentManagerSign, p => p.FinancialDepartmentManagerIsAudit, p => p.FinancialDepartmentManagerOpinion, p => p.FinancialDepartmentManagerSignDateTime, p => p.StepDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        /// <summary>
        /// 审计巡查部审批合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateAudit(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.AuditDepartmentManagerID, p => p.AuditDepartmentManagerSign, p => p.AuditDepartmentManagerIsAudit, p => p.AuditDepartmentManagerOpinion, p => p.AuditDepartmentManagerSignDateTime, p => p.StepDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        /// <summary>
        /// 总经理审批合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateGeneral(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.GeneralManagerId, p => p.GeneralManagerSign, p => p.GeneralManagerIsAudit, p => p.GeneralManagerOpinion, p => p.GeneralManagerSignDateTime, p => p.StepDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        /// <summary>
        /// 董事长审批合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateChairman(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.ChairmanId, p => p.ChairmanSign, p => p.ChairmanIsAudit, p => p.ChairmanOpinion, p => p.ChairmanSignDateTime, p => p.StepDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        /// <summary>
        /// 董事会审批合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateBoard(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.BoardManagerID, p => p.BoardManagerSign, p => p.BoardManagerIsAudit, p => p.BoardManagerOpinion, p => p.BoardManagerSignDateTime,p=>p.MeetingAttachment, p => p.StepDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }
        /// <summary>
        /// 最后更改完善合同
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateComplated(Contract entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p =>p.ValidDate,p=> p.Amount,p=>p.Count,p=>p.Unit,p=>p.Price,p=>p.SignDate,p=>p.ReturnFileDate,p=>p.ContractNo,p=>p.StepDateTime);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">主键数组</param>
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
        /// 获取对象
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>返回对象</returns>
        public Contract Get(int id)
        {
            return repos.Get(id);
        }
                
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public List<Contract> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /*
        /// <summary>
        /// 获取启用的列表
        /// </summary>
        /// <returns>返回启用的列表</returns>
        public List<Contract> GetEnabledList()
        {
            var query = repos.NewQuery.Where(p => p.IsEnabled == true).OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }
        
        /// <summary>
        /// 获取DataTable
        /// </summary>
        /// <returns>返回DataTable</returns>
        public DataTable GetTable()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.GetTable(query);
        }
        */
        
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回分页列表</returns>
        public PageList<Contract> GetPageList(int pageIndex, int pageSize, string orderName,string orderDir, string name,DateTime sendtime_start, DateTime sendtime_end,string state,string UndertakeDepartment,string sender)
        {
            orderName = orderName.IsEmpty() ? nameof(Contract.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name));
            }
            if (sendtime_start!=null)
            {
                query.Where(p=>p.SendDateTime>= sendtime_start);
            }
            if (sendtime_end != null)
            {
                query.Where(p => p.SendDateTime <= sendtime_end);
            }
            if (state.IsNotEmpty())
            {
                query.Where(p => p.StateName == state);
            }
            if (UndertakeDepartment.IsNotEmpty())
            {
                query.Where(p => p.UndertakeDepartmentName == UndertakeDepartment);
            }
            if (sender.IsNotEmpty())
            {
                query.Where(p => p.SenderName == sender);
            }
            return repos.Page(query);
        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="name">查询关键字</param>
        /// <returns>返回分页列表</returns>
        public PageList<Contract> GetPageList_Complated(int pageIndex, int pageSize, string orderName, string orderDir, string name, DateTime sendtime_start, DateTime sendtime_end,string UndertakeDepartment, string sender)
        {
            orderName = orderName.IsEmpty() ? nameof(Contract.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            if (name.IsNotEmpty())
            {
                name = name.Trim();
                query.Where(p => p.Name.Contains(name));
            }
            if (sendtime_start != null)
            {
                query.Where(p => p.SendDateTime >= sendtime_start);
            }
            if (sendtime_end != null)
            {
                query.Where(p => p.SendDateTime <= sendtime_end);
            }
            if (UndertakeDepartment.IsNotEmpty())
            {
                query.Where(p => p.UndertakeDepartmentName == UndertakeDepartment);
            }
            if (sender.IsNotEmpty())
            {
                query.Where(p => p.SenderName == sender);
            }
            query.Where(p => p.StateId == "Compalted");
            return repos.Page(query);
        }

        #region 私有方法


        #endregion
    }
}