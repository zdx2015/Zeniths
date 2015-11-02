﻿// ===============================================================================
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
using Zeniths.Data.Extensions;

namespace Zeniths.Hr.Service
{
    /// <summary>
    /// 加班申请服务
    /// </summary>
    public class EmployeeOvertimeService
    {
        /// <summary>
        /// 加班申请存储器
        /// </summary>
        private readonly Repository<EmployeeOvertime> repos = new Repository<EmployeeOvertime>();

        /// <summary>
        /// 检测是否存在指定加班申请
        /// </summary>
        /// <param name="entity">加班申请实体</param>
        /// <returns>如果存在指定记录返回BoolMessage.False</returns>
        public BoolMessage Exists(EmployeeOvertime entity)
        {
            return BoolMessage.True;
            //var has = repos.Exists(p => p.Name == entity.Name && p.Id != entity.Id);
            //return has ? new BoolMessage(false, "输入名称已经存在") : BoolMessage.True;
        }

        /// <summary>
        /// 新增加班申请
        /// </summary>
        /// <param name="entity">加班申请实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Insert(EmployeeOvertime entity)
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
        /// 更新加班申请
        /// </summary>
        /// <param name="entity">加班申请实体</param>
        /// <returns>执行成功返回BoolMessage.True</returns>
        public BoolMessage Update(EmployeeOvertime entity)
        {
            try
            {                
                repos.Update(entity, p => p.Id == entity.Id, p => p.Reasons, p => p.StartDatetime, p => p.EndDatetime, p => p.Days);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新加班登记信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateReg(EmployeeOvertime entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.FlowInstanceId, p => p.StepId, p => p.StepName, p => p.IsFinish, p => p.Status);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新加班部门负责人审批信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateDepartmentManagerApproval(EmployeeOvertime entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.DepartmentManagerId, p => p.DepartmentManagerIsAudit, p => p.DepartmentManagerOpinion, p => p.DepartmentManagerSign, p => p.DepartmentManagerSignDate, p => p.Status);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 更新加班总经理审批信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BoolMessage UpdateGeneralManagerApproval(EmployeeOvertime entity)
        {
            try
            {
                repos.Update(entity, p => p.Id == entity.Id, p => p.GeneralManagerId, p => p.GeneralManagerIsAudit, p => p.GeneralManagerOpinion, p => p.GeneralManagerSign, p => p.GeneralManagerSignDate, p => p.Status);
                return BoolMessage.True;
            }
            catch (Exception e)
            {
                return new BoolMessage(false, e.Message);
            }
        }

        /// <summary>
        /// 删除加班申请
        /// </summary>
        /// <param name="ids">加班申请主键数组</param>
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
        /// 获取加班申请对象
        /// </summary>
        /// <param name="id">加班申请主键</param>
        /// <returns>返回加班申请对象</returns>
        public EmployeeOvertime Get(int id)
        {
            return repos.Get(id);
        }
                
        /// <summary>
        /// 获取加班申请列表
        /// </summary>
        /// <returns>返回加班申请列表</returns>
        public List<EmployeeOvertime> GetList()
        {
            var query = repos.NewQuery.OrderBy(p => p.Id);
            return repos.Query(query).ToList();
        }

        /// <summary>
        /// 获取所有加班申请分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="employeeName">员工姓名</param>
        /// <param name="department">部门名称</param>
        /// <param name="startDatetime">加班开始时间</param>
        /// <param name="startEndDatetime">加班开始终止时间</param>
        /// <param name="applyDateTime">申请日期</param>
        /// <param name="status">状态</param>
        /// <returns>返回所有加班申请分页列表</returns>
        public PageList<EmployeeOvertime> GetPageListAll(int pageIndex, int pageSize, string orderName, string orderDir, string employeeName, string department, string startDatetime, string startEndDatetime, string applyDateTime, int? status)
        {
            orderName = orderName.IsEmpty() ? nameof(EmployeeOvertime.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            if (employeeName.IsNotEmpty())
            {
                query.Where(p => p.EmployeeName.Contains(employeeName));
            }
            if (department.IsNotEmpty())
            {
                query.Where(p => p.Deparment == department);
            }
            if (startDatetime.IsNotEmpty() && startEndDatetime.IsNotEmpty())
            {
                query.Where(p => p.StartDatetime.Between(startDatetime.ToDateTime(), startEndDatetime.ToDateTime().AddDays(1).AddSeconds(-1)));
            }
            else if (startDatetime.IsNotEmpty())
            {
                DateTime startDatetimetemp = startDatetime.ToDateTime();
                query.Where(p => p.StartDatetime >= startDatetimetemp);
            }
            else if (startEndDatetime.IsNotEmpty())
            {
                DateTime startEndDatetimetemp = startEndDatetime.ToDateTime().AddDays(1);
                query.Where(p => p.StartDatetime < startEndDatetimetemp);
            }
            if (applyDateTime.IsNotEmpty())
            {
                query.Where(p => p.ApplyDateTime.Between(applyDateTime.ToDateTime(), applyDateTime.ToDateTime().AddDays(1).AddSeconds(-1)));
            }
            if (status.HasValue)
            {
                query.Where(p => p.Status == status);
            }
            return repos.Page(query);
        }

        /// <summary>
        /// 获取待审理加班申请分页列表
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="orderName">排序列名</param>
        /// <param name="orderDir">排序方式</param>
        /// <param name="userId">当前登录用户Id</param>
        /// <param name="startDatetime">加班开始时间</param>
        /// <param name="startEndDatetime">加班开始终止时间</param>
        /// <param name="applyDateTime">申请日期</param>
        /// <param name="status">状态</param>
        /// <returns>返回待审理加班申请分页列表</returns>
        public PageList<EmployeeOvertime> GetPageListMyself(int pageIndex, int pageSize, string orderName, string orderDir, int userId, string startDatetime, string startEndDatetime, string applyDateTime, int? status)
        {
            orderName = orderName.IsEmpty() ? nameof(EmployeeOvertime.Id) : orderName;//默认使用主键排序
            orderDir = orderDir.IsEmpty() ? nameof(OrderDir.Desc) : orderDir;//默认使用倒序排序
            var query = repos.NewQuery.Take(pageSize).Page(pageIndex).OrderBy(orderName, orderDir.IsAsc());
            if (userId.IsNotEmpty())
            {
                query.Where(p => p.CreateUserId == userId);
            }
            if (startDatetime.IsNotEmpty() && startEndDatetime.IsNotEmpty())
            {
                query.Where(p => p.StartDatetime.Between(startDatetime.ToDateTime(), startEndDatetime.ToDateTime().AddDays(1).AddSeconds(-1)));
            }
            else if (startDatetime.IsNotEmpty())
            {
                DateTime startDatetimetemp = startDatetime.ToDateTime();
                query.Where(p => p.StartDatetime >= startDatetimetemp);
            }
            else if (startEndDatetime.IsNotEmpty())
            {
                DateTime startEndDatetimetemp = startEndDatetime.ToDateTime().AddDays(1);
                query.Where(p => p.StartDatetime < startEndDatetimetemp);
            }
            if (applyDateTime.IsNotEmpty())
            {
                query.Where(p => p.ApplyDateTime.Between(applyDateTime.ToDateTime(), applyDateTime.ToDateTime().AddDays(1).AddSeconds(-1)));
            }
            if (status.HasValue)
            {
                query.Where(p => p.Status == status);
            }
            return repos.Page(query);
        }

        #region 私有方法


        #endregion
    }
}