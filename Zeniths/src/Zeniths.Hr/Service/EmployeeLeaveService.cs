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
    /// 请休假申请服务
    /// </summary>
    public class EmployeeLeaveService
    {
        /// <summary>
        /// 请休假申请存储器
        /// </summary>
        private readonly Repository<EmployeeLeave> repos = new Repository<EmployeeLeave>();

        /// <summary>
        /// 检测是否存在指定请休假申请
        /// </summary>
        /// <param name="entity">请休假申请实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(EmployeeLeave entity)
        {
            return BoolMessage.True;
            //var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增请休假申请
        /// </summary>
        /// <param name="entity">请休假申请实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(EmployeeLeave entity)
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
        /// 更新请休假申请
        /// </summary>
        /// <param name="entity">请休假申请实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(EmployeeLeave entity)
        {
            try
            {
                //repos.Update(entity);
                repos.Update(entity, p => p.Id == entity.Id, p => p.LeaveCategory, p => p.StartDatetime, p => p.EndDatetime, p => p.Days, p => p.Reason);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新请休假工作代理人审批信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateJobAgentApproval(EmployeeLeave entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.JobAgentId, p => p.JobAgentIsAudit, p => p.JobAgentOpinion, p => p.JobAgentSign, p => p.JobAgentSignDate);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新请休假部门负责人审批信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateDepartmentManagerApproval(EmployeeLeave entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.DepartmentManagerId, p => p.DepartmentManagerIsAudit, p => p.DepartmentManagerOpinion, p => p.DepartmentManagerSign, p => p.DepartmentManagerSignDate);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新请休假总经理审批信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateGeneralManagerApproval(EmployeeLeave entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.GeneralManagerId, p => p.GeneralManagerIsAudit, p => p.GeneralManagerOpinion, p => p.GeneralManagerSign, p => p.GeneralManagerSignDate);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新请休假销假信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateCancelLeaveInfo(EmployeeLeave entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.RealStartDatetime, p => p.RealEndDatetime, p => p.ActualDays, p => p.CancelLeaveDateTime, p => p.CancelLeavePersonId, p => p.CancelLeavePersonName);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新请休假销假部门负责人审批信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateCancelLeaveDepartmentManagerApproval(EmployeeLeave entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.DepartmentManagerCancelId, p => p.DepartmentManagerCancelIsAudit, p => p.DepartmentManagerCancelOpinion, p => p.DepartmentManagerCancelSign, p => p.DepartmentManagerCancelSignDate);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新请休假销假行政人力资源部审批信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateCancelLeaveHRManagerApproval(EmployeeLeave entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.HRManagerId, p => p.HRManagerIsAudit, p => p.HRManagerOpinion, p => p.HRManagerSign, p => p.HRManagerSignDate);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除请休假申请
        /// </summary>
        /// <param name="ids">请休假申请主键数组</param>
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
        /// 获取请休假申请对象
        /// </summary>
        /// <param name="id">请休假申请主键</param>
        /// <returns>返回请休假申请对象</returns>
        public EmployeeLeave Get(int id)
        {
            return repos.Get(id);
        }
                
        /// <summary>
        /// 获取请休假申请列表
        /// </summary>
        /// <returns>返回请休假申请列表</returns>
        public List<EmployeeLeave> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取请休假申请分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="userId">当前登录用户Id</param>
        /// <returns>返回请休假申请分页列表</returns>
        public PageList<EmployeeLeave> GetPageList(int pageIndex, int pageSize, string orderName,string orderDir, int userId)
        {
            orderName = orderName.IsEmpty() ? nameof(EmployeeLeave.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());

            if (userId.IsNotEmpty())
            {
                query.Where(p => p.CreateUserId == userId);
            }

            return repos.Page(query);
        }

        
        #region 私有方法


        #endregion
    }
}